using System;

namespace AccessModel.Models;

/// <summary>
/// Защищаемый объект (ресурс)
/// </summary>
public class Resource
{
    /// <summary>
    /// Псевдоуникальный идентификатор объекта
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Название объекта
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Дата и время создания объекта
    /// </summary>
    public DateTime CreateDateTime { get; set; }
    
    /// <summary>
    /// Содержимое файла
    /// </summary>
    public string? Content { get; set; }
}