using System;
using System.Collections.Generic;
using System.Linq;
using Point = ASD.Geometry.Point;
using Segment = ASD.Geometry.Segment;
namespace ASD
{
    class Lab12_main
    {
        static void Main(string[] args)
        {

            ConvexHullTest();

            MaxDiameterTests();

            ChineeseAltarTests();
            Console.ReadKey();
        }

        #region Tests Definition

        static void ConvexHullTest()
        {
            Random r = new Random(122);
            Console.WriteLine("Wyznaczanie otoczki wypukłej : ");
            List<Point> t3 = new List<Point>();
            for (int i = 0; i < 100; i++)
            {
                Point ns = new Point(r.Next() % 1000 - 500, r.Next() % 1000 - 500);
                t3.Add(ns);
            }
            var tests = new[]{
                new
                {
                    Points = new Point[]{new Point(0,0), new Point(10,0),new Point(10,10),new Point(0,10)},
                    Hull = new Point[]{new Point(0,0), new Point(10,0),new Point(10,10),new Point(0,10)},

                },
               new
                {
                    Points = new Point[]{new Point(0,0), new Point(10,0),new Point(10,10),new Point(0,10), new Point(1,2),new Point(0,5), new Point(1,10), new Point(8,9), new Point(9.5,10)},
                    Hull = new Point[]{new Point(0,0), new Point(10,0),new Point(10,10),new Point(0,10)},

                },
                new
                {
                    Points=t3.ToArray(),
                    Hull=new Point[]{new Point(-157, -500),new Point(237, -494),new Point(467, -469),new Point(484, -335),new Point(491, -187),new Point(484, 345),
                        new Point(415, 462),new Point(-133, 488),new Point(-444, 378),new Point(-495, 316), new Point(-480,-491)}
                }

            };




            for (int i = 0; i < tests.Length; i++)
            {
                var t = tests[i];
                bool success = false;
                string message = "";
                try
                {
                    Point[] result = Lab12.ConvexHull(t.Points);
                    success = CheckConvexHullResult(result, t.Hull);
                }
                catch (Exception e)
                {
                    success = false;
                    message = e.Message;
                }
                if (success)
                    Console.WriteLine("{0} : {1} ", i + 1, "SUKCES");
                else
                    Console.WriteLine("{0} : {1} - {2}", i + 1, "BŁĄD", message);
            }



            Console.WriteLine();

        }

        static void MaxDiameterTests()
        {
            Random r = new Random(210);
            Console.WriteLine("Maksymalna odległość między punktami : ");
            List<Point> t3 = new List<Point>();
            for (int i = 0; i < 100; i++)
            {
                Point np = new Point(r.Next() % 1000 - 500, r.Next() % 1200 - 600);
                if (!t3.Contains(np))
                    t3.Add(np);
            }
            List<Point> t4 = new List<Point>();
            for (int i = 0; i < 1000; i++)
            {
                Point np = new Point(r.NextDouble() * 100, r.NextDouble() * 100);
                if (!t4.Contains(np))
                    t4.Add(np);
            }

            var tests = new[]{
                new
                {
                    Points=new Point[]{new Point(0,0), new Point(10,0),new Point(10,10),new Point(0,9.5)},
                    ResultLenght=Math.Sqrt(200)
                },
                new
                {
                    Points=new Point[]{new Point(0,0), new Point(10,0),new Point(10,10),new Point(0,9.5), new Point(5,5), new Point(2.5,7), new Point(8,8), new Point(10,5), new Point(9,9)},
                    ResultLenght=Math.Sqrt(200)
                },
                new
                {
                    Points=new Point[]{new Point(0,0), new Point(50,0),new Point(150,200),new Point(0,9.5), new Point(55,55), new Point(25,70), new Point(18,82), new Point(10,5), new Point(96,9), new Point(100,54), new Point(95,90), new Point(10,5), new Point(119,39), new Point(88,5), new Point(180,90)},
                    ResultLenght=250.0
                },
                new
                {
                    Points=t3.ToArray(),
                    ResultLenght=1461.7694756698129
                },
                new{
                    Points=t4.ToArray(),
                    ResultLenght=134.27793211203621
                }
            };

            for (int i = 0; i < tests.Length; i++)
            {
                var t = tests[i];
                bool success = false;
                string message = "";
                try
                {
                    Point[] result;
                    double lenght = Lab12.MaxDiameter(t.Points, out result);
                    success = CheckMaxDiameterResult(result, lenght, t.ResultLenght, t.Points);
                }
                catch (Exception e)
                {
                    success = false;
                    message = e.Message;
                }
                if (success)
                    Console.WriteLine("{0} : {1} ", i + 1, "SUKCES");
                else
                    Console.WriteLine("{0} : {1} - {2}", i + 1, "BŁĄD", message);
            }



            Console.WriteLine();
        }

