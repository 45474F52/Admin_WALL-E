using Admin_WALL_E.Core;

namespace Admin_WALL_E.ViewModel
{
    internal sealed class HomeVM : ObservableObject
    {
        public HomeVM()
        {
            BotIsRun = TelegramBotHandler.BotIsActive;

            ButtonText = BotIsRun ? "Остановить бота" : "Запустить бота";

            ExecuteTBot = new RelayCommand(async execute =>
            {
                if (BotIsRun)
                {
                    BotIsRun = false;
                    ButtonText = "Запустить бота";
                    await TelegramBotHandler.StopAsync();
                }
                else
                {
                    BotIsRun = true;
                    ButtonText = "Остановить бота";
                    await TelegramBotHandler.StartAsync();
                }
            });
        }

        private bool _botIsRun;
        public bool BotIsRun
        {
            get => _botIsRun;
            set
            {
                _botIsRun = value;
                OnPropertyChanged();
            }
        }

        private string _buttonText = string.Empty;
        public string ButtonText
        {
            get => _buttonText;
            set
            {
                _buttonText = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand ExecuteTBot { get ; private set; }
    }
}