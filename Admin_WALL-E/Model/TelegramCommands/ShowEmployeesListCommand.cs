using Admin_WALL_E.Core;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Admin_WALL_E.Model.TelegramCommands
{
    /// <summary>
    /// Отображает список сотрудников для просмотра информации о них
    /// </summary>
    internal class ShowEmployeesListCommand : TelegramCommand
    {
        /// <summary>
        /// Отображает список сотрудников для просмотра информации о них
        /// </summary>
        public ShowEmployeesListCommand() { }

        public override string Name => "Список сотрудников";

        public override async Task Execute(Message message, ITelegramBotClient botClient, CancellationToken token)
        {
            InlineKeyboardMarkup employeesList = new InlineKeyboardMarkup(App.OrganizationInstance.Employees
                    .Select(employee =>
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(employee.FullName, employee.Position.ToString())
                        }));

            ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(new KeyboardButton("Меню")) { ResizeKeyboard = true };

            await botClient.SendTextMessageAsync(message.Chat.Id,
                                     "Выберите должность для просмотра информации о ней:",
                                     replyMarkup: employeesList,
                                     cancellationToken: token);

            await botClient.SendTextMessageAsync(message.Chat.Id, "Или вернитесь назад", replyMarkup: keyboard, cancellationToken: token);
        }
    }
}