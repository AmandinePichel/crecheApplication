using CrecheApplication.Entity.Implement;
using CrecheApplication.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace CrecheApplication.ViewModel
{
    public class ActivitiesViewModel : INotifyPropertyChanged
    {
        public event Action CommandCompleted;
        public ICommand AddPicture { get; set; }
        public ICommand AddActivity { get; set; }
        public ICommand DeleteActivity { get; set; }
        public ICommand EditActivity { get; set; }
        public ICommand DisplayActivityInfos { get; set; }
        

        ActivityEntity activityEntity = new ActivityEntity();

        public ValidationInput validation { get; set; }
        string pictureName;

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

        private Activity selectedActivity;
        public Activity SelectedActivity
        {
            get { return selectedActivity; }
            set { selectedActivity = value; }
        }


        private ObservableCollection<Activity> activitiesListBox;
        public ObservableCollection<Activity> ActivitiesListBox
        {
            get { return activitiesListBox; }
            set { 
                activitiesListBox = value;
                OnPropertyChanged("ActivitiesListBox");
            }
        }



        public ActivitiesViewModel()
        {
            validation = new ValidationInput();
            picture = new BitmapImage(new Uri("pack://application:,,,/Images/localActivity.png"));

            fillActivities();

            AddPicture = new RelayCommand(ExecuteAddPicture, CanExecuteAddPicture);
            AddActivity = new RelayCommand(ExecuteAddActivity, CanExecuteAddActivity);
            DisplayActivityInfos = new RelayCommand(ExecuteDisplayActivityInfos, CanExecuteDisplayActivityInfos);
            EditActivity = new RelayCommand(ExecuteEditActivity, CanExecuteEditActivity);
            DeleteActivity = new RelayCommand(ExecuteDeleteActivity, CanExecutDeleteActivity);
        }

        #region méthodes Modifier une activité

        private bool CanExecuteDisplayActivityInfos(object arg)
        {
            return true;
        }

        private void ExecuteDisplayActivityInfos(object obj)
        {
            if ((string.IsNullOrEmpty(selectedActivity.Photo)))
            {
                picture = new BitmapImage(new Uri("pack://application:,,,/Images/localActivity.png"));
            }
            else
            {
                picture = LoadImageFile(activityEntity.find(selectedActivity).Photo);
            }

            validation.DescriptionActivityEntry = selectedActivity.Name;


            if (CommandCompleted != null)
                CommandCompleted();
        }

        private bool CanExecuteEditActivity(object arg)
        {
            if (validation.IsValidActivityDescription())
            {
                return true;
            }
            return false;
        }

        private void ExecuteEditActivity(object obj)
        {
            if ((string.IsNullOrEmpty(pictureName)))
            {
                pictureName = activityEntity.find(selectedActivity).Photo;
            }

            Activity newActivity = new Activity(selectedActivity.ActivityID, pictureName, validation.DescriptionActivityEntry);

            activityEntity.update(newActivity);
            MessageBox.Show("L'activité a bien été modifiée");
        }

        #endregion

        #region méthodes Supprimer une activité
        private bool CanExecutDeleteActivity(object arg)
        {
            return true;
        }

        private void ExecuteDeleteActivity(object obj)
        {
            int activityID = selectedActivity.ActivityID;

            MessageBoxResult result = MessageBox.Show($"Etes-vous sûr de vouloir supprimer cette activité?", "Vérification", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
            {
                //Ne fait rien
            }
            else if (result == MessageBoxResult.Yes)
            {
                // Supprime l'activité sélectionnée
                activityEntity.delete(activityID);
                fillActivities();
            }
        }

        #endregion

        #region méthodes Afficher les activités

        private void fillActivities()
        {
            ActivitiesListBox = new ObservableCollection<Activity>();

            foreach (var item in activityEntity.findAll(DateTime.Now.Date))
            {
                picture = LoadImageFile(item.Photo);
                ActivitiesListBox.Add(new Activity() { ActivityID = item.ActivityID, Name = item.Name, Photo = picture.ToString() }) ;
            }
        }

        #endregion

        #region méthode Ajouter une activité
        private bool CanExecuteAddPicture(object arg)
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

        private bool CanExecuteAddActivity(object arg)
        {
            if (validation.IsValidActivityDescription())
            {
                return true;
            }
            return false;
        }

        private void ExecuteAddActivity(object obj)
        {
            Activity newActivity = new Activity(pictureName, validation.DescriptionActivityEntry);
            activityEntity.create(newActivity, DateTime.Now.Date);

            MessageBox.Show("L'activité a bien été ajoutée");
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
