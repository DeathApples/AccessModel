using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AccessModel.Models;
using AccessModel.Services;
using DynamicData;

namespace AccessModel.ViewModels;

public class ResourceViewModel : ViewModelBase
{
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
            UserList.AddRange(AccessControlEntryManager.GetAccessControlEntries(value.Resource));
            CurrentUser = UserList.FirstOrDefault(entry => entry.User.Id == UserManager.CurrentUser?.Id) ?? new AccessControlEntry();
            
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
        AccessControlEntryManager.CreateAccessControlEntry();
        UpdateResources();
    }

    public void ModifyResource()
    {
        AccessControlEntryManager.ChangeAccessControlEntry(CurrentUser);
        ChangeEditMode();
    }

    public void DeleteResource()
    {
        AccessControlEntryManager.DeleteAccessControlEntriesForResource(CurrentResource.Resource);
        UpdateResources();
    }

    private void UpdateResources()
    {
        ResourceList.Clear();
        ResourceList.AddRange(AccessControlEntryManager.GetAccessControlEntries());
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

    public ResourceViewModel()
    {
        _resourceList = new ObservableCollection<AccessControlEntry>(
            AccessControlEntryManager.GetAccessControlEntries()
        );
        
        _currentResource = _resourceList.FirstOrDefault() ?? new AccessControlEntry();
        
        _userList = new ObservableCollection<AccessControlEntry>(
            AccessControlEntryManager.GetAccessControlEntries(_currentResource.Resource)
        );
        
        _currentUser = _userList.FirstOrDefault() ?? new AccessControlEntry();
    }
}