using CrecheApplication.Model;
using CrecheApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour AddKidStayingMoment.xaml
    /// </summary>
    public partial class AddKidStayingMoment : Window, IClosable
    {
        public AddKidStayingMoment(ObservableCollection<string> dateSelected, string buttonName, SchedulerViewModel schedulerViewModel)
        {
            InitializeComponent();
            DataContext = new AddKidStayingMomentViewModel(dateSelected, buttonName, schedulerViewModel);
        }

        private void AddKidSchedule(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
