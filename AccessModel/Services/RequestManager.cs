using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AccessModel.Models;

namespace AccessModel.Services;

public static class RequestManager
{
    public static DeletionRequest? GetRequest(long id)
    {
        using var db = new AccessModelContext();
        return db.Requests
            .Include(request => request.Resource)
            .Include(request => request.User)
            .FirstOrDefault(request => request.Id == id);
    }
    
    public static IEnumerable<DeletionRequest> GetAllRequests()
    {
        using var db = new AccessModelContext();
        return db.Requests
            .Include(request => request.Resource)
            .Include(request => request.User);
    }

    public static void CreateRequest(Resource resource)
    {
        using var db = new AccessModelContext();
        var requestEntity = db.Requests.Add(new DeletionRequest());
        requestEntity.Entity.User = UserManager.CurrentUser!;
        requestEntity.Entity.Resource = resource;
        db.SaveChanges();
    }
    
    public static void DeleteRequest(DeletionRequest request)
    {
        using var db = new AccessModelContext();
        db.Requests.Remove(request);
        db.SaveChanges();
    }
}