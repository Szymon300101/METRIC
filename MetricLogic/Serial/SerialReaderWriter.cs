using MetricLogic.Helpers;
using MetricLogic.Serial.Helpers;
using MetricLogic.Serial.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricLogic.Serial
{
    internal class SerialReaderWriter : ISerialCommunicator
    {
        private SerialConnection _connection;
        private IRawSerialListener _rawSerialListener;
        private CancellationToken readCT;

        private SerialMessage message;
        private int newByte = -1;
        private bool connectionEstablished = false;

        public void Begin(SerialConnection connection)
        {
            _connection = connection;
            message = SerialMessage.Empty;
        }

        public void Send(SerialMessage msg)
        {
            if (_connection != null && _connection.port.IsOpen)
            {
                byte[] package = makePackageToSend(msg);
                _connection.port.Write(package, 0, 2);
            }
        }

        public byte[] makePackageToSend(SerialMessage msg)
        {
            return new byte[] { (byte)msg.Header, (byte)msg.Value };
        }

        public void Read()
        {
            if(_connection != null)
            {
                while (_connection.port.IsOpen)
                {
                    if (connectionEstablished)
                        tryToRead();
                    else
                        establishConnection();

                    if (readCT.IsCancellationRequested)
                    {
                        return;
                    }
                }
            }
        }

        private void tryToRead()
        {
            try
            {
                if (_connection.port.BytesToRead > 0)
                {
                    readNewByte();
                    callbackRawData();
                    decodeMessage();
                    newByte = -1;
                }
            }
            catch (TimeoutException) { }
        }

        private void readNewByte()
        {
            if(newByte == -1)
                newByte = _connection.port.ReadByte();
        }

        private void callbackRawData()
        {
            if (_rawSerialListener != null)
                _rawSerialListener.OnSerialByte(newByte);
        }

        private void decodeMessage()
        {
            if (message == SerialMessage.Empty)
            {
                message = new SerialMessage();
                readMessageHeader();
            }
            else
            {
                readMessageValue();
                callbackNewMessage();
            }
        }

        private void readMessageHeader()
        {
            message.HeaderFromInt(newByte);
        }

        private void readMessageValue()
        {
            message.Value = newByte;
        }

        private void callbackNewMessage()
        {
            _connection._listener.OnSerialReceived(message);
            message = SerialMessage.Empty;
        }

        private void establishConnection()
        {
            Stopwatch timeout = new Stopwatch();
            timeout.Start();

            while (newByte != (int)SerialHeaderEnum.HELLO)
            {
                if (_connection.port.BytesToRead > 0)
                {
                    newByte = _connection.port.ReadByte();
                    callbackRawData();

                    if (timeout.ElapsedMilliseconds > 5000)
                        throw new Exception("Connection not established: timeout");
                }
            }

            connectionEstablished = true;
        }

        public void SetReadCT(CancellationToken token)
        {
            readCT = token;
        }
        public void SetRawSerialListener(IRawSerialListener rawSerialListener)
        {
            _rawSerialListener = rawSerialListener;
        }
    }
}
