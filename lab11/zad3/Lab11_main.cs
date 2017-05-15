using System;
using ASD.Graphs;

public class Lab11
{

    public static void Main()
    {

        Graph g1 = new AdjacencyMatrixGraph(true, 8);
        Graph c1 = new AdjacencyMatrixGraph(true, 8);

        g1.AddEdge(0, 2, 20);
        g1.AddEdge(0, 3, 30);
        g1.AddEdge(1, 2, 10);
        g1.AddEdge(1, 2, 20);
        g1.AddEdge(1, 3, 40);
        g1.AddEdge(2, 4, 20);
        g1.AddEdge(2, 5, 10);
        g1.AddEdge(3, 6, 50);
        g1.AddEdge(3, 7, 30);
        g1.AddEdge(4, 5, 10);
        g1.AddEdge(6, 5, 10);
        g1.AddEdge(6, 7, 10);

        c1.AddEdge(0, 2);
        c1.AddEdge(0, 3);
        c1.AddEdge(1, 2);
        c1.AddEdge(1, 3);
        c1.AddEdge(2, 4);
        c1.AddEdge(2, 5);
        c1.AddEdge(3, 6);
        c1.AddEdge(3, 7);
        c1.AddEdge(4, 5);
        c1.AddEdge(6, 5);
        c1.AddEdge(6, 7);

        int[] p1 = new int[8] { 50, 50, 0, 0, -10, -20, -30, -40 };

        int fv, cost, res;
        Graph f;
        Edge[] ext;

        Console.WriteLine();

        res = g1.BottleNeck(c1, p1, out fv, out cost, out f, out ext);

        Console.Write("Test 1 - nie ma potrzeby rozbudowy sieci: ");
        if (res == 0 && fv == 100 && cost == 0 && ext.Length == 0)
            Console.WriteLine("OK");
        else
            Console.WriteLine("FAILED");
        Console.WriteLine();

        g1.DelEdge(0, 3);
        g1.DelEdge(2, 4);
        g1.AddEdge(0, 3, 20);
        g1.AddEdge(2, 4, 10);

        res = g1.BottleNeck(c1, p1, out fv, out cost, out f, out ext);

        Console.Write("Test 2 - trzeba rozbudowac siec i mozna to zrobic: ");
        if (res == 1 && fv == 100 && cost == 20 && ext.Length > 0)
        {
            Console.WriteLine("OK");
            Console.WriteLine("  rozbudowano krawedzie:");
            foreach (Edge ee in ext)
                Console.WriteLine("    {0}", ee);
        }
        else
            Console.WriteLine("FAILED");
        Console.WriteLine();

        p1[0] = 40;

        res = g1.BottleNeck(c1, p1, out fv, out cost, out f, out ext);

        Console.Write("Test 3 - nie da sie rozbudowac sieci: ");
        if (res == 2 && fv == 90 && cost == 10 && ext.Length > 0)
        {
            Console.WriteLine("OK");
            Console.WriteLine("  rozbudowano krawedzie:");
            foreach (Edge ee in ext)
                Console.WriteLine("    {0}", ee);
        }
        else
            Console.WriteLine("FAILED");
        Console.WriteLine();

        p1[0] = 50;
        g1.DelEdge(3, 6);

        res = g1.BottleNeck(c1, p1, out fv, out cost, out f, out ext);

        Console.Write("Test 4 - nie da sie rozbudowac sieci: ");
        if (res == 2 && fv == 70 && cost == 20 && ext.Length > 0)
        {
            Console.WriteLine("OK");
            Console.WriteLine("  rozbudowano krawedzie:");
            foreach (Edge ee in ext)
                Console.WriteLine("    {0}", ee);
        }
        else
            Console.WriteLine("FAILED");
        Console.WriteLine();
    }

}

