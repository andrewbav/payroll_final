using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class AddHourlyEmployee : AddEmployeeTransaction
    {
        private readonly double hourly;
        public AddHourlyEmployee(int id, string name, string address, double hourly) : base(id, name, address){ this.hourly = hourly; }
        protected override PaymentClassification MakeClassification() { return new HourlyClassification(hourly);}
        protected override PaymentSchedule MakeSchedule() { return new WeeklySchedule(); }
    }
}
