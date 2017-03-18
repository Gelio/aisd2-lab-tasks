
namespace ASD.Graphs
{

    /// <summary>
    /// Rozszerzenie interfejsu <see cref="IGraph"/> o wyznaczanie minimalnego drzewa rozpinającego algorytmem Kruskala
    /// </summary>
    public static class KruskalGraphExtender
    {

        /// <summary>
        /// Wyznacza minimalne drzewo rozpinające grafu algorytmem Kruskala
        /// </summary>
        /// <param name="g">Badany graf</param>
        /// <param name="mst">Wyznaczone drzewo rozpinające (parametr wyjściowy)</param>
        /// <returns>Waga minimalnego drzewa rozpinającego</returns>
        /// <remarks>
        /// Dla grafu skierowanego metoda zgłasza wyjątek <see cref="System.ArgumentException"/>.<br/>
        /// Wyznaczone drzewo reprezentowane jast jako graf bez cykli, to umożliwia jednolitą obsługę sytuacji
        /// gdy analizowany graf jest niespójny, wyzmnaczany jest wówczas las rozpinający.
        /// </remarks>
        public static double Lab04_Kruskal(this Graph g, out Graph mst)
        {
            // 1 pkt
            if (g.Directed)
                throw new System.ArgumentException("Graf jest skierowany");

            EdgesPriorityQueue edgesQueue = new EdgesMinPriorityQueue();
            g.GeneralSearchAll<EdgesQueue>(null, null, e =>
            {
                if (e.From < e.To)
                    edgesQueue.Put(e);
                return true;
            }, out int cc);

            UnionFind uf = new UnionFind(g.VerticesCount);
            mst = g.IsolatedVerticesGraph();

            double weight = 0;
            while (!edgesQueue.Empty)
            {
                Edge e = edgesQueue.Get();
                if (uf.Find(e.From) != uf.Find(e.To))
                {
                    uf.Union(e.From, e.To);
                    mst.AddEdge(e);
                    weight += e.Weight;
                }
            }
            return weight;
        }

    }  // class KruskalGraphExtender

}  // namespace ASD.Graph
