using CrecheApplication.Model;
using CrecheApplication.ViewModel;
using Microsoft.Win32;
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
    /// Logique d'interaction pour AddKidsInfos.xaml
    /// </summary>
    public partial class AddKidsInfos : UserControl
    {
        User userConnected;
        public AddKidsInfos(User userConnected)
        {
            InitializeComponent();
            this.userConnected = userConnected;
            DataContext = new KidsInfosViewModel(userConnected);
        }

        private void TextBoxNameClick(object sender, MouseButtonEventArgs e)
        {
            LabelName.Content = "";
            if (textBoxFirstname.Text == "")
            {
                LabelFirstname.Content = "Prénom";
            }
            if (textBoxAddress.Text == "")
            {
                LabelAddress.Content = "Lieu de résidence";
            }
        }

        private void TextBoxFirstnameClick(object sender, MouseButtonEventArgs e)
        {
            LabelFirstname.Content = "";
            if (textBoxName.Text == "")
            {
                LabelName.Content = "Nom";
            }
            if (textBoxAddress.Text == "")
            {
                LabelAddress.Content = "Lieu de résidence";
            }
        }

        private void TextBoxAddressClick(object sender, MouseButtonEventArgs e)
        {
            LabelAddress.Content = "";
            if (textBoxName.Text == "")
            {
                LabelName.Content = "Nom";
            }
            if (textBoxName.Text == "")
            {
                LabelFirstname.Content = "Prénom";
            }
        }
    }
}
