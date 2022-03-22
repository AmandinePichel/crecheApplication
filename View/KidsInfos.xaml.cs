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
    /// Logique d'interaction pour KidsInfos.xaml
    /// </summary>
    public partial class KidsInfos : UserControl
    {
        User userConnected;
        public KidsInfos(User userConnected)
        {
            InitializeComponent();
            this.userConnected = userConnected;
            KidsInfosViewModel viewModel = new KidsInfosViewModel(userConnected);
            DataContext = viewModel;
            viewModel.CommandCompleted += EditKid;
        }
        
        private void AddKid(object sender, RoutedEventArgs e)
        {
            var addKidsInfosUC = new AddKidsInfos(userConnected);
            MainArea.Children.Clear();
            MainArea.Children.Add(addKidsInfosUC);
        }

        private void EditKid()
        {
            var editKidUC = new EditKid();
            MainArea.Children.Clear();
            MainArea.Children.Add(editKidUC);
        }

        private void DeleteKid(object sender, RoutedEventArgs e)
        {
            var deleteKidUC = new DeleteKid(userConnected);
            MainArea.Children.Clear();
            MainArea.Children.Add(deleteKidUC);
        }

    }
}
