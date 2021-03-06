﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Employee
    {
        private int empid;
        private string name, address;
        private PaymentClassification classification;
        private PaymentSchedule schedule;
        private PaymentMethod method;
        private Affiliation affiliation = new NoAffiliation();

        public void PayDay(Paycheck paycheck)
        {
            double grossPay = classification.CalculatePay(paycheck);
            double deductions = affiliation.CalculateDeductions(paycheck);
            double netPay = grossPay - deductions;
            paycheck.GrossPay = grossPay;
            paycheck.Deductions = deductions;
            paycheck.NetPay = netPay;
            method.Pay(paycheck);
        }

        public PaymentClassification Classification
        {
            get { return classification; }
            set { classification = value; }
        }
        public PaymentSchedule Schedule
        {
            get { return schedule; }
            set { schedule = value; }
        }
        public PaymentMethod Method
        {
            get { return method; }
            set { method = value; }
        }
        public Affiliation Affiliation
        {
            get { return affiliation; }
            set { affiliation = value; }
        }
        public Employee(int empid, string name, string address)
        {
            this.empid = empid;
            this.name = name;
            this.address = address;
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public int EmpId
        {
            get { return empid; }
        }
        public bool IsPayDate(DateTime date)
        {
            return schedule.IsPaydate(date);
        }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Emp#: ").Append(empid).Append(" ");
            builder.Append(name).Append(" ");
            builder.Append(address).Append(" ");
            builder.Append("Paid").Append(classification).Append(" ");
            builder.Append(schedule);
            builder.Append("by ").Append(method);
            return builder.ToString();
        }
        public DateTime GetPayPeriodStartDate(DateTime date) { return schedule.GetPayPeriodStartDate(date); }
    }
}
