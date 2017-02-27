using System;
using System.Threading;

namespace ASD
{
    class Program
    {
        static void Main(string[] args)
        {
            TestCase[] testy =
            {
                makeTestCase(true, 1, 0, "()", "()"),
                makeTestCase(false, int.MaxValue, 1, "())", "()"),
                makeTestCase(true, 8, 0, "(()(()()))()(())", "()"),
                makeTestCase(false, int.MaxValue, 3, "())())(()", "()"),
                makeTestCase(true, 10, 0, "()?..().) (.)(.)", ".", "()", " ", ")?", ".."),
                makeTestCase(false, int.MaxValue, 3, "abcdefgabcdefgabcdefg", "ab", "bc", "cd", "de", "ef", "fg"),
                makeTestCase(true, 5, 0, "xAAAxBxC", "x", "xA", "xB", "xC", "AB", "AC"),
                makeTestCase(false, int.MaxValue, 10, "(xAAxBxC)(xAAxBxC)(xAAxBxC)(xAAxBxC)(xAAxBxC)", "x", "xA", "xB", "xC", "AB", "AC"),
                makeTestCase(true, 250, 0, "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", "AA", "A"),
                makeTestCase(false, int.MaxValue, 1, "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", "AA"),
            };

            Console.WriteLine("Wynik metody Eraseable:");
            int i = 0;
            foreach (TestCase tc in testy)
            {
                string msg;
                tc.TestResults(out msg);
                Console.WriteLine("Test " + (i++).ToString() + ":\t" + msg);
            }

            Console.WriteLine("\n\nOptymalna liczba skreslen:");
            i = 0;
            foreach (TestCase tc in testy)
            {
                string msg;
                tc.TestCrossoutsNumber(out msg);
                Console.WriteLine("Test " + (i++).ToString() + ":\t" + msg);
            }

            Console.WriteLine("\n\nMinimalna liczba nieskreslonych syboli:");
            i = 0;
            foreach (TestCase tc in testy)
            {
                string msg;
                tc.TestMinimumRemainder(out msg);
                Console.WriteLine("Test " + (i++).ToString() + ":\t" + msg);
            }
        }

        static TestCase makeTestCase(bool result, int minCrossouts, int minRemainder, string sequence, params string[] patterns)
        {
            char[] seq = new char[sequence.Length];
            for (int i = 0; i < sequence.Length; i++)
                seq[i] = sequence[i];
            char[][] pat = new char[patterns.GetLength(0)][];
            for (int i = 0; i < patterns.GetLength(0); i++)
            {
                pat[i] = new char[patterns[i].Length];
                for (int j = 0; j < patterns[i].Length; j++)
                    pat[i][j] = patterns[i][j];
            }
            return new TestCase(seq, pat, result, minCrossouts, minRemainder);
        }
    }

    enum TestResult
    {
        OK, Timeout, Exception, BadResult, BadCrossoutsNumber, BadMinimumRemainder
    }

    class TestCase
    {
        public char[] sequence;
        public char[][] patterns;

        public bool result;
        public int crossoutNumber;
        public int minimumRemainder;

        public TestCase(char[] seq, char[][] pat, bool res, int cn, int mr)
        {
            sequence = seq;
            patterns = pat;
            result = res;
            crossoutNumber = cn;
            minimumRemainder = mr;
        }

        public TestResult TestResults(out string message)
        {
            bool res=false;
            int w;
            TestResult ret = TestResult.OK;
            string msg = "OK";
            res = CrossoutChecker.Erasable(sequence, patterns, out w);
            if (res != result)
            {
                ret = TestResult.BadResult;
                msg = "BLAD: jest " + res.ToString() + " a powinno byc " + result.ToString();
            }
            message = msg;
            return ret;
        }

        public TestResult TestCrossoutsNumber(out string message)
        {
            bool res;
            int w = -1;
            TestResult ret = TestResult.OK;
            string msg = "OK";
            res = CrossoutChecker.Erasable(sequence, patterns, out w);
            if (w != crossoutNumber)
            {
                ret = TestResult.BadCrossoutsNumber;
                msg = "BLAD: jest " + w.ToString() + " a powinno byc " + crossoutNumber.ToString();
            }
            message = msg;
            return ret;
        }

        public TestResult TestMinimumRemainder(out string message)
        {
            int w = -1;
            TestResult ret = TestResult.OK;
            string msg = "OK";
            w = CrossoutChecker.MinimumRemainder(sequence, patterns);
            if (w != minimumRemainder)
            {
                ret = TestResult.BadMinimumRemainder;
                msg = "BLAD: jest " + w.ToString() + " a powinno byc " + minimumRemainder.ToString();
            }
            message = msg;
            return ret;
        }
    }
}
