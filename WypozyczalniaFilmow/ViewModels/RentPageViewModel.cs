using System.Windows.Input;
using WypozyczalniaFilmow.Helpers;
using WypozyczalniaFilmow.ViewModels;

public class RentPageViewModel : ObservableObject
{
    public UserViewModel UserViewModel { get; set; }
    public FilmViewModel FilmViewModel { get; set; }


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

    public ICommand AddUserCommand { get; set; }
    public ICommand AddMovieCommand { get; set; }


/*    public RentPageViewModel(UserViewModel userViewModel, FilmViewModel filmViewModel)
    {

        UserViewModel = userViewModel;
        FilmViewModel = filmViewModel;

        AddUserCommand = new RelayCommand(() => ToggleAddUser());
        AddMovieCommand = new RelayCommand(() => ToggleAddMovie());
    }
*/
    public RentPageViewModel()
    {
        UserViewModel = new UserViewModel();
        FilmViewModel = new FilmViewModel();

/*        AddUserCommand = new RelayCommand(() => ToggleAddUser());
        AddMovieCommand = new RelayCommand(() => ToggleAddMovie());*/
    }

    private void ToggleAddUser()
    {
        IsAddingUser = !IsAddingUser;
    }

    private void ToggleAddMovie()
    {
        IsAddingMovie = !IsAddingMovie;
    }
}
