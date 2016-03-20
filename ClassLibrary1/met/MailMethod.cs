using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class MailMethod : PaymentMethod
    {
        private readonly string mailAddress;

        public MailMethod(string mailAddress) { this.mailAddress = mailAddress;}
        public string MailAddress { get { return mailAddress; } }
        public override string ToString()
        { return String.Format("Mail {0}", mailAddress); }
        public void Pay(Paycheck paycheck){ }
    }
}