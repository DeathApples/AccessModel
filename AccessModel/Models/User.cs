using Microsoft.EntityFrameworkCore;
namespace AccessModel.Models;

/// <summary>
/// Пользователь системы
/// </summary>
public class User: DbContext
{
    /// <summary>
    /// Целочисленный идентификатор пользователя
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Имя пользователя, под которым он может совершать вход в систему
    /// </summary>
    public string Login { get; set; }
    
    /// <summary>
    /// Хэш пароля пользователя
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// Уровень привилегированности (администратор/пользователь)
    /// </summary>
    public Role Role { get; set; }

    /// <summary>
    /// Базовый конструктор класса Пользователь
    /// </summary>
    public User()
    {
        Login = "";
        Password = "";
    }

    /// <summary>
    /// Конструктор класса Пользователь
    /// </summary>
    /// <param name="login"> Имя пользователя </param>
    /// <param name="password"> Хэш пароля </param>
    public User(string login, string password)
    {
        /* ToDo: Реализовать конструктор класса Пользователь
         * Инициализировать свойства классаа
         */
    }
}