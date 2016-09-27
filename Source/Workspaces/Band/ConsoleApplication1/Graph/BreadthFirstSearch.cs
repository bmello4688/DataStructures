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
        }

        protected internal override void OnClear()
        {
            foreach (var vertex in Graph.GetVertices())
            {
                if (!levelDicitonary.ContainsKey(vertex))
                    levelDicitonary.Add(vertex, -1);
                else
                    levelDicitonary[vertex] = -1;
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

                        if (PathPredecessors[edge.EndingVertex] == null || PathPredecessors[edge.EndingVertex].Weight > edge.Weight)
                            PathPredecessors[edge.EndingVertex] = (TVertex)vertex;

                        vertexQueue.Enqueue(edge.EndingVertex);
                    }
                }
            }
        }

        protected internal override void IgnoreVertex(TVertex vertex)
        {
            levelDicitonary[vertex] = int.MaxValue;
        }
    }
}
