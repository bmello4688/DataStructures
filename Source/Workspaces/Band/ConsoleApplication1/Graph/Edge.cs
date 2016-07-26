using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures
{
    public class Edge
    {
        public Vertex StartingVertex { get; private set; }
        public Vertex EndingVertex { get; private set; }
        public int Weight { get; private set; }

        public Edge(Vertex startingVertex, Vertex endingVertex, int weight)
        {
            this.StartingVertex = startingVertex;
            this.EndingVertex = endingVertex;
            this.Weight = weight;
        }
    }
}
