namespace CrecheApplication.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class Child : INotifyPropertyChanged
    {
        public Child()
        {
            this.ChildSchedule = new HashSet<ChildSchedule>();
        }
        public Child(string firstname, string name, int? fk_parent, DateTime? birthdate, string address, string photo)
        {
            this.Firstname = firstname;
            this.Name = name;
            this.fk_parent = fk_parent;
            this.Birthdate = birthdate;
            this.Address = address;
            this.Photo = photo;
        }

        public Child(int ID, string firstname, string name, int? fk_parent, DateTime? birthdate, string address, string photo)
        {
            this.KidID = ID;
            this.Firstname = firstname;
            this.Name = name;
            this.fk_parent = fk_parent;
            this.Birthdate = birthdate;
            this.Address = address;
            this.Photo = photo;
        }

        public Child(int ID, string firstname, string name)
        {
            this.KidID = ID;
            this.Firstname = firstname;
            this.Name = name;
        }

        public Child(string firstname, string name)
        {
            this.Firstname = firstname;
            this.Name = name;
        }

        private int kidId;
        private string name;
        private string firstname;
        private string fullName;

        public int KidID
        {
            get { return kidId; }
            set
            {
                kidId = value;
                OnPropertyChanged("KidID");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Firstname
        {
            get { return firstname; }
            set
            {
                firstname = value;
                OnPropertyChanged("Firstname");
            }
        }
        public string FullName
        {
            get { return fullName; }
            set
            {
                fullName = value;
                OnPropertyChanged("FullName");
            }
        }

        public Nullable<System.DateTime> Birthdate { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public Nullable<int> fk_parent { get; set; }
        public virtual ICollection<ChildSchedule> ChildSchedule { get; set; }
        public virtual ICollection<ChildSchedule> ChildActivity { get; set; }
        public virtual ICollection<ChildSchedule> ChildFood { get; set; }
        public virtual User User { get; set; }



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
