using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Admin_WALL_E.Core
{
    /// <summary>
    /// Предоставляет базовые поля и методы для команды
    /// </summary>
    public abstract class TelegramCommand
    {
        /// <summary>
        /// Название команды
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Выполняет команду
        /// </summary>
        public abstract Task Execute(Message message, ITelegramBotClient botClient, CancellationToken token);

        /// <summary>
        /// Проверяет объект <paramref name="message"/> на совпадение с именем команды независимо от регистра
        /// </summary>
        /// <returns>Возвращает <see langword="true"/>, если текст сообщения равен имени команды, иначе <see langword="false"/></returns>
        public virtual bool Equals(Message message) => message.Type == MessageType.Text && message!.Text!.ToLower().Equals(Name.ToLower());
    }
}