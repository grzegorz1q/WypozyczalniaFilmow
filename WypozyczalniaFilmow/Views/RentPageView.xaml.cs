using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WypozyczalniaFilmow.ViewModels;

namespace WypozyczalniaFilmow.Views
{
    /// <summary>
    /// Logika interakcji dla klasy RentPageView.xaml
    /// </summary>
    public partial class RentPageView : UserControl
    {
        bool hidden = true;
        public RentPageView()
        {
            InitializeComponent();
            var userViewModel = new UserViewModel();
            var filmViewModel = new FilmViewModel();
            DataContext = new RentPageViewModel(userViewModel);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(hidden == true)
            {
                userView.Visibility = Visibility.Hidden;
                hidden = false;
            }
            else
            {
                userView.Visibility = Visibility.Visible; 
                hidden = true;
            }
        }
    }
}
