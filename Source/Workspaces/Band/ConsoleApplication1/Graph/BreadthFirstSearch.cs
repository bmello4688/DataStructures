using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures
{
    public class BreadthFirstSearch : GraphSearch
    {
        private Dictionary<Vertex, int> levelDicitonary = new Dictionary<Vertex, int>();

        public BreadthFirstSearch(Graph<Vertex> graph)
            :base(graph)
        {
            foreach (var vertex in graph.GetVertices())
            {
                levelDicitonary.Add(vertex, -1);
            }
        }
        public void FindBreadthFirstShortestPath(int startVertex)
        {
            //BreadthFirstSearch
            Queue<int> vertexQueue = new Queue<int>();

            levelDicitonary[Graph[startVertex]] = 0;
            vertexQueue.Enqueue(startVertex);

            while (vertexQueue.Count > 0)
            {
                int vertex = vertexQueue.Dequeue();

                foreach (var edge in Graph[vertex].Edges)
                {
                    if (levelDicitonary[edge.EndingVertex] < 0 && edge.EndingVertex.Number != startVertex)
                    {
                        levelDicitonary[edge.EndingVertex] = levelDicitonary[Graph[vertex]] + 1;

                        if (Graph[edge.EndingVertex.Number].Parent == null || Graph[edge.EndingVertex.Number].GetNextEdgeInShortestPath().Weight > edge.Weight)
                            Graph[edge.EndingVertex.Number].Parent = Graph[vertex];

                        vertexQueue.Enqueue(edge.EndingVertex.Number);
                    }
                }
            }
        }

        public int GetShortestPathLength(int vertex)
        {
            if (levelDicitonary[Graph[vertex]] < 0)
                return levelDicitonary[Graph[vertex]];
            else
            {
                int pathLength = 0;
                int currentVertex = vertex;

                while (levelDicitonary[Graph[currentVertex]] > 0)
                {
                    Edge nextEdge = Graph[currentVertex].GetNextEdgeInShortestPath();

                    pathLength += nextEdge.Weight;

                    currentVertex = nextEdge.EndingVertex.Number;
                }

                return pathLength;
            }

        }
    }
}
