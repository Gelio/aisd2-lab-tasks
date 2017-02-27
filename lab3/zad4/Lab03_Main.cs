using System;
using ASD.Graphs;

namespace ASD.Lab03
{

class Lab03Exception : ApplicationException
    {
    public Lab03Exception(bool rec=true) { }
    public Lab03Exception(string msg) : base(msg) { }
    public Lab03Exception(string msg, Exception ex) : base(msg,ex) { }
    }

class Lab03
    {

    const int ReverseTestSize   = 5;
    const int BipartiteTestSize = 5;
    const int KruskalTestSize   = 5;
    const int AcyclicTestSize   = 5;

    static bool maskExceptions  = false;

    static void Main(string[] args)
        {
        Console.WriteLine("\nPart 1 - Reverse");
        TestReverse();
        Console.WriteLine("\nPart 2 - Bipartite");
        TestBipartite();
        Console.WriteLine("\nPart 3 - Kruskal");
        TestKruskal();
        Console.WriteLine("\nPart 4 - Acyclic");
        TestAcyclic();
        Console.WriteLine();
        }

    private static void TestReverse()
        {
        var rgg = new RandomGraphGenerator(12345);
        Graph[] g = new Graph[ReverseTestSize];
        bool[] ex = { false, false, true, false, false };
        Graph r, gg;
        ulong cr, cgg;
        g[0] = rgg.DirectedGraph(typeof(AdjacencyMatrixGraph),10,0.7,-99,99);
        g[1] = rgg.DirectedGraph(typeof(AdjacencyListsGraph<HashTableAdjacencyList>),100,0.1,-999,999);
        g[2] = rgg.UndirectedGraph(typeof(AdjacencyMatrixGraph),10,0.5);
        g[3] = rgg.DirectedCycle(typeof(AdjacencyListsGraph<SimplyAdjacencyList>),50000,-99,99);
        g[4] = new AdjacencyListsGraph<AVLAdjacencyList>(true,50000);

        for ( int i=0 ; i<ReverseTestSize ; ++i )
            {
            Console.Write($"  Test {i} - ");
            gg=g[i].Clone();
            try
                {
                cr=Graph.Counter;
                r=g[i].Lab03Reverse();
                cr=Graph.Counter-cr;
                if ( ex[i] )
                    {
                    Console.WriteLine("Failed : exception Lab03Exception expected");
                    continue;
                    }
                if ( r==null )
                    {
                    Console.WriteLine("Failed : null returned");
                    continue;
                    }
                if ( !r.Directed )
                    {
                    Console.WriteLine("Failed : returned graph is undirected");
                    continue;
                    }
                if ( r.GetType()!=g[i].GetType() )
                    {
                    Console.WriteLine("Failed : invalid graph representation");
                    continue;
                    }
                if ( !g[i].IsEqual(gg) )
                    {
                    Console.WriteLine("Failed : graph was destroyed");
                    continue;
                    }
                cgg=Graph.Counter;
                gg=g[i].Reverse();
                cgg=Graph.Counter-cgg;
                if ( !r.IsEqual(gg) )
                    {
                    Console.WriteLine("Failed : bad result");
                    continue;
                    }
                if ( cr>1.5*cgg )
                    {
                    Console.WriteLine($"Failed : poor efficiency {cr} (should be {cgg})");
                    continue;
                    }
                Console.WriteLine("Passed");
                }
            catch ( Lab03Exception e )
                {
                if ( ex[i] )
                    Console.WriteLine("Passed");
                else
                    Console.WriteLine($"Failed : {e.GetType()} : {e.Message}");
                }
            catch ( System.Exception e) when ( maskExceptions )
                {
                Console.WriteLine($"Failed : {e.GetType()} : {e.Message}");
                }
            }
        }

