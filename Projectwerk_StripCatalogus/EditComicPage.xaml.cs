using System;
using System.Collections.Generic;
using System.Linq;
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
using ViewModel.PresentationBaseClasses;

namespace Projectwerk_StripCatalogus_UI
{
    /// <summary>
    /// Interaction logic for EditComicPage.xaml
    /// </summary>
    public partial class EditComicPage : Page
    {
        public EditComicPage(ViewComic selectedComic)
        {
            this.DataContext = new EditComicViewModel(selectedComic);
            InitializeComponent();
        }
    }
}
