using System;
using System.Collections.Generic;
using AccessModel.Models;

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
    public static List<User> GetAllUsers()
    {
        
        
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Возвращает пользователя по заданному имени
    /// </summary>
    /// <param name="login"> Имя пользователя </param>
    /// <returns> Пользователь системы </returns>
    public static User GetUser(string login)
    {
        
        
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Аутентификация пользователя
    /// </summary>
    /// <param name="login"> Имя пользователя </param>
    /// <param name="password"> Пароль </param>
    /// <returns> Успешность выполнения операции </returns>
    public static bool UserVerification(string login, string password)
    {
        
        
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Создание пользователя
    /// </summary>
    /// <param name="login"> Имя пользователя </param>
    /// <param name="password"> Пароль </param>
    /// <returns> Успешность выполнения операции </returns>
    public static bool CreateUser(string login, string password)
    {
        
        
        throw new NotImplementedException();
    }

    /// <summary>
    /// Переименование пользователя
    /// </summary>
    /// <param name="user"> Ссылка на пользователя </param>
    /// <param name="login"> Новое имя пользователя </param>
    /// <returns> Успешность выполнения операции </returns>
    public static bool RenameUser(User user, string login)
    {
        
        
        throw new NotImplementedException();
    }

    /// <summary>
    /// Подтверждение пароля
    /// </summary>
    /// <param name="user"> Ссылка на пользователя </param>
    /// <param name="password"> Пароль </param>
    /// <returns> Успешность проверки </returns>
    public static bool PasswordVerification(User user, string password)
    {
        
        
        throw new NotImplementedException();
    }

    /// <summary>
    /// Смена пароля
    /// </summary>
    /// <param name="user"> Ссылка на пользователя </param>
    /// <param name="password"> Новый пароль </param>
    /// <returns> Успешность выполнения операции </returns>
    public static bool ChangePassword(User user, string password)
    {
        
        
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Удаление пользователя
    /// </summary>
    /// <param name="user"> Ссылка на пользователя </param>
    /// <returns> Успешность выполнения операции </returns>
    public static bool DeleteUser(User user)
    {
        
        
        throw new NotImplementedException();
    }
}