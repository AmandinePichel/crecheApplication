using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrecheApplication.ViewModel
{
    public class KidsFullName : INotifyPropertyChanged
    {

        private string kidsNames;
        public string KidsNames
        {
            get { return this.kidsNames; }
            set
            {
                if (this.kidsNames != value)
                {
                    this.kidsNames = value;
                    this.OnPropertyChanged("KidsNames");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
