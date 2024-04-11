using System.ComponentModel.DataAnnotations;

namespace AccessModel.Models;

/// <summary>
/// Пользователь системы
/// </summary>
public class User
{
    /// <summary>
    /// Целочисленный идентификатор пользователя
    /// </summary>
    [Required]
    public long Id { get; set; }
    
    /// <summary>
    /// Имя пользователя системы
    /// </summary>
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Логин пользователя, под которым он может совершать вход в систему
    /// </summary>
    [MaxLength(50)]
    public string Login { get; set; } = string.Empty;

    /// <summary>
    /// Хэш пароля пользователя
    /// </summary>
    [MaxLength(50)]
    public string Password { get; set; } = string.Empty;

    public bool IsAdmin => Id == 0;
}