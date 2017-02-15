using ASD.Graph;

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
    public static int GetMaxMatching(this IGraph g, out IGraph matching)
        {
        //
        // TODO (2 pkt.)
        //
        matching = null;
        return -1;
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
    public static int ConstrainedMaxFlow(this IGraph network, int s, int t, int[] capacity, out IGraph flowGraph)
        {
        //
        // TODO (1 pkt.)
        //
            IGraph net = new AdjacencyMatrixGraph(true, network.VerticesCount * 2);
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
            int flow = MaxFlowGraphExtender.PushRelabelMaxFlow(net, s, t, out flowGraph);
            IGraph fll = new AdjacencyMatrixGraph(true, network.VerticesCount);
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
    public static int FindMaxIndependentPaths(this IGraph g, int start, int finish, out IGraph paths)
        {
        //
        // TODO (1 pkt.)
        //
        paths = null;
        return -1;
        }

    }

}
