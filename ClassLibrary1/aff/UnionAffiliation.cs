using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class UnionAffiliation : Affiliation
    {
        private Hashtable charges = new Hashtable();
        private int memberTd;
        private readonly double dues;
        private readonly double charge;

        public UnionAffiliation(int memberId, double charge)
        {
            this.memberTd = memberId;
            this.charge = charge;
        }

        public UnionAffiliation() : this(-1, 0.0) { }
        public ServiceCharge GetServiceCharge(DateTime date) { return charges[date] as ServiceCharge; }
        public void AddServiceCharge(ServiceCharge sc) { charges[sc.Date] = sc;}
        public double Charge { get { return charge; } }
        public int MemberId { get { return memberTd; } }
        public double CalculateDeductions(Paycheck paycheck)
        {
            double totalDues = 0;
            int friday = NumberOfFridaysInPayPeriod(paycheck.PayPeriodStartDate, paycheck.PayPeriodEndDate);
            totalDues = charge * friday;
            foreach (ServiceCharge ch in charges.Values)
                if (DateUtil.IsInPayPeriod(ch.Date, paycheck.PayPeriodStartDate, paycheck.PayPeriodEndDate))
                    totalDues += ch.Charge;
            return totalDues;
        }

        private int NumberOfFridaysInPayPeriod(DateTime payPeriodStart, DateTime payPeriodEnd)
        {
            int fridays = 0;
            DateTime day = payPeriodStart;
            while (day.Date <= payPeriodEnd)
            {
                if (day.DayOfWeek == DayOfWeek.Friday)
                    fridays++;
                day = day.AddDays(1);
            }
            return fridays;
        }
    }
}
