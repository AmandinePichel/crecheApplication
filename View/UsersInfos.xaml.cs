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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CrecheApplication.View
{
    /// <summary>
    /// Logique d'interaction pour UsersInfos.xaml
    /// </summary>
    public partial class UsersInfos : UserControl
    {
        public UsersInfos(User userConnected)
        {
            InitializeComponent();
            UserViewModel viewModel = new UserViewModel(userConnected);
            DataContext = viewModel;
            viewModel.CommandCompleted += EditUser;
        }

        private void AddUser(object sender, RoutedEventArgs e)
        {
            var subscriptionUC = new Subscription();
            MainArea.Children.Clear();
            MainArea.Children.Add(subscriptionUC);
        }

        private void EditUser()
        {
            var editUserUC = new EditUser();
            MainArea.Children.Clear();
            MainArea.Children.Add(editUserUC);
        }

        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            var deleteUserUC = new DeleteUser();
            MainArea.Children.Clear();
            MainArea.Children.Add(deleteUserUC);
        }
    }
}
