using System;
using System.Linq;
using ASD.Graph;

namespace zadanie8
{
    class Program
    {

        private static int CheckColoring(IGraph g, int[] colors)
        {
            if (colors==null)
            {
                Console.WriteLine("Graf nie jest pokolorowany !!!");
                return 0;
            }
            int n = g.VerticesCount;
            bool[] used = new bool[n+1];

            for (int i = 0; i < n; ++i)
                if (colors[i] == 0)
                {
                    Console.WriteLine("Wierzchołek {0} jest niepokolorowany!", i);
                }

            for (int i = 0; i < n; ++i)
                if (colors[i] < 0 || colors[i] > n)
                {
                    Console.WriteLine("Wierzchołek {0} jest pokolorowany nieprawidłowym kolorem {1} !", i, colors[i]);
                }
                else
                {
                    used[colors[i]] = true;
                }
            if ( colors.Max()!=used.Count(x=>x) )
                Console.WriteLine("Nieprawidłowe kolorowanie!");

            for (int i = 0; i < n; ++i)
                foreach (var e in g.OutEdges(i))
                {
                    if (colors[i] == colors[e.To])
                    {
                        Console.WriteLine("Konflikt na krawędzi ({0},{1})!", i, e.To);
                    }
                }
            return colors.Max();
        }


        static void TestSG(IGraph[] graphs, int[] expected)
        {
            int cols;
            int[] colors;
            IGraph g;

            Console.WriteLine("----------------------");
            Console.WriteLine("----SIMPLE GREEDY-----");
            Console.WriteLine("----------------------");

            for (int i=0; i< graphs.Length; ++i)
            {
                Console.WriteLine("Test {0}:",i+1);
                g=graphs[i].Clone();
                try
                {
                    colors = graphs[i].SimpleGreedy();
                    if ( !g.IsEqual(graphs[i]) )
                        Console.WriteLine("zmieniono graf!");
                    cols = CheckColoring(graphs[i], colors);
                }
                catch ( Exception e )
                {
                    Console.WriteLine("Wyjątek: {0}", e.Message);
                    cols=0;
                }
                Console.WriteLine("Użyto kolorów: {0}, oczekiwano {1}", cols, expected[i]);
            }
        }
        static void TestSL(IGraph[] graphs, int[] expected)
        {
            int cols;
            int[] colors;
            IGraph g;

            Console.WriteLine("----------------------");
            Console.WriteLine("----SMALLEST LAST-----");
            Console.WriteLine("----------------------");

            for (int i = 0; i < graphs.Length; ++i)
            {
                Console.WriteLine("Test {0}:",i+1);
                g=graphs[i].Clone();
                try
                {
                    colors = graphs[i].SmallestLast();
                    if ( !g.IsEqual(graphs[i]) )
                        Console.WriteLine("zmieniono graf!");
                    cols = CheckColoring(graphs[i], colors);
                }
                catch ( Exception e )
                {
                    Console.WriteLine("Wyjątek: {0}", e.Message);
                    cols=0;
                }
                Console.WriteLine("Użyto kolorów: {0}, oczekiwano {1}", cols, expected[i]);
            }
        }
        static void TestDS(IGraph[] graphs, int[] expected)
        {
            int cols;
            int[] colors;
            IGraph g;

            Console.WriteLine("----------------------");
            Console.WriteLine("--------DSATUR--------");
            Console.WriteLine("----------------------");

            for (int i = 0; i < graphs.Length; ++i)
            {
                Console.WriteLine("Test {0}:",i+1);
                g=graphs[i].Clone();
                try
                {
                    colors = graphs[i].DSatur();
                    if ( !g.IsEqual(graphs[i]) )
                        Console.WriteLine("zmieniono graf!");
                    cols = CheckColoring(graphs[i], colors);
                }
                catch ( Exception e )
                {
                    Console.WriteLine("Wyjątek: {0}", e.Message);
                    cols=0;
                }
                Console.WriteLine("Użyto kolorów: {0}, oczekiwano {1}", cols, expected[i]);
            }
        }
        static void TestInc(IGraph[] graphs, int[] expected)
        {
            int cols;
            int[] colors;
            IGraph g;

            Console.WriteLine("----------------------");
            Console.WriteLine("------INCREMENTAL-----");
            Console.WriteLine("----------------------");

            for (int i = 0; i < graphs.Length; ++i)
            {
                Console.WriteLine("Test {0}:",i+1);
                g=graphs[i].Clone();
                try
                {
                    colors = graphs[i].Incremental();
                    if ( !g.IsEqual(graphs[i]) )
                        Console.WriteLine("zmieniono graf!");
                    cols = CheckColoring(graphs[i], colors);
                }
                catch ( Exception e )
                {
                    Console.WriteLine("Wyjątek: {0}", e.Message);
                    cols=0;
                }
                Console.WriteLine("Użyto kolorów: {0}, oczekiwano {1}", cols, expected[i]);
            }
        }

