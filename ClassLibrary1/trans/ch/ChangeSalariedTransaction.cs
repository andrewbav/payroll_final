using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class ChangeSalariedTransaction : ChangeClassificationTransaction
    {
        private readonly double salaryRate;
        public ChangeSalariedTransaction(int id, double salaryRate): base(id){ this.salaryRate = salaryRate;}
        protected override PaymentClassification Classification { get { return new SalariedClassification(salaryRate); } }
        protected override PaymentSchedule Schedule { get{ return new MonthlySchedule(); } }
    }
}
