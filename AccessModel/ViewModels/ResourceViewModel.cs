using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AccessModel.Models;
using AccessModel.Services;

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
            UserList = new ObservableCollection<AccessControlEntry>(
                AccessControlEntryManager.GetAccessControlEntries(value.Resource)
                ?? new List<AccessControlEntry>()
            );
            
            CurrentUser = UserList.First(entry => entry.User.Id == UserManager.CurrentUser?.Id);
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
        ResourceList.Add( AccessControlEntryManager.CreateAccessControlEntry());
        CurrentResource = ResourceList.Last();
    }

    public void SaveResource()
    {
        
    }

    public void DeleteResource()
    {
        ResourceList.Remove(CurrentResource);
        if (ResourceList.Count > 0) CurrentResource = ResourceList.First();
    }
    
    private bool _isEditMode;
    public bool IsEditMode
    {
        get => _isEditMode;
        set => this.RaiseAndSetIfChanged(ref _isEditMode, value);
    }

    public void ChangeEditMode() { IsEditMode = !IsEditMode; }

    public ResourceViewModel()
    {
        _resourceList = new ObservableCollection<AccessControlEntry>(
            AccessControlEntryManager.GetAccessControlEntries() 
            ?? new List<AccessControlEntry>()
        );
        
        _currentResource = _resourceList.FirstOrDefault() ?? new AccessControlEntry();
        
        _userList = new ObservableCollection<AccessControlEntry>(
            AccessControlEntryManager.GetAccessControlEntries(_currentResource.Resource)
            ?? new List<AccessControlEntry>()
        );
        
        _currentUser = _userList.FirstOrDefault() ?? new AccessControlEntry();
    }
}