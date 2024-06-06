using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using AccessModel.Models;
using AccessModel.Services;
using DynamicData;

namespace AccessModel.ViewModels;

public class ResourceViewModel : ViewModelBase
{
    public static event Action<string>? LogEvent;
    private static bool IsAdmin => UserManager.CurrentUser?.IsAdmin ?? false;
    
    private ObservableCollection<AccessControlEntry> _resourceList;
    public ObservableCollection<AccessControlEntry> ResourceList
    {
        get => _resourceList;
        set => this.RaiseAndSetIfChanged(ref _resourceList, value);
    }
    
    private AccessControlEntry? _currentResource;
    public AccessControlEntry? CurrentResource
    {
        get => _currentResource;
        set
        {
            UserList.Clear();
            UserList.AddRange(AccessControlEntryManager.GetEntries(value?.Resource ?? new Resource()));
            CurrentUser = UserList.FirstOrDefault(entry => entry.User?.Id == UserManager.CurrentUser?.Id) ?? new AccessControlEntry();
            
            if (value is null || !value.IsRead) ResourceContent = string.Empty;
            else ResourceContent = value.Resource?.Content ?? string.Empty;
            
            this.RaiseAndSetIfChanged(ref _currentResource, value);
        }
    }

    private ObservableCollection<AccessControlEntry> _userList;
    public ObservableCollection<AccessControlEntry> UserList
    {
        get => _userList;
        set => this.RaiseAndSetIfChanged(ref _userList, value);
    }
    
    private AccessControlEntry _currentUser;
    public AccessControlEntry CurrentUser
    {
        get => _currentUser;
        set => this.RaiseAndSetIfChanged(ref _currentUser, value);
    }

    private string _resourceContent;
    public string ResourceContent
    {
        get => _resourceContent;
        set => this.RaiseAndSetIfChanged(ref _resourceContent, value);
    }

    public void CreateResource()
    {
        LogEvent?.Invoke("Создан новый документ");
        ResourceManager.CreateObject();
        UpdateResources();
    }

    public void ModifyEntry()
    {
        var oldEntry = AccessControlEntryManager.GetEntry(CurrentUser.Id);
        if (CurrentResource is null || oldEntry is null) return;
        
        if ((oldEntry.IsRead != CurrentUser.IsRead || oldEntry.IsWrite != CurrentUser.IsWrite) && !CurrentResource.IsTakeGrant && !IsAdmin) {
            LogEvent?.Invoke("Ошибка изменения записи контроля доступа: недостаточно прав");
            return;
        }
        
        if (oldEntry.IsTakeGrant != CurrentUser.IsTakeGrant && UserManager.CurrentUser?.Id != CurrentResource?.Resource?.Owner?.Id && !IsAdmin) {
            LogEvent?.Invoke("Ошибка изменения записи контроля доступа: недостаточно прав");
            return;
        }
        
        AccessControlEntryManager.ModifyEntry(CurrentUser);
        
        if (CurrentUser is { IsRead: false, IsWrite: false, IsTakeGrant: false }) {
            LogEvent?.Invoke("Запись контроля доступа успешно удалена");
            AccessControlEntryManager.DeleteEntry(CurrentUser);
        } else {
            LogEvent?.Invoke("Запись контроля доступа успешно изменена");
            ResourceManager.ModifyObject(CurrentUser.Resource);
        }
        
        UpdateResources();
        ChangeEditMode();
    }
    
    public void ModifyResource()
    {
        if (CurrentResource is { IsRead: false, IsWrite: true, Resource: not null }) 
            CurrentResource.Resource.Content += $"\n{ResourceContent}";
        
        if (CurrentResource is { IsRead: true, IsWrite: true, Resource: not null }) 
            CurrentResource.Resource.Content = ResourceContent;

        var oldResource = ResourceManager.GetObject(CurrentUser.Resource?.Id ?? 0);
        if (oldResource?.Name == CurrentResource?.Resource?.Name &&
            oldResource?.Content == CurrentResource?.Resource?.Content) return;
        
        ResourceManager.ModifyObject(CurrentResource?.Resource!);
        LogEvent?.Invoke("Документ успешно изменён");
    }
    