    private static void TestBipartite()
        {
        var rgg = new RandomGraphGenerator(12345);
        Graph[] g = new Graph[BipartiteTestSize];
        bool?[] res = { true, false, null, false, true };
        Graph gg;
        bool r;
        int[] part;
        ulong cr;
        ulong[] cgg = { 77, 457, 1, 2500007, 1 };
        g[0] = rgg.BipariteGraph(typeof(AdjacencyMatrixGraph),4,3,0.4,-99,99);
        g[1] = rgg.UndirectedGraph(typeof(AdjacencyListsGraph<HashTableAdjacencyList>),100,0.1,-999,999);
        g[2] = rgg.DirectedGraph(typeof(AdjacencyMatrixGraph),10,0.5);
        g[3] = rgg.UndirectedCycle(typeof(AdjacencyListsGraph<SimplyAdjacencyList>),50001,-99,99);
        g[4] = new AdjacencyListsGraph<AVLAdjacencyList>(false,50000);

        for ( int i=0 ; i<BipartiteTestSize ; ++i )
            {
            Console.Write($"  Test {i} - ");
            gg=g[i].Clone();
            try
                {
                cr=Graph.Counter;
                r=g[i].Lab03IsBipartite(out part);
                cr=Graph.Counter-cr;
                if ( res[i]==null )
                    {
                    Console.WriteLine("Failed : exception Lab03Exception expected");
                    continue;
                    }
                if ( !g[i].IsEqual(gg) )
                    {
                    Console.WriteLine("Failed : graph was destroyed");
                    continue;
                    }
                if ( r!=res[i] )
                    {
                    Console.WriteLine("Failed : bad result");
                    continue;
                    }
                if ( r && !IsProperPartition(g[i],part) )
                    {
                    Console.WriteLine("Failed : invalid partition");
                    continue;
                    }
                if ( !r && part!=null )
                    {
                    Console.WriteLine("Failed : part==null expected");
                    continue;
                    }
                if ( cr>1.5*cgg[i] )
                    {
                    Console.WriteLine($"Failed : poor efficiency {cr} (should be {cgg[i]})");
                    continue;
                    }
                Console.WriteLine("Passed");
                }
            catch ( Lab03Exception e )
                {
                if ( res[i]==null )
                    Console.WriteLine("Passed");
                else
                    Console.WriteLine($"Failed : {e.GetType()} : {e.Message}");
                }
            catch ( System.Exception e) when ( maskExceptions )
                {
                Console.WriteLine($"Failed : {e.GetType()} : {e.Message}");
                }
            }
        }

    private static void TestKruskal()
        {
        var rgg = new RandomGraphGenerator(12345);
        Graph[] g = new Graph[KruskalTestSize];
        bool[] ex = { false, false, true, false, false };
        Graph r, gg;
        ulong cr, cgg;
        int mstwr, mstwgg;
        g[0] = rgg.UndirectedGraph(typeof(AdjacencyMatrixGraph),5,0.7,-99,99);
        g[1] = rgg.UndirectedGraph(typeof(AdjacencyListsGraph<HashTableAdjacencyList>),100,0.1,-999,999);
        g[2] = rgg.DirectedGraph(typeof(AdjacencyMatrixGraph),10,0.5);
        g[3] = rgg.UndirectedCycle(typeof(AdjacencyListsGraph<SimplyAdjacencyList>),50000,-99,99);
        g[4] = new AdjacencyListsGraph<AVLAdjacencyList>(false,50000);

        for ( int i=0 ; i<KruskalTestSize ; ++i )
            {
            Console.Write($"  Test {i} - ");
            gg=g[i].Clone();
            try
                {
                cr=Graph.Counter;
                r=g[i].Lab03Kruskal(out mstwr);
                cr=Graph.Counter-cr;
                if ( ex[i] )
                    {
                    Console.WriteLine("Failed : exception Lab03Exception expected");
                    continue;
                    }
                if ( r==null )
                    {
                    Console.WriteLine("Failed : null returned");
                    continue;
                    }
                if ( r.Directed )
                    {
                    Console.WriteLine("Failed : returned graph is directed");
                    continue;
                    }
                if ( r.GetType()!=g[i].GetType() )
                    {
                    Console.WriteLine("Failed : invalid graph representation");
                    continue;
                    }
                if ( !g[i].IsEqual(gg) )
                    {
                    Console.WriteLine("Failed : graph was destroyed");
                    continue;
                    }
                cgg=Graph.Counter;
                mstwgg=g[i].Kruskal(out gg);
                cgg=Graph.Counter-cgg;
                if ( mstwr!=mstwgg || !IsSubtree(g[i],r) )
                    {
                    Console.WriteLine("Failed : bad result");
                    continue;
                    }
                if ( cr>1.5*cgg )
                    {
                    Console.WriteLine($"Failed : poor efficiency {cr} (should be {cgg})");
                    continue;
                    }
                Console.WriteLine("Passed");
                }
            catch ( Lab03Exception e )
                {
                if ( ex[i] )
                    Console.WriteLine("Passed");
                else
                    Console.WriteLine($"Failed : {e.GetType()} : {e.Message}");
                }
            catch ( System.Exception e) when ( maskExceptions )
                {
                Console.WriteLine($"Failed : {e.GetType()} : {e.Message}");
                }
            }
        }

