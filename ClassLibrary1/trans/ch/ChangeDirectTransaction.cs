using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class ChangeDirectTransaction : ChangeMethodTransaction
    {
        private readonly string bank;
        private readonly string account;

        public ChangeDirectTransaction(int id, string bank, string account): base(id)
        {
            this.bank = bank;
            this.account = account;
        }
        protected override void Change(Employee e) { e.Method = new DirectMethod(bank, account);}
    }
}