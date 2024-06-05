using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AccessModel.Models;

namespace AccessModel.Services;

/// <summary>
/// Менеджер пользователей
/// </summary>
public static class UserManager
{
    /// <summary>
    /// Пользователь, под которым был выполнен вход
    /// </summary>
    public static User? CurrentUser { get; set; }
    
    /// <summary>
    /// Возвращает всех пользователей системы
    /// </summary>
    /// <returns> Список пользователей системы </returns>
    public static List<User> GetAllUsers()
    {
        using var db = new AccessModelContext();
        return db.Users.ToList();
    }
    
    /// <summary>
    /// Возвращает пользователя по заданному имени
    /// </summary>
    /// <param name="id"> Идентификатор пользователя </param>
    /// <returns></returns>
    public static User? GetUser(long id)
    {   
        using var db = new AccessModelContext();
        return db.Users.FirstOrDefault(p => p.Id == id);
    }
    
    /// <summary>
    /// Возвращает пользователя по заданному имени
    /// </summary>
    /// <param name="login"> Имя пользователя </param>
    /// <returns> Пользователь системы </returns>
    public static User? GetUser(string? login)
    {   
        using var db = new AccessModelContext();
        return db.Users.FirstOrDefault(p => p.Login == login);
    }
    
    /// <summary>
    /// Подтверждение пароля
    /// </summary>
    /// <param name="user"> Ссылка на пользователя </param>
    /// <param name="password"> Пароль </param>
    /// <returns> Успешность проверки </returns>
    public static bool PasswordVerification(User user, string? password)
    {
        using var db = new AccessModelContext();
        var hash = db.Users.FirstOrDefault(p=> p.Login == user.Login)?.Password ?? string.Empty;
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
    
    /// <summary>
    /// Создание пользователя
    /// </summary>
    /// <returns> Успешность операции </returns>
    public static bool CreateUser()
    {
        using var db = new AccessModelContext();
        db.Users.Add(new User());
        return db.SaveChanges() > 0;
    }
    
    /// <summary>
    /// Создание пользователя
    /// </summary>
    /// <param name="name"> Имя пользователя </param>
    /// <param name="login"> Логин пользователя </param>
    /// <param name="password"> пароль пользователя </param>
    /// <returns> Созданный пользователь </returns>
    public static User CreateUser(string name, string login, string password)
    {
        using var db = new AccessModelContext();
        var user = new User {
            Name = name,
            Login = login,
            Password = BCrypt.Net.BCrypt.HashPassword(password)
        };
        
        db.Users.Add(user);
        db.SaveChanges();
        
        return user;
    }

    /// <summary>
    /// Изменение параметров пользователя
    /// </summary>
    /// <param name="user"> Ссылка на пользователя </param>
    /// <returns> Успешность выполнения операции </returns>
    public static bool ChangeUser(User user)
    {
        using var db = new AccessModelContext();
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        db.Entry(user).State = EntityState.Modified;
        
        return db.SaveChanges() > 0;
    }
    
    /// <summary>
    /// Удаление пользователя
    /// </summary>
    /// <param name="user"> Ссылка на пользователя </param>
    /// <returns> Успешность выполнения операции </returns>
    public static bool DeleteUser(User user)
    {
        using var db = new AccessModelContext();
        var resources = db.Resources.Where(resource => resource.Owner != null && resource.Owner.Id == user.Id);
        var entries = db.AccessControlEntries.Where(entry => entry.User == user);
        db.AccessControlEntries.RemoveRange(entries);
        db.Resources.RemoveRange(resources);
        db.Users.Remove(user);
        
        return db.SaveChanges() > 0;
    }
}