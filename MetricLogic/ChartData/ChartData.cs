using MetricLogic.Helpers;
using MetricLogic.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricLogic.ChartData
{
    public abstract class ChartData
    {
        internal List<int> rawData;

        public abstract Dictionary<int, double> GetChartData();

        public abstract void AddData(int value);

        public int GetCount()
        {
            return rawData.Count;
        }

        public void Clear()
        {
            rawData.Clear();
        }

        public int GetLastRawReading()
        {
            if (rawData.Count > 0)
                return rawData.Last();
            else
                return 0;
        }

        public void SaveToFile(string filePath)
        {
            FileIO.SerializeToFile(filePath, this.rawData);
        }

        public void LoadFromFile(string filePath)
        {
            string contents = FileIO.ReadTxt(filePath);

            rawData = JsonConvert.DeserializeObject<List<int>>(contents);

            FileIO.WriteTxt(filePath, contents);
        }
    }
}
