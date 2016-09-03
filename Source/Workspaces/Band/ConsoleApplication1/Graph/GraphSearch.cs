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

        public List<Vertex> GetShortestPath(int startVertex = 0, int targetVertex)
        {
            return GetShortestPath(Graph[startVertex], Graph[targetVertex]);
        }

        public List<Vertex> GetShortestPath(Vertex targetVertex)
        {
            return GetShortestPath(Graph[0], targetVertex);
        }

        public List<Vertex> GetShortestPath(Vertex startVertex, Vertex targetVertex)
        {
            List<Vertex> path = new List<Vertex>();
            Vertex step = targetVertex;
            // check if a path exists
            if (!PathPredecessors.ContainsKey(step))
                return null;

            path.Add(step);

            while (!PathPredecessors.ContainsKey(step))
            {
                step = PathPredecessors[step];
                path.Add(step);

                if (step == startVertex)
                    break;//Found starting vertex so end path
            }

            // Reverse because current list is from end to start
            path.Reverse();

            return path;
        }

        public int GetShortestPathCost(int target)
        {
            return GetShortestPathCost(Graph[target]);
        }

        public int GetShortestPathCost(Vertex target)
        {
            List<Vertex> shortestPath = GetShortestPath(target);

            int cost = 0;
            shortestPath.ForEach(vertex => cost += vertex.Weight);

            return cost;
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
