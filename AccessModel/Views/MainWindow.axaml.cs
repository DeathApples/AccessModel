using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using AccessModel.Models;
using AccessModel.ViewModels;

namespace AccessModel.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        
        if (Design.IsDesignMode) return;
        this.WhenActivated(action => action(MainWindowViewModel.CloseCommand!.Subscribe(Close)));
        this.WhenActivated(action => action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
    }
    
    private async Task DoShowDialogAsync(InteractionContext<ConfirmationViewModel, ConfirmationResult> interaction)
    {
        var dialog = new ConfirmationWindow {
            DataContext = interaction.Input
        };
        
        IsEnabled = false;
        
        var result = await dialog.ShowDialog<ConfirmationResult>(this);
        interaction.SetOutput(result);
        
        IsEnabled = true;
    }
}