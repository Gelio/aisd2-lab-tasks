using System;
using ASD.Graphs;
using System.Collections.Generic;
using System.Linq;

namespace lab9
{
    public static class AntsExtender
    {

        /// <summary>
        /// Sprawdza czy istnieje kraw�d�, kt�rej dodanie/poszerzenie poprawi przep�yw zapas�w
        /// </summary>
        /// <param name="baseGraph">graf</param>
        /// <param name="sources">numery wierzcho�k�w - wej�� do mrowiska</param>
        /// <param name="destinations">numery wierzcho�k�w - magazyn�w</param>
        /// <param name="flowValue">warto�� przep�ywu przed rozbudow� mrowiska</param>
        /// <returns>kraw�d� o wadze 1, kt�r� nale�y doda� lub poszerzy� (zwracamy te� kraw�d�
        /// o wadze 1)</returns>
		public static Edge? ImprovementChecker(this Graph baseGraph, int[] sources, int[] destinations, out double flowValue)
        {
            int n = baseGraph.VerticesCount;
            Graph extendedGraph = baseGraph.IsolatedVerticesGraph(true, n + 2);
            Graph reversed = baseGraph.Reverse();

            // vertex n is going to be the main source, n+1 the main destination 
            for (int v = 0; v < n; v++)
            {
                foreach (Edge e in baseGraph.OutEdges(v))
                    extendedGraph.AddEdge(e);
            }

            foreach (int sourceV in sources)
                extendedGraph.AddEdge(n, sourceV, double.PositiveInfinity);
            foreach (int destinationV in destinations)
                extendedGraph.AddEdge(destinationV, n + 1, double.PositiveInfinity);

            flowValue = extendedGraph.FordFulkersonDinicMaxFlow(n, n + 1, out Graph initialFlow, MaxFlowGraphExtender.BFPath);

            int sourceNeighbor = -1,
                destinationNeighbor = -1;

            foreach (int sourceV in sources)
            {
                foreach (Edge e in baseGraph.OutEdges(sourceV))
                {
                    double flowEdge = initialFlow.GetEdgeWeight(sourceV, e.To);
                    if (flowEdge.IsNaN() || flowEdge < e.Weight)
                    {
                        sourceNeighbor = e.To;
                        break;
                    }
                }
            }

            if (sourceNeighbor == -1)
                return null;
            
            foreach (int destinationV in destinations)
            {
                foreach (Edge e in reversed.OutEdges(destinationV))
                {
                    double flowEdge = initialFlow.GetEdgeWeight(e.To, destinationV);
                    if (flowEdge.IsNaN() || flowEdge < e.Weight)
                    {
                        destinationNeighbor = e.To;
                        break;
                    }
                }
            }

            if (destinationNeighbor == -1)
                return null;

            return new Edge(sourceNeighbor, destinationNeighbor, 1);
        }

    }
}

