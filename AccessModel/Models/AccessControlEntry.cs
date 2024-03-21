using Microsoft.EntityFrameworkCore;
namespace AccessModel.Models;

/// <summary>
/// Запись контроля доступа
/// </summary>
public class AccessControlEntry: DbContext
{
    /// <summary>
    /// Пользователь системы
    /// </summary>
    public User User { get; set; }
    
    /// <summary>
    /// Защищаемый объект
    /// </summary>
    public Resource Resource { get; set; }
    
    /// <summary>
    /// Разрешения, которыми обладает данный пользователь над данным объектом
    /// </summary>
    public Permission Permission { get; set; }

    /// <summary>
    /// Базовый конструктор класса Записи Контроля Доступа
    /// </summary>
    public AccessControlEntry()
    {
        User = new User();
        Resource = new Resource();
        Permission = new Permission();
    }
    
    /// <summary>
    /// Конструктор класса Записи Контроля Доступа
    /// </summary>
    /// <param name="user"> Пользователь </param>
    /// <param name="resource"> Защищаемый объект </param>
    /// <param name="permission"> Разрешения </param>
    public AccessControlEntry(User user, Resource resource, Permission permission)
    {
        User = user;
        Resource = resource;
        Permission = permission;
    }
}