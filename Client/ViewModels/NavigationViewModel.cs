// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace Client.ViewModels;

class NavigationViewModel : BindableBase, INavigationAware
{
    protected readonly IEventAggregator _eventAggregator;

    protected NavigationViewModel(IEventAggregator eventAggregator)
    {
        _eventAggregator = eventAggregator;
    }

    public virtual void OnNavigatedTo(NavigationContext navigationContext)
    {
        // Do nothing...
    }

    public virtual bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return true;
    }

    public virtual void OnNavigatedFrom(NavigationContext navigationContext)
    {
        // Do nothing...
    }
}