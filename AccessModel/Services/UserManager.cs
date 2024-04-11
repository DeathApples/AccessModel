using System;
using BCrypt.Net;
using System.Linq;
using System.Collections.Generic;
using AccessModel.Models;
using Microsoft.EntityFrameworkCore;

namespace AccessModel.Services;

/// <summary>
/// Менеджер пользователей
/// </summary>
public class UserManager
{
    /// <summary>
    /// Пользователь, под которым был выполнен вход
    /// </summary>
    public static User? CurrentUser { get; set; }
    
    /// <summary>
    /// Возвращает всех пользователей системы
    /// </summary>
    /// <returns> Список пользователей системы </returns>
    public static List<User>? GetAllUsers()
    {
        using var db = new AccessModelContext();
        return db.Users?.ToList();
    }
    
    /// <summary>
    /// Возвращает пользователя по заданному имени
    /// </summary>
    /// <param name="login"> Имя пользователя </param>
    /// <returns> Пользователь системы </returns>
    public static User? GetUser(string login)
    {   
        using var db = new AccessModelContext();
        return db.Users?.FirstOrDefault(p => p.Login == login);
    }
    
    /// <summary>
    /// Аутентификация пользователя
    /// </summary>
    /// <param name="login"> Имя пользователя </param>
    /// <param name="password"> Пароль </param>
    /// <returns> Успешность выполнения операции </returns>
    public static bool UserVerification(string login, string password)
    {
        using var db = new AccessModelContext();
        return db.Users?.FirstOrDefault(p => p.Login == login && p.Password == BCrypt.Net.BCrypt.HashPassword(password)) != null;
    }
    
    /// <summary>
    /// Создание пользователя
    /// </summary>
    /// <param name="name"> Имя пользователя</param>
    /// <param name="login"> Логин пользователя </param>
    /// <param name="password"> Пароль </param>
    /// <returns> Успешность выполнения операции </returns>
    public static bool CreateUser(string name, string login, string password)
    {
        using var db = new AccessModelContext();
        var user = new User {
            Name = name,
            Login = login,
            Password = BCrypt.Net.BCrypt.HashPassword(password)
        };
        
        db.Users?.Add(user);
        if (db.Entry(user).State != EntityState.Added) return false;
        db.SaveChanges();
        
        return true;
    }

    /// <summary>
    /// Переименование пользователя
    /// </summary>
    /// <param name="user"> Ссылка на пользователя </param>
    /// <param name="login"> Новое имя пользователя </param>
    /// <returns> Успешность выполнения операции </returns>
    public static bool RenameUser(User? user, string login)
    {
        using var db = new AccessModelContext();
        if (user != null)
        {
            if (user.Login != login)
            {
                user.Login = login;
                db.Entry(user).State = EntityState.Modified;
            }
            else return true;
        }
        var countUpdate = db.SaveChanges();
        return countUpdate > 0;
    }

    /// <summary>
    /// Подтверждение пароля
    /// </summary>
    /// <param name="user"> Ссылка на пользователя </param>
    /// <param name="password"> Пароль </param>
    /// <returns> Успешность проверки </returns>
    public static bool PasswordVerification(User user, string password)
    {
        using var db = new AccessModelContext();
        var hash = db.Users?.FirstOrDefault(p=> p.Login == user.Login)?.Password;
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }

    /// <summary>
    /// Смена пароля
    /// </summary>
    /// <param name="user"> Ссылка на пользователя </param>
    /// <param name="password"> Новый пароль </param>
    /// <returns> Успешность выполнения операции </returns>
    public static bool ChangePassword(User? user, string password)
    {
        using var db = new AccessModelContext();
        if (user != null)
        {
            if (user.Password != BCrypt.Net.BCrypt.HashPassword(password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(password);
                db.Entry(user).State = EntityState.Modified;
            }
            else return true;
        }
        var countUpdate = db.SaveChanges();
        return countUpdate > 0;
    }
    
    /// <summary>
    /// Удаление пользователя
    /// </summary>
    /// <param name="user"> Ссылка на пользователя </param>
    /// <returns> Успешность выполнения операции </returns>
    public static bool DeleteUser(User? user)
    {
        using var db = new AccessModelContext();
        if (user != null)
        {
            db.Users?.Remove(user);
            var countUpdate = db.SaveChanges();
            return countUpdate > 0;
        }
        else return true;
    }
}