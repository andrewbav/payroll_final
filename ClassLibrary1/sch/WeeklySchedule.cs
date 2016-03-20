using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class WeeklySchedule : PaymentSchedule
    {
        public override string ToString() { return "weekly";}
        public bool IsPaydate(DateTime date)
        { return date.DayOfWeek == DayOfWeek.Friday; }
        public DateTime GetPayPeriodStartDate(DateTime date) { return date.AddDays(-6); }
    }
}
