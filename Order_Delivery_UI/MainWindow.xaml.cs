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

namespace Order_Delivery_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OrderDeliveryViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new OrderDeliveryViewModel();      
        }
        /// <summary>
        /// adds item to combobox
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="comic"></param>
        private void addItem(ComboBox comboBox, ViewComic comic)
        {
            if (!comboBox.Items.Contains(comic))
                comboBox.Items.Add(comic);
        }
        /// <summary>
        /// used for combobox autocomplete feature
        /// </summary>
        private void comicCmb_KeyUp(object sender, KeyEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            bool found = false;
            List<ViewComic> data = viewModel.GetComics();

            string query = (sender as ComboBox).Text;

            if (query.Length == 0)
            {
                comboBox.Items.Clear();
                comboBox.IsDropDownOpen = false;
            }
            comboBox.IsDropDownOpen = true;

            string input = comboBox.Text;
            comboBox.Items.Clear();
            comboBox.Text = input;

            foreach (ViewComic comic in data)
            {
                if (comic.Title.ToLower().StartsWith(query.ToLower()) && !(query == ""))
                {
                    addItem(comboBox, comic);
                    found = true;
                }
            }

            if (!found)
                comboBox.Items.Add("Er zijn geen suggestie gevonden");
        }
    }
}
