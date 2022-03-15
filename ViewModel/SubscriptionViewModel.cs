using CrecheApplication.Entity.Implement;
using CrecheApplication.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace CrecheApplication.ViewModel
{
    class SubscriptionViewModel
    {
        public ICommand AddUser { get; set; }
        public ValidationInput validation { get; set; }
        public CrecheEntities context = new CrecheEntities();
        UserEntity utilisateurEntity = new UserEntity();
        ObservableCollection<User> usersList = new ObservableCollection<User>();
        PasswordHash passwordHash = new PasswordHash();


        List<string> statusList = new List<string> { "Parent", "Employé", "Directeur" };
        private List<string> statusCombobox;
        public List<string> StatusCombobox
        {
            get { return statusCombobox; }
            set { statusCombobox = value; }
        }

        public SubscriptionViewModel()
        {
            fillStatusCombobox();
            validation = new ValidationInput();
            AddUser = new RelayCommand(ExecuteAddUser, CanExecuteAddUser);
        }

        private bool CanExecuteAddUser(object parameter)
        {
            if (validation.IsValidSubscription())
            {
                return true;
            }
            return false;
        }
        

        // Ajoute un utilisateur en db
        private void ExecuteAddUser(object parameter)
        {
            string statusToDB ="";
            // Si l'utilisateur n'est pas déjà en db
            if (!(isInDataBase()))
            {
                // Hacher le mot de passe
                string passwordHashed = passwordHash.Hash(validation.PasswordEntry);

                // Traduire le statut en anglais
                if (validation.SelectedStatus == "Parent")
                {
                    statusToDB = "parent";
                }
                else if (validation.SelectedStatus == "Employé")
                {
                    statusToDB = "employee";
                }
                else if (validation.SelectedStatus == "Directeur")
                {
                    statusToDB = "director";
                }

                // Ajout de l'utilisateur en db
                User userEntry = new User(validation.NameEntry, validation.FirstnameEntry, validation.EmailEntry, passwordHashed, statusToDB);
                utilisateurEntity.create(userEntry);

                MessageBox.Show("Hello" + " " + userEntry.Name + " " + userEntry.Firstname + "!");
            }
            else
            {
                MessageBox.Show("Cet utilisateur existe déjà");
            }
        }

        // Retourne true si l'utilisateur entré par l'utilisateur est déjà en db
        // Retourne false si l'utilisateur entré par l'utilisateur n'est pas en db
        private bool isInDataBase()
        {
            // Récupère la liste des utilisateurs en db
            usersList = utilisateurEntity.findAll();

            // Parcours la liste d'utilisateur présente en db
            foreach (User item in usersList)
            {
                string emailDb = item.Email;
                if ((emailDb).Equals(validation.EmailEntry))
                {
                    return true;
                }
            }
            return false;
        }

        private void fillStatusCombobox()
        {
            statusCombobox = new List<string>();
            statusCombobox = statusList;
        }
        
    }
}
