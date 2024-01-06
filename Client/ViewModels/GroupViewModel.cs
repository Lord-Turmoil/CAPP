using System.Collections.ObjectModel;
using System.Windows;
using Client.Extensions.Popup;
using Client.Services;
using Prism.Events;
using Prism.Regions;
using Shared.Dtos;
using Tonisoft.AspExtensions.Response;

namespace Client.ViewModels;

class GroupViewModel : NavigationViewModel
{
    private ObservableCollection<GroupDto> _allGroups = null!;
    private readonly IGroupService _groupService;
    private readonly IProcedureService _procedureService;

    public GroupViewModel(IEventAggregator eventAggregator, IGroupService groupService, IProcedureService procedureService)
        : base(eventAggregator)
    {
        _groupService = groupService;
        _procedureService = procedureService;

        AllGroups = new ObservableCollection<GroupDto>();
    }

    public ObservableCollection<GroupDto> AllGroups {
        get => _allGroups;
        set => SetProperty(ref _allGroups, value);
    }

    public GroupDto? SelectedPart { get; set; }


    public override void OnNavigatedTo(NavigationContext navigationContext)
    {
        base.OnNavigatedTo(navigationContext);
        FetchAllGroups();
    }

    private async void FetchAllGroups()
    {
        try
        {
            ApiResponse<List<GroupDto>> response = await _groupService.GetGroupsAsync();
            if (response.Status)
            {
                AllGroups = new ObservableCollection<GroupDto>(response.Result!);
            }
            else
            {
                PopupManager.ShowInvalidResponse(response);
            }
        }
        catch (Exception e)
        {
            PopupManager.ShowNetworkError(e);
        }
    }
}