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
                    new Actor { Name= "Morgan", Surname = "Freeman"},
                    new Actor { Name= "Leonardo", Surname = "DiCaprio"},
                    new Actor { Name= "Elliot", Surname = "Page"},
                    new Actor { Name= "Keanu", Surname = "Reeves"},
                    new Actor { Name= "Laurence", Surname = "Fishburne"},
                    new Actor { Name= "Adam", Surname = "Fidusiewicz" },
                    new Actor { Name= "Karolina", Surname = "Sawka" },
                    new Actor { Name= "Marlon", Surname = "Branco" },
                    new Actor { Name= "Al", Surname = "Pacino" },
                    new Actor { Name= "Mark", Surname = "Ruffalo" }
                );

                _context.SaveChanges(); // Zapisujemy zmiany w bazie
            }
            if(!_context.Films.Any())
            {
                var actors = _context.Persons.OfType<Actor>().ToList();
                _context.Films.AddRange(
                    /*new Film { Title = "Skazani na Shawshank", Actors = new List<Actor> { actors[0], actors[1] }, Count = 1 },
                    new Film { Title = "W pustyni i w puszczy", Count = 1 },*/
                    new Film { Title = "Skazani na Shawshank", Director= "Frank Darabont", Actors = new List<Actor> { actors[0], actors[1] },Count = 1, Category = "Dramat", ReleaseDate = new DateOnly(1994, 9, 22), Description = "Film opowiada historię skazanych mężczyzn w amerykańskim więzieniu Shawshank.", Cover = "/Covers/shawshank.jpg" },
                    new Film { Title = "W pustyni i w puszczy", Director = "Gavin Hood", Actors = new List<Actor> { actors[2], actors[3] }, Count = 1, Category = "Przygodowy", ReleaseDate = new DateOnly(2001, 12, 25), Description = "Film opowiada o przygodach dwójki dzieci w Afryce.", Cover = "/Covers/pustynia.jpg" },
                    new Film { Title = "Incepcja", Director = "Christopher Nolan", Actors = new List<Actor> { actors[2], actors[3] }, Count = 3, Category = "Sci-Fi", ReleaseDate = new DateOnly(2010, 7, 16), Description = "Film o mężczyźnie, który potrafi wchodzić w sny innych ludzi i je kontrolować.", Cover = "/Covers/incepcja.jpg" },
                    new Film { Title = "Matrix", Director = "Lilly Wachowski", Actors = new List<Actor> { actors[4], actors[5] }, Count = 2, Category = "Sci-Fi", ReleaseDate = new DateOnly(1999, 3, 31), Description = "Film o człowieku, który odkrywa, że żyje w symulowanej rzeczywistości.", Cover = "/Covers/matrix.jpg" },
                    new Film { Title = "Ojciec chrzestny", Director = "Francis Ford Coppola", Actors = new List<Actor> { actors[6], actors[7] }, Count = 2, Category = "Dramat", ReleaseDate = new DateOnly(1972, 12, 31), Description = "Opowieść o nowojorskiej rodzinie mafijnej. Starzejący się Don Corleone pragnie przekazać władzę swojemu synowi.", Cover = "/Covers/ojciec_chrzestny.jpg" },
                    new Film { Title = "Wyspa tajemnic", Director = "Martin Scorsese", Actors = new List<Actor> { actors[2], actors[8] }, Count = 2, Category = "Thriller", ReleaseDate = new DateOnly(2010, 3, 26), Description = "Szeryf federalny Daniels stara się dowiedzieć, jakim sposobem z zamkniętej celi w pilnie strzeżonym szpitalu dla chorych psychicznie przestępców zniknęła pacjentka.", Cover = "/Covers/wyspa.jpeg" }
                );
                _context.SaveChanges();
            }
        }
    }
}
