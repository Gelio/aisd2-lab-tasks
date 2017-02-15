using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zad12
{

    public enum ChangeType
    {
        None, ChangeChar, RemoveChar, AddChar, TransposeChar, MultiplyChar
    }

    public struct StringDiffInfo
    {
        #region Cost

        private int cost;

        public int Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        #endregion

        #region ChangesCount

        private int changesCount;

        public int ChangesCount
        {
            get { return changesCount; }
            set { changesCount = value; }
        }

        #endregion

        #region Change

        private ChangeType change;

        public ChangeType Change
        {
            get { return change; }
            set { change = value; }
        }

        #endregion

        public StringDiffInfo(int cost, int changesCount, ChangeType change)
        {
            this.cost = cost;
            this.changesCount = changesCount;
            this.change = change;
        }
    }

    partial class Editorial
    {

    static void Main(string[] args)
        {
            int[] costs = new int[6];

            costs[(int)ChangeType.None] = 0;
            costs[(int)ChangeType.AddChar] = 4;
            costs[(int)ChangeType.RemoveChar] = 3;
            costs[(int)ChangeType.ChangeChar] = 5;
            costs[(int)ChangeType.TransposeChar] = 5;
            costs[(int)ChangeType.MultiplyChar] = 3;

            Console.WriteLine("\n*******************1*******************\n");
            string s = "abcdefghij";
            string d = "abc dfghijk";
            Test(s, d, costs, 3, 11, 3, 10);

            Console.WriteLine("\n*******************2*******************\n");
            s = "abcdef";
            d = "";
            Test(s, d, costs, 6, 18, 6, 24);

            Console.WriteLine("\n*******************3*******************\n");
            s = "Ala ma kota";
            d = "Aga zna Robka";
            Test(s, d, costs, 6, 28, 6, 26);

            Console.WriteLine("\n*******************4*******************\n");
            s = "aaaa";
            d = "bbb";
            Test(s, d, costs, 4, 18, 4, 19);

            Console.WriteLine("\n*******************5*******************\n");
            s = "Ala ma kota";
            d = "Allla ma kottaa";
            Test(s, d, costs, 4, 12, 4, 12);

            Console.WriteLine("\n*******************6*******************\n");
            s = "Cezksi bald";
            d = "Czeski blad";
            Test(s, d, costs, 3, 15, 3, 15);

            Console.WriteLine("\n*******************7*******************\n");
            s = "ca";
            d = "abc";
            Test(s, d, costs, 3, 11, 3, 10);

            Console.WriteLine("\n*******************8*******************\n");
            s = "na blya ww zo";
            d = "Anna byla w zoo";
            Test(s, d, costs, 5, 18, 5, 17);

            Console.WriteLine("\n*******************9*******************\n");
            s = "Chodzmy stad wreszcie";
            d = "Musimy dokonczyc zadanie";
            Test(s, d, costs, 20, 85, 20, 82);
        }

        private static void Test(string s, string d, int[] costs, int ch1, int c1, int ch2, int c2)
        {
            Console.WriteLine("Tekst1: {0}, tekst2: {1}", s, d);
            int changes, cost;
            List<string> strings = GetEditorialDistance(String.Copy(s), String.Copy(d), costs, out changes, out cost);
            Console.WriteLine("changes: {0} ({2})\ncost: {1} ({3})\n", changes, cost, ch1, c1);
            int i = 0;
            foreach (string str in strings)
                Console.WriteLine("{0} {1}", i++, str);
            Console.WriteLine("\n**********\n");
            strings = GetEditorialDistance(String.Copy(d), String.Copy(s), costs, out changes, out cost);
            Console.WriteLine("changes: {0} ({2})\ncost: {1} ({3})\n", changes, cost, ch2, c2);
            i = 0;
            foreach (string str in strings)
                Console.WriteLine("{0} {1}", i++, str);
        }
    }
}
