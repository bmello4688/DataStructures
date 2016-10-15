using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures
{
    public class DijkstraSearch<TVertex> : GraphSearch<TVertex> where TVertex : Vertex
    {
        private HashSet<TVertex> settledNodes = new HashSet<TVertex>();
        private HashSet<TVertex> unSettledNodes = new HashSet<TVertex>();
        private Dictionary<TVertex, int> shortestDistanceDictionary = new Dictionary<TVertex, int>();

        public DijkstraSearch(Graph<TVertex> graph)
            :base(graph)
        {
        }

        protected internal override void OnClear()
        {
            settledNodes.Clear();
            unSettledNodes.Clear();
            shortestDistanceDictionary.Clear();

            foreach (var vertex in Graph.GetVertices())
            {
                shortestDistanceDictionary.Add(vertex, int.MaxValue);
            }
        }

        public  override void ExecuteSearch(TVertex startingVertex)
        {
            shortestDistanceDictionary[startingVertex] = 0;
            unSettledNodes.Add(startingVertex);
            while (unSettledNodes.Count > 0)
            {
                TVertex node = GetMinimum(unSettledNodes);
                settledNodes.Add(node);
                unSettledNodes.Remove(node);
                FindMinimalDistances(node);
            }
        }

        private void FindMinimalDistances(TVertex vertex)
        {
            List<TVertex> adjacentNodes = GetUnsettledNeighbors(vertex);
            foreach (var target in adjacentNodes)
            {
                if (GetDistance(target) > GetDistance(vertex) + Graph.GetEdgeDistance(vertex, target))
                {
                    FoundNewPath(vertex, target);
                    PathPredecessors[target] = vertex;
                    unSettledNodes.Add(target);
                }
            }
        }

        protected internal virtual void FoundNewPath(TVertex vertex, TVertex target)
        {
            shortestDistanceDictionary[target] = GetDistance(vertex) + Graph.GetEdgeDistance(vertex, target);
        }

        private List<TVertex> GetUnsettledNeighbors(TVertex startingVertex)
        {
            return startingVertex.Edges.Where(edge => !IsSettled(edge.EndingVertex)).Select(edge => edge.EndingVertex).Cast<TVertex>().ToList();
        }

        private TVertex GetMinimum(HashSet<TVertex> vertices)
        {
            TVertex minimum = null;
            foreach (var vertex in vertices)
            {
                if (minimum == null)
                    minimum = vertex;
                else
                {
                    if (GetDistance(vertex) < GetDistance(minimum))
                        minimum = vertex;
                }
            }

            return minimum;
        }

        private bool IsSettled(Vertex vertex)
        {
            return settledNodes.Contains((TVertex)vertex);
        }

        public virtual int GetDistance(TVertex destination)
        {
            return shortestDistanceDictionary[destination];
        }
    }
}
