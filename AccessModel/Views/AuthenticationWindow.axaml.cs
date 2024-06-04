using System;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using AccessModel.ViewModels;

namespace AccessModel.Views;

public partial class AuthenticationWindow : ReactiveWindow<AuthenticationViewModel>
{
    public AuthenticationWindow()
    {
        InitializeComponent();

        if (Design.IsDesignMode) return;
        this.WhenActivated(action => action(AuthenticationViewModel.CloseCommand.Subscribe(Close)));
    }
}