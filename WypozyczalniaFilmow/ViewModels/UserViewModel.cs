using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WypozyczalniaFilmow.Database;
using WypozyczalniaFilmow.Helpers;
using WypozyczalniaFilmow.Models;

namespace WypozyczalniaFilmow.ViewModels
{
    public class UserViewModel : ObservableObject
    {
        public ObservableCollection<Client> Users { get; set; } = default!;
        public Client SelectedUser { get; set; }
        public UserViewModel()
        {
            LoadUsers();
        }

        private void LoadUsers()
        {
            using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
            {
                // Debugowanie liczby użytkowników
                var usersFromDb = context.Persons.OfType<Client>().ToList();
                Console.WriteLine($"Liczba użytkowników w bazie: {usersFromDb.Count}");

                // Przypisanie do ObservableCollection
                Users = new ObservableCollection<Client>(usersFromDb);
            }
        }

    }
}
