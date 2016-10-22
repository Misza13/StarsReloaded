namespace StarsReloaded.Shared.Model
{
    using System;

    public class MineralConcentration
    {
        public MineralConcentration(int concentration)
        {
            if (concentration < 0 || concentration > 100)
            {
                throw new ArgumentException("Mineral concentration must be between 0 and 100.");
            }

            this.Concentration = concentration;
        }

        public int Concentration { get; private set; }
    }
}