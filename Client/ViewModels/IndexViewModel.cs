using Prism.Events;
using Prism.Mvvm;

namespace Client.ViewModels;

class IndexViewModel : NavigationViewModel
{
    public IndexViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
    {
    }
}