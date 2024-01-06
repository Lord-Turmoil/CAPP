// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

using System.Collections.ObjectModel;
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

        AllGroups = new ObservableCollection<GroupDto>();

        CreateCommand = new DelegateCommand(CreateGroup);
        UpdateCommand = new DelegateCommand(UpdateGroup);
        ResetCommand = new DelegateCommand(ResetGroup);
        DeleteCommand = new DelegateCommand(DeleteGroup);
    }

    private ObservableCollection<GroupDto> _allGroups = null!;

    public ObservableCollection<GroupDto> AllGroups {
        get => _allGroups;
        set => SetProperty(ref _allGroups, value);
    }

    private GroupDto? _selectedGroup;

    public GroupDto? SelectedGroup {
        get => _selectedGroup;
        set {
            SetProperty(ref _selectedGroup, value);
            if (value != null)
            {
                Swatches = SwatchHelper.InitSwatches(value.Matrix);
                Description = value.Description;
            }
        }
    }

    public ObservableCollection<SwatchSet> Swatches {
        get => _swatches;
        set => SetProperty(ref _swatches, value);
    }

    public IEnumerable<SwatchItem> SwatchHeaders { get; }

    public DelegateCommand CreateCommand { get; }
    public DelegateCommand UpdateCommand { get; }
    public DelegateCommand ResetCommand { get; }
    public DelegateCommand DeleteCommand { get; }

    public string Description {
        get => _description;
        set => SetProperty(ref _description, value);
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
            AllGroups.Add(group);
            Description = "";
            PopupManager.Success(Texts.GroupCreated);
        }
    }

    private async void UpdateGroup()
    {
        if (SelectedGroup == null)
        {
            return;
        }

        string description = string.IsNullOrEmpty(Description) ? SelectedGroup.Description : Description;
        string matrix = SwatchHelper.GetSwatchString(Swatches.SelectMany(s => s.Items));

        GroupDto? group = await UpdateGroupImpl(SelectedGroup.Id, description, matrix);
        if (group != null)
        {
            int index = AllGroups.IndexOf(SelectedGroup);
            AllGroups.RemoveAt(index);
            AllGroups.Insert(index, group);
        }
    }

    private void ResetGroup()
    {
        Swatches = SwatchHelper.InitSwatches();
        SelectedGroup = null;
    }

    private async void DeleteGroup()
    {
        if (SelectedGroup == null)
        {
            return;
        }

        await DeleteGroupImpl(SelectedGroup.Id);
        AllGroups.Remove(SelectedGroup);
        ResetGroup();
        Description = "";
    }

    public override void OnNavigatedTo(NavigationContext navigationContext)
    {
        base.OnNavigatedTo(navigationContext);
        FetchAllGroups();
    }

    private async void FetchAllGroups()
    {
        try
        {
            ApiResponse<List<GroupDto>> response = await _service.GetGroupsAsync();
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

    private async Task<GroupDto?> UpdateGroupImpl(int id, string description, string matrix)
    {
        try
        {
            ApiResponse<GroupDto> response = await _service.UpdateGroupAsync(id, description, matrix);
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

    public async Task DeleteGroupImpl(int id)
    {
        try
        {
            ApiResponse response = await _service.DeleteGroupAsync(id);
            if (response.Status)
            {
                return;
            }

            PopupManager.ShowInvalidResponse(response);
        }
        catch (Exception e)
        {
            PopupManager.ShowNetworkError(e);
        }
    }
}