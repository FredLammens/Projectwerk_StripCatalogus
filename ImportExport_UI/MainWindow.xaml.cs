using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModel;

namespace ImportExport_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ImportExportViewModel vm = new ImportExportViewModel();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = vm;
        }
        private async void ImportComicsBtn_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => Importcomics());
        }
        private void Importcomics()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string path = "";
            if (openFileDialog.ShowDialog() == true)
                path = openFileDialog.FileName;
            try
            {
                if (vm.Import(path) == true)
                    MessageBox.Show("finished importing");
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message, "Error parsing file", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        if (vm.ContinueImport() == true)
                            MessageBox.Show("finished importing");
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                }
            }
        }
        private void export() 
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            try
            {
                vm.Export(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void btnExport_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => export());
        }
    }
}
