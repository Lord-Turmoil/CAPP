﻿// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

using System.Collections.ObjectModel;
using Client.Extensions;
using Client.Extensions.Popup;
using Client.Models;
using Client.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Server.Modules.Core.Dtos;
using Shared.Dtos;
using Tonisoft.AspExtensions.Response;

namespace Client.ViewModels;

class GroupViewModel : NavigationViewModel
{
    private readonly IGroupService _groupService;
    private readonly IProcedureService _procedureService;

    private ObservableCollection<GroupDto> _allGroups = null!;
    private ObservableCollection<ProcedureDto> _allProcedures = null!;
    private string _description = "";

    private ObservableCollection<PartDto> _partsInGroup = null!;
    private GroupDto? _selectedGroup;

    private ProcedureDto? _selectedProcedure;

    private ObservableCollection<SwatchSet> _swatches = null!;

    public GroupViewModel(IEventAggregator eventAggregator, IGroupService groupService,
        IProcedureService procedureService)
        : base(eventAggregator)
    {
        _groupService = groupService;
        _procedureService = procedureService;

        AllGroups = new ObservableCollection<GroupDto>();

        SwatchHeaders = SwatchHelper.InitSwatchHeaders();
        Swatches = SwatchHelper.InitSwatches();

        CreateProcedureCommand = new DelegateCommand(CreateProcedure);
        UpdateProcedureCommand = new DelegateCommand(UpdateProcedure);
        DeleteSelectedProcedureCommand = new DelegateCommand(DeleteProcedure);
        DeleteSelectedGroupCommand = new DelegateCommand(DeleteGroup);
    }

    public string Description {
        get => _description;
        set => SetProperty(ref _description, value);
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

    public ProcedureDto? SelectedProcedure {
        get => _selectedProcedure;
        set {
            if (SetProperty(ref _selectedProcedure, value))
            {
                if (value != null)
                {
                    Description = value.Description;
                }
            }
        }
    }

    public ObservableCollection<PartDto> PartsInGroup {
        get => _partsInGroup;
        set => SetProperty(ref _partsInGroup, value);
    }

    public ObservableCollection<SwatchSet> Swatches {
        get => _swatches;
        set => SetProperty(ref _swatches, value);
    }

    public IEnumerable<SwatchItem> SwatchHeaders { get; }

    public DelegateCommand CreateProcedureCommand { get; }
    public DelegateCommand UpdateProcedureCommand { get; }
    public DelegateCommand DeleteSelectedProcedureCommand { get; }
    public DelegateCommand DeleteSelectedGroupCommand { get; }

    public override void OnNavigatedTo(NavigationContext navigationContext)
    {
        base.OnNavigatedTo(navigationContext);
        FetchAllGroups();
    }

    private void OnSelectedGroupChanged(GroupDto? value)
    {
        if (value == null)
        {
            return;
        }

        AllProcedures = new ObservableCollection<ProcedureDto>(value.Procedures);
        PartsInGroup = new ObservableCollection<PartDto>(value.Parts);
        Swatches = SwatchHelper.InitSwatches(value.Matrix);
    }

    private async void CreateProcedure()
    {
        if (string.IsNullOrEmpty(Description))
        {
            return;
        }

        if (SelectedGroup == null)
        {
            return;
        }

        ProcedureDto? procedure =
            await CreateProcedureImpl(SelectedGroup.Id, Description, SelectedGroup.Procedures.Count());
        if (procedure != null)
        {
            AllProcedures.Add(procedure);
            SelectedGroup.Procedures = AllProcedures;
            Description = "";
            PopupManager.Success(Texts.ProcedureCreated);
        }
    }

    private async void UpdateProcedure()
    {
        if (SelectedProcedure == null)
        {
            return;
        }

        string description = string.IsNullOrWhiteSpace(Description) ? SelectedProcedure.Description : Description;
        ProcedureDto? procedure = await UpdateProcedureImpl(SelectedProcedure.Id, description);
        if (procedure != null)
        {
            int index = AllProcedures.IndexOf(SelectedProcedure);
            AllProcedures.RemoveAt(index);
            AllProcedures.Insert(index, procedure);
            PopupManager.Success(Texts.ProcedureUpdated);
        }
    }

    private void DeleteProcedure()
    {
        if (SelectedProcedure == null)
        {
            return;
        }

        ProcedureDto? procedure = AllProcedures.FirstOrDefault(x => x.Id == SelectedProcedure.Id);
        if (procedure != null)
        {
            DeleteProcedureImpl(procedure.Id);
            AllProcedures.Remove(procedure);
            Description = "";
            PopupManager.Success(Texts.ProcedureDeleted);
        }
    }

    private void DeleteGroup()
    {
        if (SelectedGroup == null)
        {
            return;
        }

        GroupDto? group = AllGroups.FirstOrDefault(x => x.Id == SelectedGroup.Id);
        if (group != null)
        {
            DeleteGroupImpl(group.Id);
            AllGroups.Remove(group);
            AllProcedures.Clear();
        }
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

    private async Task<ProcedureDto?> CreateProcedureImpl(int groupId, string description, int order)
    {
        try
        {
            ApiResponse<ProcedureDto> response =
                await _procedureService.CreateProcedureAsync(groupId, description, order);
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

    private async Task<ProcedureDto?> UpdateProcedureImpl(int id, string description)
    {
        try
        {
            ApiResponse<ProcedureDto> response = await _procedureService.UpdateProcedureAsync(id, description);
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

    private async void DeleteProcedureImpl(int id)
    {
        try
        {
            ApiResponse response = await _procedureService.DeleteProcedureAsync(id);
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

    public async void DeleteGroupImpl(int id)
    {
        try
        {
            ApiResponse response = await _groupService.DeleteGroupAsync(id);
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