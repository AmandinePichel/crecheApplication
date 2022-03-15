using CrecheApplication.Entity.Implement;
using CrecheApplication.Model;
using CrecheApplication.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

namespace CrecheApplication.ViewModel
{
    public class SchedulerViewModel : INotifyPropertyChanged
    {
        public ICommand AddKidStayingMoment { get; set; }
        public ICommand DeleteKidStayingMoment { get; set; }
        public DaysOfWeek days { get; set; }
        public ObservableCollection<string> daysOfWeek = new ObservableCollection<string>();
        WeekDaysDates weekDaysDates = new WeekDaysDates();

        DateTime date;
        string dateString;
        string moment;
        string childName;
        string childFirstname;

        CrecheEntities context = new CrecheEntities();

        public ObservableCollection<Child> kidsForMondayAM { get; set; }
        public ObservableCollection<Child> kidsForMondayPM { get; set; }
        public ObservableCollection<Child> kidsForTuesdayAM { get; set; }
        public ObservableCollection<Child> kidsForTuesdayPM { get; set; }
        public ObservableCollection<Child> kidsForWednesdayAM { get; set; }
        public ObservableCollection<Child> kidsForWednesdayPM { get; set; }
        public ObservableCollection<Child> kidsForThursdayAM { get; set; }
        public ObservableCollection<Child> kidsForThursdayPM { get; set; }
        public ObservableCollection<Child> kidsForFridayAM { get; set; }
        public ObservableCollection<Child> kidsForFridayPM { get; set; }


        private Child selectedChild;
        public Child SelectedChild
        {
            get { return selectedChild; }
            set { selectedChild = value; }
        }

        public SchedulerViewModel()
        {
            daysOfWeek = weekDaysDates.getDaysOfWeek();

            days = new DaysOfWeek();
            days.DayOfWeek = daysOfWeek;
            days.Monday = daysOfWeek[0];
            days.Tuesday = daysOfWeek[1];
            days.Wednesday = daysOfWeek[2];
            days.Thursday = daysOfWeek[3];
            days.Friday = daysOfWeek[4];

            LoadListBoxData();

            AddKidStayingMoment = new RelayCommand(ExecuteAddKidStayingMoment, CanExecuteAddKidStayingMoment);
            DeleteKidStayingMoment = new RelayCommand(ExecuteDeleteKidStayingMoment, CanExecuteDeleteKidStayingMoment);
        }

        private bool CanExecuteDeleteKidStayingMoment(object parameter)
        {
            return true;
        }

        private void ExecuteDeleteKidStayingMoment(object parameter)
        {
            string buttonName = parameter as string;

            try
            {
                DeleteKidStayingMomentViewModel deleteKidStayingMoment = new DeleteKidStayingMomentViewModel(daysOfWeek, selectedChild, buttonName);

                kidsForMondayAM.Remove(selectedChild);
                kidsForMondayPM.Remove(selectedChild);
                kidsForTuesdayAM.Remove(selectedChild);
                kidsForTuesdayPM.Remove(selectedChild);
                kidsForWednesdayAM.Remove(selectedChild);
                kidsForWednesdayPM.Remove(selectedChild);
                kidsForThursdayAM.Remove(selectedChild);
                kidsForThursdayPM.Remove(selectedChild);
                kidsForFridayAM.Remove(selectedChild);
                kidsForFridayPM.Remove(selectedChild);
            } catch (Exception e)
            {
                Console.WriteLine("Erreur lors de la suppression de l'enfant dans l'ExecuteDeleteKidStayingMoment");
            }
            
        }

        private bool CanExecuteAddKidStayingMoment(object parameter)
        {
            return true;
        }

        private void ExecuteAddKidStayingMoment(object parameter)
        {
            string buttonName = parameter as string;

            AddKidStayingMoment addKidStayingMoment = new AddKidStayingMoment(daysOfWeek, buttonName, this);
            addKidStayingMoment.Show();
        }

