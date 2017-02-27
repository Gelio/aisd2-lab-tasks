using System;
using System.Threading;
using System.Linq;
using System.Diagnostics;

namespace Lab07
{

    static class Util
    {
        public static readonly int debugerThreadingTime = 80;

        public static int[][] ConvertPreferences(int[][] truePreferences, int smellCount)
        {
            int[][] output = new int[truePreferences.Length][];

            for (int i = 0; i < truePreferences.Length; i++)
            {
                output[i] = new int[smellCount];
                foreach (int p in truePreferences[i])
                {
                    output[i][Math.Abs(p) - 1] = Math.Sign(p);
                }
            }
            return output;
        }
    }

    public class TestCase
    {
        public readonly int[][] preferences;
        public readonly int smellCount;
        public readonly int satisfactionLevel;

        public readonly bool result;
        public readonly int timeout;

        public static String ArrayToString(bool[] assignment)
        {
            return assignment != null ? String.Join(", ", assignment) : "null";
        }

        public TestCase(int[][] preferences, int smellCount, int satisfactionLevel, bool result, int timeout)
        {
            this.preferences = preferences;
            this.smellCount = smellCount;
            this.result = result;
            this.timeout = timeout;
            this.satisfactionLevel = satisfactionLevel;
        }

        private Boolean CheckAssignment(bool result, bool[] assignment)
        {
            return (result == false && assignment == null) || (result == true && assignment != null && assignment.Length == smellCount && preferences.All(
                pref => pref.Sum(i => i < 0 ? (assignment[-i - 1] ? -1 : 0) : (assignment[i - 1] ? 1 : 0)) >= satisfactionLevel
            ));
        }

        private Boolean CheckResults(bool result, bool[] assignment, Exception exception, bool verbose)
        {
            if (exception != null)
            {
                Console.Out.WriteLine("Wyjątek!");
                return false;
            }
            else if (this.result != result)
            {
                Console.WriteLine("Zły wynik! Otrzymano: {0}, oczekiwano {1}, zwrócone przypisanie {2}", 
                    result, this.result, ArrayToString(assignment));
                return false;
            }
            else if (!CheckAssignment(result, assignment))
            {
                Console.WriteLine("Niepoprawne przypisanie!");
                if (verbose)
                {
                    Console.WriteLine("Wynik: {1}, Otrzymano przypisanie: {0}", ArrayToString(assignment), result);
                }
                return false;
            }
            else
            {
                Console.WriteLine("OK!");
                return true;
            }
        }


        public Boolean PerformTest(bool enforceTimeout, double systemSpeedFactor, bool verbose, bool swallowExceptions)
        {
            bool result = false;
            bool[] assignment = null;
            Exception exception = null;
            Stopwatch sw = new Stopwatch();
            int[][] thePreferences = Util.ConvertPreferences(this.preferences, this.smellCount);


            Thread thread = new Thread(() =>
                {
                    try
                    {
                        sw.Start();
                        SmellsChecker sc = new SmellsChecker(this.smellCount, thePreferences, this.satisfactionLevel);
                        result = sc.AssignSmells(out assignment);
                        sw.Stop();
                        Console.Write("Czas: {0} jed.", sw.ElapsedMilliseconds / systemSpeedFactor);
                    }
                    catch (Exception ex)
                    {
                        if (!swallowExceptions)
                        {
                            throw ex;
                        }
                        else
                        {
                            exception = ex;
                        }
                    }

                });

            thread.Start();
            if (enforceTimeout)
            {
                if (!thread.Join(Util.debugerThreadingTime+(int)Math.Ceiling(timeout * systemSpeedFactor)))
                {
                    thread.Abort();
                    Console.WriteLine("Timeout!");
                    return false;
                }
                else
                {
                    return CheckResults(result, assignment, exception, verbose);
                }

            }
            else
            {
                thread.Join();
                return CheckResults(result, assignment, exception, verbose);
            }
        }
		
    }

    public class TestCase2
    {
        public readonly int[][] preferences;
        public readonly int smellCount;
        public readonly int satisfactionLevel;

        public readonly int result;
        public readonly int timeout;

        public TestCase2(int[][] preferences, int smellCount, int satisfactionLevel, int result, int timeout)
        {
            this.preferences = preferences;
            this.smellCount = smellCount;
            this.result = result;
            this.timeout = timeout;
            this.satisfactionLevel = satisfactionLevel;
        }

        private Boolean CheckAssignment(int result, bool[] assignment)
        {
            return assignment != null && assignment.Length == smellCount && (preferences.Count(
                pref => pref.Sum(i => i < 0 ? (assignment[-i - 1] ? -1 : 0) : (assignment[i - 1] ? 1 : 0)) >= satisfactionLevel
            ) == result);
        }

