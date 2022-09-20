using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricLogic.ChartData
{
    public class ChartScanData : ChartData
    {
        private static readonly int pixelCount = 128;
        private static readonly int RESET_VALUE = 255;
        private int currentPixel = 0;
        public ChartScanData()
        {
            rawData = new List<int>(new int[pixelCount+1]);
        }
        public override void AddData(int value)
        {
            if (currentPixel > pixelCount)
                currentPixel = 0;

            if (value == RESET_VALUE)
            {
                currentPixel = 0;
            }

            rawData[currentPixel++] = value;
        }
        public override Dictionary<int, double> GetChartData()
        {
            Dictionary<int,double> chartValues = new Dictionary<int,double>();
            int index = 0;
            rawData.ForEach(item => chartValues.Add(index++, item));
            return chartValues;
        }
    }
}
