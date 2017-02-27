using System;
using System.Collections.Generic;

namespace discs
{
    class Program
    {
        public const double epsilon = 10e-6;

        class ITTest
            {
            public Disk d1,d2;
            public IntersectionType type;
            public Point[] cp;
            public ITTest(Point p1, double r1, Point p2, double r2, IntersectionType t, Point[] cp)
                { this.d1=new Disk(p1,r1); this.d2=new Disk(p2,r2); this.type=t; this.cp=cp; }
            }

        private static void IntersectionTypeTests()
            {
            ITTest[] examples = new ITTest[] {
                                             new ITTest(new Point(-1,2), 3, new Point(30,-10), 10, IntersectionType.Disjoint, new Point[0]) ,                                 // 1 - rozlaczne
                                             new ITTest(new Point(4,3), 5,  new Point(16,12), 10, IntersectionType.Touches, new Point[] { new Point(8,6) } ) ,                // 2 - styczne zewnetrznie
                                             new ITTest(new Point(1,1), 5,  new Point(9,1), 5, IntersectionType.Crosses, new Point[] { new Point(5,4), new Point(5,-2) } ) ,  // 3 - przecinaja sie
                                             new ITTest(new Point(1,1), 5,  new Point(5,1), 3, IntersectionType.Crosses, new Point[] { new Point(5,4), new Point(5,-2) } ) ,  // 4 - przecinaja sie
                                             new ITTest(new Point(2,1), 10, new Point(6,4), 5, IntersectionType.Contains, new Point[0] ) ,                                 // 5 - pierwsze zawiera w sobie drugie (stycznie)
                                             new ITTest(new Point(-5,2), 20, new Point(2,4), 2, IntersectionType.Contains, new Point[0]) ,                                 // 6 - pierwsze zawiera w sobie drugie
                                             new ITTest(new Point(-4,3), 3, new Point(1,-2), 25, IntersectionType.IsContained, new Point[0]) ,                             // 7 - pierwsze zawiera sie w drugim
                                             new ITTest(new Point(1,-2), 4, new Point(1,-2), 4, IntersectionType.Identical, new Point[0]) ,                                // 8 - pokrywaja sie
                                             };
                                             
            Point[] cp;
            IntersectionType type;
            Console.WriteLine("\nTesty przecinania sie dwoch okregow");
            for ( int i=0 ; i<examples.Length ; ++i )
                {
                Console.WriteLine("\nTest {0}",i+1);
                try
                    {
                    type=examples[i].d1.GetIntersectionType(examples[i].d2,out cp);
                    if ( type!=examples[i].type )
                        {
                        Console.WriteLine("Nieprawidlowy typ przeciecia");
                        continue;
                        }
                    switch ( type )
                        {
                        case IntersectionType.Disjoint:
                        case IntersectionType.Contains:
                        case IntersectionType.IsContained:
                        case IntersectionType.Identical:
                            if ( cp==null || cp.Length!=0 )
                                {
                                Console.WriteLine("Nieprawidlowa tablica punktow przeciecia");
                                continue;
                                }
                            break;
                        case IntersectionType.Touches:
                            if ( cp==null || cp.Length!=1 )
                                {
                                Console.WriteLine("Nieprawidlowa tablica punktow przeciecia");
                                continue;
                                }
                            if ( !cp[0].Equals(examples[i].cp[0]) )
                                {
                                Console.WriteLine("Nieprawidlowy punkt przeciecia");
                                continue;
                                }
                            break;
                        case IntersectionType.Crosses:
                            if ( cp==null || cp.Length!=2 )
                                {
                                Console.WriteLine("Nieprawidlowa tablica punktow przeciecia");
                                continue;
                                }
                            if ( !cp[0].Equals(examples[i].cp[0]) || !cp[1].Equals(examples[i].cp[1]) )
                                {
                                Console.WriteLine("Nieprawidlowe punkty przeciecia");
                                continue;
                                }
                            break;
                        }
                    Console.WriteLine("OK");
                    }
                catch ( Exception e )
                    {
                    Console.WriteLine(e.Message);
                    }
                }

            }

