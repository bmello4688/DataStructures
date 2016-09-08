using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures
{
    public abstract class GraphSearch<TVertex> where TVertex : Vertex
    {
        protected Dictionary<Vertex, TVertex> PathPredecessors;
        protected Graph<TVertex> Graph { get; private set; }

        public GraphSearch(Graph<TVertex> graph)
        {
            this.Graph = graph;
            PathPredecessors = new Dictionary<Vertex, TVertex>(graph.GetVertices().Count());
        }


        public void ExecuteSearch(int startingVertexNumber)
        {
            ExecuteSearch(Graph[startingVertexNumber]);
        }

        public abstract void ExecuteSearch(TVertex startingVertex);

        public List<TVertex> GetShortestPath(int startVertex, int targetVertex)
        {
            return GetShortestPath(Graph[startVertex], Graph[targetVertex]);
        }

        public List<TVertex> GetShortestPath(TVertex targetVertex)
        {
            return GetShortestPath(Graph[0], targetVertex);
        }

        public List<TVertex> GetShortestPath(TVertex startVertex, TVertex targetVertex)
        {
            List<TVertex> path = new List<TVertex>();
            TVertex step = targetVertex;
            // check if a path exists
            if (!PathPredecessors.ContainsKey(step))
                return null;

            path.Add(step);

            while (PathPredecessors.ContainsKey(step))
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

        public int GetShortestPathCost(TVertex target)
        {
            List<TVertex> shortestPath = GetShortestPath(target);

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