        static void Main(string[] args)
        {
            Random rnd = new Random();
            IGraph g1, g2, g3, g4, g5;
            
            // test 1 -- graf pełny
            g1 = new AdjacencyMatrixGraph(false, 20);
            for (int i = 0; i < 20; ++i)
                for (int j = i + 1; j < 20; ++j)
                    g1.AddEdge(i, j);

            //test 2 -- graf pusty
            g2 = new AdjacencyMatrixGraph(false, 20);

            // test 3
            g3 = new AdjacencyMatrixGraph(false, 8);

            for (int i = 1; i < 6; ++i)
                g3.AddEdge(i - 1, i);
            g3.AddEdge(6, 1);
            g3.AddEdge(7, 4);

            // test 4 -- K_n,n - skojarzenie doskonałe, nieparzyste i parzyste w osobnych klasach dwudzielności

            g4 = new AdjacencyMatrixGraph(false, 20);
            for (int i = 0; i < 10; ++i)
                for (int j = 0; j < 10; ++j)
                    if (i != j)
                        g4.AddEdge(2 * i, 2 * j + 1);

            // test 5 -- prismoid - przypadek dla SL

            g5 = new AdjacencyMatrixGraph(false, 8);

            g5.AddEdge(0, 1);
            g5.AddEdge(0, 2);
            g5.AddEdge(0, 4);
            g5.AddEdge(0, 6);
            g5.AddEdge(1, 2);
            g5.AddEdge(1, 3);
            g5.AddEdge(1, 7);
            g5.AddEdge(2, 3);
            g5.AddEdge(2, 4);
            g5.AddEdge(3, 5);
            g5.AddEdge(3, 7);
            g5.AddEdge(4, 5);
            g5.AddEdge(4, 6);
            g5.AddEdge(5, 6);
            g5.AddEdge(5, 7);
            g5.AddEdge(6, 7);
            
            IGraph[] graphs = new IGraph[] { g1, g2, g3, g4, g5 };
            int[] expected_sg = new int[] { 20, 1, 2, 10, 5 };
            int[] expected_sl = new int[] { 20, 1, 2, 2, 5 };
            int[] expected_ds = new int[] { 20, 1, 2, 2, 5 };
            int[] expected_inc = new int[] { 20, 1, 2, 10, 5 };

            Console.WriteLine("Liczba chromatyczna grafów testowych:");
            Console.WriteLine("Test 1. 20\nTest 2. 1\nTest 3. 2\nTest 4. 2\nTest 5. 4\n");
            TestSG(graphs, expected_sg);
            Console.WriteLine();
            TestSL(graphs, expected_sl);
            Console.WriteLine();
            TestDS(graphs, expected_ds);
            Console.WriteLine();
            TestInc(graphs, expected_inc);
            Console.WriteLine();
        }
    }
}
