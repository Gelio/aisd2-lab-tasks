
namespace ASD.Graph
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
    public static int? TSP_Kruskal(this IGraph g, out Edge[] cycle)
        {
        // ToDo - algorytm "kruskalopodobny"
        cycle=null;
        return null;
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
    public static int? TSP_TreeBased(this IGraph g, out Edge[] cycle)
        {
        // ToDo - algorytm oparty na minimalnym drzewie rozpinajacym
        cycle=null;
        return null;
        }  // TSP_TreeBased

    }  // class Lab08Extender

}  // namespace ASD.Graph
