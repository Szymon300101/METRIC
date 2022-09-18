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
        private CancellationToken readCT;

        private SerialMessage message;
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
                    decodeMessage();
                }
            }
            catch (TimeoutException) { }
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
            message.HeaderFromInt(_connection.port.ReadByte());
        }

        private void readMessageValue()
        {
            message.Value = _connection.port.ReadByte();
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
    }
}
