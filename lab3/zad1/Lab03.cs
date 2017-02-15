using System;
using ASD.Graph;

class Lab03
    {

    /// <summary>
    /// Konstruowanie grafu krawedziowego
    /// </summary>
    /// <param name="graph">Zadany graf</param>
    /// <param name="desc">Tablica opisow wierzcholkow grafu krawedziowego</param>
    /// <returns>Skonsturowany graf krawedziowy</returns>
    /// <remarks>
    /// Wierzcholek grafu krawedziowego odpowiadajacy krawedzi <u,v> grafu pierwotnego ma opis "u-v"
    /// Czyli np. dla krawedzi <1,2> powinno byc "1-2".
    /// </remarks>
    public static IGraph LineGraph(IGraph graph, out string[] desc)
        {
            IGraph t = new AdjacencyMatrixGraph(false, graph.EdgesCount);
            int[,] vert = new int[graph.VerticesCount,graph.VerticesCount];
            for (int i = 0; i < graph.VerticesCount; i++) for (int j = 0; j < graph.VerticesCount; j++) vert[i, j] = -1;
            int k = 0;
            string[] description = new string[graph.EdgesCount];

            for ( int v=0 ; v<graph.VerticesCount ; ++v )
            {

                foreach (Edge e1 in graph.OutEdges(v))
                    if (vert[e1.From, e1.To] == -1)
                    {
                        description[k] = e1.From.ToString() + "-" + e1.To.ToString();
                        vert[e1.From, e1.To] = vert[e1.To, e1.From] = k++;

                    }
            }

            for (int v = 0; v < graph.VerticesCount; ++v)
            {

                foreach (Edge e1 in graph.OutEdges(v))
                    foreach (Edge e2 in graph.OutEdges(e1.To))
                        if (vert[e1.From, e1.To] != vert[e2.From, e2.To])
                            t.AddEdge(vert[e1.From, e1.To], vert[e2.From, e2.To]);
                         
            }
        
        desc=description;     // zmienic
        return t;  // zmienic (wynikiem ma byc calkiem inny graf !!!)
        }

    /// <summary>
    /// Sortowanie topologiczne z wykorzystaniem wierzchołków o stopniu wejściowym równym zero
    /// </summary>
    /// <param name="graph">Graf dla ktorego chcemy znalezc porzadek topologiczny</param>
    /// <returns>tablica nowych numerow wierzcholkow, gdy graf zawiera cykl zwracamy null</returns>
    /// <remarks>
    /// Oznaczmy wynikowa tablice jako ord.
    /// Wowczas: ord[i] to numer wierzcholka i w porzadku topologicznym
    /// Nie wolno zmianiac zadanego grafu !!!
    /// </remarks>
    public static int[] TopologicalSort_V0(IGraph graph)
        {
            IGraph t = graph.Clone();
            int[] ord = new int[graph.VerticesCount];            
            for (int i = 0; i < ord.Length; i++) ord[i] = -1;           

            int key = 0;

            while(key < t.VerticesCount)
            {
                bool deleted = false;
                for (int v = 0; v < t.VerticesCount; ++v)
                    if (t.InDegree(v) == 0 && ord[v] == -1)
                    {
                        ord[v] = key++;
                        foreach (Edge e in t.OutEdges(v)) t.DelEdge(e);
                        deleted = true;
                    }
                
                if (deleted == false) return null;
            }
            
            
        //for(int i = 0; i < ord.Length; i++) Console.WriteLine("{0}. {1}",i, ord[i]);

        return ord;  // zmienic
        }

    /// <summary>
    /// Sortowanie topologiczne z uzyciem DFS
    /// </summary>
    /// <param name="graph">Graf acykliczny dla ktorego chcemy znalezc porzadek topologiczny</param>
    /// <returns>tablica nowych numerow wierzcholkow</returns>
    /// <remarks>
    /// Oznaczmy wynikowa tablice jako ord.
    /// Wowczas: ord[i] to numer wierzcholka i w porzadku topologicznym
    /// Nie wolno zmianiac zadanego grafu !!!
    /// </remarks>
    public static int[] TopologicalSort_DFS(IGraph graph)
        {
        return null;  // zmienic
        }
}

