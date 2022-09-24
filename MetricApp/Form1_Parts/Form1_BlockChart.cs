using MetricLogic.ChartData;
using MetricLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace MetricApp
{
    public partial class Form1 : Form //Chart
    {
        ChartReadingData readingChartData;
        ChartScanData scanChartData;

        private void refreshChart()
        {
            Dictionary<int, double> data = getDataForChart();

            insertToChart(data);
        }

        private Dictionary<int, double> getDataForChart()
        {
            switch (board.Mode)
            {
                case BoardModeEnum.read:
                    return readingChartData.GetChartData();
                case BoardModeEnum.scan:
                    return scanChartData.GetChartData();
                case BoardModeEnum.idle:
                    return readingChartData.GetChartData();
                default:
                    return new Dictionary<int, double>();
            }
        }

        private void insertToChart(Dictionary<int, double> data)
        {
            dataChart.Series["dataSeries"].Points.Clear();

            foreach (var item in data)
            {
                dataChart.Series["dataSeries"].Points.Add(new DataPoint(item.Key, item.Value));
            }
        }
    }
}
