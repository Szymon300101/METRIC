using MetricLogic.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricLogic
{
    public class ReadingCalibrator
    {
        public double Scaling { get; set; }
        public double Offset { get; set; }

        private static readonly string defaultPath = FileIO.appPath + "metricCalibration.json";

        public ReadingCalibrator()
        {
            Scaling = 0.008;
            Offset = 0;
        }

        public double Calibrate(int reading)
        {
            return (reading * Scaling) + Offset;
        }

        public List<double> Calibrate(List<int> readings)
        {
            List<double> calibrated = new List<double>();
            foreach (var item in readings)
            {
                calibrated.Add(this.Calibrate(item));
            }
            return calibrated;
        }

        public void SetFromReading(double calValue, int reading)
        {
            Offset = calValue - (reading * Scaling);
        }

        public void LoadDefault()
        {
            if(File.Exists(defaultPath))
                loadFromFile(defaultPath);
        }

        public void SaveDefault()
        {
            saveToFile(defaultPath);
        }

        private void loadFromFile(string fileName)
        {
            string contents = FileIO.ReadTxt(fileName);

            ReadingCalibrator readingCalibrator = JsonConvert.DeserializeObject<ReadingCalibrator>(contents);

            this.Scaling = readingCalibrator.Scaling;
            this.Offset = readingCalibrator.Offset;
        }

        private void saveToFile(string filePath)
        {
            string contents = JsonConvert.SerializeObject(this, Formatting.Indented);

            FileIO.WriteTxt(filePath, contents);
        }


    }
}
