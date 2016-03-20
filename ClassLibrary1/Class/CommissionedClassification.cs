using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class CommissionedClassification : PaymentClassification
    {
        private readonly double commission;
        private readonly double salary;
        private Hashtable salesReceipt = new Hashtable();

        public SalesReceipt GetSalary(DateTime date) { return salesReceipt[date] as SalesReceipt; }
        public void AddSalesReceipt(SalesReceipt card){ salesReceipt[card.Date] = card; }
        public CommissionedClassification(double salary, double commission)
        {
            this.salary = salary;
            this.commission = commission;
        }
        public double Commission { get { return commission; } }
        public double Salary { get { return salary; } }
        public override string ToString(){ return String.Format("${0}${0}", commission, salary);}
        public override double CalculatePay(Paycheck paycheck)
        {
            double totalPay = 0.0;
            foreach (SalesReceipt sales in salesReceipt.Values)
            {
                if (IsInPayPeriod(sales.Date, paycheck))
                    totalPay += sales.Amount / 100.0 * commission;
            }
            return totalPay + salary;
        }        
    }
}