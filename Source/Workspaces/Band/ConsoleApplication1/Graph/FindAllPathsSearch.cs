using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures
{
    public class FindAllPathsSearch<TVertex> : GraphSearch<TVertex> where TVertex : Vertex
    {
        private List<Path<TVertex>> paths = new List<Path<TVertex>>();
        private HashSet<Path<TVertex>> setPaths = new HashSet<Path<TVertex>>();

        public FindAllPathsSearch(Graph<TVertex> graph)
            :base(graph)
        {
        }

        public override void ExecuteSearch(TVertex startingVertex)
        {
            ExtendPath(new Path<TVertex>(startingVertex));
            while (setPaths.Count > 0)
            {
                Path<TVertex> path = setPaths.First();
                TVertex lastAddedVertex = path.Vertices.Last();
                setPaths.Remove(setPaths.First());

                if (lastAddedVertex != Graph.Last())
                {
                    foreach (var adjacentVertex in GetNeighbors(lastAddedVertex))
                    {
                        ExtendPath(Path<TVertex>.Extend(path, adjacentVertex));
                    }
                }
            }

            //trim all paths that do not end at the last node
            paths.RemoveAll(path => path.Vertices.Last() != Graph.Last());
        }

        private void ExtendPath(Path<TVertex> newPath)
        {
            
            if (paths.Exists(existingPath => existingPath.Equals(newPath) && existingPath.GetDistance() <= newPath.GetDistance()))
                return;
            else
            {
                if (setPaths.Count > 0)
                    setPaths.RemoveWhere(path => path.Equals(newPath));

                paths.Add(newPath);
                setPaths.Add(newPath);
            }
        }

        protected internal override void OnClear()
        {
            paths.Clear();   
        }

        public List<Path<TVertex>> GetAllPaths()
        {
            return paths;
        }
    }
}
