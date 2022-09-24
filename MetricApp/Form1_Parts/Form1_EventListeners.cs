using MetricLogic.Helpers;
using MetricLogic.Serial.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricApp
{
    public partial class Form1 : Form, ISerialStateListener, IBoardModeListener, IRawSerialListener, IBoardDataListener
    {

        private void callWithInvoke<T>(Action<T> action, T value)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(action, new object[] { value });
            }
            else
            {
                action(value);
            }
        }

        public void OnSerialStateChange(SerialStateEnum state)
        {
            callWithInvoke(displaySerialState, state);
        }
        private void displaySerialState(SerialStateEnum state)
        {
            switch (state)
            {
                case SerialStateEnum.disconnected:
                    displayConnectionState(is_connected: false);
                    break;
                case SerialStateEnum.connecting:
                    connectBtn.Enabled = false;
                    break;
                case SerialStateEnum.connected:
                    displayConnectionState(is_connected: true);
                    break;
            }
        }

        public void OnBoardModeChanged(BoardModeEnum mode)
        {
            callWithInvoke(displayBoardMode, mode);
        }
        private void displayBoardMode(BoardModeEnum mode)
        {
            modeIdleRadio.Font = new Font(modeIdleRadio.Font, FontStyle.Regular);
            modeReadRadio.Font = new Font(modeReadRadio.Font, FontStyle.Regular);
            modeScanRadio.Font = new Font(modeScanRadio.Font, FontStyle.Regular);
            switch (mode)
            {
                case BoardModeEnum.idle:
                    modeIdleRadio.Font = new Font(modeIdleRadio.Font, FontStyle.Bold);
                    modeIdleRadio.Checked = true;
                    break;
                case BoardModeEnum.read:
                    modeReadRadio.Font = new Font(modeReadRadio.Font, FontStyle.Bold);
                    modeReadRadio.Checked = true;
                    break;
                case BoardModeEnum.scan:
                    modeScanRadio.Font = new Font(modeScanRadio.Font, FontStyle.Bold);
                    modeScanRadio.Checked = true;
                    break;
            }
        }

        public void OnSerialByte(int value)
        {
            callWithInvoke(appendRawSerialLog, value);
        }
        private void appendRawSerialLog(int value)
        {
            rawSerialLog.Text = value.ToString() + "\n" + rawSerialLog.Text;
            if(rawSerialLog.Text.Length > 800)
                rawSerialLog.Text = rawSerialLog.Text.Substring(0, 200);
        }

        public void OnNewReading(int reading)
        {
            callWithInvoke(addReading, reading);
        }
        private void addReading(int reading)
        {
            readingChartData.AddData(reading);
            refreshChart();
            refreshDataInfo();
        }

        public void OnNewScan(int value)
        {
            callWithInvoke(addNewScanValue, value);
        }
        public void addNewScanValue(int value)
        {
            scanChartData.AddData(value);
            refreshChart();
        }
    }
}
