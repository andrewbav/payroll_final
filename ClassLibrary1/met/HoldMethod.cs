using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class HoldMethod : PaymentMethod
    {
        public override string ToString() { return "HOLD"; }
        public void Pay(Paycheck paycheck) { paycheck.SetField("Disposition", "HOLD"); }
    }
}

