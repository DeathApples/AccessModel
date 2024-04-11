using System.Collections.Generic;
using System.Collections.ObjectModel;
using AccessModel.Models;

namespace AccessModel.ViewModels;

public class UserViewModel : ViewModelBase
{
    public ObservableCollection<User> UserList { get; }

    public UserViewModel()
    {
        UserList = new ObservableCollection<User>(new List<User> {
            new() { Id = 0, Name = "Администратор", Login = "Admin", Password = "A123!", Role = new Role { Name = "Admin", IsPrivileged = true } },
            new() { Id = 1, Name = "Вася", Login = "vasya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 2, Name = "Петя", Login = "petya", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } },
            new() { Id = 3, Name = "Лёша", Login = "alex", Password = "123456", Role = new Role { Name = "User", IsPrivileged = false } }
        });
    }
}