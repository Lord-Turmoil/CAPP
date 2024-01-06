using System.Collections.ObjectModel;
using System.Windows;
using Client.Extensions.Popup;
using Client.Services;
using ImTools;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Shared.Dtos;
using Tonisoft.AspExtensions.Response;

namespace Client.ViewModels;

class PartsViewModel : NavigationViewModel
{
    private ObservableCollection<PartDto> _allParts = null!;
    private string _name = "";
    private int _opitz;
    private readonly IPartService _service;

    public PartsViewModel(IEventAggregator eventAggregator, IPartService service)
        : base(eventAggregator)
    {
        _service = service;

        CreatePartCommand = new DelegateCommand(CreatePart);
        DeleteSelectedCommand = new DelegateCommand(DeleteSelectedPart);

        AllParts = new ObservableCollection<PartDto>();
    }

    public string Name {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public string Opitz {
        get => _opitz == 0 ? "" : _opitz.ToString();
        set => SetProperty(ref _opitz, VerifyOpitz(value));
    }

    public ObservableCollection<PartDto> AllParts {
        get => _allParts;
        set => SetProperty(ref _allParts, value);
    }

    public PartDto? SelectedPart { get; set; }

    public DelegateCommand CreatePartCommand { get; }
    public DelegateCommand DeleteSelectedCommand { get; }

    private static int VerifyOpitz(string opitz)
    {
        if (opitz.Length == 9 && int.TryParse(opitz, out int value))
        {
            return value;
        }

        return 0;
    }

    private async void CreatePart()
    {
        if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrEmpty(Opitz))
        {
            return;
        }

        PartDto? part = await CreatePartImpl(Name, Opitz);
        if (part != null)
        {
            AllParts.Add(part);

            // Clear input to prevent consecutive invocation.
            Name = "";
            Opitz = "";
        }

    }

    private async void DeleteSelectedPart()
    {
        if (SelectedPart == null)
        {
            return;
        }

        PartDto? part = AllParts.FindFirst(p => p.Id == SelectedPart.Id);
        if (part != null)
        {
            await DeletePartImpl(part.Id);
            AllParts.Remove(part);
        }

        SelectedPart = null;
    }

    public override void OnNavigatedTo(NavigationContext navigationContext)
    {
        base.OnNavigatedTo(navigationContext);
        FetchAllParts();
    }

    private async void FetchAllParts()
    {
        try
        {
            ApiResponse<List<PartDto>> response = await _service.GetPartsAsync();
            if (response.Status)
            {
                AllParts = new ObservableCollection<PartDto>(response.Result!);
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

    private async Task<PartDto?> CreatePartImpl(string name, string opitz)
    {
        try
        {
            ApiResponse<PartDto> response = await _service.CreatePartAsync(name, opitz);
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

    private async Task DeletePartImpl(int id)
    {
        try
        {
            ApiResponse response = await _service.DeletePartAsync(id);
            if (!response.Status)
            {
                throw new Exception($"Invalid Response: {response.Message}");
            }
        }
        catch (Exception e)
        {
            MessageBox.Show($"Error:\n{e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}