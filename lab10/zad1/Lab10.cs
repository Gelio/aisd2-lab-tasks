using ASD.Graphs;

namespace ASD2
{

    public static class FlowGraphExtender
    {

        /// <summary>
        /// Metoda zwraca graf reprezentujący najliczniejsze skojarzenie w grafie dwudzielnym.
        /// Skojarzenie znajdowane jest przy pomocy algorytmu znajdowania maksymalnego przepływu w sieci.
        /// Dla grafu dwudzielnego (nieskierowanego) G = (X,Y,E) tworzymy sieć N:
        /// V(N) = X u Y u {s,t}
        /// E(N) = { (x,y) : x należy do X i y należy do Y i {x,y} należy do E(G) } 
        ///        u {(s,x) : x należy do X } 
        ///        u {(y,t) : y należy do Y }
        /// c(e) = 1 dla każdego e należącego do E(N)
        /// 
        /// Krawędzie realizujące maksymalny przepływ w sieci N (poza krawędziami zawierającymi źródło i ujście) 
        /// odpowiadają najliczniejszemu skojarzeniu w G.
        /// </summary>
        /// <param name="g">Graf dwudzielny</param>
        /// <param name="matching">Znalezione skojarzenie - 
        /// graf nieskierowany będący kopią g z usuniętymi krawędziami spoza znalezionego skojarzenia</param>
        /// <returns>Liczność znalezionego skojarzenia</returns>
        /// <remarks>
        /// Uwaga 1: metoda nie modyfikuje zadanego grafu
        /// Uwaga 2: jeśli dany graf nie jest dwudzielny, metoda zgłasza wyjątek ArgumentException
        /// </remarks>
        public static int GetMaxMatching(this Graph g, out Graph matching)
        {
            int[] vertexInClass = g.GetBipariteClasses();
            int n = g.VerticesCount;

            Graph network = g.IsolatedVerticesGraph(true, n + 2);
            // n - source, (n + 1) - destination
            for (int v = 0; v < n; v++)
            {
                foreach (Edge e in g.OutEdges(v))
                {
                    if (e.To < e.From)
                        continue;
                    network.AddEdge(e.From, e.To, 1);
                }

                if (vertexInClass[v] == 1)
                {
                    // in class X, near source
                    network.AddEdge(n, v, 1);
                }
                else
                {
                    // in class Y, near destination
                    network.AddEdge(v, n + 1, 1);
                }
            }

            int matchingCount = (int)network.FordFulkersonDinicMaxFlow(n, n + 1, out Graph flow, MaxFlowGraphExtender.BFPath);

            matching = g.IsolatedVerticesGraph();
            for (int v = 0; v < n; v++)
            {
                foreach (Edge e in flow.OutEdges(v))
                {
                    if (e.Weight.IsNaN() || e.Weight == 0 || e.To >= n)
                        continue;
                    matching.AddEdge(e.From, e.To, g.GetEdgeWeight(e.From, e.To));
                }
            }
            return matchingCount;
        }


        private static int[] GetBipariteClasses(this Graph g)
        {
            int n = g.VerticesCount;
            int[] vertexInClass = new int[n];
            if (n == 0)
                return vertexInClass;

            Graph bipariteGraph = g.IsolatedVerticesGraph();
            for (int v=0; v < n; v++)
            {
                foreach (Edge e in g.OutEdges(v))
                {
                    if (!g.Directed && e.To < e.From)
                        continue;

                    bipariteGraph.AddEdge(e.From, e.To, 1);
                }
            }

            bipariteGraph.DijkstraShortestPaths(0, out PathsInfo[] d);

            for (int v=0; v < g.VerticesCount; v++)
            {
                if (d[v].Dist.IsNaN())
                {
                    throw new System.ArgumentException("Graph is not biparite");
                }
                    
                vertexInClass[v] = d[v].Dist % 2 == 0 ? 1 : 2;
            }

            return vertexInClass;
        }

