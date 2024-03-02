using System.Reactive;
using ReactiveUI;

namespace AccessModel.ViewModels;

public class AuthenticationViewModel : ViewModelBase
{
    private string? _name = string.Empty;

    public string? Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }
    
    private string? _login = string.Empty;

    public string? Login
    {
        get => _login;
        set => this.RaiseAndSetIfChanged(ref _login, value);
    }
    
    private string? _password = string.Empty;

    public string? Password
    {
        get => _password;
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }
    
    private string? _passwordConfirmation = string.Empty;

    public string? PasswordConfirmation
    {
        get => _passwordConfirmation;
        set => this.RaiseAndSetIfChanged(ref _passwordConfirmation, value);
    }
    
    public ReactiveCommand<Unit, Unit> SignInCommand { get; }

    private void SignIn()
    {
        
    }
    
    public ReactiveCommand<Unit, Unit> SignUpCommand { get; }

    private void SignUp()
    {
        
    }

    public AuthenticationViewModel()
    {
        SignInCommand = ReactiveCommand.Create(SignIn);
        SignUpCommand = ReactiveCommand.Create(SignUp);
    }
}