using System.Collections.Generic;

namespace Admin_WALL_E.Model.Entities.Employees
{
    internal sealed class Cleaner : Employee
    {
        public Cleaner(Positions position) : base(position) { }

        public override IEnumerable<string> JobDuties => new string[] { "Уборщик" };

        public override string FullName => "Уборщик";
    }
}