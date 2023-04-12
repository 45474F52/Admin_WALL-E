using Admin_WALL_E.Core;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Admin_WALL_E.Model.TelegramCommands
{
    /// <summary>
    /// Меню
    /// </summary>
    internal class MainCommand : TelegramCommand
    {
        /// <summary>
        /// Меню
        /// </summary>
        public MainCommand() { }

        public override string Name { get; } = "/start";

        private const string SecondName = "Меню";

        /// <summary>
        /// Проверяет объект <paramref name="message"/> на совпадение с именем команды независимо от регистра
        /// и на совпадение с <see cref="SecondName"/>
        /// </summary>
        /// <returns>Возвращает <see langword="true"/>, если текст сообщения равен имени команды или <see cref="SecondName"/>,
        /// иначе <see langword="false"/></returns>
        public override bool Equals(Message message) => base.Equals(message) || message.Text == SecondName;

        public override async Task Execute(Message message, ITelegramBotClient botClient, CancellationToken token)
        {
            string text = "Бот WALL-E на связи!" + Environment.NewLine + Environment.NewLine + "Выбери команду:";

            ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(new[]
                {
                    new[]
                    {
                        new KeyboardButton("О компании")
                    },
                    new[]
                    {
                        new KeyboardButton("Список сотрудников")
                    }
                })
            { ResizeKeyboard = true };

            await botClient.SendTextMessageAsync(message.Chat.Id, text, replyMarkup: keyboard, cancellationToken: token);
        }
    }
}