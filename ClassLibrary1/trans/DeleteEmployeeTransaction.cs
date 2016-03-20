using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class DeleteEmployeeTransaction : Transaction
    {
        private readonly int empid;
        public DeleteEmployeeTransaction(int id) { this.empid = id;}
        public void Execute(){ PayrollDatabase.instance.DeleteEmployee(empid); }
    }
}
