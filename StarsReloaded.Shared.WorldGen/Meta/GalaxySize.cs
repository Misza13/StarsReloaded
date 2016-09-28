namespace StarsReloaded.Shared.WorldGen.Meta
{
    public enum GalaxySize
    {
        [GalaxyEdge(400)]
        Tiny,

        [GalaxyEdge(800)]
        Small,

        [GalaxyEdge(1200)]
        Medium,

        [GalaxyEdge(1600)]
        Large,

        [GalaxyEdge(2000)]
        Huge
    }
}