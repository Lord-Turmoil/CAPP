using System.Windows;
using Client.Extensions.Request;
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
        containerRegistry.GetContainer().Register<IHttpRestClient, HttpRestClient>(made: Parameters.Of.Type<string>(serviceKey: "WebUrl"));
        containerRegistry.GetContainer().RegisterInstance(@"http://localhost:6256/", serviceKey: "WebUrl");

        containerRegistry.RegisterForNavigation<MainView, MainViewModel>();
        containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>();
        containerRegistry.RegisterForNavigation<PartsView, PartsViewModel>();
        containerRegistry.RegisterForNavigation<CreateGroupView, CreateGroupViewModel>();
        containerRegistry.RegisterForNavigation<GroupView, GroupViewModel>();
    }

    protected override Window CreateShell()
    {
        return Container.Resolve<MainView>();
    }
}