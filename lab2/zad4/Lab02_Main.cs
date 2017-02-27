using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASD
{
    class Lab02
    {
        const int n = 60;
        const int n_rec = 30;
        static void Main(string[] args)
        {
            int[] outputs = new int[] { 
                   0,    9,   29,   79,  189,  416,  858, 1677, 3133, 5632, 9792, 6532, 7190, 3677, 8675, 5888,  356, 8843,  311, 6493, 
                2579, 8030, 7536, 2135,  511,  490,  754, 2794, 3124, 5779, 5121, 8978, 2142,  253, 4097, 4347, 6777, 7980, 1622, 5265,
                7793, 7476,  708, 1456, 1458, 1209, 1775, 7476, 9480,  351, 9595,  249, 6559, 2794, 3244, 3451, 2723, 7982, 8998, 5062,
            };

            Console.WriteLine();
            Test_RecFunction(outputs);
            Test_DPFunction(outputs);
            Test_DP2Function(outputs);
            Console.WriteLine();
        }

        private static void Test_RecFunction(int[] outputs)
        {
            Console.WriteLine("FUNKCJA REKURENCYJNA");

            int[] result = new int[n_rec];
            for (int i = 0; i < n_rec; ++i)
                try
                {
                    result[i] = SpecialNumbers.SpecialNumbersRec(i);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Incorrect exception: " + e.Message);
                }
            if (Compare_Results(outputs, result))
                Console.WriteLine("Test ZDANY!\n");
            else
                Console.WriteLine("Test OBLANY!\n");
        }

        private static void Test_DPFunction(int[] outputs)
        {
            Console.WriteLine("FUNKCJA WYKORZYSTUJACA PROGRAMOWANIE DYNAMICZNE");

            int[] result = new int[n];
            for (int i = 0; i < n; ++i)
                try
                {
                    result[i] = SpecialNumbers.SpecialNumbersDP(i);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Incorrect exception: " + e.Message);
                }
            if (Compare_Results(outputs, result))
                Console.WriteLine("Test ZDANY!\n");
            else
                Console.WriteLine("Test OBLANY!\n");
        }

        private static bool Compare_Results(int[] model, int[] result)
        {
            int res = 0;
            for (int i = 0; i < result.Length; ++i)
            {
                if (model[i] != result[i])
                    Console.WriteLine("Dla {0} cyfr: powinno byc {1}, a jest {2}", i, model[i], result[i]);
                else
                    ++res;
            }
            return res == result.Length;
        }

        private static void Test_DP2Function(int[] outputs)
        {
            Console.WriteLine("FUNKCJA WYKORZYSTUJACA PROGRAMOWANIE DYNAMICZNE, Z WYMAGANIAMI W TABLICY");

            Console.WriteLine("TEST 1 - specjalne liczby");
            bool[,] req = new bool [9, 9];
            for (int i = 0; i < 9; ++i)
                for (int j = 0; j < 9; ++j)
                    req[i, j] = ((i == j) || ((i > j) && (i % 2 != j % 2))) ;
            int[] result = new int[n];
            for (int i = 0; i < n; ++i)
                try
                {
                    result[i] = SpecialNumbers.SpecialNumbersDP(i, req);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Incorrect exception: " + e.Message);
                }
            if (Compare_Results(outputs, result))
                Console.WriteLine("Test ZDANY!");
            else
                Console.WriteLine("Test OBLANY!");

            Console.WriteLine("TEST 2 - wszystkie cyfry takie same");
            for (int i = 0; i < 9; ++i)
                for (int j = 0; j < 9; ++j)
                    req[i, j] = (i == j)? true: false;
            int[] result_t2 = new int[10];
            int[] outp_t2 = new int[10] { 0, 9, 9, 9, 9, 9, 9, 9, 9, 9 };
            for (int i = 0; i < 10; ++i)
                try
                {
                    result_t2[i] = SpecialNumbers.SpecialNumbersDP(i, req);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Incorrect exception: " + e.Message);
                }
            if (Compare_Results(outp_t2, result_t2))
                Console.WriteLine("Test ZDANY!");
            else
                Console.WriteLine("Test OBLANY!");

            Console.WriteLine("TEST 3 - tylko jednocyfrowe");
            for (int i = 0; i < 9; ++i)
                for (int j = 0; j < 9; ++j)
                    req[i, j] = false;
            int[] result_t3 = new int[10];
            int[] outp_t3 = new int[10] { 0, 9, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < 10; ++i)
                try
                {
                    result_t3[i] = SpecialNumbers.SpecialNumbersDP(i, req);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Incorrect exception: " + e.Message);
                }
            if (Compare_Results(outp_t3, result_t3))
                Console.WriteLine("Test ZDANY!");
            else
                Console.WriteLine("Test OBLANY!");

            Console.WriteLine("TEST 4 - kolejna cyfra mniejsza od poprzedniej");
            for (int i = 0; i < 9; ++i)
                for (int j = 0; j < 9; ++j)
                    req[i, j] = i > j ? true: false;
            int[] result_t4 = new int[20];
            int[] outp_t4 = new int[20] { 0, 9, 36, 84, 126, 126, 84, 36, 9, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < 20; ++i)
                try
                {
                    result_t4[i] = SpecialNumbers.SpecialNumbersDP(i, req);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Incorrect exception: " + e.Message);
                }
            if (Compare_Results(outp_t4, result_t4))
                Console.WriteLine("Test ZDANY!");
            else
                Console.WriteLine("Test OBLANY!");

        }

    }

}

