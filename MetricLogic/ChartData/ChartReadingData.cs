using MetricLogic.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricLogic.ChartData
{
    public class ChartReadingData : ChartData
    {
        public ReadingCalibrator Calibrator { get; private set; }
        private static readonly int ptsToShow = 50;
        public ChartReadingData()
        {
            Calibrator = new ReadingCalibrator();
            rawData = new List<int>();
        }

        public override void AddData(int value)
        {
            rawData.Add(value);
        }

        public override Dictionary<int, double> GetChartData()
        {
            Dictionary<int, double> readyData = new Dictionary<int, double>();

            int dataCount = rawData.Count;

            for (int i = 0; i < dataCount; i++)
            {
                readyData.Add(dataCount - i, Calibrator.Calibrate(rawData[dataCount - i - 1]));
                if (i > ptsToShow)
                    break;
            }

            return readyData;
        }

        public void SaveCalibratedToFile(string fileName)
        {
            if (fileName.EndsWith(".csv"))
                saveCalibratedToCsv(fileName);
            else
                saveCalibratedToJson(fileName);
        }

        private void saveCalibratedToJson(string filePath)
        {
            FileIO.SerializeToFile(filePath, Calibrator.Calibrate(rawData));
        }

        private void saveCalibratedToCsv(string filePath)
        {
            string contents = "";
            int index = 1;
            foreach (var item in Calibrator.Calibrate(rawData))
            {
                contents += (index++).ToString() + "; ";
                contents += item.ToString("0.####") + "\n";
            }

            FileIO.WriteTxt(filePath, contents);
        }
    }
}
