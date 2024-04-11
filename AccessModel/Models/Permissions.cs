using System.ComponentModel.DataAnnotations;

namespace AccessModel.Models;

/// <summary>
/// Права пользователя на защищаемый объект
/// </summary>
public class Permissions
{
    /// <summary>
    /// Право на чтение
    /// </summary>
    [Required]
    public bool Read { get; set; }
    
    /// <summary>
    /// Право на запись
    /// </summary>
    [Required]
    public bool Write { get; set; }
    
    /// <summary>
    /// Право на передачу разрешений другому пользователю
    /// </summary>
    [Required]
    public bool TakeGrant { get; set; }
}