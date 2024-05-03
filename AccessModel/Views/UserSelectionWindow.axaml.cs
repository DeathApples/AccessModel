using System;
using AccessModel.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace AccessModel.Views;

public partial class UserSelectionWindow : ReactiveWindow<UserSelectionViewModel>
{
    public UserSelectionWindow()
    {
        InitializeComponent();
        
        if (Design.IsDesignMode) return;
        this.WhenActivated(action => action(ViewModel!.CloseCommand.Subscribe(Close)));
    }
}