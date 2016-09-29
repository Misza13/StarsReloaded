namespace StarsReloaded.Shared.WorldGen.Meta
{
    public enum PlanetDistribution
    {
        /// Uniform distribution of planets.
        Uniform,

        /// Planets allowed to freely clump together,
        /// forming irregular and arbitrarily large clusters.
        /// Provides a more natural layout of the galaxy.
        FreeClumping,

        /// Planets are clumped but size of a single cluster is limited.
        /// Provides a more regular and "fair" layout.
        /// Closest to Stars! original clumping.
        UniformClumping
    }
}