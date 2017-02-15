using System;
using Point = ASD.Geometry.Point;
using Triangle = ASD.Geometry.Triangle;
using System.Collections.Generic;

namespace ASD
{
    partial class lab13
    {

        public static void Test1()
        {
            List<Point[]> tests = new List<Point[]>();
            List<bool> mon = new List<bool>();
            List<Point[]> resp = new List<Point[]>();
            tests.Add(new Point[] { new Point(2, 0), new Point(0, 0), new Point(1, 1) });
            mon.Add(true);
            resp.Add(new Point[] { new Point(0, 0), new Point(1, 1), new Point(2, 0) });

            tests.Add(new Point[] { new Point(2, 0), new Point(1, 1), new Point(0, 0) });
            mon.Add(true);
            resp.Add(new Point[] { new Point(0, 0), new Point(2, 0), new Point(1, 1) });

            tests.Add(new Point[] { new Point(0, 3), new Point(2, 3), new Point(3, 2), new Point(4, 1), new Point(9, 1), new Point(7, 5), new Point(6, 4), new Point(5, 5) });
            mon.Add(true);
            resp.Add(new Point[] { new Point(0, 3), new Point(2, 3), new Point(3, 2), new Point(4, 1), new Point(9, 1), new Point(7, 5), new Point(6, 4), new Point(5, 5) });


            tests.Add(new Point[] { new Point(0, 0), new Point(4, 0), new Point(2, 1), new Point(3, 2) });
            mon.Add(false);

            tests.Add(new Point[] { new Point(4, 0), new Point(1, 2), new Point(2, 1), new Point(0, 0) });
            mon.Add(false);

            tests.Add(new Point[] { new Point(2, 2), new Point(0, 0), new Point(1, -1), new Point(1, 0), new Point(3, 0) });
            mon.Add(false);
            
            
            Point[] sortedPolygon;
            
            bool m;

            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine("TEST {0}",i+1);
                //res = triangulateMonotone(tests[i], out triangulation);
                m = isMonotone(tests[i], out sortedPolygon);

                Console.Write(" monotonicznosc: ");
                if (m==mon[i]) 
                    Console.WriteLine("OK");
                else Console.WriteLine("bledna odpowiedz");



                Console.Write(" sortedPolygon:  ");

                if (i > 2)
                {
                    if (sortedPolygon == null)
                        Console.WriteLine("OK");
                    else Console.WriteLine("blad - niepusta tablica");
                    continue;
                }
                if (sortedPolygon == null)
                {
                    Console.WriteLine("blad - brak tablicy");
                    continue;
                }

                if (sortedPolygon.GetLength(0)!=tests[i].GetLength(0))
                {
                    Console.WriteLine("bledna dlugosc tablicy");
                    continue;
                }
                else
                    for (int j=0; j<sortedPolygon.GetLength(0); j++)
                        if ((sortedPolygon[j].x != resp[i][j].x)
                            || (sortedPolygon[j].y != resp[i][j].y))
                        {
                            Console.WriteLine("bledne dane w tablicy");
                            continue;
                        }
                Console.WriteLine("OK");
            }


        }



