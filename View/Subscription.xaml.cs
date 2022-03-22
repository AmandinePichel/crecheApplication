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
    /// Logique d'interaction pour Subscription.xaml
    /// </summary>
    public partial class Subscription : UserControl
    {
        public Subscription()
        {
            InitializeComponent();
            DataContext = new SubscriptionViewModel();
        }

        private void TextBoxNameClick(object sender, MouseButtonEventArgs e)
        {
            LabelName.Content = "";
            if (textBoxFirstname.Text == "")
            {
                LabelFirstname.Content = "Prénom";
            }
            if (textBoxEmail.Text == "")
            {
                LabelEmail.Content = "Email";
            }
            if (textBoxPassword.Text == "")
            {
                LabelPassword.Content = "Mot de passe";
            }
        }

        private void TextBoxFirstnameClick(object sender, MouseButtonEventArgs e)
        {
            LabelFirstname.Content = "";
            if (textBoxName.Text == "")
            {
                LabelName.Content = "Nom";
            }
            if (textBoxEmail.Text == "")
            {
                LabelEmail.Content = "Email";
            }
            if (textBoxPassword.Text == "")
            {
                LabelPassword.Content = "Mot de passe";
            }
        }

        private void TextBoxEmailClick(object sender, MouseButtonEventArgs e)
        {
            LabelEmail.Content = "";
            if (textBoxName.Text == "")
            {
                LabelName.Content = "Nom";
            }
            if (textBoxFirstname.Text == "")
            {
                LabelFirstname.Content = "Prénom";
            }
            if (textBoxPassword.Text == "")
            {
                LabelPassword.Content = "Mot de passe";
            }
        }

        private void TextBoxPasswordClick(object sender, MouseButtonEventArgs e)
        {
            LabelPassword.Content = "";
            if (textBoxName.Text == "")
            {
                LabelName.Content = "Nom";
            }
            if (textBoxFirstname.Text == "")
            {
                LabelFirstname.Content = "Prénom";
            }
            if (textBoxEmail.Text == "")
            {
                LabelEmail.Content = "Email";
            }
        }
    }
}
