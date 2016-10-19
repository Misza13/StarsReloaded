namespace StarsReloaded.Client.ViewModel.Controls
{
    using System;

    using GalaSoft.MvvmLight.Messaging;

    using StarsReloaded.Client.ViewModel.Messages;
    using StarsReloaded.Client.ViewModel.ModelWrap;
    using StarsReloaded.Shared.Model;

    public class SummaryPanelControlViewModel : BaseViewModel
    {
        private PlanetViewModel selectedObject;

        public SummaryPanelControlViewModel()
        {
            if (this.IsInDesignMode)
            {
                this.selectedObject = new PlanetViewModel(new Planet(13, 37)
                    {
                        Name = Guid.NewGuid().ToString(),
                        Gravity = new HabitationParameter(27),
                        Temperature = new HabitationParameter(46),
                        Radiation = new HabitationParameter(95)
                    });
            }

            Messenger.Default.Register<PlanetSelectedMessage>(this, this.PlanetSelected);
        }

        public string GravityDisplayValue => this.selectedObject?.GravityViewModel.DisplayValue ?? string.Empty;

        public string TemperatureDisplayValue => this.selectedObject?.TemperatureViewModel.DisplayValue ?? string.Empty;

        public string RadiationDisplayValue => this.selectedObject?.RadiationViewModel.DisplayValue ?? string.Empty;

        private void PlanetSelected(PlanetSelectedMessage message)
        {
            this.selectedObject = message.Planet;
            this.RaisePropertyChanged(nameof(this.GravityDisplayValue));
            this.RaisePropertyChanged(nameof(this.TemperatureDisplayValue));
            this.RaisePropertyChanged(nameof(this.RadiationDisplayValue));
        }
    }
}