using System.Collections.ObjectModel;
using Client.Extensions;
using Client.Extensions.Popup;
using Client.Models;
using Client.Services;
using Prism.Commands;
using Prism.Events;
using Server.Modules.Core.Dtos;
using Shared.Common;
using Shared.Dtos;
using Tonisoft.AspExtensions.Response;

namespace Client.ViewModels;

class DeriveViewModel : NavigationViewModel
{
    private readonly IGroupService _service;

    private ObservableCollection<GroupDto> _allGroups = null!;

    private ObservableCollection<PartDto> _allParts = null!;

    private ObservableCollection<ProcedureDto> _allProcedures = null!;

    private GroupDto? _selectedGroup;

    private ObservableCollection<SwatchSet> _swatches = null!;

    public DeriveViewModel(IEventAggregator eventAggregator, IGroupService service) : base(eventAggregator)
    {
        _service = service;
        SwatchHeaders = SwatchHelper.InitSwatchHeaders();
        Swatches = SwatchHelper.InitSwatches();

        SearchCommand = new DelegateCommand(SearchGroups);
    }

    public ObservableCollection<GroupDto> AllGroups {
        get => _allGroups;
        set => SetProperty(ref _allGroups, value);
    }

    public GroupDto? SelectedGroup {
        get => _selectedGroup;
        set {
            if (SetProperty(ref _selectedGroup, value))
            {
                OnSelectedGroupChanged(value);
            }
        }
    }

    public ObservableCollection<ProcedureDto> AllProcedures {
        get => _allProcedures;
        set => SetProperty(ref _allProcedures, value);
    }

    public ObservableCollection<PartDto> AllParts {
        get => _allParts;
        set => SetProperty(ref _allParts, value);
    }

    public ObservableCollection<SwatchSet> Swatches {
        get => _swatches;
        set => SetProperty(ref _swatches, value);
    }

    public IEnumerable<SwatchItem> SwatchHeaders { get; }

    private string _query = null!;

    public string Query {
        get => _query;
        set {
            int opitz = GtCore.VerifyOpitz(value);
            SetProperty(ref _query, opitz == 0 ? "" : value);
        }
    }

    public DelegateCommand SearchCommand { get; }

    private async void SearchGroups()
    {
        if (string.IsNullOrEmpty(Query))
        {
            return;
        }

        IEnumerable<GroupDto> groups = await SearchGroupsImpl(Query);
        AllGroups = new ObservableCollection<GroupDto>(groups);
        SelectedGroup = null;
    }

    private void OnSelectedGroupChanged(GroupDto? value)
    {
        if (value == null)
        {
            return;
        }

        AllProcedures = new ObservableCollection<ProcedureDto>(value.Procedures);
        AllParts = new ObservableCollection<PartDto>(value.Parts);
        Swatches = SwatchHelper.InitSwatches(value.Matrix);
    }

    private async Task<IEnumerable<GroupDto>> SearchGroupsImpl(string opitz)
    {
        try
        {
            ApiResponse<List<GroupDto>> response = await _service.SearchGroupsAsync(opitz);
            if (response.Status)
            {
                return response.Result!;
            }

            PopupManager.ShowInvalidResponse(response);
        }
        catch (Exception e)
        {
            PopupManager.ShowNetworkError(e);
        }

        return new List<GroupDto>();
    }
}