using System.Collections.ObjectModel;
using System.Linq;
using AccessModel.Models;
using AccessModel.Services;
using DynamicData;
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
    
    public void CreateUser()
    {
        // ToDo: Добавить проверки и реализовать систему оповещений
        UserManager.CreateUser();
        UpdateUsers();
    }
    
    public void ChangeUser()
    {
        // ToDo: Добавить проверки и реализовать систему оповещений
        UserManager.ChangeUser(CurrentUser);
        UpdateUsers();
    }
    
    public void DeleteUser()
    {
        // ToDo: Добавить проверки и реализовать систему оповещений
        UserManager.DeleteUser(CurrentUser);
        UpdateUsers();
    }

    private void UpdateUsers()
    {
        UserList.Clear();
        UserList.AddRange(UserManager.GetAllUsers());
        CurrentUser = UserList.FirstOrDefault() ?? new User();
    }

    public UserViewModel()
    {
        _userList = new ObservableCollection<User>(
            UserManager.GetAllUsers()
        );
        
        _currentUser = _userList.FirstOrDefault() ?? new User();
    }
}