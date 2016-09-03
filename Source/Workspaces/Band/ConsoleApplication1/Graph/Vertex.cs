using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures
{
    public class Vertex
    {
        private Dictionary<int, Edge> edges = new Dictionary<int, Edge>();

        public int Number { get; private set; }
        public int Weight { get; set; }

        public IEnumerable<Edge> Edges { get { return edges.Values; } }

        public Vertex(int number)
        {
            Number = number;
            Weight = 1;
        }

        public void AddEdge(Vertex endVertex, int weight)
        {
            edges.Add(endVertex.Number, new Edge(this, endVertex, weight));
        }

        internal void RemoveEdge(int endVertex)
        {
            edges.Remove(endVertex);
        }

        internal void RemoveAllEdges()
        {
            edges.Clear();
        }

        internal void ReplaceEdge(int oldVertex, int newVertex)
        {
            if (edges.ContainsKey(oldVertex))
                edges[oldVertex].EndingVertex.Number = newVertex;
        }

        internal Edge GetEdge(Vertex vertex)
        {
            return edges[vertex.Number];
        }
    }
}
