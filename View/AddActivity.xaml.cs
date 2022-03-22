﻿using CrecheApplication.ViewModel;
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
    /// Logique d'interaction pour AddActivity.xaml
    /// </summary>
    public partial class AddActivity : UserControl
    {
        public AddActivity()
        {
            InitializeComponent();
            DataContext = new ActivitiesViewModel();
        }

        private void AddActivityClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}