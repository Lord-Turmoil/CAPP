﻿using System.Collections.ObjectModel;
using System.Windows;
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

    private void CreatePart()
    {
        if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrEmpty(Opitz))
        {
            return;
        }

        AllParts.Add(new PartDto { Id = AllParts.Count + 1, Name = Name, Opitz = Opitz });
    }

    private void DeleteSelectedPart()
    {
        if (SelectedPart == null)
        {
            return;
        }

        AllParts.FindFirst(p => p.Id == SelectedPart.Id).Do(p => AllParts.Remove(p));
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
            if (!response.Status)
            {
                throw new Exception($"Invalid Response: {response.Message}");
            }

            AllParts = new ObservableCollection<PartDto>(response.Result!.ToList());
        }
        catch (Exception e)
        {
            MessageBox.Show($"Error:\n{e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
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