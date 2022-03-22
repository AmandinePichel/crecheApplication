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
    /// Logique d'interaction pour SelectKid.xaml
    /// </summary>
    public partial class DeleteKid : UserControl
    {
        User userConnected;
        public DeleteKid(User userConnected)
        {
            InitializeComponent();
            this.userConnected = userConnected;
            DataContext = new KidsInfosViewModel(userConnected);
        }
    }
}
