using System.Collections.Generic;

namespace Admin_WALL_E.Model.Entities.Employees
{
    internal sealed class Security : Employee
    {
        public Security(Positions position) : base(position) { }

        public override IEnumerable<string> JobDuties => new string[] { "Охранник" };

        public override string FullName => "Охранник";
    }
}