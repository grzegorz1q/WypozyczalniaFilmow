﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WypozyczalniaFilmow.Database;
using WypozyczalniaFilmow.Helpers;
using WypozyczalniaFilmow.Models;
using WypozyczalniaFilmow.Validators;

namespace WypozyczalniaFilmow.ViewModels
{
    public class UserViewModel : ObservableObject
    {
        public ObservableCollection<Client> Users { get; set; } = default!;
        private string _name = string.Empty!;
        private string _surname = string.Empty!;
        private string _email = string.Empty!;
        private int _phonenumber;
        public UserViewModel()
        {
            LoadUsers();
            SubmitCommand = new RelayCommand(AddUser);
            CancelCommand = new RelayCommand(ClearForm);
            ClearForm();
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        private string ValidateClient()
        {
            var client = new Client
            {
                Name = this.Name,
                Surname = this.Surname,
                Email = this.Email,
                PhoneNumber = this.PhoneNumber
            };

            using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
            {
                var validator = new ClientValidator(context);
                var results = validator.Validate(client);

                if (!results.IsValid)
                {
                    return string.Join("\n", results.Errors.Select(e => e.ErrorMessage));
                }
            }

            return string.Empty;
        }

        private void AddUser()
        {
            var validationMessage = ValidateClient();
            if (!string.IsNullOrEmpty(validationMessage))
            {
                MessageBox.Show(validationMessage, "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
            {
                var newUser = new Client
                {
                    Name = this.Name,
                    Surname = this.Surname,
                    Email = this.Email,
                    PhoneNumber = this.PhoneNumber
                };

                context.Persons.Add(newUser);
                context.SaveChanges();
                Users.Add(newUser);
            }
        }
        private void LoadUsers()
        {
            using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
            {
                var usersFromDb = context.Persons.OfType<Client>().ToList();
                Console.WriteLine($"Liczba użytkowników w bazie: {usersFromDb.Count}");

                Users = new ObservableCollection<Client>(usersFromDb);
            }
        }

        private void ClearForm()
        {
            Name = string.Empty;
            Surname = string.Empty;
            Email = string.Empty;
            PhoneNumber = 0;
        }


        //Gettery i Settery
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public int PhoneNumber
        {
            get
            {
                return _phonenumber;
            }
            set
            {
                _phonenumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }
    }
}
