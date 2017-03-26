
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
        public static double AStar(this Graph g, int s, int t, out Edge[] p, System.Collections.Generic.Dictionary<int, string> description, System.Func<int, int, int> h = null)
        {
            var open = new PriorityQueue<int, int>((x, y) => x.Key < y.Key, Graph.Access);
            var close = new System.Collections.Generic.HashSet<int>();  // dodać referencję system.dll
            var dist = new HashTable<int, int>(Graph.Access);
            var last = new HashTable<int, Edge>(Graph.Access);
            if (h == null)
                h = (x, y) => 0;

            p = null;
            //
            //  TO DO 
            //
            return double.NaN;
        }

    }  // class AStarGraphExtender

}  // namespace ASD.Graph
