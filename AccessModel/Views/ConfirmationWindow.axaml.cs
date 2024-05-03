using System;
using AccessModel.ViewModels;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace AccessModel.Views;

public partial class ConfirmationWindow : ReactiveWindow<ConfirmationViewModel>
{
    public ConfirmationWindow()
    {
        InitializeComponent();

        if (Design.IsDesignMode) return;
        this.WhenActivated(action => action(ViewModel!.CloseCommand.Subscribe(Close)));
    }
}