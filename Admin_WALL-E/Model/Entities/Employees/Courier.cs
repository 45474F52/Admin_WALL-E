using System.Collections.Generic;

namespace Admin_WALL_E.Model.Entities.Employees
{
    internal sealed class Courier : Employee
    {
        public Courier(Positions position) : base(position) { }

        public override IEnumerable<string> JobDuties => new string[] { "Курьер" };

        public override string FullName => "Курьер";
    }
}