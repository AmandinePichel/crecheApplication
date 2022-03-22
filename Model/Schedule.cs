namespace CrecheApplication.Model
{
    using System;
    using System.Collections.Generic;

    public partial class Schedule
    {
        public Schedule(DateTime date, string moment)
        {
            this.Date = date;
            this.Moment = moment;
        }

        public Schedule()
        {
            this.ChildSchedule = new HashSet<ChildSchedule>();
        }

        public int ScheduleID { get; set; }
        public string Moment { get; set; }
        public Nullable<System.DateTime> Date { get; set; }

        public virtual ICollection<ChildSchedule> ChildSchedule { get; set; }
    }
}
