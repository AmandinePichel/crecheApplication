using CrecheApplication.Model;
using CrecheApplication.View;
using CrecheApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
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

namespace CrecheApplication
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IClosable
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ConnectionViewModel();
        }

        private void TextBoxEmailClick(object sender, MouseButtonEventArgs e)
        {
            LabelEmail.Content = "";
            if (textBoxPassword.Text == "")
            {
                LabelPassword.Content = "Mot de passe";
            }
        }

        private void TextBoxPasswordClick(object sender, MouseButtonEventArgs e)
        {
            LabelPassword.Content = "";
            if (textBoxEmail.Text == "")
            {
                LabelEmail.Content = "Email";
            }
        }

        private void ForgottenPasswordClick(object sender, RoutedEventArgs e)
        {
            ForgottenPassword forgottenPassword = new ForgottenPassword();
            this.Content = forgottenPassword;
        }        
    }
}
