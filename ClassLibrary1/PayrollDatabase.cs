using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class PayrollDatabase
    {
        private static Hashtable employees = new Hashtable();
        private static Hashtable unionMembers = new Hashtable();
        public static PayrollDatabase instance = new PayrollDatabase();

        public static ArrayList GetAllEmployeeIds()
        {
            ArrayList allEmpIds = new ArrayList();
            allEmpIds.AddRange(employees.Keys);
            return allEmpIds;
        }
        public static void AddEmployee_Static(Employee employee) { instance.AddEmployee(employee); }
        public void AddEmployee(Employee employee) { employees[employee.EmpId] = employee; }
        public static void AddUnionMember_Static(int id, Employee e) { instance.AddUnionMember(id, e); }
        public void AddUnionMember(int id, Employee e) { unionMembers[id] = e; }
        public static Employee GetEmployee_Static(int empid) { return instance.GetEmployee(empid); }
        public Employee GetEmployee(int empid) { return employees[empid] as Employee; }
        public static Employee GetUnionMember_Static(int id) { return instance.GetUnionMember(id); }
        public Employee GetUnionMember(int id) { return unionMembers[id] as Employee; }
        public static void RemoveUnionMember_Static(int memberId){ instance.RemoveUnionMember(memberId); }
        public void RemoveUnionMember(int memberId) { unionMembers.Remove(memberId); }
        public static void DeleteEmployee_Static(int empid){ instance.DeleteEmployee(empid); }
        public void DeleteEmployee(int empid){ employees.Remove(empid); }        
    }
}
