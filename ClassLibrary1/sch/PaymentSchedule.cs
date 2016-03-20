using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public interface PaymentSchedule
    {
        bool IsPaydate(DateTime date);
        DateTime GetPayPeriodStartDate(DateTime date);
    }
}
