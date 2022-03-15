using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrecheApplication.ViewModel
{
    public class WeekDaysDates
    {
        public ObservableCollection<string> daysOfWeek = new ObservableCollection<string>();
        DateTimeFormatInfo dateTimeFormats;
        int currentYear;
        int currentWeekNumber;
        DateTime firstDayOfWeek;

        public WeekDaysDates()
        {
            currentYear = GetCurrentYear();

            currentWeekNumber = GetCurrentWeek(DateTime.Today);

            firstDayOfWeek = FirstDateOfWeek(currentYear, currentWeekNumber, CultureInfo.CurrentCulture);

            isTheWeekend();

            GetNextDay(firstDayOfWeek);
        }

        #region methods

        public void isTheWeekend()
        {
            dateTimeFormats = new CultureInfo("fr-FR").DateTimeFormat;
            // Si on est samedi ou dimanche, afficher la semaine suivante
            if (DateTime.Today.ToString("dddd", dateTimeFormats).Equals("samedi") || DateTime.Today.ToString("dddd", dateTimeFormats).Equals("dimanche"))
            {
                firstDayOfWeek = FirstDateOfWeek(currentYear, currentWeekNumber + 1, CultureInfo.CurrentCulture);
            }
        }

        public ObservableCollection<string> getDaysOfWeek()
        {
            return daysOfWeek;
        }

        // Retourne l'année en cours
        public static int GetCurrentYear()
        {
            DateTime currentTime = DateTime.Now;
            return currentTime.Year;
        }

        // Retourne le chiffre de la semaine en cours en fonction de la date entrée
        public static int GetCurrentWeek(DateTime date)
        {
            CultureInfo culture = new CultureInfo("fr-Fr");
            Calendar calendar = culture.Calendar;

            CalendarWeekRule calendarWeekRule = culture.DateTimeFormat.CalendarWeekRule;
            DayOfWeek dayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;

            return calendar.GetWeekOfYear(date, calendarWeekRule, dayOfWeek);
        }

        public static DateTime FirstDateOfWeek(int year, int weekOfYear, System.Globalization.CultureInfo cultureInfo)
        {
            // Jan1 = 1 janvier 2021
            DateTime jan1 = new DateTime(year, 1, 1);
            // daysOffset = 1(lundi) - 5(vendredi)
            int daysOffset = (int)cultureInfo.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
            // firstWeekDay = 28 décembre 2020 (lundi) 
            DateTime firstWeekDay = jan1.AddDays(daysOffset);

            return firstWeekDay.AddDays(weekOfYear * 7);
        }

        public void GetNextDay(DateTime day)
        {
            string date;
            DateTimeFormatInfo dateTimeFormats;
            dateTimeFormats = new CultureInfo("fr-FR").DateTimeFormat;

            for (int i = 0; i < 5; i++)
            {
                date = day.AddDays(i).ToString("dd/MM/yyyy", dateTimeFormats);
                daysOfWeek.Add(date);
            }
        }
        #endregion
    }
}
