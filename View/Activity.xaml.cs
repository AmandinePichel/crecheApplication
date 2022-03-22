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
    /// Logique d'interaction pour Activity.xaml
    /// </summary>
    public partial class Activity : UserControl
    {
        public Activity()
        {
            InitializeComponent();
            DataContext = new ActivitiesViewModel();
            ActivitiesViewModel viewModel = new ActivitiesViewModel();
            DataContext = viewModel;
            viewModel.CommandCompleted += EditActivity;
        }

        private void EditActivity()
        {
            ButtonAddActivity.Visibility = Visibility.Collapsed;
            var editActivityUC = new EditActivity();
            MainArea.Children.Clear();
            MainArea.Children.Add(editActivityUC);
        }

        private void AddActivity(object sender, RoutedEventArgs e)
        {
            ButtonAddActivity.Visibility = Visibility.Collapsed;

            var addActivityUC = new AddActivity();
            MainArea.Children.Clear();
            MainArea.Children.Add(addActivityUC);
        }
    }
}
