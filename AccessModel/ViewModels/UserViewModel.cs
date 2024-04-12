using System.Collections.ObjectModel;
using System.Linq;
using AccessModel.Models;
using AccessModel.Services;
using ReactiveUI;

namespace AccessModel.ViewModels;

public class UserViewModel : ViewModelBase
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

    public UserViewModel()
    {
        _userList = new ObservableCollection<User>(
            UserManager.GetAllUsers()
        );
        
        _currentUser = _userList.FirstOrDefault() ?? new User();
    }
}