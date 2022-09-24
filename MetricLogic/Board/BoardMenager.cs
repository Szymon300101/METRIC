using MetricLogic.Helpers;
using MetricLogic.Serial;
using MetricLogic.Serial.Helpers;
using MetricLogic.Serial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricLogic
{
    public class BoardMenager : ISerialListener
    {
        private BoardModeEnum mode;

        private SerialConnection _connection;
        private IBoardModeListener _modeListener;
        private IBoardDataListener _dataListener;

        public BoardModeEnum Mode
        {
            get
            {
                return mode;
            }
            set
            {
                mode = value;
                if(_modeListener != null)
                    _modeListener.OnBoardModeChanged(mode);
            }
        }

        public BoardMenager(SerialConnection connection)
        {
            _connection = connection;
        }

        public void AddModeListener(IBoardModeListener modeListener)
        {
            _modeListener = modeListener;
        }

        public void AddReadingsListener(IBoardDataListener readingsListener)
        {
            _dataListener = readingsListener;
        }

        private void processMessage(SerialMessage msg)
        {
            switch (msg.Header)
            {
                case SerialHeaderEnum.PING:
                    _connection.Send(SerialMessage.Ack);
                    break;
                case SerialHeaderEnum.ACK:
                    break;
                case SerialHeaderEnum.HELLO:
                    _connection.State = SerialStateEnum.connected;
                    _connection.Send(SerialMessage.Hello);
                    break;
                case SerialHeaderEnum.READING:
                    if (_dataListener != null)
                        _dataListener.OnNewReading(msg.Value);
                    break;
                case SerialHeaderEnum.SCAN:
                    if (_dataListener != null)
                        _dataListener.OnNewScan(msg.Value);
                    break;
                case SerialHeaderEnum.MODE:
                    Mode = EnumUtils.EnumFromInt<BoardModeEnum>(msg.Value);
                    break;
                default:
                    break;
            }
        }

        public void OnSerialReceived(SerialMessage msg)
        {
            processMessage(msg);
        }
    }
}
