
using System;
using ASD.Graphs;

class Example
{

    public static void Main()
    {
        var ge = new GraphExport();
        var rgg = new RandomGraphGenerator();

        Graph g1 = new AdjacencyMatrixGraph(true, 15);
        g1.AddEdge(0, 2);
        g1.AddEdge(0, 7);
        g1.AddEdge(1, 0);
        g1.AddEdge(2, 1);
        g1.AddEdge(2, 3);
        g1.AddEdge(2, 7);
        g1.AddEdge(3, 5);
        g1.AddEdge(4, 3);
        g1.AddEdge(5, 4);
        g1.AddEdge(5, 6);
        g1.AddEdge(6, 7);
        g1.AddEdge(7, 6);
        g1.AddEdge(7, 8);
        g1.AddEdge(8, 9);
        g1.AddEdge(10, 0);
        g1.AddEdge(12, 13);
        g1.AddEdge(13, 14);
        g1.AddEdge(14, 12);
        //ge.Export(g1);

        int[] scc;
        int n;
        n = g1.StronglyConnectedComponents(out scc);
        Console.WriteLine("\nLiczba silnie spojnych skladowych: {0} (powinno byc 8)", n);
        for (int c = 0; c < n; ++c)
        {
            Console.WriteLine();
            Console.Write("skladowa {0}:", c);
            for (int v = 0; v < g1.VerticesCount; ++v)
                if (scc[v] == c)
                    Console.Write(" {0}", v);
        }
        Console.WriteLine();

        Graph k1 = g1.Kernel();
        //ge.Export(k1);

        rgg.SetSeed(500);
        Graph g2 = rgg.DirectedGraph(typeof(AdjacencyMatrixGraph), 1000, 0.005);
        n = g2.StronglyConnectedComponents(out scc);
        Console.WriteLine("\nLiczba silnie spojnych skladowych: {0} (powinno byc 17)", n);

        Graph g3 = rgg.UndirectedGraph(typeof(AdjacencyMatrixGraph), 1000, 0.1);
        try
        {
            n = g3.StronglyConnectedComponents(out scc);
            Console.WriteLine("\nBlad - powinien byc wyjatek");
        }
        catch (ArgumentException e)
        {
            Console.WriteLine("\n" + e.Message + "  - dobrze");
        }

        Console.WriteLine("\nSciezka maksymalnie powiekszajaca");
        PathsInfo[] d;
        bool b;
        Graph m1 = new AdjacencyMatrixGraph(true, 7);
        m1.AddEdge(0, 2, 10);
        m1.AddEdge(2, 3, 4);
        m1.AddEdge(2, 5, 3);
        m1.AddEdge(2, 4, 7);
        m1.AddEdge(3, 1, 2);
        m1.AddEdge(4, 1, 1);
        m1.AddEdge(5, 6, 5);
        m1.AddEdge(6, 1, 4);
        //ge.Export(m1);

        b = m1.MaxFlowPathsLab05(2, out d);
        Console.WriteLine("graf m1");
        for (int v = 0; v < m1.VerticesCount; ++v)
            Console.WriteLine("przepustowosc od 2 do {0} wynosi {1}", v, d[v].Dist.IsNaN() ? 0 : d[v].Dist);

        int[] p = { 55, 33, 65, 73, 0, 73, 84, 76, 45, 78 };
        rgg.SetSeed(200);
        Graph m2 = rgg.UndirectedGraph(typeof(AdjacencyMatrixGraph), 200, 0.02, 1, 99);
        b = m2.MaxFlowPathsLab05(5, out d);
        Console.WriteLine("graf m2");
        for (int v = 10; v < 20; ++v)
            Console.WriteLine("przepustowosc od 5 do do {0} wynosi {1} ({2})", v, d[v].Dist.IsNaN() ? 0 : d[v].Dist, (d[v].Dist.IsNaN() ? 0 : d[v].Dist) == p[v - 10] ? "OK" : "blad");

        Console.WriteLine("\nTesty Acyklicznosci\n");
        Graph a1, a2, a3, a4, a5;
        bool b1, b2, b3, b4, b5;

        rgg.SetSeed(101);
        a1 = rgg.TreeGraph(typeof(AdjacencyListsGraph<AVLAdjacencyList>), 10, 1);
        //        ge.Export(a1,"a1");
        b1 = a1.IsUndirectedAcyclic();
        Console.WriteLine("Czy graf a1 jest acykliczny ? : {0} (powinno byc True)", b1);

        rgg.SetSeed(102);
        a2 = rgg.TreeGraph(typeof(AdjacencyListsGraph<AVLAdjacencyList>), 15, 1);
        a2.DelEdge(1, 7);
        a2.DelEdge(6, 12);
        //        ge.Export(a2,"a2");
        b2 = a2.IsUndirectedAcyclic();
        Console.WriteLine("Czy graf a2 jest acykliczny ? : {0} (powinno byc True)", b2);

        rgg.SetSeed(103);
        a3 = rgg.UndirectedGraph(typeof(AdjacencyMatrixGraph), 10, 0.3);
        //        ge.Export(a3,"a3");
        b3 = a3.IsUndirectedAcyclic();
        Console.WriteLine("Czy graf a3 jest acykliczny ? : {0} (powinno byc False)", b3);

        rgg.SetSeed(104);
        a4 = rgg.DirectedGraph(typeof(AdjacencyMatrixGraph), 1000, 0.1);
        try
        {
            b4 = a4.IsUndirectedAcyclic();
            Console.WriteLine("Blad - powinien byc wyjatek");
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
        Console.WriteLine("Czy graf a4 jest acykliczny ? (przed chwila powinien byc wyjatek)");

        rgg.SetSeed(105);
        a5 = rgg.UndirectedGraph(typeof(AdjacencyMatrixGraph), 2000, 0.8);
        b5 = a5.IsUndirectedAcyclic();
        Console.WriteLine("Czy graf a5 jest acykliczny ? : {0} (powinno byc False)", b5);

        Console.WriteLine("KONIEC !!!\n");
    }

}
