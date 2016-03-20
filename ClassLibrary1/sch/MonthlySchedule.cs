using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class MonthlySchedule : PaymentSchedule
    {
        public override string ToString() { return "monthly"; }
        public bool IsPaydate(DateTime paydate) { return IsLastDayOfMonth(paydate); }
        private bool IsLastDayOfMonth(DateTime date)
        {
            int m1 = date.Month;
            int m2 = date.AddDays(1).Month;
            return (m1 != m2);
        }
        public DateTime GetPayPeriodStartDate(DateTime date)
        {
            int days = 0;
            while (date.AddDays(days - 1).Month == date.Month)
                days--;
            return date.AddDays(days);
        }
    }
}
