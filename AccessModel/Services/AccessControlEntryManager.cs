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

    public static AccessControlEntry CreateAccessControlEntry()
    {
        using var db = new AccessModelContext();
        var currentUser = db.Users.First(u => u.Id == UserManager.CurrentUser!.Id);
        var entry = new AccessControlEntry {
            User = currentUser,
            IsRead = true, IsWrite = true, IsTakeGrant = true,
            Resource = new Resource { Name = "Unnamed", Owner = currentUser, CreateDateTime = DateTime.UtcNow }
        };
        
        db.AccessControlEntries.Add(entry);
        db.SaveChanges();
        
        return entry;
    }

    public static void DeleteAccessControlEntry()
    {
        
    }
}