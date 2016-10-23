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
        private HabitationBarControlViewModel gravityBar;
        private HabitationBarControlViewModel temperatureBar;
        private HabitationBarControlViewModel radiationBar;

        #endregion

        #region Constructors

        public SummaryPanelControlViewModel()
        {
            Messenger.Default.Register<GameStateLoadedMessage>(this, this.OnGameStateLoaded);
            Messenger.Default.Register<PlanetSelectedMessage>(this, this.OnPlanetSelected);

            this.GravityBar = ViewModelLocator.HabitationBarControl;
            this.GravityBar.ParameterType = HabitationParameterType.Gravity;

            this.TemperatureBar = ViewModelLocator.HabitationBarControl;
            this.TemperatureBar.ParameterType = HabitationParameterType.Temperature;

            this.RadiationBar = ViewModelLocator.HabitationBarControl;
            this.RadiationBar.ParameterType = HabitationParameterType.Radiation;

            if (this.IsInDesignMode)
            {
                this.GravityBar.Range = HabitationRange.Immunity;
                this.TemperatureBar.Range = new HabitationRange(-35, +5);
                this.RadiationBar.Range = new HabitationRange(-20, +30);

                this.SelectedPlanet =
                    new PlanetWrapper(
                        new Planet(13, 37)
                            {
                                Name = Guid.NewGuid().ToString(),
                                Gravity = new HabitationParameter(27),
                                Temperature = new HabitationParameter(46),
                                Radiation = new HabitationParameter(95),
                                OriginalGravity = new HabitationParameter(35),
                                OriginalTemperature = new HabitationParameter(12),
                                OriginalRadiation = new HabitationParameter(70)
                            });
            }
        }

        #endregion

        #region Public properties

        [DependsUpon(nameof(SelectedPlanet))]
        public Visibility PlanetSummaryVisibility => this.SelectedPlanet != null ? Visibility.Visible : Visibility.Hidden;

        [DependsUpon(nameof(SelectedPlanet))]
        public string PlanetName => this.SelectedPlanet?.Name ?? string.Empty;

        [DependsUpon(nameof(SelectedPlanet))]
        public string GravityDisplayValue => this.SelectedPlanet?.Gravity.DisplayValue ?? string.Empty;

        [DependsUpon(nameof(SelectedPlanet))]
        public string TemperatureDisplayValue => this.SelectedPlanet?.Temperature.DisplayValue ?? string.Empty;

        [DependsUpon(nameof(SelectedPlanet))]
        public string RadiationDisplayValue => this.SelectedPlanet?.Radiation.DisplayValue ?? string.Empty;

        public PlanetWrapper SelectedPlanet
        {
            get
            {
                return this.selectedPlanet;
            }

            set
            {
                this.Set(() => this.SelectedPlanet, ref this.selectedPlanet, value);
                this.UpdatePlanetValues();
            }
        }

        [DependsUpon(nameof(SelectedPlanet))]
        public HabitationBarControlViewModel GravityBar
        {
            get
            {
                return this.gravityBar;
            }

            set
            {
                this.Set(() => this.GravityBar, ref this.gravityBar, value);
            }
        }

        [DependsUpon(nameof(SelectedPlanet))]
        public HabitationBarControlViewModel TemperatureBar
        {
            get
            {
                return this.temperatureBar;
            }

            set
            {
                this.Set(() => this.TemperatureBar, ref this.temperatureBar, value);
            }
        }

        [DependsUpon(nameof(SelectedPlanet))]
        public HabitationBarControlViewModel RadiationBar
        {
            get
            {
                return this.radiationBar;
            }

            set
            {
                this.Set(() => this.RadiationBar, ref this.radiationBar, value);
            }
        }

        #endregion

        #region Private methods

        private void OnGameStateLoaded(GameStateLoadedMessage message)
        {
            this.UpdateHabRanges(message.GameState.CurrentPlayerRace);
        }

        private void OnPlanetSelected(PlanetSelectedMessage message)
        {
            this.SelectedPlanet = message.Planet;
        }

        private void UpdateHabRanges(PlayerRace race)
        {
            /*if (this.GravityBar == null)
            {
                return;
            }*/

            this.GravityBar.Range = race.GravityTolerance;
            this.TemperatureBar.Range = race.TemperatureTolerance;
            this.RadiationBar.Range = race.RadiationTolerance;

            this.GravityBar.MaxTerraformTech = race.GetMaxTerraformTech(HabitationParameterType.Gravity);
            this.TemperatureBar.MaxTerraformTech = race.GetMaxTerraformTech(HabitationParameterType.Temperature);
            this.RadiationBar.MaxTerraformTech = race.GetMaxTerraformTech(HabitationParameterType.Radiation);
        }

        private void UpdatePlanetValues()
        {
            /*if (this.GravityBar == null)
            {
                return;
            }*/

            this.GravityBar.CurrentValue = this.SelectedPlanet?.Gravity.Model;
            this.TemperatureBar.CurrentValue = this.SelectedPlanet?.Temperature.Model;
            this.RadiationBar.CurrentValue = this.SelectedPlanet?.Radiation.Model;

            this.GravityBar.OriginalValue = this.SelectedPlanet?.OriginalGravity.Model;
            this.TemperatureBar.OriginalValue = this.SelectedPlanet?.OriginalTemperature.Model;
            this.RadiationBar.OriginalValue = this.SelectedPlanet?.OriginalRadiation.Model;
        }

        #endregion
    }
}