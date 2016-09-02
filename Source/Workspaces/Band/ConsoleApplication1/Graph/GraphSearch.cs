using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures
{
    public abstract class GraphSearch
    {
        protected Graph<Vertex> Graph { get; private set; }

        public GraphSearch(Graph<Vertex> graph)
        {
            this.Graph = graph;
        }
    }
}
