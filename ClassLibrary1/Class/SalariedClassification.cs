using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class SalariedClassification : PaymentClassification
    {
        private readonly double salary;
        private Hashtable salesReceipts = new Hashtable();

        public SalariedClassification(double salary) { this.salary = salary; }
        public double Salary {get { return salary; } }
        public override string ToString() {return String.Format("${0}", salary); }
        public override double CalculatePay(Paycheck paycheck){ return salary; }
    }
}