        static void Main(string[] args)
        {

            IntersectionTypeTests();

            Console.WriteLine("\n\nTesty glowne");

            Disk[] disks;
            //////// Test 1 - 1 koło //////////

            disks = new Disk[] { new Disk(new Point(0, 0), 1) };
            Test(1, disks, true);
            
            ///// Test 2 - dużo równych kół //////////

            disks = new Disk[] { 
                new Disk(new Point(0, 0),1),
                new Disk(new Point(0, 0),1),
                new Disk(new Point(0, 0),1),
                new Disk(new Point(0, 0),1),
                new Disk(new Point(0, 0),1),
                new Disk(new Point(0, 0),1),
                new Disk(new Point(0, 0),1),
                new Disk(new Point(0, 0),1),
                new Disk(new Point(0, 0),1)
            };
            Test(2, disks, true);

            ///// Test 3 - normalny przypadek, jest rozwiązanie ///////////////

            disks = new Disk[] { 
                new Disk(new Point(-1, 0),1.5),
                new Disk(new Point(1, 0),1.5),
                new Disk(new Point(0, 2),2.5),

            };
            Test(3, disks, true);

            //// Test 4 - dwa koła rozłączne i trzecie je przecina //////////////////

            disks = new Disk[] { 
                new Disk(new Point(-2, 0),1.5),
                new Disk(new Point(2, 0),1.5),
                new Disk(new Point(0, 2),5),

            };
            Test(4, disks, false);


            ////// Test 5 - trzy koła przecinające się parami, brak rozwiązania ////////

            disks = new Disk[] { 
                new Disk(new Point(0, 0),0.55),
                new Disk(new Point(1, 0),0.55),
                new Disk(new Point(0.5, 0.86),0.55),

            };
            Test(5, disks, false);

            ////// Test 6 - tak jak 5, ale jedno koło jest zwielokrotnione ////////

            disks = new Disk[] { 
                new Disk(new Point(0, 0),0.55),
                new Disk(new Point(1, 0),0.55),
                new Disk(new Point(0.5, 0.86),0.55),
                new Disk(new Point(0.5, 0.86),0.55),
                new Disk(new Point(0.5, 0.86),0.55),
                new Disk(new Point(0.5, 0.86),0.55),
                new Disk(new Point(0.5, 0.86),0.55),

            };
            Test(6, disks, false);

            ////// Test 7 - byłoby rozwiązanie, gdyby nie dwa równe koła ////////

            disks = new Disk[] { 
                new Disk(new Point(0, 0),0.55),
                new Disk(new Point(1, 0),0.55),
                new Disk(new Point(5, 5),1),
                new Disk(new Point(5, 5),1),

            };
            Test(7, disks, false);


            ////// Test 8 - pozytywny test wydajnościowy: dużo kół, jest rozwiązanie ////////

            int n = 100;
            disks = new Disk[n];
            Random rnd = new Random();

            for (int i = 0; i < n; ++i)
            {
                double x = rnd.NextDouble();
                double y = rnd.NextDouble();

                disks[i] = new Disk(new Point(x, y), 1);
            }
            Test(8, disks, true);

            ////// Test 9 - negatywny test wydajnościowy: dużo istotnie różnych kół, nie ma rozwiązania (nietrywialne) ////////

            List<Disk> listD = new List<Disk>();

            listD.Add(new Disk(new Point(0, 0), 55));
            listD.Add(new Disk(new Point(50, 86), 55));

            for (int i = 2; (i-2)/100.0 < 10; ++i)
                listD.Add(new Disk(new Point(100.0 + (i-2)/100.0,0), 55));

            Test(9, listD.ToArray(), false);

            listD.Clear();

            ////// Test 10 - nieopisany

            n=300;
            double a=(2*Math.PI)/n;
            for ( int i=0 ; i<n ; ++i )
                listD.Add(new Disk(new Point(1+Math.Cos(i*a)*0.1,2+Math.Sin(i*a)*0.1),10.1));
            Test(10, listD.ToArray(), true);

            ////// Test 11 - nieopisany

            listD.Add(new Disk(new Point(12.5,12),10.1));
            listD.Add(new Disk(new Point(12.5,-8),10.1));
            Test(11, listD.ToArray(), false);

            disks=new Disk[] {
                new Disk(new Point(0, 0),1),
                new Disk(new Point(0, 0),2),
                new Disk(new Point(0, 0),3),
            };
            Test(12,disks,true);

            disks = new Disk[] {
                new Disk(new Point(0, 0),3),
                new Disk(new Point(0, 0),2),
                new Disk(new Point(0, 0),1),
            };
            Test(13, disks, true);

            Console.WriteLine();

        }

        private static void Test(int num, Disk[] disks, bool isSolution)
        {
//            Console.WriteLine("\n\nTest {0} \n--------------------------\n", num);
            Console.WriteLine("\nTest {0}", num);

            DateTime start = DateTime.Now;
            Point? solution = IntersectionFinder.FindCommonPoint(disks);
            DateTime stop = DateTime.Now;

            if (solution == null && !isSolution)
                Console.WriteLine("Nie znaleziono rozwiązania, i bardzo dobrze, bo nie ma.");
            else if (solution == null && isSolution)
                Console.WriteLine("Nie znaleziono rozwiązania, a szkoda, bo jest -- BŁĄD.");

            if (solution != null)
            {
                Console.WriteLine("Znaleziono punkt: {0}.", solution);
                bool ok = true;
                for (int i = 0; i < disks.Length; ++i)
                    if (!disks[i].Contains(solution.Value))
                    {
                        ok = false;
                        Console.WriteLine("BŁĄD: punkt nie należy do koła {0} o środku w {1} i promieniu {2}.",i, disks[i].Center, disks[i].Radius);
                    }
                if (ok)
                    Console.WriteLine("Jest to poprawne rozwiązanie.");
            }

            Console.WriteLine("Czas obliczeń to {0}ms", (stop - start).TotalMilliseconds);

           
        }
    }
}
