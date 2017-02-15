
using System;
using System.Collections.Generic;
using ASD.Text;

namespace ASD2
{

class TestP
    {
    public string text;
    public int len;
    public TestP(string t, int l)
        {
        text=t;
        len=l;
        }
    }

class TestE
    {
    public string text1;
    public string text2;
    public int? ind;
    public TestE(string t1, string t2, int? i)
        {
        text1=t1;
        text2=t2;
        ind=i;;
        }
    }

public static partial class Lab14
    {

    public static void Main()
        {
        TestP[] tests1 = {
            new TestP("aaaaa",1),
            new TestP("ababab",2),
            new TestP("abababa",3),
            new TestP("abcabcab",5),
            new TestP("ababbababbabababbabaababbababbaba",8),
            };
        TestP[] tests1w = new TestP[2];
        System.Text.StringBuilder sb = new System.Text.StringBuilder(100001);
        for ( int i=0 ; i<100000 ; ++ i ) sb.Append("a");
        tests1w[0] = new TestP(sb.ToString(),1);
        tests1w[1] = new TestP(sb.ToString()+"b",100001);
        int count;
        int len;
        int[] ind;
        int? cei;
        Console.WriteLine();

        count=0;
        Console.WriteLine("Testy szablonu - podstawowe");
        for ( int i=0 ; i<tests1.Length ; ++i )
            {
            Console.Write("Test {0,2}:  ",i);
            len=MinPattern(tests1[i].text, out ind);
            if ( Verify(tests1[i].text,tests1[i].len,len,ind) )
                    ++count;
            }
        Console.WriteLine();

        TestE[] tests2 = {
            new TestE("aaaaa","aaaaa",0),
            new TestE("abab","baba",1),
            new TestE("abc","abd",null),
            new TestE("abcdef","efabcd",4),
            new TestE("abcdef","bcdefa",1),
            new TestE("efabcd","abcdef",2),
            new TestE("bcdefa","abcdef",5),
            };

        count=0;
        Console.WriteLine("Testy rownowaznosci cyklicznej (KMR)");
        for ( int i=0 ; i<tests2.Length ; ++i )
            {
            Console.Write("Test {0,2}:  ",i+tests1.Length);
            cei=CyclicEquivalenceKMP(tests2[i].text1, tests2[i].text2);
            if ( cei==tests2[i].ind )
                {
                Console.WriteLine("OK");
                ++count;
                }
            else
                if ( cei==null || tests2[i].ind==null )
                    Console.WriteLine("blad");
                else
                    Console.WriteLine("dobrze ropoznano rownowaznosc, ale zle przesuniecie");

            }
        Console.WriteLine();

        count=0;
        Console.WriteLine("Testy rownowaznosci cyklicznej (bezposrednio)");
        for ( int i=0 ; i<tests2.Length ; ++i )
            {
            Console.Write("Test {0,2}:  ",i+tests1.Length+tests2.Length );
            cei=CyclicEquivalenceDirect(tests2[i].text1, tests2[i].text2);
            if ( cei==tests2[i].ind )
                {
                Console.WriteLine("OK");
                ++count;
                }
            else
                if ( cei==null || tests2[i].ind==null )
                    Console.WriteLine("blad");
                else
                    Console.WriteLine("dobrze ropoznano rownowaznosc, ale zle przesuniecie");

            }
        Console.WriteLine();

        count=0;
        Console.WriteLine("Testy szablonu - wydajnosc");
        for ( int i=0 ; i<tests1w.Length ; ++i )
            {
            Console.Write("Test {0,2}:  ",i+tests1.Length+2*tests2.Length);
            len=MinPattern(tests1w[i].text, out ind);
            if ( Verify(tests1w[i].text,tests1w[i].len,len,ind) )
                    ++count;
            }
        Console.WriteLine();

        }

    private static bool Verify(string text, int expected_len, int len, int[] ind)
        {
        if ( len!=expected_len )
            {
            Console.WriteLine("nieprawidlowa dlugosc szablonu");
            return false;
            }
        foreach ( int i in ind )
            for ( int j=0 ; j<len ; ++j )
                if ( text[i+j]!=text[j] )
                    {
                    Console.WriteLine("szablonu umieszczony od pozycji {0} nie pasuje",i);
                    return false;
                    }
        Console.WriteLine("OK");
        return true;
        }

    }

}