        /// <summary>
        /// Znajduje przepływ w sieci N z ograniczonymi przepustowościami wierzchołków (c(v)).
        /// Do ograniczeń wynikających ze klasycznego problemu maksymalnego przepływu
        /// w sieci dokładamy dodatkowe:
        /// dla każdego wierzchołka v, niebędącego źródłem lub ujściem przepływ przez
        /// dany wierzchołek nie może przekraczać jego przepustowości.
        /// Przepływ taki możemy znaleźć konstruując pomocniczą sieć N':
        /// V(N') = { v_in, v_out dla każdego v należącego do V(N) \ {s,t} } u {s,t}
        /// Dla każdego v należącego do V(N) \ {s,t} wierzchołki v_in i v_out łączymy krawędzią
        /// o przepustowości c(v). Każda krawędź (u,v) w E(N) jest reprezentowana przez krawędź
        /// (u_out, v_in) w N'. (przyjmujemy, że w N' s=s_in=s_out i t=t_in=t_out) - przepustowości pozostają bez zmian.
        /// Maksymalny przepływ w sieci N' odpowiada maksymalnemu przepływowi z ograniczeniami w sieci N.
        /// 
        /// </summary>
        /// <param name="network">sieć wejściowa</param>
        /// <param name="s">źródło sieci</param>
        /// <param name="t">ujście sieci</param>
        /// <param name="capacity">przepustowości wierzchołków, przepustowości źródła i ujścia to int.MaxValue</param>
        /// <param name="flowGraph">Znaleziony graf przepływu w sieci wejściowej</param>
        /// <returns>Wartość maksymalnego przepływu</returns>
        /// <remarks>
        /// Wskazówka: Można przyjąć, że przepustowości źródła i ujścia są nieskończone
        /// i traktować je jak wszystkie inne wierzchołki.
        /// </remarks>
        public static int ConstrainedMaxFlow(this Graph network, int s, int t, int[] capacity, out Graph flowGraph)
        {
            //
            // TODO (1 pkt.)
            //
            Graph net = new AdjacencyMatrixGraph(true, network.VerticesCount * 2);
            for (int v = 0; v < network.VerticesCount; ++v)
            {
                if (v != s && v != t)
                    net.AddEdge(v + network.VerticesCount, v, capacity[v]);
                foreach (Edge e in network.OutEdges(v))
                {
                    if (e.To != t)
                        net.AddEdge(v, e.To + network.VerticesCount, e.Weight);
                    else
                        net.AddEdge(e);

                }
            }
            int flow = (int)MaxFlowGraphExtender.PushRelabelMaxFlow(net, s, t, out flowGraph);
            Graph fll = new AdjacencyMatrixGraph(true, network.VerticesCount);
            for (int v = 0; v < fll.VerticesCount; ++v)
            {
                foreach (Edge e in flowGraph.OutEdges(v))
                {
                    if (e.To != t)
                        fll.AddEdge(v, e.To - network.VerticesCount, e.Weight);
                    else
                        fll.AddEdge(e);
                }
            }
            flowGraph = fll;
            return flow;
        }

        /// <summary>
        /// Znajduje największą liczbę rozłącznych ścieżek pomiędzy wierzchołkami s i f w grafe G.
        /// Ścieżki są rozłączne, jeśli poza końcami nie mają wierzchołków wspólnych.
        /// Problem rozwiązujemy sprowadzając go do problemu maksymalnego przepływu z przepustowościami wierzchołków.
        /// Konstruujemy sieć N.
        /// V(N) = V(G)
        /// (u,v) należy do E(N) <=> (u,v) należy do E(G)
        /// c(e) = 1 dla każdego e należącego do E(N)
        /// c(v) = 1 dla każdego v różnego od s i f.
        /// Wierzchołki s i f są odpowiednio źródłem i ujściem sieci.
        /// </summary>
        /// <param name="g">Graf wejściowy</param>
        /// <param name="start">Wierzchołek startowy ścieżek</param>
        /// <param name="finish">Wierzchołek końcowy ścieżek</param>
        /// <param name="paths">Graf ścieżek</param>
        /// <returns>Liczba znalezionych ścieżek</returns>
        /// <remarks>
        /// Uwaga: Metoda działa zarówno dla grafów skierowanych, jak i nieskierowanych.
        /// </remarks>
        public static int FindMaxIndependentPaths(this Graph g, int start, int finish, out Graph paths)
        {
            //
            // TODO (1 pkt.)
            //
            int n = g.VerticesCount;
            int[] capacity = new int[n];
            for (int v = 0; v < n; v++)
                capacity[v] = 1;
            capacity[start] = capacity[finish] = int.MaxValue;

            Graph constrainedGraph = g.IsolatedVerticesGraph();
            for (int v = 0; v < n; v++)
            {
                foreach (Edge e in g.OutEdges(v))
                {
                    if (!g.Directed && e.To < e.From)
                        continue;
                    constrainedGraph.AddEdge(e.From, e.To, 1);
                }
            }

            return constrainedGraph.ConstrainedMaxFlow(start, finish, capacity, out paths);
        }

    }

}
