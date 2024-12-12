using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WypozyczalniaFilmow.Database;
using WypozyczalniaFilmow.Helpers;
using WypozyczalniaFilmow.Models;
using WypozyczalniaFilmow.Views;

namespace WypozyczalniaFilmow.ViewModels
{
    public class HomeViewModel : ObservableObject
    {
        public FilmViewModel FilmViewModel { get; set; } = new FilmViewModel();
        public ObservableCollection<Film> FilmsList { get; set; } = new ObservableCollection<Film>();
        public ICommand ScrollLeftCommand { get; }
        public ICommand ScrollRightCommand { get; }
        public ICommand NavigateToFilmDetailsCommand { get; }

        public HomeViewModel()
        {
            ScrollLeftCommand = new RelayCommand(ScrollLeft);
            ScrollRightCommand = new RelayCommand(ScrollRight);
            NavigateToFilmDetailsCommand = new RelayCommand(NavigateToFilmDetailsPage);
            GetFilm(0, 3);
        }
        private void NavigateToFilmDetailsPage(object parameter)
        {
            var selectedFilm = parameter as Film;
            if (selectedFilm != null)
            {
                var filmDetailsPage = new FilmDetailsViewModel
                {
                     Film = selectedFilm
                };
                var mainWindow = (Application.Current.MainWindow as MainWindow);
                mainWindow?.MainFrame.Navigate(new FilmDetailsPageWindow { DataContext = filmDetailsPage });
            }
        }

        int lastIndex = 0;
        private void GetFilm(int startIndex, int endIndex)
        {
            using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
            {
                var allFilms = context.Films
                    .Include(f => f.Actors)
                    .ToList();
                var threeFilms = allFilms
                    .Skip(startIndex)
                    .Take(endIndex - startIndex)
                    .ToList();

                lastIndex = allFilms.Count - 1;

                FilmsList.Clear();
                foreach (var film in threeFilms)
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

            if (FilmViewModel.Films.Any() && endIndex<=lastIndex)
            {
                startIndex++;
                endIndex++;
                GetFilm(startIndex, endIndex);
                Debug.WriteLine($"w prawo{startIndex},{endIndex}");
            }
        }
    }
}
