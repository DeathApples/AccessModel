using System;
using System.ComponentModel.DataAnnotations;

namespace AccessModel.Models;

public class Resource
{
    [Required]
    public long Id { get; init; }
    
    [MaxLength(50)]
    public string Name { get; set; } = "Unnamed";

    [MaxLength(1024)]
    public string Content { get; set; } = string.Empty;

    public SecurityLevel SecurityClassification { get; set; }

    public DateTime CreateDateTime { get; init; } = DateTime.UtcNow;
}