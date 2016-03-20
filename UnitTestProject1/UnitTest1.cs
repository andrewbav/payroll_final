using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestEmployee()
        {
            int empid = 1;
            Employee e = new Employee(empid, "Bill", "Home");
            Assert.AreEqual("Bill", e.Name);
            Assert.AreEqual("Home", e.Address);
            Assert.AreEqual(empid, e.EmpId);
        }
        [TestMethod]
        public void EmployeeToString()
        {
            int empid = 2;
            Employee e = new Employee(empid, "Bill", "Home");
            Assert.AreEqual("Emp#: "+ empid + " Bill Home Paid by ", e.ToString());
        }
        [TestMethod]
        public void AddSalariedEmployee()
        {
            int empid = 3;
            AddSalariedEmployee t = new AddSalariedEmployee(empid, "Bob", "Home", 1000.00);
            t.Execute();
            Employee e = PayrollDatabase.instance.GetEmployee(empid);
            Assert.AreEqual("Bob", e.Name);
            PaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is SalariedClassification);
            SalariedClassification sc = pc as SalariedClassification;
            Assert.AreEqual(1000.00, sc.Salary, .001);
            PaymentSchedule ps = e.Schedule;
            Assert.IsTrue(ps is MonthlySchedule);
            PaymentMethod pm = e.Method;
            Assert.IsTrue(pm is HoldMethod);
        }
        [TestMethod]
        public void AddHourlyEmployee()
        {
            int empid = 3;
            AddHourlyEmployee t = new AddHourlyEmployee(empid, "Bill", "Home", 265);
            t.Execute();
            Employee e = PayrollDatabase.GetEmployee_Static(empid);
            Assert.AreEqual("Bill", e.Name);
            PaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is HourlyClassification);
            HourlyClassification sc = pc as HourlyClassification;
            Assert.AreEqual(265, sc.Hourly, .001);
            PaymentSchedule ps = e.Schedule;
            Assert.IsTrue(ps is WeeklySchedule);
            PaymentMethod pm = e.Method;
            Assert.IsTrue(pm is HoldMethod);
        }
        [TestMethod]
        public void AddCommissionedEmployee()
        {
            int empid = 4;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empid, "Bob", "Home", 265, 10);
            t.Execute();
            Employee e = PayrollDatabase.GetEmployee_Static(empid);
            Assert.AreEqual("Bob", e.Name);
            PaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is CommissionedClassification);
            CommissionedClassification sc = pc as CommissionedClassification;
            Assert.AreEqual(10, sc.Commission, .001);
            Assert.AreEqual(265, sc.Salary, .001);
            PaymentSchedule ps = e.Schedule;
            Assert.IsTrue(ps is BiweeklySchedule);
            PaymentMethod pm = e.Method;
            Assert.IsTrue(pm is HoldMethod);
        }

        [TestMethod]
        public void DeleteEmployee()
        {
            int empid = 5;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empid, "Bill", "Home", 265, 123.804);
            t.Execute();
            Employee e = PayrollDatabase.GetEmployee_Static(empid);
            Assert.IsNotNull(e);
            DeleteEmployeeTransaction dt = new DeleteEmployeeTransaction(empid);
            dt.Execute();
            e = PayrollDatabase.GetEmployee_Static(empid);
            Assert.IsNull(e);
        }


        [TestMethod]
        public void TimeCardTransaction()
        {
            int empid = 6;
            AddHourlyEmployee t = new AddHourlyEmployee(empid, "Bob", "Home", 23.41);
            t.Execute();
            TimeCardTransaction tct = new TimeCardTransaction(new DateTime(2015, 10, 31), 8.0, empid);
            tct.Execute();
            Employee e = PayrollDatabase.GetEmployee_Static(empid);
            Assert.IsNotNull(e);
            PaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is HourlyClassification);
            HourlyClassification hc = pc as HourlyClassification;
            TimeCard tc = hc.GetTimeCard(new DateTime(2015, 10, 31));
            Assert.IsNotNull(tc);
            Assert.AreEqual(8.0, tc.Hours);
        }

        [TestMethod]
        public void SalesReceiptTransaction()
        {
            int empid = 7;
            AddCommissionedEmployee s = new AddCommissionedEmployee(empid, "Bill", "Home", 38, 25000);
            s.Execute();
            SalesReceiptTransaction crt = new SalesReceiptTransaction(new DateTime(2015, 10, 31), 15.15, empid);
            crt.Execute();
            Employee e = PayrollDatabase.GetEmployee_Static(empid);
            Assert.IsNotNull(e);
            PaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is CommissionedClassification);
            CommissionedClassification cc = pc as CommissionedClassification;
            SalesReceipt sr = cc.GetSalary(new DateTime(2015, 10, 31));
            Assert.IsNotNull(sr);
            Assert.AreEqual(15.15, sr.Amount);
        }

        [TestMethod]
        public void AddserviceCharge()
        {
            int empid = 8;
            AddHourlyEmployee t = new AddHourlyEmployee(empid, "Bob", "Home", 23.41);
            t.Execute();
            Employee e = PayrollDatabase.GetEmployee_Static(empid);
            Assert.IsNotNull(e);
            UnionAffiliation af = new UnionAffiliation();
            e.Affiliation = af;
            int memberId = 86;
            PayrollDatabase.AddUnionMember_Static(memberId, e);
            ServiceChargeTransaction sct = new ServiceChargeTransaction(memberId, new DateTime(2015, 11, 8), 12.95);
            sct.Execute();
            ServiceCharge sc = af.GetServiceCharge(new DateTime(2015, 11, 8));
            Assert.IsNotNull(sc);
            Assert.AreEqual(12.95, sc.Charge, .001);
        }

        [TestMethod]
        public void ChangeNameTransaction()
        {
            int empid = 9;
            AddHourlyEmployee t = new AddHourlyEmployee(empid, "Bill", "Home", 265);
            t.Execute();
            ChangeNameTransaction cnt = new ChangeNameTransaction(empid, "Bob");
            cnt.Execute();
            Employee e = PayrollDatabase.GetEmployee_Static(empid);
            Assert.IsNotNull(e);
            Assert.AreEqual("Bob", e.Name);
        }

        [TestMethod]
        public void ChangeAddressTransaction()
        {
            int empid = 10;
            AddHourlyEmployee t = new AddHourlyEmployee(empid, "Bob", "Home", 23.41);
            t.Execute();
            ChangeAddressTransaction cat = new ChangeAddressTransaction(empid, "Home2");
            cat.Execute();
            Employee e = PayrollDatabase.GetEmployee_Static(empid);
            Assert.IsNotNull(e);
            Assert.AreEqual("Home2", e.Address);
        }

        [TestMethod]
        public void ChangeHourlyTransaction()
        {
            int empid = 11;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empid, "Bill", "Home", 23.41, 3.2);
            t.Execute();
            ChangeHourlyTransaction cht = new ChangeHourlyTransaction(empid, 19.84);
            cht.Execute();
            Employee e = PayrollDatabase.GetEmployee_Static(empid);
            Assert.IsNotNull(e);
            PaymentClassification pc = e.Classification;
            Assert.IsNotNull(pc);
            Assert.IsTrue(pc is HourlyClassification);
            HourlyClassification hc = pc as HourlyClassification;
            Assert.AreEqual(19.84, hc.Hourly, .001);
            PaymentSchedule ps = e.Schedule;
            Assert.IsTrue(ps is WeeklySchedule);
        }

        [TestMethod]
        public void ChangeSalariedTransaction()
        {
            int empid = 12;
            AddHourlyEmployee t = new AddHourlyEmployee(empid, "Bob", "Home", 23.41);
            t.Execute();
            ChangeSalariedTransaction cht = new ChangeSalariedTransaction(empid, 20.44);
            cht.Execute();
            Employee e = PayrollDatabase.GetEmployee_Static(empid);
            Assert.IsNotNull(e);
            PaymentClassification pc = e.Classification;
            Assert.IsNotNull(pc);
            Assert.IsTrue(pc is SalariedClassification);
            SalariedClassification sc = pc as SalariedClassification;
            Assert.AreEqual(20.44, sc.Salary);
            PaymentSchedule ps = e.Schedule;
            Assert.IsTrue(ps is MonthlySchedule);
        }

        [TestMethod]
        public void ChangeCommissionedTransaction()
        {
            int empid = 13;
            AddSalariedEmployee t = new AddSalariedEmployee(empid, "Bill", "Home", 23.41);
            t.Execute();
            ChangeCommissionedTransaction cht = new ChangeCommissionedTransaction(empid, 20.44, 10);
            cht.Execute();
            Employee e = PayrollDatabase.GetEmployee_Static(empid);
            Assert.IsNotNull(e);
            PaymentClassification pc = e.Classification;
            Assert.IsNotNull(pc);
            Assert.IsTrue(pc is CommissionedClassification);
            CommissionedClassification sc = pc as CommissionedClassification;
            Assert.AreEqual(10, sc.Commission);
            Assert.AreEqual(20.44, sc.Salary);
            PaymentSchedule ps = e.Schedule;
            Assert.IsTrue(ps is BiweeklySchedule);
        }

        [TestMethod]
        public void ChangeDirectMethod()
        {
            int empid = 14;
            AddHourlyEmployee t = new AddHourlyEmployee(empid, "Bob", "Home", 313);
            t.Execute();
            ChangeDirectTransaction cdt = new ChangeDirectTransaction(empid, "sberbank", "123456789");
            cdt.Execute();
            Employee e = PayrollDatabase.GetEmployee_Static(empid);
            Assert.IsNotNull(e);
            Assert.AreEqual("Direct sberbank 123456789", e.Method.ToString());

        }

        [TestMethod]
        public void ChangeMailMethod()
        {
            int empid = 15;
            AddHourlyEmployee t = new AddHourlyEmployee(empid, "Bill", "Home", 313);
            t.Execute();
            ChangeMailTransaction cmt = new ChangeMailTransaction(empid, "bill@mail.ru");
            cmt.Execute();
            Employee e = PayrollDatabase.GetEmployee_Static(empid);
            Assert.IsNotNull(e);
            Assert.AreEqual("Mail bill@mail.ru", e.Method.ToString());

        }

        [TestMethod]
        public void ChangeUnionMember()
        {
            int empid = 16;
            AddHourlyEmployee t = new AddHourlyEmployee(empid, "Bob", "Home", 256.1);
            t.Execute();
            int memberId = 7743;
            ChangeMemberTransaction cmt = new ChangeMemberTransaction(empid, memberId, 99.42);
            cmt.Execute();
            Employee e = PayrollDatabase.GetEmployee_Static(empid);
            Assert.IsNotNull(e);
            Affiliation affiliation = e.Affiliation;
            Assert.IsNotNull(affiliation);
            Assert.IsTrue(affiliation is UnionAffiliation);
            UnionAffiliation uf = affiliation as UnionAffiliation;
            Assert.AreEqual(99.42, uf.Charge, .001);
            Employee member = PayrollDatabase.GetUnionMember_Static(memberId);
            Assert.AreEqual(e, member);

        }

        [TestMethod]
        public void PaySingleSalariedEmployee()
        {
            int empid = 17;
            AddSalariedEmployee t = new AddSalariedEmployee(empid, "Bill", "Home", 1000.00);
            t.Execute();
            DateTime payDate = new DateTime(2001, 11, 30);
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            Paycheck pc = pt.GetPaycheck(empid);
            Assert.IsNotNull(pc);
            Assert.AreEqual(payDate, pc.PayDate);
            Assert.AreEqual(1000.00, pc.GrossPay, .001);
            Assert.AreEqual("HOLD", pc.GetField("Disposition"));
            Assert.AreEqual(0.0, pc.Deductions, .001);
            Assert.AreEqual(1000.00, pc.NetPay, .001);
        }

        [TestMethod]
        public void PaySingleSalariedEmployeeOnWrongDate()
        {
            int empid = 18;
            AddSalariedEmployee t = new AddSalariedEmployee(empid, "Bob", "Home", 1000.00);
            t.Execute();
            DateTime payDate = new DateTime(2015, 12, 16);
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            Paycheck pc = pt.GetPaycheck(empid);
            Assert.IsNull(pc);
        }

        [TestMethod]
        public void PayingSingleHourlyEmployeeNoTimeCards()
        {
            int empid = 19;
            AddHourlyEmployee t = new AddHourlyEmployee(empid, "Bill", "Home", 15.25);
            t.Execute();
            DateTime payDate = new DateTime(2015, 11, 27);
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            Paycheck pc = pt.GetPaycheck(empid);
            Assert.IsNotNull(pc);
            Assert.AreEqual(payDate, pc.PayDate);
            Assert.AreEqual(0.0, pc.GrossPay);
            Assert.AreEqual("HOLD", pc.GetField("Disposition"));
            Assert.AreEqual(0.0, pc.Deductions, .001);
            Assert.AreEqual(0.0, pc.NetPay, .001);
        }




        private void ValidateHourlyPaycheck(PaydayTransaction pt, int empid, DateTime payDate, double pay)
        {
            Paycheck pc = pt.GetPaycheck(empid);
            Assert.IsNotNull(pc);
            Assert.AreEqual(payDate, pc.PayDate);
            Assert.AreEqual(pay, pc.GrossPay, .001);
            Assert.AreEqual("HOLD", pc.GetField("Disposition"));
            Assert.AreEqual(0.0, pc.Deductions, .001);
            Assert.AreEqual(pay, pc.NetPay, .001);
        }

        [TestMethod]
        public void PayingSingleHourlyEmployeeOneTimeCards()
        {
            int empid = 20;
            AddHourlyEmployee t = new AddHourlyEmployee(empid, "Bob", "Home", 15.25);
            t.Execute();
            DateTime payDate = new DateTime(2015, 12, 4);
            TimeCardTransaction tc = new TimeCardTransaction(payDate, 2.0, empid);
            tc.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            ValidateHourlyPaycheck(pt, empid, payDate, 30.5);

        }

        [TestMethod]
        public void PayingSingleHourlyEmployeeOvertimeTimeCards()
        {
            int empid = 21;
            AddHourlyEmployee t = new AddHourlyEmployee(empid, "Bill", "Home", 15.25);
            t.Execute();
            DateTime payDate = new DateTime(2015, 12, 4);
            TimeCardTransaction tc = new TimeCardTransaction(payDate, 9.0, empid);
            tc.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            ValidateHourlyPaycheck(pt, empid, payDate, 9.5 * 15.25);

        }

        [TestMethod]
        public void PaySingleHourlyEmployeeOnWrongDate()
        {
            int empid = 22;
            AddHourlyEmployee t = new AddHourlyEmployee(empid, "Bob", "Home", 15.25);
            t.Execute();
            DateTime payDate = new DateTime(2015, 12, 3);
            TimeCardTransaction tc = new TimeCardTransaction(payDate, 9.0, empid);
            tc.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            Paycheck pc = pt.GetPaycheck(empid);
            Assert.IsNull(pc);
        }

        [TestMethod]
        public void PayingSingleHourlyEmployeeTwoTimeCards()
        {
            int empid = 23;
            AddHourlyEmployee t = new AddHourlyEmployee(empid, "Bill", "HOme", 15.25);
            t.Execute();
            DateTime payDate = new DateTime(2015, 12, 4);
            TimeCardTransaction tc = new TimeCardTransaction(payDate, 2.0, empid);
            tc.Execute();
            TimeCardTransaction tc2 = new TimeCardTransaction(payDate.AddDays(-1), 5.0, empid);
            tc2.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            ValidateHourlyPaycheck(pt, empid, payDate, 7 * 15.25);

        }



        [TestMethod]
        public void PayingSingleHourlyEmployeeWithTimeCardsSpanningTwoPayPeriods()
        {
            int empid = 24;
            AddHourlyEmployee t = new AddHourlyEmployee(empid, "Bob", "Home", 15.25);
            t.Execute();
            DateTime payDate = new DateTime(2015, 12, 4);
            DateTime dateInPreviousPayPeriod = new DateTime(2015, 11, 21);
            TimeCardTransaction tc = new TimeCardTransaction(payDate, 2.0, empid);
            tc.Execute();
            TimeCardTransaction tc1 = new TimeCardTransaction(payDate.AddDays(-1), 5.0, empid);
            tc1.Execute();
            TimeCardTransaction tc2 = new TimeCardTransaction(dateInPreviousPayPeriod, 5.0, empid);
            tc2.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            ValidateHourlyPaycheck(pt, empid, payDate, 7 * 15.25);

        }

        [TestMethod]
        public void PayingSingleCommissionedEmployeeNoSalaryCards()
        {
            int empid = 25;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empid, "Bill", "Home", 15000, 10);
            t.Execute();
            DateTime payDate = new DateTime(2015, 12, 4);
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            Paycheck pc = pt.GetPaycheck(empid);
            Assert.IsNotNull(pc);
            Assert.AreEqual(payDate, pc.PayDate);
            Assert.AreEqual(15000, pc.GrossPay);
            Assert.AreEqual("HOLD", pc.GetField("Disposition"));
            Assert.AreEqual(0.0, pc.Deductions, .001);
            Assert.AreEqual(15000, pc.NetPay, .001);
        }

        private void ValidateCommissionPaycheck(PaydayTransaction pt, int empid, DateTime payDate, double pay)
        {
            Paycheck pc = pt.GetPaycheck(empid);
            Assert.IsNotNull(pc);
            Assert.AreEqual(payDate, pc.PayDate);
            Assert.AreEqual(pay, pc.GrossPay);
            Assert.AreEqual("HOLD", pc.GetField("Disposition"));
            Assert.AreEqual(0.0, pc.Deductions, .001);
            Assert.AreEqual(pay, pc.NetPay, .001);
        }

        [TestMethod]
        public void PayingSingleCommissionEmployeeOneSalesCards()
        {
            int empid = 26;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empid, "Bob", "Home", 15000, 10);
            t.Execute();
            DateTime payDate = new DateTime(2015, 12, 4);
            SalesReceiptTransaction tc = new SalesReceiptTransaction(payDate, 1000, empid);
            tc.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            ValidateCommissionPaycheck(pt, empid, payDate, 15100);

        }
        [TestMethod]
        public void PaySingleCommissionEmployeeOnWrongDate()
        {
            int empid = 27;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empid, "Bill", "Home", 15000, 10);
            t.Execute();
            DateTime payDate = new DateTime(2015, 12, 3);
            SalesReceiptTransaction tc = new SalesReceiptTransaction(payDate, 3, empid);
            tc.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            Paycheck pc = pt.GetPaycheck(empid);
            Assert.IsNull(pc);
        }

        [TestMethod]
        public void PayingSingleCommissionEmployeeTwoSalesCards()
        {
            int empid = 28;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empid, "Bob", "Home", 15000, 10);
            t.Execute();
            DateTime payDate = new DateTime(2015, 12, 4);
            SalesReceiptTransaction tc1 = new SalesReceiptTransaction(payDate, 2000, empid);
            tc1.Execute();

            SalesReceiptTransaction tc2 = new SalesReceiptTransaction(payDate.AddDays(-1), 1000, empid);
            tc2.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            ValidateCommissionPaycheck(pt, empid, payDate, 15300);

        }

        [TestMethod]
        public void PayingSingleCommissionEmployeeWithSalesCardsSpanningTwoPayPeriods()
        {
            int empid = 29;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empid, "Bill", "Home", 15000, 10);
            t.Execute();
            DateTime payDate = new DateTime(2015, 12, 4);
            DateTime dateInPreviousPayPeriod = new DateTime(2015, 11, 14);
            SalesReceiptTransaction tc = new SalesReceiptTransaction(payDate, 1000, empid);
            tc.Execute();
            SalesReceiptTransaction tc1 = new SalesReceiptTransaction(payDate.AddDays(-1), 2000, empid);
            tc1.Execute();
            SalesReceiptTransaction tc2 = new SalesReceiptTransaction(dateInPreviousPayPeriod, 3000, empid);
            tc2.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            ValidateCommissionPaycheck(pt, empid, payDate, 15300);

        }

        [TestMethod]
        public void SalariedUnionMemberDues()
        {
            int empid = 30;
            AddSalariedEmployee t = new AddSalariedEmployee(empid, "Bob", "Home", 1000.00);
            t.Execute();
            int memberId = 7734;
            ChangeMemberTransaction cmt = new ChangeMemberTransaction(empid, memberId, 9.42);
            cmt.Execute();
            DateTime payDate = new DateTime(2015, 10, 31);
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            Paycheck pc = pt.GetPaycheck(empid);
            Assert.IsNotNull(pc);
            Assert.AreEqual(payDate, pc.PayDate);
            Assert.AreEqual(1000.00, pc.GrossPay, .001);
            Assert.AreEqual("HOLD", pc.GetField("Disposition"));
            Assert.AreEqual(9.42 * 5, pc.Deductions, .001);
            Assert.AreEqual(1000.00 - 9.42 * 5, pc.NetPay, .001);


        }

        [TestMethod]
        public void HourlyUnionMemberServiceCharge()
        {
            int empid = 31;
            AddHourlyEmployee t = new AddHourlyEmployee(empid, "Bill", "Home", 15.24);
            t.Execute();
            int memberId = 7664;
            ChangeMemberTransaction cmt = new ChangeMemberTransaction(empid, memberId, 9.42);
            cmt.Execute();
            DateTime payDate = new DateTime(2015, 11, 27);
            ServiceChargeTransaction sct = new ServiceChargeTransaction(memberId, payDate, 19.42);
            sct.Execute();
            TimeCardTransaction tct = new TimeCardTransaction(payDate, 8.0, empid);
            tct.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            Paycheck pc = pt.GetPaycheck(empid);
            Assert.IsNotNull(pc);
            Assert.AreEqual(payDate, pc.PayPeriodEndDate);
            Assert.AreEqual(8 * 15.24, pc.GrossPay, .001);
            Assert.AreEqual("HOLD", pc.GetField("Disposition"));
            Assert.AreEqual(9.42 + 19.42, pc.Deductions, .001);
            Assert.AreEqual((8 * 15.24) - (9.42 + 19.42), pc.NetPay, .001);


        }

        [TestMethod]
        public void ServiceChargesSpanningmultiplePayPeriods()
        {
            int empid = 32;
            AddHourlyEmployee t = new AddHourlyEmployee(empid, "Bob", "Home", 15.24);
            t.Execute();
            int memberId = 7734;
            ChangeMemberTransaction cmt = new ChangeMemberTransaction(empid, memberId, 9.42);
            cmt.Execute();
            DateTime payDate = new DateTime(2015, 12, 11);
            DateTime earlyDate = new DateTime(2015, 12, 4);
            DateTime lateDate = new DateTime(2015, 12, 18);
            ServiceChargeTransaction sct = new ServiceChargeTransaction(memberId, payDate, 19.42);
            sct.Execute();
            ServiceChargeTransaction sctEarly = new ServiceChargeTransaction(memberId, earlyDate, 100.00);
            sctEarly.Execute();
            ServiceChargeTransaction sctLate = new ServiceChargeTransaction(memberId, lateDate, 200.00);
            sctLate.Execute();
            TimeCardTransaction tct = new TimeCardTransaction(payDate, 8.0, empid);
            tct.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate);
            pt.Execute();
            Paycheck pc = pt.GetPaycheck(empid);
            Assert.IsNotNull(pc);
            Assert.AreEqual(payDate, pc.PayPeriodEndDate);
            Assert.AreEqual(8 * 15.24, pc.GrossPay, .001);
            Assert.AreEqual("HOLD", pc.GetField("Disposition"));
            Assert.AreEqual(9.42 + 19.42, pc.Deductions, .001);
            Assert.AreEqual((8 * 15.24) - (9.42 + 19.42), pc.NetPay, .001);

        }
    }
}
