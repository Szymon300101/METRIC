using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricApp
{
    public partial class Form1 : Form //Data
    {
        private void refreshDataInfo()
        {
            dataInfoLabel.Text = $"Count: {readingChartData.GetCount()}";
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            readingChartData.Clear();
            refreshDataInfo();
            refreshChart();
        }

        private void dataSaveRawBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.AddExtension = true;
            dlg.Filter = "JSON file (*.json)|*.json";
            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                dataSaveRawBtn.Enabled = false;
                readingChartData.SaveToFile(dlg.FileName);
                dataSaveRawBtn.Enabled = true;
            }
        }

        private void dataSaveCalibBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.AddExtension = true;
            dlg.Filter = "CSV file (*.csv)|*.csv|JSON file (*.json)|*.json";
            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                dataSaveCalibBtn.Enabled = false;
                readingChartData.SaveCalibratedToFile(dlg.FileName);
                dataSaveCalibBtn.Enabled = true;
            }
        }

        private void dataLoadBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.AddExtension = true;
            dlg.Filter = "Data file (*.json;*.txt)|*.json;*.txt";
            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                dataLoadBtn.Enabled = false;
                readingChartData.LoadFromFile(dlg.FileName);
                refreshChart();
                refreshDataInfo();
                dataLoadBtn.Enabled = true;
            }
        }
    }
}
