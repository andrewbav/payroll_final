using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public abstract class PaymentClassification
    {
        public bool IsInPayPeriod(DateTime theDate, Paycheck paycheck)
        {
            DateTime payPeriodEndDate = paycheck.PayPeriodEndDate;
            DateTime payPeriodStartDate = paycheck.PayPeriodStartDate;
            return (theDate >= payPeriodStartDate) && (theDate <= payPeriodEndDate);
        }
        public abstract double CalculatePay(Paycheck paycheck);
    }
}
