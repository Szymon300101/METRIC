using MetricLogic;
using MetricLogic.Helpers;
using MetricLogic.Serial;
using MetricLogic.Serial.Helpers;
using MetricLogic.Serial.Models;

namespace MetricApp
{
    public partial class Form1 : Form, ISerialStateListener, IBoardModeListener, IRawSerialListener
    {
        private static readonly int BAUD_RATE = 115200;

        private delegate void SerialStateCallback(SerialStateEnum state);
        private delegate void BoardModeCallback(BoardModeEnum mode);
        private delegate void RawSerialCallback(int value);

        SerialConnection connection;
        BoardMenager board;

        public Form1()
        {
            InitializeComponent();

            connection = new SerialConnection();
            board = new BoardMenager(connection);
            board.AddModeListener(this);
            connection.AddStateListener(this);
            connection.AddSerialListener(board);
            connection.AddRawSerialListener(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string s in SerialConnection.GetPortNames())
            {
                portCombo.Items.Add(s);
            }
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            connection.Connect(portCombo.Text, BAUD_RATE);

        }

        private void displaySerialState(SerialStateEnum state)
        {
            switch (state)
            {
                case SerialStateEnum.disconnected:
                    disconnectBtn.Visible = false;
                    pingBtn.Visible = false;
                    connectBtn.Visible = true;
                    connectBtn.Enabled = true;
                    portCombo.Enabled = true;
                    break;
                case SerialStateEnum.connecting:
                    connectBtn.Enabled = false;
                    portCombo.Enabled = false;
                    break;
                case SerialStateEnum.connected:
                    disconnectBtn.Visible = true;
                    disconnectBtn.Enabled = true;
                    pingBtn.Visible = true;
                    connectBtn.Visible = false;
                    break;
            }
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

        public void OnSerialStateChange(SerialStateEnum state)
        {
            if (this.rawSerialLog.InvokeRequired)
            {
                SerialStateCallback d = new SerialStateCallback(displaySerialState);
                this.Invoke(d, new object[] { state });
            }
            else
            {
                displaySerialState(state);
            }
        }

        public void OnBoardModeChanged(BoardModeEnum mode)
        {
            if (this.rawSerialLog.InvokeRequired)
            {
                BoardModeCallback d = new BoardModeCallback(displayBoardMode);
                this.Invoke(d, new object[] { mode });
            }
            else
            {
                displayBoardMode(mode);
            }

        }

        public void OnSerialByte(int value)
        {
            if (this.rawSerialLog.InvokeRequired)
            {
                RawSerialCallback d = new RawSerialCallback(appendRawSerialLog);
                this.Invoke(d, new object[] { value });
            }
            else
            {
                appendRawSerialLog(value);
            }
        }

        private void appendRawSerialLog(int value)
        {
            rawSerialLog.Text = value.ToString() + "\n" + rawSerialLog.Text;
        }

        private void modeIdleRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (modeIdleRadio.Checked)
                connection.Send(SerialHeaderEnum.MODE, (int)BoardModeEnum.idle);
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.Disconnect();
        }

        private void disconnectBtn_Click(object sender, EventArgs e)
        {
            connection.Disconnect();
            disconnectBtn.Enabled = false;
        }
    }
}