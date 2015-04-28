using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockWatch.Algorithm
{
    public static class AlgorithmHelper
    {
        public static double GetLast(double[] indicatorValues)
        {
            if (indicatorValues.Length == 0)
                return 0;
            return indicatorValues[indicatorValues.Length - 1];
        }
    }
}
