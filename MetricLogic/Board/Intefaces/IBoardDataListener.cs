using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricLogic.Helpers
{
    public interface IBoardDataListener
    {
        void OnNewReading(int reading);
        void OnNewScan(int value);
    }
}
