
namespace ASD.Graphs
{

    /// <summary>
    /// Rozszerzenie interfejsu <see cref="IGraph"/> o algorytm A*
    /// </summary>
    public static class AStarLabGraphExtender
    {

        /// <summary>
        /// Wyznacza najkrótszą ścieżkę do wskazanego wierzchołka algorytmem A*
        /// </summary>
        /// <param name="g">Badany graf</param>
        /// <param name="s">Wierzchołek źródłowy</param>
        /// <param name="t">Wierzchołek docelowy</param>
        /// <param name="p">Znaleziona ścieżka (parametr wyjściowy)</param>
        /// <param name="h">Oszacowanie odległości wierzchołków (funkcja)</param>
        /// <returns>Długość ścieżki (jeśli nie istnieje to NaN)</returns>
        /// <remarks>
        /// Domyślna wartość parametru <i>h</i> (<b>null</b>) oznacza, że zostanie przyjęte oszacowanie zerowe.
        /// Algorytm A* sprowadza się wówczas do algorytmu Dijkstry.<br/>
        /// <br/>
        /// Metoda nie bada spełnienia założeń algorytmu A* - jeśli nie one są spełnione może zwrócić błędny wynik (nieoptymalną ścieżkę).<br/>
        /// Informacja, czy szukana ścieżka istnieje, zawsze jest zwracana poprawnie.
        /// Jeśli ścieżka nie istnieje (wynik <b>NaN</b>), to parametr <i>p</i> również jest równy <b>null</b>.
        /// </remarks>
        public static double AStar(this Graph g, int s, int t, out Edge[] p, System.Collections.Generic.Dictionary<int, string> description, System.Func<int, int, double> h = null)
        {
            var open = new PriorityQueue<int, double>((x, y) => x.Value < y.Value, Graph.Access);
            var close = new System.Collections.Generic.HashSet<int>();  // dodać referencję system.dll
            var dist = new HashTable<int, double>(Graph.Access);
            var last = new HashTable<int, Edge>(Graph.Access);
            if (h == null)
                h = (x, y) => 0;
            int n = g.VerticesCount;
            double[] approximateDistances = new double[n];

            for (int i = 0; i < n; i++)
            {
                approximateDistances[i] = double.NaN;
                dist[i] = double.PositiveInfinity;
            }
                


            // Rozpoczęcie algorytmu przez dodanie wierzchołka startowego
            // (nie liczymy oszacowania odległości, bo to tylko niepotrzebna operacja w tym przypadku -
            // zawsze zaczniemy od startowego)
            approximateDistances[s] = 0;
            open.Put(s, 0);
            dist[s] = 0;

            while (!open.Empty)
            {
                int v = open.Get();
                close.Add(v);
                if (v == t)
                    break;

                foreach (Edge e in g.OutEdges(v))
                {
                    // Nie wracamy do już ustalonych wierzchołków
                    if (close.Contains(e.To))
                        continue;

                    // Sprawdzamy czy mamy lepszą odległość od źródła do danego wierzchołka
                    double prospectiveDistance = dist[v] + e.Weight;
                    if (dist[e.To] > prospectiveDistance)
                    {
                        dist[e.To] = prospectiveDistance;
                        last[e.To] = e;
                        if (approximateDistances[e.To].IsNaN())
                            approximateDistances[e.To] = h(e.To, t);
                        double newPriority = approximateDistances[e.To] + dist[e.To];
                        if (open.Contains(e.To))
                            open.ImprovePriority(e.To, newPriority);
                        else
                            open.Put(e.To, newPriority);
                    }
                }
            }

            foreach (int v in close)
                description[v] = "close";
            while (!open.Empty)
                description[open.Get()] = "open";

            if (double.IsPositiveInfinity(dist[t]))
            {
                p = null;
                return double.NaN;
            }

            EdgesStack path = new EdgesStack();
            int currentV = t;
            while (currentV != s)
            {
                Edge e = last[currentV];
                path.Put(e);
                currentV = e.From;
            }

            p = path.ToArray();
            return dist[t];
        }

    }  // class AStarGraphExtender

}  // namespace ASD.Graph
