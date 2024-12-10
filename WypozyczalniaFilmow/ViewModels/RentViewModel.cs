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
        //private DateTime _rentDate;
        public ObservableCollection<Rent> Rents { get; set; } = default!;
        /*public string SelectedUserFilmsListLabel =>
        SelectedUser != null
            ? $"Lista filmów użytkownika: {SelectedUser.Name} {SelectedUser.Surname}"
            : "Wybierz użytkownika";*/
        public string SelectedUserFilmsListLabel
        {
            get
            {
                if (SelectedUser != null)
                {
                    return $"Lista filmów użytkownika: {SelectedUser.Name} {SelectedUser.Surname}";
                }
                else
                {
                    return "Wybierz użytkownika";
                }
            }
        }
        public IEnumerable<Rent> ClientRentsDetails
        {
            get
            {
                if (SelectedUser == null)
                {
                    // Jeśli SelectedUser jest null, zwróć pustą kolekcję
                    Rents = new ObservableCollection<Rent>(Enumerable.Empty<Rent>());
                    Debug.WriteLine($"Komunikat SelectedUser is null");
                    return Rents;
                }
                else
                {
                    using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
                    {
                        var rentsFromDb = context.Rents
                            .Where(r => r.ClientId == this.SelectedUser.Id)
                            .Include(r => r.Client)
                            .Include(r => r.Film)
                            .ToList();
                        Debug.WriteLine($"Liczba wypożyczeń w bazie: {rentsFromDb.Count}");

                        Rents = new ObservableCollection<Rent>(rentsFromDb);
                        return Rents;
                    }
                }
            }
        }
        

        public ICommand ShowAddUserFormCommand { get; }
        public ICommand RentFilmCommand { get; }
        public RentViewModel()
        {
            ShowAddUserFormCommand = new RelayCommand(ShowUserRegistration);
            RentFilmCommand = new RelayCommand(RentFilm);
            //LoadRents();
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
                    // Jeśli istnieje, wyświetl komunikat
                    MessageBox.Show("Ten użytkownik już wypożyczył ten film.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return; // Zatrzymaj dalsze wykonywanie metody
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
                   // .Where(r => r.ClientId == this.SelectedUser?.Id)
                    .Include(r => r.Client)
                    .Include(r => r.Film)
                    .ToList();
                Debug.WriteLine($"Liczba wypożyczeń w bazie: {rentsFromDb.Count}");

                Rents = new ObservableCollection<Rent>(rentsFromDb);
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
        public Client SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
                OnPropertyChanged(nameof(SelectedUserFilmsListLabel));
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
