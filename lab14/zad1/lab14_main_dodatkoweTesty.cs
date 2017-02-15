using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab14_Kankei
{
    class Program
    {

        static void Main(string[] args)
        {
            var tests = GenerateTests();
            foreach (var k in tests)
                PrintKankeiTest(k);
        }

        static void PrintKankeiTest(Kankei k)
        {
            Console.WriteLine(k);
            //k.Solve(0);   //odkomentowanie powoduje użycie innej metody rozwiązywania
            foreach (var r in k.Solutions)
            {
                for (int i = 0; i < r.GetLength(0); i++)
                {
                    for (int j = 0; j < r.GetLength(1); j++)
                        Console.Write("{0} ", r[i, j]==true ? 1 : 0);
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

        const int noTests = 6;
        static Kankei[] GenerateTests()
        {
            Kankei[] ret = new Kankei[noTests];

            bool?[,] b0 = { { true, null, null }, { null, null, null }, { null, null, null } };
            int[,] c0 = { { 3, 3 }, { 1, 1 }, { 1, 1 } };
            ret[0] = new Kankei(b0, c0);


            bool?[,] b1 = new bool?[5, 5];
            b1[2, 1] = true;
            b1[2, 3] = true;
            int[,] c1 = { {1,2},
                       {2,3},
                       {3,0},
                       {2,3},
                       {2,2}};
            ret[1] = new Kankei(b1, c1);


            bool?[,] b2 = new bool?[10, 10];
            b2[0, 1] = true;
            b2[0, 5] = true;
            b2[1, 3] = true;
            b2[3, 0] = true;
            b2[5, 0] = true;
            b2[5, 2] = true;
            b2[6, 6] = true;
            b2[7, 4] = true;
            b2[8, 0] = true;
            b2[8, 9] = true;
            b2[9, 2] = true;
            b2[9, 7] = true;
            int[,] c2 = { {3, 7},
                        {5, 6},
                        {1, 2},
                        {1, 3},
                        {0, 3},
                        {3, 3},
                        {5, 1},
                        {3, 2},
                        {5, 2},
                        {4, 1}
                        };
            ret[2] = new Kankei(b2, c2);

            bool?[,] b3 = new bool?[10, 10];
            b3[0, 0] = true;
            b3[2, 0] = true;
            b3[2, 5] = true;
            b3[2, 9] = true;
            b3[3, 3] = true;
            b3[4, 9] = true;
            b3[6, 5] = true;
            b3[8, 0] = true;
            b3[8, 5] = true;
            b3[9, 2] = true;

            int[,] c3 = { {2, 4},
                            {1, 4},
                            {5, 2},
                            {4, 2},
                            {2, 2},
                            {2, 5},
                            {1, 1},
                            {1, 0},
                            {4, 1},
                            {3, 4}
                        };

            ret[3] = new Kankei(b3, c3);

            bool?[,] b4 = new bool?[10, 10];
            b4[0, 3] = true;
            b4[0, 6] = true;
            b4[2, 3] = true;
            b4[2, 9] = true;
            b4[3, 0] = true;
            b4[3, 6] = true;
            b4[4, 2] = true;
            b4[4, 4] = true;
            b4[4, 8] = true;
            b4[6, 6] = true;
            b4[9, 6] = true;
            b4[9, 8] = true;

            int[,] c4 = { {2, 2},
                            {3, 2},
                            {6, 3},
                            {4, 3},
                            {5, 2},
                            {3, 3},
                            {2, 7},
                            {0, 1},
                            {1, 4},
                            {4, 3},
                        };

            ret[4] = new Kankei(b4, c4);

            bool?[,] b5 = new bool?[10, 10];
            b5[0, 9] = true;
            b5[1, 1] = true;
            b5[1, 7] = true;
            b5[2, 5] = true;
            b5[3, 1] = true;
            b5[3, 8] = true;
            b5[4, 4] = true;
            b5[5, 7] = true;
            b5[7, 6] = true;
            b5[9, 0] = true;
            b5[9, 2] = true;
            b5[9, 7] = true;

            int[,] c5 = { {3, 2},
                             {3, 5},
                             {5, 2},
                             {3, 1},
                             {4, 3},
                             {2, 3},
                             {0, 3},
                             {1, 4},
                             {2, 4},
                             {7, 3}
                         };

            ret[5] = new Kankei(b5, c5);

            return ret;
        }
    }
}