    private static void TestAcyclic()
        {
        var rgg = new RandomGraphGenerator(12345);
        Graph[] g = new Graph[AcyclicTestSize];
        bool?[] res = { true, false, null, false, true };
        Graph gg;
        bool r;
        ulong cr;
        ulong[] cgg = { 73, 3724, 1, 300000, 1 };
        g[0] = rgg.TreeGraph(typeof(AdjacencyMatrixGraph),7,1.0,-99,99);
        g[1] = rgg.UndirectedGraph(typeof(AdjacencyListsGraph<HashTableAdjacencyList>),100,0.1,-999,999);
        g[2] = rgg.DirectedGraph(typeof(AdjacencyMatrixGraph),10,0.5);
        g[3] = rgg.UndirectedCycle(typeof(AdjacencyListsGraph<SimplyAdjacencyList>),50000,-99,99);
        g[4] = new AdjacencyListsGraph<AVLAdjacencyList>(false,50000);

        for ( int i=0 ; i<AcyclicTestSize ; ++i )
            {
            Console.Write($"  Test {i} - ");
            gg=g[i].Clone();
            try
                {
                cr=Graph.Counter;
                r=g[i].Lab03IsUndirectedAcyclic();
                cr=Graph.Counter-cr;
                if ( res[i]==null )
                    {
                    Console.WriteLine("Failed : exception Lab03Exception expected");
                    continue;
                    }
                if ( !g[i].IsEqual(gg) )
                    {
                    Console.WriteLine("Failed : graph was destroyed");
                    continue;
                    }
                if ( r!=res[i] )
                    {
                    Console.WriteLine("Failed : bad result");
                    continue;
                    }
                if ( cr>1.5*cgg[i] )
                    {
                    Console.WriteLine($"Failed : poor efficiency {cr} (should be {cgg[i]})");
                    continue;
                    }
                Console.WriteLine("Passed");
                }
            catch ( Lab03Exception e )
                {
                if ( res[i]==null )
                    Console.WriteLine("Passed");
                else
                    Console.WriteLine($"Failed : {e.GetType()} : {e.Message}");
                }
            catch ( System.Exception e) when ( maskExceptions )
                {
                Console.WriteLine($"Failed : {e.GetType()} : {e.Message}");
                }
            }
        }

    private static bool IsSubtree(Graph g, Graph t)
        {
        if ( g.VerticesCount!=t.VerticesCount ) return false;
        for ( int v=0 ; v<t.VerticesCount ; ++v )
            foreach ( Edge e in t.OutEdges(v) )
                if ( e.Weight!=g.GetEdgeWeight(e.From,e.To) )
                    return false;
        int gcc, tcc;
        g.GeneralSearchAll<EdgesStack>(null,null,out gcc);
        t.GeneralSearchAll<EdgesStack>(null,null,out tcc);
        return gcc==tcc;
        }

    private static bool IsProperPartition(Graph g, int[] part)
        {
        if ( part==null || part.Length!=g.VerticesCount ) return false;
        for ( int v=0 ; v<g.VerticesCount ; ++v )
            if ( part[v]!=1 && part[v]!=2 )
                return false;
        for ( int v=0 ; v<g.VerticesCount ; ++v )
            foreach ( Edge e in g.OutEdges(v) )
                if ( part[e.From]==part[e.To] )
                    return false;
        return true;
        }

    }  // class Lab03

}
