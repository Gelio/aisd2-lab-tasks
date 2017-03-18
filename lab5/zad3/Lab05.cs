
namespace ASD.Graphs
{

    public static class Lab05GraphExtender
    {

        /// <summary>
        /// Wyznacza silnie spójne składowe
        /// </summary>
        /// <param name="g">Badany graf</param>
        /// <param name="scc">Silnie spójne składowe (parametr wyjściowy)</param>
        /// <returns>Liczba silnie spójnych składowych</returns>
        /// <remarks>
        /// scc[v] = numer silnie spójnej składowej do której należy wierzchołek v<br/>
        /// (numerujemy od 0)<br/>
        /// <br/>
        /// Metoda uruchomiona dla grafu nieskierowanego zgłasza wyjątek <see cref="System.ArgumentException"/>.
        /// <br/>
        /// Graf wejściowy pozostaje niezmieniony.
        /// </remarks>
        public static int StronglyConnectedComponents(this Graph g, out int[] scc)
        {
            if (!g.Directed)
                throw new System.ArgumentException("Graf jest nieskierowany");

            // Algorytm Kosaraju
            int[] initialOrder = new int[g.VerticesCount];
            int currentOrderValue = 0;
            g.GeneralSearchAll<EdgesStack>(null, v =>
            {
                initialOrder[currentOrderValue++] = v;
                return true;
            }, null, out int cc);

            int[] vertexInComponent = new int[g.VerticesCount];
            Graph reversed = g.CustomReverse(); // można skorzystać z bibliotecznego g.Reverse, ale zapewne o to chodziło w zadaniu, żeby zrobić własne

            bool[] visited = new bool[g.VerticesCount];
            int leftToVisit = g.VerticesCount;
            int currentComponent = 0;
            while (leftToVisit > 0)
            {
                int startingVertex = 0;
                for (int i = g.VerticesCount - 1; i >= 0; i--)
                {
                    int v = initialOrder[i];
                    if (!visited[v])
                    {
                        startingVertex = v;
                        break;
                    }
                }

                reversed.GeneralSearchFrom<EdgesStack>(startingVertex, v =>
                {
                    leftToVisit--;
                    vertexInComponent[v] = currentComponent;
                    return true;
                }, null, null, visited);
                currentComponent++;
            }


            scc = vertexInComponent;
            return currentComponent;
        }

        /// <summary>
        /// Wyznacza odwrotność grafu
        /// </summary>
        /// <param name="g">Badany graf</param>
        /// <returns>Odwrotność grafu</returns>
        /// <remarks>
        /// Odwrotność grafu to graf skierowany o wszystkich krawędziach przeciwnie skierowanych niż w grafie pierwotnym.<br/>
        /// <br/>
        /// Metoda uruchomiona dla grafu nieskierowanego zgłasza wyjątek <see cref="System.ArgumentException"/>.
        /// <br/>
        /// Graf wejściowy pozostaje niezmieniony.
        /// </remarks>
        public static Graph CustomReverse(this Graph g)
        {
            Graph reversed = g.IsolatedVerticesGraph();
            g.GeneralSearchAll<EdgesQueue>(null, null, e =>
            {
                reversed.AddEdge(e.To, e.From, e.Weight);
                return true;
            }, out int cc);
            return reversed;
        }

