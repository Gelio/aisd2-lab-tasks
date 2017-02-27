using System;
using ASD.Graphs;

public class Lab11
    {

    public static void Main()
        {
        int r;
  
        Graph g1 = new AdjacencyMatrixGraph(true,8);
        Graph c1 = new AdjacencyMatrixGraph(true,8);

        g1.AddEdge(0,2,20);
        g1.AddEdge(0,3,30);
        g1.AddEdge(1,2,10);
        g1.AddEdge(1,3,40);
        g1.AddEdge(2,4,20);
        g1.AddEdge(2,5,10);
        g1.AddEdge(3,6,50);
        g1.AddEdge(3,7,30);
        g1.AddEdge(4,5,10);
        g1.AddEdge(6,5,10);
        g1.AddEdge(6,7,10);

        c1.AddEdge(0,2);
        c1.AddEdge(0,3,3);
        c1.AddEdge(1,2);
        c1.AddEdge(1,3);
        c1.AddEdge(2,4,3);
        c1.AddEdge(2,5,2);
        c1.AddEdge(3,6);
        c1.AddEdge(3,7);
        c1.AddEdge(4,5);
        c1.AddEdge(6,5);
        c1.AddEdge(6,7);

        int[] p1 = new int[8] { 50, 50, 0, 0, -10, -20, -30, -40 }; 

        Console.WriteLine();

        Console.WriteLine("Test 1 - nie ma potrzeby rozbudowy sieci");
        r=Test(g1,c1,p1,0,100,0);
        Console.WriteLine(r==0?"  OK":"  BLAD: {0}",message[r]);
        Console.WriteLine();

        g1.DelEdge(0,3);
        g1.DelEdge(2,4);
        g1.AddEdge(0,3,20);
        g1.AddEdge(2,4,10);

        Console.WriteLine("Test 2 - trzeba rozbudowac siec i mozna to zrobic");
        r=Test(g1,c1,p1,1,100,40);
        Console.WriteLine(r==0?"  OK":"  BLAD: {0}",message[r]);
        Console.WriteLine();

        p1[0]=40;

        Console.WriteLine("Test 3 - nie da sie rozbudowac sieci");
        r=Test(g1,c1,p1,2,90,10);
        Console.WriteLine(r==0?"  OK":"  BLAD: {0}",message[r]);
        Console.WriteLine();

        p1[0]=50;
        g1.DelEdge(3,6);
        c1.DelEdge(3,6);

        Console.WriteLine("Test 4 - nie da sie rozbudowac sieci");
        r=Test(g1,c1,p1,2,70,30);
        Console.WriteLine(r==0?"  OK":"  BLAD: {0}",message[r]);
        Console.WriteLine();

        }

    public static string[] message = {
         /*  0 */ "OK",
         /*  1 */ "Nieprawidlowy wynik" ,
         /*  2 */ "Nieprawidlowa wartosc przeplywu" ,
         /*  3 */ "Nieprawidlowy koszt rozbudowy" ,
         /*  4 */ "Niezdefiniowany graf przeplywu" ,
         /*  5 */ "Nieprawidlowa liczba wierzcholkow grafu przeplywu" ,
         /*  6 */ "Nieprawidlowa struktura grafu przeplywu" ,
         /*  7 */ "Przeplyw nie jest w rownowadze" ,
         /*  8 */ "Niezdefiniowana tablica rozbudowywanych krawedzi" ,
         /*  9 */ "Nieprawidlowa wartosc rozbudowy krawedzi" ,
         /* 10 */ "Nieprawidlowa rozbudowywana krawedz" ,
         /* 11 */ "Niespojny koszt rozbudowy" };

    public static int Test(Graph gg, Graph gc, int[] p, int res, int flowValue, int cost)
        {
        int r, fv, c, cc;
        int? w;
        Graph f;
        Edge[] ee;
        int[] pv = new int[p.Length]; 
        r=gg.BottleNeck(gc,p,out fv, out c, out f, out ee);
        if ( r!=res ) return 1;
        if ( fv!=flowValue ) return 2;
        if ( c!=cost ) return 3;
        if ( f==null ) return 4;
        if ( f.VerticesCount!=gg.VerticesCount ) return 5;
        for ( int v = 0 ; v<f.VerticesCount ; ++v )
            {
            if ( f.OutDegree(v)!=gg.OutDegree(v) ) return 6;
            foreach ( Edge e in f.OutEdges(v) )
                {
                if ( gg.GetEdgeWeight(e.From,e.To)==null ) return 6;
                pv[v]+=e.Weight;
                pv[e.To]-=e.Weight;
                }
            }
        for ( int v = 0 ; v<p.Length ; ++v )
            if ( pv[v]<Math.Min(p[v],0) || pv[v]>Math.Max(p[v],0) ) return 7;
        if ( ee==null ) return 8;
        cc=0;
        foreach ( Edge e in ee )
            {
            Console.WriteLine("    rozbudowano: {0}",e);
            if ( e.Weight<=0 ) return 9;
            w=gg.GetEdgeWeight(e.From,e.To);
            if ( w==null || e.Weight+w != f.GetEdgeWeight(e.From,e.To) ) return 10;
            cc+=e.Weight*(int)gc.GetEdgeWeight(e.From,e.To);
            }
        if ( cc!=c ) return 11;
        return 0;
        }

    }

