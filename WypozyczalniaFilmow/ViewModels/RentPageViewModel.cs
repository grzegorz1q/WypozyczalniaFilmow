using System.Windows.Input;
using WypozyczalniaFilmow.Helpers;
using WypozyczalniaFilmow.ViewModels;

public class RentPageViewModel : ObservableObject
{
    private readonly UserViewModel _userViewModel;
    private readonly FilmViewModel _filmViewModel;

    private bool _isAddingUser;
    public bool IsAddingUser
    {
        get => _isAddingUser;
        set
        {
            _isAddingUser = value;
            OnPropertyChanged(nameof(IsAddingUser));
        }
    }

    private bool _isAddingMovie;
    public bool IsAddingMovie
    {
        get => _isAddingMovie;
        set
        {
            _isAddingMovie = value;
            OnPropertyChanged(nameof(IsAddingMovie));
        }
    }

    // Komendy
    public ICommand AddUserCommand { get; set; }
    public ICommand AddMovieCommand { get; set; }

    public RentPageViewModel(UserViewModel userViewModel, FilmViewModel movieViewModel)
    {
        _userViewModel = userViewModel;
        _filmViewModel = movieViewModel;

        // Inicjalizacja komend, które będą wykonywać odpowiednie akcje
        AddUserCommand = new RelayCommand(AddUser);
        AddMovieCommand = new RelayCommand(AddMovie);
    }

    private void AddUser()
    {
        _userViewModel.AddUser();  // Wywołanie metody AddUser w UserViewModel
        IsAddingUser = true;  // Zmieniamy stan, aby wyświetlić widok rejestracji użytkownika
    }

    private void AddMovie()
    {
        _filmViewModel.AddFilms();  // Wywołanie metody AddMovie w MovieViewModel
        IsAddingMovie = true;  // Zmieniamy stan, aby wyświetlić widok rejestracji filmu
    }
}
