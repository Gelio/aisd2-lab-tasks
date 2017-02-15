using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace ASD
{
    class Program
    {
        static void Main(string[] args)
        {
            TriangleTest();

            SeamCarvinTest();

            ResizeBitmap();
        }

        static void TriangleTest()
        {

            #region TestDefinition
            List<TriangleTestData> tests = new List<TriangleTestData>();
            tests.Add(new TriangleTestData()
            {
                Tab = new[] {new[] { 8, 9, 5, 3 },new[] { 2, 4, 7 },new[] { 8, 5 }, new[] { 3 }},
                MinPath = 17,
                PathsCount = 1
            });
            tests.Add(new TriangleTestData()
            {
                Tab = new[] { new[] { 8, 5, 9, 3 },new[] { 2, 4, 6 }, new[] { 7, 5 },new[] { 3 } },
                MinPath = 17,
                PathsCount = 3
            });
            Random r = new Random(567);
            int[][] t3 = new int[1000][];
            for (int y = 0; y < 1000; y++)
            {
                t3[y] = new int[1000-y + 1];
                for (int x = 0; x <= 1000-y; x++)
                    t3[y][x] = r.Next(1000);
            }

            tests.Add(new TriangleTestData()
            {
                Tab = t3,
                MinPath = 248630,
                PathsCount = 2
            });

            int[][] t4 = new int[5][];
            for (int y = 0; y < 5; y++)
            {
                t4[y] = new int[5- y + 1];
                for (int x = 0; x <= 5-y; x++)
                    t4[y][x] = 1;
            }
            tests.Add(new TriangleTestData()
            {
                Tab = t4,
                MinPath = 5,
                PathsCount = 16
            });

            #endregion

            List<int[]> result;

            Console.WriteLine("Triangle Path test: ");
            for (int i = 0; i < tests.Count; i++)
            {
                int minPath = TrianglePath.FindPath(tests[i].Tab, out result);
                Console.WriteLine("Test {0} : {1}", i + 1, tests[i].MinPath == minPath && tests[i].PathsCount == result.Count ? "OK" : "BŁĄD");
                if (i == 0 && result.Count == 1)
                {
                    for (int j = 0; j < result[0].Length; j++)
                        Console.Write("{0} ", result[0][j]);
                    Console.WriteLine(" - Powinno być: 2 1 1 0");
                }
            }
            Console.WriteLine();
        }

        static void SeamCarvinTest()
        {
            #region TestDefinition
            List<SeamCarvinTestData> tests = new List<SeamCarvinTestData>();
            tests.Add(new SeamCarvinTestData()
            {
                Tab = new int[,]{{1,2,3,1},{2,3,6,8},{8,6,5,1},{9,5,6,2}},
                MinPath = 10,
                Path = new int[] { 3, 2, 3, 3 }

            });

            tests.Add(new SeamCarvinTestData()
            {
                Tab = new int[,] {{1,2,5,12},{2,3,7,11},{3,4,11,2},{4,5,15,3},{99,99,19,1}},
                MinPath = 15,
                Path = new int[] { 1, 2, 3, 3, 3 }

            });

            tests.Add(new SeamCarvinTestData()
            {
                Tab = new int[,] { { 1, 2, 2, 2, 2 }, { 2, 1, 2, 2, 2 }, { 2, 2, 1, 2, 2 }, { 2, 2, 2, 1, 2 }, { 2, 2, 2, 2, 1 } },
                MinPath = 5,
                Path = new int[] { 0, 1, 2, 3, 4 }

            });
            Random r = new Random(99);
            int[,] t4 = new int[1000, 500];
            for(int x=0;x<1000;x++)
                for (int y = 0; y < 500; y++)
                {
                    t4[x, y] = r.Next(1000);
                }
            tests.Add(new SeamCarvinTestData()
            {
                Tab = t4,
                MinPath = 176503,
                Path = null

            });

            #endregion

            int[] result;
            Console.WriteLine("Seam Carvin tests: ");
            for (int i = 0; i < tests.Count; i++)
            {
                int minPath = SeamCarvin.CalculateSeam(tests[i].Tab, out result);
                Console.WriteLine("Test {0} : {1}", i + 1, tests[i].MinPath == minPath && (tests[i].Path == null || AreArraysEqual(tests[i].Path, result)) ? "OK" : "BŁĄD");
                if (i == 0 && result!=null && minPath == tests[i].MinPath)
                {
                 for (int j = 0; j < result.Length; j++)
                        Console.Write("{0} ", result[j]);
                 Console.WriteLine(" - Powinno być: 3 2 3 3");
                
                }
            }
            Console.WriteLine();

        }
         
        static void ResizeBitmap()
        {
            
            const int  STEPS = 50;
            try
            {
                Console.WriteLine("Resize Bitmap Test");
                Bitmap bmp = (Bitmap)Bitmap.FromFile("image.jpg");
                int[] seam;
                for (int i = 0; i < STEPS; i++)
                {
                    int[,] energy = SeamCarvin.ComputeEnergyTable(bmp);
                    SeamCarvin.CalculateSeam(energy, out seam);
                    bmp = SeamCarvin.ScaleBitmap(bmp, seam);
                }
                bmp.Save("image_2.bmp");
                Console.WriteLine("Resize Bitmap: Done");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Brak plik image.jpg. Proszę wgrać go do katalogu bin/Debug lub bin/Release.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Resize Bitmap error: {0}", e.Message);
            }
        }

        private static bool AreArraysEqual(int[] t1, int[] t2)
        {
            if (t1 == null || t2 == null) return false;
            if (t1.Length != t2.Length) return false;
            for (int i = 0; i < t1.Length; i++)
                if (t1[i] != t2[i]) return false;
            return true;
        }

        #region Test Classes
        private class TriangleTestData
        {
            public int[][] Tab { get; set; }
            public int MinPath { get; set; }
            public int PathsCount { get; set; }

        }

        private class SeamCarvinTestData
        {
            public int[,] Tab { get; set; }
            public int MinPath { get; set; }
            public int[] Path { get; set; }

        }
        #endregion
    }
}
