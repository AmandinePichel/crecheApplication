using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrecheApplication.ViewModel
{
    public class DaysOfWeek
    {
        private string monday;
        private string tuesday;
        private string wednesday;
        private string thursday;
        private string friday;
        private ObservableCollection<string> dayOfWeek;

        public string Monday
        {
            get { return monday; }
            set { monday = value; }
        }

        public string Tuesday
        {
            get { return tuesday; }
            set { tuesday = value; }
        }

        public string Wednesday
        {
            get { return wednesday; }
            set { wednesday = value; }
        }

        public string Thursday
        {
            get { return thursday; }
            set { thursday = value; }
        }

        public string Friday
        {
            get { return friday; }
            set { friday = value; }
        }
        public ObservableCollection<string> DayOfWeek
        {
            get { return dayOfWeek; }
            set { dayOfWeek = value; }
        }
    }
}
