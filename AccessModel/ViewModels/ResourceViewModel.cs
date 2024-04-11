using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using AccessModel.Models;
using AccessModel.Services;
using DynamicData;
using ReactiveUI;

namespace AccessModel.ViewModels;

public class ResourceViewModel : ViewModelBase
{
    private ObservableCollection<AccessControlEntry> _aclList;
    public ObservableCollection<AccessControlEntry> AclList
    {
        get => _aclList;
        set => this.RaiseAndSetIfChanged(ref _aclList, value);
    }
    
    private AccessControlEntry _currentAcl;
    public AccessControlEntry CurrentAcl
    {
        get => _currentAcl;
        set => this.RaiseAndSetIfChanged(ref _currentAcl, value);
    }

    private ObservableCollection<User> _userList;
    public ObservableCollection<User> UserList
    {
        get => _userList;
        set => this.RaiseAndSetIfChanged(ref _userList, value);
    }
    
    private User _currentUser;
    public User CurrentUser
    {
        get => _currentUser;
        set => this.RaiseAndSetIfChanged(ref _currentUser, value);
    }

    public void CreateResource()
    {
        var currentUser = new User { Name = "Администратор", Login = "Admin", Password = "A123!" };
        var currentPermissions = new Permissions { Read = true, Write = true, TakeGrant = true };
        
        AclList.Add(new AccessControlEntry { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "123", Owner = currentUser, CreateDateTime = DateTime.Now } });
    }

    public void DeleteResource()
    {
        AclList.Remove(CurrentAcl);
        if (AclList.Count > 0) CurrentAcl = AclList.First();
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
        var currentUser = new User { Name = "Администратор", Login = "Admin", Password = "A123!" };
        var currentPermissions = new Permissions { Read = true, Write = true, TakeGrant = true };
        
        _aclList = new ObservableCollection<AccessControlEntry>(new List<AccessControlEntry>  {
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "01", Owner = currentUser, CreateDateTime = new DateTime(2024, 03, 01) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "02", Owner = currentUser, CreateDateTime = new DateTime(2024, 03, 02) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "03", Owner = currentUser, CreateDateTime = new DateTime(2024, 03, 03) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "04", Owner = currentUser, CreateDateTime = new DateTime(2024, 03, 04) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "05", Owner = currentUser, CreateDateTime = new DateTime(2024, 03, 05) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "06", Owner = currentUser, CreateDateTime = new DateTime(2024, 03, 06) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "07", Owner = currentUser, CreateDateTime = new DateTime(2024, 03, 07) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "08", Owner = currentUser, CreateDateTime = new DateTime(2024, 03, 08) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "09", Owner = currentUser, CreateDateTime = new DateTime(2024, 03, 09) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "10", Owner = currentUser, CreateDateTime = new DateTime(2024, 03, 10) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "11", Owner = currentUser, CreateDateTime = new DateTime(2024, 03, 11) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "12", Owner = currentUser, CreateDateTime = new DateTime(2024, 03, 12) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "13", Owner = currentUser, CreateDateTime = new DateTime(2024, 03, 13) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "14", Owner = currentUser, CreateDateTime = new DateTime(2024, 03, 14) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "15", Owner = currentUser, CreateDateTime = new DateTime(2024, 03, 15) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "16", Owner = currentUser, CreateDateTime = new DateTime(2024, 03, 16) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "17", Owner = currentUser, CreateDateTime = new DateTime(2024, 03, 17) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "18", Owner = currentUser, CreateDateTime = new DateTime(2024, 03, 18) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "19", Owner = currentUser, CreateDateTime = new DateTime(2024, 03, 19) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "20", Owner = currentUser, CreateDateTime = new DateTime(2024, 03, 20) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "21", Owner = currentUser, CreateDateTime = new DateTime(2024, 03, 21) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "22", Owner = currentUser, CreateDateTime = new DateTime(2024, 03, 22) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "23", Owner = currentUser, CreateDateTime = new DateTime(2024, 03, 23) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "24", Owner = currentUser, CreateDateTime = new DateTime(2024, 03, 24) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "25", Owner = currentUser, CreateDateTime = new DateTime(2024, 03, 25) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "26", Owner = currentUser, CreateDateTime = new DateTime(2024, 03, 26) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "27", Owner = currentUser, CreateDateTime = new DateTime(2024, 03, 27) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "28", Owner = currentUser, CreateDateTime = new DateTime(2024, 03, 28) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "29", Owner = currentUser, CreateDateTime = new DateTime(2024, 03, 29) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "30", Owner = currentUser, CreateDateTime = new DateTime(2024, 03, 30) } }
        });
        
        _currentAcl = AclList[0];
        
        _userList = new ObservableCollection<User>(new List<User> {
            new() { Id = 0, Name = "Администратор", Login = "Admin", Password = "A123!" },
            new() { Id = 1, Name = "Вася", Login = "vasya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456" },
            new() { Id = 3, Name = "Лёша", Login = "alex", Password = "123456" }
        });
            
        _currentUser = UserList[0];
    }
}