
using System;
using ASD.Graphs;

class Lab08
{

    public static bool HamiltonTest(Graph g, Edge[] cycle, double len)
    {
        if (g.VerticesCount != cycle.Length) return false;
        double l;
        bool[] v = new bool[g.VerticesCount];
        l = cycle[0].Weight;
        v[cycle[0].From] = true;
        for (int i = 1; i < g.VerticesCount; ++i)
        {
            if (cycle[i].From != cycle[i - 1].To) return false;
            if (v[cycle[i].From]) return false;
            v[cycle[i].From] = true;
            l += cycle[i].Weight;
        }
        return (cycle[0].From == cycle[g.VerticesCount - 1].To) && l == len;
    }

    public static void Test(Graph g, Edge[] c, double m)
    {
        if (m.IsNaN() && c == null)
        {
            Console.WriteLine("nie znaleziono cyklu Hamiltona");
            return;
        }
        if (m.IsNaN() || c == null)
        {
            Console.WriteLine("BLAD 1  !!!");
            return;
        }
        if (HamiltonTest(g, c, m))
            Console.WriteLine("znaleziono cykl Hamiltona dlugosci {0}", m);
        else
            Console.WriteLine("BLAD 2  !!!");
    }

    public static void Main()
    {
        double m;
        Graph g0, g1, g2, g3, g4, g5, g6, g7;
        Edge[] c;

        RandomGraphGenerator gen = new RandomGraphGenerator(1);
        g0 = gen.UndirectedEuclidGraph(typeof(AdjacencyMatrixGraph), 5, 1.0, 0.0, 100.0, 0.0, 100.0);
        g1 = gen.UndirectedEuclidGraph(typeof(AdjacencyMatrixGraph), 100, 1.0, 0.0, 100.0, 0.0, 100.0);
        g2 = gen.UndirectedGraph(typeof(AdjacencyMatrixGraph), 100, 1.0, 1, 99);
        g3 = gen.DirectedGraph(typeof(AdjacencyMatrixGraph), 100, 1.0, 1, 99);
        g4 = gen.UndirectedGraph(typeof(AdjacencyMatrixGraph), 100, 0.9, 1, 99);
        g5 = gen.DirectedGraph(typeof(AdjacencyMatrixGraph), 100, 0.9, 1, 99);
        g6 = gen.UndirectedGraph(typeof(AdjacencyListsGraph<AVLAdjacencyList>), 100, 0.2, 1, 99);
        g7 = gen.DirectedGraph(typeof(AdjacencyListsGraph<AVLAdjacencyList>), 100, 0.2, 1, 99);

        Console.WriteLine("\nAlgorytm \"Kruskalopodobny\"");

        Console.Write("  maly graf euklidesowy     -  ");
        m = g0.TSP_Kruskal(out c);
        Test(g0, c, m);
        if (m.IsNaN())
            Console.WriteLine("    Nie znaleziono cyklu Hamiltona");
        else
        {
            Console.Write("    ");
            for (int i = 0; i < g0.VerticesCount; ++i)
                Console.Write("  {0}", c[i]);
            Console.WriteLine();
        }

        Console.Write("  graf pelny euklidesowy    -  ");
        m = g1.TSP_Kruskal(out c);
        Test(g1, c, m);

        Console.Write("  graf pelny nieskierowany  -  ");
        m = g2.TSP_Kruskal(out c);
        Test(g2, c, m);

        Console.Write("  graf pelny skierowany     -  ");
        m = g3.TSP_Kruskal(out c);
        Test(g3, c, m);

        Console.Write("  graf nieskierowany        -  ");
        m = g4.TSP_Kruskal(out c);
        Test(g4, c, m);

        Console.Write("  graf skierowany           -  ");
        m = g5.TSP_Kruskal(out c);
        Test(g5, c, m);

        Console.Write("  graf rzadki nieskierowany -  ");
        m = g6.TSP_Kruskal(out c);
        Test(g6, c, m);

        Console.Write("  graf rzadki skierowany    -  ");
        m = g7.TSP_Kruskal(out c);
        Test(g7, c, m);

        Console.WriteLine("\nAlgorytm na podstawie drzewa");

        Console.Write("  maly graf euklidesowy     -  ");
        m = g0.TSP_TreeBased(out c);
        Test(g0, c, m);
        if (m.IsNaN())
            Console.WriteLine("    Nie znaleziono cyklu Hamiltona");
        else
        {
            Console.Write("    ");
            for (int i = 0; i < g0.VerticesCount; ++i)
                Console.Write("  {0}", c[i]);
            Console.WriteLine();
        }

        Console.Write("  graf pelny euklidesowy    -  ");
        m = g1.TSP_TreeBased(out c);
        Test(g1, c, m);

        Console.Write("  graf pelny nieskierowany  -  ");
        m = g2.TSP_TreeBased(out c);
        Test(g2, c, m);

        Console.Write("  graf pelny skierowany     -  ");
        try
        {
            m = g3.TSP_TreeBased(out c);
            Console.WriteLine("BLAD 3  !!!");
        }
        catch (ArgumentException)
        {
            Console.WriteLine("wyjatek (to dobrze)");
        }

        Console.Write("  graf nieskierowany        -  ");
        m = g4.TSP_TreeBased(out c);
        Test(g4, c, m);

        Console.Write("  graf skierowany           -  ");
        try
        {
            m = g5.TSP_TreeBased(out c);
            Console.WriteLine("BLAD 3  !!!");
        }
        catch (ArgumentException)
        {
            Console.WriteLine("wyjatek (to dobrze)");
        }

        Console.Write("  graf rzadki nieskierowany -  ");
        m = g6.TSP_TreeBased(out c);
        Test(g6, c, m);

        Console.Write("  graf rzadki skierowany    -  ");
        try
        {
            m = g7.TSP_TreeBased(out c);
            Console.WriteLine("BLAD 3  !!!");
        }
        catch (ArgumentException)
        {
            Console.WriteLine("wyjatek (to dobrze)");
        }
    }

}