        private Boolean CheckResults(int result, bool[] assignment, Exception exception, bool verbose)
        {
            if (exception != null)
            {
                Console.Out.WriteLine("Wyjątek!");
                return false;
            }
            else if (this.result != result)
            {
                Console.WriteLine("Zły wynik! Otrzymano: {0}, oczekiwano {1}, zwrócone przypisanie {2}", result, this.result, TestCase.ArrayToString(assignment));
                return false;
            }
            else if (!CheckAssignment(result, assignment))
            {
                Console.WriteLine("Niepoprawne przypisanie!");
                if (verbose)
                {
                    Console.WriteLine("Wynik: {1}, Otrzymano przypisanie: {0}", TestCase.ArrayToString(assignment), result);
                }
                return false;
            }
            else
            {
                Console.WriteLine("OK!");
                return true;
            }
        }

        public Boolean PerformTest(bool enforceTimeout, double systemSpeedFactor, bool verbose, bool swallowExceptions)
        {
            int result = -1;
            bool[] assignment = null;
            Exception exception = null;
            Stopwatch sw = new Stopwatch();
            int[][] thePreferences = Util.ConvertPreferences(this.preferences, smellCount);

            Thread thread = new Thread(() =>
                {
                    try
                    {
                        sw.Start();
                        SmellsChecker sc = new SmellsChecker(this.smellCount, thePreferences, this.satisfactionLevel);
                        result = sc.AssignSmellsMaximizeHappyCustomers(out assignment);
                        sw.Stop();
                        Console.Write("Czas: {0} jed.", sw.ElapsedMilliseconds / systemSpeedFactor);
                    }
                    catch (Exception ex)
                    {
                        if (!swallowExceptions)
                        {
                            throw ex;
                        }
                        else
                        {
                            exception = ex;
                        }
                    }

                });

            thread.Start();
            if (enforceTimeout)
            {
                if (!thread.Join(Util.debugerThreadingTime+(int)Math.Ceiling(timeout * systemSpeedFactor)))
                {
                    thread.Abort();
                    Console.WriteLine("Timeout!");
                    return false;
                }
                else
                {
                    return CheckResults(result, assignment, exception, verbose);
                }

            }
            else
            {
                thread.Join();
                return CheckResults(result, assignment, exception, verbose);
            }
        }

    }


    public class Program
    {

        public static int fib(int n)
        {
            if (n == 1 || n == 2)
                return 1;

            return fib(n - 1) + fib(n - 2);

        }

