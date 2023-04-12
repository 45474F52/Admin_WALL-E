using Admin_WALL_E.Model.TelegramCommands;
using System.Collections.Generic;

namespace Admin_WALL_E.Core
{
    /// <summary>
    /// Представляет класс управления командами
    /// </summary>
    internal class TelegramCommandService : ITelegramCommandService
    {
        private readonly IEnumerable<TelegramCommand> _commands;

        /// <summary>
        /// Инициализирует список команд
        /// </summary>
        public TelegramCommandService()
        {
            _commands = new List<TelegramCommand>()
            {
                new MainCommand(),
                new ShowCompanyInfoCommand(),
                new ShowEmployeesListCommand(),
                new ShowEmployeeInfoCommand(),
            };
        }

        public IEnumerable<TelegramCommand> Get() => _commands;
    }
}