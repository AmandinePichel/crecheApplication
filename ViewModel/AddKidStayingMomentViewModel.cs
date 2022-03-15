using CrecheApplication.Entity.Implement;
using CrecheApplication.Model;
using CrecheApplication.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static CrecheApplication.ViewModel.SchedulerViewModel;

namespace CrecheApplication.ViewModel
{
    public class AddKidStayingMomentViewModel : INotifyPropertyChanged
    {

        #region Propriétés
        public ICommand AddKidInDB { get; set; }

        private ObservableCollection<string> datesList;
        public ObservableCollection<string> DatesList
        {
            get { return datesList; }
            set { datesList = value; }
        }

        private string selectedDate;
        public string SelectedDate
        {
            get { return selectedDate; }
            set { selectedDate = value; }
        }

        private ObservableCollection<Child> childrenCombobox;
        public ObservableCollection<Child> ChildrenCombobox
        {
            get { return childrenCombobox; }
            set { childrenCombobox = value; }
        }

        private Child selectedChildAddMoment;
        public Child SelectedChildAddMoment
        {
            get { return selectedChildAddMoment; }
            set { selectedChildAddMoment = value; }
        }

        CrecheEntities context = new CrecheEntities();

        private SchedulerViewModel schedulerViewModel;

        private string moment;
        private DateTime date;
        string buttonName;

        ScheduleEntity scheduleEntity = new ScheduleEntity();
        ChildScheduleEntity childScheduleEntity = new ChildScheduleEntity();

        #endregion

        public AddKidStayingMomentViewModel(ObservableCollection<string> datesList, string buttonName, SchedulerViewModel schedulerViewModel)
        {
            this.schedulerViewModel = schedulerViewModel;
            this.datesList = datesList;
            this.buttonName = buttonName;

            fillCombobox();
            getDatesKidStaying();


            AddKidInDB = new RelayCommand(ExecuteAddKidInDB, CanExecuteAddKidInDB);
        }

        private bool CanExecuteAddKidInDB(object parameter)
        {
            return true;
        }

        private void ExecuteAddKidInDB(object parameter)
        {
            Schedule newSchedule = new Schedule(date, moment);

            int scheduleID = scheduleEntity.Find(newSchedule);

            if(scheduleID == 0)
            {
                scheduleEntity.create(newSchedule);
                newSchedule.ScheduleID = scheduleEntity.Find(newSchedule);
            }
            else
            {
                newSchedule.ScheduleID = scheduleID;
            }

            ChildSchedule newChildSchedule = new ChildSchedule(selectedChildAddMoment.KidID, newSchedule.ScheduleID);

            // Vérfier si le childSchedule n'existe pas encore
            ChildSchedule childScheduleExist = childScheduleEntity.find(newChildSchedule);

            // S'il n'existe pas encore, le créer
            if (childScheduleExist == null)
            {
                childScheduleEntity.create(newChildSchedule);
                UpdateListBox();
            }
            else
            {
                MessageBox.Show("Cet enfant est déjà entré pour cette date");
            }

            IClosable window = parameter as IClosable;

            if (window != null)
            {
                window.Close();
            }
        }

