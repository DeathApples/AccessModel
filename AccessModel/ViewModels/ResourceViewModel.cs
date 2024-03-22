using System.Collections.Generic;
using System.Collections.ObjectModel;
using AccessModel.Models;
using AccessModel.Services;
using ReactiveUI;

namespace AccessModel.ViewModels;

public class ResourceViewModel : ViewModelBase
{
    public ObservableCollection<Resource>? ResourceList { get; set; }

    public ResourceViewModel()
    {
        ResourceList = new ObservableCollection<Resource>(new List<Resource> {
            new() { Name = "01" }, new() { Name = "02" },
            new() { Name = "03" }, new() { Name = "04" },
            new() { Name = "05" }, new() { Name = "06" },
            new() { Name = "07" }, new() { Name = "08" },
            new() { Name = "09" }, new() { Name = "10" },
            new() { Name = "11" }, new() { Name = "12" },
            new() { Name = "13" }, new() { Name = "14" },
            new() { Name = "15" }, new() { Name = "16" },
            new() { Name = "17" }, new() { Name = "18" },
            new() { Name = "19" }, new() { Name = "20" },
            new() { Name = "21" }, new() { Name = "22" },
            new() { Name = "23" }, new() { Name = "24" },
            new() { Name = "25" }, new() { Name = "26" },
            new() { Name = "27" }, new() { Name = "28" },
            new() { Name = "29" }, new() { Name = "30" }
        });
    }
}