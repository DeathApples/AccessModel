using System.ComponentModel.DataAnnotations;

namespace AccessModel.Models;

public class User
{
    [Required]
    public long Id { get; init; }
    
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Login { get; set; } = string.Empty;

    [MaxLength(1024)]
    public string Password { get; set; } = string.Empty;
    
    public SecurityLevel SecurityClearance { get; set; }

    public bool IsAdmin => Id == 1;
    
    public string SecurityLabel => SecurityClearance switch {
        SecurityLevel.Unclassified => "Неклассифицированно",
        SecurityLevel.TopSecret => "Совершенно секретно",
        SecurityLevel.Confidential => "Конфиденциально",
        SecurityLevel.Secret => "Секретно",
        _ => string.Empty
    };
}