        static void ChineeseAltarTests()
        {
            Random r = new Random(211);
            Console.WriteLine("Chińskie ołtarze : ");
            List<Segment> t3 = new List<Segment>();
            for (int i = 0; i < 100; i++)
            {
                Segment ns = new Segment(new Point(r.Next() % 100 - 50, r.Next() % 100 - 50), new Point(r.Next() % 100 - 50, r.Next() % 100 - 50));
                t3.Add(ns);
            }
            var tests = new[]{
                new
                {
                    Altar = new Point(0,0),
                    Walls = new []{
                        new Segment(new Point(-10,10),new Point(-10,-10)),
                        new Segment(new Point(10,10),new Point(10,-10)),
                        new Segment(new Point(-9,2),new Point(9,2)),
                        new Segment(new Point(-9,-2),new Point(9,-2))
                    },
                    Result=true
                },
                new
                {
                    Altar = new Point(0,0),
                    Walls = new []{
                        new Segment(new Point(-11,2.2),new Point(-11,-10)),
                        new Segment(new Point(10,10),new Point(10,-10)),
                        new Segment(new Point(-9,2),new Point(9,2)),
                        new Segment(new Point(-9,-2),new Point(9,-2))
                    },
                    Result=false
                },
                new
                {
                    Altar = new Point(5,1.5),
                    Walls = new []{
                        new Segment(new Point(-11,2.2),new Point(-11,-10)),
                        new Segment(new Point(10,10),new Point(10,-10)),
                        new Segment(new Point(-9,2),new Point(9,2)),
                        new Segment(new Point(-9,-2),new Point(9,-2))
                    },
                    Result=true
                },
                new
                {
                    Altar = new Point(1,1),
                    Walls = new []{
                        new Segment(new Point(-11,2.2),new Point(-11,-10)),
                        new Segment(new Point(10,10),new Point(10,1.1)),
                        new Segment(new Point(10,0.9),new Point(10,-10)),
                        new Segment(new Point(-9,2),new Point(9,2)),
                        new Segment(new Point(-9,-2),new Point(9,-2))
                    },
                    Result=false
                },
                new
                {
                    Altar = new Point(0,0),
                    Walls = t3.ToArray(),
                    Result=true
                },
                new
                {
                    Altar = new Point(3,2),
                    Walls = new []
                        {
                        new Segment(new Point(1,2),new Point(0,3)),
                        new Segment(new Point(1,3),new Point(5,3)),
                        new Segment(new Point(4,4),new Point(2,5)),
                        new Segment(new Point(0,2),new Point(2,4))
                        },
                    Result=false
                },
                new
                {
                    Altar = new Point(3,4),
                    Walls = new []
                        {
                        new Segment(new Point(2,6),new Point(2,3)),
                        new Segment(new Point(4,3),new Point(4,5))
                        },
                    Result=false
                },
                new
                {
                    Altar = new Point(3,3),
                    Walls = new []
                        {
                        new Segment(new Point(2,4),new Point(4,4))
                        },
                    Result=false
                },
                new
                {
                    Altar = new Point(4,4),
                    Walls = new []
                        {
                        new Segment(new Point(0,3),new Point(2,1)),
                        new Segment(new Point(5,1),new Point(7,3)),
                        new Segment(new Point(2,3),new Point(5,3)),
                        new Segment(new Point(1,3),new Point(1,6)),
                        new Segment(new Point(2,5),new Point(3.5,7)),
                        new Segment(new Point(5,8),new Point(7,6)),
                        new Segment(new Point(6,3),new Point(6,5))
                        },
                    Result=false
                },
                new
                {
                    Altar = new Point(4,4),
                    Walls = new []
                        {
                        new Segment(new Point(0,3),new Point(2,1)),
                        new Segment(new Point(5,1),new Point(7,3)),
                        new Segment(new Point(2,3),new Point(5,3)),
                        new Segment(new Point(1,3),new Point(1,6)),
                        new Segment(new Point(2,5),new Point(4,7)),
                        new Segment(new Point(5,8),new Point(7,6)),
                        new Segment(new Point(6,3),new Point(6,5.5)),
                        new Segment(new Point(3.5,9), new Point(6,9))
                        },
                    Result=true
                },
                new
                {
                    Altar = new Point(3,4),
                    Walls = new []
                        {
                        new Segment(new Point(4,2.8),new Point(4,5.2)),
                        new Segment(new Point(1,2),new Point(5,2)),
                        new Segment(new Point(1,6),new Point(5,6)),
                        new Segment(new Point(2,2.8),new Point(2,5.2)),
                        },
                    Result=true
                },
            };




            for (int i = 0; i < tests.Length; i++)
            {
                var t = tests[i];
                bool success = false;
                string message = "";
                try
                {
                    bool result = Lab12.ChineeseAltars(t.Altar, t.Walls);
                    success = CheckChineeseAltarResult(result, t.Result);
                }
                catch (Exception e)
                {
                    success = false;
                    message = e.Message;
                }
                if (success)
                    Console.WriteLine("{0} : {1} ", i + 1, "SUKCES");
                else
                    Console.WriteLine("{0} : {1} - {2}", i + 1, "BŁĄD", message);
            }



            Console.WriteLine();

        }

        static bool CheckConvexHullResult(Point[] result, Point[] expected)
        {
            if (result.Length != expected.Length)
                throw new Exception("Niewłaściwa liczba punktów");
            foreach (var p in result)
            {
                if (!expected.Any(p2 => p.x == p2.x && p.y == p2.y))
                    throw new Exception(String.Format("Niewłaściwy punkt na otoczce : {0},{1}", p.x, p.y));
            }
            return true;
        }

        private static bool CheckMaxDiameterResult(Point[] result, double lenghtResult, double lenghtExpected, Point[] points)
        {
            const double EPS = 10e-8;
            if (result.Length != 2)
                throw new Exception("Niepoprawna długość tablicy wyników");

            if (Math.Abs(lenghtExpected - lenghtResult) > EPS)
                throw new Exception("Niepoprawny wynik");

            if (!points.Contains(result[0]) || !points.Contains(result[1]))
                throw new Exception("Niepoprawne punkty");
            return true;
        }

        private static bool CheckChineeseAltarResult(bool expectedResult, bool result)
        {
            if (expectedResult != result)
                throw new Exception("Niepoprawy wynik");
            return true;
        }
        #endregion
    }
}
