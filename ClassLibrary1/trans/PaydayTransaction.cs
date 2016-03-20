using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class PaydayTransaction : Transaction
    {
        private readonly DateTime payDate;
        private Hashtable paychecks = new Hashtable();

        public PaydayTransaction(DateTime payDate) { this.payDate = payDate; }
        public void Execute()
        {
            ArrayList empids = new ArrayList();
            empids.AddRange(PayrollDatabase.GetAllEmployeeIds());
            foreach (int empid in empids)
            {
                Employee employee = PayrollDatabase.instance.GetEmployee(empid);
                if (employee.IsPayDate(payDate))
                {
                    DateTime startDate = employee.GetPayPeriodStartDate(payDate);
                    Paycheck pc = new Paycheck(startDate, payDate);
                    paychecks[empid] = pc;
                    employee.PayDay(pc);
                }
            }
        }
        public Paycheck GetPaycheck(int empid) { return paychecks[empid] as Paycheck; }
    }
}