        public static void Test2()
        {
            List<Point[]> tests = new List<Point[]>();
            tests.Add(new Point[] { new Point(0, 4), new Point(2, 1), new Point(4, 3), new Point(5, 2), new Point(6, 1), new Point(7, 5), new Point(5, 6) });
            tests.Add(new Point[] { new Point(0, 3), new Point(2, 3), new Point(3, 2), new Point(4, 1), new Point(9, 1), new Point(7, 5), new Point(6, 4), new Point(5, 5) });
            tests.Add(new Point[] { new Point(0, 0), new Point(1, 1), new Point(2, 2), new Point(3, 1), new Point(4, 0), new Point(3, 0), new Point(2, 0), new Point(1, 0) });

            List<Triangle>[] resp = new List<Triangle>[3];   
            int[] len= new int[3];

            resp[0] = new List<Triangle>();
            resp[0].Add(new Triangle(new Point(0, 4), new Point(2, 1), new Point(4, 3)));
            resp[0].Add(new Triangle(new Point(0, 4), new Point(4, 3), new Point(5, 6)));
            resp[0].Add(new Triangle(new Point(4, 3), new Point(5, 6), new Point(5, 2)));
            resp[0].Add(new Triangle(new Point(5, 6), new Point(5, 2), new Point(6, 1)));
            resp[0].Add(new Triangle(new Point(5, 6), new Point(6, 1), new Point(7, 5)));    
            len[0]=5;

            resp[1] = new List<Triangle>();
            resp[1].Add(new Triangle(new Point(0, 3), new Point(2, 3), new Point(5, 5)));
            resp[1].Add(new Triangle(new Point(2, 3), new Point(3, 2), new Point(5, 5)));
            resp[1].Add(new Triangle(new Point(3, 2), new Point(4, 1), new Point(5, 5)));
            resp[1].Add(new Triangle(new Point(4, 1), new Point(5, 5), new Point(6, 4)));
            resp[1].Add(new Triangle(new Point(4, 1), new Point(6, 4), new Point(7, 5)));
            resp[1].Add(new Triangle(new Point(4, 1), new Point(7, 5), new Point(9, 1)));
            len[1] = 6;

            resp[2] = new List<Triangle>();
            resp[2].Add(new Triangle(new Point(0, 0), new Point(1, 0), new Point(1, 1)));
            resp[2].Add(new Triangle(new Point(1, 0), new Point(1, 1), new Point(2, 0)));
            resp[2].Add(new Triangle(new Point(1, 1), new Point(2, 0), new Point(2, 2)));
            resp[2].Add(new Triangle(new Point(2, 0), new Point(2, 2), new Point(3, 0)));
            resp[2].Add(new Triangle(new Point(2, 2), new Point(3, 0), new Point(3, 1)));
            resp[2].Add(new Triangle(new Point(3, 0), new Point(3, 1), new Point(4, 0)));
            len[2] = 6;
            
            
            
            Triangle[] triangulation;

            int res;

            for (int i=0; i<3; i++)
            {
                Console.WriteLine("\nTEST {0} ", i+1);

                res = triangulateMonotone(tests[i], out triangulation);

                Console.WriteLine(" Liczba trojkatow: {0}", res==len[i]?"OK":"bledna liczba");

                Console.Write("\n Znalezione trojkaty: ");

                if (triangulation == null)
                {
                    Console.WriteLine("blad - brak tablicy");
                    continue;
                }

                Console.Write("\n");

                foreach(Triangle t in triangulation)
                {
                    Console.WriteLine("{0} {1} {2}",t.a, t.b, t.c );
                }
                Console.WriteLine("\n Prawidlowa triangulacja: ");
                foreach (Triangle t in resp[i])
                {
                    Console.WriteLine("{0} {1} {2}", t.a, t.b, t.c);
                }
            }


        }



        public static void Test3()
        {
            List<Triangle>[] tests = new List<Triangle>[3];
            double[] ar = new double[3];

            tests[0] = new List<Triangle>();
            tests[0].Add(new Triangle(new Point(0, 0), new Point(0, 1), new Point(1, 0)));
            ar[0] = 0.5;
                       
            
            tests[1] = new List<Triangle>();
            tests[1].Add(new Triangle(new Point(0, 0), new Point(0, 1), new Point(1, 0)));
            tests[1].Add(new Triangle(new Point(1, 0), new Point(0, 1), new Point(1, 1)));
            ar[1] = 1;

            tests[2] = new List<Triangle>();
            tests[2].Add(new Triangle(new Point(0, 0), new Point(1, 0), new Point(1, 1)));
            tests[2].Add(new Triangle(new Point(1, 0), new Point(1, 1), new Point(2, 0)));
            tests[2].Add(new Triangle(new Point(1, 1), new Point(2, 0), new Point(2, 2)));
            tests[2].Add(new Triangle(new Point(2, 0), new Point(2, 2), new Point(3, 0)));
            tests[2].Add(new Triangle(new Point(2, 2), new Point(3, 0), new Point(3, 1)));
            tests[2].Add(new Triangle(new Point(3, 0), new Point(3, 1), new Point(4, 0)));
            ar[2] = 4;


            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("\nTEST {0} \n Pole: {1} powinno byc: {2}", i+1, polygonArea(tests[i].ToArray()), ar[i]);
            }

        }


        static void Main(string[] args)
        {
            Console.WriteLine("*** ZADANIE 1 ***\n");            
            Test1();

            Console.WriteLine("\n\n*** ZADANIE 2 ***\n");
            Test2();

            Console.WriteLine("\n\n*** ZADANIE 3 ***\n");
            Test3();

        }
    }
}