        private void LoadListBoxData()
        {
            kidsForMondayAM = new ObservableCollection<Child>();
            kidsForMondayPM = new ObservableCollection<Child>();
            kidsForTuesdayAM = new ObservableCollection<Child>();
            kidsForTuesdayPM = new ObservableCollection<Child>();
            kidsForWednesdayAM = new ObservableCollection<Child>();
            kidsForWednesdayPM = new ObservableCollection<Child>();
            kidsForThursdayAM = new ObservableCollection<Child>();
            kidsForThursdayPM = new ObservableCollection<Child>();
            kidsForFridayAM = new ObservableCollection<Child>();
            kidsForFridayPM = new ObservableCollection<Child>();

            /*
            if (userConnected.Status == "employee") { 
                var query =
                from cs in context.ChildSchedule
                join child in context.Child on cs.fk_Child equals child.KidID
                join sch in context.Schedule on cs.fk_Schedule equals sch.ScheduleID
                select new { child.Firstname, child.Name, sch.Date, sch.Moment };
            }
            if (userConnected.Status == "parent")
            {
                var query =
                from cs in context.ChildSchedule
                join child in context.Child on cs.fk_Child equals child.KidID
                join sch in context.Schedule on cs.fk_Schedule equals sch.ScheduleID
                where child.fk_parent == userConnected.UserID
                select new { child.Firstname, child.Name, sch.Date, sch.Moment };
            }*/


            var query =
            from cs in context.ChildSchedule
            join child in context.Child on cs.fk_Child equals child.KidID
            join sch in context.Schedule on cs.fk_Schedule equals sch.ScheduleID
            select new { child.Firstname, child.Name, sch.Date, sch.Moment };


            foreach (var item in query)
            {
                DateTimeFormatInfo dateTimeFormats;
                dateTimeFormats = new CultureInfo("fr-FR").DateTimeFormat;

                date = (DateTime)item.Date;
                dateString = date.ToString("dd/MM/yyyy", dateTimeFormats);
                moment = item.Moment;
                childName = item.Name;
                childFirstname = item.Firstname;

                if (dateString.Equals(days.Monday) && moment == "AM")
                {
                    kidsForMondayAM.Add(new Child() { FullName = childFirstname + " " + childName });
                }
                if (dateString.Equals(days.Monday) && moment == "PM")
                {
                    kidsForMondayPM.Add(new Child() { FullName = childFirstname + " " + childName });
                }
                if (dateString.Equals(days.Tuesday) && moment == "AM")
                {
                    kidsForTuesdayAM.Add(new Child() { FullName = childFirstname + " " + childName });
                }
                if (dateString.Equals(days.Tuesday) && moment == "PM")
                {
                    kidsForTuesdayPM.Add(new Child() { FullName = childFirstname + " " + childName });
                }
                if (dateString.Equals(days.Wednesday) && moment == "AM")
                {
                    kidsForWednesdayAM.Add(new Child() { FullName = childFirstname + " " + childName });
                }
                if (dateString.Equals(days.Wednesday) && moment == "PM")
                {
                    kidsForWednesdayPM.Add(new Child() { FullName = childFirstname + " " + childName });
                }
                if (dateString.Equals(days.Thursday) && moment == "AM")
                {
                    kidsForThursdayAM.Add(new Child() { FullName = childFirstname + " " + childName });
                }
                if (dateString.Equals(days.Thursday) && moment == "PM")
                {
                    kidsForThursdayPM.Add(new Child() { FullName = childFirstname + " " + childName });
                }
                if (dateString.Equals(days.Friday) && moment == "AM")
                {
                    kidsForFridayAM.Add(new Child() { FullName = childFirstname + " " + childName });
                }
                if (dateString.Equals(days.Friday) && moment == "PM")
                {
                    kidsForFridayPM.Add(new Child() { FullName = childFirstname + " " + childName });
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
    }
}
