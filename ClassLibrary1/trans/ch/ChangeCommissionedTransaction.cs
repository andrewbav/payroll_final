using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class ChangeCommissionedTransaction : ChangeClassificationTransaction
    {
        private readonly double commissionRate;
        private readonly double salaryRate;
        public ChangeCommissionedTransaction(int id, double commissionRate, double salaryRate): base(id)
        {
            this.commissionRate = commissionRate;
            this.salaryRate = salaryRate;
        }
        protected override PaymentClassification Classification { get {  return new CommissionedClassification(commissionRate, salaryRate);} }
        protected override PaymentSchedule Schedule { get{ return new BiweeklySchedule(); } }
    }
}