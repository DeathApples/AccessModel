using System.Threading.Tasks;
using AccessModel.Models;
using AccessModel.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace AccessModel.Views;

public partial class ResourceView : UserControl
{
    public ResourceView()
    {
        InitializeComponent();
    }
    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        var window = (ReactiveWindow<MainWindowViewModel>)(Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow!;
        window.WhenActivated(action => action((DataContext as ResourceViewModel)!.ConfirmationDialog.RegisterHandler(DoShowDialogAsync)));
        window.WhenActivated(action => action((DataContext as ResourceViewModel)!.UserSelectionDialog.RegisterHandler(DoShowDialogAsync)));
    }
    
    private async Task DoShowDialogAsync(InteractionContext<ConfirmationViewModel, ConfirmationResult> interaction)
    {
        var dialog = new ConfirmationWindow {
            DataContext = interaction.Input
        };
        
        var window = (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
        IsEnabled = false;
        
        var result = await dialog.ShowDialog<ConfirmationResult>(window);
        interaction.SetOutput(result);
        IsEnabled = true;
    }
    
    private async Task DoShowDialogAsync(InteractionContext<UserSelectionViewModel, User?> interaction)
    {
        var dialog = new UserSelectionWindow() {
            DataContext = interaction.Input
        };
        
        var window = (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
        IsEnabled = false;
        
        var result = await dialog.ShowDialog<User?>(window);
        interaction.SetOutput(result);
        IsEnabled = true;
    }
}