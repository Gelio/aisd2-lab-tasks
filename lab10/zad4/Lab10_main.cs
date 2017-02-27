
using System;
using System.Threading;
using ASD.Graphs;

class Lab10
    {

    private static Graph[] cliq_test;
    private static Graph[,] izo_test;
    private static int[] cliq_res;
    private static bool[] izo_res;

    private static Graph[] cliq_test2;
    private static Graph[,] izo_test2;
    private static int[] cliq_res2;
    private static bool[] izo_res2;

    private static double speedFactor;
    
    public static int Fib(int n)
        {
        if ( n<2 )
            return n;
        return Fib(n-1)+Fib(n-2);
        }

    public static void Main()
        {
        int[] clique, map;
        bool izo, res;
        int n;
        Graph g, h;

        DateTime t1 = DateTime.Now;
        Fib(39);
        DateTime t2 = DateTime.Now;
        speedFactor=(t2-t1).TotalMilliseconds/1000;
        Console.WriteLine($"SpeedFactor = {speedFactor}");

        PrepareTests();
        PrepareTests2();

        Console.WriteLine();
        Console.WriteLine("Clique Tests");
        for ( int i=0 ; i<cliq_test.Length ; ++i )
            {
            Console.Write($"Test {i+1}:  ");
            g=cliq_test[i].Clone();
            n = cliq_test[i].MaxClique(out clique);
            Console.WriteLine("{0}", n==cliq_res[i] && CliqueTest(cliq_test[i],n,clique) && cliq_test[i].IsEqualParallel(g) ? "Passed" : "Fail" );
            }

        Console.WriteLine();
        Console.WriteLine("Clique Tests - efficiency");
        for ( int i=0 ; i<cliq_test2.Length ; ++i )
            {
            n=0;
            clique=null;
            Thread thr = new Thread(()=>{n=cliq_test2[i].MaxClique(out clique);});
            Console.Write($"Test {i+1}:  ");
            g=cliq_test2[i].Clone();
            thr.Start();
            if ( !thr.Join((int)(speedFactor*4000)) )  // powinno wystarczyc 2000 
                {
                thr.Abort();
                Console.WriteLine("Timeout");
                }
            else
                Console.WriteLine("{0}", n==cliq_res2[i] && CliqueTest(cliq_test2[i],n,clique) && cliq_test2[i].IsEqualParallel(g) ? "Passed" : "Fail" );
            }

        Console.WriteLine();
        Console.WriteLine("Isomorpism Tests");
        for ( int i=0 ; i<izo_test.GetLength(0) ; ++i )
            {
            Console.Write($"Test {i+1}:  ");
            g = izo_test[i,0].Clone();
            h = izo_test[i,1].Clone();
            izo = izo_test[i,0].IsomorpchismTest(izo_test[i,1], out map);
            res = ( izo ? izo_res[i] && map!=null && izo_test[i,0].IsIsomorphicParallel(izo_test[i,1],map)==izo_res[i] : !izo_res[i] && map==null )
                    && izo_test[i,0].IsEqualParallel(g) && izo_test[i,1].IsEqualParallel(h) ;
            Console.WriteLine("{0}", res ?  "Passed" : "Fail" );
            }

        Console.WriteLine();
        Console.WriteLine("Isomorpism Tests - efficiency");
        for ( int i=0 ; i<izo_test2.GetLength(0) ; ++i )
            {
            map=null;
            izo=!izo_res2[i];
            Thread thr = new Thread(()=>{izo=izo_test2[i,0].IsomorpchismTest(izo_test2[i,1], out map);});
            Console.Write($"Test {i+1}:  ");
            g = izo_test2[i,0].Clone();
            h = izo_test2[i,1].Clone();
            thr.Start();
            if ( !thr.Join((int)(speedFactor*4000)) )  // powinno wystarczyc 2000 
                {
                thr.Abort();
                Console.WriteLine("Timeout");
                }
            else
                {
                res = ( izo ? izo_res2[i] && map!=null && izo_test2[i,0].IsIsomorphicParallel(izo_test2[i,1],map)==izo_res2[i] : !izo_res2[i] && map==null )
                        && izo_test2[i,0].IsEqualParallel(g) && izo_test2[i,1].IsEqualParallel(h) ;
                Console.WriteLine("{0}", res ?  "Passed" : "Fail" );
                }
            }

        Console.WriteLine();
        }

    public static void PrepareTests()
        {
        var rgg = new RandomGraphGenerator();

        cliq_test = new Graph[5];
        izo_test = new Graph[4,2];

        cliq_res = new int[] { 4, 20, 19, 19, 9 };
        izo_res = new bool[] { true, false, true, false };

        if ( cliq_test.Length!=cliq_res.Length || izo_test.GetLongLength(0)!=izo_res.Length )
            throw new ApplicationException("Zle zddefiniowane testy");

        cliq_test[0] = new AdjacencyMatrixGraph(false,8);
        cliq_test[0].AddEdge(0,4);
        cliq_test[0].AddEdge(0,7);
        cliq_test[0].AddEdge(1,2);
        cliq_test[0].AddEdge(1,3);
        cliq_test[0].AddEdge(1,5);
        cliq_test[0].AddEdge(1,6);
        cliq_test[0].AddEdge(2,5);
        cliq_test[0].AddEdge(2,6);
        cliq_test[0].AddEdge(3,4);
        cliq_test[0].AddEdge(3,7);
        cliq_test[0].AddEdge(4,7);
        cliq_test[0].AddEdge(5,6);

        cliq_test[1] = new AdjacencyMatrixGraph(false,20);
        for ( int i=0 ; i<cliq_test[1].VerticesCount ; ++i )
            for ( int j=i+1 ; j<cliq_test[1].VerticesCount ; ++j )
                cliq_test[1].AddEdge(i,j);

        cliq_test[2]=cliq_test[1].Clone();
        cliq_test[2].DelEdge(0,1);

        cliq_test[3]=cliq_test[2].Clone();
        cliq_test[3].DelEdge(0,2);

        rgg.SetSeed(123);
        cliq_test[4]=rgg.DirectedGraph(typeof(AdjacencyMatrixGraph),100,0.7);

        izo_test[0,0] = cliq_test[0].Clone();
        izo_test[0,1] = rgg.Permute(izo_test[0,0]);

        izo_test[1,0] = izo_test[0,0].Clone();
        izo_test[1,1] = izo_test[0,1].Clone();
        izo_test[1,0].ModifyEdgeWeight(2,5,3);

        rgg.SetSeed(1234);
        izo_test[2,0] = rgg.DirectedGraph(typeof(AdjacencyMatrixGraph),50,0.95,1,999);
        izo_test[2,1] = new AdjacencyListsGraph<HashTableAdjacencyList>(izo_test[2,0]);
        izo_test[2,1] = rgg.Permute(izo_test[2,1]);

        izo_test[3,0] = rgg.UndirectedGraph(typeof(AdjacencyListsGraph<HashTableAdjacencyList>),5000,0.01,1,3);
        izo_test[3,1] = new AdjacencyListsGraph<HashTableAdjacencyList>(true,5000);
        for ( int v=0 ; v<5000 ; ++v )
            foreach ( Edge e in izo_test[3,0].OutEdges(v) )
                izo_test[3,1].AddEdge(e);
        }

    public static void PrepareTests2()
        {
        int n;
        var rgg = new RandomGraphGenerator();

        cliq_test2 = new Graph[3];
        izo_test2 = new Graph[2,2];

        cliq_res2 = new int[] { 3, 3, 5 };
        izo_res2 = new bool[] { false, true };

        if ( cliq_test2.Length!=cliq_res2.Length || izo_test2.GetLongLength(0)!=izo_res2.Length )
            throw new ApplicationException("Zle zddefiniowane testy");

        rgg.SetSeed(123);
        cliq_test2[0] = rgg.UndirectedGraph(typeof(AdjacencyListsGraph<HashTableAdjacencyList>),4000,0.001); 
        rgg.SetSeed(125);
        cliq_test2[1] = rgg.DirectedGraph(typeof(AdjacencyListsGraph<HashTableAdjacencyList>),3000,0.05); 

        n=1500;
        cliq_test2[2] = new AdjacencyListsGraph<HashTableAdjacencyList>(false,n);
        for ( int i=0 ; i<n ; ++i )
            for ( int j=1 ; j<=4 ; ++j )
                cliq_test2[2].AddEdge(i,(i+j)%n);

        n = 50;
        izo_test2[0,0]=new AdjacencyMatrixGraph(true,n);
        for ( int i=0 ; i<n ; ++i )
            for ( int j=0 ; j<n ; ++j )
                if ( i!=j )
                    izo_test2[0,0].AddEdge(i,j);
        izo_test2[0,1]=izo_test2[0,0].Clone();
        for ( int i=0 ; i<n ; ++i )
            izo_test2[0,0].DelEdge(i,(i+1)%n);
        for ( int i=0 ; i<n ; ++i )
            izo_test2[0,1].DelEdge(i,(i+2)%n);

        rgg.SetSeed(1234);
        izo_test2[1,0]=rgg.DirectedGraph(typeof(AdjacencyMatrixGraph),2500,0.95,1,999);
        izo_test2[1,1]=new AdjacencyListsGraph<HashTableAdjacencyList>(izo_test2[1,0]);
        izo_test2[1,1]=rgg.Permute(izo_test2[1,1]);
        }

    public static bool CliqueTest(Graph g, int cn, int[] cl)
        {
        if ( cl==null || cn!=cl.Length ) return false;
        for ( int i=0 ; i<cn ; ++i )
            for ( int j=i+1 ; j<cn ; ++j )
                if ( g.GetEdgeWeight(cl[i],cl[j])==null || g.GetEdgeWeight(cl[j],cl[i])==null )
                    return false;
        return true;
        }

    }
