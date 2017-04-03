
using System.Collections.Generic;
using System.Linq;

namespace ASD.Graphs
{

    /// <summary>
    /// Klasa rozszerzająca interfejs IGraph o rozwiązania problemów największej kliki i izomorfizmu podgrafu metodą pełnego przeglądu (backtracking)
    /// </summary>
    public static class CliqueGraphExtender
    {

        /// <summary>
        /// Wyznacza liczbę klikową grafu
        /// </summary>
        /// <param name="g">Badany graf</param>
        /// <param name="clique">Wierzchołki znalezionej kliki (parametr wyjściowy)</param>
        /// <returns>Rozmiar największej kliki</returns>
        /// <remarks>
        /// Liczba klikowa grafu G to rozmiar największego grafu pełnego będącego podgrafem G
        /// </remarks>
        public static int CliqueNumber(this Graph g, out int[] clique)
        {

            // Wskazówki
            // 1) w sposób systematyczny sprawdzać wszystkie podzbiory wierzchołków grafu, czy tworzą podgraf pełny
            // 2) unikać wielokrotnego sprawdzania tego samego podzbioru
            // 3) zastosować algorytm z powrotami (backtracking)
            // 4) zdefiniować pomocniczą funkcję rekurencyjną
            // 5) do badania krawędzi pomiędzy wierzchołkami i oraz j użyć metody GetEdgeWeight(i,j)

            bool[] isVertexInClique = new bool[g.VerticesCount];
            int cliqueNumber = CliqueNumberRecursive(g, ref isVertexInClique, 0);

            List<int> cliqueList = new List<int>(g.VerticesCount);
            for (int v=0; v < g.VerticesCount; v++)
            {
                if (isVertexInClique[v])
                    cliqueList.Add(v);
            }
            clique = cliqueList.ToArray();
            return cliqueNumber;
        }

        private static int CliqueNumberRecursive(Graph g, ref bool[] isVertexInClique, int cliqueSize, int startingVertex = 0)
        {
            bool[] biggestClique = isVertexInClique;
            int biggestCliqueSize = cliqueSize;

            if (cliqueSize > 0)
            {
                // Instead of analizying all the vertices
                // we only take into account neighbors of the last vertex in the clique
                // This is especially benefitial for sparse graphs (the last one in the test set)
                foreach (Edge e in g.OutEdges(startingVertex - 1))
                {
                    if (e.To <= e.From)
                        continue;

                    CheckNewCliqueVertex(g, e.To, cliqueSize, ref isVertexInClique, ref biggestClique, ref biggestCliqueSize);
                }
                    
            }
            else
            {
                // In the initial step we analyze every vertex
                // because any vertex is a valid clique
                for (int v = startingVertex; v < g.VerticesCount; v++)
                    CheckNewCliqueVertex(g, v, cliqueSize, ref isVertexInClique, ref biggestClique, ref biggestCliqueSize);
            }

            isVertexInClique = biggestClique;
            return biggestCliqueSize;
        }

        // Helper method so as not to duplicate code in the CliqueNumberRecursive method
        private static void CheckNewCliqueVertex(Graph g, int v, int cliqueSize, ref bool[] isVertexInClique, ref bool[] biggestClique, ref int biggestCliqueSize)
        {
            // If vertex has a lower degree than there are already vertices in the clique
            // it definitely cannot be added to the clique
            if (g.OutDegree(v) < cliqueSize || g.InDegree(v) < cliqueSize)
                return;

            // Check if there are edges between this vertex (v) and all the vertices already in the clique
            bool validVertex = true;
            for (int cliqueVertex = 0; cliqueVertex < v; cliqueVertex++)
            {
                if (isVertexInClique[cliqueVertex] && (g.GetEdgeWeight(v, cliqueVertex).IsNaN() || g.GetEdgeWeight(cliqueVertex, v).IsNaN()))
                {
                    validVertex = false;
                    break;
                }
            }

            if (!validVertex)
                return;

            isVertexInClique[v] = true;
            bool[] newClique = isVertexInClique.Clone() as bool[];
            int newCliqueSize = CliqueNumberRecursive(g, ref newClique, cliqueSize + 1, v + 1);
            if (newCliqueSize > biggestCliqueSize)
            {
                biggestClique = newClique;
                biggestCliqueSize = newCliqueSize;
            }
            isVertexInClique[v] = false;
        }

