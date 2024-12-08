using System.Diagnostics;
using System.Windows.Input;
using WypozyczalniaFilmow.Helpers;
using WypozyczalniaFilmow.ViewModels;

public class RentPageViewModel : ObservableObject
{
    private UserViewModel _userViewModel;
    private FilmViewModel _filmViewModel;
    public ICommand AddUserCheck { get; set; }
    public ICommand AddFilmCheck { get; set; }

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
            Debug.WriteLine($"Changing IsAddingUser from {_isAddingUser} to {value}");
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
            Debug.WriteLine($"Changing IsAddingMovie from {_isAddingMovie} to {value}");
            _isAddingMovie = value;
            OnPropertyChanged(nameof(IsAddingMovie));
        }
    }
    public RentPageViewModel(UserViewModel userViewModel, FilmViewModel filmViewModel)
    {
        UserViewModel = userViewModel;
        FilmViewModel = filmViewModel;
    }

/*    public RentPageViewModel()
    {
        UserViewModel = new UserViewModel();
        FilmViewModel = new FilmViewModel();
    }*/
}
