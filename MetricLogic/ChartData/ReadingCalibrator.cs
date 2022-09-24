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
        public int Smoothing { get; set; }

        private static readonly string defaultPath = FileIO.appPath + "metricCalibration.json";

        public ReadingCalibrator()
        {
            Scaling = 0.008;
            Offset = 0;
            Smoothing = 1;
        }

        public List<double> SmoothAndCalibrate(List<int> readings)
        {
            List<double> calibrated = new List<double>();
            for (int i = 0; i < readings.Count; i++)
            {
                calibrated.Add(SmoothAndCalibrate(readings,i));
            }
            return calibrated;
        }

        public double SmoothAndCalibrate(List<int> allData, int indexToSmooth)
        {
            return calibrate(smooth(allData,indexToSmooth));
        }

        private double smooth(List<int> allData, int indexToSmooth)
        {
            List<int> history = allData.Take(indexToSmooth).ToList();
            return runningAvg(allData[indexToSmooth], history);
        }

        public double runningAvg(int value, List<int> history)
        {
            if (history.Count == 0)
                return value;

            int runningAvgSize = Math.Min(Smoothing, history.Count);

            double sum = value;
            for (int i = 0; i < runningAvgSize-1; i++)
            {
                sum += history[history.Count - i - 1];
            }
            return sum / runningAvgSize;
        }

        private double calibrate(double reading)
        {
            return (reading * Scaling) + Offset;
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
            this.Smoothing = readingCalibrator.Smoothing;
        }

        private void saveToFile(string filePath)
        {
            string contents = JsonConvert.SerializeObject(this, Formatting.Indented);

            FileIO.WriteTxt(filePath, contents);
        }


    }
}
