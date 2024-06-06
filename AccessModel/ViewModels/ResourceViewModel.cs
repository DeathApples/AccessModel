using System;
using System.Linq;
using System.Reactive;
using System.Globalization;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ReactiveUI;
using DynamicData;
using AccessModel.Models;
using AccessModel.Services;

namespace AccessModel.ViewModels;

public class ResourceViewModel : ViewModelBase
{
    public static event Action<string>? LogEvent;
    
    private ObservableCollection<Resource> _resources;
    public ObservableCollection<Resource> Resources
    {
        get => _resources;
        set => this.RaiseAndSetIfChanged(ref _resources, value);
    }
    
    private ObservableCollection<DeletionRequest> _requests;
    public ObservableCollection<DeletionRequest> Requests
    {
        get => _requests;
        set => this.RaiseAndSetIfChanged(ref _requests, value);
    }
    
    private Resource? _currentResource;
    public Resource? CurrentResource
    {
        get => _currentResource;
        set => this.RaiseAndSetIfChanged(ref _currentResource, value);
    }

    private DeletionRequest? _currentRequest;
    public DeletionRequest? CurrentRequest
    {
        get => _currentRequest;
        set => this.RaiseAndSetIfChanged(ref _currentRequest, value);
    }

    public bool IsRead => 
        UserManager.CurrentUser?.SecurityClearance <= CurrentResource?.SecurityClassification;
    
    public bool IsWrite => 
        UserManager.CurrentUser?.SecurityClearance >= CurrentResource?.SecurityClassification;

    public void CreateResource()
    {
        LogEvent?.Invoke("Создан новый документ");
        ResourceManager.CreateResource();
        UpdateResources();
    }

    public void ModifySecurityClassification()
    {
        if (CurrentResource is null) return;
        
        var resource = ResourceManager.GetResource(CurrentResource.Id);
        if (resource is null || resource.SecurityClassification == CurrentResource.SecurityClassification) return;
        
        LogEvent?.Invoke("Метка конфиденциальности у выбранного документа успешно изменена");
        ResourceManager.ModifyResource(CurrentResource);
        ChangeEditMode();
    }
    
    public void ModifyContent()
    {
        if (CurrentResource is null) return;
        
        var resource = ResourceManager.GetResource(CurrentResource.Id);
        if (resource is null || resource.Content == CurrentResource.Content) { return; }
        
        if (IsWrite && !IsRead) { resource.Content += $"\n{CurrentResource.Content}"; }
        if (IsWrite && IsRead) { resource.Content = CurrentResource.Content; }

        CurrentResource.Content = resource.Content;
        
        LogEvent?.Invoke("Содержание документа успешно изменено");
        ResourceManager.ModifyResource(CurrentResource);
        UpdateResources();
    }
    
    public ReactiveCommand<Unit, Unit> DeleteResourceCommand { get; }
    private async Task DeleteResource()
    {
        if (CurrentResource is null) {
            LogEvent?.Invoke("Ошибка удаления документа: файл не выбран");
            return;
        }
        
        const string message = "Вы действительно хотите удалить \n выбранный документ?";
        var result = await Confirmation(message);
        
        if (result == ConfirmationResult.Yes) {
            if (UserManager.CurrentUser is not null && UserManager.CurrentUser.IsAdmin) {
                LogEvent?.Invoke("Запрос на удаление документа отправлен Администратору Системы");
                RequestManager.CreateRequest(CurrentResource);
            } else if (CurrentRequest is not null) {
                ResourceManager.DeleteResource(CurrentRequest.Resource);
                LogEvent?.Invoke("Документ успешно удалён");
            }
            
            UpdateResources();
        }
    }
    
    public void CancelRequest()
    {
        if (CurrentRequest is null) {
            LogEvent?.Invoke("Ошибка удаления запроса: запрос не выбран");
            return;
        }
        
        LogEvent?.Invoke("Запрос на удаление документа был отклонён");
        RequestManager.DeleteRequest(CurrentRequest);
        UpdateResources();
    }

    private void UpdateResources()
    {
        Resources.Clear(); Requests.Clear();
        Resources.AddRange(ResourceManager.GetAllResources());
        CurrentResource = Resources.FirstOrDefault() ?? new Resource();
        Requests.AddRange(RequestManager.GetAllRequests());
        CurrentRequest = Requests.FirstOrDefault() ?? new DeletionRequest();
    }
    
    private bool _isEditMode;
    public bool IsEditMode
    {
        get => _isEditMode;
        set => this.RaiseAndSetIfChanged(ref _isEditMode, value);
    }
    
    public void ChangeEditMode()
    {
        if (IsEditMode) UpdateResources();
        IsEditMode = !IsEditMode;
    }
    
    private async Task<ConfirmationResult> Confirmation(string message)
    {
        var confirmation = new ConfirmationViewModel {
            Message = message
        };
        
        return await ConfirmationDialog.Handle(confirmation);
    }
    
    public Interaction<ConfirmationViewModel, ConfirmationResult> ConfirmationDialog { get; }

    public ResourceViewModel()
    {
        ConfirmationDialog = new Interaction<ConfirmationViewModel, ConfirmationResult>();
        DeleteResourceCommand = ReactiveCommand.CreateFromTask(DeleteResource);
        
        _resources = new ObservableCollection<Resource>(
            ResourceManager.GetAllResources()
        );
        
        _requests = new ObservableCollection<DeletionRequest>(
            RequestManager.GetAllRequests()
        );
        
        _currentResource = _resources.FirstOrDefault();
        _currentRequest = _requests.FirstOrDefault();
    }
}