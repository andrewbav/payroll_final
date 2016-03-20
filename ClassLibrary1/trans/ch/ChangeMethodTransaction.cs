using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public abstract class ChangeMethodTransaction
    {
        private readonly int empid;

        public ChangeMethodTransaction(int empid) { this.empid = empid;}
        public void Execute()
        {
            Employee e = PayrollDatabase.instance.GetEmployee(empid);
            if (e != null)
                Change(e);
            else
                throw new InvalidOperationException("Работник не найден!");
        }
        protected abstract void Change(Employee e);
    }
}
