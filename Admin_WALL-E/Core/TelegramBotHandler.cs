using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Configuration;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.Enums;

namespace Admin_WALL_E.Core
{
    /// <summary>
    /// Инкапсулирует функции управления телеграм-ботом
    /// </summary>
    public static class TelegramBotHandler
    {
        private static bool _executeBot;
        public static bool BotIsActive
        {
            get => _executeBot;
            private set
            {
                _executeBot = value;
                OnBotExecuteStateChanged?.Invoke(value);
            }
        }

        public delegate void BotExecuteState(bool state);
        public delegate void BotMessage(string message);

        /// <summary>
        /// Возникает при изменении состояния работы телеграм-бота
        /// </summary>
        public static event BotExecuteState? OnBotExecuteStateChanged;

        /// <summary>
        /// Возникает при передаче API сообщения
        /// </summary>
        public static event BotMessage? OnBotAPIMessageTaken;

        /// <summary>
        /// Возникает при перехвате исключения
        /// </summary>
        public static event BotMessage? OnBotCatchingException;

        /// <summary>
        /// Запускает телеграм-бота
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task StartAsync()
        {
            string token = ConfigurationManager.AppSettings["Token"] ?? throw new ArgumentNullException("Токен не найден");
            TelegramBotClient botClient = new TelegramBotClient(token);
            BotIsActive = true;

            using (CancellationTokenSource cts = new CancellationTokenSource())
            {
                ReceiverOptions receiverOptions = new ReceiverOptions() { AllowedUpdates = Array.Empty<UpdateType>() };
                botClient.StartReceiving(
                    updateHandler: HandleUpdateAsync,
                    pollingErrorHandler: HandlePollingErrorAsync,
                    receiverOptions: receiverOptions,
                    cancellationToken: cts.Token);

                botClient.OnApiResponseReceived += HandleAPIResponseAsync;
                botClient.OnMakingApiRequest += HandleAPIRequestAsync;

                await Task.Run(() => { while (BotIsActive) { } });
                cts.Cancel();
            }
        }

        /// <summary>
        /// Останавливает телеграм-бота
        /// </summary>
        public static async Task StopAsync() => await Task.Run(() => BotIsActive = false);

        private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            Message? message = update.Message;
            CallbackQuery? callback = update.CallbackQuery;

            if (callback is not null)
            {
                if (message is not null)
                    message.Text = callback.Data;
                else if (message is null)
                    message = new Message() { Text = callback.Data, Chat = callback.Message!.Chat };
                else
                    return;
            }
            else if (update.Message is null)
                return;

            TelegramCommand? command = App._telegramCommandService.Get().FirstOrDefault(c => c.Equals(message!));
            if (command != null)
                await command.Execute(message!, botClient, token);
            else
                await botClient.SendTextMessageAsync(message!.Chat.Id, $"Команда \"{message.Text}\" не реализована", cancellationToken: token);
        }

        #region Обработка API сообщений
        private static async ValueTask HandleAPIResponseAsync(ITelegramBotClient botClient, ApiResponseEventArgs e, CancellationToken token)
        {
            byte[] buffer = await e.ResponseMessage.Content.ReadAsByteArrayAsync(token);
            string content = Encoding.UTF8.GetString(buffer);

            OnBotAPIMessageTaken?.Invoke("RESPONSE " + content + Environment.NewLine);
        }

        private static async ValueTask HandleAPIRequestAsync(ITelegramBotClient botClient, ApiRequestEventArgs e, CancellationToken token)
        {
            string content = await e.HttpRequestMessage?.Content?.ReadAsStringAsync(token)! ?? "NULL REQUEST";

            OnBotAPIMessageTaken?.Invoke("REQUEST " + content + Environment.NewLine);
        }
        #endregion

        #region Обработка ошибок
        private static async Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception ex, CancellationToken token)
        {
            string errorMessage = ex is ApiRequestException requestException
                ? $"Telegram API Error:\n[{requestException.ErrorCode}]\n{requestException.Message}"
                : ex.ToString();

            OnBotCatchingException?.Invoke(errorMessage);
            await Task.CompletedTask;
        }
        #endregion
    }
}