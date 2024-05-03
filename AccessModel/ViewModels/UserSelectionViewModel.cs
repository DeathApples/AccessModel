using System.Collections.ObjectModel;
using System.Linq;
using AccessModel.Models;
using AccessModel.Services;
using ReactiveUI;

namespace AccessModel.ViewModels;

public class UserSelectionViewModel : ViewModelBase
{
    private ObservableCollection<User> _userList;
    public ObservableCollection<User> UserList
    {
        get => _userList;
        set => this.RaiseAndSetIfChanged(ref _userList , value);
    }
    
    private User _currentUser;
    public User CurrentUser
    {
        get => _currentUser;
        set => this.RaiseAndSetIfChanged(ref _currentUser, value);
    }
    
    private string _message;
    public string Message
    {
        get => _message;
        set => this.RaiseAndSetIfChanged(ref _message, value);
    }
    
    public ReactiveCommand<string, object?> CloseCommand { get; }
    private object? Close(string result)
    {
        return result switch {
            "yes" => CurrentUser,
            "cancel" => null,
            _ => null
        };
    }

    public UserSelectionViewModel()
    {
        CloseCommand = ReactiveCommand.Create<string, object?>(Close);
        _message = "Выберите пользователя, которому вы хотите выдать права";
        
        _userList = new ObservableCollection<User>(
            UserManager.GetAllUsers()
        );
        
        _currentUser = _userList.FirstOrDefault() ?? new User();
    }
}