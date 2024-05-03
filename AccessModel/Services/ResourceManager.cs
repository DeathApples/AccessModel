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
    /// Создание защищаемого объекта
    /// </summary>
    /// <returns> Успешность выполнения операции </returns>
    public static void CreateObject()
    {
        using var db = new AccessModelContext();
        var entryEntity = db.Resources.Add(new Resource());

        entryEntity.Entity.Owner = UserManager.CurrentUser!;
        AccessControlEntryManager.CreateEntry(entryEntity.Entity, UserManager.CurrentUser);
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
        return db.SaveChanges() > 0;
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
        return db.SaveChanges() > 0;
    }

    /// <summary>
    /// Удаление защищаемого объекта
    /// </summary>
    /// <param name="resource"> Ссылка на объект, над которым выполняется операция </param>
    public static void DeleteObject(Resource? resource)
    {
        if (resource == null) return;
        
        using var db = new AccessModelContext();
        var entries = db.AccessControlEntries.Where(entry => entry.Resource != null && entry.Resource.Id == resource.Id);
        AccessControlEntryManager.DeleteEntryRange(entries);
        db.Resources.Remove(resource);
        db.SaveChanges();
    }
}