using System;
using System.Collections.Generic;
using System.Linq;
using AccessModel.Models;
using Microsoft.EntityFrameworkCore;

namespace AccessModel.Services;

/// <summary>
/// Менеджер ресурсов, управляющий защищаемыми объектами системы
/// </summary>
public static class ResourceManager
{
    /// <summary>
    /// Возвращает список ресурсов, на которые есть какие-либо права у данного пользователя
    /// </summary>
    /// <param name="user"> Активный пользователь </param>
    /// <returns> Список защищаемых объектов </returns>
    public static List<Resource>? GetObjects(User user)
    {
        var resources = new List<Resource>();
        using var db = new AccessModelContext();
        var list = db.UsersAccessControlEntries?.Where(p =>
            p.Permission != null && p.User == user)
            .ToList();
        if (list == null) return null;
        var index = 0;
        for (; index < list.Count; index++)
        {
            var s = list[index];
            resources.Add(s.Resource);
        }
        return resources;

    }

    /// <summary>
    /// Чтение содержания защищаемого объекта
    /// </summary>
    /// <param name="resource"> Ресурс, содержание которого было запрошено </param>
    /// <param name="user"> Пользователь, запросивший содержание </param>
    /// <returns> Содержание защищаемого объекта </returns>
    public static string? ReadObject(Resource resource, User user)
    {
        using var db = new AccessModelContext();
        var permission = GetPermission(user, resource);
        if (permission?.Read != true) return "No rights";
        var resourceContent = db.UsersAccessControlEntries?.Include(p => p.Resource)
            .FirstOrDefault(p => p.User == user && p.Resource == resource)?.Resource.Content;
        return resourceContent;
    }
    
    /// <summary>
    /// Создание защищаемого объекта
    /// </summary>
    /// <param name="user"> Пользователь, создавший ресурс </param>
    /// <param name="name"> Название объекта </param>
    /// <returns> Успешность выполнения операции </returns>
    public static bool CreateObject(User user, string name = "")
    {
        using var db = new AccessModelContext();
        var resource = new Resource
        {
            Name = name,
            CreateDateTime = DateTime.Now,
            Content = ""
        };
        var accessControlEntry = new AccessControlEntry
        {
                User = user,
                Resource = resource,
                Permission = new Permission{Own = true, Read = true, Write = true, TakeGrant = true}
        };
        db.Resources?.Add(resource);
        if (db.Entry(resource).State != EntityState.Added) return false;
        db.UsersAccessControlEntries?.Add(accessControlEntry);
        if (db.Entry(accessControlEntry).State != EntityState.Added) return false;
        db.SaveChanges();
        return true;
    }

    /// <summary>
    /// Переименование защищаемого объекта
    /// </summary>
    /// <param name="resource"> Ссылка на объект, над которым выполняется операция </param>
    /// <param name="user"> Пользователь, пытающийся выполнить данную операцию </param>
    /// <param name="name"> Новое название объекта </param>
    /// <returns> Успешность выполнения операции </returns>
    public static bool RenameObject(Resource? resource, User user, string name)
    {
        using var db = new AccessModelContext();
        if (resource != null)
        {
            var permission = GetPermission(user, resource);
            if (permission?.Write != true) return false;
            resource.Name = name;
            db.Entry(resource).State = EntityState.Modified;
        }
        else return true;
        var countUpdate = db.SaveChanges();
        return countUpdate > 0;
    }
    
    /// <summary>
    /// Изменение содержание объекта
    /// </summary>
    /// <param name="resource"> Ссылка на защищаемый объект </param>
    /// <param name="user"> Пользователь, пытающий модифицировать содержимое объекта </param>
    /// <param name="text"> Новое содержание защищаемого объекта </param>
    /// <returns> Успешность выполнения операции </returns>
    public static bool ModifyObject(Resource? resource, User user, string text)
    {
        using var db = new AccessModelContext();
        if (resource != null)
        {
            var permission = GetPermission(user, resource);
            if (permission?.Write != true) return false;
            resource.Content = text;
            db.Entry(resource).State = EntityState.Modified;
        }
        else return true;
        var countUpdate = db.SaveChanges();
        return countUpdate > 0;
    }

    /// <summary>
    /// Возвращает права доступа для зазащиаеомго объекта
    /// </summary>
    /// <param name="resource"> Ссылка на объект, над которым выполняется операция </param>
    /// <param name="user"> Пользователь, пытающийся выполнить данную операцию </param>
    /// <returns> Права доступа для зазащиаеомго объекта </returns>
    public static Permission? GetPermission(User user, Resource resource)
    {
        using var db = new AccessModelContext();
        return  db.UsersAccessControlEntries?.FirstOrDefault(p => p.User == user || p.Resource == resource)!.Permission;
    }
    /// <summary>
    /// Удаление защищаемого объекта
    /// </summary>
    /// <param name="resource"> Ссылка на объект, над которым выполняется операция </param>
    /// <param name="user"> Пользователь, пытающийся выполнить данную операцию </param>
    /// <returns> Успешность выполнения операции </returns>
    public static bool DeleteObject(Resource resource, User user)
    {
        using var db = new AccessModelContext();
        var permission = GetPermission(user, resource);
        if (permission?.Write != true) return false;
        db.Resources?.Remove(resource);
        var countUpdate = db.SaveChanges();
        return countUpdate > 0;

    }
}