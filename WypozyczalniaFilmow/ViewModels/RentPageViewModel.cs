using System.Diagnostics;
using System.Windows.Input;
using WypozyczalniaFilmow.Helpers;
using WypozyczalniaFilmow.ViewModels;

public class RentPageViewModel : ObservableObject
{
    private UserViewModel _userViewModel;
    private FilmViewModel _filmViewModel;
    public UserViewModel UserViewModel
    {
        get => _userViewModel;
        set
        {
            _userViewModel = value;
            OnPropertyChanged(nameof(UserViewModel));
        }
    }

    public FilmViewModel FilmViewModel
    {
        get => _filmViewModel;
        set
        {
            _filmViewModel = value;
            OnPropertyChanged(nameof(FilmViewModel));
        }
    }

    private bool _isAddingUser;
    public bool IsAddingUser
    {
        get => _isAddingUser;
        set
        {
            Debug.WriteLine($"Changing IsAddingUser in RentPageViewModel from {_isAddingUser} to {value}");
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
            Debug.WriteLine($"Changing IsAddingMovie in RentPageViewModel from {_isAddingMovie} to {value}");
            _isAddingMovie = value;
            OnPropertyChanged(nameof(IsAddingMovie));
        }
    }

    public RentPageViewModel()
    {
        UserViewModel = new UserViewModel();
        FilmViewModel = new FilmViewModel();
    }

/*    public RentPageViewModel(UserViewModel userViewModel, FilmViewModel filmViewModel)
    {
        UserViewModel = userViewModel;
        FilmViewModel = filmViewModel;

        Debug.WriteLine("RentPageViewModel initialized with UserViewModel and FilmViewModel");
    }*/
}
