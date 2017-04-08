
namespace ASD.Graphs
{
    using System.Linq;  // potrzebne dla metody First rozszerzającej interfejs IEnumerable

    /// <summary>
    /// Rozszerzenie interfejsu <see cref="IGraph"/> o rozwiązywanie problemu komiwojażera metodami przybliżonymi
    /// </summary>
    public static class Lab08Extender
    {

        /// <summary>
        /// Znajduje rozwiązanie przybliżone problemu komiwojażera algorytmem zachłannym "kruskalopodobnym"
        /// </summary>
        /// <param name="g">Badany graf</param>
        /// <param name="cycle">Znaleziony cykl (parametr wyjściowy)</param>
        /// <returns>Długość znalezionego cyklu (suma wag krawędzi)</returns>
        /// <remarks>
        /// Elementy (krawędzie) umieszczone są w tablicy <i>cycle</i> w kolejności swojego następstwa w znalezionym cyklu Hamiltona.<br/>
        /// <br/>
        /// Jeśli algorytm "kruskalopodobny" nie znajdzie w badanym grafie cyklu Hamiltona
        /// (co oczywiście nie znaczy, że taki cykl nie istnieje) to metoda zwraca <b>null</b>,
        /// parametr wyjściowy <i>cycle</i> również ma wówczas wartość <b>null</b>.<br/>
        /// <br/>
        /// Metodę można stosować dla grafów skierowanych i nieskierowanych.<br/>
        /// <br/>
        /// Metodę można stosować dla dla grafów z dowolnymi (również ujemnymi) wagami krawędzi.
        /// </remarks>
        public static double TSP_Kruskal(this Graph g, out Edge[] cycle)
        {
            // ToDo - algorytm "kruskalopodobny"
            int n = g.VerticesCount;
            EdgesMinPriorityQueue edgesQueue = new EdgesMinPriorityQueue();
            for (int v = 0; v < n; v++)
            {
                foreach (Edge e in g.OutEdges(v))
                {
                    // For undirected graphs only add edges once
                    if (!g.Directed && e.From >= e.To)
                        continue;

                    edgesQueue.Put(e);
                }
            }

            UnionFind uf = new UnionFind(n);
            Graph minSpanningTree = g.IsolatedVerticesGraph();

            while (!edgesQueue.Empty && minSpanningTree.EdgesCount < n - 1)
            {
                Edge e = edgesQueue.Get();
                if (uf.Find(e.From) == uf.Find(e.To))   // Edge would preemptively create a cycle
                    continue;
                if (g.Directed)
                {
                    if (minSpanningTree.OutDegree(e.From) != 0 || minSpanningTree.InDegree(e.To) != 0)  // Two out edges or two in edges for some vertex
                        continue;
                }
                else
                {
                    if (minSpanningTree.OutDegree(e.From) == 2 || minSpanningTree.OutDegree(e.To) == 2) // Edge would create a diversion on the path
                        continue;
                }

                minSpanningTree.AddEdge(e);
                uf.Union(e.From, e.To);
            }

            if (minSpanningTree.EdgesCount < n - 1)
            {
                // Unable to construct a spanning path with n-1 edges
                cycle = null;
                return double.NaN;
            }


            // Look for vertices at the beginning and end of the path
            int cycleBeginV = -1,
                cycleEndV = -1;

            for (int v = 0; v < n; v++)
            {
                if (!minSpanningTree.Directed)
                {
                    if (minSpanningTree.OutDegree(v) == 1)
                    {
                        if (cycleBeginV == -1)
                            cycleBeginV = v;
                        else
                        {
                            cycleEndV = v;
                            break;
                        }
                    }
                }
                else
                {
                    if (minSpanningTree.OutDegree(v) == 0)
                        cycleBeginV = v;
                    if (minSpanningTree.InDegree(v) == 0)
                        cycleEndV = v;

                    if (cycleBeginV != -1 && cycleEndV != -1)
                        break;
                }

            }

            if (cycleBeginV == -1 || cycleEndV == -1)
            {
                // This if is superfluous, but I'm leaving it just for clarity
                cycle = null;
                return double.NaN;
            }

            // Closing the cycle
            minSpanningTree.AddEdge(new Edge(cycleBeginV, cycleEndV, g.GetEdgeWeight(cycleBeginV, cycleEndV)));
            cycle = new Edge[n];
            int currentCycleV = 0;
            double cycleLength = 0;

            for (int i = 0; i < n; i++)
            {
                Edge? cycleEdge = minSpanningTree.OutEdges(currentCycleV).First();
                if (!minSpanningTree.Directed && i > 0)
                {
                    // Make sure the edge goes further into the cycle, not backwards (only for undirected graphs)
                    foreach (Edge e in minSpanningTree.OutEdges(currentCycleV))
                    {
                        if (e.To != cycle[i - 1].From)
                        {
                            cycleEdge = e;
                            break;
                        }
                    }
                }

                cycle[i] = cycleEdge.Value;
                currentCycleV = cycleEdge.Value.To;
                cycleLength += cycleEdge.Value.Weight;
            }

            return cycleLength;
        }  // TSP_Kruskal

