namespace StarsReloaded.Shared.Model
{
    public class PlayerRace
    {
        public HabitationRange GravityTolerance { get; set; }

        public HabitationRange TemperatureTolerance { get; set; }

        public HabitationRange RadiationTolerance { get; set; }

        public int GetMaxTerraformTech(HabitationParameterType type)
        {
            return 20; ////TODO
        }
    }
}