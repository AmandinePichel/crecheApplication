namespace CrecheApplication.Model
{
    using CrecheApplication.ViewModel;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text.RegularExpressions;
    using System.Windows;

    public partial class User : INotifyPropertyChanged
    {
        private int userId;
        private string name;
        private string firstname;
        private string email;
        private string password;
        private string status;

        public User()
        {

        }

        public User(int id, string nom, string prenom, string email, string motDePasse, string status)
        {
            this.UserID = id;
            this.Name = nom;
            this.Firstname = prenom;
            this.Email = email;
            this.Password = motDePasse;
            this.Status = status;
        }

        public User(string nom, string prenom, string email, string motDePasse, string status)
        {
            this.Name = nom;
            this.Firstname = prenom;
            this.Email = email;
            this.Password = motDePasse;
            this.Status = status;
        }

        public int UserID
        {
            get { return userId; }
            set
            {
                userId = value;
                OnPropertyChanged("idUtilisateur");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (CheckEntryName(value))
                {
                    name = value;
                    OnPropertyChanged("name");
                }
            }
        }
        public string Firstname
        {
            get { return firstname; }
            set
            {
                if (CheckEntryFirstname(value))
                {
                    firstname = value;
                    OnPropertyChanged("firstname");
                }
            }
        }
        public string Email
        {
            get { return email; }
            set
            {
                if(CheckEntryEmail(value))
                {
                    email = value;
                    OnPropertyChanged("email");
                }
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                if (CheckEntryPassword(value))
                {
                    password = value;
                    OnPropertyChanged("password");
                }
            }
        }

        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged("status");
            }
        }

        public virtual ICollection<Child> Child { get; set; }
        public virtual ICollection<ChildSchedule> UserActivity { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Tests unitaires
        static public bool CheckEntryName(string name)
        {
            //si le nom débute ou termine par un espace ou un tiret
            if (name.StartsWith(" ") || name.StartsWith("-") || name.EndsWith(" ") || name.EndsWith("-"))
            {
                MessageBox.Show($"La saisie {name} ne peut commencer ou se terminer par un espace ou un tiret", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        static public bool CheckEntryFirstname(string firstname)
        {
            //si le prénom débute ou termine par un espace ou un tiret
            if (firstname.StartsWith(" ") || firstname.StartsWith("-") || firstname.EndsWith(" ") || firstname.EndsWith("-"))
            {
                MessageBox.Show($"La saisie {firstname} ne peut commencer ou se terminer par un espace ou un tiret", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        static public bool CheckEntryPassword(string password)
        {
            //si le mot de passe est vide
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show($"Le champs mot de passe ne peut pas être vide", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //doit contenir une majuscule, une minuscule, un caractère spécial, 8 caractères minimum
            if (!Regex.IsMatch(password, @"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$"))
            {
                MessageBox.Show($"Le mot de passe '{password}' doit contenir au moins 8 caractères, une majuscule, une minuscule et un caractère spécial", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        static public bool CheckEntryEmail(string email)
        {
            //si l'email est vide
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show($"Le champs email ne peut pas être vide", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //doit contenir une chaine de caractère, un @, une chaine de caractère, un . , une chaine de caractère
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show($"L'email '{email}' n'est pas valide", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        #endregion
    }
}
