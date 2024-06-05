using System;
using System.Reactive;
using ReactiveUI;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
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
    
    private string _status = "Успешно выполнен вход";
    public string Status
    {
        get => _status;
        set => this.RaiseAndSetIfChanged(ref _status, value);
    }
    
    private User? _currentUser;
    public User? CurrentUser 
    {
        get => _currentUser; 
        set => this.RaiseAndSetIfChanged(ref _currentUser, value); 
    }

    public bool IsAdmin => CurrentUser is { IsAdmin: true };

    public void ChangePage()
    {
        if (CurrentPage is UserViewModel) {
            CurrentPage = _pages[0];
            TextChangePageButton = "ПОЛЬЗОВАТЕЛИ";
        } else {
            CurrentPage = _pages[1];
            TextChangePageButton = "НАЗАД";
        }
    }

    public ICommand SignOutCommand { get; }
    private async Task SignOut()
    {
        var message = $"Вы действительно хотите выполнить выход из под учётной записи \"{CurrentUser?.Name}\"?";
        var result = await Confirmation(message);
        
        if (result == ConfirmationResult.Yes) {
            CloseCommand.Execute().Subscribe();
        }
    }

    private async Task<ConfirmationResult> Confirmation(string message)
    {
        var confirmation = new ConfirmationViewModel {
            Message = message
        };
        
        return await ShowDialog.Handle(confirmation);
    }

    private void LogHandler(string message)
    {
        Status = message;
    }
    
    public Interaction<ConfirmationViewModel, ConfirmationResult> ShowDialog { get; }
    public static ReactiveCommand<Unit, object> CloseCommand { get; }
    
    public MainWindowViewModel()
    {
        ShowDialog = new Interaction<ConfirmationViewModel, ConfirmationResult>();
        SignOutCommand = ReactiveCommand.CreateFromTask(SignOut);
        
        ResourceViewModel.LogEvent += LogHandler;
        UserViewModel.LogEvent += LogHandler;

        _currentUser = UserManager.CurrentUser;
        
        _pages = new ViewModelBase[] {
            new ResourceViewModel(),
            new UserViewModel()
        };
        
        _currentPage = _pages[0];
    }

    static MainWindowViewModel()
    {
        CloseCommand = ReactiveCommand.Create(() => new object());
    }
}