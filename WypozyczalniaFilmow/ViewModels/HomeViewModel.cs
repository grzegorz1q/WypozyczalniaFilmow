using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WypozyczalniaFilmow.Database;
using WypozyczalniaFilmow.Helpers;
using WypozyczalniaFilmow.Models;

namespace WypozyczalniaFilmow.ViewModels
{
    public class HomeViewModel : ObservableObject
    {
        public FilmViewModel FilmViewModel { get; set; } = new FilmViewModel();
        public ObservableCollection<Film> FilmsList { get; set; } = new ObservableCollection<Film> { };
        public ICommand ScrollLeftCommand { get; }
        public ICommand ScrollRightCommand { get; }
        public HomeViewModel()
        {
            ScrollLeftCommand = new RelayCommand(ScrollLeft);
            ScrollRightCommand = new RelayCommand(ScrollRight);
            GetFilm(0,3);
        }
        private void GetFilm(int startIndex, int endIndex)
        {
            using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
            {
                var allFilms = context.Films.ToList();
                var filmsFromDb = allFilms
                    .Skip(startIndex)            
                    .Take(endIndex - startIndex)  
                    .ToList();

                FilmsList.Clear();
                foreach (var film in filmsFromDb)
                {
                    FilmsList.Add(film);
                }
            }
        }
        int startIndex = 0;
        int endIndex = 3;
        private void ScrollLeft()
        {
            if (FilmViewModel.Films.Any())
            {
                startIndex--;
                endIndex--;
                GetFilm(startIndex, endIndex );
                Debug.WriteLine($"w lewo{startIndex},{endIndex}");
            }
        }

        private void ScrollRight()
        {

            if (FilmViewModel.Films.Any())
            {
                startIndex++;
                endIndex++;
                GetFilm(startIndex, endIndex);
                Debug.WriteLine($"w prawo{startIndex},{endIndex}");
            }
        }
    }
}
