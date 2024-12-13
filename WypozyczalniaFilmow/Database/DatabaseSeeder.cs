using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WypozyczalniaFilmow.Models;

namespace WypozyczalniaFilmow.Database
{
    public class DatabaseSeeder
    {
        private readonly AppDbContext _context;

        public DatabaseSeeder(AppDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            // Sprawdzamy, czy już mamy jakieś dane w tabeli, żeby nie dodać ich ponownie
            if (!_context.Persons.Any())
            {
                // Dodajemy nowych użytkowników do bazy
                _context.Persons.AddRange(
                    new Client { Name = "Jan", Surname = "Kowalski", Email = "jan@jan.pl", PhoneNumber = 123456789 },
                    new Client { Name = "Kamila", Surname = "Nowak", Email = "kamila@nowak.pl", PhoneNumber = 123321432 },
                    new Actor { Name = "Tim", Surname = "Robbins" },
                    new Actor { Name= "Morgan", Surname = "Freeman"}
                );

                _context.SaveChanges(); // Zapisujemy zmiany w bazie
            }
            if(!_context.Films.Any())
            {
                var actors = _context.Persons.OfType<Actor>().ToList();
                _context.Films.AddRange(
                    /*new Film { Title = "Skazani na Shawshank", Actors = new List<Actor> { actors[0], actors[1] }, Count = 1 },
                    new Film { Title = "W pustyni i w puszczy", Count = 1 },*/
                    new Film { Title = "Skazani na Shawshank", Actors = new List<Actor> { actors[0], actors[1] },Count = 1, Category = "Dramat", ReleaseDate = new DateOnly(1994, 9, 22), Description = "Film opowiada historię skazanych mężczyzn w amerykańskim więzieniu Shawshank."},
                    new Film { Title = "W pustyni i w puszczy", Count = 1, Category = "Przygodowy", ReleaseDate = new DateOnly(2001, 12, 25), Description = "Film opowiada o przygodach dwójki dzieci w Afryce." },
                    new Film { Title = "Incepcja", Actors = new List<Actor> { actors[0] }, Count = 3, Category = "Sci-Fi", ReleaseDate = new DateOnly(2010, 7, 16), Description = "Film o mężczyźnie, który potrafi wchodzić w sny innych ludzi i je kontrolować." },
                    new Film { Title = "Matrix", Actors = new List<Actor> { actors[1] }, Count = 2, Category = "Sci-Fi", ReleaseDate = new DateOnly(1999, 3, 31), Description = "Film o człowieku, który odkrywa, że żyje w symulowanej rzeczywistości." }
                );
                _context.SaveChanges();
            }
        }
    }
}
