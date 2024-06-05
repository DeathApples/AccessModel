using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ReactiveUI;
using DynamicData;
using AccessModel.Models;
using AccessModel.Services;

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
        if (UserManager.CreateUser()) {
            LogEvent?.Invoke("Создан новый пользователь");
            UpdateUsers();
        } else {
            LogEvent?.Invoke("Ошибка создания пользователя: что-то пошло не так...");
        }
    }
    
    public void ChangeUser()
    {
        if (UserManager.GetUser(CurrentUser.Id) is not null) {
            if (UserManager.ChangeUser(CurrentUser)) {
                LogEvent?.Invoke("Информация о пользователе была обновлена");
                UpdateUsers();
            } else {
                LogEvent?.Invoke("Ошибка обновления информации о пользователе: что-то пошло не так...");
            }
        } else {
            LogEvent?.Invoke($"Ошибка обновления информации о пользователе: пользователь не выбран или выбранный пользователь не найден");
        }
    }
    
    public ReactiveCommand<Unit, Unit> DeleteUserCommand { get; }
    private async Task DeleteUser()
    {
        var message = $"Вы действительно хотите удалить \n пользователя \"{CurrentUser.Name}\"?";
        var result = await Confirmation(message);
        
        if (result == ConfirmationResult.Yes) {
            if (!CurrentUser.IsAdmin) {
                if (UserManager.DeleteUser(CurrentUser)) {
                    LogEvent?.Invoke("Пользователь был удалён");
                    UpdateUsers();
                } else {
                    LogEvent?.Invoke("Ошибка обновления информации о пользователе: что-то пошло не так...");
                }
            } else {
                LogEvent?.Invoke($"Ошибка удаления пользователя: невозможно удалить учётную запись с правами Администратора Системы");
            }
        }
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