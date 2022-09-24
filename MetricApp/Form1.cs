using MetricLogic;
using MetricLogic.ChartData;
using MetricLogic.Helpers;
using MetricLogic.Serial;
using MetricLogic.Serial.Helpers;
using MetricLogic.Serial.Models;
using System.Windows.Forms.DataVisualization.Charting;

namespace MetricApp
{
    public partial class Form1 : Form
    {
        SerialConnection connection;
        BoardMenager board;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();

            connection = new SerialConnection();
            board = new BoardMenager(connection);
            readingChartData = new ChartReadingData();
            scanChartData = new ChartScanData();

            readingChartData.Calibrator.LoadDefault();

            board.AddModeListener(this);
            board.AddReadingsListener(this);

            connection.AddStateListener(this);
            connection.AddSerialListener(board);
            connection.AddRawSerialListener(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            displayConnectionState(is_connected: false);
            refreshCalibration();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.Disconnect();
        }

        //Rest of code in Form1_Parts directory
    }
}