using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class ChangeMemberTransaction : ChangeAffiliationTransaction
    {
        private readonly int memberId;
        private readonly double charge;

        public ChangeMemberTransaction(int empid, int memberId, double charge): base(empid)
        {
            this.memberId = memberId;
            this.charge = charge;
        }
        protected override Affiliation Affiliation { get { return new UnionAffiliation(memberId, charge); } }
        protected override void RecordMembership(Employee e) { PayrollDatabase.instance.AddUnionMember(memberId, e);}
    }
}