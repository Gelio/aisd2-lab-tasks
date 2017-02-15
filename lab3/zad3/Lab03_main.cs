using System;
using ASD.Graph;

namespace Lab03
{

    class Program
    {

        static void Main(string[] args)
        {
            try
            {
                var rgg = new RandomGraphGenerator(123);
                IGraph h1, h2;
                IGraph g1 = new AdjacencyMatrixGraph(false, 5);
                IGraph g2 = new AdjacencyListsGraph(true, 4);
                Console.WriteLine("# licznik: {0}", Graph.Counter);

                g1.AddEdge(0, 1);
                g1.AddEdge(1, 2);
                g1.AddEdge(0, 2);
                g1.AddEdge(0, 4);
                g1.AddEdge(2, 4);
                g1.AddEdge(2, 3);
                Console.WriteLine("# licznik: {0}", Graph.Counter);
                Console.WriteLine("Graf g1 jest typu: {0} i jest skierowany: {1}", g1.GetType(), g1.Directed);


                h1 = g1.AddVertex();
                Console.WriteLine("\nDodawanie\nGraf h1 ma {0} (powinno być 6) wierzchołków, a ostatni wierzchołek ma stopień {1} (powinno być 0)",
                    h1.VerticesCount, h1.InDegree(h1.VerticesCount - 1));
                Console.WriteLine("Graf h1 ma {0} (powinno być 6) krawędzi", h1.EdgesCount);
                Console.WriteLine("# licznik: {0}", Graph.Counter);


                h1 = g1.DeleteVertex(2);
                Console.WriteLine("\nUsuwanie\nGraf h1 ma {0} (powinno być 4) wierzchołków, a wierzchołki 1,2,3 mają odpowiednio stopienie {1} (powinno być 1),{2} (powinno być 0),{3} (powinno być 1)",
                    h1.VerticesCount, h1.InDegree(1), h1.InDegree(2), h1.InDegree(3));
                Console.WriteLine("Graf h1 ma {0} (powinno być 2) krawędzi", h1.EdgesCount);
                Console.WriteLine("# licznik: {0}", Graph.Counter);


                h1 = g1.Complement();
                Console.WriteLine("\nDopełnienie g1 ma {0} (powinno być 4) krawędzi", h1.EdgesCount);
                Console.WriteLine("# licznik: {0}", Graph.Counter);


                h1 = g1.Closure();
                Console.WriteLine("\nDomknięcie g1 ma {0} (powinno być 10) krawędzi", h1.EdgesCount);
                Console.WriteLine("# licznik: {0}", Graph.Counter);

                h1 = (g1.AddVertex()).Closure();
                Console.WriteLine("Domknięcie g1 + K1 ma {0} (powinno być 10) krawędzi", h1.EdgesCount);
                Console.WriteLine("# licznik: {0}", Graph.Counter);


                Console.WriteLine("\nCzy h1 jest dwudzielny ?: {0} (powinno być False)", h1.IsBipartite());
                Console.WriteLine("# licznik: {0}", Graph.Counter);

                h1 = rgg.BipariteGraph(typeof(AdjacencyMatrixGraph),30,50,0.5);
                Console.WriteLine("\nCzy nowy h1 jest dwudzielny ?: {0} (powinno być True)", h1.IsBipartite());
                Console.WriteLine("# licznik: {0}", Graph.Counter);

                Console.WriteLine("\n\n*************************\n\n");
                g2.AddEdge(0, 1);
                g2.AddEdge(2, 1);
                g2.AddEdge(3, 2);
                Console.WriteLine("# licznik: {0}", Graph.Counter);
                Console.WriteLine("Graf g2 jest typu: {0} i jest skierowany: {1}", g2.GetType(), g2.Directed);

                h2 = g2.AddVertex();
                Console.WriteLine("\nDodawanie\nGraf h2 ma {0} (powinno być 5) wierzchołków, a ostatni wierzchołek ma stopień wy: {1} (powinno być 0) i we: {2} (powinno być 0) ",
                    h2.VerticesCount, h2.InDegree(h2.VerticesCount - 1), h2.OutDegree(h2.VerticesCount - 1));
                Console.WriteLine("Graf h2 ma {0} (powinno być 3) krawędzi", h2.EdgesCount);
                Console.WriteLine("# licznik: {0}", Graph.Counter);


                h2 = g2.DeleteVertex(1);
                Console.WriteLine("\nUsuwanie\nGraf h2 ma {0} (powinno być 3) wierzchołków, a wierzchołek 1 ma stopień wy: {1} (powinno być 0) ", h2.VerticesCount, h2.OutDegree(1));
                Console.WriteLine("Graf h2 ma {0} (powinno być 1) krawędzi", h2.EdgesCount);
                Console.WriteLine("# licznik: {0}", Graph.Counter);


                h2 = g2.Complement();
                Console.WriteLine("\nDopełnienie g2 ma {0} (powinno być 9) krawędzi", h2.EdgesCount);
                Console.WriteLine("# licznik: {0}", Graph.Counter);


                h2 = g2.Closure();
                Console.WriteLine("\nDomknięcie g2 + K1 ma {0} (powinno być 4) krawędzi", h2.EdgesCount);
                Console.WriteLine("# licznik: {0}", Graph.Counter);

                h2 = (g2.AddVertex()).Closure();
                Console.WriteLine("Domknięcie g2 + K1 ma {0} (powinno być 4) krawędzi", h2.EdgesCount);
                Console.WriteLine("# licznik: {0}", Graph.Counter);


                Console.WriteLine("\nCzy h2 jest dwudzielny ?: {0} (powinno być False)", g2.IsBipartite());
                Console.WriteLine("# licznik: {0}", Graph.Counter);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }// class Program

}
