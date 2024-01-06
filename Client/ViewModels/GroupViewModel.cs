using System.Collections.ObjectModel;
using Prism.Events;
using Prism.Regions;
using Shared.Dtos;

namespace Client.ViewModels;

class GroupViewModel : NavigationViewModel
{
    private ObservableCollection<GroupDto> _allGroups = null!;

    public GroupViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        : base(eventAggregator)
    {
        AllGroups = GetAllGroups();
    }

    public ObservableCollection<GroupDto> AllGroups {
        get => _allGroups;
        set => SetProperty(ref _allGroups, value);
    }

    public GroupDto? SelectedPart { get; set; }

    private ObservableCollection<GroupDto> GetAllGroups()
    {
        ObservableCollection<GroupDto> groups = new() {
            new GroupDto { Id = 1, Description = "Group 1" },
            new GroupDto { Id = 2, Description = "Group 2" },
            new GroupDto { Id = 3, Description = "Group 3" },
            new GroupDto { Id = 4, Description = "Group 4" }
        };

        return groups;
    }
}