using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using WypozyczalniaFilmow.Database;
using WypozyczalniaFilmow.Helpers;
using WypozyczalniaFilmow.Models;

namespace WypozyczalniaFilmow.ViewModels
{
    public class FilmViewModel : ObservableObject
    {
        public ObservableCollection<Film> Films { get; set; }
        public Film SelectedFilm { get; set; }
        public FilmViewModel()
        {
            LoadFilms();
            SubmitFilmCommand = new RelayCommand(AddFilms);
            //CancelFilmCommand = new RelayCommand();
        }

        private string _title;
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

        private string _director;
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

        private string _category;
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


        private DateTime _releasedate;
        public DateTime ReleaseDate
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

        private string _description;
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
        private string _cover;
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

        private int _count;
        public int Count
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

        private bool _isAddingMovie;
        public bool IsAddingMovie
        {
            get => _isAddingMovie;
            set
            {
                Debug.WriteLine($"Changing IsAddingMovie from {_isAddingMovie} to {value}");
                _isAddingMovie = value;
                OnPropertyChanged(nameof(IsAddingMovie));
            }
        }

        public ICommand SubmitFilmCommand { get; }
        public ICommand CancelFilmCommand { get; }

        public void AddFilms()
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
                    Count = this.Count,
                };

                context.Films.Add(newFilm);
                context.SaveChanges();
                Films.Add(newFilm);
            }
        }
        private void LoadFilms()
        {
            using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
            {
                var filmsFromDb = context.Films.ToList();
                Console.WriteLine($"Liczba użytkowników w bazie: {filmsFromDb.Count}");
                Films = new ObservableCollection<Film>(filmsFromDb);
            }
        }

    }
}

