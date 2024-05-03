using ReactiveUI;
using AccessModel.Models;

namespace AccessModel.ViewModels;

public class ConfirmationViewModel : ViewModelBase
{
    private string _message = string.Empty;
    public string Message
    {
        get => _message;
        set => this.RaiseAndSetIfChanged(ref _message, value);
    }

    private bool _isWithCancel;
    public bool IsWithCancel
    {
        get => _isWithCancel;
        set => this.RaiseAndSetIfChanged(ref _isWithCancel, value);
    }

    public ReactiveCommand<string, object> CloseCommand { get; }
    private object Close(string result)
    {
        var value = result switch {
            "yes" => ConfirmationResult.Yes,
            "no" => ConfirmationResult.No,
            "cancel" => ConfirmationResult.Cancel,
            _ => ConfirmationResult.Cancel
        };
        return value;
    }
    
    public ConfirmationViewModel()
    {
        CloseCommand = ReactiveCommand.Create<string, object>(Close);
    }
}