using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class ServiceCharge
    {
        private readonly DateTime date;
        private readonly double charge;

        public ServiceCharge(DateTime date, double charge)
        {
            this.date = date;
            this.charge = charge;
        }
        public double Charge { get { return charge; } }
        public DateTime Date { get { return date; } }
    }
}
