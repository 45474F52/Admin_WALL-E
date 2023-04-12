using Admin_WALL_E.Core;
using Admin_WALL_E.View;
using Admin_WALL_E.Model.Entities;
using Admin_WALL_E.Model.Entities.Employees;
using System.Windows;
using System.Collections.Generic;

namespace Admin_WALL_E
{
    public partial class App : Application
    {
        #region Singleton Messenger
        private static readonly object locker = new object();
        private static volatile IMessenger _messenger = null!;
        public static IMessenger Messenger
        {
            get
            {
                if (_messenger == null)
                {
                    lock (locker)
                    {
                        _messenger ??= new Messenger();
                    }
                }

                return _messenger;
            }
        }
        #endregion

        #region Singleton Organization
        private static readonly object _locker = new object();
        private static volatile Organization _organization = null!;
        public static Organization OrganizationInstance
        {
            get
            {
                if (_organization is null)
                {
                    lock (_locker)
                    {
                        _organization ??= new Organization(name: "ДА!", employees: new HashSet<Employee>()
                        {
                            new Director(Positions.Director),
                            new Administrator(Positions.Administrator),
                            new Supervisor(Positions.Supervisor),
                            new Operator(Positions.Operator_1C),
                            new Storekeeper(Positions.Storekeeper),
                            new Security(Positions.Security),
                            new Courier(Positions.Courier),
                            new Cashier(Positions.Cashier),
                            new Cleaner(Positions.Cleaner)
                        })
                        { Description = "Магазин ДА!" };
                    }
                }

                return _organization;
            }
        }
        #endregion

        /// <summary>
        /// Служба управления командами телеграм-бота
        /// </summary>
        public static readonly ITelegramCommandService _telegramCommandService;

        static App() => _telegramCommandService = new TelegramCommandService();

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainView();
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}