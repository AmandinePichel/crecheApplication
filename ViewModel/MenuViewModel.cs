using CrecheApplication.Entity.Implement;
using CrecheApplication.Model;
using CrecheApplication.View;
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
    public class MenuViewModel : INotifyPropertyChanged
    {
        #region propriétés

        public event Action CommandCompleted;
        public ICommand AddMenu { get; set; }
        public ICommand DeleteMenu { get; set; }
        public ICommand CheckAllergy { get; set; }
        public ICommand AddAllergy { get; set; }

        public ValidationInput validation { get; set; }

        MenuEntity menuEntity = new MenuEntity();
        FoodEntity foodEntity = new FoodEntity();
        ChildEntity childEntity = new ChildEntity();

        private string menuOfTheDay;
        public string MenuOfTheDay
        {
            get { return menuOfTheDay; }
            set { menuOfTheDay = value;
                OnPropertyChanged("MenuOfTheDay");
            }
        }

        private string allergyName;
        public string AllergyName
        {
            get
            { return allergyName; }
            set
            {
                allergyName = value;
                OnPropertyChanged("AllergyName");
            }
        }

        // Label notifiant que l'aliment a bien été ajouté
        private string allergyAdded;
        public string AllergyAdded
        {
            get { return allergyAdded; }
            set
            {
                allergyAdded = value;
                OnPropertyChanged("AllergyAdded");
            }
        }

        // Label notifiant qu'aucun enfant est allergique à l'aliment sélectionné
        private string allergyChecked;
        public string AllergyChecked
        {
            get { return allergyChecked; }
            set
            {
                allergyChecked = value;
                OnPropertyChanged("AllergyChecked");
            }
        }
        
        private ObservableCollection<Food> foodCombobox;
        public ObservableCollection<Food> FoodCombobox
        {
            get { return foodCombobox; }
            set { foodCombobox = value;
                OnPropertyChanged("FoodCombobox");
            }
        }

        private Food selectedFood;
        public Food SelectedFood
        {
            get { return selectedFood; }
            set { selectedFood = value; }
        }

        #endregion

        public MenuViewModel()
        {
            validation = new ValidationInput();

            AddMenu = new RelayCommand(ExecuteAddMenu, CanExecuteAddMenu);
            DeleteMenu = new RelayCommand(ExecuteDeleteMenu, CanExecuteDeleteMenu);
            CheckAllergy = new RelayCommand(ExecuteCheckAllergy, CanExecuteCheckAllergy);
            AddAllergy = new RelayCommand(ExecuteAddAllergy, CanExecuteAddAllergy);

            MenuOfTheDay = ShowMenu();

            fillCheckAllergyCombobox();
        }

        #region méthodes Ajouter un menu

        private bool CanExecuteAddMenu(object parameter)
        {
            // Vérifie si le nombre de caractères ne dépasse pas 500
            if(validation.IsValidMenu())
            {
                return true;
            }
            return false;
        }

        private void ExecuteAddMenu(object parameter)
        {
            // Vérifie si un menu a déjà été entré pour ce jour
            int menuID = menuEntity.find(DateTime.Now).MenuID;

            if (menuID == 0)
            {
                // Ajouter le menu en DB
                Menu menu = new Menu(validation.MenuEntry, DateTime.Now.Date);
                menuEntity.create(menu);
                MenuOfTheDay = ShowMenu();

            } else
            {
                MessageBox.Show("Il existe déjà un menu pour ce jour, veuillez d'abord le supprimer pour pouvoir en ajouter un nouveau");
            }
        }

        #endregion

        #region méthodes Supprimer un menu

        private bool CanExecuteDeleteMenu(object parameter)
        {
            return true;
        }

        private void ExecuteDeleteMenu(object parameter)
        {
            // Vérifie si un menu a déjà été entré pour ce jour
            int menuID = menuEntity.find(DateTime.Now).MenuID;

            if (menuID != 0)
            {
                // Ajouter le menu en DB
                menuEntity.delete(DateTime.Now.Date);

                // Rafraichir l'affichage du menu
                MenuOfTheDay = ShowMenu();
            }
            else
            {
                MessageBox.Show("Aucun menu n'existe pour ce jour, veuillez d'abord en ajouter un avant de pour le supprimer");
            }
        }

        #endregion

        #region méthodes Vérifier allergie

        private void fillCheckAllergyCombobox()
        {
            foodCombobox = new ObservableCollection<Food>();
            foodCombobox = foodEntity.findAll();
        }

        private bool CanExecuteCheckAllergy(object parameter)
        {
            return true;
        }

        private void ExecuteCheckAllergy(object parameter)
        {
            List<string> childListString = new List<string>();
            AllergyChecked = "";
            List<Child> childList = new List<Child>();

            childList = childEntity.findWithFood(selectedFood.FoodID);
            bool isEmptyChildList = !childList.Any();

            if (isEmptyChildList)
            {
                AllergyChecked = $"Aucun enfant allergique à {selectedFood.Name}";
            } else
            {
                // Récupérer les enfants présents aujourd'hui
                List<Child> kidsForToday = childEntity.findForADay((DateTime.Now).Date);
                bool isEmptykidsForToday = !kidsForToday.Any();

                if(isEmptykidsForToday)
                {
                    AllergyChecked = $"Aucun enfant allergique à {selectedFood.Name}";
                } else
                {
                    // Parcourir la liste des enfants qui sont allergique à l'aliment sélectionné
                    foreach (var item in childList)
                    {
                        // Ne sélectionner que les enfants allergiques présents aujourd'hui
                        foreach (var a in kidsForToday)
                        {
                            if (item.Firstname == a.Firstname && item.Name == item.Name)
                            {
                                childListString.Add(a.Firstname + " " + a.Name);
                            }
                        }
                    }
                    var children = string.Join(", ", childListString);
                    MessageBox.Show($"Attention ! " + children + " est/sont allergique(s) à " + selectedFood.Name + " et est présent aujourd'hui", "Notification enfant allergique", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                

            }
        }

        #endregion

        #region méthodes ajouter une allergie en db
        private bool CanExecuteAddAllergy(object parameter)
        {
            if (string.IsNullOrEmpty(AllergyName))
            {
                return false;
            }
            return true;
        }

        private void ExecuteAddAllergy(object parameter)
        {
            // Vérifier si l'allergie n'existe pas encore en DB, l'ajouter
            if (foodEntity.find(AllergyName).FoodID == 0)
            {
                Food food = new Food(AllergyName);
                foodEntity.create(food);

                AllergyAdded = $"L'aliment {AllergyName} a bien été ajouté";

                // Ajouter de suite l'allergie ajoutée au combobox
                foodCombobox.Add(foodEntity.find(AllergyName));
            } else
            {
                MessageBox.Show("Cet aliment existe déjà en base de données");
            }
        }

        #endregion

        #region méthodes Afficher le menu

        private string ShowMenu()
        {
            Menu menu = menuEntity.find(DateTime.Now);
            return menu.Description;
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
