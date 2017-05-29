/*
 * Created by SharpDevelop.
 * User: Jajko
 * Date: 2014-05-20
 * Time: 19:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace asd2_lab13
{
    class Program
    {
        struct QueryTest
        {
            public double px, py;
            public bool answer;
            public QueryTest(double px, double py, bool answer)
            {
                this.px = px;
                this.py = py;
                this.answer = answer;
            }

            public static bool DoTheTests(Polygon p, IEnumerable<QueryTest> tests)
            {
                bool ok = true;
                foreach (QueryTest qt in tests)
                {
                    bool okok = (qt.answer == p.ContainsPoint(qt.px, qt.py));
                    ok = ok && okok;
                    if (!okok)
                        Console.WriteLine("BLAD dla punktu ({0},{1}) (zwracane {2} zamiast {3})", qt.px, qt.py, !qt.answer, qt.answer);
                }
                return ok;
            }
        }

        class Tester
        {
            IEnumerable<QueryTest> q;
            Point[] p;

            public Tester(Point[] polygon, IEnumerable<QueryTest> queries)
            {
                p = polygon;
                q = queries;
            }

            public bool RunTests()
            {
                return QueryTest.DoTheTests(new Polygon(p), q);
            }
        }

        public static void Main(string[] args)
        {
            int n = 10;
            Point[] klucha = new Point[2 * n];
            QueryTest[] testyKluchy = new QueryTest[4 * n];
            for (int i = 0; i < n; i++)
            {
                klucha[i] = new Point(6 * n - 6 * i, 2);
                klucha[i + n] = new Point(6 * i + 3, 1);
            }
            for (int i = 0; i < 2 * n; i++)
            {
                testyKluchy[i] = new Program.QueryTest(3 * i + 5, 1.5, true);
                testyKluchy[i + 2 * n] = new Program.QueryTest(3 * i + 5, 2.5 - 2 * (i % 2), false);
            }
            testyKluchy[2 * n - 2].answer = false;
            testyKluchy[2 * n - 1] = new Program.QueryTest(3.5, 1.1, true);

            Point[] glizda = new Point[2 * n];
            QueryTest[] testyGlizdy = new Program.QueryTest[2 * n];
            for (int i = 0; i < n; i++)
            {
                glizda[i] = new Point(3 * i + 2, 4 * n * i);
                glizda[2 * n - i - 1] = new Point(3 * i, 4 * n * i);
                if (i % 2 == 1)
                {
                    glizda[i].x += 4 * n;
                    glizda[2 * n - i - 1].x += 4 * n;
                }
            }
            for (int i = 0; i < n; i++)
            {
                if (i % 2 == 0)
                {
                    testyGlizdy[i] = new Program.QueryTest(3 * i + 1, 4 * n * i + 0.1, true);
                    testyGlizdy[i + n] = new Program.QueryTest(3 * i + 1, 4 * n * i + 1.1, false);
                }
                else
                {
                    testyGlizdy[i] = new Program.QueryTest(3 * i + 1 + 4 * n, 4 * n * i - 0.1, true);
                    testyGlizdy[i + n] = new Program.QueryTest(3 * i + 1 + 4 * n, 4 * n * i - 1.1, false);
                }
            }

            Tester[] simpleTests = {
                new Tester( new Point[]{
                               new Point(5,5),
                               new Point(-3,-1),
                               new Point(5,-1)
                           }, new QueryTest[]{
                               new QueryTest(-2,-0.5,true),
                               new QueryTest(2,-10, false),
                               new QueryTest(2, 10, false)
                           }),
                new Tester(klucha, testyKluchy),
                new Tester(glizda, testyGlizdy)
            };

            Tester[] advancedTests = {
                new Tester(new Point[]{
                               new Point(5,5),
                               new Point(-3,-1),
                               new Point(5,-1)
                           },new QueryTest[]{
                               new QueryTest(5,5,true),
                               new QueryTest(-3,-1,true),
                               new QueryTest(5,-1,true),
                               new QueryTest(-5,-1,false),
                               new QueryTest(1,2,true),
                               new QueryTest(1,-1,true),
                               new QueryTest(5,6,false),
                               new QueryTest(5,3,true),
                               new QueryTest(5,-3,false),
                               new QueryTest(6,5,false)
                           })
            };

            Tester[] nastyTests = {
                new Tester(new Point[]{
                               new Point(10,10),
                               new Point(0,9),
                               new Point(20,8),
                               new Point(0,7),
                               new Point(1,0),
                               new Point(2,7),
                               new Point(3,0),
                               new Point(4,7),
                               new Point(5,0),
                               new Point(6,7),
                               new Point(7,0),
                               new Point(8,7),
                               new Point(25,8),
                               new Point(10,20),
                               new Point(20,9),
                               new Point(0,20),
                               new Point(1,15),
                               new Point(2,14),
                               new Point(3,13),
                               new Point(4,12),
                               new Point(5,11),
                               new Point(6,10)
                           },new QueryTest[]{
                               new QueryTest(0,9,true),
                               new QueryTest(2,4,false),
                               new QueryTest(3,5,true),
                               new QueryTest(4,5,false),
                               new QueryTest(5,9,true),
                               new QueryTest(5,10,false),
                               new QueryTest(10,10,true),
                               new QueryTest(10,20,true),
                               new QueryTest(10,25,false),
                               new QueryTest(10,19,false),
                               new QueryTest(10,13,true),
                               new QueryTest(10,8,false),
                               new QueryTest(25,8,true),
                               new QueryTest(25,8.5,false),
                               new QueryTest(25.5,8,false),
                               new QueryTest(3.1,12.9,true),
                               new QueryTest(4.1,11.9,true),
                               new QueryTest(4.1,11.8,false)
                           })
            };

            Console.WriteLine("Podstawowe testy:\n");
            runTestArray(simpleTests);

            Console.WriteLine("\n\nZaawansowane testy:\n");
            runTestArray(advancedTests);

            Console.WriteLine("\n\nZlosliwe testy:\n");
            runTestArray(nastyTests);

            Console.WriteLine("\n\nTesty wydajnosci (poprawnosc nie sprawdzana): ");
            int rozm = 1250000;
            Polygon p = new Polygon(makeSpiralPolygon(rozm));
            Console.WriteLine("\tSkonstruowano wielokat");

            int tests = 10000;
            Random rand = new Random(13);
            for (int i = 0; i < tests; i++)
                for (int j = 0; j < tests; j++)
                {
                    p.ContainsPoint(i + 0.5, j + 0.5);
                }

            Console.WriteLine("\tZakonczono testy\n");
        }

        static Point[] makeSpiralPolygon(int n)
        {
            Point[] ret = new Point[2 * n];
            int t = 0;
            int jump = 3;
            int x = 0;
            int y = 0;
            for (int i = 0; i < n; i++)
            {
                ret[t] = new Point(x, y);
                switch (i % 4)
                {
                    case 0:
                        ret[2 * n - t - 1] = new Point(x + 1, y + 1);
                        x += jump;
                        break;
                    case 1:
                        ret[2 * n - t - 1] = new Point(x - 1, y + 1);
                        y += jump;
                        break;
                    case 2:
                        ret[2 * n - t - 1] = new Point(x - 1, y - 1);
                        x -= jump;
                        break;
                    case 3:
                        ret[2 * n - t - 1] = new Point(x + 1, y - 1);
                        y -= jump;
                        break;
                }
                jump++;
            }
            return ret;
        }

        static bool runTestArray(Tester[] tests)
        {
            bool allok = true;
            int i = 0;
            foreach (Tester t in tests)
            {
                Console.Write("\tTest {0}: ", i++);
                bool res = t.RunTests();
                if (res)
                    Console.WriteLine("ok");
                allok = allok && res;
            }
            return allok;
        }
    }
}