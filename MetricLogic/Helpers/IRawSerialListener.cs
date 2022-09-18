using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricLogic.Helpers
{
    public interface IRawSerialListener
    {
        void OnSerialByte(int value);
    }
}
