namespace AccessModel.Models;

/// <summary>
/// Уровень привилегированности (администратор/пользователь)
/// </summary>
public enum Role
{
    /// <summary>
    /// Администратор
    /// </summary>
    Admin,
    
    /// <summary>
    /// Непривилегированный пользователь
    /// </summary>
    User
}