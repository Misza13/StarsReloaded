namespace StarsReloaded.Shared.WorldGen.Meta
{
    using System;

    public class GalaxyEdgeAttribute : Attribute
    {
        public int Edge { get; }

        public GalaxyEdgeAttribute(int edge)
        {
            this.Edge = edge;
        }
    }
}