    public ReactiveCommand<Unit, Unit> DeleteResourceCommand { get; }
    private async Task DeleteResource()
    {
        if (CurrentResource?.Resource?.Owner?.Id != UserManager.CurrentUser?.Id) {
            LogEvent?.Invoke("Ошибка удаления документа: недостаточно прав");
            return;
        }
        
        var message = $"Вы действительно хотите удалить \n документ \"{CurrentUser.Resource?.Name}\"?";
        var result = await Confirmation(message);
        
        if (result == ConfirmationResult.Yes)
        {
            ResourceManager.DeleteObject(CurrentResource?.Resource);
            LogEvent?.Invoke("Документ успешно удалён");
            UpdateResources();
        }
    }
    
    public ReactiveCommand<Unit, Unit> GrantAccessCommand { get; }
    private async Task GrantAccess()
    {
        if (CurrentResource?.IsTakeGrant is not null && CurrentResource.IsTakeGrant) {
            LogEvent?.Invoke("Ошибка предоставления прав: недостаточно прав");
            return;
        }
        
        var result = await UserSelection(new ObservableCollection<User>(
            UserManager.GetAllUsers().Where(user => !UserList.ToList().Exists(entry => entry.User?.Id == user.Id))
        ));
        
        if (result is not null) {
            AccessControlEntryManager.CreateEntry(CurrentUser.Resource, result);
            LogEvent?.Invoke("Добавлена новая запись контроля доступа");
            UpdateResources();
        }
    }
    
    public ReactiveCommand<Unit, Unit> ChangeOwnerCommand { get; }
    private async Task ChangeOwner()
    {
        if (CurrentResource?.Resource?.Owner?.Id != UserManager.CurrentUser?.Id) {
            LogEvent?.Invoke("Ошибка смены владельца: недостаточно прав");
            return;
        }
        
        var result = await UserSelection(new ObservableCollection<User>(
            UserManager.GetAllUsers().Where(user => user.Id != UserManager.CurrentUser?.Id)
        ));
        
        if (result is not null && CurrentResource?.Resource is not null) {
            CurrentResource.Resource.Owner = result;
            ResourceManager.ChangeOwnerObject(CurrentResource.Resource, result);
            LogEvent?.Invoke("У выбранного документа успешно сменился владелец");
            UpdateResources();
        }
    }

    private void UpdateResources()
    {
        ResourceList.Clear();
        ResourceList.AddRange(AccessControlEntryManager.GetEntries());
        CurrentResource = ResourceList.FirstOrDefault() ?? new AccessControlEntry();
    }
    
    private bool _isEditMode;
    public bool IsEditMode
    {
        get => _isEditMode;
        set => this.RaiseAndSetIfChanged(ref _isEditMode, value);
    }

    public void ChangeEditMode()
    {
        if (IsEditMode) UpdateResources();
        IsEditMode = !IsEditMode;
    }
    
    private async Task<ConfirmationResult> Confirmation(string message)
    {
        var confirmation = new ConfirmationViewModel {
            Message = message
        };
        
        return await ConfirmationDialog.Handle(confirmation);
    }
    
    private async Task<User?> UserSelection(ObservableCollection<User> users)
    {
        var selection = new UserSelectionViewModel {
            UserList = users
        };
        
        return await UserSelectionDialog.Handle(selection);
    }
    
    public Interaction<ConfirmationViewModel, ConfirmationResult> ConfirmationDialog { get; }
    public Interaction<UserSelectionViewModel, User?> UserSelectionDialog { get; }

    public ResourceViewModel()
    {
        ConfirmationDialog = new Interaction<ConfirmationViewModel, ConfirmationResult>();
        UserSelectionDialog = new Interaction<UserSelectionViewModel, User?>();
        DeleteResourceCommand = ReactiveCommand.CreateFromTask(DeleteResource);
        GrantAccessCommand = ReactiveCommand.CreateFromTask(GrantAccess);
        ChangeOwnerCommand = ReactiveCommand.CreateFromTask(ChangeOwner);
        
        _resourceList = new ObservableCollection<AccessControlEntry>(
            AccessControlEntryManager.GetEntries()
        );
        
        _currentResource = _resourceList.FirstOrDefault() ?? new AccessControlEntry();
        
        _userList = new ObservableCollection<AccessControlEntry>(
            AccessControlEntryManager.GetEntries(_currentResource.Resource)
        );
        
        _currentUser = _userList.FirstOrDefault() ?? new AccessControlEntry();
        _resourceContent = string.Empty;
        CurrentUser = _currentUser;
    }
}