namespace StarsReloaded.Shared.WorldGen.Meta
{
    using System;

    public class BasePlanetCountAttribute : Attribute
    {
        public BasePlanetCountAttribute(int num)
        {
            this.Num = num;
        }

        public int Num { get; }
    }
}