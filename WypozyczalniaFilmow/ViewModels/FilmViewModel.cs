﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using WypozyczalniaFilmow.Database;
using WypozyczalniaFilmow.Helpers;
using WypozyczalniaFilmow.Models;
using WypozyczalniaFilmow.Views;
using static Azure.Core.HttpHeader;

namespace WypozyczalniaFilmow.ViewModels
{
    public class FilmViewModel : ObservableObject
    {
        public ObservableCollection<Film> Films { get; set; } = default!;
        public ObservableCollection<Actor> NewActors { get; set; } = new ObservableCollection<Actor>();
        public ObservableCollection<Actor> AllActors { get; set; } = new ObservableCollection<Actor>();
        private string _actorName = string.Empty;
        private string _actorSurname = string.Empty;
        private string _title = string.Empty;
        private string _director = string.Empty;
        private string _category = string.Empty;
        private DateOnly _releasedate = default!;
        private string _description = string.Empty;
        private string _cover = string.Empty;
        private int? _count;
        private string _formTitle = "Dodaj Film";
        private Film _selectedFilm = default!;
        private Actor _selectedActor = default!;
        private string _selectedFilmLabel = "Dodaj Film";
        private bool tmpEdit = false;

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand LoadImageCommand { get; }
        public ICommand DeleteFilmCommand { get; }
        public ICommand AddActorCommand { get; }
        public ICommand EditFilmCommand { get; }

