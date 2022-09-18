using MetricLogic.Serial.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricLogic.Serial.Models
{
    public class SerialMessage
    {
        public SerialHeaderEnum Header { get; set; }
        public int Value { get; set; }

        public static readonly SerialMessage Empty = new SerialMessage();
        public static readonly SerialMessage Ack = new SerialMessage(SerialHeaderEnum.ACK, 0);
        public static readonly SerialMessage Hello = new SerialMessage(SerialHeaderEnum.HELLO, 0);

        public SerialMessage()
        {

        }

        public SerialMessage(SerialHeaderEnum header, int value)
        {
            Header = header;
            Value = value;
        }

        public void HeaderFromInt(int value)
        {
            Header = EnumUtils.EnumFromInt<SerialHeaderEnum>(value);
        }

    }
}
