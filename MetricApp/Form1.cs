using MetricLogic;
using MetricLogic.Helpers;
using MetricLogic.Serial;
using MetricLogic.Serial.Helpers;
using MetricLogic.Serial.Models;
using System.Windows.Forms.DataVisualization.Charting;

namespace MetricApp
{
    public partial class Form1 : Form, ISerialStateListener, IBoardModeListener, IRawSerialListener, IReadingsListener
    {
        private static readonly int BAUD_RATE = 115200;

        private delegate void SerialStateCallback(SerialStateEnum state);
        private delegate void BoardModeCallback(BoardModeEnum mode);
        private delegate void RawSerialCallback(int value);
        private delegate void ReadingsCallback(int value);

        SerialConnection connection;
        BoardMenager board;
        ChartData chartData;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();

            connection = new SerialConnection();
            board = new BoardMenager(connection);
            chartData = new ChartData();
            board.AddModeListener(this);
            board.AddReadingsListener(this);
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
            //chartData.Calibrator.Scaling = (double)this.scalingInput.Value;
            //chartData.Calibrator.Offset = (double)this.offsetInput.Value;
            //chartData.Calibrator.SaveDefault();
            chartData.Calibrator.LoadDefault();
            refreshCalibration();
        }

        private void refreshCalibration()
        {
            this.scalingInput.Value = (decimal)chartData.Calibrator.Scaling;
            this.offsetInput.Value = (decimal)chartData.Calibrator.Offset;
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
        public void OnNewReading(int reading)
        {
            if (this.rawSerialLog.InvokeRequired)
            {
                ReadingsCallback d = new ReadingsCallback(addReading);
                this.Invoke(d, new object[] { reading });
            }
            else
            {
                addReading(reading);
            }
        }

        private void addReading(int reading)
        {
            chartData.AddReading(reading);
            refreshChart();
            refreshDataInfo();
        }

        private void refreshChart()
        {
            Dictionary<int, double> data = chartData.GetChartData();

            dataChart.Series["dataSeries"].Points.Clear();

            foreach (var item in data)
            {
                dataChart.Series["dataSeries"].Points.Add(new DataPoint(item.Key, item.Value));
            }

        }

        private void refreshDataInfo()
        {
            dataInfoLabel.Text = $"Count: {chartData.GetCount()}";
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

        private void clearBtn_Click(object sender, EventArgs e)
        {
            chartData.Clear();
            refreshDataInfo();
            refreshChart();
        }

        private void calibSaveBtn_Click(object sender, EventArgs e)
        {
            chartData.Calibrator.SaveDefault();
        }

        private void calibLoadBtn_Click(object sender, EventArgs e)
        {
            chartData.Calibrator.LoadDefault();
            refreshCalibration();
        }

        private void scalingInput_ValueChanged(object sender, EventArgs e)
        {
            chartData.Calibrator.Scaling = (double)scalingInput.Value;
        }

        private void offsetInput_ValueChanged(object sender, EventArgs e)
        {
            chartData.Calibrator.Offset = (double)offsetInput.Value;
        }

        private void InitializeCustomComponents()
        {
            // 
            // chart1
            // 
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

        private void calibBtn_Click(object sender, EventArgs e)
        {
            chartData.Calibrator.SetFromReading((double)calibReadInput.Value, chartData.GetLastRawReading());
            refreshCalibration();
        }
    }
}