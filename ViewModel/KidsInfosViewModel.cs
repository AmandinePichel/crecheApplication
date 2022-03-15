using CrecheApplication.Entity.Implement;
using CrecheApplication.Model;
using CrecheApplication.View;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace CrecheApplication.ViewModel
{
    public class KidsInfosViewModel : INotifyPropertyChanged
    {

        #region Propriétés
        public event Action CommandCompleted;

        public ICommand DisplayKid { get; set; }
        public ICommand AddChild { get; set; }
        public ICommand AddPicture { get; set; }
        public ICommand EditChild { get; set; }
        public ICommand DeleteChild { get; set; }

        FoodEntity foodEntity = new FoodEntity();
        ChildEntity childEntity = new ChildEntity();
        ChildFoodEntity childFoodEntity = new ChildFoodEntity();
        UserEntity userEntity = new UserEntity();
        ChildScheduleEntity childSchedule = new ChildScheduleEntity();


        public ValidationInput validation { get; set; }
        public Food food { get; set; }
        private User userConnected;
        string pictureName;
        public List<Food> foodList;

        public ObservableCollection<Food> FoodCheckBox { get; set; }

        private BitmapSource picture;
        public BitmapSource Picture
        {
            get { return picture; }
            set
            {
                picture = value;
                OnPropertyChanged("Picture");
            }
        }


        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        private string parentDisplayed;
        public string ParentDisplayed
        {
            get { return parentDisplayed; }
            set
            {
                parentDisplayed = value;
            }
        }

        private ObservableCollection<Child> childrenCombobox;
        public ObservableCollection<Child> ChildrenCombobox
        {
            get { return childrenCombobox; }
            set
            {
                childrenCombobox = value;
                OnPropertyChanged("ChildrenCombobox");
            }
        }

        private ObservableCollection<User> parentsCombobox;
        public ObservableCollection<User> ParentsCombobox
        {
            get { return parentsCombobox; }
            set { parentsCombobox = value; }
        }

        private DateTime selectedDate;

        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set { selectedDate = value; }
        }

        #endregion

        public KidsInfosViewModel(User userConnected)
        {
            this.userConnected = userConnected;
            validation = new ValidationInput();
            food = new Food();
            picture = new BitmapImage(new Uri("pack://application:,,,/Images/noPictureYet.png"));

            fillCheckBoxes();
            fillChildCombobox();
            fillParentCombobox();
            DisplayKid = new RelayCommand(ExecuteDisplayKid, CanExecuteDisplayKid);
            AddChild = new RelayCommand(ExecuteAddChild, CanExecutAddChild);
            AddPicture = new RelayCommand(ExecuteAddPicture, CanExecutAddPicture);
            EditChild = new RelayCommand(ExecuteEditChild, CanExecutEditChild);
            DeleteChild = new RelayCommand(ExecuteDeleteChild, CanExecutDeleteChild);
        }

        #region méthodes Ajouter un enfant
        private bool CanExecutAddPicture(object arg)
        {
            return true;
        }

        private void ExecuteAddPicture(object obj)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            openFile.Multiselect = false;
            openFile.Title = "Sélectionner une image";
            openFile.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";
            if (openFile.ShowDialog() == true)
            {
                Picture = LoadImageFile(openFile.FileName);
            }

            pictureName = openFile.FileName;
        }

        // Renvoie l'image sélectionnée droite (sans rotation due au BitMapMetaData)
        public static BitmapSource LoadImageFile(String path)
        {
            Rotation rotation = Rotation.Rotate0;
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                BitmapFrame bitmapFrame = BitmapFrame.Create(fileStream, BitmapCreateOptions.DelayCreation, BitmapCacheOption.None);
                BitmapMetadata bitmapMetadata = bitmapFrame.Metadata as BitmapMetadata;

                if ((bitmapMetadata != null) && (bitmapMetadata.ContainsQuery("System.Photo.Orientation")))
                {
                    object o = bitmapMetadata.GetQuery("System.Photo.Orientation");

                    if (o != null)
                    {
                        switch ((ushort)o)
                        {
                            case 6:
                                {
                                    rotation = Rotation.Rotate90;
                                }
                                break;
                            case 3:
                                {
                                    rotation = Rotation.Rotate180;
                                }
                                break;
                            case 8:
                                {
                                    rotation = Rotation.Rotate270;
                                }
                                break;
                        }
                    }
                }
            }

            BitmapImage _image = new BitmapImage();
            _image.BeginInit();
            _image.UriSource = new Uri(path);
            _image.Rotation = rotation;
            _image.EndInit();
            _image.Freeze();

            return _image;
        }

        private bool CanExecutAddChild(object SelectedItems)
        {
            if (validation.IsValidChildSubscription())
            {
                return true;
            }
            return false;
        }

        private void ExecuteAddChild(object SelectedItems)
        {
            IList items = (IList)SelectedItems;
            var collection = items.Cast<Food>();

            // Si l'enfant n'est pas déjà en db
            if (!(isInDataBase()))
            {
                Child newChild = new Child(validation.FirstnameEntry, validation.NameEntry, validation.SelectedParent.UserID, selectedDate, validation.AddressEntry, pictureName);
                // Créer l'enfant
                childEntity.create(newChild);

                // Récupérer l'ID de l'enfant inséré en DB
                int kidId = childEntity.find(newChild).KidID;

                // Lier l'enfant aux allergies sélectionnées
                foreach (var item in collection)
                {
                    childFoodEntity.create(kidId, item.FoodID);
                }

                MessageBox.Show(newChild.Name + " " + newChild.Firstname + " fait désormait partie de la crèche");
            }
            else
            {
                MessageBox.Show("Cet enfant existe déjà");
            }
        }

        private void fillCheckBoxes()
        {
            // Récupère le nom des aliments et les affiches dans une listbox contenant des checkbox
            FoodCheckBox = new ObservableCollection<Food>();

            foodEntity.findAll();

            foreach (var item in foodEntity.findAll())
            {
                FoodCheckBox.Add(new Food() { Name = item.Name, FoodID = item.FoodID });
            }
        }
        private void fillParentCombobox()
        {
            UserEntity userEntity = new UserEntity();

            parentsCombobox = new ObservableCollection<User>();
            parentsCombobox = userEntity.findAll();
        }

        // Retourne true si l'enfant est déjà en db
        // Retourne false si l'enfant n'est pas en db
        private bool isInDataBase()
        {
            ObservableCollection<Child> children = new ObservableCollection<Child>();

            children = childEntity.findAll();

            foreach (Child item in children)
            {
                /*string nameDb = item.Name;
                string firstnameDb = item.Firstname;
                int fk_parentDb = (int)item.fk_parent;*/

                if ((item.Name).Equals(validation.NameEntry, StringComparison.OrdinalIgnoreCase) && (item.Firstname).Equals(validation.FirstnameEntry, StringComparison.OrdinalIgnoreCase) && item.fk_parent == validation.SelectedParent.UserID)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region méthodes Afficher un enfant

        private void fillChildCombobox()
        {
            ChildEntity childEntity = new ChildEntity();

            childrenCombobox = new ObservableCollection<Child>();
            childrenCombobox = childEntity.findAll();
        }

        private bool CanExecuteDisplayKid(object arg)
        {
            if (validation.IsValidDisplayChild())
            {
                return true;
            }
            return false;
        }

        private void ExecuteDisplayKid(object obj)
        {
            // Récupérer les informations de l'enfant sélectionné
            Child childToDisplay = childEntity.find(validation.SelectedChild);

            // Ouvrir la fenêtre d'infos sur l'enfant sélectionné
            validation.FirstnameEntry = childToDisplay.Firstname;
            validation.NameEntry = childToDisplay.Name;
            validation.AddressEntry = childToDisplay.Address;
            selectedDate = (DateTime)childToDisplay.Birthdate;

            if ((string.IsNullOrEmpty(childToDisplay.Photo)))
            {
                picture = new BitmapImage(new Uri("pack://application:,,,/Images/noPictureYet.png"));
            }
            else
            {
                picture = LoadImageFile(childToDisplay.Photo);
            }


            //allergies
            foodList = childFoodEntity.findForAChild(validation.SelectedChild);

            foreach (var item in FoodCheckBox)
            {
                // Si les id des food correspondent avec ceux de la liste d'aliment (la liste complète), mettre la propriété IsChecked à true
                item.IsChecked = foodList.Any(x => x.FoodID == item.FoodID);
            }

            // Récupérer le nom du parent lié à l'enfant
            if (childToDisplay.fk_parent != null)
            {
                User parent = userEntity.find((int)childToDisplay.fk_parent);
                validation.SelectedParent = parent;
                ParentDisplayed = parent.Firstname + " " + parent.Name;
            }

            if (CommandCompleted != null)
                CommandCompleted();
        }

        #endregion

        #region méthodes Modifier un enfant

        private bool CanExecutEditChild(object arg)
        {
            if (validation.IsValidChildSubscription())
            {
                return true;
            }
            return false;
        }

        private void ExecuteEditChild(object SelectedItems)
        {
            IList items = (IList)SelectedItems;
            var collection = items.Cast<Food>();

            int selectedParentID;
            // Récupérer les données de l'enfant sélectionné
            Child oldChild = childEntity.find(validation.SelectedChild);

            // Si l'utilisateur a sélectionné un parent, récupérer la sélection. Si pas, garder la fk_parent actuelle
            if (validation.SelectedParent.UserID == 0)
            {
                selectedParentID = (int)oldChild.fk_parent;
            }
            else
            {
                selectedParentID = validation.SelectedParent.UserID;
            }

            if ((string.IsNullOrEmpty(pictureName)))
            {
                pictureName = oldChild.Photo;
            }

            Child newChild = new Child(validation.FirstnameEntry, validation.NameEntry, selectedParentID, selectedDate, validation.AddressEntry, pictureName);

            // Modifier l'enfant
            childEntity.update(oldChild, newChild);

            // Lier l'enfant aux allergies sélectionnées
            foreach (var item in collection)
            {
                childFoodEntity.update(oldChild, item.FoodID);
            }
            foreach (var item in collection)
            {
                childFoodEntity.create(oldChild.KidID, item.FoodID);
            }

            MessageBox.Show(newChild.Name + " " + newChild.Firstname + " a bien été modifié");
        }

        #endregion

        #region méthodes pour supprimer un enfant

        private bool CanExecutDeleteChild(object arg)
        {
            // Si c'est un directeur qui est connecté, autoriser la suppresion de la fiche
            if (userConnected.Status == "director")
            {
                return true;
            }
            return false;
        }

        private void ExecuteDeleteChild(object obj)
        {

            MessageBoxResult result = MessageBox.Show($"Etes-vous sûr de vouloir supprimer la fiche de {validation.SelectedChild.Firstname} {validation.SelectedChild.Name} ?", "Vérification", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
            {
                //Ne fait rien
            }
            else if (result == MessageBoxResult.Yes)
            {
                // Recherche si l'enfant a une allergie
                List<Food> allergies = childFoodEntity.findForAChild(validation.SelectedChild);

                // Si oui, supprimer le lien
                if (allergies?.Any() == true)
                {
                    childFoodEntity.delete(validation.SelectedChild);
                }

                // Recherche si un enfant a eu des childSchedule
                List<ChildSchedule> childScheduleList = childSchedule.findForAKid(validation.SelectedChild);

                // si oui, supprimer le lien
                if (childScheduleList?.Any() == true)
                {
                    foreach (var item in childScheduleList)
                    {
                        childSchedule.delete(item);
                    }
                }

                childEntity.delete(validation.SelectedChild);
                MessageBox.Show($"La fiche de {validation.SelectedChild.Firstname} {validation.SelectedChild.Name} a bien été supprimée");
                ChildrenCombobox.Remove(validation.SelectedChild);
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
