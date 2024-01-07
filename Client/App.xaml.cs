// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

using System.Diagnostics;
using System.Windows;
using Client.Extensions.Request;
using Client.Services;
using Client.ViewModels;
using Client.Views;
using DryIoc;
using Prism.DryIoc;
using Prism.Ioc;

namespace Client;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : PrismApplication
{
    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.GetContainer()
            .Register<IHttpRestClient, HttpRestClient>(made: Parameters.Of.Type<string>(serviceKey: "WebUrl"));
        containerRegistry.GetContainer().RegisterInstance(@"http://localhost:6256/", serviceKey: "WebUrl");

        containerRegistry.Register<IPartService, PartService>();
        containerRegistry.Register<IGroupService, GroupService>();
        containerRegistry.Register<IProcedureService, ProcedureService>();

        containerRegistry.RegisterForNavigation<MainView, MainViewModel>();
        containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>();
        containerRegistry.RegisterForNavigation<PartsView, PartsViewModel>();
        containerRegistry.RegisterForNavigation<CreateGroupView, CreateGroupViewModel>();
        containerRegistry.RegisterForNavigation<GroupView, GroupViewModel>();
        containerRegistry.RegisterForNavigation<DeriveView, DeriveViewModel>();
    }

    protected override Window CreateShell()
    {
        Process.Start("Launch.bat", "start");
        return Container.Resolve<MainView>();
    }
}