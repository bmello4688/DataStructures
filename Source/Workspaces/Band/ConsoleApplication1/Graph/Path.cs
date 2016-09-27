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
    }
}
