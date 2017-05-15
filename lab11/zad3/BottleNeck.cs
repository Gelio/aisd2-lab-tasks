
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

namespace ASD.Graphs
{

    public static class BottleNeckExtender
    {

        /// <summary>
        /// Wyszukiwanie "wąskich gardeł" w sieci przesyłowej
        /// </summary>
        /// <param name="g">Graf przepustowości krawędzi</param>
        /// <param name="c">Graf kosztów rozbudowy sieci (kosztów zwiększenia przepustowości)</param>
        /// <param name="p">Tablica mocy produkcyjnych/zapotrzebowania w poszczególnych węzłach</param>
        /// <param name="flowValue">Maksymalna osiągalna produkcja (parametr wyjściowy)</param>
        /// <param name="cost">Koszt rozbudowy sieci, aby możliwe było osiągnięcie produkcji flowValue (parametr wyjściowy)</param>
        /// <param name="flow">Graf przepływu dla produkcji flowValue (parametr wyjściowy)</param>
        /// <param name="ext">Tablica rozbudowywanych krawędzi (parametr wyjściowy)</param>
        /// <returns>
        /// 0 - zapotrzebowanie można zaspokoić bez konieczności zwiększania przepustowości krawędzi<br/>
        /// 1 - zapotrzebowanie można zaspokoić, ale trzeba zwiększyć przepustowość (niektórych) krawędzi<br/>
        /// 2 - zapotrzebowania nie można zaspokoić (zbyt małe moce produkcyjne lub nieodpowiednia struktura sieci
        ///     - można jedynie zwiększać przepustowości istniejących krawędzi, nie wolno dodawać nowych)
        /// </returns>
        /// <remarks>
        /// Każdy element tablicy p opisuje odpowiadający mu wierzchołek<br/>
        ///    wartość dodatnia oznacza moce produkcyjne (wierzchołek jest źródłem)<br/>
        ///    wartość ujemna oznacza zapotrzebowanie (wierzchołek jest ujściem),
        ///       oczywiście "możliwości pochłaniające" ujścia to moduł wartości elementu<br/>
        ///    "zwykłym" wierzchołkom odpowiada wartość 0 w tablicy p<br/>
        /// <br/>
        /// Jeśli funkcja zwraca 0, to<br/>
        ///    parametr flowValue jest równy modułowi sumy zapotrzebowań<br/>
        ///    parametr cost jest równy 0<br/>
        ///    parametr ext jest pustą (zeroelementową) tablicą<br/>
        /// Jeśli funkcja zwraca 1, to<br/>
        ///    parametr flowValue jest równy modułowi sumy zapotrzebowań<br/>
        ///    parametr cost jest równy sumarycznemu kosztowi rozbudowy sieci (zwiększenia przepustowości krawędzi)<br/>
        ///    parametr ext jest tablicą zawierającą informację o tym o ile należy zwiększyć przepustowości krawędzi<br/>
        /// Jeśli funkcja zwraca 2, to<br/>
        ///    parametr flowValue jest równy maksymalnej możliwej do osiągnięcia produkcji
        ///      (z uwzględnieniem zwiększenia przepustowości)<br/>
        ///    parametr cost jest równy sumarycznemu kosztowi rozbudowy sieci (zwiększenia przepustowości krawędzi)<br/>
        ///    parametr ext jest tablicą zawierającą informację o tym o ile należy zwiększyć przepustowości krawędzi<br/>
        /// Uwaga: parametr ext zawiera informacje jedynie o krawędziach, których przepustowości trzeba zwiększyć
        //     (każdy element tablicy to opis jednej takiej krawędzi)
        /// </remarks>
        public static int BottleNeck(this Graph g, Graph c, int[] p, out int flowValue, out int cost, out Graph flow, out Edge[] ext)
        {
            int n = g.VerticesCount;
            Graph network = g.IsolatedVerticesGraph(true, 2 * n + 2);
            Graph costs = network.IsolatedVerticesGraph();
            // 0, ..., n - 1 - regular vertices
            // n, ..., 2n - 1 - additional vertices (used for extending the network)
            int s = 2 * n;
            int t = 2 * n + 1;


            double totalDemand = 0;
            for (int v = 0; v < n; v++)
            {
                if (p[v] > 0)
                {
                    // v is a source
                    network.AddEdge(s, v, p[v]);
                    costs.AddEdge(s, v, 0);
                }
                else if (p[v] < 0)
                {
                    // v consumes the flow
                    network.AddEdge(v, t, -p[v]);
                    costs.AddEdge(v, t, 0);
                    totalDemand += -p[v];
                }

                foreach (Edge e in g.OutEdges(v))
                {
                    // x - y
                    network.AddEdge(e);
                    costs.AddEdge(v, e.To, 0);

                    // x - x2
                    network.AddEdge(v, n + v, int.MaxValue);
                    costs.AddEdge(v, n + v, c.GetEdgeWeight(v, e.To));

                    // x2 - y
                    network.AddEdge(n + v, e.To, int.MaxValue);
                    costs.AddEdge(n + v, e.To, 0);
                }
            }


            double networkFlowValue = network.MinCostFlow(costs, s, t, out double networkCost, out Graph networkFlow);
            flowValue = Convert.ToInt32(networkFlowValue);
            cost = Convert.ToInt32(networkCost);

            List<Edge> extensionEdges = new List<Edge>();
            
            flow = g.IsolatedVerticesGraph();
            for (int v = 0; v < n; v++)
            {
                foreach (Edge e in g.OutEdges(v))
                {
                    double totalFlow = networkFlow.GetEdgeWeight(v, e.To);
                    double extendedFlow = networkFlow.GetEdgeWeight(n + v, e.To);

                    if (!extendedFlow.IsNaN() && extendedFlow > 0)
                    {
                        totalFlow += extendedFlow;
                        extensionEdges.Add(new Edge(v, e.To, extendedFlow));
                    }

                    flow.AddEdge(v, e.To, totalFlow);
                }
            }

            ext = extensionEdges.ToArray();

            if (extensionEdges.Count == 0)
                return 0;

            return flowValue == totalDemand ? 1 : 2;
        }

    }

}