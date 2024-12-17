using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
        private int? _phonenumber;
        private Client _selectedUser = default!;
        private string _selectedUserLabel = "Dodaj Użytkownika";
        private bool tmpEdit = false;

        public UserViewModel()
        {
            LoadUsers();
            SubmitCommand = new RelayCommand(SubmitAction);
            CancelCommand = new RelayCommand(ClearForm);
            DeleteUserCommand = new RelayCommand(DeleteUser);
            EditCommand = new RelayCommand(EditUser);
            ClearForm();
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand EditCommand { get; }

        private void SubmitAction()
        {
            /*if (tmpEdit)
            {
                EditUser(); 
            }
            else
            {
                AddUser(); 
            }*/
            AddOrUpdateUser();
        }

        private void EditUser()
        {
            if (tmpEdit == false)
            {
                if (SelectedUser == null)
                {
                    MessageBox.Show("Musisz wybrać użytkownika z listy", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                SelectedUserLabel = "Edycja Użytkownika";
                tmpEdit = true;
                Name = SelectedUser.Name;
                Surname = SelectedUser.Surname;
                Email = SelectedUser.Email;
                PhoneNumber = SelectedUser.PhoneNumber;
            }
            else
            {
                /*using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
                {
                    if (SelectedUser == null)
                    {
                        MessageBox.Show("Musisz wybrać użytkownika z listy", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    var userToUpdate = context.Clients.FirstOrDefault(u => u.Id == SelectedUser.Id);
                    if (userToUpdate == null)
                    {
                        MessageBox.Show("Wybrany użytkownik nie istnieje w bazie danych!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    *//*                var validationMessage = ValidateClient();
                                    if (!string.IsNullOrEmpty(validationMessage))
                                    {
                                        MessageBox.Show(validationMessage, "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                                        return;
                                    }*//*

                    userToUpdate.Name = Name;
                    userToUpdate.Surname = Surname;
                    userToUpdate.Email = Email;
                    userToUpdate.PhoneNumber = PhoneNumber;
                    context.SaveChanges();

                    var index = Users.IndexOf(SelectedUser);
                    if (index >= 0)
                    {
                        Users[index] = userToUpdate;
                    }

                    MessageBox.Show("Dane użytkownika zostały zaktualizowane", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);*/
                tmpEdit = false;
                SelectedUserLabel = "Dodaj Użytkownika";
                ClearForm();
                //}
            }

        }
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
        private void DeleteUser()
        {
            using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
            {
                if (SelectedUser == null)
                {
                    MessageBox.Show("Musisz wybrać użytkownika z listy", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var selectedUser = context.Persons.FirstOrDefault(u => u.Id == SelectedUser.Id);
                if (selectedUser == null)
                {
                    MessageBox.Show("Wybrany użytkownik nie istnieje w bazie danych!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                context.Persons.Remove(selectedUser);
                context.SaveChanges();
                Users.Remove(SelectedUser);
            }
        }
        private void AddOrUpdateUser()
        {


            using (var context = new DesignTimeDbContextFactory().CreateDbContext(null))
            {
                if (tmpEdit == false)
                {
                    var validationMessage = ValidateClient();
                    if (!string.IsNullOrEmpty(validationMessage))
                    {
                        MessageBox.Show(validationMessage, "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
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
                else
                {
                    var userToUpdate = context.Persons.OfType<Client>().FirstOrDefault(u => u.Id == SelectedUser.Id);
                    userToUpdate.Name = Name;
                    userToUpdate.Surname = Surname;
                    userToUpdate.Email = Email;
                    userToUpdate.PhoneNumber = PhoneNumber;

                    context.Persons.Update(userToUpdate);
                    context.SaveChanges();

                    var index = Users.IndexOf(SelectedUser);
                    Users[index] = userToUpdate;
                    tmpEdit = false;
                    MessageBox.Show("Dane użytkownika zostały zaktualizowane", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
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
            PhoneNumber = null;
            SelectedUserLabel = "Dodaj użytkownika";
            tmpEdit = false;
        }


        //Gettery i Settery
        /*        public Client SelectedUser
                {
                    get => _selectedUser;
                    set
                    {
                        _selectedUser = value;
                        OnPropertyChanged(nameof(SelectedUser));
                    }
                }*/

        public Client SelectedUser
        {
            get
            {
                return _selectedUser;
            }
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));

                /*if (_selectedUser != null)
                {
                    Name = _selectedUser.Name;
                    Surname = _selectedUser.Surname;
                    Email = _selectedUser.Email;
                    PhoneNumber = _selectedUser.PhoneNumber;
                }
                else
                {
                    ClearForm();
                }*/
            }
        }
        public string SelectedUserLabel
        {
            get
            {
                return _selectedUserLabel;
            }
            set
            {
                _selectedUserLabel = value;
                OnPropertyChanged(nameof(SelectedUserLabel));
            }
        }
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
        public int? PhoneNumber
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
