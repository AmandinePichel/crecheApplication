namespace CrecheApplication.Model
{
    using System;
    using System.Collections.Generic;

    public partial class ChildSchedule
    {
        public int ChildScheduleID { get; set; }
        public Nullable<int> fk_Child { get; set; }
        public Nullable<int> fk_Schedule { get; set; }

        public virtual Child Child { get; set; }
        public virtual Schedule Schedule { get; set; }

        public ChildSchedule(int? fk_Child, int? fk_Schedule)
        {
            this.fk_Child = fk_Child;
            this.fk_Schedule = fk_Schedule;
        }

        public ChildSchedule()
        {

        }
        public ChildSchedule(int ChildScheduleID)
        {
            this.ChildScheduleID = ChildScheduleID;
        }
    }
}
