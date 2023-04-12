using Admin_WALL_E.Core;
using Admin_WALL_E.Model.Entities.Employees;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Admin_WALL_E.Model.TelegramCommands
{
    /// <summary>
    /// Предоставляет информацию о сотруднике
    /// </summary>
    internal class ShowEmployeeInfoCommand : TelegramCommand
    {
        /// <summary>
        /// Предоставляет информацию о сотруднике
        /// </summary>
        public ShowEmployeeInfoCommand() { }

        private Positions _position;

        public override string Name => nameof(Employee);

        public override bool Equals(Message message) => message.Type == MessageType.Text && Enum.TryParse(message.Text, out _position);

        public override async Task Execute(Message message, ITelegramBotClient botClient, CancellationToken token)
        {
            Employee employee = App.OrganizationInstance.Employees.FirstOrDefault(e => e.Position.Equals(_position))
                ?? throw new ArgumentException($"Сотрудника с должностью {_position} не существует");

            ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(new[]
                {
                    new[]
                    {
                        new KeyboardButton("Подробнее")
                    },
                    new[]
                    {
                        new KeyboardButton("Меню")
                    }
                })
            { ResizeKeyboard = true };

            StringBuilder employeeInfo = new StringBuilder();
            foreach (string info in employee.JobDuties)
                employeeInfo.AppendLine(info);

            await botClient.SendTextMessageAsync(message.Chat.Id, employeeInfo.ToString(), replyMarkup: keyboard,
                                                 cancellationToken: token);
        }
    }
}