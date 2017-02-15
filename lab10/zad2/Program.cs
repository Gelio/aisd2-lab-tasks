using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab10
{
    class Program
    {
        public class Test
        {
            public Test(int n, int m, Point[] dragons, bool answer)
            {
                N = n;
                M = m;
                Dragons = dragons;
                Answer = answer;
            }

            public int N { get; set; }
            public int M { get; set; }


            public Point[] Dragons { get; set; }

            public bool Answer { get; set; }

            public bool Verify(Point[] route)
            {
                if (route==null)
                {
                    Console.WriteLine("Brak zdefiniowanej drogi");
                    return false;
                }

                if (route.Length != N * M - Dragons.Length)
                {
                    Console.WriteLine("Nieprawidłowa liczba odwiedzonych pól");
                    return false;
                }

                var repetitions = route.GroupBy(x => x).Where(g => g.Count() > 1).ToList();

                if (repetitions.Count > 0)
                {
                    Console.WriteLine("Następujące pola powtarzają się:");
                    foreach (var repetition in repetitions)
                        Console.WriteLine("\t{0}", repetition.Key);
                    return false;
                }

                var fieldsWithDragons = route.Intersect(Dragons).ToList();

                if (fieldsWithDragons.Count > 0)
                {
                    Console.WriteLine("Następujące pola ze smokami zostały odwiedzone:");
                    foreach (var fieldWithDragon in fieldsWithDragons)
                        Console.WriteLine("\t{0}", fieldWithDragon);
                    return false;
                }

                if (!route[0].Equals(new Point(0,0)) )
                {
                    Console.WriteLine("Droga nie zaczyna się od pola [0,0]");
                    return false;
                }

                bool res=true;
                for ( int i=1 ; i<route.Length ; ++i )
                    {
                    if ( route[i].X<0 || route[i].X>=N || route[i].Y<0 || route[i].X>=M )
                        {
                        Console.WriteLine("\tNieprawidłowe pole {0}", route[i]);
                        res=false;
                        }
                    if ( Math.Abs(route[i].X-route[i-1].X)+Math.Abs(route[i].Y-route[i-1].Y)!=3 || Math.Abs(route[i].X-route[i-1].X)==0 || Math.Abs(route[i].Y-route[i-1].Y)==0 )
                        {
                        Console.WriteLine("\tPrzejście z pola {0} na pole {1} jest niepoprawne", route[i-1], route[i]);
                        res=false;
                        }
                    }

                return res;
            }
        }


        static void Main(string[] args)
        {
            Test[] tests = { new Test(4,4, new Point[0], false),
                             new Test(4,5, new Point[0], true),
                             new Test(4,6, new Point[0], true),
                             new Test(4,7, new Point[0], true),
                             new Test(5,5, new Point[0], true),
                             new Test(6,6, new Point[0], true),
                             new Test(
                                4, 4, 
                                new [] 
                                {
                                    new Point(1,0),
                                    new Point(1,1),
                                    new Point(3,0),
                                    new Point(3,1),
                                    new Point(0,2),
                                    new Point(0,3),
                                    new Point(2,2),
                                    new Point(2,3)
                                }, 
                                true),
                             new Test(
                                5, 5, 
                                new [] 
                                {
                                    new Point(1,0),
                                    new Point(0,1),
                                    new Point(0,2),
                                    new Point(0,4),
                                    new Point(1,3),
                                    new Point(1,4),
                                    new Point(3,1),
                                    new Point(3,3),
                                    new Point(3,4),
                                    new Point(4,0),
                                    new Point(4,1),
                                }, 
                                true),
                             new Test(5,5, new [] { new Point(1,2), new Point(2,1) }, false),
                             new Test(4,7, new [] { new Point(1,2), new Point(1,1), new Point(3,6) }, false),
                             new Test(5,5, new [] { new Point(3,4), new Point(2,1), new Point(3,3) }, false),
                           };


            for (int i = 0; i < tests.Length; i++)
            {
                Console.WriteLine("Test {0} ", i);
                Point[] route;

                bool result = new Knights(tests[i].N, tests[i].M, tests[i].Dragons).CalculateRoute(out route);

                if (tests[i].Answer == result)
                {
                    Console.WriteLine("wynik OK");
                }
                else
                {
                    Console.WriteLine("Niepoprawny wynik!");
                }

                if (result)
                {
                    if (tests[i].Verify(route))
                    {
                        Console.WriteLine("droga OK");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
