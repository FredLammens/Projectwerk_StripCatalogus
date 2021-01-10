using Projectwerk_StripCatalogus_UI;
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

namespace Projectwerk_StripCatalogus
{
    /// <summary>
    /// This is the main window for the application where we choose the different pages.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new CataloguePage();
        }

        /// <summary>
        /// The button to open the catalogue page 
        /// </summary>
        private void BtnCatalogue_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new CataloguePage();
        }

        /// <summary>
        /// The button to open the page where we add new comics
        /// </summary>
        private void BtnAddComic_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new AddComicPage();
        }

        private void btnAddOthers_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new AddOthers();
        }

        private void btnUpdateOthers_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new UpdateOthers();
        }
    }
}
