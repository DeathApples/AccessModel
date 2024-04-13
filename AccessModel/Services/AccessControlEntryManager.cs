using System;
using System.Linq;
using System.Collections.Generic;
using AccessModel.Models;
using Microsoft.EntityFrameworkCore;

namespace AccessModel.Services;

public static class AccessControlEntryManager
{
    public static List<AccessControlEntry> GetAccessControlEntries()
    {
        using var db = new AccessModelContext();
        return db.AccessControlEntries
            .Include(entry => entry.Resource)
            .Include(entry => entry.User)
            .Where(entry => entry.User == UserManager.CurrentUser)
            .ToList();
    }
    
    public static List<AccessControlEntry> GetAccessControlEntries(Resource resource)
    {
        using var db = new AccessModelContext();
        return db.AccessControlEntries
            .Include(entry => entry.Resource)
            .Include(entry => entry.User)
            .Where(entry => entry.Resource == resource)
            .ToList();
    }

    public static bool CreateAccessControlEntry()
    {
        using var db = new AccessModelContext();
        var entry = new AccessControlEntry {
            User = UserManager.CurrentUser!,
            IsRead = true, IsWrite = true, IsTakeGrant = true,
            Resource = new Resource { Name = "Unnamed", Owner = UserManager.CurrentUser!, CreateDateTime = DateTime.UtcNow }
        };
        
        db.AccessControlEntries.Add(entry);
        return db.SaveChanges() > 0;
    }

    public static bool ChangeAccessControlEntry(AccessControlEntry entry)
    {
        using var db = new AccessModelContext();
        db.Entry(entry.Resource).State = EntityState.Modified;
        db.Entry(entry).State = EntityState.Modified;
        
        return db.SaveChanges() > 0;
    }

    public static void DeleteAccessControlEntry()
    {
        
    }
    
    public static void DeleteAccessControlEntriesForResource(Resource resource)
    {
        
    }
}