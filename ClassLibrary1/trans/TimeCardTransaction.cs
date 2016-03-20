using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class TimeCardTransaction : Transaction
    {
        private readonly DateTime date;
        private readonly double hourly;
        private readonly int empid;

        public TimeCardTransaction(DateTime date, double hourly, int empid)
        {
            this.date = date;
            this.hourly = hourly;
            this.empid = empid;
        }
        public void Execute()
        {
            Employee e = PayrollDatabase.instance.GetEmployee(empid);
            if (e != null)
            {
                HourlyClassification hc = e.Classification as HourlyClassification;
                if (hc != null)
                    hc.AddTimeCard(new TimeCard(date, hourly));
                else
                    throw new InvalidOperationException("Попытка добавить карточку табельного учёта " +
                        " для работника не на часовой оплате");
            }
            else
                throw new InvalidOperationException("Работник не найден.");
        }
    }
}
