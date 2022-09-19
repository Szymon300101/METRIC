using MetricLogic.Helpers;
using MetricLogic.Serial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricLogic.Serial.Helpers
{
    internal interface ISerialCommunicator
    {
        void Begin(SerialConnection connection);
        void Read();
        void Send(SerialMessage msg);
        void SetReadCT(CancellationToken token);
        void SetRawSerialListener(IRawSerialListener rawSerialListener);
    }
}
