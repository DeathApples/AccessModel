namespace AccessModel.Models;

/// <summary>
/// Права пользователя на защищаемый объект
/// </summary>
public class Permissions
{
    /// <summary>
    /// Целочисленный идентификатор прав
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Право на чтение
    /// </summary>
    public bool Read { get; set; }
    
    /// <summary>
    /// Право на запись
    /// </summary>
    public bool Write { get; set; }
    
    /// <summary>
    /// Право на передачу разрешений другому пользователю
    /// </summary>
    public bool TakeGrant { get; set; }
}