using System.Windows;
using System.Windows.Input;

namespace Admin_WALL_E.View
{
    public partial class MainView : Window
    {
        public MainView() => InitializeComponent();

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void HideWindow_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void ExitApp_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
    }
}