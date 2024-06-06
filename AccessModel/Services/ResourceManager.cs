using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AccessModel.Models;

namespace AccessModel.Services;

public static class ResourceManager
{
    public static Resource? GetResource(long id)
    {
        using var db = new AccessModelContext();
        return db.Resources.FirstOrDefault(resource => resource.Id == id);
    }
    
    public static IEnumerable<Resource> GetResources()
    {
        using var db = new AccessModelContext();
        var level = UserManager.CurrentUser?.SecurityClearance ?? 0;
        return db.Resources.Where(resource => resource.SecurityClassification == level);
    }
    
    public static void CreateResource()
    {
        using var db = new AccessModelContext();
        var level = UserManager.CurrentUser?.SecurityClearance ?? 0;
        db.Resources.Add(new Resource { SecurityClassification = level });
        db.SaveChanges();
    }
    
    public static void ModifyResource(Resource resource)
    {
        using var db = new AccessModelContext();
        db.Entry(resource).State = EntityState.Modified;
        db.SaveChanges();
    }
    
    public static void DeleteResource(Resource resource)
    {
        using var db = new AccessModelContext();
        db.Requests.RemoveRange(db.Requests.Where(request => request.Resource.Id == resource.Id));
        db.Resources.Remove(resource);
        db.SaveChanges();
    }
}