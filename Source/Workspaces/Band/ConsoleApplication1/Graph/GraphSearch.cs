using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures
{
    public abstract class GraphSearch
    {
        protected Dictionary<Vertex, Vertex> PathPredecessors;
        protected Graph<Vertex> Graph { get; private set; }

        public GraphSearch(Graph<Vertex> graph)
        {
            this.Graph = graph;
        }


        public void ExecuteSearch(int startingVertexNumber)
        {
            ExecuteSearch(Graph[startingVertexNumber]);
        }

        public abstract void ExecuteSearch(Vertex startingVertex);

        public List<Vertex> GetShortestPath(Vertex target)
        {
            List<Vertex> path = new List<Vertex>();
            Vertex step = target;
            // check if a path exists
            if (!PathPredecessors.ContainsKey(step))
                return null;

            path.Add(step);

            while (!PathPredecessors.ContainsKey(step))
            {
                step = PathPredecessors[step];
                path.Add(step);
            }

            // Reverse because current list is from end to start
            path.Reverse();

            return path;
        }

        protected int GetDistance(Vertex startingVertex, Vertex endingVertex)
        {
            return startingVertex.GetEdge(endingVertex).Weight;
        }

        protected int GetDistance(int startingVertex, int endingVertex)
        {
            return Graph[startingVertex].GetEdge(Graph[endingVertex]).Weight;
        }
    }
}
