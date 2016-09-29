namespace StarsReloaded.Shared.WorldGen.Meta
{
    public enum GalaxyDensity
    {
        /// 1.5 planets / 10k l.y.^2
        [BasePlanetCount(24)]
        Sparse,

        /// 2 planets / 10k l.y.^2
        [BasePlanetCount(32)]
        Normal,

        /// 2.5 planets / 10k l.y.^2
        [BasePlanetCount(40)]
        Dense,

        /// 3.75 planets / 10k l.y.^2
        [BasePlanetCount(60)]
        Packed
    }
}