using ReactiveUI;
using AccessModel.Models;
using AccessModel.Services;

namespace AccessModel.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly ViewModelBase[] _pages;

    private ViewModelBase _currentPage;
    public ViewModelBase CurrentPage
    {
        get => _currentPage;
        private set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }
    
    private string _textChangePageButton = "ПОЛЬЗОВАТЕЛИ";
    public string TextChangePageButton
    {
        get => _textChangePageButton;
        set => this.RaiseAndSetIfChanged(ref _textChangePageButton, value);
    }
    
    private string _status = "Успешно";
    public string Status
    {
        get => _status;
        set => this.RaiseAndSetIfChanged(ref _status, value);
    }
    
    private User _currentUser;
    public User CurrentUser 
    {
        get => _currentUser; 
        set => this.RaiseAndSetIfChanged(ref _currentUser, value); 
    }

    public bool IsAdmin => CurrentUser is { IsAdmin: true };

    public void ChangePage()
    {
        if (CurrentPage is UserViewModel)
        {
            CurrentPage = _pages[0];
            TextChangePageButton = "ПОЛЬЗОВАТЕЛИ";
        }
        else
        {
            CurrentPage = _pages[1];
            TextChangePageButton = "НАЗАД";
        }
    }
    
    public MainWindowViewModel()
    {
        AccessModelContext context = new();
        
        UserManager.CurrentUser = UserManager.GetUser("admin");
        
        if (UserManager.CurrentUser == null)
        {
            UserManager.CreateUser("Администратор", "admin", "A123!");
            UserManager.CurrentUser = UserManager.GetUser("admin");
        }
        
        _currentUser = UserManager.CurrentUser!;
        
        _pages = new ViewModelBase[] {
            new ResourceViewModel(),
            new UserViewModel()
        };
        
        _currentPage = _pages[0];
    }
}