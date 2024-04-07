using System;
using System.Globalization;
using AccessModel.Models;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace AccessModel.Converters;

public class PermissionsConverter : IValueConverter
{
    private Permissions _currentPermissions = new();
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Permissions permissions || parameter is not string targetPermission || !targetType.IsAssignableTo(typeof(bool?)))
            return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);

        _currentPermissions = new Permissions
            { Read = permissions.Read, Write = permissions.Write, TakeGrant = permissions.TakeGrant };
        
        return targetPermission switch
        {
            "read" => permissions.Read,
            "write" => permissions.Write,
            "tg" => permissions.TakeGrant,
            _ => new BindingNotification(new InvalidCastException(), BindingErrorType.Error)
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not bool permission || parameter is not string targetPermission || !targetType.IsAssignableTo(typeof(Permissions)))
            return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
        
        switch (targetPermission)
        {
            case "read": _currentPermissions.Read = permission; break;
            case "write": _currentPermissions.Write = permission; break;
            case "tg": _currentPermissions.TakeGrant = permission; break;
            default : return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
        }

        return _currentPermissions;
    }
}