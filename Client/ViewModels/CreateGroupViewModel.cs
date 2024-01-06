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

    private ObservableCollection<SwatchSet> _swatches = null!;

    public CreateGroupViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IGroupService service)
        : base(eventAggregator)
    {
        _service = service;

        Swatches = InitSwatches();
        SwatchHeaders = InitSwatchHeaders();

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

    private string _description;

    public string Description {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    private static ObservableCollection<SwatchSet> InitSwatches()
    {
        var swatches = new ObservableCollection<SwatchSet>();
        for (int i = 0; i < 10; i++)
        {
            var swatchSet = new SwatchSet { Name = $"{i}" };
            var swatchItems = new List<SwatchItem>();
            for (int j = 0; j < 9; j++)
            {
                swatchItems.Add(new SwatchItem { Id = i + j * 10 + 1 });
            }

            swatchSet.Items = swatchItems;
            swatches.Add(swatchSet);
        }

        return swatches;
    }

    private static IEnumerable<SwatchItem> InitSwatchHeaders()
    {
        var swatchHeaders = new List<SwatchItem>();
        for (int i = 0; i < 9; i++)
        {
            swatchHeaders.Add(new SwatchItem { Id = i + 1 });
        }

        return swatchHeaders;
    }

    private void ResetGroup()
    {
        Swatches = InitSwatches();
    }

    private async void CreateGroup()
    {
        if (string.IsNullOrEmpty(Description))
        {
            return;
        }

        var builder = new StringBuilder();
        foreach (var swatchSet in Swatches)
        {
            foreach (var swatchItem in swatchSet.Items)
            {
                builder.Append(swatchItem.IsChecked ? "1" : "0");
            }
        }

        GroupDto? group = await CreateGroupImpl(Description, builder.ToString());
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