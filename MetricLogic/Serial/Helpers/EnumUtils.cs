using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricLogic.Serial.Helpers
{
    internal static class EnumUtils
    {
        public static T EnumFromInt<T>(int value)
        {
            if (Enum.IsDefined(typeof(T), value))
            {
                return (T)(object)value;
            }
            else
                throw new Exception($"Bad enum ({typeof(T).Name}) value: {value}");
        }
    }
}
