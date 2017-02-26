﻿using System;
using System.Collections.Generic;
using ASD.Graphs;


namespace zadanie3
{
    class AirlinePlanner
    {
        int num;
        Graph airline;
        GraphExport ge;

        // Możesz dodać nowe składowe


        public AirlinePlanner(string path, int n)
        {
            num = n;
            ge = new GraphExport(false, null, path);
        }

        public void CreateNetwork(City[] coordinates)
        {
            airline = new AdjacencyListsGraph<AVLAdjacencyList>(false, coordinates.Length);

            for (int i=1; i < coordinates.Length; i++)
            {
                int closestCity = 0;
                double closestCityDistance = CalculateDistanceSquared(coordinates[i], coordinates[closestCity]);

                for (int j = 1; j < i; j++)
                {
                    double currentDistance = CalculateDistanceSquared(coordinates[i], coordinates[j]);
                    if (currentDistance < closestCityDistance)
                    {
                        closestCity = j;
                        closestCityDistance = currentDistance;
                    }
                }

                airline.AddEdge(i, closestCity);
            }
        }

        public int[] FindNewBase()
        {
            // uzupełnij

            // najprostszy algorytm polega na powtarzaniu procesu
            //  - wyznaczania liści w grafie (drzewie)
            //  - usuwania liści
            // dopóki pozostały wiecej niż 2 nie usunięte wierzchołki

            return new int[0]; // zmień
        }

        public int GetMaximumDistance(int start)
        {
            // uzupełnij

            // można wykorzystać przeszukiwanie wszerz
            // rozważ użycie metody GeneralSearchFrom

            // Zabronione jest korzystanie z metod rozszerzających zdefiniowanych w klasie ShortestPathsGraphExtender !!!!

            return 0; // zmień
        }

        public void Show()
        {
            if (airline != null)
                ge.Export(airline, null, string.Format("Test{0}", num));
        }

        private static double CalculateDistanceSquared(City c1, City c2)
        {
            return Math.Pow(c1.x - c2.x, 2) + Math.Pow(c1.y - c2.y, 2);
        }

        private static double CalculateDistance(City c1, City c2)
        {
            return Math.Sqrt(CalculateDistanceSquared(c1, c2));
        }
    }
}
