using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class NoAffiliation : Affiliation
    {
        public double CalculateDeductions(Paycheck paycheck) { return 0; }
    }
}
