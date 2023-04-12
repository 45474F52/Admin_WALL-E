using System.Collections.Generic;

namespace Admin_WALL_E.Core
{
    /// <summary>
    /// Предоставляет базовый функционал, необходимый для реализации объекта управления командами
    /// </summary>
    public interface ITelegramCommandService
    {
        /// <summary>
        /// Возвращает список команд
        /// </summary>
        /// <returns>Возвращает <see cref="IEnumerable{T}"/>, где <see langword="T"/> — <see cref="TelegramCommand"/></returns>
        IEnumerable<TelegramCommand> Get();
    }
}