using System;

namespace ASD.Graphs
{

    public static class ColoringExtender
    {

        // koloruje graf algorytmem zachlannym (byc moze niepotymalnie)
        public static int GreedyColor(this Graph g, out int[] colors)
        {
            // kazdemu wierzcholkowi 
            // przydzielamy najmniejszy kolor nie kolidujacy z juz pokolorowanymi sasiadami
            // (wpisujemy go do tablicy colors)
            // zwracamy liczbe uzytych kolorow

            int n = g.VerticesCount;
            colors = new int[n];
            if (n == 0)
                return 0;

            int maxColorUsed = 1;
            colors[0] = 0;
            bool[] colorsUsedByNeighbors = new bool[n];
            for (int v = 1; v < n; ++v)
            {
                for (int i = 0; i < n; i++)
                    colorsUsedByNeighbors[i] = false;

                foreach (Edge e in g.OutEdges(v))
                {
                    if (e.To >= v)
                        continue;
                    colorsUsedByNeighbors[colors[e.To]] = true;
                }

                int lowestColorForV = -1;
                for (int i = 0; i < n; i++)
                {
                    if (!colorsUsedByNeighbors[i])
                    {
                        lowestColorForV = i;
                        break;
                    }
                }

                colors[v] = lowestColorForV;
                if (lowestColorForV > maxColorUsed)
                    maxColorUsed = lowestColorForV;
            }

            return maxColorUsed + 1;
        }

        // koloruje graf algorytmem z powrotami (optymalnie)
        public static int BacktrackingColor(this Graph g, out int[] colors)
        {
            var gc = new Coloring(g);
            gc.Color(0, new int[g.VerticesCount], 0);
            colors = gc.bestColors;
            return gc.bestColorsNumber;
        }

        // klasa pomocnicza dla algorytmu z powrotami
        private sealed class Coloring
        {

            // tablica pamietajaca najlepsze dotychczas znalezione pokolorowanie
            internal int[] bestColors = null;

            // zmienna pamietajaca liczbe kolorow w najlepszym dotychczas znalezionym pokolorowaniu
            internal int bestColorsNumber;

            internal int n;

            // badany graf
            private Graph g;

            // konstruktor
            internal Coloring(Graph g)
            {
                this.g = g;
                n = g.VerticesCount;
                bestColorsNumber = n + 1;
                bestColors = new int[n];
            }

            // rekurencyjna metoda znajdujaca najlepsze pokolorowanie
            // v - wierzcholek do pokolorowania
            // colors - tablica kolorow
            // k - maksymalny kolor u¿yty w colors
            internal void Color(int v, int[] colors, int k)
            {
                if (v == n)
                {
                    if (bestColorsNumber > k + 1)
                    {
                        for (int i = 0; i < n; i++)
                            bestColors[i] = colors[i];
                        bestColorsNumber = k + 1;
                    }
                    return;
                }
                
                
                bool[] colorsUsedByNeighbors = new bool[n];
                foreach (Edge e in g.OutEdges(v))
                {
                    if (e.To >= v)
                        continue;
                    colorsUsedByNeighbors[colors[e.To]] = true;
                }

                for (int i = 0; i < bestColorsNumber; i++)
                {
                    if (colorsUsedByNeighbors[i])
                        continue;
                    colors[v] = i;
                    Color(v + 1, colors, Math.Max(i, k));
                }
            }

        }  // class Coloring

    }  // class ColoringExtender

}  // namespace ASD.Graph
