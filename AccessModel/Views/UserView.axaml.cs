using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using AccessModel.Models;
using AccessModel.ViewModels;

namespace AccessModel.Views;

public partial class UserView : UserControl
{
    public UserView()
    {
        InitializeComponent();
    }
    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        if (Design.IsDesignMode) return;
        AttachedToVisualTree += (_, _) => { (DataContext as UserViewModel)!.ConfirmationDialog.RegisterHandler(DoShowDialogAsync); };
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
}