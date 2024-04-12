using ReactiveUI;
using System.Reactive;
using AccessModel.Models;
using AccessModel.Services;

namespace AccessModel.ViewModels;

public class AuthenticationViewModel : ViewModelBase
{
    private string? _name;
    public string? Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }
    
    private string? _login;
    public string? Login
    {
        get => _login;
        set => this.RaiseAndSetIfChanged(ref _login, value);
    }
    
    private string? _password;
    public string? Password
    {
        get => _password;
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }
    
    public ReactiveCommand<Unit, Unit> SignInCommand { get; }
    private void SignIn()
    {
    }

    public AuthenticationViewModel()
    {
        SignInCommand = ReactiveCommand.Create(SignIn);
        
        UserManager.CurrentUser =
            UserManager.GetUser("admin") 
            ?? UserManager.CreateUser("Администратор", "admin", "A123!");
    }
}