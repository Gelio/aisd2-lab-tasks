using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASD.Graph;

namespace MatrixRounding
{
    class MatrixRoundingMain
    {
        private class CirculationTestCase
        {
            public IGraph TestGraph { get; set; }
            public IGraph LowerBounds { get; set; }
            public int[] Demands { get; set; }
            public bool ExpectedResult { get; set; }
        }

        static void Main(string[] args)
        {
            List<CirculationTestCase> testCases = GetTestCases();
            Console.WriteLine("Circulation Tests:");

            foreach (var testCase in testCases)
            {
                IGraph circulation;
                bool result;
                if (testCase.LowerBounds == null)
                {
                    result = testCase.TestGraph.FindCirculation(testCase.Demands, out circulation);
                }
                else
                {
                    result = testCase.TestGraph.FindCirculationWithLowerBounds(testCase.Demands, testCase.LowerBounds, out circulation);
                }

                if (VerifyResult(result, circulation, testCase))
                {
                    Console.WriteLine("{0} - Test PASSED", testCase.LowerBounds == null ? "No LowerBound" : "With LowerBound");
                }
                else
                {
                    Console.WriteLine("{0} - Test FAILED", testCase.LowerBounds == null ? "No LowerBound" : "With LowerBound");
                }
            }

            Console.WriteLine("MatrixRounding Tests:");

            List<double[,]> testMatrixes = new List<double[,]>()
                { 
                    new double[,] { { 3.14, 6.8, 7.3 }, { 9.6, 2.4, 0.7 }, { 3.6, 1.2, 6.5 } },
                    new double[,] { { 3.1, 6.6, 7.3 }, { 9.6, 2.4, 0.7 }, { 3.3, 1.2, 6.5 } },
                    new double[,] { { 3.14, 6.8, 7.3, 4.6 }, { 9.6, 2.4, 0.7, 9.5 }, { 3.6, 1.2, 6.5, 1.2 } },
                    new double[,] { { 0.1, 1.1 }, { 0.1, 0.2 }, { 0.1, 1.2 } },
                };

            foreach (var matrix in testMatrixes)
            {
                RunTest(matrix);
            }

//            Console.ReadLine();
        }

        private static bool VerifyResult(bool result, IGraph circulation, CirculationTestCase testCase)
        {
            if (result != testCase.ExpectedResult)
            {
                Console.WriteLine("Wrong result");
                return false;
            }

            if (!result)
            {
                return true;
            }

            int[] flowValues = new int[circulation.VerticesCount];

            for (int v = 0; v < circulation.VerticesCount; v++)
            {
                foreach (var e in circulation.OutEdges(v))
                {
                    if (testCase.LowerBounds != null && testCase.LowerBounds.GetEdgeWeight(e.From, e.To) > e.Weight)
                    {
                        Console.WriteLine("Wrong flow values - lower bound not met");
                        return false;
                    }

                    flowValues[e.From] -= e.Weight;
                    flowValues[e.To] += e.Weight;
                }
            }

            for (int v = 0; v < testCase.TestGraph.VerticesCount; v++)
            {
                if (flowValues[v] != testCase.Demands[v])
                {
                    Console.WriteLine("Wrong flow values - demand not met");
                    return false;
                }
            }

            return true;
        }

