using MetricLogic.Helpers;
using MetricLogic.Serial.Helpers;
using MetricLogic.Serial.Models;
using System;
using System.Collections.Generic;
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
        private int newByte;

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
                    tryToRead();

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
                    newByte = _connection.port.ReadByte();
                    callbackRawData();
                    decodeMessage();
                }
            }
            catch (TimeoutException) { }
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
