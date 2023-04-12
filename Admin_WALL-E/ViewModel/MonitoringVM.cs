using Admin_WALL_E.Core;
using System.Text;

namespace Admin_WALL_E.ViewModel
{
    internal sealed class MonitoringVM : ObservableObject
    {
        public MonitoringVM()
        {
            _infoBuilder = new StringBuilder();
			_errorBuilder = new StringBuilder();

			LogErrors = new RelayCommand(execute => { }, canExecute => false);

			ClearAPIInfo = new RelayCommand(execute =>
			{
				_infoBuilder.Clear();
				Info = string.Empty;
			}, canExecute => _infoBuilder.Length > 0);

			ClearErrors = new RelayCommand(execute =>
			{
				_errorBuilder.Clear();
				Error = string.Empty;
			}, canExecute => _errorBuilder.Length > 0);

            TelegramBotHandler.OnBotAPIMessageTaken += TelegramBotAPIMessageTaken;
            TelegramBotHandler.OnBotCatchingException += TelegramBotCatchingException;
        }

        private void TelegramBotAPIMessageTaken(string message)
        {
            _infoBuilder.AppendLine(message);
			Info = _infoBuilder.ToString();
        }

        private void TelegramBotCatchingException(string message)
        {
            _errorBuilder.AppendLine(message);
            Error = _errorBuilder.ToString();
        }

        private readonly StringBuilder _infoBuilder;
		private readonly StringBuilder _errorBuilder;

		public RelayCommand LogErrors { get; private set; }
		public RelayCommand ClearAPIInfo { get; private set; }
		public RelayCommand ClearErrors { get; private set; }

		private string _info = "API INFO";
		public string Info
		{
			get => _info;
			set
			{
				_info = value;
				OnPropertyChanged();
			}
		}

		private string _error = "ERRORS";
		public string Error
		{
			get => _error;
			set
			{
				_error = value;
				OnPropertyChanged();
			}
		}
	}
}