using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AccessModel.Models;
using AccessModel.Services;
using DynamicData;

namespace AccessModel.ViewModels;

public class ResourceViewModel : ViewModelBase
{
    public static event Action<string>? LogEvent;
    
    private ObservableCollection<AccessControlEntry> _resourceList;
    public ObservableCollection<AccessControlEntry> ResourceList
    {
        get => _resourceList;
        set => this.RaiseAndSetIfChanged(ref _resourceList, value);
    }
    
    private AccessControlEntry _currentResource;
    public AccessControlEntry CurrentResource
    {
        get => _currentResource;
        set
        {
            UserList.Clear();
            UserList.AddRange(AccessControlEntryManager.GetEntries(value.Resource));
            CurrentUser = UserList.FirstOrDefault(entry => entry.User?.Id == UserManager.CurrentUser?.Id) ?? new AccessControlEntry();
            
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

    public void CreateResource()
    {
        ResourceManager.CreateObject();
        UpdateResources();
    }

    public void ModifyResource()
    {
        AccessControlEntryManager.ModifyEntry(CurrentUser);
        ChangeEditMode();
    }
    
    public ICommand DeleteResourceCommand { get; }
    private async Task DeleteResource()
    {
        var message = $"Вы действительно хотите удалить \n документ \"{CurrentUser.Resource?.Name}\"?";
        var result = await Confirmation(message);
        
        if (result == ConfirmationResult.Yes)
        {
            ResourceManager.DeleteObject(CurrentResource.Resource);
            UpdateResources();
        }
    }
    
    public ICommand SelectUserCommand { get; }
    private async Task SelectUser()
    {
        var result = await UserSelection();
        if (result != null && UserList.ToList().Exists(entry => entry.User?.Id != result.Id))
        {
            AccessControlEntryManager.CreateEntry(CurrentUser.Resource, result);
            UpdateResources();
        }
    }

    private void UpdateResources()
    {
        ResourceList.Clear();
        ResourceList.AddRange(AccessControlEntryManager.GetEntries());
        CurrentResource = ResourceList.FirstOrDefault(entry => entry.Id == CurrentResource.Id) 
                          ?? ResourceList.FirstOrDefault() ?? new AccessControlEntry();
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
    
    private async Task<User?> UserSelection()
    {
        var selection = new UserSelectionViewModel {
            /*UserList = new ObservableCollection<User>(
                UserManager.GetAllUsers().Where(user => !UserList.ToList().Exists(entry => entry.User == user))
            ),*/
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
        SelectUserCommand = ReactiveCommand.CreateFromTask(SelectUser);
        
        _resourceList = new ObservableCollection<AccessControlEntry>(
            AccessControlEntryManager.GetEntries()
        );
        
        _currentResource = _resourceList.FirstOrDefault() ?? new AccessControlEntry();
        
        _userList = new ObservableCollection<AccessControlEntry>(
            AccessControlEntryManager.GetEntries(_currentResource.Resource)
        );
        
        _currentUser = _userList.FirstOrDefault() ?? new AccessControlEntry();
    }
}