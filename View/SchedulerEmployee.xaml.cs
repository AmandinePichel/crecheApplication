using CrecheApplication.Entity.Implement;
using CrecheApplication.Model;
using CrecheApplication.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
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
    /// Logique d'interaction pour SchedulerEmployee.xaml
    /// </summary>
    public partial class SchedulerEmployee : Window
    {
        public SchedulerEmployee(User employeeConnected)
        {
            InitializeComponent();
        }
    }
}
