namespace StarsReloaded.Shared.WorldGen.Meta
{
    using System;

    public class BasePlanetCountAttribute : Attribute
    {
        public int Num { get; set; }

        public BasePlanetCountAttribute(int num)
        {
            Num = num;
        }
    }
}