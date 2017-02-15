
namespace ASD.Graph
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
    public static int Lab04_Kruskal(this IGraph g, out IGraph mst)
        {
        // 1 pkt

        // wykorzystac klase UnionFind z biblioteki Graph

        mst=g;
        return 0;
        }

    }  // class KruskalGraphExtender

}  // namespace ASD.Graph
