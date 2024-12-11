using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WypozyczalniaFilmow.Helpers;

namespace WypozyczalniaFilmow.ViewModels
{
    class RentViewModel : ObservableObject
    {
        public UserViewModel UserViewModel { get; set; } = new UserViewModel();
        public FilmViewModel FilmViewModel { get; set; } = new FilmViewModel();
        private Visibility _addUserVisibility = Visibility.Collapsed;
        private string _contentText = "Pokaż pole wprowadzania użytkownika";
        public ICommand ShowAddUserFormCommand { get; }

        public RentViewModel()
        {
            ShowAddUserFormCommand = new RelayCommand(ShowUserRegistration);
        }
        private void ShowUserRegistration()
        {
            if(AddUserVisibility != Visibility.Collapsed)
            {
                AddUserVisibility = Visibility.Collapsed;
                ContentText = "Pokaż pole wprowadzania użytkownika";
            }
            else 
            { 
                AddUserVisibility = Visibility.Visible;
                ContentText = "Ukryj pole wprowadzania użytkownika";
            }
        }


        //Gettery i Settery
        public string ContentText
        {
            get => _contentText;
            set
            {
                _contentText = value;
                OnPropertyChanged(nameof(ContentText));
            }
        }

        public Visibility AddUserVisibility
        {
            get => _addUserVisibility;
            set
            {
                _addUserVisibility = value;
                OnPropertyChanged(nameof(AddUserVisibility));
            }
        }

    }
}
