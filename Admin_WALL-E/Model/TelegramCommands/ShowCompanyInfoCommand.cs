using Admin_WALL_E.Core;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Admin_WALL_E.Model.TelegramCommands
{
    /// <summary>
    /// Предоставляет информацию об организации
    /// </summary>
    internal class ShowCompanyInfoCommand : TelegramCommand
    {
        /// <summary>
        /// Предоставляет информацию об организации
        /// </summary>
        public ShowCompanyInfoCommand() { }

        public override string Name => "О компании";

        public override async Task Execute(Message message, ITelegramBotClient botClient, CancellationToken token)
        {
            string text =
                App.OrganizationInstance.Name + Environment.NewLine + Environment.NewLine + App.OrganizationInstance.Description;

            ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(new[]
                {
                    new[]
                    {
                        new KeyboardButton("Список сотрудников")
                    },
                    new[]
                    {
                        new KeyboardButton("Меню")
                    }
                })
            { ResizeKeyboard = true };

            await botClient.SendTextMessageAsync(message.Chat.Id, text, replyMarkup: keyboard, cancellationToken: token);
        }
    }
}