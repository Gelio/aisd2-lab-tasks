
using System.Collections.Generic;
using ASD.Graphs;

/// <summary>
/// Klasa rozszerzająca klasę Graph o rozwiązania problemów największej kliki i izomorfizmu grafów metodą pełnego przeglądu (backtracking)
/// </summary>
public static class Lab10GraphExtender
{
    /// <summary>
    /// Wyznacza największą klikę w grafie i jej rozmiar metodą pełnego przeglądu (backtracking)
    /// </summary>
    /// <param name="g">Badany graf</param>
    /// <param name="clique">Wierzchołki znalezionej największej kliki - parametr wyjściowy</param>
    /// <returns>Rozmiar największej kliki</returns>
    /// <remarks>
    /// 1) Uwzględniamy grafy sierowane i nieskierowane.
    /// 2) Nie wolno modyfikować badanego grafu.
    /// </remarks>
    public static int MaxClique(this Graph g, out int[] clique)
    {
        CliqueBacktracker cliqueBacktracker = new CliqueBacktracker(g);
        cliqueBacktracker.FindMaxClique(new List<int>(g.VerticesCount), 0);
        clique = cliqueBacktracker.maxClique;
        return clique.Length;
    }

    private class CliqueBacktracker
    {
        private Graph _g;
        private int _n;
        public int[] maxClique = null;
        public int maxCliqueSize = 0;

        public CliqueBacktracker(Graph g)
        {
            _g = g;
            _n = g.VerticesCount;
        }

        public void FindMaxClique(List<int> currentClique, int nextV)
        {
            if (currentClique.Count > maxCliqueSize)
            {
                maxClique = currentClique.ToArray();
                maxCliqueSize = currentClique.Count;
            }

            if (nextV == _n)
                return;

            if (currentClique.Count == _n || maxCliqueSize == _n)
                return;

            for (int v = nextV; v < _n; v++)
            {
                bool extendsClique = true;
                foreach (int cliqueV in currentClique)
                {
                    if (_g.GetEdgeWeight(cliqueV, v).IsNaN() || _g.GetEdgeWeight(v, cliqueV).IsNaN())
                    {
                        extendsClique = false;
                        break;
                    }
                }

                if (!extendsClique)
                    continue;

                currentClique.Add(v);
                FindMaxClique(currentClique, v + 1);
                currentClique.RemoveAt(currentClique.Count - 1);
            }
        }
    }

    /// <summary>
    /// Bada izomorfizm grafów metodą pełnego przeglądu (backtracking)
    /// </summary>
    /// <param name="g">Pierwszy badany graf</param>
    /// <param name="h">Drugi badany graf</param>
    /// <param name="map">Mapowanie wierzchołków grafu h na wierzchołki grafu g (jeśli grafy nie są izomorficzne to null) - parametr wyjściowy</param>
    /// <returns>Informacja, czy grafy g i h są izomorficzne</returns>
    /// <remarks>
    /// 1) Nie wolno korzystać z bibliotecznych metod do badania izomorfizmu
    /// 2) Uwzględniamy wagi krawędzi i "skierowalność grafu"
    /// 3) Nie wolno modyfikować badanych grafów.
    /// </remarks>
    public static bool IsomorpchismTest(this Graph g, Graph h, out int[] map)
    {
        if (g.VerticesCount != h.VerticesCount)
        {
            map = null;
            return false;
        }

        //map = h.Isomorpchism(g);
        //bool isIsomorphic = map == null;

        IsomorphismBacktracker isomorphismBacktracker = new IsomorphismBacktracker(g, h);
        map = new int[g.VerticesCount];
        bool isIsomorphic = isomorphismBacktracker.FindIsomorphism(map, 0);
        if (!isIsomorphic)
            map = null;
        return isIsomorphic;
    }

    private class IsomorphismBacktracker
    {
        private Graph _g;
        private Graph _h;
        private int _n;
        private bool[] _gVertexUsed = null;

        public IsomorphismBacktracker(Graph g, Graph h)
        {
            _g = g;
            _h = h;
            _n = g.VerticesCount;
            _gVertexUsed = new bool[_n];
        }

        public bool FindIsomorphism(int[] map, int nextHVertex)
        {
            // vertex vH of graph H is isomorphic with vertex map[vH] of graph G (map[vH] = vG)

            if (nextHVertex == _n)
                return true;

            for (int gVertex = 0; gVertex < _n; gVertex++)
            {
                if (_gVertexUsed[gVertex])
                    continue;

                if (_g.OutDegree(gVertex) != _h.OutDegree(nextHVertex) || _g.InDegree(gVertex) != _h.OutDegree(nextHVertex))
                    continue;

                map[nextHVertex] = gVertex;
                bool validVertex = true;
                foreach (Edge hEdge in _h.OutEdges(nextHVertex))
                {
                    if (hEdge.To > nextHVertex)
                        continue;
                    if (hEdge.Weight != _g.GetEdgeWeight(gVertex, map[hEdge.To]))
                    {
                        validVertex = false;
                        break;
                    }
                }

                if (!validVertex)
                    continue;

                _gVertexUsed[gVertex] = true;
                if (FindIsomorphism(map, nextHVertex + 1))
                    return true;
                _gVertexUsed[gVertex] = false;
            }

            return false;
        }
    }

}

