using System;
using AccessModel.Models;

namespace AccessModel.Services;

/// <summary>
/// Менеджер безопасности, управляющий разрешениями на защищаемые объекты
/// </summary>
public static class SecurityManager
{
    /// <summary>
    /// Проверка разрешения на чтение файла
    /// </summary>
    /// <param name="user"> Пользователь, чьи права проходят проверку </param>
    /// <param name="resource"> Объект, права на который проверяются </param>
    /// <returns> Успешность проверки </returns>
    public static bool ReadPermissionCheck(User user, Resource resource)
    {
        
        
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Проверка разрешения на запись файла
    /// </summary>
    /// <param name="user"> Пользователь, чьи права проходят проверку </param>
    /// <param name="resource"> Объект, права на который проверяются </param>
    /// <returns> Успешность проверки </returns>
    public static bool WritePermissionCheck(User user, Resource resource)
    {
        
        
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Проверка разрешения на удаление файла
    /// </summary>
    /// <param name="user"> Пользователь, чьи права проходят проверку </param>
    /// <param name="resource"> Объект, права на который проверяются </param>
    /// <returns> Успешность проверки </returns>
    public static bool DeletePermissionCheck(User user, Resource resource)
    {
        
        
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Предоставление прав другому пользователю
    /// </summary>
    /// <param name="owner"> Пользователь, который пытается предоставить право </param>
    /// <param name="user"> Пользователь, которому будут предоставлены права </param>
    /// <param name="permissions"> Права на защищаемый объект </param>
    /// <param name="resource"> Защищаемый объект, права на который предоставляются другому пользователю </param>
    /// <returns> Успешность выполнения операции </returns>
    public static bool GrantPermission(User owner, User user, Resource resource, Permissions permissions)
    {
        
        
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Изменение разрешающих прав на ресурс
    /// </summary>
    /// <param name="user"> Пользователь, для которого права изменяются </param>
    /// <param name="resource"> Объект, права на который изменяются </param>
    /// <returns> Успешность выполнения операции </returns>
    public static bool ChangePermission(User user, Resource resource)
    {
        
        
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Передача права владения другому пользователю
    /// </summary>
    /// <param name="owner"> Прежний владелец объекта </param>
    /// <param name="user"> Новый владелец объекта </param>
    /// <returns> Успешность выполнения операции </returns>
    public static bool TransferOwnership(User owner, User user)
    {
        
        
        throw new NotImplementedException();
    }
}