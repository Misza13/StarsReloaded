namespace StarsReloaded.Shared.Model
{
    public class HabitationParameter
    {
        public HabitationParameter(int clicks)
        {
            this.Clicks = clicks;
        }

        public int Clicks { get; private set; }
    }
}