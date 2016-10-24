namespace StarsReloaded.Client.ViewModel
{
    using GalaSoft.MvvmLight.Ioc;
    using Microsoft.Practices.ServiceLocation;
    using StarsReloaded.Client.ViewModel.Controls;
    using StarsReloaded.Client.ViewModel.Fragments;
    using StarsReloaded.Client.ViewModel.Windows;
    using StarsReloaded.Shared.Guts.Services;
    using StarsReloaded.Shared.Services;
    using StarsReloaded.Shared.WorldGen.Services;

    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}
            
            ////Services
            SimpleIoc.Default.Register<IRngService, RngService>();
            SimpleIoc.Default.Register<IGalaxyGeneratorService, GalaxyGeneratorService>();
            SimpleIoc.Default.Register<IPlanetSimulationService, PlanetSimulationService>();

            ////Windows
            SimpleIoc.Default.Register<MainWindowViewModel>();
            SimpleIoc.Default.Register<StartupWindowViewModel>();

            ////Controls
            SimpleIoc.Default.Register<GalaxyMapPanelViewModel>();
            SimpleIoc.Default.Register<SummaryPanelViewModel>();

            ////Fragments
            SimpleIoc.Default.Register<CollapsiblePanelViewModel>();
            SimpleIoc.Default.Register<HabitationBarControlViewModel>();
        }

        public static MainWindowViewModel MainWindow => ServiceLocator.Current.GetInstance<MainWindowViewModel>();

        public static StartupWindowViewModel StartupWindow => ServiceLocator.Current.GetInstance<StartupWindowViewModel>();

        public static GalaxyMapPanelViewModel GalaxyMapPanel => ServiceLocator.Current.GetInstance<GalaxyMapPanelViewModel>();

        public static SummaryPanelViewModel SummaryPanel => ServiceLocator.Current.GetInstance<SummaryPanelViewModel>();

        public static HabitationBarControlViewModel HabitationBarControl => new HabitationBarControlViewModel();

        public static CollapsiblePanelViewModel CollapsiblePanel => new CollapsiblePanelViewModel();
    }
}