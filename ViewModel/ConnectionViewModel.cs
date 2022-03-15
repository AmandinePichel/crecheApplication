using CrecheApplication.Entity.Implement;
using CrecheApplication.Model;
using CrecheApplication.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CrecheApplication.ViewModel
{
    class ConnectionViewModel
    {
        public ICommand ConnectUser { get; set; }
        public ValidationInput validation { get; set; }
        public CrecheEntities context = new CrecheEntities();
        UserEntity utilisateurEntity = new UserEntity();
        ObservableCollection<User> usersList = new ObservableCollection<User>();
        PasswordHash passwordHash = new PasswordHash();
        public int userConnected;

        public ConnectionViewModel()
        {
            validation = new ValidationInput();
            ConnectUser = new RelayCommand(ExecuteConnectUser, CanExecuteConnectUser);
        }

        private bool CanExecuteConnectUser(object parameter)
        {
            if(validation.IsValidConnection())
            {
                return true;
            }
            return false;
        }

        private void ExecuteConnectUser(object parameter)
        {
            // Retrouver le même utilisateur en db grâce à son email
            if (findUserDB() != null)
            {
                // Comparer les mots de passe
                // Le mot de passe correspondant à l'utilisateur en db
                string dbPassword = findUserDB().Password;

                if (PasswordHash.Verify(validation.PasswordEntry, dbPassword))
                {
                    // Vérifie le statut de l'utilisateur
                    if (findUserDB().Status != "parent")
                    {
                        EmployeeMainPage employeeMainPage = new EmployeeMainPage(findUserDB());
                        employeeMainPage.Show(); 

                        IClosable window = parameter as IClosable;

                        if (window != null)
                        {
                            window.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vous n'avez pas accès à cette partie");
                    }
                }
                else
                {
                    MessageBox.Show("Le mot de passe est erroné");
                }
            }
            else
            {
                MessageBox.Show("Cet utilisateur n'existe pas");
            }
        }

        private User findUserDB ()
        {
            User userFound = null;

            // Récupère la liste des utilisateurs en db
            usersList = utilisateurEntity.findAll();

            // Parcours la liste d'utilisateur présente en db
            foreach (User item in usersList)
            {
                string emailDb = item.Email;
                if ((emailDb).Equals(validation.EmailEntry))
                {
                    userFound = item;
                }
            }
            return userFound;
        }

    }
}
