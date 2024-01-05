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

        CreateMenuTabs();

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


    private void CreateMenuTabs()
    {
        MenuTabs = new ObservableCollection<MenuTab> {
            new(Texts.Tab0, "IndexView", Texts.Description0),
            new(Texts.Tab1, "PartView", Texts.Description1),
            new(Texts.Tab2, "GroupView", Texts.Description2),
            new(Texts.Tab3, "GroupView", Texts.Description3)
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