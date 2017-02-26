using System;
using System.Collections.Generic;
using ASD.Graphs;

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
    public static Graph LineGraph(Graph graph, out string[] desc)
    {
        Graph t = new AdjacencyMatrixGraph(false, graph.EdgesCount);
        int[,] vert = new int[graph.VerticesCount, graph.VerticesCount];
        for (int i = 0; i < graph.VerticesCount; i++) for (int j = 0; j < graph.VerticesCount; j++) vert[i, j] = -1;
        int k = 0;
        string[] description = new string[graph.EdgesCount];

        for (int v = 0; v < graph.VerticesCount; ++v)
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

        desc = description;
        return t;
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
    public static int[] TopologicalSort_V0(Graph graph)
    {
        // Działamy na kopii grafu, bo będziemy usuwali krawędzie
        Graph clonedGraph = graph.Clone();

        int verticesCount = graph.VerticesCount;
        int[] order = new int[verticesCount];
        for (int i = 0; i < order.Length; i++)
            order[i] = -1;

        int key = 0;

        while (key < verticesCount)
        {
            bool deleted = false;
            for (int v = 0; v < verticesCount; v++)
            {
                if (clonedGraph.InDegree(v) == 0 && order[v] == -1)
                {
                    order[v] = key++;
                    foreach (Edge e in clonedGraph.OutEdges(v))
                        clonedGraph.DelEdge(e);
                    deleted = true;
                }
            }

            if (!deleted)
                return null;
        }

        return order;
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
    public static int[] TopologicalSort_DFS(Graph graph)
    {
        int verticesCount = graph.VerticesCount;
        int[] order = new int[verticesCount];
        for (int i = 0; i < verticesCount; i++)
            order[i] = verticesCount;

        Predicate<int> setVertexOrder = v =>
        {
            int minNeighborOrder = verticesCount;
            foreach (Edge e in graph.OutEdges(v))
                minNeighborOrder = Math.Min(minNeighborOrder, order[e.To]);

            order[v] = minNeighborOrder - 1;
            return true;
        };

        int counter = 0;
        graph.DFSearchAll(null, setVertexOrder, out counter);

        return order;
    }
}

