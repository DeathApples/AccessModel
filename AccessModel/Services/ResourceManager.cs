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
    /// Получение объекта по его идентификатор
    /// </summary>
    /// <param name="id"> идентификатор объекта </param>
    /// <returns></returns>
    public static Resource? GetObject(long id)
    {
        using var db = new AccessModelContext();
        return db.Resources.FirstOrDefault(resource => resource.Id == id);
    }
    
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
    /// Изменение содержание объекта
    /// </summary>
    /// <param name="resource"> Ссылка на защищаемый объект </param>
    public static void ModifyObject(Resource resource)
    {
        using var db = new AccessModelContext();
        db.Entry(resource).State = EntityState.Modified;
        db.SaveChanges();
    }
    
    public static void ChangeOwnerObject(Resource resource, User user)
    {
        using var db = new AccessModelContext();

        var entry = db.AccessControlEntries.FirstOrDefault(entry => 
            entry.Resource!.Id == resource.Id && entry.Resource!.Owner!.Id == user.Id);

        if (entry is null) {
            AccessControlEntryManager.CreateEntry(resource, user);
        } else {
            AccessControlEntryManager.ModifyEntry(entry);
        }
        
        db.Entry(resource).State = EntityState.Modified;
        db.SaveChanges();
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
        db.AccessControlEntries.RemoveRange(entries);
        db.Resources.Remove(resource);
        db.SaveChanges();
    }
}