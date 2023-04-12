using System.Collections.Generic;

namespace Admin_WALL_E.Model.Entities.Employees
{
    internal sealed class Storekeeper : Employee
    {
        public Storekeeper(Positions position) : base(position) { }

        public override IEnumerable<string> JobDuties => new string[] { "Кладовщик" };

        public override string FullName => "Кладовщик";
    }
}