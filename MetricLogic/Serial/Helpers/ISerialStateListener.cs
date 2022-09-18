using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricLogic.Serial.Helpers
{
    public interface ISerialStateListener
    {
        void OnSerialStateChange(SerialStateEnum state);
    }
}
