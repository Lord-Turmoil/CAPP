using System.Collections.ObjectModel;
using System.Diagnostics;
using ImTools;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Shared.Dtos;

namespace Client.ViewModels;

class PartsViewModel : BindableBase
{
    private readonly IContainerProvider _containerProvider;
    private readonly IRegionManager _regionManager;

    private ObservableCollection<PartDto> _allParts = null!;

    private string _name = "";

    private int _opitz;

    public PartsViewModel(IContainerProvider containerProvider, IRegionManager regionManager)
    {
        _containerProvider = containerProvider;
        _regionManager = regionManager;

        AllParts = GetAllParts();

        CreatePartCommand = new DelegateCommand(CreatePart);
        DeleteSelectedCommand = new DelegateCommand(DeleteSelectedPart);
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

    private ObservableCollection<PartDto> GetAllParts()
    {
        return new ObservableCollection<PartDto> {
            new() { Id = 1, Name = "Part 1", Opitz = "123456789" },
            new() { Id = 2, Name = "Part 2", Opitz = "987654321" },
            new() { Id = 3, Name = "Part 3", Opitz = "164382954" },
            new() { Id = 4, Name = "Part 4", Opitz = "914682375" },
            new() { Id = 5, Name = "Part 5", Opitz = "192837465" }
        };
    }
}