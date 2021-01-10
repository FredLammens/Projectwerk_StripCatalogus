using System.Windows;
using System.Windows.Threading;

namespace ImportExport_UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// used for calling unhandledexceptionhandler
        /// </summary>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            this.Dispatcher.UnhandledException += OnDispatcherUnhandledException;
        }
        /// <summary>
        /// shows the exception in an error message
        /// </summary>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            MessageBox.Show(e.Exception.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
