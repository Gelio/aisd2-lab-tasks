
using System;
using System.Collections.Generic;

namespace ASD
{

partial class Lab02
    {

    public static void Main()
        {
        int max;
        int count1, count2, count3;
        Elem winner;
        List<Elem[]> tests = new List<Elem[]>{ Test1(), Test2(), Test3(), Test4(), Test5(), GenTest(128,1234), GenTest(128,123456) };
        List<string> tw = new List<string>   {  "   0",  "brak",  "brak",  "   1",  "   1",            "brak",              "   0" };
        List<int>    tg = new List<int>      {       5,       0,       0,    5033,       9,                 0,                  65 };

        Console.WriteLine();
        for ( int i=0 ; i<tests.Count ; ++i )
            {
            Console.WriteLine("Test {0}",i+1);
            Console.WriteLine("  Powinno byc  -  zwyciezca:  {0}, glosow:  {1,4}", tw[i], tg[i]);
            count1=Elem.CompareCount;
            winner=FindWinnerSimply(tests[i],out max);
            count2=Elem.CompareCount;
            Console.WriteLine("  Alg. prosty  -  zwyciezca:  {0}, glosow:  {1,4}, porownan: {2,9}",winner.Description(), max, count2-count1);
            winner=FindWinnerFast(tests[i],out max);
            count3=Elem.CompareCount;
            Console.WriteLine("  Alg. szybki  -  zwyciezca:  {0}, glosow:  {1,4}, porownan: {2,9}",winner.Description(), max, count3-count2);
            Console.WriteLine();
            }
        }
    
    static Elem[] Test1()
        {
        Elem[] t = new Elem[9];
        for ( int i=0 ; i<t.Length ; ++i )
            t[i]=new Elem(i%2);
        return t;
        }

    static Elem[] Test2()
        {
        Elem[] t = new Elem[8];
        for ( int i=0 ; i<t.Length ; ++i )
            t[i]=new Elem(i/4);
        return t;
        }

    static Elem[] Test3()
        {
        Elem[] t = new Elem[9999];
        for ( int i=0 ; i<t.Length ; ++i )
            t[i]=new Elem(i);
        return t;
        }

    static Elem[] Test4()
        {
        Random r = new Random(1111);
        Elem[] t = new Elem[9999];
        for ( int i=0 ; i<t.Length ; ++i )
            t[i]=new Elem(r.Next(2));
        return t;
        }

    static Elem[] Test5()
        {
        Elem[] t = new Elem[16];
        t[ 0]=new Elem(0);
        t[ 1]=new Elem(0);
        t[ 2]=new Elem(1);
        t[ 3]=new Elem(0);
        t[ 4]=new Elem(0);
        t[ 5]=new Elem(1);
        t[ 6]=new Elem(0);
        t[ 7]=new Elem(0);
        t[ 8]=new Elem(1);
        t[ 9]=new Elem(1);
        t[10]=new Elem(1);
        t[11]=new Elem(1);
        t[12]=new Elem(1);
        t[13]=new Elem(0);
        t[14]=new Elem(1);
        t[15]=new Elem(1);
        return t;
        }

    static Elem[] GenTest(int n, int seed)
        {
        Random r = new Random(seed);
        Elem[] t = new Elem[n];
        int v;
        for ( int i=0 ; i<n ; ++i )
            {
            v=r.Next(4)%3;
            t[i]=new Elem(v);
            }
        return t;
        }

    }

class Elem
    {
    private int value;

    public static int CompareCount { get; private set; }

    public Elem(int v)
        {
        value=v;
        }

    public bool Compare(Elem e)
        {
        ++CompareCount;
        return value==e.value;
        }

    public override string ToString()
        {
        return value.ToString();
        }

    }

static class ElemExtender
    {

    public static string Description(this Elem e)
        {
        return e==null ? "brak" : string.Format("{0,4}",e.ToString());
        }

    }

}  // namespace ASD
