using System;
using ASD.Graphs;
using System.Linq;
using System.Collections.Generic;

namespace lab7
{
    class Test
    {
        public Graph graph;
        public int solutionSize;
        public int solutionsCount;

        public Test(Graph graph, int solutionSize, int solutionsCount)
        {
            this.graph = graph;
            this.solutionSize = solutionSize;
            this.solutionsCount = solutionsCount;
        }

        public bool CheckSolution(int[] solution)
        {
            for (int i = 0; i < graph.VerticesCount; i++)
            {
                foreach (Edge e in graph.OutEdges(i))
                    if (!(solution.Contains(e.From) || solution.Contains(e.To)))
                        return false;
            }
            return true;
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            AdjacencyListsGraph<SimpleAdjacencyList> star
                = new AdjacencyListsGraph<SimpleAdjacencyList>(false, 10);
            for (int i = 0; i < 9; i++)
            {
                star.AddEdge(i, 9);
            }

            var evenCSmall = new AdjacencyListsGraph<SimpleAdjacencyList>(false, 10);
            for (int i = 0; i < evenCSmall.VerticesCount; i++)
            {
                evenCSmall.AddEdge(i, (i + 1) % evenCSmall.VerticesCount);
            }

            var oddCSmall = new AdjacencyListsGraph<SimpleAdjacencyList>(false, 11);
            for (int i = 0; i < oddCSmall.VerticesCount; i++)
            {
                oddCSmall.AddEdge(i, (i + 1) % oddCSmall.VerticesCount);
            }

            RandomGraphGenerator rgg = new RandomGraphGenerator(12345);

            var biparSmall = rgg.BipariteGraph(typeof(AdjacencyMatrixGraph),9,9,1);


            var hypercube = new AdjacencyListsGraph<SimpleAdjacencyList>(false, 32);
            for (int i = 0; i < hypercube.VerticesCount; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (NumberOfBits(i ^ j) == 1)
                        hypercube.AddEdge(i, j);
                }
            }

            var oddC = new AdjacencyListsGraph<SimpleAdjacencyList>(false, 31);
            for (int i = 0; i < oddC.VerticesCount; i++)
            {
                oddC.AddEdge(i, (i + 2) % oddC.VerticesCount);
            }

            AdjacencyMatrixGraph full = new AdjacencyMatrixGraph(false, 50);
            for (int i = 0; i < full.VerticesCount; i++)
                for (int j = 0; j < i; j++)
                    full.AddEdge(i, j);


            Test[] tests = { new Test(star, 1, 1), new Test(evenCSmall, 5, 2), new Test(oddCSmall, 6, 11), new Test(biparSmall, 9, 2) };
            Test[] testLarge = { new Test(hypercube, 16, 2), new Test(oddC, 16, 31), new Test(full, 49, 50)};

            Console.Out.WriteLine("Ma³e testy");
            for (int i = 0; i < tests.Length; i++)
            {
                PerformTest(i, tests[i]);
            }

            Console.Out.WriteLine("Du¿e testy");
            for (int i = 0; i < testLarge.Length; i++)
            {
                PerformTest(i, testLarge[i]);
            }
        }

        private static int NumberOfBits(int p)
        {
            int n=0;
            while (p != 0)
            {
                n += p & 1;
                p >>= 1;
            }
            return n;
        }

        static bool PerformTest(int num, Test test)
        {
            List<int[]> solutions = test.graph.GardenPlanner();
            bool solutionsCorrect = solutions.Count > 0;
            bool sizeCorrect = solutionsCorrect;

            foreach (var solution in solutions)
            {
                solutionsCorrect &= test.CheckSolution(solution);
                sizeCorrect &= solution.Length == test.solutionSize;
                if (solution.Length != test.solutionSize)
                    Console.Out.WriteLine("Test {0}: Rozwi¹zanie o rozmiarze {1}, oczekiwane {2}", num, solution.Length, test.solutionSize);
            }

            if (sizeCorrect && solutionsCorrect && solutions.Count != test.solutionsCount)
            {
                Console.Out.WriteLine("Test {0}: znalezione rozwi¹zenie poprawne, Z£A LIBA ROZWI¥ZAÑ {1}, oczekiwane {2}", num, solutions.Count, test.solutionsCount);
                return false;
            }
            if (sizeCorrect && solutionsCorrect)
            {
                Console.Out.WriteLine("Test {0}: znalezione rozwi¹zenie poprawne, liczba OK -- {1}", num, solutions.Count);
                return true;
            }
            if (!sizeCorrect)
                Console.Out.WriteLine("Test {0}: nieoptymalne rozwi¹zanie. Oczekiwany rozmiar: {1}", num, test.solutionSize);
            if (!solutionsCorrect)
                Console.Out.WriteLine("Test {0}: b³êdne rozwi¹zanie.", num);
            return false;
        }
    }
}
