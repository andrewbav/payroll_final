using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class DirectMethod : PaymentMethod
    {
        private readonly string bank;
        private readonly string account;

        public DirectMethod(string bank, string account)
        {
            this.bank = bank;
            this.account = account;
        }
        public string Bank { get { return bank; } }
        public string Account { get { return account; } }
        public override string ToString() { return String.Format("Direct {0} {1}", bank, account); }
        public void Pay(Paycheck paycheck){ }
    }
}
