using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures
{
    public class DijkstraAlgorithm : GraphSearch
    {
        private HashSet<Vertex> settledNodes;
        private HashSet<Vertex> unSettledNodes;
        private Dictionary<Vertex, int> shortestDistanceDictionary;

        public DijkstraAlgorithm(Graph<Vertex> graph)
            :base(graph)
        {
        }

        public  override void ExecuteSearch(Vertex startingVertex)
        {
            settledNodes = new HashSet<Vertex>();
            unSettledNodes = new HashSet<Vertex>();
            shortestDistanceDictionary = new Dictionary<Vertex, int>();
            PathPredecessors = new Dictionary<Vertex, Vertex>();
            shortestDistanceDictionary.Add(startingVertex, 0);
            unSettledNodes.Add(startingVertex);
            while (unSettledNodes.Count > 0)
            {
                Vertex node = GetMinimum(unSettledNodes);
                settledNodes.Add(node);
                unSettledNodes.Remove(node);
                FindMinimalDistances(node);
            }
        }

        private void FindMinimalDistances(Vertex vertex)
        {
            List<Vertex> adjacentNodes = GetUnsettledNeighbors(vertex);
            foreach (var target in adjacentNodes)
            {
                if (GetShortestDistance(target) > GetShortestDistance(vertex) + GetDistance(vertex, target))
                {
                    shortestDistanceDictionary.Add(target, GetShortestDistance(vertex) + GetDistance(vertex, target));
                    PathPredecessors.Add(target, vertex);
                    unSettledNodes.Add(target);
                }
            }
        }

        private List<Vertex> GetUnsettledNeighbors(Vertex startingVertex)
        {
            return startingVertex.Edges.Where(edge => !IsSettled(edge.EndingVertex)).Select(edge => edge.EndingVertex).ToList();
        }

        private Vertex GetMinimum(HashSet<Vertex> vertices)
        {
            Vertex minimum = null;
            foreach (var vertex in vertices)
            {
                if (minimum == null)
                    minimum = vertex;
                else
                {
                    if (GetShortestDistance(vertex) < GetShortestDistance(minimum))
                        minimum = vertex;
                }
            }

            return minimum;
        }

        private bool IsSettled(Vertex vertex)
        {
            return settledNodes.Contains(vertex);
        }

        private int GetShortestDistance(Vertex destination)
        {
            if (!shortestDistanceDictionary.ContainsKey(destination))
                return int.MaxValue;
            else
                return shortestDistanceDictionary[destination];
        }
    }
}