        public FilmViewModel()
        {
            LoadFilms();
            LoadActors();
            SubmitCommand = new RelayCommand(SubmitAction);
            CancelCommand = new RelayCommand(ClearForm);
            LoadImageCommand = new RelayCommand(LoadImage);
            DeleteFilmCommand = new RelayCommand(DeleteFilm);
            AddActorCommand = new RelayCommand(AddActorsToFilm);
            EditFilmCommand = new RelayCommand(EditFilm);
        }
        private void SubmitAction()
        {
            if (tmpEdit)
            {
                EditFilm();
            }
            else
            {
                AddFilms();
            }
        }
        private void EditFilm()
        {
            if (tmpEdit == false)
            {
                SelectedFilmLabel = "Edycja Filmu";
                tmpEdit = true;
            }
            else
            {
                using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
                {
                    if (SelectedFilm == null)
                    {
                        MessageBox.Show("Musisz wybrać film z listy", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Wyszukiwanie filmu w bazie danych
                    var selectedFilm = context.Films.FirstOrDefault(f => f.Id == SelectedFilm.Id);
                    if (selectedFilm == null)
                    {
                        MessageBox.Show("Wybrany film nie istnieje w bazie danych!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Aktualizacja danych filmu
                    selectedFilm.Title = this.Title;
                    selectedFilm.Director = this.Director;
                    selectedFilm.Description = this.Description;
                    selectedFilm.Category = this.Category;
                    selectedFilm.ReleaseDate = this.ReleaseDate;
                    selectedFilm.Description = this.Description;
                    selectedFilm.Count = this.Count;



                    context.SaveChanges();
                    var index = Films.IndexOf(SelectedFilm);
                    Films[index] = selectedFilm; 
                    ClearForm();
                    tmpEdit = false;
                    SelectedFilmLabel = "Dodaj Film";
                }
            }
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
        private void LoadActors()
        {
            using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
            {
                // Debugowanie liczby użytkowników
                var actorsFromDb = context.Persons.OfType<Actor>()
                    .ToList();

                // Przypisanie do ObservableCollection
                 AllActors = new ObservableCollection<Actor>(actorsFromDb);
                
            }
        }

        private void AddFilms()
        {
            /* using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
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
                     Actors = NewActors
                 };


                 context.Films.Add(newFilm);
                 context.SaveChanges();
                 Films.Add(newFilm);
                 ClearForm();
                 NewActors.Clear();
             }*/
            using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
            {
                // Lista aktorów do dodania do filmu
                var filmActors = new List<Actor>();

                foreach (var actor in NewActors)
                {
                    // Sprawdź, czy aktor już istnieje w bazie
                    var existingActor = context.Actors
                        .FirstOrDefault(a => a.Name == actor.Name && a.Surname == actor.Surname);

                    if (existingActor != null)
                    {
                        // Dodaj istniejącego aktora
                        filmActors.Add(existingActor);
                    }
                    else
                    {
                        // Dodaj nowego aktora
                        filmActors.Add(actor);
                        context.Actors.Add(actor);
                    }
                }

                // Utwórz nowy film z aktorami
                var newFilm = new Film
                {
                    Title = this.Title,
                    Director = this.Director,
                    Category = this.Category,
                    ReleaseDate = this.ReleaseDate,
                    Description = this.Description,
                    Cover = this.Cover,
                    Count = this.Count,
                    Actors = filmActors
                };

                // Dodaj film do bazy
                context.Films.Add(newFilm);
                context.SaveChanges();

                // Dodaj film do kolekcji
                Films.Add(newFilm);

                // Wyczyść formularz
                ClearForm();
                NewActors.Clear();
            }
        }
        private void DeleteFilm()
        {
            using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
            {
                // Debugowanie liczby użytkowników
                if (SelectedFilm == null)
                {
                    MessageBox.Show("Musisz wybrać użytkownika z listy", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var selectedUser = context.Films.FirstOrDefault(u => u.Id == SelectedFilm.Id);
                if (selectedUser == null)
                {
                    MessageBox.Show("Wybrany użytkownik nie istnieje w bazie danych!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                context.Films.Remove(selectedUser);
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
                    //.Include(f => f.Actors)
                    .ToList();
                Console.WriteLine($"Liczba użytkowników w bazie: {filmsFromDb.Count}");

                // Przypisanie do ObservableCollection
                Films = new ObservableCollection<Film>(filmsFromDb);
            }
        }

        private void AddActorsToFilm() // trzeba poprawic bo komunikaty źle się wyświeitlają(coś z ifami)
        {
            if(ActorName == string.Empty && ActorSurname == string.Empty && SelectedActor == null)
            {
                MessageBox.Show("Musisz wybrać aktora do dodania", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if(ActorName == string.Empty && ActorSurname == string.Empty && SelectedActor != null)
            {
                NewActors.Add(SelectedActor);
                SelectedActor = null;
            }
            else if(ActorName != string.Empty && ActorSurname != string.Empty && SelectedActor == null)
            {
                var newActor = new Actor { Name = ActorName, Surname = ActorSurname };
                NewActors.Add(newActor);

                ActorName = string.Empty;
                ActorSurname = string.Empty;
            }
            else
            {
                MessageBox.Show("Musisz wybrać aktora do dodania.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
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
            NewActors.Clear();

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
        public string ActorName
        {
            get
            {
                return _actorName;
            }
            set
            {
                _actorName = value;
                OnPropertyChanged(nameof(ActorName));
            }
        }public string ActorSurname
        {
            get
            {
                return _actorSurname;
            }
            set
            {
                _actorSurname = value;
                OnPropertyChanged(nameof(ActorSurname));
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

                if (_selectedFilm != null)
                {
                    Title = _selectedFilm.Title;
                    Director = _selectedFilm.Director;
                    Category = _selectedFilm.Category;
                    ReleaseDate = _selectedFilm.ReleaseDate;
                    Description = _selectedFilm.Description;
                    Cover = _selectedFilm.Cover;
                    Count = _selectedFilm.Count;
                }
                else
                {
                    ClearForm();
                }
            }
        }
        public Actor SelectedActor
        {
            get
            {
                return _selectedActor;
            }
            set
            {
                _selectedActor = value;
                OnPropertyChanged(nameof(SelectedActor));
            }
        }
        public string SelectedFilmLabel
        {
            get => _selectedFilmLabel;
            set
            {
                _selectedFilmLabel = value;
                OnPropertyChanged(nameof(SelectedFilmLabel));
            }
        }

    }
}