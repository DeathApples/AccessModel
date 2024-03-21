using AccessModel.Models;

namespace AccessModel.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";

    public MainWindowViewModel()
    {
        AccessModelContext context = new();
    }
}