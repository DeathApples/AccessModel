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
    public Guid Id { get; set; }
    
    /// <summary>
    /// Название объекта
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Содержимое объекта
    /// </summary>
    public string Content { get; set; }
    
    /// <summary>
    /// Владелец объекта
    /// </summary>
    public User Owner { get; set; }
    
    /// <summary>
    /// Дата и время создания объекта
    /// </summary>
    public DateTime CreateDateTime { get; set; }

    /// <summary>
    /// Базовый конструктор класса Ресурс
    /// </summary>
    public Resource()
    {
        Name = string.Empty;
        Content = string.Empty;
        Owner = new User();
        CreateDateTime = DateTime.Now;
    }
}