using System.Collections.Generic;

namespace Admin_WALL_E.Model.Entities.Employees
{
    internal sealed class Supervisor : Employee
    {
        public Supervisor(Positions position) : base(position) { }

        public override IEnumerable<string> JobDuties => new string[] { "Супервайзер (чмо)", "Конь педальный", "", "Не будьте как он" };

        public override string FullName => "Супервайзер";
    }
}