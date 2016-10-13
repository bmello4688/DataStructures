using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures
{
    public class FindAllPathsSearch<TVertex> : GraphSearch<TVertex> where TVertex : Vertex
    {
        private Dictionary<Edge, bool> visitedDictionary = new Dictionary<Edge, bool>();
        private Dictionary<Vertex, bool> visitedVertexDictionary = new Dictionary<Vertex, bool>();
        private List<Path<TVertex>> paths = new List<Path<TVertex>>();

        public FindAllPathsSearch(Graph<TVertex> graph)
            :base(graph)
        {
        }

        public override void ExecuteSearch(TVertex startingVertex)
        {
            Stack<TVertex> vertexStack = new Stack<TVertex>();

            TVertex lastVertex = Graph[Graph.LastVertexNumber];
            visitedVertexDictionary[startingVertex] = true;
            vertexStack.Push(startingVertex);

            while (vertexStack.Count > 0)
            {
                TVertex vertex = vertexStack.Peek();

                bool foundUnvisitedEdge = false;
                //ignore backward paths
                foreach (var edge in vertex.Edges)
                {
                    if (!visitedDictionary[edge] && !visitedVertexDictionary[edge.EndingVertex])
                    {
                        foundUnvisitedEdge = true;
                        visitedDictionary[edge] = foundUnvisitedEdge;
                        visitedVertexDictionary[vertex] = foundUnvisitedEdge;
                        PathPredecessors[edge.EndingVertex] = vertex;
                        vertexStack.Push((TVertex)edge.EndingVertex);
                        break;
                    }
                }

                if (!foundUnvisitedEdge)
                    vertexStack.Pop();
                else if(vertexStack.Peek() == lastVertex)
                {
                    //found path
                    paths.Add(GetShortestPath(startingVertex, vertexStack.Peek()));
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

            ClearVerticesVisited();
        }

        private void ClearVerticesVisited()
        {
            foreach (var vertex in Graph.GetVertices())
            {
                if (!visitedVertexDictionary.ContainsKey(vertex))
                    visitedVertexDictionary.Add(vertex, false);
                else
                    visitedVertexDictionary[vertex] = false;
            }
        }

        public List<Path<TVertex>> GetAllPaths()
        {
            return paths;
        }
    }
}
