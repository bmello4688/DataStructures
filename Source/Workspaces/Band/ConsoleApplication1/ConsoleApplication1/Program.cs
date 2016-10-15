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
        private List<int> fish = new List<int>();

        public ShoppingCenters(int centers)
            : base(centers)
        {

        }

        internal int GetAllFish()
        {
            FindAllPathsSearch<ShoppingCenter> search = new FindAllPathsSearch<ShoppingCenter>(this);

            search.ExecuteSearch(1);

            List<Path<ShoppingCenter>> paths = search.GetAllPaths();

            int best_time = int.MaxValue;

            for (int i = 0; i < paths.Count; i++)
            {
                for (int j = i; j < paths.Count; j++)
                {
                    var fishList1 = paths[i].Vertices.SelectMany(center => center.FishTypes);
                    var fishList2 = paths[j].Vertices.SelectMany(center => center.FishTypes);

                    if (fishList1 != null && fishList2 != null && fishList1.Union(fishList2).Distinct().OrderBy(x => x).SequenceEqual(fish))
                        best_time = Math.Min(best_time, Math.Max(paths[i].GetDistance(), paths[j].GetDistance()));
                }
            }

            return best_time;
        }

        private List<int> GetFishInPath(Path<ShoppingCenter> path)
        {
            return path.Vertices.SelectMany(center => center.FishTypes).Distinct().ToList();
        }

        internal void SetKindsOfFish(int k)
        {
            fish.Clear();

            for (int i = 1; i <= k; i++)
            {
                fish.Add(i);
            }
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
