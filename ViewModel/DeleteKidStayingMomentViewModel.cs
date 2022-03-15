using CrecheApplication.Entity.Implement;
using CrecheApplication.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CrecheApplication.ViewModel
{
    public class DeleteKidStayingMomentViewModel
    {
        private string selectedChildName;
        private string buttonName;
        private int userConnectedID;
        private DateTime date;
        private string moment;

        ChildEntity childEntity = new ChildEntity();
        ScheduleEntity scheduleEntity = new ScheduleEntity();
        ChildScheduleEntity childScheduleEntity = new ChildScheduleEntity();

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

        public DeleteKidStayingMomentViewModel(ObservableCollection<string> datesList, Child childName, string buttonName)
        {
            this.datesList = datesList;
            this.buttonName = buttonName;

            // Si un enfant a bel et bien été sélectionné
            if (childName != null)
            {
                this.selectedChildName = childName.FullName;
                
                // Récupère uniquement le prénom
                string[] namesParts = selectedChildName.Split(' ');
                string firstname = namesParts[0];

                // Récupère uniquement le nom
                string name = namesParts[1];

                // Récupère l'id de l'enfant sélectionné
                Child child = new Child(firstname, name);
                Child selectedChild = childEntity.find(child);
                int selectedChildID = selectedChild.KidID;

                // Récupère la date et le moment sélectionné
                getDatesKidStaying(buttonName);
                Schedule newSchedule = new Schedule(date, moment);
                int scheduleID = scheduleEntity.Find(newSchedule);

                // Supprime l'enfant de cette date
                ChildSchedule childSchedule = new ChildSchedule(selectedChildID, scheduleID);
                childScheduleEntity.delete(childSchedule);
            } else
            {
                MessageBox.Show("Veuillez sélectionner un enfant");
            }
        }

        private void getDatesKidStaying(string buttonName)
        {
            DateTimeFormatInfo dateTimeFormats;
            dateTimeFormats = new CultureInfo("fr-FR").DateTimeFormat;

            if (buttonName == "DeleteKidMondayAM")
            {
                selectedDate = datesList[0];
                date = Convert.ToDateTime(selectedDate);
                moment = "AM";
            }
            if (buttonName == "DeleteKidMondayPM")
            {
                selectedDate = datesList[0];
                date = Convert.ToDateTime(selectedDate);
                moment = "PM";
            }
            if (buttonName == "DeleteKidTuesdayAM")
            {
                selectedDate = datesList[1];
                date = Convert.ToDateTime(selectedDate);
                moment = "AM";
            }
            if (buttonName == "DeleteKidTuesdayPM")
            {
                selectedDate = datesList[1];
                date = Convert.ToDateTime(selectedDate);
                moment = "PM";
            }
            if (buttonName == "DeleteKidWednesdayAM")
            {
                selectedDate = datesList[2];
                date = Convert.ToDateTime(selectedDate);
                moment = "AM";
            }
            if (buttonName == "DeleteKidWednesdayPM")
            {
                selectedDate = datesList[2];
                date = Convert.ToDateTime(selectedDate);
                moment = "PM";
            }
            if (buttonName == "DeleteKidThursdayAM")
            {
                selectedDate = datesList[3];
                date = Convert.ToDateTime(selectedDate);
                moment = "AM";
            }
            if (buttonName == "DeleteKidThursdayPM")
            {
                selectedDate = datesList[3];
                date = Convert.ToDateTime(selectedDate);
                moment = "PM";
            }
            if (buttonName == "DeleteKidFridayAM")
            {
                selectedDate = datesList[4];
                date = Convert.ToDateTime(selectedDate);
                moment = "AM";
            }
            if (buttonName == "DeleteKidFridayPM")
            {
                selectedDate = datesList[4];
                date = Convert.ToDateTime(selectedDate);
                moment = "PM";
            }
        }
    }
}
