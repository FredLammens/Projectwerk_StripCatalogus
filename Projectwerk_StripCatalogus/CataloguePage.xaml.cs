using System;
using System.Collections.Generic;
using System.Text;
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

namespace Projectwerk_StripCatalogus_UI
{
    /// <summary>
    /// Interaction logic for CataloguePage.xaml
    /// </summary>
    public partial class CataloguePage : Page
    {
        public CataloguePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// In this method we will change the properties of the selected comic
        /// </summary>
        private void btnChangeComic_Click(object sender, RoutedEventArgs e)
        {
            GridRow selected = (GridRow)dgComic.SelectedItem;
            if (selected == null)
                MessageBox.Show("Gelieve een strip te selecteren a.u.b.","Warning",MessageBoxButton.OK);
            else
            {
                EditComicPage editComicPage = new EditComicPage(selected.Comic);
                NavigationService.Navigate(editComicPage);
            }
        }
    }
}
