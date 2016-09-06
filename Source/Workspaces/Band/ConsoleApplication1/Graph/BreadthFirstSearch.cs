using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures
{
    public class BreadthFirstSearch<TVertex> : GraphSearch<TVertex> where TVertex : Vertex
    {
        private Dictionary<Vertex, int> levelDicitonary = new Dictionary<Vertex, int>();

        public BreadthFirstSearch(Graph<TVertex> graph)
            :base(graph)
        {
            foreach (var vertex in graph.GetVertices())
            {
                levelDicitonary.Add(vertex, -1);
            }
        }
        public override void ExecuteSearch(TVertex startVertex)
        {
            //BreadthFirstSearch
            Queue<Vertex> vertexQueue = new Queue<Vertex>();

            levelDicitonary[startVertex] = 0;
            vertexQueue.Enqueue(startVertex);

            while (vertexQueue.Count > 0)
            {
                Vertex vertex = vertexQueue.Dequeue();

                foreach (var edge in vertex.Edges)
                {
                    if (levelDicitonary[edge.EndingVertex] < 0 && edge.EndingVertex != startVertex)
                    {
                        levelDicitonary[edge.EndingVertex] = levelDicitonary[vertex] + 1;

                        if (!PathPredecessors.ContainsKey(edge.EndingVertex))
                            PathPredecessors.Add(edge.EndingVertex, (TVertex)vertex);
                        else if (PathPredecessors[edge.EndingVertex].Weight > edge.Weight)
                            PathPredecessors[edge.EndingVertex] = (TVertex)vertex;

                        vertexQueue.Enqueue(edge.EndingVertex);
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
                    Edge nextEdge = Graph[currentVertex].GetEdge(PathPredecessors[Graph[currentVertex]]);

                    pathLength += nextEdge.Weight;

                    currentVertex = nextEdge.EndingVertex.Number;
                }

                return pathLength;
            }

        }
    }
}