        /// <summary>
        /// Bada izomorfizm grafów metodą pełnego przeglądu (backtracking)
        /// </summary>
        /// <param name="g">Pierwszy badany graf</param>
        /// <param name="h">Drugi badany graf</param>
        /// <param name="map">Mapowanie wierzchołków grafu h na wierzchołki grafu g</param>
        /// <returns>Informacja, czy grafy g i h są izomorficzne</returns>
        public static bool IsIzomorpchic(this Graph g, Graph h, out int[] map)
        {
            map = null;
            if (g.VerticesCount != h.VerticesCount || g.EdgesCount != h.EdgesCount || g.Directed != h.Directed)
                return false;
            var helper = new IzomorpchismHelper(g, h);
            map = new int[g.VerticesCount];
            bool[] isVertexInMapping = new bool[g.VerticesCount];
            return helper.FindMapping(0, map, isVertexInMapping);
        }

        /// <summary>
        /// Klasa pomocnicza dla badania izomorfizmu grafów metodą pełnego przeglądu (backtracking)
        /// </summary>
        /// <remarks>
        /// Dzięki wprowadzeniu tej klasy wygodniej implementuje się rekurencyjne badanie izomorfizmu.
        /// </remarks>
        private sealed class IzomorpchismHelper
        {

            /// <summary>
            /// Pierwszy badany graf
            /// </summary>
            private Graph g;

            /// <summary>
            /// Drugi badany graf
            /// </summary>
            private Graph h;

            /// <summary>
            /// Informacja czy dany wierzchołek grafu g już jest wykorzystany w mapowaniu
            /// </summary>
            private bool[] used;

            /// <summary>
            /// Tworzy obiekt klasy pomocniczej dla badania izomorfizmu grafów metodą pełnego przeglądu
            /// </summary>
            /// <param name="g">Pierwszy badany graf</param>
            /// <param name="h">Drugi badany graf</param>
            internal IzomorpchismHelper(Graph g, Graph h)
            {
                this.g = g;
                this.h = h;
                used = new bool[g.VerticesCount];
            }

            /// <summary>
            /// Bada izomorfizm grafów metodą pełnego przeglądu (rekurencyjnie)
            /// </summary>
            /// <param name="currentV">Aktualnie rozważany wierzchołek</param>
            /// <param name="map">Mapowanie wierzchołków grafu h na wierzchołki grafu g</param>
            /// <returns>Informacja czy znaleziono mapowanie definiujące izomotfizm</returns>
            internal bool FindMapping(int currentV, int[] map, bool[] isVertexInMapping)
            {

                // Wskazówki
                // 1) w sposób systematyczny sprawdzać wszystkie potencjalne mapowania
                // 2) unikać wielokrotnego sprawdzania tego samego mapowania
                // 3) zastosować algorytm z powrotami (backtracking)
                // 4) do badania krawędzi pomiędzy wierzchołkami i oraz j użyć metody GetEdgeWeight(i,j)

                // We assume that vertices 0 - (currentV - 1) are already mapped
                int n = g.VerticesCount;

                if (currentV >= n)
                    return true;

                // I assume that map[i] = j means, that vertex i in graph G is isomorphic with vertex j in graph H

                // We check every possible vertex from graph H that is not yet used
                // and try to match it with currentV (from graph G)
                for (int vH = 0; vH < n; vH++)
                {
                    if (isVertexInMapping[vH])
                        continue;
                    if (g.OutDegree(currentV) != h.OutDegree(vH) || g.InDegree(currentV) != h.InDegree(vH))
                        continue;

                    // Check every neighbor of currentV that is already in mapping, if it matches neighbors of vH
                    map[currentV] = vH;
                    bool validMatch = true;
                    foreach (Edge eG in g.OutEdges(currentV))
                    {
                        // Edge leads to a neighbor that is not yet in the mapping (because we already analyzed vertices 0 - (currentVertex - 1))
                        if (eG.To > currentV)
                            continue;

                        // Edge weights should be equal in both graphs
                        double edgeWeightH = h.GetEdgeWeight(vH, map[eG.To]);
                        if (eG.Weight != edgeWeightH)
                        {
                            validMatch = false;
                            break;
                        }

                        // The same goes for reverse edges
                        double reverseEdgeWeightG = g.GetEdgeWeight(eG.To, currentV);
                        double reverseEdgeWeightH = h.GetEdgeWeight(map[eG.To], vH);
                        if ((!reverseEdgeWeightG.IsNaN() || !reverseEdgeWeightH.IsNaN()) && reverseEdgeWeightG != reverseEdgeWeightH)
                        {
                            validMatch = false;
                            break;
                        }
                    }
                    if (!validMatch)
                        continue;

                    // vH in H is isomorphic with currentV in G
                    isVertexInMapping[vH] = true;
                    bool isomorphismFound = FindMapping(currentV + 1, map, isVertexInMapping);
                    if (isomorphismFound)
                        return true;

                    isVertexInMapping[vH] = false;

                    // Isomorphism with this matching not found, look for other possibilities of vertices isomorphic with currentV
                }

                // No isomorphism found
                return false;
            }

        }  // class IzomorpchismHelper

    }

}