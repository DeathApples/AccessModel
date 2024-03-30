using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using AccessModel.Models;
using AccessModel.Services;
using ReactiveUI;

namespace AccessModel.ViewModels;

public class ResourceViewModel : ViewModelBase
{
    public ObservableCollection<AccessControlEntry>? AclList { get; set; }
    
    private bool _isEditMode;
    public bool IsEditMode
    {
        get => _isEditMode;
        set => this.RaiseAndSetIfChanged(ref _isEditMode, value);
    }
    
    private AccessControlEntry _currentAcl;
    public AccessControlEntry CurrentAcl
    {
        get => _currentAcl;
        set => this.RaiseAndSetIfChanged(ref _currentAcl, value);
    }

    public void ChangeEditMode() { IsEditMode = !IsEditMode; }

    public ResourceViewModel()
    {
        var currentUser = new User { Name = "Администратор", Login = "Admin", Password = "A123!", Role = new Role { Name = "Admin", IsPrivileged = true } };
        var currentPermissions = new Permissions { Read = true, Write = true, TakeGrant = true };
        
        AclList = new ObservableCollection<AccessControlEntry>(new List<AccessControlEntry> {
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "01", CreateDateTime = new DateTime(2024, 03, 01) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "02", CreateDateTime = new DateTime(2024, 03, 02) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "03", CreateDateTime = new DateTime(2024, 03, 03) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "04", CreateDateTime = new DateTime(2024, 03, 04) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "05", CreateDateTime = new DateTime(2024, 03, 05) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "06", CreateDateTime = new DateTime(2024, 03, 06) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "07", CreateDateTime = new DateTime(2024, 03, 07) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "08", CreateDateTime = new DateTime(2024, 03, 08) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "09", CreateDateTime = new DateTime(2024, 03, 09) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "10", CreateDateTime = new DateTime(2024, 03, 10) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "11", CreateDateTime = new DateTime(2024, 03, 11) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "12", CreateDateTime = new DateTime(2024, 03, 12) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "13", CreateDateTime = new DateTime(2024, 03, 13) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "14", CreateDateTime = new DateTime(2024, 03, 14) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "15", CreateDateTime = new DateTime(2024, 03, 15) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "16", CreateDateTime = new DateTime(2024, 03, 16) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "17", CreateDateTime = new DateTime(2024, 03, 17) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "18", CreateDateTime = new DateTime(2024, 03, 18) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "19", CreateDateTime = new DateTime(2024, 03, 19) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "20", CreateDateTime = new DateTime(2024, 03, 20) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "21", CreateDateTime = new DateTime(2024, 03, 21) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "22", CreateDateTime = new DateTime(2024, 03, 22) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "23", CreateDateTime = new DateTime(2024, 03, 23) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "24", CreateDateTime = new DateTime(2024, 03, 24) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "25", CreateDateTime = new DateTime(2024, 03, 25) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "26", CreateDateTime = new DateTime(2024, 03, 26) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "27", CreateDateTime = new DateTime(2024, 03, 27) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "28", CreateDateTime = new DateTime(2024, 03, 28) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "29", CreateDateTime = new DateTime(2024, 03, 29) } },
            new() { User = currentUser, Permissions = currentPermissions, Resource = new Resource { Name = "30", CreateDateTime = new DateTime(2024, 03, 30) } }
        });

        CurrentAcl = AclList[0];
    }
}