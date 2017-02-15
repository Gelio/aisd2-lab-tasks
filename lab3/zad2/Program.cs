using System;
using System.Collections.Generic;
using ASD.Graph;

namespace zadanie3
{

    struct City
    {
    public int x;
    public int y;
    public City(int px, int py) { x=px; y=py; }
    }

    class Program
    {
        /* wpisz poprawną ścieżkę */
        const string path = null;
//        const string path = @"C:\Program Files (x86)\Graphviz2.36\bin\dot.exe";

        private static void Test(int num, City[] cities, int max_dist1, List<int> bases, int max_dist2)
        {
            Console.WriteLine("\n=================== Test {0} =====================\n",num);

            AirlinePlanner ap = new AirlinePlanner(path,num);
            ap.CreateNetwork(cities);
            Console.WriteLine("Największa odległość od bazy to: {0}, powinno być {1}", ap.GetMaximumDistance(0), max_dist1);


            int[] centers = ap.FindNewBase();

            Console.Write("Nowa baza powinna mieścić się w: ");
            foreach (var i in centers)
                Console.Write("{0}, ", i);
            Console.Write(" powinno być: ");
            foreach (var i in bases)
                Console.Write("{0}, ", i);

            Console.WriteLine();
            Console.Write("Największa odległość od nowych baz to odpowiednio: ");
            foreach (var i in centers)
                Console.Write("{0}, ", ap.GetMaximumDistance(i));
            Console.Write(" powinno być wszędzie {0}\n", max_dist2);
            if ( num<5 ) ap.Show();
        }

        static void Main(string[] args)
        {
            Random rnd = new Random(123);
            List<City> cities = new List<City>();
            List<int> bases = new List<int>();

            

            cities.Add(new City(0, 0));
            cities.Add(new City(1, 0));
            cities.Add(new City(3, 0));
            cities.Add(new City(4, 0));
            cities.Add(new City(3, 1));
            bases.Add(1);
            bases.Add(2);
            Test(1, cities.ToArray(), 3, bases, 2);
            cities.Clear();
            bases.Clear();


            cities.Add(new City(0, 0));
            for (int i = 1; i < 7; ++i)
            {
                cities.Add(new City(i, 0));
                cities.Add(new City(-i, 0));
            }
            bases.Add(0);
            Test(2, cities.ToArray(), 6, bases, 6);
            cities.Clear();
            bases.Clear();


            for (int i = 0; i < 20; ++i)
            {
                cities.Add(new City(rnd.Next(20), rnd.Next(20) ));
            }
            bases.Add(2);
            Test(3, cities.ToArray(), 6, bases, 4);
            cities.Clear();
            bases.Clear();


            for (int i = 0; i < 25; ++i)
            {
                cities.Add(new City(rnd.Next(20), rnd.Next(20) ));
            }
            bases.Add(3);
            bases.Add(4);
            Test(4, cities.ToArray(), 7, bases, 5);
            cities.Clear();
            bases.Clear();


            for (int i = 0; i < 10001; ++i)
            {
                cities.Add(new City(i, i)) ;
            }
            bases.Add(5000);
            Test(5, cities.ToArray(), 10000, bases, 5000);
            cities.Clear();
            bases.Clear();

            Console.WriteLine();
        }
    }
}
