using CrecheApplication.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace CrecheApplication.ViewModel
{
    public class ValidationInput : IDataErrorInfo, INotifyPropertyChanged
    {
        #region Properties

        private string name;
        private string firstname;
        private string email;
        private string password;
        private string address;
        private string passwordEditUserEntry;
        private string descriptionActivityEntry;

        private string menu;
        public string MenuEntry
        {
            get { return menu; }
            set { menu = value; }
        }

        private User selectedUser;
        public User SelectedUser
        {
            get { return selectedUser; }
            set { selectedUser = value; }
        }


        private User selectedParent;
        public User SelectedParent
        {
            get { return selectedParent; }
            set { selectedParent = value;
                OnPropertyChanged("SelectedParent");
            }
        }

        private Child selectedChild;
        public Child SelectedChild
        {
            get { return selectedChild; }
            set { selectedChild = value; }
        }

        private string selectedStatus;
        public string SelectedStatus
        {
            get { return selectedStatus; }
            set { selectedStatus = value; }
        }

        public string NameEntry
        {
            get { return name; }
            set { name = value; }
        }

        public string FirstnameEntry
        {
            get { return firstname; }
            set { firstname = value;
                OnPropertyChanged("FirstnameEntry");
            }
        }

        public string EmailEntry
        {
            get { return email; }
            set { email = value; }
        }

        public string PasswordEntry
        {
            get { return password; }
            set { password = value; }
        }

        public string AddressEntry
        {
            get { return address; }
            set { address = value; }
        }

        public string PasswordEditUserEntry
        {
            get { return passwordEditUserEntry; }
            set { passwordEditUserEntry = value; }
        }

        public string DescriptionActivityEntry
        {
            get { return descriptionActivityEntry; }
            set
            {
                descriptionActivityEntry = value;
            }
        }

        #endregion

        #region IDataErrorInfo Members

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;

                switch (columnName)
                {
                    case "MenuEntry":
                        result = this.MenuValidation();
                        break;

                    case "SelectedUser":
                        result = this.UserValidation();
                        break;

                    case "SelectedParent":
                        result = this.ParentValidation();
                        break;

                    case "SelectedChild":
                            result = this.ChildValidation();
                        break; 

                    case "SelectedStatus":
                        result = this.StatusValidation();
                        break;

                    case "NameEntry":
                        result = this.LastNameValidation();
                        break;

                    case "AddressEntry":
                        result = this.AddressValidation();
                        break;

                    case "FirstnameEntry":
                        result = this.FirstNameValidation();
                        break;

                    case "EmailEntry":
                        result = this.EmailValidation();
                        break;

                    case "PasswordEntry":
                        result = this.PasswordValidation();
                        break;

                    case "PasswordEditUserEntry":
                        result = this.PasswordEditUserValidation();
                        break;

                    case "DescriptionActivityEntry":
                        result = this.DescriptionActivityValidation();
                        break;

                    default:
                        break;
                }

                return result;
            }
        }

        #endregion

        #region Methods
        public bool IsValidConnection()
        {
            string emailValid = this.EmailValidation();
            string passwordValid = this.PasswordValidation();

            // Si tous les champs sont null, ceci signifie qu'ils n'ont retourné aucune erreur, la méthode IsValidConnection retourne true
            if (emailValid == null && passwordValid == null)
            {
                return true;
            }
            return false;
        }

        public bool IsValidSubscription()
        {
            string firstNameValid = this.FirstNameValidation();
            string lastNameValid = this.LastNameValidation();
            string emailValid = this.EmailValidation();
            string passwordValid = this.PasswordValidation();
            string statusValid = this.StatusValidation();

            // Si tous les champs sont null, ceci signifie qu'ils n'ont retourné aucune erreur, la méthode IsValidSubscription retourne true
            if (firstNameValid == null && lastNameValid == null && emailValid == null && passwordValid == null && statusValid == null)
            {
                return true;
            }
           return false;
       }

        public bool IsValidEditUser()
        {
            string firstNameValid = this.FirstNameValidation();
            string lastNameValid = this.LastNameValidation();
            string emailValid = this.EmailValidation();
            string passwordValid = this.PasswordEditUserValidation();

            // Si tous les champs sont null, ceci signifie qu'ils n'ont retourné aucune erreur, la méthode IsValidSubscription retourne true
            if (firstNameValid == null && lastNameValid == null && emailValid == null && passwordValid == null)
            {
                return true;
            }
            return false;
        }

        public bool IsValidMenu()
        {
            string menuValid = this.MenuValidation();

            // Si tous les champs sont null, ceci signifie qu'ils n'ont retourné aucune erreur, la méthode IsValidMenu retourne true
            if (menuValid == null)
            {
                return true;
            }
            return false;
        }

        public bool IsValidChildSubscription()
        {
            string firstNameValid = this.FirstNameValidation();
            string lastNameValid = this.LastNameValidation();
            string addressValid = this.AddressValidation();
            string parentValid = this.ParentValidation();

            // Si tous les champs sont null, ceci signifie qu'ils n'ont retourné aucune erreur, la méthode IsValidSubscription retourne true
            if (firstNameValid == null && lastNameValid == null && addressValid == null && parentValid == null)
            {
                return true;
            }
            return false;
        }

        public bool IsValidDisplayUser()
        {
            string userValid = this.UserValidation();

            if (userValid == null)
            {
                return true;
            }
            return false;
        }

        public bool IsValidDisplayChild()
        {
            string childValid = this.ChildValidation();

            if (childValid == null)
            {
                return true;
            }
            return false;
        }

        public bool IsValidActivityDescription()
        {
            string descriptionActivityValid = this.DescriptionActivityValidation();

            // Si tous les champs sont null, ceci signifie qu'ils n'ont retourné aucune erreur, la méthode IsValidActivityDescription retourne true
            if (descriptionActivityValid == null)
            {
                return true;
            }
            return false;
        }
        
        private string FirstNameValidation()
        {
            string result = null;

            if (string.IsNullOrEmpty(this.FirstnameEntry))
                result = "Veuillez entrer un prénom";
            else if (this.FirstnameEntry.Length > 30)
            { 
                result = "Le prénom est trop long";
            } 
            else if (!IsValidFirstNameAndName(this.FirstnameEntry)) 
            {
                result = "Le prénom ne peut pas contenir que des lettres";
            }
            return result;
        }

        private string LastNameValidation()
        {
            string result = null;

            if (string.IsNullOrEmpty(this.NameEntry))
            {
                result = "Veuillez entrer un nom";
            }
            else if (this.NameEntry.Length > 30)
            {
                result = "Le nom est trop long";
            }
            else if (!IsValidFirstNameAndName(this.NameEntry))
            {
                result = "Le nom ne peut pas contenir que des lettres";
            }
            return result;
        }

        bool IsValidFirstNameAndName(string firstname)
        {
            var accentedCharacters = "àèìòùÀÈÌÒÙáéíóúýÁÉÍÓÚÝâêîôûÂÊÎÔÛãñõÃÑÕäëïöüÿÄËÏÖÜŸçÇßØøÅåÆæœ";
            var tirets = "-";
            try
            {
                return Regex.IsMatch(firstname, "^[a-zA-Z" + accentedCharacters + tirets + "]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private string EmailValidation()
        {
            string result = null;

            if (string.IsNullOrEmpty(this.EmailEntry))
            {
                result = "Veuillez entrer un email";
            }
            else if (this.EmailEntry.Length > 30)
            {
                result = "L'email est trop long";
            }
            else if (!IsValidEmail(this.EmailEntry))
            {
                result = "L'email n'est pas valide";
            }
            return result;
        }

        bool IsValidEmail(string email)
        {
            try
            {
                // Signification Regex : doit contenir une chaine de caractère, un @, une chaine de caractère, un . , une chaine de caractère
                return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private string PasswordValidation()
        {
            string result = null;

            if (string.IsNullOrEmpty(this.PasswordEntry))
            {
                result = "Veuillez entrer un mot de passe";
            }
            else if (this.PasswordEntry.Length > 30)
            {
                result = "Le mot de passe est trop long";
            }
            else if (!IsValidPassword(this.PasswordEntry))
            {
                result = "Le mot de passe doit contenir au minimum 8 caractères,\run caractère spécial, des chiffres, une majuscule et une minuscule";
            }
            return result;
        }

        private string PasswordEditUserValidation()
        {
            string result = null;

            if (string.IsNullOrEmpty(this.PasswordEditUserEntry))
            {
                result = null;
            }
            else if (this.PasswordEditUserEntry.Length > 30)
            {
                result = "Le mot de passe est trop long";
            }
            else if (!IsValidPassword(this.PasswordEditUserEntry))
            {
                result = "Le mot de passe doit contenir au minimum 8 caractères,\run caractère spécial, des chiffres, une majuscule et une minuscule";
            }
            return result;
        }

        bool IsValidPassword(string password)
        {
            try
            {
                return Regex.IsMatch(password, @"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private string AddressValidation()
        {
            string result = null;

            if (string.IsNullOrEmpty(this.AddressEntry))
                result = "Veuillez entrer une adresse";
            else if (this.AddressEntry.Length > 500)
            {
                result = "L'adresse est trop longue";
            }
            return result;
        }

        private string MenuValidation()
        {
            string result = null;

            if (string.IsNullOrEmpty(this.MenuEntry))
            {
                result = "";
            }
            else if (this.MenuEntry.Length > 499)
            {
                result = "La description du menu est limitée à 500 caractères";
            }
            return result;
        }

        private string ParentValidation()
        {
            string result = null;

            if (this.SelectedParent == null)
            {
                result = "Veuillez sélectionner un parent";
            }
            return result;
        }

        private string StatusValidation()
        {
            string result = null;

            if (this.SelectedStatus == null)
            {
                result = "Veuillez sélectionner un statut";
            }
            return result;
        }

        private string UserValidation()
        {
            string result = null;

            if (this.SelectedUser == null)
            {
                result = "Veuillez sélectionner un utilisateur";
            }
            return result;
        }

        private string ChildValidation()
        {
            string result = null;

            if (this.SelectedChild == null)
            {
                result = "Veuillez sélectionner un enfant";
            }
            return result;
        }

        private string DescriptionActivityValidation()
        {
            string result = null;

            if (string.IsNullOrEmpty(this.DescriptionActivityEntry))
                result = "Veuillez entrer une description";
            else if (this.DescriptionActivityEntry.Length > 499)
            {
                result = "La description est trop longue";
            }
            return result;
        }



        #endregion

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
