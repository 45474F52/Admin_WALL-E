using Admin_WALL_E.Model.Entities.Employees;
using System.Collections.Generic;

namespace Admin_WALL_E.Model.Entities
{
    /// <summary>
    /// Представляет объект организации с именем, описанием и списком сотрудников
    /// </summary>
    public sealed class Organization
    {
        /// <summary>
        /// Инициализирует объект <see cref="Organization"/>
        /// </summary>
        /// <param name="name">Название организации</param>
        /// <param name="employees">Список сотрудников в организации</param>
        public Organization(string name, HashSet<Employee> employees)
        {
            Name = name;
            Employees = employees;
        }

        /// <summary>
        /// Название организации
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Список сотрудников в организации
        /// </summary>
        public HashSet<Employee> Employees { get; init; }

        /// <summary>
        /// Описание организации
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}