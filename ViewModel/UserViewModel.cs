using CrecheApplication.Entity.Implement;
using CrecheApplication.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CrecheApplication.ViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {
        public event Action CommandCompleted;
        public ICommand DisplayUser { get; set; }
        public ICommand EditUser { get; set; }
        public ICommand DeleteUser { get; set; }
        

        UserEntity userEntity = new UserEntity();
        ChildEntity childEntity = new ChildEntity();
        PasswordHash passwordHash = new PasswordHash();
        private User userConnected;

        public ValidationInput validation { get; set; }

        List<string> statusList = new List<string> {"Parent","Employé", "Directeur"};

        private ObservableCollection<User> usersCombobox;
        public ObservableCollection<User> UsersCombobox
        {
            get { return usersCombobox; }
            set { usersCombobox = value;
                OnPropertyChanged("UsersCombobox");
            }
        }

        private List<string> statusCombobox;
        public List<string> StatusCombobox
        {
            get { return statusCombobox; }
            set { statusCombobox = value; }
        }

        private string userDisplayed;
        public string UserDisplayed
        {
            get { return userDisplayed; }
            set
            {
                userDisplayed = value;
            }
        }

        public UserViewModel(User userConnected)
        {
            this.userConnected = userConnected;
            validation = new ValidationInput();
            fillUsersCombobox();
            DisplayUser = new RelayCommand(ExecuteDisplayUser, CanExecuteDisplayUser);
            EditUser = new RelayCommand(ExecuteEditUser, CanExecuteEditUser);
            DeleteUser = new RelayCommand(ExecuteDeleteUser, CanExecuteDeleteUser);
        }

        #region méthodes Afficher un utilisateur

        private void fillUsersCombobox()
        {
            usersCombobox = new ObservableCollection<User>();
            ObservableCollection<User> usersFound = userEntity.findAll();
            ObservableCollection<User> usersToShow = new ObservableCollection<User>();

            // N'affiche que les parents et l'utilisateur connecté
            foreach (var item in usersFound)
            {
                if (item.Status == "parent")
                {
                    usersToShow.Add(item);
                }
            }
            usersToShow.Add(userConnected);
            usersCombobox = usersToShow;
        }

        private bool CanExecuteDisplayUser(object arg)
        {
            if (validation.IsValidDisplayUser())
            {
                return true;
            }
            return false;
        }

        private void ExecuteDisplayUser(object obj)
        {
            // Récupérer les informations de l'enfant sélectionné
            User userToDisplay = userEntity.find(validation.SelectedUser.UserID);

            // Ouvrir la fenêtre d'infos sur l'utilisateur sélectionné
            validation.FirstnameEntry = userToDisplay.Firstname;
            validation.NameEntry = userToDisplay.Name;
            validation.EmailEntry = userToDisplay.Email;

            // Traduire le statut écrit en DB
            if (userToDisplay.Status == "parent")
            {
                userDisplayed = "Parent";
            }
            else if (userToDisplay.Status == "employee")
            {
                userDisplayed = "Employé";
            }
            else if (userToDisplay.Status == "director")
            {
                userDisplayed = "Directeur";
            }

            statusCombobox = new List<string>();
            statusCombobox = statusList;


            if (CommandCompleted != null)
                CommandCompleted();
        }

        #endregion

        #region méthodes Modifier un utilisateur
        private bool CanExecuteEditUser(object arg)
        {
            if (validation.IsValidEditUser())
            {
                return true;
            }
            return false;
        }

        private void ExecuteEditUser(object obj)
        {
            string selectedStatusToDB = "";
            string passwordToDB;
            // Récupérer les données de l'utilisateur sélectionné
            User oldUser = userEntity.find(validation.SelectedUser.UserID);

            // Si l'utilisateur a sélectionné un statut, récupérer la sélection. Si pas, garder le statut actuel
            if (string.IsNullOrEmpty(validation.SelectedStatus))
            {
                selectedStatusToDB = oldUser.Status;
            }
            /*else if (validation.SelectedStatus == "Parent")
            {
                selectedStatusToDB = "parent";
            }
            else if (validation.SelectedStatus == "Employé")
            {
                selectedStatusToDB = "employee";
            }
            else if (validation.SelectedStatus == "Directeur")
            {
                selectedStatusToDB = "director";
            }*/

            // Si l'utilisateur a entré un nouveau mot de passe, récupérer l'entrée. Si pas, garder le mot de passe actuel
            if (string.IsNullOrEmpty(validation.PasswordEditUserEntry))
            {
                passwordToDB = oldUser.Password;
            }
            else
            {
                passwordToDB = passwordHash.Hash(validation.PasswordEditUserEntry);
            }

            User newUser = new User(validation.NameEntry, validation.FirstnameEntry, validation.EmailEntry, passwordToDB, selectedStatusToDB);

            // Modifier l'utilisateur
            userEntity.updateFromID(oldUser, newUser);

            MessageBox.Show(newUser.Name + " " + newUser.Firstname + " a bien été modifié");
        }
        #endregion

        #region méthodes Supprimer un utilisateur
        private bool CanExecuteDeleteUser(object arg)
        {
            return true;
        }

        private void ExecuteDeleteUser(object obj)
        {
            MessageBoxResult result = MessageBox.Show($"Etes-vous sûr de vouloir supprimer la fiche de {validation.SelectedUser.Firstname} {validation.SelectedUser.Name} ?", "Vérification", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
            {
                //Ne fait rien
            }
            else if (result == MessageBoxResult.Yes)
            {
                // Vérifier si un enfant a un lien avec le parent sélectionné
                ObservableCollection<Child> children = childEntity.findAll();
                foreach (var item in children)
                {
                    if (item.fk_parent == validation.SelectedUser.UserID)
                    {
                        Console.WriteLine(item.Firstname +" " +item.Address + " " + item.Birthdate); ;
                        // Modifier l'enfant pour lui supprimer le lien avec le parent
                        Child newChild = new Child(item.Firstname, item.Name, null, item.Birthdate, item.Address, item.Photo);
                        childEntity.update(item, newChild);
                    }
                }

                // Supprimer l'utilisateur
                userEntity.delete(validation.SelectedUser);
                MessageBox.Show($"L'utilisateur {validation.SelectedUser.Firstname} {validation.SelectedUser.Name} a bien été supprimé");
                usersCombobox.Remove(validation.SelectedUser);
            }

        }
        #endregion

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
