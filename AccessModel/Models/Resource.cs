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
    /// Дата и время создания объекта
    /// </summary>
    public DateTime CreateDateTime { get; }

    /// <summary>
    /// Базовый конструктор класса Ресурс
    /// </summary>
    public Resource()
    {
        /* ToDo: Реализовать базовый конструктор класса Ресурс
         * Инициализировать свойства класса
         */
    }

    /// <summary>
    /// Конструктор класса Ресурс
    /// </summary>
    /// <param name="name"> Название объекта </param>
    public Resource(string name)
    {
        /* ToDo: Реализовать конструктор класса Ресурс
         * Инициализировать свойства класса
         */
    }
}