
using System;
using ASD.Graph;

class Test
{

    public static void Main()
        {
        var ge = new GraphExport();
        var rgg = new RandomGraphGenerator(123);
        int[] order;
        string[] desc;
        int[] ec =new int[3];

        IGraph g = new AdjacencyMatrixGraph(false, 5);
        g.AddEdge(0, 1);
        g.AddEdge(0, 4);
        g.AddEdge(1, 2);
        g.AddEdge(2, 0);
        g.AddEdge(2, 3);
        g.AddEdge(3, 4);
        g.AddEdge(4, 3);
        ge.Export(g,null,"G");

        IGraph lg = Lab03.LineGraph(g,out desc);
        ge.Export(lg,desc,"LG");

        IGraph[] g2 = new IGraph[3]; 
        g2[0] = new AdjacencyMatrixGraph(true, 4);
        g2[0].AddEdge(0, 1);
        g2[0].AddEdge(0, 3);
        g2[0].AddEdge(1, 2);
        g2[0].AddEdge(3, 2);
       
        
        g2[1] = rgg.DAG(typeof(AdjacencyMatrixGraph),100,0.9,1,1);
        g2[2] = rgg.DAG(typeof(AdjacencyListsGraph),1000,0.2,1,1);

        ec[0] = g2[0].EdgesCount;
        ec[1] = g2[1].EdgesCount;
        ec[2] = g2[2].EdgesCount;

        Console.WriteLine("Sortowanie topologiczne - DFS");
        for ( int i=0 ; i<3 ; ++i )
            {
            order = Lab03.TopologicalSort_DFS(g2[i]);
            Console.WriteLine("  test {0} : {1}",i,TopologicalSortTest(g2[i],order));
            }

        if ( ec[0]!=g2[0].EdgesCount || ec[1]!=g2[1].EdgesCount ||ec[2]!=g2[2].EdgesCount )
            Console.WriteLine("  Blad - zmieniono graf");

        ec[0] = g2[0].EdgesCount;
        ec[1] = g2[1].EdgesCount;
        ec[2] = g2[2].EdgesCount;

        Console.WriteLine("Sortowanie topologiczne - zrodla 1");
        for ( int i=0 ; i<3 ; ++i )
            {
            order = Lab03.TopologicalSort_V0(g2[i]);
            Console.WriteLine("  test {0} : {1}",i,TopologicalSortTest(g2[i],order));
            }

        if ( ec[0]!=g2[0].EdgesCount || ec[1]!=g2[1].EdgesCount ||ec[2]!=g2[2].EdgesCount )
            Console.WriteLine("  Blad - zmieniono graf");

        IGraph[] g3 = new IGraph[3];
        g3[0] = g2[0].Clone();
        g3[0].AddEdge(1,0);
        g3[1] = rgg.DirectedGraph(typeof(AdjacencyMatrixGraph),100,0.9,1,1);
        g3[2] = rgg.DirectedGraph(typeof(AdjacencyListsGraph),1000,0.2,1,1);
        
        ec[0] = g3[0].EdgesCount;
        ec[1] = g3[1].EdgesCount;
        ec[2] = g3[2].EdgesCount;

        Console.WriteLine("Sortowanie topologiczne - zrodla 2");
        for ( int i=0 ; i<3 ; ++i )
            {
            order = Lab03.TopologicalSort_V0(g3[i]);
            Console.WriteLine("  test {0} : {1}",i,order==null);
            }

        if ( ec[0]!=g3[0].EdgesCount || ec[1]!=g3[1].EdgesCount || ec[2]!=g3[2].EdgesCount )
            Console.WriteLine("  Blad - zmieniono graf");

        }

    static bool TopologicalSortTest(IGraph g, int[] ord)
        {

        if ( ord==null ) return false;
        for ( int v=0 ; v<g.VerticesCount ; ++v )
            foreach ( var e in g.OutEdges(v) )
                if ( ord[e.From]>=ord[e.To] )
                    return false;
        return true;
        }

}