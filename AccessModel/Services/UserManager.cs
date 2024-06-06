using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AccessModel.Models;

namespace AccessModel.Services;

public static class UserManager
{
    public static User? CurrentUser { get; set; }
    
    public static IEnumerable<User> GetAllUsers()
    {
        using var db = new AccessModelContext();
        return db.Users.OrderBy(user => user.Id);
    }
    
    public static User? GetUser(long id)
    {   
        using var db = new AccessModelContext();
        return db.Users.FirstOrDefault(p => p.Id == id);
    }
    
    public static User? GetUser(string? login)
    {   
        using var db = new AccessModelContext();
        return db.Users.FirstOrDefault(p => p.Login == login);
    }
    
    public static bool PasswordVerification(User user, string? password)
    {
        using var db = new AccessModelContext();
        var hash = db.Users.FirstOrDefault(p=> p.Login == user.Login)?.Password ?? string.Empty;
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
    
    public static void CreateUser()
    {
        using var db = new AccessModelContext();
        db.Users.Add(new User());
        db.SaveChanges();
    }
    
    public static User CreateUser(string name, string login, string password)
    {
        using var db = new AccessModelContext();
        var user = new User {
            Name = name,
            Login = login,
            Password = BCrypt.Net.BCrypt.HashPassword(password)
        };
        
        db.Users.Add(user);
        db.SaveChanges();
        
        return user;
    }
    
    public static void ModifyUser(User user)
    {
        using var db = new AccessModelContext();
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        db.Entry(user).State = EntityState.Modified;
        db.SaveChanges();
    }
    
    public static void DeleteUser(User user)
    {
        using var db = new AccessModelContext();
        db.Requests.RemoveRange(db.Requests.Where(request => request.User.Id == user.Id));
        db.Users.Remove(user);
        db.SaveChanges();
    }
}