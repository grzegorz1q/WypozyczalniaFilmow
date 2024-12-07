using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WypozyczalniaFilmow.Helpers;
using WypozyczalniaFilmow.Views;

namespace WypozyczalniaFilmow.ViewModels
{
    public class MainViewModel
    {
        public ICommand NavigateToUserPageCommand { get; }
        public ICommand NavigateToFilmPageCommand { get; }
        public ICommand NavigateToRentPageCommand { get; }

        public MainViewModel()
        {
            NavigateToUserPageCommand = new RelayCommand(NavigateToUserPage);
            NavigateToFilmPageCommand = new RelayCommand(NavigateToFilmPage);
            NavigateToRentPageCommand = new RelayCommand(NavigateToRentPage);
        }

        private void NavigateToUserPage()
        {
            var mainWindow = (Application.Current.MainWindow as MainWindow);
            mainWindow?.MainFrame.Navigate(new UsersPageWindow());
        }
        private void NavigateToFilmPage()
        {
            var mainWindow = (Application.Current.MainWindow as MainWindow);
            mainWindow?.MainFrame.Navigate(new FilmPageWindow());
        }
        private void NavigateToRentPage()
        {
            var mainWindow = (Application.Current.MainWindow as MainWindow);
            mainWindow?.MainFrame.Navigate(new RentPageView());
        }
    }
}
