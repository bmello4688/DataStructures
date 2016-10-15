using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures
{
    /// <summary>
    /// List of vertices connected by edges
    /// </summary>
    public class Path<TVertex> where TVertex : Vertex
    {
        private List<TVertex> path;

        public IEnumerable<TVertex> Vertices { get { return path; } }

        public Path(TVertex startingVertexInPath)
            : this(new List<TVertex>(){startingVertexInPath})
        {
        }

        public Path(List<TVertex> path)
        {
            this.path = path;
        }

        public int GetDistance()
        {
            int distance = 0;

            for (int i = 0; i < path.Count - 1; i++)
            {
                distance += GetEdgeDistance(path[i], path[i + 1]);
            }

            return distance;
        }

        public static int GetEdgeDistance(Vertex startingVertex, Vertex endingVertex)
        {
            return startingVertex.GetEdge(endingVertex).Weight;
        }

        internal IEnumerable<TVertex> GetMidPath()
        {
            int middleVertexCount = path.Count - 2;

            if (path.Count > 2)
                return path.Skip(1).Take(middleVertexCount);
            else
                return null;
        }

        public override bool Equals(object obj)
        {
            if (obj is Path<TVertex>)
            {
                var otherPath = (Path<TVertex>)obj;
                return Vertices.OrderBy(x => x).SequenceEqual(otherPath.Vertices.OrderBy(x => x));
            }
            else
                return false;
        }

        internal static Path<TVertex> Extend(Path<TVertex> path, TVertex vertex)
        {
            var newPath = path.Vertices.ToList();

            if (!newPath.Contains(vertex))
            {   //makes unique
                newPath.Add(vertex);
                return new Path<TVertex>(newPath);
            }
            else
                return path;
            
        }
    }
}
