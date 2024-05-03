using System.ComponentModel.DataAnnotations;

namespace AccessModel.Models;

/// <summary>
/// Запись контроля доступа
/// </summary>
public class AccessControlEntry
{
    /// <summary>
    /// Целочисленный идентификатор записи контроля доступа
    /// </summary>
    [Required]
    public long Id { get; set; }
    
    /// <summary>
    /// Право на чтение
    /// </summary>
    public bool IsRead { get; set; }

    /// <summary>
    /// Право на запись
    /// </summary>
    public bool IsWrite { get; set; }

    /// <summary>
    /// Право на передачу разрешений другому пользователю
    /// </summary>
    public bool IsTakeGrant { get; set; }
    
    /// <summary>
    /// Пользователь системы
    /// </summary>
    public User? User { get; set; }
    
    /// <summary>
    /// Защищаемый объект
    /// </summary>
    public Resource? Resource { get; set; }

    /// <summary>
    /// Базовый конструктор класса записи контроля доступа
    /// </summary>
    public AccessControlEntry()
    {
        IsRead = true;
        IsWrite = true;
        IsTakeGrant = true;
    }
}