        /// <summary>
        /// Wyznacza jądro grafu
        /// </summary>
        /// <param name="g">Badany graf</param>
        /// <returns>Jądro grafu</returns>
        /// <remarks>
        /// Jądro grafu to graf skierowany, którego wierzchołkami są silnie spójne składowe pierwotnego grafu.<br/>
        /// Cała silnie spójna składowa jest "ściśnięta" do jednego wierzchiłka.<br/>
        /// Wierzchołki jądra są połączone krawędzią gdy w grafie pierwotnym połączone są krawędzią dowolne
        /// z wierzchołków odpowiednich składowych (ale nie wprowadzamy pętli !). Wagi krawędzi przyjmujemy równe 1.<br/>
        /// <br/>
        /// Metoda uruchomiona dla grafu nieskierowanego zgłasza wyjątek <see cref="System.ArgumentException"/>.
        /// <br/>
        /// Graf wejściowy pozostaje niezmieniony.
        /// </remarks>
        public static Graph Kernel(this Graph g)
        {
            if (!g.Directed)
                throw new System.ArgumentException("Graf jest nieskierowany");

            int componentsCount = g.StronglyConnectedComponents(out int[] vertexInComponent);
            Graph kernel = g.IsolatedVerticesGraph(g.Directed, componentsCount);

            // Funkcja znajdująca dowolny wierzchołek z wybranej silnie spójnej składowej
            System.Func<int, int> FindVertexFromComponent = componentNumber =>
            {
                for (int v = 0; v < vertexInComponent.Length; v++)
                {
                    if (vertexInComponent[v] == componentNumber)
                        return v;
                }

                return int.MaxValue;
            };

            for (int componentNumber = 0; componentNumber < componentsCount; componentNumber++)
            {
                // Zaczynając od dowolnego wierzchołka w aktualnie rozpatrywanej silnie spójnej składowej
                // szukamy innych bezpośrednio połączonych silnie spójnych składowych.
                // Czyli przeszukujemy graf aż dojdziemy do wierzchołka, który należy do innej składowej niż
                // ta, z której zaczynaliśmy przeszukiwanie
                int componentVertex = FindVertexFromComponent(componentNumber);
                bool[] wasEverInQueue = new bool[g.VerticesCount];
                EdgesQueue edgesQueue = new EdgesQueue();

                // Zaczynamy przeszukiwanie
                foreach (Edge e in g.OutEdges(componentVertex))
                    edgesQueue.Put(e);
                wasEverInQueue[componentVertex] = true;

                bool[] isComponentConnected = new bool[componentsCount];

                while (!edgesQueue.Empty)
                {
                    Edge e = edgesQueue.Get();
                    if (vertexInComponent[e.To] == componentNumber)
                    {
                        // Idziemy dalej w tej samej silnie spójnej składowej
                        foreach (Edge outEdge in g.OutEdges(e.To))
                        {
                            if (!wasEverInQueue[outEdge.To])
                            {
                                wasEverInQueue[outEdge.To] = true;
                                edgesQueue.Put(outEdge);
                            }
                        }
                    }
                    else
                    {
                        // Dochodzimy do innej składowej
                        isComponentConnected[vertexInComponent[e.To]] = true;
                    }
                }

                for (int i = 0; i < componentsCount; i++)
                {
                    if (isComponentConnected[i])
                        kernel.AddEdge(componentNumber, i);
                }
                    

            }
            return kernel;
        }

        /// <summary>
        /// Wyznacza ścieżki o maksymalnej przepustowości
        /// </summary>
        /// <param name="g">Badany graf</param>
        /// <param name="s">Wierzchołek źródłowy</param>
        /// <param name="d">Znalezione ścieżki (parametr wyjściowy)</param>
        /// <returns><b>true</b></returns>
        /// <remarks>
        /// Metoda przydaje się w algorytmach wyznaczania maksymalnego przepływu, wagi krawędzi oznaczają tu przepustowość krawędzi.<br/>
        /// Przepustowość ścieżki to najmniejsza z przepustowości krawędzi wchodzących w jej skład.<br/>
        /// <br/>
        /// Elementy tablicy <i>d</i> zawierają przepustowości ścieżek od źródła do wierzchołka określonego przez indeks elementu.<br/>
        /// Jeśli ścieżka od źródła do danego wierzchołka nie istnieje, to przepustowość ma wartość <b>null</b>.
        /// <br/>
        /// Metoda zawsze zwraca <b>true</b> (można ją stosować do każdego grafu).
        /// </remarks>
        public static bool MaxFlowPathsLab05(this Graph g, int s, out PathsInfo[] d)
        {
            d = new PathsInfo[g.VerticesCount];
            return true;
        }

        /// <summary>
        /// Bada czy graf nieskierowany jest acykliczny
        /// </summary>
        /// <param name="g">Badany graf</param>
        /// <returns>Informacja czy graf jest acykliczny</returns>
        /// <remarks>
        /// Metoda uruchomiona dla grafu skierowanego zgłasza wyjątek <see cref="System.ArgumentException"/>.
        /// <br/>
        /// Graf wejściowy pozostaje niezmieniony.
        /// </remarks>
        public static bool IsUndirectedAcyclic(this Graph g)
        {
            if (g.Directed)
                throw new System.ArgumentException("Graf jest skierowany");

            bool[] isVertexInCurrentPath = new bool[g.VerticesCount];
            EdgesStack edgesInCurrentPath = new EdgesStack();
            bool isAcyclic = g.GeneralSearchAll<EdgesStack>(v =>
            {
                isVertexInCurrentPath[v] = true;
                return true;
            }, v =>
            {
                isVertexInCurrentPath[v] = false;
                return true;
            }, e =>
            {
                if (!edgesInCurrentPath.Empty && edgesInCurrentPath.Peek().From == e.To)
                {
                    // Krawędź "powrotna" aktualnej ścieżki
                    edgesInCurrentPath.Get();
                    return true;
                }

                if (isVertexInCurrentPath[e.To])
                    return false;
                edgesInCurrentPath.Put(e);
                return true;
            }, out int cc);

            return isAcyclic;
        }

    }  // class Lab05GraphExtender

}  // namespace ASD.Graph
