using MetricLogic.Serial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricApp
{
    public partial class Form1 : Form //Connection
    {
        private void displayConnectionState(bool is_connected)
        {
            disconnectBtn.Visible = is_connected;
            disconnectBtn.Enabled = is_connected;
            modeIdleRadio.Enabled = is_connected;
            modeReadRadio.Enabled = is_connected;
            modeScanRadio.Enabled = is_connected;

            connectBtn.Visible = !is_connected;
            connectBtn.Enabled = !is_connected;
            portCombo.Enabled = !is_connected;
        }

        private void refreshSerialPorts()
        {
            portCombo.Items.Clear();
            foreach (string s in SerialConnection.GetPortNames())
            {
                portCombo.Items.Add(s);
            }
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            connection.Connect(portCombo.Text);
        }

        private void disconnectBtn_Click(object sender, EventArgs e)
        {
            connection.Disconnect();
            disconnectBtn.Enabled = false;
        }
    }
}
