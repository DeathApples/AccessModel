using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AccessModel.ViewModels;
using AccessModel.Views;

namespace AccessModel;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var authenticationWindow = new AuthenticationWindow {
                DataContext = new AuthenticationViewModel()
            };
            
            desktop.MainWindow = authenticationWindow;
            
            AuthenticationViewModel.CloseCommand.Subscribe(_ =>
            {
                var mainWindow = new MainWindow {
                    DataContext = new MainWindowViewModel()
                };
                desktop.MainWindow = mainWindow;
                mainWindow.Show();
            });
            
            MainWindowViewModel.CloseCommand.Subscribe(_ =>
            {
                var authWindow = new AuthenticationWindow {
                    DataContext = new AuthenticationViewModel()
                };
                desktop.MainWindow = authWindow;
                authWindow.Show();
            });
        }
        
        base.OnFrameworkInitializationCompleted();
    }
}