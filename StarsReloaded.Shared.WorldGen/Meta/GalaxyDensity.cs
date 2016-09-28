namespace StarsReloaded.Shared.WorldGen.Meta
{
    public enum GalaxyDensity
    {
        [BasePlanetCount(24)]
        Sparse,

        [BasePlanetCount(32)]
        Normal,

        [BasePlanetCount(40)]
        Dense,

        [BasePlanetCount(60)]
        Packed
    }
}