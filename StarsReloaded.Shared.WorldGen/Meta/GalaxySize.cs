namespace StarsReloaded.Shared.WorldGen.Meta
{
    public enum GalaxySize
    {
        /// 400x400 galaxy
        [GalaxyEdge(400)]
        Tiny,

        /// 800x800 galaxy
        [GalaxyEdge(800)]
        Small,

        /// 1200x1200 galaxy
        [GalaxyEdge(1200)]
        Medium,

        /// 1600x1600 galaxy
        [GalaxyEdge(1600)]
        Large,

        /// 2000x2000 galaxy
        [GalaxyEdge(2000)]
        Huge
    }
}