        public static void Main(string[] args)
        {
            // czy wypisywać nieprawidłowe przypisania?
            const Boolean verbose = false;

            // czy przechwytywać wyjątki?
            const Boolean swallowExceptions = true;


            Stopwatch sw = new Stopwatch();
            sw.Start();
            fib(40);
            sw.Stop();

            const double jajkoConstant = 2.0;

            double speedFactor = sw.ElapsedMilliseconds / 2383.0 * jajkoConstant;

            System.Console.WriteLine("System performance meter: {0}", speedFactor);

            int[][] test1 = new int[][]
            {
                new int[]{ -1, 2, 3 },
                new int[]{ 1, 2, 3 },
                new int[]{ 2, -3 }
            };


            int[][] test2 = new int[][]
            {
                new int[]{ -1, 2, 3 },
                new int[]{ 1, 2, 3 },
                new int[]{ -2, -3 }
            };

            int[][] testNeg = new int[][]
            {
                new int[] { -15, -24 },
                new int[] { -10, -24 },
                new int[] { -14 },
                new int[] { 10 },
                new int[] { 4 }
            };

            int[][] test3a = new int[24][];
            for (int i = 0; i < 24; i++)
            {
                test3a[i] = new int[] { i + 1, -((i + 1) % 24 + 1) };
            }

            int[][] test3 = new int[50][];
            for (int i = 0; i < 50; i++)
            {
                test3[i] = new int[] { i + 1, -((i + 1) % 50 + 1) };
            }

            int[][] test4 = new int[50][];
            for (int i = 0; i < 50; i++)
            {
                test4[i] = new int[] { i + 1, (i + 1) % 50 + 1, -((i + 2) % 50 + 1) };
            }

            int[][] test3o = new int[27][];
            for (int i = 0; i < 27; i++)
            {
                test3o[i] = new int[] { i + 1, -((i + 1) % 27 + 1) };
            }

            int[][] test4o = new int[21][];
            for (int i = 0; i < 21; i++)
            {
                test4o[i] = new int[] { i + 1, (i + 1) % 21 + 1, -((i + 2) % 21 + 1) };
            }

            int[][] test5 = new int[102][];
            for (int i = 0; i < 102; i++)
            {
                switch (i % 3)
                {
                    case 0:
                        test5[i] = new int[] { i % 21 + 1, -(i + 1) % 21 - 1, (i + 2) % 21 + 1, (i + 3) % 21 + 1 };
                        break;
                    case 1: 
                        test5[i] = new int[] { -(i % 21) - 1, (i + 2) % 21 + 1, (i + 1) % 21 + 1, (i + 4) % 21 + 1  };
                        break;
                    case 2: 
                        test5[i] = new int[] { i % 21 + 1, (i + 2) % 21 + 1, (i + 1) % 21 + 1 };
                        break;
                }
            }

            int[][] test6 =
                {
                    new int[]{ 1, -2, 3, 4, 5, -6, -7, -8, 9, 10 },
                    new int[]{ -1, 2, 4, 6, -8, 9, 10 },
                    new int[]{ 2, 6, 9, 10 },
                    new int[]{ 1, 2, 3, -8, -9, 5 },
                    new int[]{ -8, 2, 5, 7 },
                    new int[]{ -1, -2, 3, 4, 5, 6, 7, 8, 9 },
                    new int[]{ 1, 2, -3, -4, 5, 6, 7, 8, 9 }
                };

            //Etap1 
            TestCase[] tc =
                {
                    new TestCase(test1, 3, 1, true, 5),
                    new TestCase(test1, 3, 2, false, 5),
                    new TestCase(test2, 3, 1, false, 5),
                    new TestCase(test6, 10, 3, true, 5),
                    new TestCase(test3a, 24, 1, false, 5),
                    new TestCase(testNeg, 24, 1, false, 5),
                    new TestCase(test4, 50, 0, true, 5),
                    new TestCase(test3, 50, 1, false, 5),
                    new TestCase(test4, 50, 1, true, 5),
                    new TestCase(test4, 50, 2, false, 5),
                    new TestCase(test5, 21, 2, true, 5),
                };
            int bigTests = 5;

            Console.WriteLine("*\nEtap1, poprawność. Start!\n*");
            int score1 = 0;
            for (int i = 0; i < tc.Length - bigTests; i++)
            {
                Console.Out.Write("Test {0}: ", i);
                if (tc[i].PerformTest(false, speedFactor, verbose, swallowExceptions))
                    score1++;
            }
            Console.WriteLine("*\nEtap1, poprawność. Wynik: {0}, {1}p\n*", score1 == tc.Length - bigTests, score1 == tc.Length - bigTests ? 1 : 0);

            Console.WriteLine("*\nEtap1, wydajność. Start!\n*");
            int score2 = 0;
            for (int i = 0; i < tc.Length; i++)
            {
                Console.Out.Write("Test {0}: ", i);
                if (tc[i].PerformTest(true, speedFactor, verbose, swallowExceptions))
                    score2++;
            }
            Console.WriteLine("*\nEtap1, wydajność. Wynik: {0}, {1}p\n*", score2 == tc.Length, score2 == tc.Length ? 1 : 0);


            // Etap 2
            TestCase2[] tc2 =
                {
                    new TestCase2(test1, 3, 1, 3, 5),
                    new TestCase2(test1, 3, 2, 2, 5),
                    new TestCase2(test1, 3, 5, 0, 5),
                    new TestCase2(test2, 3, 1, 2, 5),
                    new TestCase2(test2, 3, 0, 3, 5),
                    new TestCase2(test6, 10, 4, 4, 5),
                    new TestCase2(test6, 10, 3, 7, 5),
                    new TestCase2(test3a, 24, 1, 12, 2000),
                    new TestCase2(test3a, 24, 2, 0, 5),
                    new TestCase2(testNeg, 24, 1, 2, 5),
                    new TestCase2(test3o, 27, 1, 13, 20000),
                    new TestCase2(test4o, 21, 1, 21, 5),
                    new TestCase2(test4o, 21, 2, 7, 1200),
                    new TestCase2(test5, 21, 3, 68, 90),
                };
            int bigTests2 = 7;

            Console.WriteLine("*\nEtap2, poprawność. Start!\n*");
            int score3 = 0;
            for (int i = 0; i < tc2.Length - bigTests2; i++)
            {
                Console.Out.Write("Test {0}: ", i);
                if (tc2[i].PerformTest(false, speedFactor, verbose, swallowExceptions))
                    score3++;
            }
            Console.WriteLine("*\nEtap2, poprawność. Wynik: {0}, {1}p\n*", score3 == tc2.Length - bigTests2, score3 == tc2.Length - bigTests2 ? 1 : 0);

            Console.WriteLine("*\nEtap2, wydajność. Start!\n*");
            int score4 = 0;
            for (int i = 0; i < tc2.Length; i++)
            {
                Console.Out.Write("Test {0}: ", i);
                if (tc2[i].PerformTest(true, speedFactor, verbose, swallowExceptions))
                    score4++;
            }
            Console.WriteLine("*\nEtap2, wydajność. Wynik: {0}, {1}p\n*", score4 == tc2.Length, score4 == tc2.Length ? 1 : 0);


        }
    }
}
