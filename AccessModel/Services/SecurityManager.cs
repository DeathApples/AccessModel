using System;
using AccessModel.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
        using var db = new AccessModelContext();
        var permission = db.UsersAccessControlEntries?.Include(accessControlEntry => accessControlEntry.Permission)?.FirstOrDefault(p => p.User == user || p.Resource == resource);
        return permission?.Permission?.Read != false;
    }
    
    /// <summary>
    /// Проверка разрешения на запись файла
    /// </summary>
    /// <param name="user"> Пользователь, чьи права проходят проверку </param>
    /// <param name="resource"> Объект, права на который проверяются </param>
    /// <returns> Успешность проверки </returns>
    public static bool WritePermissionCheck(User user, Resource resource)
    {
        using var db = new AccessModelContext();
        var permission = db.UsersAccessControlEntries?.Include(accessControlEntry => accessControlEntry.Permission)?.FirstOrDefault(p => p.User == user || p.Resource == resource);
        return permission?.Permission?.Write != false;
    }
    
    /// <summary>
    /// Проверка разрешения на удаление файла
    /// </summary>
    /// <param name="user"> Пользователь, чьи права проходят проверку </param>
    /// <param name="resource"> Объект, права на который проверяются </param>
    /// <returns> Успешность проверки </returns>
    public static bool DeletePermissionCheck(User user, Resource resource)
    {
        using var db = new AccessModelContext();
        var permission = db.UsersAccessControlEntries?.Include(accessControlEntry => accessControlEntry.Permission)?.FirstOrDefault(p => p.User == user || p.Resource == resource);
        return permission?.Permission?.Own != false;
    }
    
    /// <summary>
    /// Предоставление прав другому пользователю
    /// </summary>
    /// <param name="owner"> Пользователь, который пытается предоставить право </param>
    /// <param name="user"> Пользователь, которому будут предоставлены права </param>
    /// <param name="permission"> Права на защищаемый объект </param>
    /// <param name="resource"> Защищаемый объект, права на который предоставляются другому пользователю </param>
    /// <returns> Успешность выполнения операции </returns>
    public static bool GrantPermission(User owner, User user, Resource resource, Permission permission)
    {
        using var db = new AccessModelContext();
        var permissionOwner = db.UsersAccessControlEntries
            ?.Include(accessControlEntry => accessControlEntry.Permission)
            ?.FirstOrDefault(p => p.User == owner && p.Resource == resource);
        return permissionOwner?.Permission?.TakeGrant != false && ChangePermission(user, resource, permission);

    }
    
    /// <summary>
    /// Изменение разрешающих прав на ресурс
    /// </summary>
    /// <param name="user"> Пользователь, для которого права изменяются </param>
    /// <param name="resource"> Объект, права на который изменяются </param>
    /// <param name="permission"> Новые права доступа для объекта </param>
    /// <returns> Успешность выполнения операции </returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public static bool ChangePermission(User user, Resource resource, Permission permission)
    {
        using var db = new AccessModelContext();
        var accessControlEntries =
            db.UsersAccessControlEntries?.FirstOrDefault(p => p.User == user && p.Resource == resource);
        if (accessControlEntries != null)
        {
            accessControlEntries.Permission = permission;
            db.Entry(accessControlEntries).State = EntityState.Modified;
        }
        else
        {
            var accessControl = new AccessControlEntry
            {
                User = user,
                Resource = resource,
                Permission = permission
            };
            db.UsersAccessControlEntries?.Add(accessControl);
        }
        var countUpdate = db.SaveChanges();
        return countUpdate > 0;
    }
    
    /// <summary>
    /// Передача права владения объектом другому пользователю
    /// </summary>
    /// <param name="owner"> Прежний владелец объекта </param>
    /// <param name="user"> Новый владелец объекта </param>
    /// <param name="resource"> Объект, права на который передаются </param>
    /// <returns> Успешность выполнения операции </returns>
    public static bool TransferOwnership(User owner, User user, Resource resource)
    {
        using var db = new AccessModelContext();
        var permission = db.UsersAccessControlEntries?.Include(p => p.Permission)
            .FirstOrDefault(p => p.User == owner && p.Resource == resource);
        if (permission?.Permission != null)
        {
            db.UsersAccessControlEntries?.Remove(permission);
            //permission.Permission.Own = false;
            //permission.Permission.Read = false;
            //permission.Permission.TakeGrant = false;
            //permission.Permission.Write = false;// 
            //db.Entry(permission.Permission).State = EntityState.Modified;
        } 
        var accessControlEntries = db.UsersAccessControlEntries?.Include(p => p.Permission)
            .FirstOrDefault(p => p.User == user && p.Resource == resource);
        if (accessControlEntries?.Permission != null)
        {
            accessControlEntries.Permission.Own = true;
            db.Entry(accessControlEntries.Permission).State = EntityState.Modified;
        }
        else
        {
            var accessControl = new AccessControlEntry
            {
                User = user,
                Resource = resource,
                Permission = new Permission{Own = true, Read = true, Write = true, TakeGrant = true} // Я так понял новый сообственник должен иметь все права доступа
            };
            db.UsersAccessControlEntries?.Add(accessControl);
            
        }
        var countUpdate = db.SaveChanges();
        return countUpdate > 1;
    }
}