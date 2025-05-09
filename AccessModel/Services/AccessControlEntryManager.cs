using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccessModel.Models;
using Microsoft.EntityFrameworkCore;

namespace AccessModel.Services;

public static class AccessControlEntryManager
{
    public static AccessControlEntry? GetEntry(long id)
    {
        using var db = new AccessModelContext();
        return db.AccessControlEntries
            .Include(entry => entry.Resource)
            .Include(entry => entry.User)
            .FirstOrDefault(entry => entry.Id == id);
    }
    
    public static List<AccessControlEntry> GetEntries()
    {
        using var db = new AccessModelContext();
        return db.AccessControlEntries
            .Include(entry => entry.User)
            .Include(entry => entry.Resource)
            .Include(entry => entry.Resource!.Owner)
            .Where(entry => entry.User == UserManager.CurrentUser)
            .OrderBy(entry => entry.Resource!.Name)
            .ToList();
    }
    
    public static List<AccessControlEntry> GetEntries(Resource? resource)
    {
        using var db = new AccessModelContext();
        return db.AccessControlEntries
            .Include(entry => entry.Resource)
            .Include(entry => entry.User)
            .Where(entry => entry.Resource == resource)
            .OrderBy(entry => entry.User!.Name)
            .ToList();
    }

    public static void CreateEntry(Resource? resource, User? user)
    {
        using var db = new AccessModelContext();
        
        var entryEntity = db.AccessControlEntries.Add(new AccessControlEntry());
        entryEntity.Entity.Resource = resource;
        entryEntity.Entity.User = user;
        
        db.SaveChanges();
    }

    public static void ModifyEntry(AccessControlEntry entry)
    {
        using var db = new AccessModelContext();
        db.Entry(entry).State = EntityState.Modified;
        db.SaveChanges();
    }
    
    public static void DeleteEntry(AccessControlEntry entry)
    {
        using var db = new AccessModelContext();
        db.AccessControlEntries.Remove(entry);
        db.SaveChanges();
    }
}