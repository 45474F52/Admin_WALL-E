using Admin_WALL_E.Core;

namespace Admin_WALL_E.ViewModel
{
    internal sealed class MainVM : ObservableObject
    {
        public MainVM()
        {
			HomeVM = new HomeVM();
			MonitoringVM = new MonitoringVM();

			GoToHome = new RelayCommand(execute => CurrentView = HomeVM, canExecute => CurrentView is not ViewModel.HomeVM);
			GoToMonitoring = new RelayCommand(execute => CurrentView = MonitoringVM, canExecute => CurrentView is not ViewModel.MonitoringVM);

            TelegramBotHandler.OnBotExecuteStateChanged += state => Status = state ? "Бот запущен" : "Бот остановлен"; ;

			CurrentView = HomeVM;
        }

        public HomeVM HomeVM { get; private set; }
		public MonitoringVM MonitoringVM { get; private set; }

		public RelayCommand GoToHome { get; private set; }
		public RelayCommand GoToMonitoring { get; private set; }

        private object _currentView = null!;
		public object CurrentView
		{
			get => _currentView;
			set
			{
				_currentView = value;
				OnPropertyChanged();
			}
		}

		private string _status = "Бот остановлен";
		public string Status
		{
			get => _status;
			set
			{
				_status = value;
				OnPropertyChanged();
			}
		}
	}
}