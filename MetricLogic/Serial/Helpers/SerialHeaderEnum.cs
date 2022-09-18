using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricLogic.Serial.Helpers
{
    public enum SerialHeaderEnum
    {
        PING = 0,
        ACK = 1,
        HELLO = 2,
        READING = 3,
        MODE = 4,
        NULL = 255
    }
}
