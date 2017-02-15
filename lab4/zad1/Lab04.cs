
// dodac do projektu referencje do System.dll

namespace ASD.Graph
{

/// <summary>
/// Rozszerzenie interfejsu <see cref="IGraph"/> o algorytmy wyznaczania minimalnego drzewa rozpinającego grafu i silnie spójnych składowych grafu
/// </summary>
public static class Lab04Extender
    {

    /// <summary>
    /// Wyznacza minimalne drzewo rozpinające grafu algorytmem Boruvki
    /// </summary>
    /// <param name="g">Badany graf</param>
    /// <param name="mst">Wyznaczone drzewo rozpinające (parametr wyjściowy)</param>
    /// <returns>Waga minimalnego drzewa rozpinającego</returns>
    /// <remarks>
    /// Dla grafu skierowanego metoda zgłasza wyjątek <see cref="System.ArgumentException"/>.<br/>
    /// Wyznaczone drzewo reprezentowane jast jako graf bez cykli, to umożliwia jednolitą obsługę sytuacji
    /// gdy analizowany graf jest niespójny, wyznaczany jest wówczas las rozpinający.
    /// </remarks>
    public static int Boruvka(this IGraph g, out IGraph mst)
        {
        if ( g.Directed ) throw new System.ArgumentException("Directed graphs are not allowed");
        IGraph t = g.IsolatedVerticesGraph();
        UnionFind uf = new UnionFind(g.VerticesCount);
        EdgesMinPriorityQueue q = new EdgesMinPriorityQueue();
        int TotalWeight = 0;

        bool change;
        do
        {
            change = false;
            for (int i = 0; i < g.VerticesCount; i++)
            {
                Edge MinEdge = new Edge(i, int.MaxValue, int.MaxValue);
                bool find = false;

                foreach (Edge e in g.OutEdges(i))
                    if((e.Weight < MinEdge.Weight || (e.Weight == MinEdge.Weight && e.To < MinEdge.To)) && uf.Find(e.To) != uf.Find(e.From))
                    {
                        MinEdge = e;
                        find = true;
                    }

                if(find) q.Put(MinEdge);
            }

            while (!q.Empty)
            {
                Edge e = q.Get();                
                if (uf.Find(e.To) != uf.Find(e.From))
                {
                    uf.Union(e.To, e.From);
                    t.AddEdge(e);
                    TotalWeight += e.Weight;
                    change = true;
                }
            }            
        } while (change);
        // utworz graf mst skladajacy sie z izolowanych wierzcholkow
        // zainicjuj "poczatkowe spojne skladowe" jako pojedyncze wierzcholki (klasa UnionFind)
        // utworz pusta kolejke priorytetowa krawedzi q (klasa EdgesMinPriorityQueue)
        //
        // dopoki cos sie zmienia
        //     {
        //     dla kazdego wierzcholka
        //         {
        //         znajdz najkrotsza wychodzaca z niego krawedz prowadzaca do wierzcholka z innej spojnej skladowej (pamietaj o uwadze !)
        //         wstaw ta krawedz do kolejki priorytetowej q
        //         }
        //     dopoki kolejka q jest niepusta
        //         {
        //         pobierz element (krawedz) z kolejki q
        //         jesli krawedz laczy rozne skladowe spojne
        //             {
        //             dodaj ja do grafu mst
        //             wykonaj inne niezbedne czynnosci
        //             }
        //         }
        //     zwroc wage drzewa
        //     }
        //
        // Uwaga: jesli kilka krawedzi ma te sama wage wybierany krawedz prowadzaca do wierzcholka o najnizszym numerze

        mst=t;  // zmien to !!!
        return TotalWeight;       // zmien to !!!
        }

    /// <summary>
    /// Wyznacza silnie spójne składowe
    /// </summary>
    /// <param name="g">Badany graf</param>
    /// <param name="scc">Silnie spójne składowe (parametr wyjściowy)</param>
    /// <returns>Liczba silnie spójnych składowych</returns>
    /// <remarks>
    /// Metoda uruchomiona dla grafu nieskierowanego zgłasza wyjątek <see cref="System.ArgumentException"/>.
    /// </remarks>
    public static int Tarjan(this IGraph g, out int[] scc)
        {
        if ( !g.Directed ) throw new System.ArgumentException("Undirected graphs are not allowed");
        TarjanHelper helper = new TarjanHelper(g);
        for ( int v=0 ; v<g.VerticesCount ; ++v )
            if ( !helper.visited[v] )
                helper.TarjanSearch(v);
        scc=helper.id;
        return helper.scc_nr;
        }

    private sealed class TarjanHelper
        {
        private IGraph   g;
        private System.Collections.Generic.Stack<int>  s;  // jawnie pamietany stos odwiedzanych wierzcholkow
        private int      nr;        // licznik odwiedzanych wierzcholkow
        internal int     scc_nr;    // licznik slnie spojnych skladowych
        internal bool[]  visited;   // wiadommo
        internal int[]   pref;      // numery prefiksowe wierzcholkow (biezaca wartosc nr w chwili odwiedzania wierzcholka)
        internal int[]   id;        // identyfikatory silnie spojnych skladowych (to mamy policzyc !!!)
        private  int[]   low;       // najnizszy numer prefiksowy wierzcholka osiagalnego z danego przez krawedz powrotną (czyli nie nalezaca do drzewa DFS)

        internal TarjanHelper(IGraph g)
            {
            this.g=g;
            int n=g.VerticesCount;
            s = new System.Collections.Generic.Stack<int>();
            visited = new bool[n];
            pref = new int[n];
            id  = new int[n];
            low = new int[n];
            }

        internal void TarjanSearch(int w)
            {

            int min;   // minimum z numeru prefiksowego wierzcholka w (w przeszukiwaniu DFS)
                       // oraz wartosci low[u] dla wszystkich wierzcholkow u do ktorych prowadz krawedz z w
            visited[w] = true;
            s.Push(w);
            pref[w] = nr;
            low[w] = nr;
            min = nr;
            foreach (Edge e in g.OutEdges(w))
            {
                if (!visited[e.To]) 
                {
                    nr++;
                    TarjanSearch(e.To);
                }
                if (low[e.To] < min) min = low[e.To];
            }

            if (min < low[w])
            {
                low[w] = min;
                return;
            }

            int v;
            do
            {
                v = s.Pop();
                id[v] = scc_nr;
                low[v] = g.VerticesCount;
            } while (v != w);
            scc_nr++;
            
            // oznacz wierzcholek w jako odwiedzony
            // umiesc wierzcholek w na stosie s
            // przypisz zmiennym min, low[w], pref[w] numer prefiksowy zwiazany z przeszukiwaniem DFS
            // dla kazdej krawedzi <w,u> wychodzecej z w
            //     {
            //     jesli u jest nieodwiedzony wywolaj rekurencyjnie TarjanSearch(u)
            //     jesli trzeba zaktualizuj min (przy pomocy low[u]) 
            //     }
            // jesli min < low[w]
            //     {
            //     zaktualizuj low[w] (przy pomocy min)
            //     wyjdz z procedury
            //     }
            // powtarzaj
            //     {
            //     pobierz do v element ze stosu s
            //     przypisz id[v] = numer silnie spojnej skladowej
            //     przypisz low[v] duza wartosc (np. liczbe wierzcholkow grafu)
            //     }
            // dopoki v jest rozne od w;
            // zwieksz (o 1) numer silnie spojnej skladowej

            }

        }

    }  // class Lab04Extender

}  // namespace ASD.Graph
