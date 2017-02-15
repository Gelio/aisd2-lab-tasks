using System;
using System.Collections.Generic;

namespace SplittingSet
{

    struct Colors
    {
        public int n;

        private int[] colors;
        static int operationCounter;

        public static int OperationCounter
        {
            get { return Colors.operationCounter; }
        }

        public static void ResetCounter()
        {
            operationCounter = 0;
        }

        public int this[int i]
        {
            get
            {
                //operationCounter++;
                return colors[i];
            }
            set
            {
                colors[i] = value;
                operationCounter++;
            }
        }

        public Colors(int n)
        {
            this.n = n;
            colors = new int[n];
        }

        public Colors Clone()
        {
            Colors clone;
            clone.n = this.n;
            clone.colors = (int[])this.colors.Clone();
            return clone;
        }
    }

    class Program
    {
        static int testnum = 1;
        static Random rnd = new Random(1232412);

        static void Main(string[] args)
        {
            int n;
            List<HashSet<int>> sets = new List<HashSet<int>>();

            /* Test 1 */
            n = 5;
            sets.Clear();
            sets.Add(new HashSet<int>() { 1, 2 });
            sets.Add(new HashSet<int>() { 3, 4 });
            sets.Add(new HashSet<int>() { 2, 4 });
            sets.Add(new HashSet<int>() { 2, 3 });
            Test(n, sets, false);

            /* Test 2 */
            n = 5;
            sets.Clear();
            Test(n, sets, true);

            /* Test 3 */
            n = 5;
            sets.Clear();
            sets.Add(new HashSet<int>() { 1, 2 });
            sets.Add(new HashSet<int>() { 3, 4 });
            sets.Add(new HashSet<int>() { 2, 4 });
            Test(n, sets, true);

           /* Test 4 */
            n = 20;
            sets.Clear();

            while (sets.Count < 10000)
            {
                HashSet<int> set = new HashSet<int>();
                for (int j = 0; j < n; ++j)
                    if (rnd.Next(2) != 0) set.Add(j);
                if (set.Count >= 3 && set.Count <= 7) sets.Add(set);
            }
            Test(n, sets, false);

            /* Test 5 */
            n = 12;
            sets.Clear();
            rnd = new Random(1000);
            while (sets.Count < 150)
            {
                HashSet<int> set = new HashSet<int>();
                for (int j = 0; j < n; ++j)
                    if (rnd.Next(2) != 0) set.Add(j);
                if (set.Count >= 3 && set.Count <= 5) sets.Add(set);
            }
            Test(n, sets, false);

            /* Test 6 */
            n = 500;
            sets.Clear();
            for (int i = 0; i < n - 3; ++i)
                sets.Add(new HashSet<int>() { i, i + 1, i + 2 });
            Test(n, sets, true);

            
        }

        private static void Test(int n, List<HashSet<int>> sets, bool expected)
        {
            bool res;
            Colors colors = new Colors(n);
            Solver sol = new Solver(n);
            Colors.ResetCounter();
            res = sol.Split(sets, ref colors);

            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Test {0}\nJest {1}, powinno być {2}, wynik testu: {3}\nLiczba operacji: {4}", testnum++, res, expected, res==expected, Colors.OperationCounter);
            
            if (n <= 10)
            {
                for (int i = 0; i < n; ++i)
                    Console.WriteLine("{0} {1}", i, colors[i]);
            }
           
        }
    }
}
