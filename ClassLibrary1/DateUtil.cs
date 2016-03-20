using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    class DateUtil
    {
        public static bool IsInPayPeriod(DateTime theDate, DateTime startDate, DateTime endDate) { return (theDate >= startDate) && (theDate <= endDate);}
    }
}