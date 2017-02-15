using System;
using ASD.Graph;

namespace ASD2
{
    static class Flow_main
    {
        static RandomGraphGenerator generator = new RandomGraphGenerator();
        static Random random = new Random(0);

        public static void Main(string[] args)
        {
            generator.SetSeed(0);
            BipartiteMatchings();
            Console.WriteLine("*****************");
            ConstrainedMaxFlow();
            Console.WriteLine("*****************");
            MaxIndependentPaths();
        }

        private static void BipartiteMatchings()
        {
            IGraph matching;
            int count;
            IGraph g;

            for (int i = 0; i <= 5; ++i)
            {
                g = generator.BipariteGraph(typeof(AdjacencyMatrixGraph), random.Next(5) + 5, random.Next(5) + 5, 0.6);
                try
                {
                    count = g.GetMaxMatching(out matching);
                    if ( IsMatchingFeasible(matching,g) )
                        Console.WriteLine("g{0} : {1}", i + 1, count);
                    else
                        Console.WriteLine("g{0} : {1}", i + 1, "skojarzenie nie jest dopuszczalne");
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine("g{0}: {1}", i + 1, e.Message);
                }
            }
        }

        /// <summary>
        /// Sprawdza, czy znalezione skojarzenie jest dopuszczalne (niekoniecznie najliczniejsze)
        /// </summary>
        /// <param name="matching">Znalezione skojarzenie</param>
        /// <param name="g">Graf, w którym szuakmy skojarzenia</param>
        /// <returns>Czy skojarzenie jest dopuszczalne</returns>
        static bool IsMatchingFeasible(IGraph matching, IGraph g)
        {
            if (matching==null)
                return false;
            if (matching.Directed)
                return false;
            if (matching.VerticesCount != g.VerticesCount)
                return false;            
            for (int i = 0; i < matching.VerticesCount; ++i)
                if (matching.OutDegree(i) > 1)
                    return false;
            for (int i = 0; i < matching.VerticesCount; ++i)
                foreach ( Edge e in matching.OutEdges(i) )
                    if ( !g.GetEdgeWeight(i, e.To).HasValue )
                        return false;
            return true;
        }

        private static void ConstrainedMaxFlow()
        {
            for (int j = 0; j < 5; ++j)
            {
                IGraph network = generator.DirectedGraph(typeof(AdjacencyMatrixGraph), random.Next(20) + 5, 0.6, 1, 10);
                for (int i = 0; i < network.VerticesCount; ++i)
                {
                    network.DelEdge(i, 0);
                    network.DelEdge(1, i);
                }
                int[] capacites = new int[network.VerticesCount];
                capacites[0] = capacites[1] = int.MaxValue;
                for (int i = 2; i < network.VerticesCount; ++i)
                    capacites[i] = random.Next(9) + 1;
                int flow1, flow2;
                IGraph flow;
                flow1 = network.FordFulkersonMaxFlow(0, 1, out flow);
                flow2 = network.ConstrainedMaxFlow(0, 1, capacites, out flow);
                if ( IsFlowFeasible(network,0,1,capacites,flow) )
                    Console.WriteLine("g{0}\n bez ogr: {1} \n   z ogr: {2}", j + 1, flow1, flow2);
                else
                    Console.WriteLine("g{0}\n bez ogr: {1} \n   z ogr: przeplyw niedopuszczalny", j + 1, flow1);
            }
        }

        /// <summary>
        /// Sprawdza, czy znaleziony przepływ jest dopuszczalny (niekoniecznie maksymalny)
        /// </summary>
        /// <param name="network">Sieć wejściowa</param>
        /// <param name="s">źródło sieci </param>
        /// <param name="t">ujście sieci</param>
        /// <param name="capacities">przepustowości wierzchołków</param>
        /// <param name="flow">znaleziony graf przepływu</param>
        /// <returns>Czy graf przepływu jest jest dopuszczalny</returns>
        static bool IsFlowFeasible(IGraph network, int s, int t, int[] capacities, IGraph flow)
        {
            if ( flow==null )
                return false;
            int[,] vertFlows = new int[2,network.VerticesCount];
            if (flow.VerticesCount != network.VerticesCount)
                return false;

            for (int i = 0; i < flow.VerticesCount; ++i)
            {
                foreach ( Edge e in flow.OutEdges(i) )
                {
                    if (!network.GetEdgeWeight(i, e.To).HasValue)
                        return false;
                    vertFlows[0, i] += e.Weight;
                    vertFlows[1, e.To] += e.Weight;
                }                
            }

            for (int i = 0; i < flow.VerticesCount; ++i)
            {
                if (i != s && i != t)
                    if (vertFlows[0, i] != vertFlows[1, i])
                        return false;
                if (vertFlows[0, i] > capacities[i] || vertFlows[1, i] > capacities[i])
                    return false;
            }
            return true;
        }

        private static void MaxIndependentPaths()
        {
            for (int i = 0; i < 5; ++i)
            {
                IGraph g = generator.UndirectedGraph(typeof(AdjacencyMatrixGraph), random.Next(5) + 5, 0.9);
                IGraph paths;
                int count = g.FindMaxIndependentPaths(0, 1, out paths);
                Console.WriteLine("g{0} : {1}", i + 1, count);
            }
        }
    }

}

