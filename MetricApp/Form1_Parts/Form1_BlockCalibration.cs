using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricApp
{
    public partial class Form1 : Form //Calibration
    {
        private void refreshCalibration()
        {
            this.scalingInput.Value = (decimal)readingChartData.Calibrator.Scaling;
            this.offsetInput.Value = (decimal)readingChartData.Calibrator.Offset;
            this.smoothingInput.Value = (decimal)readingChartData.Calibrator.Smoothing;
        }
        private void calibSaveBtn_Click(object sender, EventArgs e)
        {
            readingChartData.Calibrator.SaveDefault();
        }

        private void portCombo_DropDown(object sender, EventArgs e)
        {
            refreshSerialPorts();
        }

        private void calibLoadBtn_Click(object sender, EventArgs e)
        {
            readingChartData.Calibrator.LoadDefault();
            refreshCalibration();
        }

        private void scalingInput_ValueChanged(object sender, EventArgs e)
        {
            readingChartData.Calibrator.Scaling = (double)scalingInput.Value;
            refreshChart();
        }

        private void offsetInput_ValueChanged(object sender, EventArgs e)
        {
            readingChartData.Calibrator.Offset = (double)offsetInput.Value;
            refreshChart();
        }

        private void smoothingInput_ValueChanged(object sender, EventArgs e)
        {
            readingChartData.Calibrator.Smoothing = (int)smoothingInput.Value;
            refreshChart();
        }

        private void calibBtn_Click(object sender, EventArgs e)
        {
            readingChartData.Calibrator.SetFromReading((double)calibReadInput.Value, readingChartData.GetLastRawReading());
            refreshCalibration();
        }
    }
}
