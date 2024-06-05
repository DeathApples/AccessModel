using System;
using ReactiveUI;
using System.Reactive;
using AccessModel.Services;

namespace AccessModel.ViewModels;

public class AuthenticationViewModel : ViewModelBase
{
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

    private bool? _isError = false;
    public bool? IsError
    {
        get => _isError;
        private set => this.RaiseAndSetIfChanged(ref _isError, value);
    }
    
    public ReactiveCommand<Unit, Unit> SignInCommand { get; }
    private void SignIn()
    {
        var user = UserManager.GetUser(Login);
        if (user is not null)
        {
            if (UserManager.PasswordVerification(user, Password))
            {
                IsError = false;
                Login = string.Empty;
                Password = string.Empty;
                UserManager.CurrentUser = user;
                CloseCommand.Execute().Subscribe();
            }
            else
            {
                IsError = true;
                Password = string.Empty;
            }
        }
        else
        {
            IsError = true;
            Password = string.Empty;
        }
    }
    
    public static ReactiveCommand<Unit, object> CloseCommand { get; }

    public AuthenticationViewModel() {
        SignInCommand = ReactiveCommand.Create(SignIn);

        if (UserManager.GetUser("admin") is null) {
            UserManager.CreateUser("Администратор", "admin", "A123!");
        }
    }
    
    static AuthenticationViewModel() {
        CloseCommand = ReactiveCommand.Create(() => new object());
    }
}