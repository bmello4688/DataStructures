using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures
{
    public class DijkstraAlgorithm
    {
        private List<Edge> edges;
        private HashSet<Vertex> settledNodes;
        private HashSet<Vertex> unSettledNodes;
        private Dictionary<Vertex, int> shortestDistanceDictionary;
        private Dictionary<Vertex, Vertex> predecessors;

        public DijkstraAlgorithm(Graph<Vertex> graph)
        {
            edges = new List<Edge>(graph.GetEdges());
        }

        public void Execute(Vertex startingVertex)
        {
            settledNodes = new HashSet<Vertex>();
            unSettledNodes = new HashSet<Vertex>();
            shortestDistanceDictionary = new Dictionary<Vertex, int>();
            predecessors = new Dictionary<Vertex, Vertex>();
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

        public List<Vertex> GetShortestPath(Vertex target)
        {
            List<Vertex> path = new List<Vertex>();
            Vertex step = target;
            // check if a path exists
            if (!predecessors.ContainsKey(step))
                return null;

            path.Add(step);

            while (!predecessors.ContainsKey(step))
            {
                step = predecessors[step];
                path.Add(step);
            }

            // Reverse because current list is from end to start
            path.Reverse();

            return path;
        }

        private void FindMinimalDistances(Vertex vertex)
        {
            List<Vertex> adjacentNodes = GetUnsettledNeighbors(vertex);
            foreach (var target in adjacentNodes)
            {
                if (GetShortestDistance(target) > GetShortestDistance(vertex) + GetDistance(vertex, target))
                {
                    shortestDistanceDictionary.Add(target, GetShortestDistance(vertex) + GetDistance(vertex, target));
                    predecessors.Add(target, vertex);
                    unSettledNodes.Add(target);
                }
            }
        }

        private int GetDistance(Vertex startingVertex, Vertex endingVertex)
        {
            Edge foundEdge = edges.Find(edge => edge.StartingVertex == startingVertex && edge.EndingVertex == endingVertex);

            if (foundEdge == null)
                throw new Exception("Could not find edge");
            else
                return foundEdge.Weight;
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
        //Foreach node set distance[node] = HIGH
        //SettledNodes = empty
        //UnSettledNodes = empty

        //Add sourceNode to UnSettledNodes
        //distance[sourceNode]= 0

        //while (UnSettledNodes is not empty) {
        //  evaluationNode = getNodeWithLowestDistance(UnSettledNodes)
        //  remove evaluationNode from UnSettledNodes 
        //    add evaluationNode to SettledNodes
        //    evaluatedNeighbors(evaluationNode)
        //}

        //getNodeWithLowestDistance(UnSettledNodes){
        //  find the node with the lowest distance in UnSettledNodes and return it 
        //}

        //evaluatedNeighbors(evaluationNode){
        //  Foreach destinationNode which can be reached via an edge from evaluationNode AND which is not in SettledNodes {
        //    edgeDistance = getDistance(edge(evaluationNode, destinationNode))
        //    newDistance = distance[evaluationNode] + edgeDistance
        //    if (distance[destinationNode]  > newDistance) {
        //      distance[destinationNode]  = newDistance 
        //      add destinationNode to UnSettledNodes
        //    }
        //  }
        //} 
    }
}
