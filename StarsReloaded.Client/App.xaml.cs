namespace StarsReloaded.Client
{
    using System.Windows;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using StarsReloaded.Client.Mediation;
    using StarsReloaded.Client.Mediation.Windows;
    using StarsReloaded.Client.ViewModel;
    using StarsReloaded.Shared.Guts.Services;
    using StarsReloaded.Shared.Services;
    using StarsReloaded.Shared.WorldGen.Services;
    using StarsReloaded.View.Windows;

    public partial class App : Application
    {
        public App()
        {
            var container = new WindsorContainer();
            IoCHelper.Container = container;

            container.Register(Component.For<IStartupWindow>().ImplementedBy<StartupWindow>());
            container.Register(Component.For<IMainWindow>().ImplementedBy<MainWindow>());

            container.Register(
                Types.FromAssemblyContaining(typeof(BaseViewModel))
                    .Where(t => t.Name.EndsWith("ViewModel"))
                    .LifestyleTransient());

            container.Register(Component.For<IWindowManager>().ImplementedBy<WindowManager>());

            container.Register(Component.For<IRngService>().ImplementedBy<RngService>());
            container.Register(Component.For<IGalaxyGeneratorService>().ImplementedBy<GalaxyGeneratorService>());
            container.Register(Component.For<IPlanetGeneratorService>().ImplementedBy<PlanetGeneratorService>());
            container.Register(Component.For<IPlanetSimulationService>().ImplementedBy<PlanetSimulationService>());

            var windowManager = container.Resolve<IWindowManager>();

            windowManager.ShowStartupWindow();
        }
    }
}
