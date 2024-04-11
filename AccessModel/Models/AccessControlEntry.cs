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
    public Permissions Permissions { get; set; }

    /// <summary>
    /// Базовый конструктор класса Записи Контроля Доступа
    /// </summary>
    public AccessControlEntry()
    {
        User = new User();
        Resource = new Resource();
        Permissions = new Permissions();
    }
}