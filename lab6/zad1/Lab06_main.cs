
using System;
using ASD.Graphs;

class Lab06
{

    private static Graph[] cliq_test;
    private static Graph[,] izo_test;
    private static int[] cliq_res;
    private static bool[] izo_res;

    public static void Main()
    {
        var ge = new GraphExport();
        int[] clique, map;
        bool izo;
        int n, ec, ec0, ec1;

        PrepareTests();

        Console.WriteLine();
        Console.WriteLine("Clique Tests");
        for (int i = 0; i < cliq_test.Length; ++i)
        {
            try
            {
                ec = cliq_test[i].EdgesCount;
                n = cliq_test[i].CliqueNumber(out clique);
                Console.WriteLine("Test {0}:  {1} ", i + 1, CliqueTest(cliq_test[i], n, clique) && n == cliq_res[i] && ec == cliq_test[i].EdgesCount);
                if (i > 0) continue;
                //ge.Export(cliq_test[i], null, "clique");
                Console.Write("  [");
                foreach (var v in clique)
                    Console.Write(" {0} ", v);
                Console.WriteLine("]");
            }
            catch (Exception e)
            {
                Console.WriteLine("Test {0}:  {1} ", i + 1, e.Message);
            }
        }

        Console.WriteLine();
        Console.WriteLine("Izomorpism Tests");
        for (int i = 0; i < izo_test.GetLength(0); ++i)
        {
            try
            {
                ec0 = izo_test[i, 0].EdgesCount;
                ec1 = izo_test[i, 1].EdgesCount;
                izo = izo_test[i, 0].IsIzomorpchic(izo_test[i, 1], out map);    // The result is correct (true, false, true, false), only the checking function is wrong
                int[] libraryIsomorphism = izo_test[i, 0].Isomorpchism(izo_test[i, 1]); // This fails every time, I don't know why
                Console.WriteLine("Test {0}:  {1} ", i + 1, (izo ? izo_res[i] && IzomorphismTest(izo_test[i, 0], izo_test[i, 1], map) == izo_res[i] : !izo_res[i])
                                                          && ec0 == izo_test[i, 0].EdgesCount && ec1 == izo_test[i, 1].EdgesCount);
            }
            catch (Exception e)
            {
                Console.WriteLine("Test {0}:  {1} ", i + 1, e.Message);
            }
        }

        Console.WriteLine();
    }

    private static bool IzomorphismTest(Graph g, Graph h, int[] map)
    {
        return h.IsIsomorphic(g, map);
    }

    public static void PrepareTests()
    {
        var rgg = new RandomGraphGenerator();

        cliq_test = new Graph[6];
        izo_test = new Graph[4, 2];

        cliq_res = new int[] { 4, 20, 19, 19, 9, 3 };
        izo_res = new bool[] { true, false, true, false };

        if (cliq_test.Length != cliq_res.Length || izo_test.GetLongLength(0) != izo_res.Length)
            throw new ApplicationException("Zle zddefiniowane testy");

        cliq_test[0] = new AdjacencyMatrixGraph(false, 8);
        cliq_test[0].AddEdge(0, 4);
        cliq_test[0].AddEdge(0, 7);
        cliq_test[0].AddEdge(1, 2);
        cliq_test[0].AddEdge(1, 3);
        cliq_test[0].AddEdge(1, 5);
        cliq_test[0].AddEdge(1, 6);
        cliq_test[0].AddEdge(2, 5);
        cliq_test[0].AddEdge(2, 6);
        cliq_test[0].AddEdge(3, 4);
        cliq_test[0].AddEdge(3, 7);
        cliq_test[0].AddEdge(4, 7);
        cliq_test[0].AddEdge(5, 6);

        cliq_test[1] = new AdjacencyMatrixGraph(false, 20);
        for (int i = 0; i < cliq_test[1].VerticesCount; ++i)
            for (int j = i + 1; j < cliq_test[1].VerticesCount; ++j)
                cliq_test[1].AddEdge(i, j);

        cliq_test[2] = cliq_test[1].Clone();
        cliq_test[2].DelEdge(0, 1);

        cliq_test[3] = cliq_test[2].Clone();
        cliq_test[3].DelEdge(0, 2);

        rgg.SetSeed(123);
        cliq_test[4] = rgg.DirectedGraph(typeof(AdjacencyMatrixGraph), 100, 0.7);

        cliq_test[5] = rgg.UndirectedGraph(typeof(AdjacencyListsGraph<AVLAdjacencyList>), 5000, 0.001);

        izo_test[0, 0] = cliq_test[0].Clone();
        izo_test[0, 1] = rgg.Permute(izo_test[0, 0]);

        int n = 50;
        izo_test[1, 0] = new AdjacencyMatrixGraph(true, n);
        for (int i = 0; i < n; ++i)
            for (int j = 0; j < n; ++j)
                if (i != j)
                    izo_test[1, 0].AddEdge(i, j);
        izo_test[1, 1] = izo_test[1, 0].Clone();
        for (int i = 0; i < n; ++i)
            izo_test[1, 0].DelEdge(i, (i + 1) % n);
        for (int i = 0; i < n; i += 2)
        {
            izo_test[1, 1].DelEdge(i, (i + 2) % n);
            izo_test[1, 1].DelEdge(i + 1, (i + 3) % n);
        }

        rgg.SetSeed(1234);
        izo_test[2, 0] = rgg.DirectedGraph(typeof(AdjacencyMatrixGraph), 50, 0.95, 1, 999);
        izo_test[2, 1] = new AdjacencyListsGraph<AVLAdjacencyList>(izo_test[2, 0]);
        izo_test[2, 1] = rgg.Permute(izo_test[2, 1]);

        izo_test[3, 0] = rgg.UndirectedGraph(typeof(AdjacencyListsGraph<AVLAdjacencyList>), 15, 0.01, 1, 3);
        izo_test[3, 0].AddEdge(izo_test[3, 0].VerticesCount / 2, izo_test[3, 0].VerticesCount / 2 + 1, 2);
        izo_test[3, 1] = izo_test[3, 0].Clone();
        izo_test[3, 1].ModifyEdgeWeight(izo_test[3, 0].VerticesCount / 2, izo_test[3, 0].VerticesCount / 2 + 1, 1);

    }

    public static bool CliqueTest(Graph g, int cn, int[] cl)
    {
        if (cl == null || cn != cl.Length) return false;
        for (int i = 0; i < cl.Length; ++i)
            for (int j = i + 1; j < cl.Length; ++j)
                if (g.GetEdgeWeight(cl[i], cl[j]).IsNaN())
                    return false;
        return true;
    }

}
