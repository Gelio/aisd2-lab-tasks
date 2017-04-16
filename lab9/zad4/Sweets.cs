using System;

using ASD.Graphs;

namespace Lab09
{
    class Sweets
    {
        /// <summary>
        /// Implementacja zadania 1
        /// </summary>
        /// <param name="childrenCount">Liczba dzieci</param>
        /// <param name="sweetsCount">Liczba batoników</param>
        /// <param name="childrenLikes">
        /// Tablica tablic upodobań dzieci. Tablica childrenLikes[i] zawiera indeksy batoników
        /// które lubi i-te dziecko. Dzieci i batoniki są indeksowane od 0.
        /// </param>
        /// <param name="assignment">
        /// Wynikowy parametr. assigment[i] zawiera indeks batonika które dostało i-te dziecko.
        /// Jeśli dziecko nie dostało żadnego batonika to -1.
        /// </param>
        /// <returns>Liczba dzieci, które dostały batonik.</returns>
        public static int Task1(int childrenCount, int sweetsCount, int[][] childrenLikes, out int[] assignment)
        {
            Graph sweetsDispenser = new AdjacencyMatrixGraph(true, childrenCount + sweetsCount + 2);
            // Vertices in the graph
            // 0 - source, 1 - destination,
            // 2, ..., (2 + childrenCount - 1) - children
            // (2 + childrenCount), ..., (2 + childrenCount + sweetsCount - 1) - sweets

            // Each child can have only one sweet
            for (int i = 0; i < childrenCount; i++)
                sweetsDispenser.AddEdge(0, 2 + i, 1);

            // There is only one piece of each sweet
            for (int i = 0; i < sweetsCount; i++)
                sweetsDispenser.AddEdge(2 + childrenCount + i, 1, 1);

            // Children' likes
            for (int i = 0; i < childrenCount; i++)
            {
                foreach (int sweet in childrenLikes[i])
                    sweetsDispenser.AddEdge(2 + i, 2 + childrenCount + sweet, 1);
            }

            int childrenSatisfied = (int)sweetsDispenser.FordFulkersonDinicMaxFlow(0, 1, out Graph sweetsAssignment, MaxFlowGraphExtender.BFPath);

            assignment = new int[childrenCount];
            for (int i = 0; i < childrenCount; i++)
            {
                assignment[i] = -1;
                foreach (Edge e in sweetsAssignment.OutEdges(2 + i))
                {
                    if (e.Weight > 0)
                        assignment[i] = e.To - (2 + childrenCount);
                }

            }
            return childrenSatisfied;
        }

        /// <summary>
        /// Implementacja zadania 2
        /// </summary>
        /// <param name="childrenCount">Liczba dzieci</param>
        /// <param name="sweetsCount">Liczba batoników</param>
        /// <param name="childrenLikes">
        /// Tablica tablic upodobań dzieci. Tablica childrenLikes[i] zawiera indeksy batoników
        /// które lubi i-te dziecko. Dzieci i batoniki są indeksowane od 0.
        /// </param>
        /// <param name="childrenLimits">Tablica ograniczeń dla dzieci. childtrenLimits[i] to maksymalna liczba batoników jakie może zjeść i-te dziecko.</param>
        /// <param name="sweetsLimits">Tablica ograniczeń batoników. sweetsLimits[i] to dostępna liczba i-tego batonika.</param>
        /// <param name="happyChildren">Wynikowy parametr zadania 2a. happyChildren[i] powinien zawierać true jeśli dziecko jest zadowolone i false wpp.</param>
        /// <param name="shoppingList">Wynikowy parametr zadania 2b. shoppingList[i] poiwnno zawierać liczbę batoników i-tego rodzaju, które trzeba dokupić.</param>
        /// <returns>Maksymalna liczba rozdanych batoników.</returns>
        public static int Task2(int childrenCount, int sweetsCount, int[][] childrenLikes, int[] childrenLimits, int[] sweetsLimits, out bool[] happyChildren, out int[] shoppingList)
        {
            Graph sweetsDispenser = new AdjacencyMatrixGraph(true, childrenCount + sweetsCount + 2);
            // Vertices in the graph
            // 0 - source, 1 - destination,
            // 2, ..., (2 + childrenCount - 1) - children
            // (2 + childrenCount), ..., (2 + childrenCount + sweetsCount - 1) - sweets

            // Each child has a limit on the amount of sweets it can it
            for (int i = 0; i < childrenCount; i++)
                sweetsDispenser.AddEdge(0, 2 + i, childrenLimits[i]);

            // Limited number of each sweet
            for (int i = 0; i < sweetsCount; i++)
                sweetsDispenser.AddEdge(2 + childrenCount + i, 1, sweetsLimits[i]);

            // Children' likes
            for (int i = 0; i < childrenCount; i++)
            {
                foreach (int sweet in childrenLikes[i])
                    sweetsDispenser.AddEdge(2 + i, 2 + childrenCount + sweet, double.PositiveInfinity);
            }

            int totalSweetsTaken = (int)sweetsDispenser.FordFulkersonDinicMaxFlow(0, 1, out Graph sweetsAssignment, MaxFlowGraphExtender.BFPath);

            happyChildren = new bool[childrenCount];
            shoppingList = new int[sweetsCount];
            for (int i = 0; i < childrenCount; i++)
            {
                double childSum = 0;
                foreach (Edge e in sweetsAssignment.OutEdges(2 + i))
                {
                    childSum += e.Weight;
                }
                happyChildren[i] = (int)childSum == childrenLimits[i];

                if (!happyChildren[i])
                {
                    foreach (Edge e in sweetsDispenser.OutEdges(2 + i))
                    {
                        shoppingList[e.To - (2 + childrenCount)] += childrenLimits[i] - (int)childSum;
                        break;
                    }
                }
            }

            return totalSweetsTaken;
        }

    }
}
