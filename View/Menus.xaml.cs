using CrecheApplication.ViewModel;
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
using System.Windows.Threading;

namespace CrecheApplication.View
{
    /// <summary>
    /// Logique d'interaction pour Menus.xaml
    /// </summary>
    public partial class Menus : UserControl
    {
        public Menus()
        {
            InitializeComponent();
            DataContext = new MenuViewModel();
            AddMenuPage.Visibility = Visibility.Collapsed;
        }

        private void AddMenu(object sender, RoutedEventArgs e)
        {
            AddMenuPage.Visibility = Visibility.Visible;
            MenuPage.Visibility = Visibility.Collapsed;
        }
        private void AddMenuButton(object sender, RoutedEventArgs e)
        {
            AddMenuPage.Visibility = Visibility.Collapsed;
            MenuPage.Visibility = Visibility.Visible;
        }

        private void CheckAllergies(object sender, RoutedEventArgs e)
        {
            LinkAddMenu.Visibility = Visibility.Collapsed;
            LinkDeleteMenu.Visibility = Visibility.Collapsed;
            var checkAllergiesUC = new CheckAllergies();
            MainArea.Children.Clear();
            MainArea.Children.Add(checkAllergiesUC);
        }
    }
}
