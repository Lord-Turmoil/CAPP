﻿// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

using System.Collections.ObjectModel;
using Client.Extensions;
using Client.Models;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;

namespace Client.ViewModels;

class MainViewModel : BindableBase
{
    private readonly IContainerProvider _containerProvider;
    private readonly IRegionManager _regionManager;
    private ObservableCollection<MenuTab> _menuTabs = null!;

    public MainViewModel(IContainerProvider containerProvider, IRegionManager regionManager)
    {
        _containerProvider = containerProvider;
        _regionManager = regionManager;

        MenuTabs = InitMenuTabs();

        NavigateCommand = new DelegateCommand<MenuTab>(Navigate);
    }

    public ObservableCollection<MenuTab> MenuTabs {
        set {
            _menuTabs = value;
            RaisePropertyChanged();
        }
        get => _menuTabs;
    }

    public DelegateCommand<MenuTab> NavigateCommand { get; }
    public DelegateCommand LoadedCommand => new(() => { NavigateCommand.Execute(MenuTabs[0]); });


    private static ObservableCollection<MenuTab> InitMenuTabs()
    {
        return new ObservableCollection<MenuTab> {
            new(Texts.Tab0, Texts.Namespace0, Texts.Description0),
            new(Texts.Tab1, Texts.Namespace1, Texts.Description1),
            new(Texts.Tab2, Texts.Namespace2, Texts.Description2),
            new(Texts.Tab3, Texts.Namespace3, Texts.Description3),
            new(Texts.Tab4, Texts.Namespace4, Texts.Description4)
        };
    }

    private void Navigate(MenuTab tab)
    {
        if (string.IsNullOrWhiteSpace(tab.NameSpace))
        {
            return;
        }

        if (_regionManager.Regions.ContainsRegionWithName(PrismManager.MainRegionName))
        {
            _regionManager.Regions[PrismManager.MainRegionName].RequestNavigate(tab.NameSpace);
        }
    }
}