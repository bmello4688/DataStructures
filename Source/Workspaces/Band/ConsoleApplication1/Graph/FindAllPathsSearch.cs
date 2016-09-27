using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures
{
    public class FindAllPathsSearch<TVertex> : GraphSearch<TVertex> where TVertex : Vertex
    {
        Dictionary<Edge, bool> visitedDictionary = new Dictionary<Edge, bool>();
        List<Path<TVertex>> paths = new List<Path<TVertex>>();

        public FindAllPathsSearch(Graph<TVertex> graph)
            :base(graph)
        {
        }

        public override void ExecuteSearch(TVertex startingVertex)
        {
            Stack<TVertex> vertexStack = new Stack<TVertex>();

            vertexStack.Push(startingVertex);

            while (vertexStack.Count > 0)
            {
                TVertex vertex = vertexStack.Peek();

                bool foundUnvisitedEdge = false;
                foreach (var edge in vertex.Edges)
                {
                    if (!visitedDictionary[edge])
                    {
                        foundUnvisitedEdge = true;
                        visitedDictionary[edge] = foundUnvisitedEdge;
                        PathPredecessors[edge.EndingVertex] = vertex;
                        vertexStack.Push((TVertex)edge.EndingVertex);
                        break;
                    }
                }

                if (!foundUnvisitedEdge)
                    vertexStack.Pop();
                else if(vertexStack.Peek() == Graph[Graph.LastVertexNumber])
                {
                    //found path
                    paths.Add(GetShortestPath(vertexStack.Peek()));
                    vertexStack.Pop();
                }
            }
        }

        protected internal override void OnClear()
        {
            foreach (var edge in Graph.GetEdges())
            {
                if (!visitedDictionary.ContainsKey(edge))
                    visitedDictionary.Add(edge, false);
                else
                    visitedDictionary[edge] = false;
            }
        }

        public List<Path<TVertex>> GetAllPaths()
        {
            return paths;
        }
    }
}
