using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class HourlyClassification : PaymentClassification
    {
        private readonly double hourly;
        private Hashtable timeCards = new Hashtable();

        public TimeCard GetTimeCard(DateTime date) { return timeCards[date] as TimeCard; }
        public void AddTimeCard(TimeCard card) { timeCards[card.Date] = card; }
        public HourlyClassification(double hourly) { this.hourly = hourly; }
        public double Hourly { get { return hourly; } }
        public override string ToString(){  return String.Format("${0}", hourly); }
        public override double CalculatePay(Paycheck paycheck)
        {
            double totalPay = 0.0;
            foreach (TimeCard timeCard in timeCards.Values)
            {
                if (IsInPayPeriod(timeCard.Date, paycheck))
                    totalPay += CalculatePayForTimeCard(timeCard);
            }
            return totalPay;
        }
        private double CalculatePayForTimeCard(TimeCard card)
        {
            double overtimeHours = Math.Max(0.0, card.Hours - 8);
            double normalHours = card.Hours - overtimeHours;
            return hourly * normalHours + hourly * 1.5 * overtimeHours;
        }
    }
}
