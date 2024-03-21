namespace AccessModel.Models;

/// <summary>
/// Группа пользователей
/// </summary>
public class Role
{
    /// <summary>
    /// Название роли
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Является ли роль привилегированной
    /// </summary>
    public bool IsPrivileged { get; set; }
}