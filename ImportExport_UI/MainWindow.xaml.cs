using Microsoft.Win32;
using System;
using System.Threading.Tasks;
using System.Windows;
using ViewModel;
namespace ImportExport_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// viewmodel for import/Export.
        /// </summary>
        private ImportExportViewModel vm = new ImportExportViewModel();
        /// <summary>
        /// constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = vm;
        }
        /// <summary>
        /// Import comics on btn click.
        /// </summary>
        private async void ImportComicsBtn_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => Importcomics());
        }
        /// <summary>
        /// asynchronously imports comic after showing dialog.
        /// </summary>
        private void Importcomics()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string path = "";
            if (openFileDialog.ShowDialog() == true)
                path = openFileDialog.FileName;
            try
            {
                vm.Import(path);
                MessageBox.Show("klaar met importeren.");
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message, "fout bij inlezen bestand", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        vm.ContinueImport();
                        MessageBox.Show("klaar met importeren.");
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                }
                else
                    vm.ResetPanels();
            }
        }
        /// <summary>
        /// Exports comics to path
        /// </summary>
        /// <param name="path">path to export to</param>
        private void export(string path)
        {

            try
            {
                vm.Export(path);
                MessageBox.Show("klaar met exporteren.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// asynchronously exports comics.
        /// </summary>
        private async void btnExport_Click(object sender, RoutedEventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                path = dialog.SelectedPath;
            }
            await Task.Run(() => export(path));
        }
    }
}
