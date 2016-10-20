namespace StarsReloaded.Shared.Model
{
    using System;

    public class HabitationParameter
    {
        public HabitationParameter(int clicks)
        {
            if (clicks < -50 || clicks > 50)
            {
                throw new ArgumentException("Habitation value must be between -50 and +50 \"clicks\".");
            }

            this.Clicks = clicks;
        }

        public int Clicks { get; private set; }
    }
}