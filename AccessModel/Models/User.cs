namespace AccessModel.Models;

/// <summary>
/// Пользователь системы
/// </summary>
public class User
{
    /// <summary>
    /// Целочисленный идентификатор пользователя
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Имя пользователя системы
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Логин пользователя, под которым он может совершать вход в систему
    /// </summary>
    public string? Login { get; set; }
    
    /// <summary>
    /// Хэш пароля пользователя
    /// </summary>
    public string? Password { get; set; }
    
    /// <summary>
    /// Уровень привилегированности (администратор/пользователь)
    /// </summary>
    public Role? Role { get; set; }
}