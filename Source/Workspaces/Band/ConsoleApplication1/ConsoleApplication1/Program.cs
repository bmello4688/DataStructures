﻿using DataStructures;
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

                shoppingCenters.AddEdge(x, y, travelTime);
            }

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
            DijkstraSearch<ShoppingCenter> search = new DijkstraSearch<ShoppingCenter>(this);

            search.ExecuteSearch(1);

            List<ShoppingCenter> shortestPath = search.GetShortestPath(1, GetVertices().Count() - 1);
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
