using System;
using System.Diagnostics;
using System.Linq;
using ASD.Graphs;

namespace asd2_lab11
{
    public static class TrailPlannerExtender
    {
        /// <summary>
        /// Minimalizacja liczby szlakow
        /// </summary>
        /// <param name="g">Wejsciowy graf. Przyjmujemy, ze jest on acykliczny. </param>
        /// <param name="totalCost">Koszt najtanszego znalezionego rozwiazania</param>
        /// <param name="trails">Graf skadajacy sie z krawedzi wchodzacych do rozwiazania</param>
        /// <returns>Metoda zwraca minimalna mozliwa liczbe szlakow</returns>
        public static int MinimumNumberOfTrails(this Graph g, out int totalCost, out Graph trails)
        {
            int n = g.VerticesCount;
            Graph network = g.IsolatedVerticesGraph(true, 2 * n + 2);
            int s = 2 * n;
            int t = 2 * n + 1;
            // 0, ..., n - 1 - in vertices
            // n, ..., 2n - 1 - out vertices
            Graph costs = network.IsolatedVerticesGraph();

            for (int v = 0; v < n; v++)
            {
                network.AddEdge(s, n + v);
                costs.AddEdge(s, n + v, 0);
                network.AddEdge(v, t);
                costs.AddEdge(v, t, 0);

                foreach (Edge e in g.OutEdges(v))
                {
                    network.AddEdge(n + v, e.To);
                    costs.AddEdge(n + v, e.To, e.Weight);
                }
            }

            double flowValue = network.MinCostFlow(costs, s, t, out double cost, out Graph flow);
            trails = g.IsolatedVerticesGraph();

            int[] vertexInComponent = new int[n];
            int totalComponents = 0;
            for (int v = 0; v < n; v++)
            {
                if (vertexInComponent[v] == 0)
                    vertexInComponent[v] = ++totalComponents;

                foreach (Edge e in flow.OutEdges(n + v))
                {
                    if (e.Weight == 0)
                        continue;

                    trails.AddEdge(v, e.To, e.Weight);
                    vertexInComponent[e.To] = vertexInComponent[v];
                }
            }

            totalCost = Convert.ToInt32(cost);
            return totalComponents;
        }

        /// <summary>
        /// Minimalizacja kosztu szlakow, z uwzglednieniem hoteli
        /// </summary>
        /// <param name="g">Wejsciowy graf. Przyjmujemy, ze jest on acykliczny. </param>
        /// <param name="vcosts">vcosts[i] to kosz postawienia holetu w miescie i</param>
        /// <param name="trails">Graf skadajacy sie z krawedzi wchodzacych do rozwiazania</param>
        /// <returns>Metoda zwraca minimalny mozliwy koszt rozwiazania</returns>
        public static int MinimumCostOfTrails(this Graph g, int[] vcosts, out Graph trails)
        {
            int n = g.VerticesCount;
            Graph network = g.IsolatedVerticesGraph(true, 2 * n + 2);
            int s = 2 * n;
            int t = 2 * n + 1;
            // 0, ..., n - 1 - in vertices
            // n, ..., 2n - 1 - out vertices
            Graph costs = network.IsolatedVerticesGraph();

            for (int v = 0; v < n; v++)
            {
                // s -> v_in (starting edge, free)
                network.AddEdge(s, v);
                costs.AddEdge(s, v, 0);

                // v_out -> t (hotel cost)
                network.AddEdge(n + v, t);
                costs.AddEdge(n + v, t, vcosts[v]);

                // v_in -> v_out (inner edge, free)
                network.AddEdge(v, n + v);
                costs.AddEdge(v, n + v, 0);

                // v_out -> u_in (trail cost)
                foreach (Edge e in g.OutEdges(v))
                {
                    network.AddEdge(n + v, e.To);
                    costs.AddEdge(n + v, e.To, e.Weight);
                }
            }

            network.MinCostFlow(costs, s, t, out double cost, out Graph flow);
            trails = g.IsolatedVerticesGraph();

            for (int v = 0; v < n; v++)
            {
                foreach (Edge e in flow.OutEdges(n + v))
                {
                    if (e.Weight == 0 || e.To == t)
                        continue;

                    trails.AddEdge(v, e.To, e.Weight);
                }
            }

            return Convert.ToInt32(cost);
        }
    }
}
