
using System;
using ASD.Graphs;

class Lab09
{
    public static void Main()
    {
        int[] backtrackingColors;
        int[] greedyColors;
        int n, i, j, mb, mg;
        ulong counter0, counter1, counter2;
        string[] message1 = { "Zwykly maly graf:",
                              "Maly dwudzielny:",
                              "Mala klika:" };
        int[] bestColorsNumbers1 = { 4, 2, 9 };
        string[] message2 = { "Zwykly graf:",
                              "Graf dwudzielny:",
                              "Cykl parzysty:",
                              "Klika:" };
        int[] bestColorsNumbers2 = { 6, 2, 2, 200 };
        string[] message3 = { "Zwykly duzy graf:",
                              "Duzy dwudzielny:",
                              "Duza klika:" };
        int[] bestColorsNumbers3 = { 59, 2, 4000 };
        Graph[] g1 = new Graph[message1.Length];
        Graph[] g2 = new Graph[message2.Length];
        Graph[] g3 = new Graph[message3.Length];
        var rgg = new RandomGraphGenerator();

        Console.WriteLine();
        Console.WriteLine("Generowanie grafow");
        Console.WriteLine();

        rgg.SetSeed(101);
        g1[0] = rgg.UndirectedGraph(typeof(AdjacencyMatrixGraph), 8, 0.5);
        rgg.SetSeed(102);
        g1[1] = rgg.BipariteGraph(typeof(AdjacencyMatrixGraph), 5, 3, 0.75);
        n = 9;
        g1[2] = new AdjacencyMatrixGraph(false, n);
        for (i = 0; i < n; ++i)
            for (j = i + 1; j < n; ++j)
                g1[2].AddEdge(i, j);

        rgg.SetSeed(103);
        g2[0] = rgg.UndirectedGraph(typeof(AdjacencyMatrixGraph), 20, 0.5);
        rgg.SetSeed(104);
        g2[1] = rgg.BipariteGraph(typeof(AdjacencyMatrixGraph), 30, 20, 0.25);
        n = 50;
        g2[2] = new AdjacencyListsGraph<AVLAdjacencyList>(false, n);
        for (i = 1; i < n; ++i)
            g2[2].AddEdge(i - 1, i);
        g2[2].AddEdge(n - 1, 0);
        rgg.SetSeed(105);
        g2[2] = rgg.Permute(g2[2]);
        n = 200;
        g2[3] = new AdjacencyMatrixGraph(false, n);
        for (i = 0; i < n; ++i)
            for (j = i + 1; j < n; ++j)
                g2[3].AddEdge(i, j);

        rgg.SetSeed(106);
        g3[0] = rgg.UndirectedGraph(typeof(AdjacencyMatrixGraph), 75, 0.99);
        rgg.SetSeed(107);
        g3[1] = rgg.BipariteGraph(typeof(AdjacencyMatrixGraph), 2000, 2000, 0.55);
        n = 5000;
        g3[2] = new AdjacencyMatrixGraph(false, n);
        for (i = 0; i < n; ++i)
            for (j = i + 1; j < n; ++j)
                g3[2].AddEdge(i, j);

        Console.WriteLine("Grafy za 1 pkt");
        Console.WriteLine();
        for (i = 0; i < g1.Length; ++i)
        {
            counter0 = Graph.Counter;
            mb = g1[i].BacktrackingColor(out backtrackingColors);
            counter1 = Graph.Counter;
            mg = g1[i].GreedyColor(out greedyColors);
            counter2 = Graph.Counter;
            Console.WriteLine("{0,-17}  liczba wierzcholkow  {1,4},  optymalna liczba kolorow  {2,4}", message1[i], g1[i].VerticesCount, bestColorsNumbers1[i]);
            Console.WriteLine("  Backtracking:    liczba kolorow  {0,4},  zlozonosc  {1,8}", mb, counter1 - counter0);
            Console.WriteLine("  Greedy:          liczba kolorow  {0,4},  zlozonosc  {1,8}", mg, counter2 - counter1);
            Console.WriteLine();
        }

        //Console.WriteLine("Grafy za 2 pkt");
        //Console.WriteLine();
        //for (i = 0; i < g2.Length; ++i)
        //{
        //    counter0 = Graph.Counter;
        //    mb = g2[i].BacktrackingColor(out backtrackingColors);
        //    counter1 = Graph.Counter;
        //    mg = g2[i].GreedyColor(out greedyColors);
        //    counter2 = Graph.Counter;
        //    Console.WriteLine("{0,-17}  liczba wierzcholkow  {1,4},  optymalna liczba kolorow  {2,4}", message2[i], g2[i].VerticesCount, bestColorsNumbers2[i]);
        //    Console.WriteLine("  Backtracking:    liczba kolorow  {0,4},  zlozonosc  {1,8}", mb, counter1 - counter0);
        //    Console.WriteLine("  Greedy:          liczba kolorow  {0,4},  zlozonosc  {1,8}", mg, counter2 - counter1);
        //    Console.WriteLine();
        //}

        //Console.WriteLine("Grafy za 3 pkt");
        //Console.WriteLine();
        //for (i = 0; i < g3.Length; ++i)
        //{
        //    counter0 = Graph.Counter;
        //    mb = g3[i].BacktrackingColor(out backtrackingColors);
        //    counter1 = Graph.Counter;
        //    mg = g3[i].GreedyColor(out greedyColors);
        //    counter2 = Graph.Counter;
        //    Console.WriteLine("{0,-17}  liczba wierzcholkow  {1,4},  optymalna liczba kolorow  {2,4}", message3[i], g3[i].VerticesCount, bestColorsNumbers3[i]);
        //    Console.WriteLine("  Backtracking:    liczba kolorow  {0,4},  zlozonosc  {1,8}", mb, counter1 - counter0);
        //    Console.WriteLine("  Greedy:          liczba kolorow  {0,4},  zlozonosc  {1,8}", mg, counter2 - counter1);
        //    Console.WriteLine();
        //}

        Console.WriteLine("Koniec");
        Console.WriteLine();
    }

}
