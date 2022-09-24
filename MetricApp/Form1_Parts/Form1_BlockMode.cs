using MetricLogic.Helpers;
using MetricLogic.Serial.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricApp
{
    public partial class Form1 : Form //Mode
    {
        private void modeIdleRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (modeIdleRadio.Checked)
                connection.Send(SerialHeaderEnum.MODE, (int)BoardModeEnum.idle);
            refreshChart();
        }

        private void modeReadRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (modeReadRadio.Checked)
                connection.Send(SerialHeaderEnum.MODE, (int)BoardModeEnum.read);
        }

        private void modeScanRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (modeScanRadio.Checked)
                connection.Send(SerialHeaderEnum.MODE, (int)BoardModeEnum.scan);
        }
    }
}
