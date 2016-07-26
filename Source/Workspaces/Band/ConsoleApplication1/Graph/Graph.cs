using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures
{
    public class Graph<T> : IEnumerable<T> where T : Vertex
    {
        Dictionary<int, T> verticesDictionary;

        public T this[int vertexNumber]
        {
            get
            {
                return verticesDictionary[vertexNumber];
            }
        }

        // Constructor - creates an empty Adjacency List
        public Graph(int vertices)
        {
            verticesDictionary = new Dictionary<int, T>(vertices);

            for (int i = 0; i < vertices; ++i)
            {
                verticesDictionary.Add(i, CreateVertex(i));
            }
        }

        protected T CreateVertex(int i)
        {
            return (T)Activator.CreateInstance(typeof(T), i);
        }

        public void AddEdge(int startVertex, int endVertex, int weight = 1)
        {
            this[startVertex].AddEdge(new Vertex(endVertex), weight);
        }

        public int GetShortestPathLength(int vertex)
        {
            if(this[vertex].Level < 0)
                return this[vertex].Level;
            else
            {
                int pathLength = 0;
                int currentVertex = vertex;

                while (this[currentVertex].Level > 0)
                {
                    Edge nextEdge = this[currentVertex].GetNextEdgeInShortestPath();

                    pathLength += nextEdge.Weight;

                    currentVertex = nextEdge.EndingVertex.Number;
                }

                return pathLength;
            }

        }

        // Removes the first occurence of an edge and returns true
        // if there was any change in the collection, else false
        public void RemoveEdge(int startVertex, int endVertex)
        {
            this[startVertex].RemoveEdge(endVertex);
        }

        public void ReplaceAllEdges(int oldVertex, int newVertex)
        {
            this[oldVertex].RemoveAllEdges();

            foreach (var vertex in this)
            {
                vertex.ReplaceEdge(oldVertex, newVertex);
            }
        }

        public void FindBreadthFirstShortestPath(int startVertex)
        {
            //BreadthFirstSearch
            Queue<int> vertexQueue = new Queue<int>();

            this[startVertex].Level = 0;
            vertexQueue.Enqueue(startVertex);

            while(vertexQueue.Count > 0)
            {
                int vertex = vertexQueue.Dequeue();

                foreach (var edge in this[vertex].Edges)
                {
                    if(this[edge.EndingVertex.Number].Level < 0 && edge.EndingVertex.Number != startVertex)
                    {
                        this[edge.EndingVertex.Number].Level = this[vertex].Level + 1;

                        if(this[edge.EndingVertex.Number].Parent == null || this[edge.EndingVertex.Number].GetNextEdgeInShortestPath().Weight > edge.Weight)
                            this[edge.EndingVertex.Number].Parent = this[vertex];

                        vertexQueue.Enqueue(edge.EndingVertex.Number);
                    }
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return verticesDictionary.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
