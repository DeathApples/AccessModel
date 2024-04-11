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
    public static List<Resource> GetObjects(User user)
    {
        var resources = new List<Resource>();
        using var db = new AccessModelContext();
        var list = db.AccessControlEntries?.Include(ace => ace.Permissions)
            .Where(ace => ace.Permissions.Read || ace.Permissions.Write || ace.Permissions.TakeGrant)
            .Where(p=> p.User == user)
            .ToList();
        //return list;
        
        throw new NotImplementedException();
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
        var res = db.AccessControlEntries?.Include(p => p.Resource)
            .FirstOrDefault(p => p.User == user && p.Resource == resource)?.Resource.Content;
        return res;
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
                //Permission = db.UsersPermissions.FirstOrDefault(p => p!.Id == 5)
        };
        db.Resources?.Add(resource);
        if (db.Entry(resource).State != EntityState.Added) return false;
        db.AccessControlEntries?.Add(accessControlEntry);
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
            resource.Content = text;
            db.Entry(resource).State = EntityState.Modified;
        }
        else return true;
        var countUpdate = db.SaveChanges();
        return countUpdate > 0;
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
        db.Resources?.Remove(resource);
        var countUpdate = db.SaveChanges();
        return countUpdate > 0;
    }
}