        /// <summary>
        /// Znajduje rozwiązanie przybliżone problemu komiwojażera tworząc cykl Hamiltona na podstawie drzewa rozpinającego
        /// </summary>
        /// <param name="g">Badany graf</param>
        /// <param name="cycle">Znaleziony cykl (parametr wyjściowy)</param>
        /// <returns>Długość znalezionego cyklu (suma wag krawędzi)</returns>
        /// <remarks>
        /// Elementy (krawędzie) umieszczone są w tablicy <i>cycle</i> w kolejności swojego następstwa w znalezionym cyklu Hamiltona.<br/>
        /// <br/>
        /// Jeśli algorytm bazujący na drzewie rozpinającym nie znajdzie w badanym grafie cyklu Hamiltona
        /// (co oczywiście nie znaczy, że taki cykl nie istnieje) to metoda zwraca <b>null</b>,
        /// parametr wyjściowy <i>cycle</i> również ma wówczas wartość <b>null</b>.<br/>
        /// <br/>
        /// Metodę można stosować dla grafów nieskierowanych.<br/>
        /// Zastosowana do grafu skierowanego zgłasza wyjątek <see cref="System.ArgumentException"/>.<br/>
        /// <br/>
        /// Metodę można stosować dla dla grafów z dowolnymi (również ujemnymi) wagami krawędzi.<br/>
        /// <br/>
        /// Dla grafu nieskierowanego spełniajacego nierówność trójkąta metoda realizuje algorytm 2-aproksymacyjny.
        /// </remarks>
        public static double TSP_TreeBased(this Graph g, out Edge[] cycle)
        {
            if (g.Directed)
                throw new System.ArgumentException("Graph is directed");

            int n = g.VerticesCount;
            EdgesMinPriorityQueue edgesQueue = new EdgesMinPriorityQueue();
            for (int v = 0; v < n; v++)
            {
                foreach (Edge e in g.OutEdges(v))
                {
                    // Only add edges once
                    if (e.From >= e.To)
                        continue;

                    edgesQueue.Put(e);
                }
            }

            Graph minSpanningTree = g.IsolatedVerticesGraph();
            UnionFind uf = new UnionFind(n);
            while (!edgesQueue.Empty && minSpanningTree.EdgesCount < n - 1)
            {
                Edge e = edgesQueue.Get();
                if (uf.Find(e.From) == uf.Find(e.To))   // Same component, would create a cycle
                    continue;

                minSpanningTree.AddEdge(e);
            }

            if (minSpanningTree.EdgesCount < n - 1)
            {
                
            }

            int[] preorderVertices = new int[n];
            int i = 0;
            minSpanningTree.GeneralSearchAll<EdgesQueue>(v =>
            {
                preorderVertices[i++] = v;
                return true;
            }, null, null, out int cc);

            cycle = new Edge[n];
            double cycleLength = 0;
            for (i = 1; i < n; i++)
            {
                int v1 = preorderVertices[i - 1],
                    v2 = preorderVertices[i];
                Edge e = new Edge(v1, v2, g.GetEdgeWeight(v1, v2));
                cycle[i - 1] = e;
                cycleLength += e.Weight;
                if (e.Weight.IsNaN())
                    break;  // Added an edge that doesn't exist
            }

            if (cycleLength.IsNaN())
            {
                cycle = null;
                return double.NaN;
            }

            double closingEdgeWeight = g.GetEdgeWeight(preorderVertices[n - 1], preorderVertices[0]);
            cycle[n - 1] = new Edge(preorderVertices[n - 1], preorderVertices[0], closingEdgeWeight);
            cycleLength += closingEdgeWeight;

            return cycleLength;
        }  // TSP_TreeBased

    }  // class Lab08Extender

}  // namespace ASD.Graph
