using System;
using ReactiveUI;
using AccessModel.Models;
using AccessModel.Services;

namespace AccessModel.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private User _user = UserManager.CurrentUser ?? new User { Name = "Администратор" };
    public User User 
    {
        get => _user; 
        set => this.RaiseAndSetIfChanged(ref _user, value); 
    }
    
    private ViewModelBase _resourceViewModel = new ResourceViewModel();
    public ViewModelBase ResourceViewModel
    {
        get => _resourceViewModel;
        private set => this.RaiseAndSetIfChanged(ref _resourceViewModel, value);
    }
    
    private ViewModelBase _userViewModel = new UserViewModel();
    public ViewModelBase UserViewModel
    {
        get => _userViewModel;
        private set => this.RaiseAndSetIfChanged(ref _userViewModel, value);
    }
    
    private string _status = "Успешно";
    public string Status
    {
        get => _status;
        set => this.RaiseAndSetIfChanged(ref _status, value);
    }

    public bool IsAdmin => User.Id.Equals(0);
    
    public MainWindowViewModel()
    {
        AccessModelContext context = new();
    }
}