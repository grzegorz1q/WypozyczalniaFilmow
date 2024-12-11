﻿using System;
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
    /// Logika interakcji dla klasy HomePageWindow.xaml
    /// </summary>
    public partial class HomePageWindow : Page
    {
        public HomePageWindow()
        {
            InitializeComponent();
            DataContext = new HomeViewModel();
            if (DataContext is HomeViewModel viewModel)
            {
                viewModel.FilmScrollViewer = FilmScrollViewer;
            }
        }
    }
}
