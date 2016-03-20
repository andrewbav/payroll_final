using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class AddCommissionedEmployee : AddEmployeeTransaction
    {
        private readonly double commission;
        private readonly double salary;
        public AddCommissionedEmployee(int id, string name, string address, double commission, double salary): base(id, name, address)
        {
            this.commission = commission;
            this.salary = salary;
        }
        protected override PaymentClassification MakeClassification(){ return new CommissionedClassification(commission, salary); }
        protected override PaymentSchedule MakeSchedule() { return new BiweeklySchedule(); }
    }
}
