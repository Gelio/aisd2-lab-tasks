
using System;

namespace ASD.Graph
{

public static class BasicGraphExtender
    {
    /// <summary>Dodawanie wierzchołka do grafu</summary>
    /// <param name="g">Graf, do którego dodajemy wierzchołek</param>
    /// <returns>Graf z dodanym wierzchołkiem</returns>
    /// <remarks>
    /// 0.5 pkt.
    /// Metoda zwraca graf, będący kopią grafu wejściowego z dodanym wierzchołkiem.
    /// Graf wejściowy pozostaje niezmieniony.
    /// W utworzonym grafie są takie same krawędzie jak w wejściowym.
    /// Utworzony graf ma taką samą reprezentację jak graf wejściowy.
    /// Uwaga: W grafach nieskierowanych nie probować dodawawać po 2 razy tej samej krawędzi
    /// </remarks>
    public static IGraph AddVertex(this IGraph g)
        {
        return g.Clone(); // zmienic !
        }

    /// <summary>Usuwanie wierzchołka z grafu</summary>
    /// <param name="g">Graf, z którego usuwamy wierzchołek</param>
    /// <param name="del">Usuwany wierzchołek</param>
    /// <returns>Graf z usunietym wierzchołkiem</returns>
    /// <remarks>
    /// 1.0 pkt.
    /// Metoda zwraca graf, będący kopią grafu wejściowego z usuniętym wierzchołkiem.
    /// Graf wejściowy pozostaje niezmieniony.
    /// W utworzonym grafie są takie same krawędzie jak w wejściowym
    /// (oczywiście z wyjątkiem incydentnych z usuniętym wierzchołkiem, numeracja wierzchołków zostaje zaktualizowana)
    /// Utworzony graf ma taką samą reprezentację jak graf wejściowy.
    /// Uwaga: W grafach nieskierowanych nie probować dodawawać po 2 razy tej samej krawędzi
    /// </remarks>
    public static IGraph DeleteVertex(this IGraph g, int del)
        {
        return g.Clone(); // zmienic !
        }

    /// <summary>Dopełnienie grafu</summary>
    /// <param name="g">Graf wejściowy</param>
    /// <returns>Graf będący dopełnieniem grafu wejściowego</returns>
    /// <remarks>
    /// 0.5 pkt.
    /// Dopełnienie grafu to graf o tym samym zbiorze wierzchołków i zbiorze krawędzi równym VxV-E-"pętle"
    /// Graf wejściowy pozostaje niezmieniony.
    /// Utworzony graf ma taką samą reprezentację jak graf wejściowy.
    /// Uwaga 1 : w przypadku stwierdzenia ze graf wejściowy jest grafem ważonym zgłosić wyjątek ArgumentException
    /// Uwaga 2 : W grafach nieskierowanych nie probować dodawawać po 2 razy tej samej krawędzi
    /// </remarks>
    public static IGraph Complement(this IGraph g)
        {
        return g.Clone(); // zmienic !
        }

    /// <summary>Domknięcie grafu</summary>
    /// <param name="g">Graf wejściowy</param>
    /// <returns>Graf będący domknięciem grafu wejściowego</returns>
    /// <remarks>
    /// 1.5 pkt.
    /// Domknięcie grafu to graf, w którym krawędzią połączone są wierzchołki, 
    /// pomiędzy którymi istnieje ścieżka w wyjściowym grafie (pętle wykluczamy).
    /// Graf wejściowy pozostaje niezmieniony.
    /// Utworzony graf ma taką samą reprezentację jak graf wejściowy.
    /// Uwaga 1 : w przypadku stwierdzenia ze graf wejściowy jest grafem ważonym zgłosić wyjątek ArgumentException
    /// </remarks>
    public static IGraph Closure(this IGraph g)
        {
        return g.Clone(); // zmienic !
        }

    /// <summary>Badanie czy graf jest dwudzielny</summary>
    /// <param name="g">Graf wejściowy</param>
    /// <returns>Informacja czy graf wejściowy jest dwudzielny</returns>
    /// <remarks>
    /// 1.5 pkt.
    /// Graf wejściowy pozostaje niezmieniony.
    /// </remarks>
    public static bool IsBipartite(this IGraph g)
        {
        return false; // zmienic !
        }

    }

}
