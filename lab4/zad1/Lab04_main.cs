
using System;
using ASD.Graph;

class Lab04
    {

    public static void Main ()
        {
        var ge = new GraphExport();
        var rgg = new RandomGraphGenerator();
        int[] scc;
        int n, w;
        string[] desc;
        int[] sccc = { 8, 17, 1000 };
        IGraph mst;
        int[] mstw = { 47, 3998, 40533 };


        IGraph g1= new AdjacencyMatrixGraph(true,16);
        g1.AddEdge(0,5);
        g1.AddEdge(1,0);
        g1.AddEdge(1,2);
        g1.AddEdge(1,7);
        g1.AddEdge(2,3);
        g1.AddEdge(3,1);
        g1.AddEdge(3,7);
        g1.AddEdge(4,15);
        g1.AddEdge(5,4);
        g1.AddEdge(5,6);
        g1.AddEdge(6,7);
        g1.AddEdge(7,6);
        g1.AddEdge(7,8);
        g1.AddEdge(8,9);
        g1.AddEdge(10,3);
        g1.AddEdge(12,13);
        g1.AddEdge(13,14);
        g1.AddEdge(14,12);
        g1.AddEdge(15,0);

        IGraph g2 = new AdjacencyMatrixGraph(false,15);
        g2.AddEdge(0,1,3);
        g2.AddEdge(0,4,5);
        g2.AddEdge(1,4,6);
        g2.AddEdge(1,5,4);
        g2.AddEdge(2,6,5);
        g2.AddEdge(4,7,8);
        g2.AddEdge(4,8,2);
        g2.AddEdge(4,10,12);
        g2.AddEdge(5,8,1);
        g2.AddEdge(5,9,7);
        g2.AddEdge(6,11,4);
        g2.AddEdge(6,14,5);
        g2.AddEdge(7,8,9);
        g2.AddEdge(8,12,3);
        g2.AddEdge(10,12,2);
        g2.AddEdge(10,13,3);

        IGraph[] scc_test = new IGraph[3];
        scc_test[0] = g1;
        rgg.SetSeed(500);
        scc_test[1] = rgg.DirectedGraph(typeof(AdjacencyListsGraph),1000,0.005);
        scc_test[2] = rgg.DAG(typeof(AdjacencyMatrixGraph),1000,0.3,1,1);

        IGraph[] mst_test = new IGraph[3];
        mst_test[0] = g2;
        mst_test[1] = rgg.UndirectedGraph(typeof(AdjacencyMatrixGraph),100,0.3,1,1000);
        mst_test[2] = rgg.UndirectedGraph(typeof(AdjacencyListsGraph),1000,0.03,1,1000);

        Console.WriteLine();
        for ( int i=0 ; i<scc_test.Length ; ++i )
            {
            n = scc_test[i].Tarjan(out scc);
            Console.WriteLine("Liczba silnie spojnych skladowych: {0,4},    powinno byc {1,4}",n,sccc[i]);
            if ( i>0 ) continue;
            desc=new string[scc_test[i].VerticesCount];
            for ( int v=0 ; v<scc_test[i].VerticesCount ; ++v )
                desc[v]=string.Format("{0}:{1}",v,scc[v]);
            ge.Export(scc_test[i],desc,"scc");
            }

        Console.WriteLine();
        for ( int i=0 ; i<mst_test.Length ; ++i )
            {
            w = mst_test[i].Boruvka(out mst);
            Console.WriteLine("Waga minimalnego drzewa rozpinajacego: {0,5},    powinno byc {1,5}",w,mstw[i]);
            if ( i>0 ) continue;
            ge.Export(mst_test[i],null,"graph");
            ge.Export(mst,null,"mst");
            }

        Console.WriteLine();
        }

    }

