// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

using Prism.Events;

namespace Client.ViewModels;

class IndexViewModel : NavigationViewModel
{
    public IndexViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
    {
    }
}