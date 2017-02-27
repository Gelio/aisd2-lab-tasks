using System;
using System.Collections.Generic;
using System.Linq;
using ASD.Graphs;

namespace lab06
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph roads, paths, paths0, paths1, paths2;
            int[] cityCosts;
            string[] names;
            List<List<int>> listOfResults0 = new List<List<int>>();
            List<List<int>> listOfResults1 = new List<List<int>>();
            List<List<int>> listOfResults2 = new List<List<int>>();
            List<int> result0 = new List<int>();
            List<int> result1 = new List<int>();
            List<int> result2 = new List<int>();
            List<Graph> graphs = new List<Graph>();
            List<Graph> listOfPaths0 = new List<Graph>();
            List<Graph> listOfPaths1 = new List<Graph>();
            List<Graph> listOfPaths2 = new List<Graph>();
            List<int[]> costs = new List<int[]>();
            List<int> bounds = new List<int>();
            List<int> expCosts = new List<int>();
            ulong cr;
            ulong [,] cgg = new ulong[3,5];
            cgg[0, 0] = 325;
            cgg[0, 1] = 211;
            cgg[0, 2] = 169;
            cgg[0, 3] = 393;
            cgg[0, 4] = 33523661;
            cgg[1, 0] = 946;
            cgg[1, 1] = 638;
            cgg[1, 2] = 550;
            cgg[1, 3] = 1090;
            cgg[1, 4] = 62343794;
            cgg[2, 0] = 1030;
            cgg[2, 1] = 758;
            cgg[2, 2] = 582;
            cgg[2, 3] = 1231;
            cgg[2, 4] = 76762096;


            Test0(out roads, out cityCosts, out result0, out result1, out result2, out paths0, out paths1, out paths2);
            graphs.Add(roads);
            costs.Add(cityCosts);
            listOfPaths0.Add(paths0);
            listOfPaths1.Add(paths1);
            listOfPaths2.Add(paths2);
            listOfResults0.Add(result0);
            listOfResults1.Add(result1);
            listOfResults2.Add(result2);
            

            Test1(out roads, out cityCosts, out result0, out result1, out result2, out paths0, out paths1, out paths2);
            graphs.Add(roads);
            costs.Add(cityCosts);
            listOfPaths0.Add(paths0);
            listOfPaths1.Add(paths1);
            listOfPaths2.Add(paths2);
            listOfResults0.Add(result0);
            listOfResults1.Add(result1);
            listOfResults2.Add(result2);


            Test2(out roads, out cityCosts, out result0, out result1, out result2, out paths0, out paths1, out paths2);
            graphs.Add(roads);
            costs.Add(cityCosts);
            listOfPaths0.Add(paths0);
            listOfPaths1.Add(paths1);
            listOfPaths2.Add(paths2);
            listOfResults0.Add(result0);
            listOfResults1.Add(result1);
            listOfResults2.Add(result2);


            Test3(out roads, out cityCosts, out result0, out result1, out result2, out paths0, out paths1, out paths2);
            graphs.Add(roads);
            costs.Add(cityCosts);
            listOfPaths0.Add(paths0);
            listOfPaths1.Add(paths1);
            listOfPaths2.Add(paths2);
            listOfResults0.Add(result0);
            listOfResults1.Add(result1);
            listOfResults2.Add(result2);

            int n = 200;
            RandomGraphGenerator rgg = new RandomGraphGenerator(12345);
            roads = rgg.UndirectedGraph(typeof(AdjacencyMatrixGraph),n,0.75,1,99);
            Random rnd = new Random(1234);
            cityCosts = new int[n];
            for(int i=0; i<n;i++)
                cityCosts[i]=rnd.Next(1,9);
            graphs.Add(roads);
            costs.Add(cityCosts);

            Pathfinder pf;
            int minCost = 0;
            int[] path;
            bool pass;

            Console.WriteLine("\nCzesc 0\n");
            for (int testno = 0; testno < graphs.Count-1; ++testno)
            {
                    roads = graphs[testno];
                    cityCosts = costs[testno];
                    result0 = listOfResults0.ElementAt(testno);

                    names = new string[cityCosts.Length];
                    for (int i = 0; i < cityCosts.Length; ++i)
                        names[i] = String.Format("{0} ({1})", i, cityCosts[i]);

                    pf = new Pathfinder(roads, cityCosts);
                    minCost = 0;
                    cr = Graph.Counter;
                    path = pf.FindBestLocationWithoutCityCosts(out minCost, out paths);
                    cr = Graph.Counter - cr;

                    pass = path!=null && path.SequenceEqual(result0) && minCost == result0.Min();
                    Console.WriteLine("Test {0} koszty  - {1}", testno, pass ? "Dobrze" : "Zle");
                    if (paths != null)
                    {
                        pass = paths.IsEqual(listOfPaths0.ElementAt(testno));
                    }
                    else
                    {
                        pass = false;
                    }
                    Console.WriteLine("Test {0} sciezki - {1}", testno, pass ? "Dobrze" : "Zle");
                    Console.WriteLine("Test {0} wydajnosc: {1} ( wzorcowa: {2} )", testno, cr, cgg[0, testno]);
            }

                    roads = graphs[graphs.Count-1];
                    cityCosts = costs[graphs.Count-1];
                    pf = new Pathfinder(roads, cityCosts);
                    minCost = 0;
                    cr = Graph.Counter;
                    path = pf.FindBestLocationWithoutCityCosts(out minCost, out paths);
                    cr = Graph.Counter - cr;
                    Console.WriteLine("\nTest wydajnosci : {0} ( wzorcowo: {1} )", cr, cgg[0, 4]);

            Console.WriteLine("\nCzesc 1\n");
            for (int testno = 0; testno < graphs.Count-1; ++testno)
            {
                    roads = graphs[testno];
                    cityCosts = costs[testno];
                    result1 = listOfResults1.ElementAt(testno);

                    names = new string[cityCosts.Length];
                    for (int i = 0; i < cityCosts.Length; ++i)
                        names[i] = String.Format("{0} ({1})", i, cityCosts[i]);

                    pf = new Pathfinder(roads, cityCosts);
                    minCost = 0;
                    cr = Graph.Counter;
                    path = pf.FindBestLocation(out minCost, out paths);
                    cr = Graph.Counter - cr;

                    pass = path!=null && path.SequenceEqual(result1) && minCost == result1.Min();
                    Console.WriteLine("Test {0} koszty  - {1}", testno, pass ? "Dobrze" : "Zle");
                    if (paths != null)
                    {
                        pass = paths.IsEqual(listOfPaths1.ElementAt(testno));
                    }
                    else
                    {
                        pass = false;
                    }
                    Console.WriteLine("Test {0} sciezki - {1}", testno, pass ? "Dobrze" : "Zle");
                    Console.WriteLine("Test {0} wydajnosc: {1} ( wzorcowa: {2} )", testno, cr, cgg[1, testno]);
            }

                    roads = graphs[graphs.Count-1];
                    cityCosts = costs[graphs.Count-1];
                    pf = new Pathfinder(roads, cityCosts);
                    minCost = 0;
                    cr = Graph.Counter;
                    path = pf.FindBestLocation(out minCost, out paths);
                    cr = Graph.Counter - cr;
                    Console.WriteLine("\nTest wydajnosci : {0} ( wzorcowo: {1} )", cr, cgg[1, 4]);

            Console.WriteLine("\nCzesc 2\n");
            for (int testno = 0; testno < graphs.Count-1; ++testno)
            {
                    roads = graphs[testno];
                    cityCosts = costs[testno];
                    result2 = listOfResults2.ElementAt(testno);

                    names = new string[cityCosts.Length];
                    for (int i = 0; i < cityCosts.Length; ++i)
                        names[i] = String.Format("{0} ({1})", i, cityCosts[i]);

                    minCost = 0;
                    pf = new Pathfinder(roads, cityCosts);
                    cr = Graph.Counter;
                    path = pf.FindBestLocationSecondMetric(out minCost, out paths);
                    cr = Graph.Counter - cr;

                    pass = path!=null && path.SequenceEqual(result2) && minCost == result2.Min();
                    Console.WriteLine("Test {0} koszty  - {1}", testno, pass ? "Dobrze" : "Zle");
                    if (paths != null)
                    {
                        pass = paths.IsEqual(listOfPaths2.ElementAt(testno));
                    }
                    else
                    {
                        pass = false;
                    }
                    Console.WriteLine("Test {0} sciezki - {1}", testno, pass ? "Dobrze" : "Zle");
                    Console.WriteLine("Test {0} wydajnosc: {1} ( wzorcowa: {2} )", testno, cr, cgg[2, testno]);
            }

                    roads = graphs[graphs.Count-1];
                    cityCosts = costs[graphs.Count-1];
                    pf = new Pathfinder(roads, cityCosts);
                    minCost = 0;
                    cr = Graph.Counter;
                    path = pf.FindBestLocationSecondMetric(out minCost, out paths);
                    cr = Graph.Counter - cr;
                    Console.WriteLine("\nTest wydajnosci : {0} ( wzorcowo: {1} )", cr, cgg[2, 4]);

            Console.WriteLine();
        }

        static void Test0(out Graph roads, out int[] cityCosts, out List<int> result0, out List<int> result1, out List<int> result2, out Graph paths0 , out Graph paths1, out Graph paths2)
        {
            int n = 4;

            roads = new AdjacencyMatrixGraph(false, n);
            cityCosts = new int[n];
            cityCosts[0] = 2;
            cityCosts[1] = 1;
            cityCosts[2] = 100;
            cityCosts[3] = 2;
            
            roads.AddEdge(0, 1, 4);
            roads.AddEdge(1, 3, 4);
            roads.AddEdge(0, 2, 1);
            roads.AddEdge(2, 3, 1);

            result0 = new List<int>();
            result0.Add(7);
            result0.Add(13);
            result0.Add(7);
            result0.Add(7);

            result1 = new List<int>();
            result1.Add(117);
            result1.Add(119);
            result1.Add(14);
            result1.Add(117);

            result2 = new List<int>();
            result2.Add(109);
            result2.Add(115);
            result2.Add(10);
            result2.Add(109);

            paths0 = roads.IsolatedVerticesGraph(true,roads.VerticesCount);
            paths0.AddEdge(1,0,4);
            paths0.AddEdge(2,0,1);
            paths0.AddEdge(3, 2, 1);

            paths1 = roads.IsolatedVerticesGraph(true, roads.VerticesCount);
            paths1.AddEdge(0, 2, 1);
            paths1.AddEdge(1,0,4);
            paths1.AddEdge(3, 2, 1);

            paths2 = paths1;
        }





        static void Test1(out Graph roads, out int[] cityCosts, out List<int> result0, out List<int> result1, out List<int> result2, out Graph paths0, out Graph paths1, out Graph paths2)
        {
            int n = 5;

            roads = new AdjacencyMatrixGraph(false, n);
            cityCosts = new int[n];
            cityCosts[0] = 5;
            cityCosts[1] = 10;
            cityCosts[2] = 1;
            cityCosts[3] = 3;
            cityCosts[4] = 4;

            
            result1 = new List<int>();
            result1.Add(40000);
            result1.Add(40000);
            result1.Add(40000);
            result1.Add(40000);
            result1.Add(40000);

            result2 = result1;

            result0 = result1;

            paths0 = roads.IsolatedVerticesGraph(true, roads.VerticesCount);
            paths0.AddEdge(1, 0, 10000);
            paths0.AddEdge(2, 0, 10000);
            paths0.AddEdge(3, 0, 10000);
            paths0.AddEdge(4, 0, 10000);
            paths1 = paths0;
            paths2 = paths0;
        }

        static void Test2(out Graph roads, out int[] cityCosts, out List<int> result0, out List<int> result1, out List<int> result2, out Graph paths0, out Graph paths1, out Graph paths2)
        {
            int n = 3;

            roads = new AdjacencyMatrixGraph(false, n);
            cityCosts = new int[n];
            cityCosts[0] = 5;
            cityCosts[1] = 10;
            cityCosts[2] = 1;

            roads.AddEdge(0, 1, 100);
            roads.AddEdge(1, 2, 5);
            roads.AddEdge(0, 2, 5);

            result0 = new List<int>();
            result0.Add(15);
            result0.Add(15);
            result0.Add(10);

            result1 = new List<int>();
            result1.Add(27);
            result1.Add(22);
            result1.Add(25);

            result2 = new List<int>();
            result2.Add(22);
            result2.Add(12);
            result2.Add(25);

            paths0 = roads.IsolatedVerticesGraph(true,roads.VerticesCount);
            paths0.AddEdge(0, 2, 5);
            paths0.AddEdge(1, 2, 5);

            paths1 = roads.IsolatedVerticesGraph(true, roads.VerticesCount);
            paths1.AddEdge(0, 2, 5);
            paths1.AddEdge(2, 1, 5);

            paths2 = roads.IsolatedVerticesGraph(true, roads.VerticesCount);
            paths2.AddEdge(0,1, 100);
            paths2.AddEdge(2, 1, 5);

        }


        static void Test3(out Graph roads, out int[] cityCosts, out List<int> result0, out List<int> result1, out List<int> result2, out Graph paths0, out Graph paths1, out Graph paths2)
        {
            int n = 5;
            roads = new AdjacencyMatrixGraph(false, n);
            cityCosts = new int[n];
            cityCosts[0] = 5;
            cityCosts[1] = 10;
            cityCosts[2] = 1;
            cityCosts[3] = 7;
            cityCosts[4] = 7;

            roads.AddEdge(0, 1, 100);
            roads.AddEdge(1, 2, 5);
            roads.AddEdge(2, 0, 5);
            roads.AddEdge(3, 4, 5);
            roads.AddEdge(4, 3, 5);

            result0 = new List<int>();
            result0.Add(20015);
            result0.Add(20015);
            result0.Add(20010);
            result0.Add(30005);
            result0.Add(30005);

            result1 = new List<int>();
            result1.Add(20027);
            result1.Add(20022);
            result1.Add(20025);
            result1.Add(30012);
            result1.Add(30012);

            result2 = new List<int>();
            result2.Add(20022);
            result2.Add(20012);
            result2.Add(20025);
            result2.Add(30012);
            result2.Add(30012);

            paths0 = roads.IsolatedVerticesGraph(true, roads.VerticesCount);
            paths0.AddEdge(0, 2, 5);
            paths0.AddEdge(1, 2, 5);
            paths0.AddEdge(3, 2, 10000);
            paths0.AddEdge(4, 2, 10000);


            paths1 = roads.IsolatedVerticesGraph(true, roads.VerticesCount);
            paths1.AddEdge(0, 2, 5);
            paths1.AddEdge(2, 1, 5);
            paths1.AddEdge(3, 1, 10000);
            paths1.AddEdge(4, 1, 10000);
            paths2 = roads.IsolatedVerticesGraph(true, roads.VerticesCount);
            paths2.AddEdge(0, 1, 100);
            paths2.AddEdge(2, 1, 5);
            paths2.AddEdge(3, 1, 10000);
            paths2.AddEdge(4, 1, 10000);
        }

    }
}
