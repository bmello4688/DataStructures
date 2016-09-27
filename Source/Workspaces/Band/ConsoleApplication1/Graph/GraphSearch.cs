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
            Clear();
        }


        public void ExecuteSearch(int startingVertexNumber)
        {
            ExecuteSearch(Graph[startingVertexNumber]);
        }

        public abstract void ExecuteSearch(TVertex startingVertex);

        public void Clear()
        {
            PathPredecessors.Clear();

            foreach (var vertex in Graph.GetVertices())
            {
                PathPredecessors.Add(vertex, null);
            }

            OnClear();
        }

        protected internal abstract void OnClear();

        public Path<TVertex> GetShortestPath(int startVertex, int targetVertex)
        {
            return GetShortestPath(Graph[startVertex], Graph[targetVertex]);
        }

        public Path<TVertex> GetShortestPath(TVertex targetVertex)
        {
            return GetShortestPath(Graph[0], targetVertex);
        }

        public Path<TVertex> GetShortestPath(TVertex startVertex, TVertex targetVertex)
        {
            List<TVertex> path = new List<TVertex>();
            TVertex step = targetVertex;
            // check if a path exists
            if (PathPredecessors[step] == null)
                return null;

            path.Add(step);

            while (PathPredecessors[step] != null)
            {
                step = PathPredecessors[step];
                path.Add(step);

                if (step == startVertex)
                    break;//Found starting vertex so end path
            }

            // Reverse because current list is from end to start
            path.Reverse();

            return new Path<TVertex>(path);
        }

        public int GetShortestPathCost(int target)
        {
            return GetShortestPathCost(Graph[target]);
        }

        public int GetShortestPathCost(TVertex target)
        {
            Path<TVertex> shortestPath = GetShortestPath(target);

            return shortestPath.GetDistance();
        }
    }
}
