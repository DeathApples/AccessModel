using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

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
    }
}