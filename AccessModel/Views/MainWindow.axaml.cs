using System;
using ReactiveUI;
using Avalonia.ReactiveUI;
using System.Threading.Tasks;
using AccessModel.Models;
using AccessModel.ViewModels;
using Avalonia.Controls;

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