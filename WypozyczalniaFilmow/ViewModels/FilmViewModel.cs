using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using WypozyczalniaFilmow.Database;
using WypozyczalniaFilmow.Helpers;
using WypozyczalniaFilmow.Models;
using WypozyczalniaFilmow.Views;

namespace WypozyczalniaFilmow.ViewModels
{
    public class FilmViewModel : ObservableObject
    {
        public ObservableCollection<Film> Films { get; set; } = default!;
        private string _title = string.Empty;
        private string _director = string.Empty;
        private string _category = string.Empty;
        private DateOnly _releasedate = default!;
        private string _description = string.Empty;
        private string _cover = string.Empty;
        private int? _count;
        private string _formTitle = "Dodaj Film";
        private Film _selectedFilm;

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand LoadImageCommand { get; }
        public ICommand DeleteFilmCommand { get; }

        public FilmViewModel()
        {
            LoadFilms();
            SubmitCommand = new RelayCommand(AddFilms);
            CancelCommand = new RelayCommand(ClearForm);
            LoadImageCommand = new RelayCommand(LoadImage);
            DeleteFilmCommand = new RelayCommand(DeleteFilm);
        }

        private void LoadImage()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
            };

            if (dialog.ShowDialog() == true)
            {
                Cover = dialog.FileName;
            }
        }

        private void AddFilms()
        {
            using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
            {
                var newFilm = new Film
                {
                    Title = this.Title,
                    Director = this.Director,
                    Category = this.Category,
                    ReleaseDate = this.ReleaseDate,
                    Description = this.Description,
                    Cover = this.Cover,
                    Count = this.Count
                };

                context.Films.Add(newFilm);
                context.SaveChanges();
                Films.Add(newFilm);
            }
        }
        private void DeleteFilm()
        {
            using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
            {
                // Debugowanie liczby użytkowników
                var filmsFromDb = context.Films
                    .Include(f => f.Actors)
                    .ToList();
                Console.WriteLine($"Liczba użytkowników w bazie: {filmsFromDb.Count}");

                // Przypisanie do ObservableCollection
                filmsFromDb.Remove(SelectedFilm);
                context.SaveChanges();
                Films.Remove(SelectedFilm);
            }
        }
        private void LoadFilms()
        {
            using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
            {
                // Debugowanie liczby użytkowników
                var filmsFromDb = context.Films
                    .Include(f => f.Actors)
                    .ToList();
                Console.WriteLine($"Liczba użytkowników w bazie: {filmsFromDb.Count}");

                // Przypisanie do ObservableCollection
                Films = new ObservableCollection<Film>(filmsFromDb);
            }
        }

        private void ClearForm()
        {
            Title = string.Empty;
            Director = string.Empty;
            Category = string.Empty;
            ReleaseDate = default!;
            Description = string.Empty;
            Cover = string.Empty;
            Count = null;

        }


        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public string Director
        {
            get
            {
                return _director;
            }
            set
            {
                _director = value;
                OnPropertyChanged(nameof(Director));
            }
        }
        public string Category
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
                OnPropertyChanged(nameof(Category));
            }
        }
        public DateOnly ReleaseDate
        {
            get
            {
                return _releasedate;
            }
            set
            {
                _releasedate = value;
                OnPropertyChanged(nameof(ReleaseDate));
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public string Cover
        {
            get
            {
                return _cover;
            }
            set
            {
                _cover = value;
                OnPropertyChanged(nameof(Cover));
            }
        }
        public int? Count
        {
            get
            {
                return _count;
            }
            set
            {
                _count = value;
                OnPropertyChanged(nameof(Count));
            }
        }
        public string FormTitle
        {
            get
            {
                return _formTitle;
            }
            set
            {
                _formTitle = value;
                OnPropertyChanged(nameof(FormTitle));
            }
        }

        public Film SelectedFilm
        {
            get
            {
                return _selectedFilm;
            }
            set
            {
                _selectedFilm = value;
                OnPropertyChanged(nameof(SelectedFilm));
            }
        }

    }
}