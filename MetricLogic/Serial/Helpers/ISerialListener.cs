using MetricLogic.Serial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricLogic.Serial.Helpers
{
    public interface ISerialListener
    {
        void OnSerialReceived(SerialMessage msg);
    }
}
