using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WypozyczalniaFilmow.Helpers;

namespace WypozyczalniaFilmow.ViewModels
{
    class RentViewModel : ObservableObject
    {
        public UserViewModel UserViewModel { get; set; }
        public FilmViewModel FilmViewModel { get; set; }


        public RentViewModel()
        {
            UserViewModel = new UserViewModel();
            FilmViewModel = new FilmViewModel();
        }


    }
}
