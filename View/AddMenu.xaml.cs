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

namespace CrecheApplication.View
{
    /// <summary>
    /// Logique d'interaction pour AddMenu.xaml
    /// </summary>
    public partial class AddMenu : UserControl
    {
        public AddMenu()
        {
            InitializeComponent();
            DataContext = new MenuViewModel();
        }

        private void AddMenuButton(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new DisplayMenu());
            //(this.Parent as Grid).Children.Remove(this);
            //Menus menus = new Menus();
            //menus.Refresh();

            //((Panel)this.Parent).Children.Remove(this);
            //((Panel)this.Parent).Children.Add(menus.MenuPage);
        }
    }
}
