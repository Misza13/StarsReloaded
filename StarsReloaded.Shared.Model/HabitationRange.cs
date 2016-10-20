namespace StarsReloaded.Shared.Model
{
    public class HabitationRange
    {
        public HabitationRange(
            HabitationParameter minValue,
            HabitationParameter maxValue)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.IsImmune = false;
        }

        public HabitationRange(int minValue, int maxValue)
        {
            this.MinValue = new HabitationParameter(minValue);
            this.MaxValue = new HabitationParameter(maxValue);
            this.IsImmune = false;
        }

        private HabitationRange()
        {
        }

        public static HabitationRange Immunity => new HabitationRange()
                                                      {
                                                          IsImmune = true
                                                      };

        public HabitationParameter MinValue { get; private set; }

        public HabitationParameter MaxValue { get; private set; }

        public bool IsImmune { get; private set; }
    }
}