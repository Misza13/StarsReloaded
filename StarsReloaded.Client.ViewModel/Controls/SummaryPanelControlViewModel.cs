namespace StarsReloaded.Client.ViewModel.Controls
{
    using System;
    using System.Windows;

    using GalaSoft.MvvmLight.Messaging;

    using StarsReloaded.Client.ViewModel.Attributes;
    using StarsReloaded.Client.ViewModel.Messages;
    using StarsReloaded.Client.ViewModel.ModelWrappers;
    using StarsReloaded.Shared.Model;

    public class SummaryPanelControlViewModel : BaseViewModel
    {
        #region Private fields

        private PlanetWrapper selectedPlanet;

        #endregion

        #region Constructors

        public SummaryPanelControlViewModel()
        {
            Messenger.Default.Register<PlanetSelectedMessage>(this, this.PlanetSelected);

            if (this.IsInDesignMode)
            {
                var race = new PlayerRace()
                {
                    GravityTolerance = HabitationRange.Immunity,
                    TemperatureTolerance = new HabitationRange(-35, +5),
                    RadiationTolerance = new HabitationRange(-20, +30)
                };
                Messenger.Default.Send(new GameStateLoadedMessage(new GameState { CurrentPlayerNum = 0, PlayerRaces = new[] { race } }));

                var planet =
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
                Messenger.Default.Send(new PlanetSelectedMessage(planet));
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