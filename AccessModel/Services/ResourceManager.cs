using System;
using System.Collections.Generic;
using AccessModel.Models;

namespace AccessModel.Services;

/// <summary>
/// Менеджер ресурсов, управляющий защищаемыми объектами системы
/// </summary>
public static class ResourceManager
{
    /// <summary>
    /// Возвращает список ресурсов, на которые есть какие-либо права у данного пользователя
    /// </summary>
    /// <param name="user"> Активный пользователь </param>
    /// <returns> Список защищаемых объектов </returns>
    public static List<Resource> GetObjects(User user)
    {
        
        
        throw new NotImplementedException();
    }

    /// <summary>
    /// Чтение содержания защищаемого объекта
    /// </summary>
    /// <param name="resource"> Ресурс, содержание которого было запрошено </param>
    /// <param name="user"> Пользователь, запросивший содержание </param>
    /// <returns> Содержание защищаемого объекта </returns>
    public static string ReadObject(Resource resource, User user)
    {
        
        
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Создание защищаемого объекта
    /// </summary>
    /// <param name="user"> Пользователь, создавший ресурс </param>
    /// <param name="name"> Название объекта </param>
    /// <returns> Успешность выполнения операции </returns>
    public static bool CreateObject(User user, string name = "")
    {
        
        
        throw new NotImplementedException();
    }

    /// <summary>
    /// Переименование защищаемого объекта
    /// </summary>
    /// <param name="resource"> Ссылка на объект, над которым выполняется операция </param>
    /// <param name="user"> Пользователь, пытающийся выполнить данную операцию </param>
    /// <param name="name"> Новое название объекта </param>
    /// <returns> Успешность выполнения операции </returns>
    public static bool RenameObject(Resource resource, User user, string name)
    {
        
        
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Изменение содержание объекта
    /// </summary>
    /// <param name="resource"> Ссылка на защищаемый объект </param>
    /// <param name="user"> Пользователь, пытающий модифицировать содержимое объекта </param>
    /// <param name="text"> Новое содержание защищаемого объекта </param>
    /// <returns> Успешность выполнения операции </returns>
    public static bool ModifyObject(Resource resource, User user, string text)
    {
        
        
        throw new NotImplementedException();
    }

    /// <summary>
    /// Удаление защищаемого объекта
    /// </summary>
    /// <param name="resource"> Ссылка на объект, над которым выполняется операция </param>
    /// <param name="user"> Пользователь, пытающийся выполнить данную операцию </param>
    /// <returns> Успешность выполнения операции </returns>
    public static bool DeleteObject(Resource resource, User user)
    {
        
        
        throw new NotImplementedException();
    }
}