using System.Collections.ObjectModel;
using System.Text;
using Client.Extensions;
using Client.Extensions.Popup;
using Client.Models;
using Client.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Shared.Dtos;
using Tonisoft.AspExtensions.Response;

namespace Client.ViewModels;

class CreateGroupViewModel : NavigationViewModel
{
    private readonly IGroupService _service;

    private string _description = "";

    private ObservableCollection<SwatchSet> _swatches = null!;

    public CreateGroupViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IGroupService service)
        : base(eventAggregator)
    {
        _service = service;

        Swatches = SwatchHelper.InitSwatches();
        SwatchHeaders = SwatchHelper.InitSwatchHeaders();

        CreateCommand = new DelegateCommand(CreateGroup);
        ResetCommand = new DelegateCommand(ResetGroup);
    }

    public ObservableCollection<SwatchSet> Swatches {
        get => _swatches;
        set => SetProperty(ref _swatches, value);
    }

    public IEnumerable<SwatchItem> SwatchHeaders { get; }

    public DelegateCommand ResetCommand { get; }
    public DelegateCommand CreateCommand { get; }

    public string Description {
        get => _description;
        set => SetProperty(ref _description, value);
    }


    private void ResetGroup()
    {
        Swatches = SwatchHelper.InitSwatches();
    }

    private async void CreateGroup()
    {
        if (string.IsNullOrEmpty(Description))
        {
            return;
        }

        string matrix = SwatchHelper.GetSwatchString(Swatches.SelectMany(s => s.Items));
        GroupDto? group = await CreateGroupImpl(Description, matrix);
        if (group != null)
        {
            ResetGroup();
            Description = "";
            PopupManager.Success(Texts.GroupCreated);
        }
    }

    private async Task<GroupDto?> CreateGroupImpl(string description, string matrix)
    {
        try
        {
            ApiResponse<GroupDto> response = await _service.CreateGroupAsync(description, matrix);
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

        return null;
    }
}