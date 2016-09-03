using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures
{
    public class DepthFirstSearch : GraphSearch
    {
        Dictionary<Vertex, bool> visitedDictionary = new Dictionary<Vertex, bool>();

        public DepthFirstSearch(Graph<Vertex> graph)
            :base(graph)
        {
            foreach (var vertex in graph.GetVertices())
            {
                visitedDictionary.Add(vertex, false);
            }
        }

        public override void ExecuteSearch(Vertex startVertex)
        {
            Stack<Vertex> vertexStack = new Stack<Vertex>();

            visitedDictionary[startVertex] = true;
            vertexStack.Push(startVertex);

            while (vertexStack.Count > 0)
            {
                Vertex vertex = vertexStack.Peek();

                bool foundUnvisitedVertex = false;
                foreach (var edge in vertex.Edges)
                {
                    if (!visitedDictionary[edge.EndingVertex])
                    {
                        foundUnvisitedVertex = true;
                        visitedDictionary[edge.EndingVertex] = foundUnvisitedVertex;
                        PathPredecessors.Add(edge.EndingVertex, vertex);
                        vertexStack.Push(edge.EndingVertex);
                        break;
                    }
                }

                if (!foundUnvisitedVertex)
                    vertexStack.Pop();
            }
        }
    }
}
