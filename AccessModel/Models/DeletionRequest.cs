using System;
using System.ComponentModel.DataAnnotations;

namespace AccessModel.Models;

public class DeletionRequest
{
    [Required]
    public long Id { get; init; }
    
    public User User { get; set; } = new();
    
    public Resource Resource { get; set; } = new();
    
    public DateTime CreateDateTime { get; init; } = DateTime.UtcNow;
}