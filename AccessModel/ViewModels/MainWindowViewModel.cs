using System;
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

    public bool IsAdmin => CurrentUser.IsAdmin;

    public void ChangePage()
    {
        CurrentPage = _currentPage is UserViewModel ? _pages[0] : _pages[1];
    }
    
    public MainWindowViewModel()
    {
        AccessModelContext context = new();
        _currentUser = UserManager.CurrentUser ?? new User { Name = "Администратор" };
        
        _pages = new ViewModelBase[] {
            new ResourceViewModel(),
            new UserViewModel()
        };
        _currentPage = _pages[0];
    }
}