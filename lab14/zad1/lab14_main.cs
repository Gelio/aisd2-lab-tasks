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

        const int noTests = 3;
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


            return ret;
        }
    }
}
