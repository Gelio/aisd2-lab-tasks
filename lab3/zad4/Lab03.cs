
using System;
using System.Collections.Generic;
using ASD.Graphs;

namespace ASD.Lab03
{

    public static class Lab03GraphExtender
    {

        // Część 1
        // Wyznaczanie odwrotności grafu
        //   0.5 pkt
        // Odwrotność grafu to graf skierowany o wszystkich krawędziach przeciwnie skierowanych niż w grafie pierwotnym
        // Parametry:
        //   g - graf wejściowy
        // Wynik:
        //   odwrotność grafu
        // Uwagi:
        //   1) Metoda uruchomiona dla grafu nieskierowanego powinna zgłaszać wyjątek Lab03Exception
        //   2) Graf wejściowy pozostaje niezmieniony
        //   3) Graf wynikowy musi być w takiej samej reprezentacji jak wejściowy
        public static Graph Lab03Reverse(this Graph g)
        {
            if (!g.Directed)
                throw new Lab03Exception();

            Graph reversed = g.IsolatedVerticesGraph();
            bool[] wasVertexInQueue = new bool[g.VerticesCount];
            Queue<int> verticesToVisit = new Queue<int>();

            for (int i=0; i < g.VerticesCount; i++)
            {
                if (wasVertexInQueue[i])
                    continue;

                verticesToVisit.Enqueue(i);
                wasVertexInQueue[i] = true;
                while (verticesToVisit.Count > 0)
                {
                    int v = verticesToVisit.Dequeue();
                    foreach (Edge e in g.OutEdges(v))
                    {
                        reversed.AddEdge(e.To, v, e.Weight);
                        if (!wasVertexInQueue[e.To])
                        {
                            verticesToVisit.Enqueue(e.To);
                            wasVertexInQueue[e.To] = true;
                        }
                    }
                }
            }
            return reversed;
        }

        // Część 2
        // Badanie czy graf jest dwudzielny
        //   1.5 pkt
        // Graf dwudzielny to graf nieskierowany, którego wierzchołki można podzielić na dwa rozłączne zbiory
        // takie, że dla każdej krawędzi jej końce należą do róźnych zbiorów
        // Parametry:
        //   g - badany graf
        //   vert - tablica opisująca podział zbioru wierzchołków na podzbiory w następujący sposób
        //          vert[i] == 1 oznacza, że wierzchołek i należy do pierwszego podzbioru
        //          vert[i] == 2 oznacza, że wierzchołek i należy do drugiego podzbioru
        // Wynik:
        //   true jeśli graf jest dwudzielny, false jeśli graf nie jest dwudzielny (w tym przypadku parametr vert ma mieć wartość null)
        // Uwagi:
        //   1) Metoda uruchomiona dla grafu skierowanego powinna zgłaszać wyjątek Lab03Exception
        //   2) Graf wejściowy pozostaje niezmieniony
        //   3) Podział wierzchołków może nie być jednoznaczny - znaleźć dowolny
        //   4) Pamiętać, że każdy z wierzchołków musi być przyporządkowany do któregoś ze zbiorów
        //   5) Metoda ma mieć taki sam rząd złożoności jak zwykłe przeszukiwanie (za większą będą kary!)
        public static bool Lab03IsBipartite(this Graph g, out int[] vert)
        {
            if (g.Directed)
                throw new Lab03Exception();

            int[] assignment = new int[g.VerticesCount];
            bool isBiparite = true;

            Predicate<Edge> assignVertexToGroup = e =>
            {
                if (assignment[e.From] == 0)
                    assignment[e.From] = 2;

                // Przypisz albo 1 albo 2 w zależności czy wierzchołek znajduje się w odległości parzystej czy nieparzystej
                // 1 - odległość nieparzysta
                // 2 - odległość parzysta
                int nextDistanceParity = 2 - ((assignment[e.From] + 1) % 2);

                if (assignment[e.To] == 0)
                {
                    assignment[e.To] = nextDistanceParity;
                    return true;
                }

                if (assignment[e.To] == nextDistanceParity)
                    return true;

                isBiparite = false;
                return false;
            };

            g.GeneralSearchAll<EdgesQueue>(null, null, assignVertexToGroup, out int cc);
            if (!isBiparite)
            {
                vert = null;
                return false;
            }

            // Wierzchołki izolowane można przypisać do dowolnej grupy, ale trzeba je przypisać
            for (int i = 0; i < g.VerticesCount; i++)
            {
                if (assignment[i] == 0)
                    assignment[i] = 1;
            }

            vert = assignment;
            return true;
        }

        // Część 3
        // Wyznaczanie minimalnego drzewa rozpinającego algorytmem Kruskala
        //   1.5 pkt
        // Schemat algorytmu Kruskala
        //   1) wrzucić wszystkie krawędzie do "wspólnego worka"
        //   2) wyciągać z "worka" krawędzie w kolejności wzrastających wag
        //      - jeśli krawędź można dodać do drzewa to dodawać, jeśli nie można to ignorować
        //      - punkt 2 powtarzać aż do skonstruowania drzewa (lub wyczerpania krawędzi)
        // Parametry:
        //   g - graf wejściowy
        //   mstw - waga skonstruowanego drzewa (lasu)
        // Wynik:
        //   skonstruowane minimalne drzewo rozpinające (albo las)
        // Uwagi:
        //   1) Metoda uruchomiona dla grafu skierowanego powinna zgłaszać wyjątek Lab03Exception
        //   2) Graf wejściowy pozostaje niezmieniony
        //   3) Wykorzystać klasę UnionFind z biblioteki Graph
        //   4) Jeśli graf g jest niespójny to metoda wyznacza las rozpinający
        //   5) Graf wynikowy (drzewo) musi być w takiej samej reprezentacji jak wejściowy
        public static Graph Lab03Kruskal(this Graph g, out double mstw)
        {
            mstw = 0;       // zmienić
            return null;  // zmienić
        }

        // Część 4
        // Badanie czy graf nieskierowany jest acykliczny
        //   0.5 pkt
        // Parametry:
        //   g - badany graf
        // Wynik:
        //   true jeśli graf jest acykliczny, false jeśli graf nie jest acykliczny
        // Uwagi:
        //   1) Metoda uruchomiona dla grafu skierowanego powinna zgłaszać wyjątek Lab03Exception
        //   2) Graf wejściowy pozostaje niezmieniony
        //   3) Najpierw pomysleć jaki, prosty do sprawdzenia, warunek spełnia acykliczny graf nieskierowany
        //      Zakodowanie tefo sprawdzenia nie powinno zająć więcej niż kilka linii!
        //      Zadanie jest bardzo łatwe (jeśli wydaje się trudne - poszukać prostszego sposobu, a nie walczyć z trudnym!)
        public static bool Lab03IsUndirectedAcyclic(this Graph g)
        {
            return false;  // zmienić
        }

    }

}
