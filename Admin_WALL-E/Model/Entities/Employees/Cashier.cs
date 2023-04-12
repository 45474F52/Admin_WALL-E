using System.Collections.Generic;

namespace Admin_WALL_E.Model.Entities.Employees
{
    internal sealed class Cashier : Employee
    {
        public Cashier(Positions position) : base(position) { }

        public override IEnumerable<string> JobDuties => new string[] { "Кассир" };

        public override string FullName => "Кассир";
    }
}