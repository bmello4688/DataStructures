using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures
{
    public class DepthFirstSearch : GraphSearch
    {
        public DepthFirstSearch(Graph<Vertex> graph)
            :base(graph)
        {
        }

        public void FindDepthFirstShortestPath(int startVertex)
        {
            Stack<int> vertexStack = new Stack<int>();

            Graph[startVertex].Visited = true;
            vertexStack.Push(startVertex);

            while (vertexStack.Count > 0)
            {
                int vertex = vertexStack.Peek();

                bool foundUnvisitedVertex = false;
                foreach (var edge in Graph[vertex].Edges)
                {
                    if (!Graph[edge.EndingVertex.Number].Visited)
                    {
                        foundUnvisitedVertex = true;
                        Graph[edge.EndingVertex.Number].Visited = foundUnvisitedVertex;
                        vertexStack.Push(edge.EndingVertex.Number);

                        break;
                    }
                }

                if (!foundUnvisitedVertex)
                    vertexStack.Pop();
            }
        }
    }
}
