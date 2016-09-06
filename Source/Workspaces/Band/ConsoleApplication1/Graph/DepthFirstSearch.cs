using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures
{
    public class DepthFirstSearch<TVertex> : GraphSearch<TVertex> where TVertex : Vertex
    {
        Dictionary<Vertex, bool> visitedDictionary = new Dictionary<Vertex, bool>();

        public DepthFirstSearch(Graph<TVertex> graph)
            :base(graph)
        {
            foreach (var vertex in graph.GetVertices())
            {
                visitedDictionary.Add(vertex, false);
            }
        }

        public override void ExecuteSearch(TVertex startVertex)
        {
            Stack<TVertex> vertexStack = new Stack<TVertex>();

            visitedDictionary[startVertex] = true;
            vertexStack.Push(startVertex);

            while (vertexStack.Count > 0)
            {
                TVertex vertex = vertexStack.Peek();

                bool foundUnvisitedVertex = false;
                foreach (var edge in vertex.Edges)
                {
                    if (!visitedDictionary[edge.EndingVertex])
                    {
                        foundUnvisitedVertex = true;
                        visitedDictionary[edge.EndingVertex] = foundUnvisitedVertex;
                        PathPredecessors.Add(edge.EndingVertex, vertex);
                        vertexStack.Push((TVertex)edge.EndingVertex);
                        break;
                    }
                }

                if (!foundUnvisitedVertex)
                    vertexStack.Pop();
            }
        }
    }
}