        private static List<CirculationTestCase> GetTestCases()
        {
            List<CirculationTestCase> testCases = new List<CirculationTestCase>();

            IGraph testGraph1 = Graph.IsolatedVerticesGraph(true, 6, typeof(AdjacencyMatrixGraph));
            testGraph1.AddEdge(0, 1, 10);
            testGraph1.AddEdge(0, 2, 3);
            testGraph1.AddEdge(1, 2, 6);
            testGraph1.AddEdge(3, 2, 7);
            testGraph1.AddEdge(1, 4, 7);
            testGraph1.AddEdge(4, 3, 4);
            testGraph1.AddEdge(3, 5, 9);
            testGraph1.AddEdge(4, 5, 4);

            int[] demands1 = new int[6] { -7, -8, 10, -6, 0, 11 };

            testCases.Add(new CirculationTestCase() { TestGraph = testGraph1, Demands = demands1, ExpectedResult = true });

            var rgg = new RandomGraphGenerator();
            var random = new Random(0);

            var testGraph2 = rgg.DirectedGraph(typeof(AdjacencyMatrixGraph), 10, 0.7, 1, 20);
            int[] demands2 = new[] { -4, -6, 2, 7, 0, -5, 8, 0, -2, 0 };
            int[] demands3 = new[] { -4, -6, 2, 7, 0, -5, 8, 0, -3, -5 };

            testCases.Add(new CirculationTestCase() { TestGraph = testGraph2, Demands = demands2, ExpectedResult = true });
            testCases.Add(new CirculationTestCase() { TestGraph = testGraph2, Demands = demands3, ExpectedResult = false });

            var lowerBounds1 = testGraph1.IsolatedVerticesGraph();
            lowerBounds1.AddEdge(0, 1, 8);

            testCases.Add(new CirculationTestCase() { TestGraph = testGraph1, Demands = demands1, LowerBounds= lowerBounds1, ExpectedResult = false });

            var lowerBounds2 = testGraph1.IsolatedVerticesGraph();
            lowerBounds2.AddEdge(0, 1, 4);
            lowerBounds2.AddEdge(0, 2, 3);
            lowerBounds2.AddEdge(1, 2, 6);
            lowerBounds2.AddEdge(3, 2, 1);
            lowerBounds2.AddEdge(1, 4, 6);
            lowerBounds2.AddEdge(4, 3, 2);
            lowerBounds2.AddEdge(3, 5, 7);
            lowerBounds2.AddEdge(4, 5, 4);

            testCases.Add(new CirculationTestCase() { TestGraph = testGraph1, Demands = demands1, LowerBounds = lowerBounds2, ExpectedResult = true });

            var lowerBounds3 = testGraph2.Clone();
            for (int v = 0; v < lowerBounds3.VerticesCount; v++)
            {
                foreach (var e in lowerBounds3.OutEdges(v))
                {
                    lowerBounds3.ModifyEdgeWeight(e.From, e.To, -e.Weight + 1);
                }
            }

            testCases.Add(new CirculationTestCase() { TestGraph = testGraph2, Demands = demands2, LowerBounds = lowerBounds3, ExpectedResult = true });


            return testCases;
        }

        private static void RunTest(double[,] matrix)
        {
            double[] rowSums = GetSums(matrix, SumDirection.Row, (x, y) => x + y);
            double[] columnSums = GetSums(matrix, SumDirection.Column, (x, y) => x + y);

            int[,] roundedMatrix = CirculationGraphExtender.RoundMatrix(matrix);

            // Odkomentować aby wyświetlić macierz - do debugowania
            // WriteMatrix(matrix, rowSums, columnSums);

            int[] roundedRowSums = GetSums(roundedMatrix, SumDirection.Row, (x, y) => x + y);
            int[] roundedColumnSums = GetSums(roundedMatrix, SumDirection.Column, (x, y) => x + y);

            // Odkomentować aby wyświetlić macierz - do debugowania
            // WriteMatrix(roundedMatrix, roundedRowSums, roundedColumnSums);

            if (!VerifyTest(matrix, roundedMatrix, rowSums, columnSums, roundedRowSums, roundedColumnSums))
            {
                Console.WriteLine("Test FAILED!!!");
            }
            else
            {
                Console.WriteLine("Test PASSED");
            }
        }

        private static bool VerifyTest(double[,] matrix, int[,] roundedMatrix, double[] rowSums, double[] columnSums, int[] roundedRowSums, int[] roundedColumnSums)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (!IsRound(matrix[i, j], roundedMatrix[i, j]))
                    {
                        return false;
                    }
                }
            }

            for (int i = 0; i < rowSums.Length; i++)
            {
                if (!IsRound(rowSums[i], roundedRowSums[i]))
                {
                    return false;
                }
            }

            for (int i = 0; i < columnSums.Length; i++)
            {
                if (!IsRound(columnSums[i], roundedColumnSums[i]))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsRound(double original, int rounded)
        {
            return rounded == (int)Math.Floor(original) ||
                   rounded == (int)Math.Ceiling(original);
        }

        private static T[] GetSums<T>(T[,] matrix, SumDirection sumDirection, Func<T, T, T> add)
        {
            T[] sums = new T[matrix.GetLength(sumDirection == SumDirection.Column ? 1 : 0)];

            for (int i = 0; i < matrix.GetLength(sumDirection == SumDirection.Column ? 1 : 0); i++)
            {
                for (int j = 0; j < matrix.GetLength(sumDirection == SumDirection.Column ? 0 : 1); j++)
                {
                    if (sumDirection == SumDirection.Column)
                        sums[i] = add(sums[i], matrix[j, i]);
                    else
                        sums[i] = add(sums[i], matrix[i, j]);
                }
            }
            return sums;
        }

        private static void WriteMatrix<T>(T[,] matrix, T[] rowSums, T[] columSums)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.WriteLine();
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j]);
                    Console.Write("\t");
                }

                Console.Write(rowSums[i]);
            }

            Console.WriteLine();
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(columSums[j]);
                Console.Write("\t");
            }

            Console.WriteLine();
        }

        private enum SumDirection
        {
            Row,
            Column
        }
    }
}
