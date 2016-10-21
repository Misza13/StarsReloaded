namespace StarsReloaded.Client.ViewModel.Controls
{
    using System;
    using System.Windows;

    using GalaSoft.MvvmLight.Messaging;

    using StarsReloaded.Client.ViewModel.Attributes;
    using StarsReloaded.Client.ViewModel.Fragments;
    using StarsReloaded.Client.ViewModel.Messages;
    using StarsReloaded.Client.ViewModel.ModelWrappers;
    using StarsReloaded.Shared.Model;

    public class SummaryPanelControlViewModel : BaseViewModel
    {
        #region Private fields

        private PlanetWrapper selectedPlanet;
        private HabitationBarControlViewModel gravityBarViewModel;
        private HabitationBarControlViewModel temperatureBarViewModel;
        private HabitationBarControlViewModel radiationBarViewModel;

        #endregion

        #region Constructors

        public SummaryPanelControlViewModel()
        {
            if (this.IsInDesignMode)
            {
                this.SelectedPlanet = new PlanetWrapper(new Planet(13, 37)
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

            Messenger.Default.Register<GameStateLoadedMessage>(this, this.OnGameStateLoaded);
            Messenger.Default.Register<PlanetSelectedMessage>(this, this.PlanetSelected);
        }

        #endregion

        #region Public properties

        [DependsUpon(nameof(SelectedPlanet))]
        public Visibility PlanetSummaryVisibility => this.SelectedPlanet != null ? Visibility.Visible : Visibility.Hidden;

        [DependsUpon(nameof(SelectedPlanet))]
        public string GravityDisplayValue => this.SelectedPlanet?.Gravity.DisplayValue ?? string.Empty;

        [DependsUpon(nameof(SelectedPlanet))]
        public string TemperatureDisplayValue => this.SelectedPlanet?.Temperature.DisplayValue ?? string.Empty;

        [DependsUpon(nameof(SelectedPlanet))]
        public string RadiationDisplayValue => this.SelectedPlanet?.Radiation.DisplayValue ?? string.Empty;

        [DependsUpon(nameof(SelectedPlanet))]
        public HabitationBarControlViewModel GravityBarViewModel
        {
            get
            {
                return this.gravityBarViewModel;
            }

            set
            {
                this.Set(() => this.GravityBarViewModel, ref this.gravityBarViewModel, value);
            }
        }

        [DependsUpon(nameof(SelectedPlanet))]
        public HabitationBarControlViewModel TemperatureBarViewModel
        {
            get
            {
                return this.temperatureBarViewModel;
            }

            set
            {
                this.Set(() => this.TemperatureBarViewModel, ref this.temperatureBarViewModel, value);
            }
        }

        [DependsUpon(nameof(SelectedPlanet))]
        public HabitationBarControlViewModel RadiationBarViewModel
        {
            get
            {
                return this.radiationBarViewModel;
            }

            set
            {
                this.Set(() => this.RadiationBarViewModel, ref this.radiationBarViewModel, value);
            }
        }

        #endregion

        #region Private properties

        public PlanetWrapper SelectedPlanet
        {
            get
            {
                return this.selectedPlanet;
            }

            set
            {
                this.Set(() => this.SelectedPlanet, ref this.selectedPlanet, value);
            }
        }

        #endregion

        #region Private methods

        private void OnGameStateLoaded(GameStateLoadedMessage message)
        {
            this.GravityBarViewModel = new HabitationBarControlViewModel()
            {
                ParameterType = HabitationParameterType.Gravity,
                Range = message.GameState.CurrentPlayerRace.GravityTolerance
            };
            this.TemperatureBarViewModel = new HabitationBarControlViewModel()
            {
                ParameterType = HabitationParameterType.Temperature,
                Range = message.GameState.CurrentPlayerRace.TemperatureTolerance
            };
            this.RadiationBarViewModel = new HabitationBarControlViewModel()
            {
                ParameterType = HabitationParameterType.Radiation,
                Range = message.GameState.CurrentPlayerRace.RadiationTolerance
            };
        }

        private void PlanetSelected(PlanetSelectedMessage message)
        {
            this.SelectedPlanet = message.Planet;
        }

        #endregion
    }
}