using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.ComponentModel;
using MetricLogic.Serial.Helpers;
using MetricLogic.Serial.Models;
using MetricLogic.Helpers;

namespace MetricLogic.Serial
{
    public class SerialConnection
    {
        private SerialStateEnum state;

        internal SerialPort port;
        internal ISerialListener _listener;
        private ISerialStateListener? _stateListener;

        private BackgroundWorker hardWorker;
        private Thread readThread; 
        CancellationTokenSource readCts;
        private ISerialCommunicator communicator;

        public SerialStateEnum State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                if(_stateListener != null)
                    _stateListener.OnSerialStateChange(state);
            }
        }

        public SerialConnection()
        {
            hardWorker = new BackgroundWorker();

            communicator = new SerialReaderWriter();
            State = SerialStateEnum.disconnected;
        }

        public void AddSerialListener(ISerialListener listener)
        {
            this._listener = listener;
        }

        public void AddStateListener(ISerialStateListener stateListener)
        {
            this._stateListener = stateListener;
        }

        public void AddRawSerialListener(IRawSerialListener rawSerialListener)
        {
            communicator.SetRawSerialListener(rawSerialListener);
        }

        public void Connect(string portName, int baudRate)
        {
            initPort(portName, baudRate);

            communicator.Begin(this);

            initTasks();

            State = SerialStateEnum.connecting;
        }

        private void initPort(string portName, int baudRate)
        {
            IContainer components = new Container();
            port = new SerialPort(components);
            port.PortName = portName;
            port.BaudRate = baudRate;
            port.DtrEnable = true;
            port.ReadTimeout = 5000;
            port.WriteTimeout = 500;
            port.Open();
        }

        private void initTasks()
        {
            readCts = new CancellationTokenSource();
            communicator.SetReadCT(readCts.Token);
            readThread = new Thread(new ThreadStart(communicator.Read));
            readThread.Start();
            hardWorker.RunWorkerAsync();
        }

        public void Send(SerialMessage msg)
        {
            communicator.Send(msg);
        }

        public void Send(SerialHeaderEnum header, int value)
        {
            communicator.Send(new SerialMessage(header, value));
        }

        public static List<string> GetPortNames()
        {
            return SerialPort.GetPortNames().ToList();
        }

        public void Disconnect()
        {
            if(State != SerialStateEnum.disconnected)
            {
                readCts.Cancel();
                port.Close();
                port.Dispose();
                State = SerialStateEnum.disconnected;
            }
        }

    }
}
