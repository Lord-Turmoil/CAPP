// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

using System.Collections.ObjectModel;
using System.Windows;
using Client.Extensions.Popup;
using Client.Services;
using ImTools;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Shared.Common;
using Shared.Dtos;
using Tonisoft.AspExtensions.Response;

namespace Client.ViewModels;

class PartsViewModel : NavigationViewModel
{
    private readonly IPartService _service;
    private ObservableCollection<PartDto> _allParts = null!;
    private string _name = "";
    private string _opitz = "";

    private PartDto? _selectedPart;

    public PartsViewModel(IEventAggregator eventAggregator, IPartService service)
        : base(eventAggregator)
    {
        _service = service;

        CreatePartCommand = new DelegateCommand(CreatePart);
        UpdatePartCommand = new DelegateCommand(UpdatePart);
        DeleteSelectedCommand = new DelegateCommand(DeleteSelectedPart);

        AllParts = new ObservableCollection<PartDto>();
    }

    public string Name {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public string Opitz {
        get => _opitz;
        set => SetProperty(ref _opitz, GtCore.VerifyOpitz(value) ?? "");
    }

    public ObservableCollection<PartDto> AllParts {
        get => _allParts;
        set => SetProperty(ref _allParts, value);
    }

    public PartDto? SelectedPart {
        get => _selectedPart;
        set {
            if (SetProperty(ref _selectedPart, value))
            {
                OnSelectedPartChange(value);
            }
        }
    }

    public DelegateCommand CreatePartCommand { get; }
    public DelegateCommand UpdatePartCommand { get; }
    public DelegateCommand DeleteSelectedCommand { get; }


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

    private async void UpdatePart()
    {
        if (SelectedPart == null)
        {
            return;
        }

        string name = string.IsNullOrWhiteSpace(Name) ? SelectedPart.Name : Name;
        string opitz = string.IsNullOrWhiteSpace(Opitz) ? SelectedPart.Opitz : Opitz;

        PartDto? part = await UpdatePartImpl(SelectedPart.Id, name, opitz);
        if (part != null)
        {
            int index = AllParts.IndexOf(SelectedPart);
            AllParts.RemoveAt(index);
            AllParts.Insert(index, part);
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

    private void OnSelectedPartChange(PartDto? value)
    {
        if (value != null)
        {
            Name = value.Name;
            Opitz = value.Opitz;
        }
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

    private async Task<PartDto?> UpdatePartImpl(int id, string name, string opitz)
    {
        try
        {
            ApiResponse<PartDto> response = await _service.UpdatePartAsync(id, name, opitz);
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