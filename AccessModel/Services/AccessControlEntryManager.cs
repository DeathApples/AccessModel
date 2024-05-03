using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccessModel.Models;
using Microsoft.EntityFrameworkCore;

namespace AccessModel.Services;

public static class AccessControlEntryManager
{
    public static List<AccessControlEntry> GetEntries()
    {
        using var db = new AccessModelContext();
        return db.AccessControlEntries
            .Include(entry => entry.Resource)
            .Include(entry => entry.User)
            .Where(entry => entry.User == UserManager.CurrentUser)
            .ToList();
    }
    
    public static List<AccessControlEntry> GetEntries(Resource resource)
    {
        using var db = new AccessModelContext();
        return db.AccessControlEntries
            .Include(entry => entry.Resource)
            .Include(entry => entry.User)
            .Where(entry => entry.Resource == resource)
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

    public static bool ModifyEntry(AccessControlEntry entry)
    {
        using var db = new AccessModelContext();
        db.Entry(entry).State = EntityState.Modified;
        
        return db.SaveChanges() > 0;
    }

    public static void DeleteEntryRange(IEnumerable<AccessControlEntry> entries)
    {
        using var db = new AccessModelContext();
        db.AccessControlEntries.RemoveRange(entries);
        db.SaveChanges();
    }
}