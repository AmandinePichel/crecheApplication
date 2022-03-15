using CrecheApplication.Entity.Implement;
using CrecheApplication.Model;
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
    public class ForgottenPasswordViewModel
    {
        public ICommand EditPassword { get; set; }
        public ValidationInput validation { get; set; }
        public CrecheEntities context = new CrecheEntities();
        UserEntity utilisateurEntity = new UserEntity();
        ObservableCollection<User> usersList = new ObservableCollection<User>();
        PasswordHash passwordHash = new PasswordHash();

        public ForgottenPasswordViewModel()
        {
            validation = new ValidationInput();
            EditPassword = new RelayCommand(ExecuteEditPassword, CanExecuteEditPassword);
        }

        private bool CanExecuteEditPassword(object parameter)
        {
            if (validation.IsValidConnection())
            {
                return true;
            }
            return false;
        }

        private void ExecuteEditPassword(object parameter)
        {
            // Retrouver le même utilisateur en db grâce à son email
            if (findUserDB() != null)
            {
                User userFound = findUserDB();
                string newPasswordHashed = passwordHash.Hash(validation.PasswordEntry);

                // Modification du mot de passe pour l'utilisateur dont l'email a été entré
                User newUser = new User(userFound.Name, userFound.Firstname, userFound.Email, newPasswordHashed, userFound.Status);
                utilisateurEntity.updateFromEmail(newUser);
                MessageBox.Show("Le mot de passe a bien été modifié");
            }
            else
            {
                MessageBox.Show("Cet utilisateur n'existe pas");
            }
        }

        private User findUserDB()
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
