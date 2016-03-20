using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class ChangeMailTransaction : ChangeMethodTransaction
    {
        private readonly string mailAddress;
        public ChangeMailTransaction(int id, string mailAddress): base(id) { this.mailAddress = mailAddress; }
        protected override void Change(Employee e) { e.Method = new MailMethod(mailAddress);}
    }
}
