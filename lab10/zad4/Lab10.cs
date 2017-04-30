
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
        map = null;
        return true;
    }

}

