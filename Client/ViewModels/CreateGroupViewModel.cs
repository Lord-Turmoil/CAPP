using System.Collections.ObjectModel;
using Client.Models;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;

namespace Client.ViewModels;

class CreateGroupViewModel : BindableBase
{
    private readonly IContainerProvider _containerProvider;
    private readonly IRegionManager _regionManager;
    private ObservableCollection<SwatchSet> _swatches = null!;

    public CreateGroupViewModel(IContainerProvider containerProvider, IRegionManager regionManager)
    {
        _containerProvider = containerProvider;
        _regionManager = regionManager;

        Swatches = InitSwatches();
        SwatchHeaders = InitSwatchHeaders();

        ResetCommand = new DelegateCommand(ResetGroup);
    }

    public ObservableCollection<SwatchSet> Swatches {
        get => _swatches;
        set => SetProperty(ref _swatches, value);
    }

    public IEnumerable<SwatchItem> SwatchHeaders { get; }

    public DelegateCommand ResetCommand { get; }
    public DelegateCommand CreateCommand { get; }

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
}