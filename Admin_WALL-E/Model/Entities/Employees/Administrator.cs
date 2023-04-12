using System.Collections.Generic;

namespace Admin_WALL_E.Model.Entities.Employees
{
    internal sealed class Administrator : Employee
    {
        public Administrator(Positions position) : base(position) { }

        public override IEnumerable<string> JobDuties => new string[] { "Администратор" };

        public override string FullName => "Администратор";
    }
}