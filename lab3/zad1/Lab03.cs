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
        // Metoda podobna do BFS, zaczynamy od wierzchołków, do których nie da się wejść
        // (mają InDegree == 0)
        int[] order = new int[graph.VerticesCount];
        for (int i = 0; i < order.Length; i++)
            order[i] = -1;

        List<int> startingVerticies = new List<int>();
        for (int v = 0; v < graph.VerticesCount; v++)
        {
            if (graph.InDegree(v) == 0)
                startingVerticies.Add(v);
        }

        foreach (int v in startingVerticies)
        {
            bool[] vertexVisited = new bool[graph.VerticesCount];
            for (int i = 0; i < vertexVisited.Length; i++)
                vertexVisited[i] = false;

            EdgesQueue edgesPending = new EdgesQueue();

            // Dodajemy wszystkie krawędzie od wierzchołka początkowego
            foreach (Edge e in graph.OutEdges(v))
                edgesPending.Put(e);
            vertexVisited[v] = true;
            // Początkowy wierzchołek ma numer 0 w kolejności
            order[v] = 0;

            while (!edgesPending.Empty)
            {
                Edge e = edgesPending.Get();
                if (vertexVisited[e.To] && order[e.From] > order[e.To])
                    return null;
                vertexVisited[e.To] = true;

                // Jeżeli wierzchołek ma już numer, to nie idziemy do niego
                if (order[e.To] > -1)
                    continue;
                
                // Ustawiamy kolejność i dodajemy wszystkie krawędzie
                order[e.To] = order[e.From] + 1;
                foreach (Edge outEdge in graph.OutEdges(e.To))
                    edgesPending.Put(outEdge);
            }
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
        int[] order = new int[graph.VerticesCount];
        for (int i = 0; i < order.Length; i++)
            order[i] = -1;

        Predicate<int> setVertexOrder = v =>
        {
            int minNeighborOrder = graph.VerticesCount;
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

