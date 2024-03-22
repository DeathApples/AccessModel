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
    
    
    private string _status = string.Empty;

    public string Status
    {
        get => _status;
        set => this.RaiseAndSetIfChanged(ref _status, value);
    }
}