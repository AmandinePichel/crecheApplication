using CrecheApplication.Model;
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
using System.Windows.Shapes;

namespace CrecheApplication.View
{
    /// <summary>
    /// Logique d'interaction pour EmployeeMainPage.xaml
    /// </summary>
    public partial class EmployeeMainPage : Window
    {
        User userConnected;
        public EmployeeMainPage(User userConnected)
        {
            InitializeComponent();
            this.userConnected = userConnected;
            DataContext = new EmployeeMainPageViewModel();


        }
        private void HomeClick(object sender, RoutedEventArgs e)
        {
            MainArea.Children.Clear();
            MainArea.Children.Add(HomePage);
        }

        private void KidsInfosClick(object sender, RoutedEventArgs e)
        {
            var kidsInfosUC = new KidsInfos(userConnected);
            MainArea.Children.Clear();
            MainArea.Children.Add(kidsInfosUC);
        }

        private void ScheduleClick(object sender, RoutedEventArgs e)
        {
            var scheduleUC = new SchedulerView();
            MainArea.Children.Clear();
            MainArea.Children.Add(scheduleUC);
        }

        private void MenusClick(object sender, RoutedEventArgs e)
        {
            var menusUC = new Menus();
            MainArea.Children.Clear();
            MainArea.Children.Add(menusUC);
        }

        private void ActivityClick(object sender, RoutedEventArgs e)
        {
            var activityUC = new Activity();
            MainArea.Children.Clear();
            MainArea.Children.Add(activityUC);
        }

        private void UserClick(object sender, RoutedEventArgs e)
        {
            var userUC = new UsersInfos(userConnected);
            MainArea.Children.Clear();
            MainArea.Children.Add(userUC);
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;

            MainArea.HorizontalAlignment = HorizontalAlignment.Right;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }
    }
}
