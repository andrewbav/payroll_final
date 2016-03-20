using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class TimeCard
    {
        private readonly DateTime date;
        private readonly double hourly;

        public TimeCard(DateTime date, double hourly)
        {
            this.date = date;
            this.hourly = hourly;
        }
        public double Hours { get { return hourly; } }
        public DateTime Date { get { return date; } }        
    }
}
