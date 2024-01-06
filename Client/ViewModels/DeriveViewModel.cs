using System.Collections.ObjectModel;
using Client.Extensions;
using Client.Models;
using Prism.Events;
using Server.Modules.Core.Dtos;
using Shared.Dtos;

namespace Client.ViewModels;

class DeriveViewModel : NavigationViewModel
{
    public DeriveViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
    {
        SwatchHeaders = SwatchHelper.InitSwatchHeaders();
        Swatches = SwatchHelper.InitSwatches();
    }

    private ObservableCollection<GroupDto> _allGroups = null!;

    public ObservableCollection<GroupDto> AllGroups {
        get => _allGroups;
        set => SetProperty(ref _allGroups, value);
    }

    private ObservableCollection<ProcedureDto> _allProcedures = null!;
    public ObservableCollection<ProcedureDto> AllProcedures {
        get => _allProcedures;
        set => SetProperty(ref _allProcedures, value);
    }

    private ObservableCollection<PartDto> _allParts = null!;
    public ObservableCollection<PartDto> AllParts {
        get => _allParts;
        set => SetProperty(ref _allParts, value);
    }

    private ObservableCollection<SwatchSet> _swatches = null!;
    public ObservableCollection<SwatchSet> Swatches {
        get => _swatches;
        set => SetProperty(ref _swatches, value);
    }
    public IEnumerable<SwatchItem> SwatchHeaders { get; }


}