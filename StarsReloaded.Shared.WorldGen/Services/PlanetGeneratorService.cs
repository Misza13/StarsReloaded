namespace StarsReloaded.Shared.WorldGen.Services
{
    using System;
    using StarsReloaded.Shared.Model;
    using StarsReloaded.Shared.Services;

    public class PlanetGeneratorService : IPlanetGeneratorService
    {
        private readonly IRngService rngService;

        public PlanetGeneratorService(IRngService rngService)
        {
            this.rngService = rngService;
        }

        public void PopulatePlanetStats(Planet planet)
        {
            planet.Name = Guid.NewGuid().ToString();

            planet.OriginalGravity = new HabitationParameter(this.rngService.HabitiationParameter(HabitationParameterType.Gravity));
            planet.OriginalTemperature = new HabitationParameter(this.rngService.HabitiationParameter(HabitationParameterType.Temperature));
            planet.OriginalRadiation = new HabitationParameter(this.rngService.HabitiationParameter(HabitationParameterType.Radiation));

            ////TODO: set to original
            planet.Gravity = new HabitationParameter(this.rngService.HabitiationParameter(HabitationParameterType.Gravity));
            planet.Temperature = new HabitationParameter(this.rngService.HabitiationParameter(HabitationParameterType.Temperature));
            planet.Radiation = new HabitationParameter(this.rngService.HabitiationParameter(HabitationParameterType.Radiation));

            var extremeHabs =
                (planet.Gravity.IsExtreme ? 1 : 0) +
                (planet.Temperature.IsExtreme ? 1 : 0) +
                (planet.Radiation.IsExtreme ? 1 : 0);

            planet.IroniumConcentration = new MineralConcentration(this.rngService.MineralConcentration(extremeHabs));
            planet.BoraniumConcentration = new MineralConcentration(this.rngService.MineralConcentration(extremeHabs));
            planet.GermaniumConcentration = new MineralConcentration(this.rngService.MineralConcentration(extremeHabs));

            ////TODO: set to 0
            planet.SurfaceIronium = this.rngService.Next(2000);
            planet.SurfaceBoranium = this.rngService.Next(2000);
            planet.SurfaceGermanium = this.rngService.Next(2000);
        }
    }
}