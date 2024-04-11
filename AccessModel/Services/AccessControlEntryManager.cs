using System;
using System.Linq;
using System.Collections.Generic;
using AccessModel.Models;
using Microsoft.EntityFrameworkCore;

namespace AccessModel.Services;

public static class AccessControlEntryManager
{
    public static List<AccessControlEntry>? GetAccessControlEntries()
    {
        using var db = new AccessModelContext();
        return db.AccessControlEntries?
            .Where(accessControlEntry => accessControlEntry.User == UserManager.CurrentUser)
            .ToList();
    }
    
    public static List<AccessControlEntry>? GetAccessControlEntries(Resource resource)
    {
        using var db = new AccessModelContext();
        return db.AccessControlEntries?
            .Where(accessControlEntry => accessControlEntry.Resource == resource)
            .ToList();
    }

    public static bool CreateAccessControlEntry()
    {
        using var db = new AccessModelContext();
        var accessControlEntry = new AccessControlEntry {
            User = UserManager.CurrentUser!,
            Permissions = new Permissions { Read = true, Write = true, TakeGrant = true },
            Resource = new Resource { Name = "Unnamed", Owner = UserManager.CurrentUser!, CreateDateTime = DateTime.Now }
        };
        
        db.AccessControlEntries?.Add(accessControlEntry);
        if (db.Entry(accessControlEntry).State != EntityState.Added) return false;
        db.SaveChanges();
        
        return true;
    }

    public static void DeleteAccessControlEntry()
    {
        
    }
}