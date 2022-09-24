using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace MetricApp
{
    public partial class Form1 : Form
    {
        private void InitializeCustomComponents()
        {
            initChart();
        }

        private void initChart()
        {
            ChartArea chartArea = new ChartArea();
            Series dataSeries = new Series();
            this.dataChart = new Chart();

            this.dataChart.Location = new System.Drawing.Point(0, 10);
            this.dataChart.Size = this.chartBox.Size;
            chartArea.AxisX.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            chartArea.AxisX.LineColor = System.Drawing.Color.Black;
            chartArea.AxisX.ScrollBar.LineColor = System.Drawing.Color.Black;
            chartArea.AxisX.ScrollBar.Size = 10;
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea.AxisY.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            chartArea.AxisY.LineColor = System.Drawing.Color.Black;
            chartArea.AxisY.IsStartedFromZero = false;
            chartArea.Name = "Default";
            chartArea.BackColor = System.Drawing.Color.Transparent;
            this.dataChart.BackColor = System.Drawing.Color.Transparent;
            dataSeries.ChartArea = "Default";
            dataSeries.ChartType = SeriesChartType.Line;
            dataSeries.Name = "dataSeries";

            this.dataChart.Series.Add(dataSeries);
            this.dataChart.ChartAreas.Add(chartArea);
            this.dataChart.TabIndex = 0;

            this.chartBox.Controls.Add(this.dataChart);
        }
    }
}
