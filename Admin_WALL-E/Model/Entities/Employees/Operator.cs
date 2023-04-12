using System.Collections.Generic;

namespace Admin_WALL_E.Model.Entities.Employees
{
    internal sealed class Operator : Employee
    {
        public Operator(Positions position) : base(position) { }

        public override IEnumerable<string> JobDuties => new string[] { "Оператор 1С" };

        public override string FullName => "Оператор 1С";
    }
}