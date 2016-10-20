namespace StarsReloaded.Client.ViewModel.Controls
{
    using System;
    using System.Windows;

    using GalaSoft.MvvmLight.Messaging;

    using StarsReloaded.Client.ViewModel.Attributes;
    using StarsReloaded.Client.ViewModel.Fragments;
    using StarsReloaded.Client.ViewModel.Messages;
    using StarsReloaded.Client.ViewModel.ModelWrap;
    using StarsReloaded.Shared.Model;

    public class SummaryPanelControlViewModel : BaseViewModel
    {
        #region Private fields

        private PlanetViewModel selectedPlanet;

        #endregion

        #region Constructors

        public SummaryPanelControlViewModel()
        {
            if (this.IsInDesignMode)
            {
                this.SelectedPlanet = new PlanetViewModel(new Planet(13, 37)
                    {
                        Name = Guid.NewGuid().ToString(),
                        Gravity = new HabitationParameter(27),
                        Temperature = new HabitationParameter(46),
                        Radiation = new HabitationParameter(95)
                    });

                this.GravityBarViewModel = new HabitationBarControlViewModel()
                    {
                        ParameterType = HabitationParameterType.Gravity,
                        Range = HabitationRange.Immunity,
                        CurrentValue = new HabitationParameter(40),
                        OriginalValue = new HabitationParameter(40)
                    };
                this.TemperatureBarViewModel = new HabitationBarControlViewModel()
                    {
                        ParameterType = HabitationParameterType.Temperature,
                        Range = new HabitationRange(-35, +5),
                        CurrentValue = new HabitationParameter(0),
                        OriginalValue = new HabitationParameter(+15)
                    };
                this.RadiationBarViewModel = new HabitationBarControlViewModel()
                    {
                        ParameterType = HabitationParameterType.Radiation,
                        Range = new HabitationRange(-20, +30),
                        CurrentValue = new HabitationParameter(+5),
                        OriginalValue = new HabitationParameter(-15)
                    };
            }

            Messenger.Default.Register<PlanetSelectedMessage>(this, this.PlanetSelected);
        }

        #endregion

        #region Public properties

        [DependsUpon(nameof(SelectedPlanet))]
        public Visibility PlanetSummaryVisibility => this.SelectedPlanet != null ? Visibility.Visible : Visibility.Hidden;

        [DependsUpon(nameof(SelectedPlanet))]
        public string GravityDisplayValue => this.SelectedPlanet?.GravityViewModel.DisplayValue ?? string.Empty;

        [DependsUpon(nameof(SelectedPlanet))]
        public string TemperatureDisplayValue => this.SelectedPlanet?.TemperatureViewModel.DisplayValue ?? string.Empty;

        [DependsUpon(nameof(SelectedPlanet))]
        public string RadiationDisplayValue => this.SelectedPlanet?.RadiationViewModel.DisplayValue ?? string.Empty;

        [DependsUpon(nameof(SelectedPlanet))]
        public HabitationBarControlViewModel GravityBarViewModel { get; set; }

        [DependsUpon(nameof(SelectedPlanet))]
        public HabitationBarControlViewModel TemperatureBarViewModel { get; set; }

        [DependsUpon(nameof(SelectedPlanet))]
        public HabitationBarControlViewModel RadiationBarViewModel { get; set; }

        #endregion

        #region Private properties

        private PlanetViewModel SelectedPlanet
        {
            get
            {
                return this.selectedPlanet;
            }

            set
            {
                if (value != this.selectedPlanet)
                {
                    this.selectedPlanet = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region Private methods

        private void PlanetSelected(PlanetSelectedMessage message)
        {
            this.SelectedPlanet = message.Planet;
        }

        #endregion
    }
}