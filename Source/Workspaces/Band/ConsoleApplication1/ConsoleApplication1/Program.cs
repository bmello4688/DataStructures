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

            for (int i = 1; i <= n; i++)
            {
                int[] linei = Array.ConvertAll(Console.ReadLine().Split(' '), Int32.Parse);

                int numberOfTypesOfFish = linei[0];

                for (int j = 0; j < numberOfTypesOfFish; j++)
                {
                    shoppingCenters[1].FishTypes.Add(linei[j]);
                }
            }

            for (int i = 1; i <= m; i++)
            {
                int[] linei = Array.ConvertAll(Console.ReadLine().Split(' '), Int32.Parse);

                int x = linei[0];
                int y = linei[1];
                int travelTime = linei[2];

                shoppingCenters.AddEdge(x, y, travelTime);
            }

            shoppingCenters.SetKindsOfFish(k);
            shoppingCenters.GetAllFish();

            Console.WriteLine(shoppingCenters.Level);

            Console.ReadKey();
        }
    }

    class ShoppingCenters : Graph<ShoppingCenter>
    {
        private int numberOfKindsOfFish;

        public bool Level { get; private set; }

        public ShoppingCenters(int centers)
            : base(centers)
        {

        }

        internal void GetAllFish()
        {
            
        }

        internal void SetKindsOfFish(int k)
        {
            numberOfKindsOfFish = k;
        }
    }

    class ShoppingCenter : Vertex
    {
        public List<int> FishTypes { get; private set; }

        public ShoppingCenter(int number) : base(number)
        {
            FishTypes = new List<int>();
        }
    }
}
