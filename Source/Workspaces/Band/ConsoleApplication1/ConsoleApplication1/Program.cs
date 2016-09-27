using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        /* Tail starts here */
        static void Main(String[] args)
        {
            string line1 = Console.ReadLine();

            int[] line1values = Array.ConvertAll(line1.Split(' '), Int32.Parse);

            int n = line1values[0];
            int m = line1values[1];
            int k = line1values[2];

            ShoppingCenters shoppingCenters = new ShoppingCenters(n + 1);
            shoppingCenters.SetKindsOfFish(k);

            for (int i = 1; i <= n; i++)
            {
                int[] linei = Array.ConvertAll(Console.ReadLine().Split(' '), Int32.Parse);

                int numberOfTypesOfFish = linei[0];

                for (int j = 1; j <= numberOfTypesOfFish; j++)
                {
                    shoppingCenters[i].FishTypes.Add(linei[j]);
                }
            }

            for (int i = 1; i <= m; i++)
            {
                int[] linej = Array.ConvertAll(Console.ReadLine().Split(' '), Int32.Parse);

                int x = linej[0];
                int y = linej[1];
                int travelTime = linej[2];

                shoppingCenters.AddUndirectedEdge(x, y, travelTime);
            }

            Console.WriteLine(shoppingCenters.GetAllFish());

            Console.ReadKey();
        }
    }

    class ShoppingCenters : Graph<ShoppingCenter>
    {
        private int numberOfKindsOfFish;

        public ShoppingCenters(int centers)
            : base(centers)
        {

        }

        internal int GetAllFish()
        {
            List<int> fishList = new List<int>(numberOfKindsOfFish);

            for (int i = 1; i <= numberOfKindsOfFish; i++)
            {
                fishList.Add(i);
            }

            FindAllPathsSearch<ShoppingCenter> findPaths = new FindAllPathsSearch<ShoppingCenter>(this);

            findPaths.ExecuteSearch(1);

            List<Path<ShoppingCenter>> paths = findPaths.GetAllPaths();

            //disregard all paths with no fish
            paths = paths.Where(path => path.Vertices.Any(center => center.HasFish)).ToList();

            return GetMinimumCostPath(fishList, paths);
        }

        private int GetMinimumCostPath(IEnumerable<int> fishList, IEnumerable<Path<ShoppingCenter>> paths)
        {
            int maximumPathCost = int.MaxValue;
            //find one or two minimum paths that contain all fish
            foreach (var path in paths)
            {
                List<int> fishTypesInPath = GetFishInPath(path);

                var missingTypes = fishList.Except(fishTypesInPath);

                int maximumPathCostTemp = maximumPathCost;
 
                maximumPathCost = Math.Min(maximumPathCost, path.GetDistance());

                bool foundSecondPath = false;
                if (missingTypes.Count() > 0)
                {
                    //find path that contains missing fish
                    foreach (var secondPath in paths.Where(otherPath => otherPath != path))
                    {
                        List<int> fishTypesInPath2 = GetFishInPath(secondPath);

                        var missingTypes2 = missingTypes.Except(fishTypesInPath2);

                        if (missingTypes2.Count() == 0)
                        {
                            //found path that contains all fish
                            maximumPathCost = Math.Max(maximumPathCost, secondPath.GetDistance());
                            foundSecondPath = true;
                        }
                    }
                }

                if (!foundSecondPath)
                    maximumPathCost = maximumPathCost;
            }

            return maximumPathCost;
        }

        private List<int> GetFishInPath(Path<ShoppingCenter> path)
        {
            return path.Vertices.SelectMany(center => center.FishTypes).Distinct().ToList();
        }

        internal void SetKindsOfFish(int k)
        {
            numberOfKindsOfFish = k;
        }
    }

    class ShoppingCenter : Vertex
    {
        public List<int> FishTypes { get; private set; }

        public bool HasFish { get { return FishTypes.Count > 0; } }

        public ShoppingCenter(int number)
            : base(number)
        {
            FishTypes = new List<int>();
        }
    }
}
