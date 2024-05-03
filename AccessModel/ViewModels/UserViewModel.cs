using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AccessModel.Models;
using AccessModel.Services;
using DynamicData;
using ReactiveUI;

namespace AccessModel.ViewModels;

public class UserViewModel : ViewModelBase
{
    public static event Action<string>? LogEvent;
    
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
    
    public ICommand DeleteUserCommand { get; }
    private async Task DeleteUser()
    {
        /*var message = $"Вы действительно хотите удалить \n пользователя \"{CurrentUser.Name}\"?";
        var result = await Confirmation(message);
        
        if (result == ConfirmationResult.Yes)
        {*/
            UserManager.DeleteUser(CurrentUser);
            UpdateUsers();
        //}
    }

    private void UpdateUsers()
    {
        UserList.Clear();
        UserList.AddRange(UserManager.GetAllUsers());
        CurrentUser = UserList.FirstOrDefault() ?? new User();
    }
    
    private async Task<ConfirmationResult> Confirmation(string message)
    {
        var confirmation = new ConfirmationViewModel {
            Message = message
        };
        
        return await ConfirmationDialog.Handle(confirmation);
    }
    
    public Interaction<ConfirmationViewModel, ConfirmationResult> ConfirmationDialog { get; }

    public UserViewModel()
    {
        ConfirmationDialog = new Interaction<ConfirmationViewModel, ConfirmationResult>();
        DeleteUserCommand = ReactiveCommand.CreateFromTask(DeleteUser);
        
        _userList = new ObservableCollection<User>(
            UserManager.GetAllUsers()
        );
        
        _currentUser = _userList.FirstOrDefault() ?? new User();
    }
}