using System.Windows;
using Client.Views;
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

    }

    protected override Window CreateShell()
    {
        return Container.Resolve<MainView>();
    }
}