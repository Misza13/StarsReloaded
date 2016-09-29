namespace StarsReloaded.Shared.WorldGen.Meta
{
    using System;

    public class GalaxyEdgeAttribute : Attribute
    {
        public GalaxyEdgeAttribute(int edge)
        {
            this.Edge = edge;
        }

        public int Edge { get; }
    }
}