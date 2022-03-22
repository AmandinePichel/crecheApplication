namespace CrecheApplication.Model
{
    using System;
    using System.Collections.Generic;

    public partial class Menu
    {
        public int MenuID { get; set; }
        public string Description { get; set; }

        public virtual Food Food2 { get; set; }
        public Nullable<System.DateTime> dayDate { get; set; }

        public Menu()
        {

        }

        public Menu(string Description, DateTime dayDate)
        {
            this.Description = Description;
            this.dayDate = dayDate;
        }

    }
}
