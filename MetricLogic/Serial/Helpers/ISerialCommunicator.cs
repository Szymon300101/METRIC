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
        internal void Begin(SerialConnection connection);
        internal void Read();
        internal void Send(SerialMessage msg);
        internal void SetReadCT(CancellationToken token);
        internal void SetRawSerialListener(IRawSerialListener rawSerialListener);
    }
}
