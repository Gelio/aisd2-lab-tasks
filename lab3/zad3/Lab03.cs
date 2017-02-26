
using System;
using System.Collections.Generic;

namespace ASD.Graphs
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
        public static Graph AddVertex(this Graph g)
        {
            int originalVerticesCount = g.VerticesCount;
            Graph newGraph = g.IsolatedVerticesGraph(g.Directed, originalVerticesCount + 1);
            for (int v=0; v < originalVerticesCount; v++)
            {
                foreach (Edge e in g.OutEdges(v))
                {
                    // W nieskierowanych dodajemy tylko krawędzie od wierzchołka o niższym indeksie
                    // do wierzchołka o wyższym indeksie, żeby uniknąć podwójnego dodawania krawędzi
                    if (!g.Directed)
                    {
                        if (e.From > e.To)
                            continue;
                    }

                    newGraph.AddEdge(e);
                }
            }

            return newGraph;
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
        public static Graph DeleteVertex(this Graph g, int del)
        {

            int originalVerticesCount = g.VerticesCount;
            Graph newGraph = g.IsolatedVerticesGraph(g.Directed, originalVerticesCount - 1);
            for (int v = 0; v < originalVerticesCount; v++)
            {
                if (v == del)
                    continue;

                foreach (Edge e in g.OutEdges(v))
                {
                    int fromVertex = e.From;
                    int toVertex = e.To;
                    if (toVertex == del)
                        continue;

                    if (fromVertex > del)
                        fromVertex -= 1;
                    if (toVertex > del)
                        toVertex -= 1;

                    // Podobnie jak przy dodawaniu, krawędź w grafach nieskierowanych dodajemy tylko raz
                    if (!g.Directed && fromVertex > toVertex)
                        continue;

                    newGraph.AddEdge(fromVertex, toVertex);
                }
            }

            return newGraph;
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
        public static Graph Complement(this Graph g)
        {
            int verticesCount = g.VerticesCount;
            double edgeWeight = double.NaN;
            Graph newGraph = g.IsolatedVerticesGraph();

            for (int v = 0; v < verticesCount; v++)
            {
                bool[] shouldEdgeBeAdded = new bool[verticesCount];
                if (!g.Directed)
                {
                    // Nie dodajemy krawędzi "do tyłu" w grafach nieskierowanych
                    for (int i = 0; i <= v; i++)
                        shouldEdgeBeAdded[i] = false;
                    for (int i = v + 1; i < verticesCount; i++)
                        shouldEdgeBeAdded[i] = true;
                }
                else
                {
                    for (int i = 0; i < verticesCount; i++)
                        shouldEdgeBeAdded[i] = true;
                    // Nie dodajemy pętli
                    shouldEdgeBeAdded[v] = false;
                }

                foreach (Edge e in g.OutEdges(v))
                {
                    if (edgeWeight.IsNaN())
                        edgeWeight = e.Weight;
                    if (edgeWeight != e.Weight)
                        throw new ArgumentException("Graf jest grafem ważonym.");

                    shouldEdgeBeAdded[e.To] = false;
                }

                for (int i = 0; i < verticesCount; i++)
                {
                    if (shouldEdgeBeAdded[i])
                        newGraph.AddEdge(v, i);
                }
                    
            }

            return newGraph;
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
        public static Graph Closure(this Graph g)
        {
            int verticesCount = g.VerticesCount;
            double edgeWeight = double.NaN;
            Graph newGraph = g.IsolatedVerticesGraph();

            for (int v = 0; v < verticesCount; v++)
            {
                bool[] shouldEdgeBeAdded = new bool[verticesCount];
                for (int i = 0; i < verticesCount; i++)
                    shouldEdgeBeAdded[i] = false;

                Predicate<Edge> updateEdgesToBeAdded = e =>
                {
                    if (edgeWeight.IsNaN())
                        edgeWeight = e.Weight;
                    if (edgeWeight != e.Weight)
                        throw new ArgumentException("Graf jest grafem ważonym.");

                    // W nieskierowanych nie dodajemy krawędzi "do tyłu"
                    if (g.Directed || e.From < e.To)
                        shouldEdgeBeAdded[e.To] = true;

                    return true;
                };

                g.GeneralSearchFrom<EdgesStack>(v, null, null, updateEdgesToBeAdded);

                // Wykluczamy ewentualną pętlę
                shouldEdgeBeAdded[v] = false;

                for (int i = 0; i < verticesCount; i++)
                {
                    if (shouldEdgeBeAdded[i])
                        newGraph.AddEdge(v, i);
                }
            }

            return newGraph;
        }

        /// <summary>Badanie czy graf jest dwudzielny</summary>
        /// <param name="g">Graf wejściowy</param>
        /// <returns>Informacja czy graf wejściowy jest dwudzielny</returns>
        /// <remarks>
        /// 1.5 pkt.
        /// Graf wejściowy pozostaje niezmieniony.
        /// </remarks>
        public static bool IsBipartite(this Graph g)
        {
            int verticesCount = g.VerticesCount;
            for (int v=0; v < verticesCount; v++)
            {
                int[] distanceFromV = new int[verticesCount];
                for (int i = 0; i < verticesCount; i++)
                    distanceFromV[i] = -1;
                distanceFromV[v] = 0;

                Predicate<Edge> updateDistanceFromV = e =>
                {
                    if (distanceFromV[e.To] > -1)
                    {
                        // Cykle muszą być długości parzystej w grafie dwudzielnym
                        if ((distanceFromV[e.To] + distanceFromV[e.From] + 1) % 2 != 0)
                            return false;
                    }

                    distanceFromV[e.To] = distanceFromV[e.From] + 1;
                    return true;
                };

                if (!g.GeneralSearchFrom<EdgesQueue>(v, null, null, updateDistanceFromV))
                    return false;
            }

            return true;
        }

    }

}
