using System;
using System.Linq;
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

        public IEnumerator<T> GetEnumerator()
        {
            return verticesDictionary.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        internal IEnumerable<Vertex> GetVertices()
        {
            return verticesDictionary.Values.Cast<Vertex>();
        }

        internal IEnumerable<Edge> GetEdges()
        {
            return GetVertices().SelectMany(vertex => vertex.Edges);
        }
    }
}
