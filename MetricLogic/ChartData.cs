using MetricLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricLogic
{
    public class ChartData
    {
        public ReadingCalibrator Calibrator { get; private set; }
        private List<int> rawData;

        private static readonly int ptsToShow = 50;

        public ChartData()
        {
            Calibrator = new ReadingCalibrator();
            rawData = new List<int>();
        }

        public void AddReading(int reading)
        {
            rawData.Add(reading);
        }

        public int GetLastRawReading()
        {
            if(rawData.Count > 0)
                return rawData.Last();
            else
                return 0;
        }

        public Dictionary<int, double> GetChartData()
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

        public int GetCount()
        {
            return rawData.Count;
        }

        public void Clear()
        {
            rawData.Clear();
        }
    }
}
