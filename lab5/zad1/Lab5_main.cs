using System;
using System.Collections.Generic;
using System.Linq;

namespace Square
{
    partial class Lab5
    {
        /// <summary>
        /// Lista przypadków testowych. Pierwsza wartość to liczba kwadratów do wydania, druga oczekiwany wynik.
        /// </summary>
        static List<Tuple<int, int>> TestCases = new List<Tuple<int, int>>()
        {
            new Tuple<int, int>(4, 1),
            new Tuple<int, int>(7, 4),
            new Tuple<int, int>(8, 2),
            new Tuple<int, int>(9, 1),
            new Tuple<int, int>(99991, 4),
            new Tuple<int, int>(99996, 4),    
            new Tuple<int, int>(99999, 4),    
            new Tuple<int, int>(100007, 4),
            new Tuple<int, int>(200007, 4),    
            new Tuple<int, int>(12345, 3),    
            new Tuple<int, int>(56342, 3), 
            new Tuple<int, int>(11881, 1),    
            new Tuple<int, int>(11882, 2),    
            new Tuple<int, int>(25281, 1),    
            new Tuple<int, int>(25280, 3),  
            new Tuple<int, int>(25282, 2),    
        };

        static void Main(string[] args)
        {
            int[] areas;

            foreach (var testCase in TestCases)
            {
                areas = RunTestCase(testCase, false);
                areas = RunTestCase(testCase, true);
            }
        }

        private static int[] RunTestCase(Tuple<int, int> testCase, bool brutForce)
        {
            int[] areas;
            Console.Write(
                "{0} TEST: {1,6} - ", brutForce ? "CertificateNumberBrutforce"
                                                : "CertificateNumber         ", testCase.Item1);

            int no;
            if (brutForce)
            {
                no = CertificateNumberLagrange(testCase.Item1, out areas);
            }
            else
            {
                no = CertificateNumberDynamicPrograming(testCase.Item1, out areas);
            }

            if (no != testCase.Item2)
            {
                Console.WriteLine("FAIL !!!");
            }
            else if (areas.Sum(i => i * i) != testCase.Item1)
            {
                Console.WriteLine("area - FAIL");
            }
            else
            {
                Console.WriteLine("OK");
            }

            return areas;
        }
    }
}
