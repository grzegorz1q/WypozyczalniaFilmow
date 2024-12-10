using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WypozyczalniaFilmow.Database;
using WypozyczalniaFilmow.Helpers;
using WypozyczalniaFilmow.Models;

namespace WypozyczalniaFilmow.ViewModels
{
    class RentViewModel : ObservableObject
    {
        public UserViewModel UserViewModel { get; set; } = new UserViewModel();
        public FilmViewModel FilmViewModel { get; set; } = new FilmViewModel();
        private Visibility _addUserVisibility = Visibility.Collapsed;
        private string _contentText = "Pokaż pole wprowadzania użytkownika";
        private Client _selectedUser = default!;
        private Film _selectedFilm = default!;
        private string _selectedUserFilmsListLabel = "Wybierz użytkownika";
        public ObservableCollection<Rent> Rents { get; set; } = new ObservableCollection<Rent>();
        public ICommand ShowAddUserFormCommand { get; }
        public ICommand RentFilmCommand { get; }
        public RentViewModel()
        {
            ShowAddUserFormCommand = new RelayCommand(ShowUserRegistration);
            RentFilmCommand = new RelayCommand(RentFilm);
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
        private void RentFilm()
        {
            using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
            {
                var existingRent = context.Rents
                    .FirstOrDefault(r => r.ClientId == this.SelectedUser.Id && r.FilmId == this.SelectedFilm.Id);

                if (existingRent != null)
                {
                    MessageBox.Show("Ten użytkownik już wypożyczył ten film.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var newRent = new Rent
                {
                    ClientId = this.SelectedUser.Id,
                    FilmId = this.SelectedFilm.Id
                };

                context.Rents.Add(newRent);
                context.SaveChanges();
                newRent.Client = this.SelectedUser;
                newRent.Film = this.SelectedFilm;
                Rents.Add(newRent);
            
                Debug.WriteLine($"Added rent: Client={newRent.Client?.Name}, Film={newRent.Film?.Title}");
            }
        }

        private void LoadRents()
        {
            using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
            {
                var rentsFromDb = context.Rents
                    .Where(r => r.ClientId == this.SelectedUser.Id)
                    .Include(r => r.Client)
                    .Include(r => r.Film)
                    .ToList();
                Debug.WriteLine($"Liczba wypożyczeń w bazie: {rentsFromDb.Count}");

                Rents.Clear();
                foreach (var rent in rentsFromDb)
                {
                    Rents.Add(rent);
                }
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
        public string SelectedUserFilmsListLabel
        {
            get => _selectedUserFilmsListLabel;
            set
            {
                _selectedUserFilmsListLabel = value;
                OnPropertyChanged(nameof(SelectedUserFilmsListLabel));
            }
        }
        public Client SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
                SelectedUserFilmsListLabel = $"Lista filmów użytkownika: {SelectedUser.Name} {SelectedUser.Surname}";
                if (_selectedUser != null)
                {
                    LoadRents();
                }
            }
        }
        public Film SelectedFilm
        {
            get => _selectedFilm;
            set
            {
                _selectedFilm = value;
                OnPropertyChanged(nameof(SelectedFilm));
            }
        }
    }
}
