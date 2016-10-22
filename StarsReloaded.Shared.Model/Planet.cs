namespace StarsReloaded.Shared.Model
{
    public class Planet
    {
        public Planet(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; private set; }

        public int Y { get; private set; }

        public string Name { get; set; }

        public HabitationParameter Gravity { get; set; }

        public HabitationParameter Temperature { get; set; }

        public HabitationParameter Radiation { get; set; }

        public HabitationParameter OriginalGravity { get; set; }

        public HabitationParameter OriginalTemperature { get; set; }

        public HabitationParameter OriginalRadiation { get; set; }

        public MineralConcentration IroniumConcentration { get; set; }

        public MineralConcentration BoraniumConcentration { get; set; }

        public MineralConcentration GermaniumConcentration { get; set; }
    }
}
