using System;

namespace ASD2
{
    static partial class Sequences
    {
        static public void Main(string[] args)
        {
            int[] x,y;
            x = new int[] { 1, 2, 3, 2, 4, 1, 2 };
            y = new int[] { 2, 4, 3, 1, 2, 1 };
            LCS(x,y);
            x = new int[] { 1, 2, 3 };
            y = new int[] { 4,5,6 };
            LCS(x,y);
            x = new int[] { 1,2,5,-1,6,7,2,5,0,9,14,5,6 };
            y = new int[] { 1,6,2,8,4,7,6,10,2,-1,5,2,5 };
            LCS(x,y);

            Console.WriteLine("****\n");
            x = new int[] { 1, -2, 3, 2, -4, 1, 2 };
            MSS(x);
            x = new int[] { 0,4,-5,6,-2,7,8,-3,4 };
            MSS(x);
            x = new int[] { -1,-2,-3,-4 };
            MSS(x);
            x = new int[] { 3,-1,5,0,0,-5,4,-1,7,-12,9,3 };
            MSS(x);

            Console.WriteLine("****\n");
            x = new int[] { 1, -2, 3, 2, -4, 1, 2 };
            LAS(x);
            x = new int[] { 0,4,-5,6,-2,7,8,-3,4 };
            LAS(x);
            x = new int[] { 3,2,2,1,0,-1 };
            LAS(x);

            Console.WriteLine("****\n");
        }

        private static void LCS(int[] x,int [] y)
        {
            Console.WriteLine("****");
            int[] lcs;
            int n = FindLongestCommonSubsequence(x, y, out lcs);
            Console.Write("x: ");
            for (int i = 0; i < x.Length; ++i)
                Console.Write("{0} ", x[i]);
            Console.Write("\ny: ");
            for (int i = 0; i < y.Length; ++i)
                Console.Write("{0} ", y[i]);
            Console.Write("\nLCS: ");
            if ( lcs!=null )
                for (int i = 0; i < lcs.Length; ++i)
                    Console.Write("{0} ", lcs[i]);
            else
                Console.Write("null ");
            Console.WriteLine("len: {0}",n);
        }

        private static void MSS(int[] x)
        {
            Console.WriteLine("****");            
            int start, end;
            int sum = FindMaxSumSegment(x, out start, out end);
            Console.Write("x: ");
            for (int i = 0; i < x.Length; ++i)
                Console.Write("{0} ", x[i]);            
            Console.Write("\nsegment: ");
            for (int i = start; i < end; ++i)
                Console.Write("{0} ", x[i]);
            Console.WriteLine("sum: {0}", sum);
        }

        private static void LAS(int[] x)
        {
            Console.WriteLine("****");
            int[] las;
            int n = FindLongestAscendingSubsequence(x, out las);
            Console.Write("x: ");
            for (int i = 0; i < x.Length; ++i)
                Console.Write("{0} ", x[i]);            
            Console.Write("\nLAS: ");
            if ( las!=null )
                for (int i = 0 ; i < las.Length; ++i)
                    Console.Write("{0} ", las[i]);
            else
                Console.Write("null ");
            Console.WriteLine("len: {0}", n);
        }
    }
}
