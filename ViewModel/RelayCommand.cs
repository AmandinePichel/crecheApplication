using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CrecheApplication.ViewModel
{
    public class RelayCommand : ICommand
    {
        Action<object> _execteMethod;
        Func<object, bool> _canexecuteMethod;

        // Constructeur
        public RelayCommand(Action<object> execteMethod, Func<object, bool> canexecuteMethod)
        {
            _execteMethod = execteMethod;
            _canexecuteMethod = canexecuteMethod;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        // Défini si on a la permission d'executer la commande ou pas (le bouton click event).
        // On met "null" pour vérifier is _canexecuteMethod is initialisé ou pas. 
        // Si non, on retourne "false", si oui, on retourne "true".
        public bool CanExecute(object parameter)
        {
            if (_canexecuteMethod != null)
            {
                return _canexecuteMethod(parameter);
            }
            else
            {
                return false;
            }
        }

        // Si la méthode CanExecute retourne "true", alors on l'execute.
        public void Execute(object parameter)
        {
            _execteMethod(parameter);
        }
    }
}
