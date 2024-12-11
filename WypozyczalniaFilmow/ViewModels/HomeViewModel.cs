using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WypozyczalniaFilmow.Helpers;
using WypozyczalniaFilmow.Models;

namespace WypozyczalniaFilmow.ViewModels
{
    public class HomeViewModel
    {
        public FilmViewModel FilmViewModel { get; set; } = new FilmViewModel();
        public ICommand ScrollLeftCommand { get; }
        public ICommand ScrollRightCommand { get; }
        // Referencja do ScrollViewer
        public ScrollViewer? FilmScrollViewer { get; set; }
        public HomeViewModel()
        {
            ScrollLeftCommand = new RelayCommand(ScrollLeft);
            ScrollRightCommand = new RelayCommand(ScrollRight);
        }
        private void ScrollLeft()
        {
            if (FilmScrollViewer != null)
            {
                // Przesuń w lewo o 300 pikseli
                FilmScrollViewer.ScrollToHorizontalOffset(FilmScrollViewer.HorizontalOffset - 300);
                Debug.WriteLine("w lewo");
            }
        }

        private void ScrollRight()
        {
            if (FilmScrollViewer != null)
            {
                // Przesuń w prawo o 300 pikseli
                FilmScrollViewer.ScrollToHorizontalOffset(FilmScrollViewer.HorizontalOffset + 300);
                Debug.WriteLine("w prawo");
            }
        }

    }
}
