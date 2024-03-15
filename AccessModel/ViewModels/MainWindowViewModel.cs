using System.Collections.Generic;
using System.Collections.ObjectModel;
using AccessModel.Models;

namespace AccessModel.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<Resource> ResourceList { get; set; }

    public MainWindowViewModel()
    {
        ResourceList = new ObservableCollection<Resource>(new List<Resource> {
            new Resource() { Name = "01" },
            new Resource() { Name = "02" },
            new Resource() { Name = "03" },
            new Resource() { Name = "04" },
            new Resource() { Name = "05" },
            new Resource() { Name = "06" },
            new Resource() { Name = "07" },
            new Resource() { Name = "08" },
            new Resource() { Name = "09" },
            new Resource() { Name = "10" },
            new Resource() { Name = "11" },
            new Resource() { Name = "12" },
            new Resource() { Name = "13" },
            new Resource() { Name = "14" },
            new Resource() { Name = "15" },
            new Resource() { Name = "16" },
            new Resource() { Name = "17" },
            new Resource() { Name = "18" },
            new Resource() { Name = "19" },
            new Resource() { Name = "20" },
            new Resource() { Name = "21" },
            new Resource() { Name = "22" },
            new Resource() { Name = "23" },
            new Resource() { Name = "24" }
        });
    }
}