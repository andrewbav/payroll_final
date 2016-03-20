using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class SalesReceiptTransaction : Transaction
    {
        private readonly DateTime date;
        private readonly double amount;
        private readonly int empid;

        public SalesReceiptTransaction(DateTime date, double amount, int empid)
        {
            this.date = date;
            this.amount = amount;
            this.empid = empid;
        }
        public void Execute()
        {
            Employee e = PayrollDatabase.instance.GetEmployee(empid);
            if (e != null)
            {
                CommissionedClassification cc = e.Classification as CommissionedClassification;
                if (cc != null)
                    cc.AddSalesReceipt(new SalesReceipt(date, amount));
                else
                    throw new InvalidOperationException("Попытка добавить карточку табельного учёта " +
                        " для работника не на часовой оплате");
            }
            else
                throw new InvalidOperationException("Работник не найден.");
        }
    }
}