        private void UpdateListBox()
        {

            if (buttonName == "AddKidMondayAM")
            {
                schedulerViewModel.kidsForMondayAM.Add(new Child() { FullName = selectedChildAddMoment.Firstname + " " + selectedChildAddMoment.Name });
            }
            if (buttonName == "AddKidMondayPM")
            {
                schedulerViewModel.kidsForMondayPM.Add(new Child() { FullName = selectedChildAddMoment.Firstname + " " + selectedChildAddMoment.Name });
            }
            if (buttonName == "AddKidTuesdayAM")
            {
                schedulerViewModel.kidsForTuesdayAM.Add(new Child() { FullName = selectedChildAddMoment.Firstname + " " + selectedChildAddMoment.Name });
            }
            if (buttonName == "AddKidTuesdayPM")
            {
                schedulerViewModel.kidsForTuesdayPM.Add(new Child() { FullName = selectedChildAddMoment.Firstname + " " + selectedChildAddMoment.Name });
            }
            if (buttonName == "AddKidWednesdayAM")
            {
                schedulerViewModel.kidsForWednesdayAM.Add(new Child() { FullName = selectedChildAddMoment.Firstname + " " + selectedChildAddMoment.Name });
            }
            if (buttonName == "AddKidWednesdayPM")
            {
                schedulerViewModel.kidsForWednesdayPM.Add(new Child() { FullName = selectedChildAddMoment.Firstname + " " + selectedChildAddMoment.Name });
            }
            if (buttonName == "AddKidThursdayAM")
            {
                schedulerViewModel.kidsForThursdayAM.Add(new Child() { FullName = selectedChildAddMoment.Firstname + " " + selectedChildAddMoment.Name });
            }
            if (buttonName == "AddKidThursdayPM")
            {
                schedulerViewModel.kidsForThursdayPM.Add(new Child() { FullName = selectedChildAddMoment.Firstname + " " + selectedChildAddMoment.Name });
            }
            if (buttonName == "AddKidFridayAM")
            {
                schedulerViewModel.kidsForFridayAM.Add(new Child() { FullName = selectedChildAddMoment.Firstname + " " + selectedChildAddMoment.Name });
            }
            if (buttonName == "AddKidFridayPM")
            {
                schedulerViewModel.kidsForFridayPM.Add(new Child() { FullName = selectedChildAddMoment.Firstname + " " + selectedChildAddMoment.Name });
            }
        }

        private void fillCombobox()
        {
            ChildEntity childEntity = new ChildEntity();

            childrenCombobox = new ObservableCollection<Child>();
            childrenCombobox = childEntity.findAll();
        }

        private void getDatesKidStaying()
        {
            DateTimeFormatInfo dateTimeFormats;
            dateTimeFormats = new CultureInfo("fr-FR").DateTimeFormat;

            if (buttonName == "AddKidMondayAM")
            {
                selectedDate = datesList[0];
                date = Convert.ToDateTime(selectedDate);
                moment = "AM";
                Console.WriteLine(selectedDate);
                Console.WriteLine(date);
            }
            if (buttonName == "AddKidMondayPM")
            {
                selectedDate = datesList[0];
                date = Convert.ToDateTime(selectedDate);
                moment = "PM";
            }
            if (buttonName == "AddKidTuesdayAM")
            {
                selectedDate = datesList[1];
                date = Convert.ToDateTime(selectedDate);
                moment = "AM";
            }
            if (buttonName == "AddKidTuesdayPM")
            {
                selectedDate = datesList[1];
                date = Convert.ToDateTime(selectedDate);
                moment = "PM";
            }
            if (buttonName == "AddKidWednesdayAM")
            {
                selectedDate = datesList[2];
                date = Convert.ToDateTime(selectedDate);
                moment = "AM";
            }
            if (buttonName == "AddKidWednesdayPM")
            {
                selectedDate = datesList[2];
                date = Convert.ToDateTime(selectedDate);
                moment = "PM";
            }
            if (buttonName == "AddKidThursdayAM")
            {
                selectedDate = datesList[3];
                date = Convert.ToDateTime(selectedDate);
                moment = "AM";
            }
            if (buttonName == "AddKidThursdayPM")
            {
                selectedDate = datesList[3];
                date = Convert.ToDateTime(selectedDate);
                moment = "PM";
            }
            if (buttonName == "AddKidFridayAM")
            {
                selectedDate = datesList[4];
                date = Convert.ToDateTime(selectedDate);
                moment = "AM";
            }
            if (buttonName == "AddKidFridayPM")
            {
                selectedDate = datesList[4];
                date = Convert.ToDateTime(selectedDate);
                moment = "PM";
            }
        }

        #region méthode PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
        #endregion
    }
}
