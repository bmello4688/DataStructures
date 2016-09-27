﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures
{
    public class DijkstraSearch<TVertex> : GraphSearch<TVertex> where TVertex : Vertex
    {
        private HashSet<Vertex> settledNodes = new HashSet<Vertex>();
        private HashSet<Vertex> unSettledNodes = new HashSet<Vertex>();
        private Dictionary<Vertex, int> shortestDistanceDictionary = new Dictionary<Vertex, int>();

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

        protected internal override void IgnoreVertex(TVertex vertex)
        {
            settledNodes.Add(vertex);
        }

        public  override void ExecuteSearch(TVertex startingVertex)
        {
            shortestDistanceDictionary[startingVertex] = 0;
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
                if (GetShortestDistance(target) > GetShortestDistance(vertex) + Graph.GetEdgeDistance(vertex, target))
                {
                    shortestDistanceDictionary[target] = GetShortestDistance(vertex) + Graph.GetEdgeDistance(vertex, target);
                    PathPredecessors[target] = (TVertex)vertex;
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
            return shortestDistanceDictionary[destination];
        }
    }
}
