using System;
using System.Globalization;
using AccessModel.Models;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace AccessModel.Converters;

public class ResourceConverter : IValueConverter
{
    private Resource _currentResource = new();
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Resource resource || !targetType.IsAssignableTo(typeof(string)))
            return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);

        _currentResource = new Resource
        {
            Id = resource.Id, Name = resource.Name, Content = resource.Content,
            Owner = resource.Owner, CreateDateTime = resource.CreateDateTime
        };
        
        return resource.Name;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string name || !targetType.IsAssignableTo(typeof(Resource)))
            return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);

        _currentResource.Name = name;
        
        return _currentResource;
    }
}