using MetricLogic;
using MetricLogic.ChartData;
using MetricLogic.Helpers;
using MetricLogic.Serial;
using MetricLogic.Serial.Helpers;
using MetricLogic.Serial.Models;
using System.Windows.Forms.DataVisualization.Charting;

namespace MetricApp
{
    public partial class Form1 : Form, ISerialStateListener, IBoardModeListener, IRawSerialListener, IBoardDataListener
    {
        private static readonly int BAUD_RATE = 115200;

        private delegate void SerialStateCallback(SerialStateEnum state);
        private delegate void BoardModeCallback(BoardModeEnum mode);
        private delegate void RawSerialCallback(int value);
        private delegate void BoardDataCallback(int value);

        SerialConnection connection;
        BoardMenager board;
        ChartReadingData readingChartData;
        ChartScanData scanChartData;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();

            connection = new SerialConnection();
            board = new BoardMenager(connection);
            readingChartData = new ChartReadingData();
            scanChartData = new ChartScanData();
            board.AddModeListener(this);
            board.AddReadingsListener(this);
            connection.AddStateListener(this);
            connection.AddSerialListener(board);
            connection.AddRawSerialListener(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            showConnectionState(is_connected: false);
            readingChartData.Calibrator.LoadDefault();
            refreshCalibration();
        }

        private void refreshCalibration()
        {
            this.scalingInput.Value = (decimal)readingChartData.Calibrator.Scaling;
            this.offsetInput.Value = (decimal)readingChartData.Calibrator.Offset;
            this.smoothingInput.Value = (decimal)readingChartData.Calibrator.Smoothing;
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            connection.Connect(portCombo.Text, BAUD_RATE);

        }

        private void portCombo_DropDown(object sender, EventArgs e)
        {
            refreshSerialPorts();
        }

        private void refreshSerialPorts()
        {
            portCombo.Items.Clear();
            foreach (string s in SerialConnection.GetPortNames())
            {
                portCombo.Items.Add(s);
            }
        }

        private void displaySerialState(SerialStateEnum state)
        {
            switch (state)
            {
                case SerialStateEnum.disconnected:
                    showConnectionState(is_connected: false);
                    break;
                case SerialStateEnum.connecting:
                    connectBtn.Enabled = false;
                    break;
                case SerialStateEnum.connected:
                    showConnectionState(is_connected: true);
                    break;
            }
        }

        private void showConnectionState(bool is_connected)
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
                BoardDataCallback d = new BoardDataCallback(addReading);
                this.Invoke(d, new object[] { reading });
            }
            else
            {
                addReading(reading);
            }
        }

        public void OnNewScan(int value)
        {
            if (this.rawSerialLog.InvokeRequired)
            {
                BoardDataCallback d = new BoardDataCallback(addNewScanValue);
                this.Invoke(d, new object[] { value });
            }
            else
            {
                addNewScanValue(value);
            }
        }

        public void addNewScanValue(int value)
        {
            scanChartData.AddData(value);
            refreshChart();
        }

        private void addReading(int reading)
        {
            readingChartData.AddData(reading);
            refreshChart();
            refreshDataInfo();
        }

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

        private void refreshDataInfo()
        {
            dataInfoLabel.Text = $"Count: {readingChartData.GetCount()}";
        }

        private void appendRawSerialLog(int value)
        {
            rawSerialLog.Text = value.ToString() + "\n" + rawSerialLog.Text;
        }

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
            readingChartData.Clear();
            refreshDataInfo();
            refreshChart();
        }

        private void calibSaveBtn_Click(object sender, EventArgs e)
        {
            readingChartData.Calibrator.SaveDefault();
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

        private void calibBtn_Click(object sender, EventArgs e)
        {
            readingChartData.Calibrator.SetFromReading((double)calibReadInput.Value, readingChartData.GetLastRawReading());
            refreshCalibration();
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