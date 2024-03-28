using Microsoft.EntityFrameworkCore.Metadata;

namespace AccessModel.Models;

/// <summary>
/// Запись контроля доступа
/// </summary>
public class AccessControlEntry
{
    /// <summary>
    /// Целочисленный идентификатор записи контроля доступа
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Пользователь системы
    /// </summary>
    public User User { get; set; }
    
    /// <summary>
    /// Защищаемый объект
    /// </summary>
    public Resource Resource { get; set; }
    
    /// <summary>
    /// Разрешения, которыми обладает данный пользователь над данным объектом
    /// </summary>
    public Permission? Permission { get; set; }
}