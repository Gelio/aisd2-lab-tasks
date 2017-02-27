using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGreedyFish
{


    class Program
    {
        static string GetDirectionName(int i)
        {
            switch (i)
            {
                case 0: return "lewo";
                case 1: return "lewo-gora";
                case 2: return "prawo-gora";
                case 3: return "prawo";
                case 4: return "prawo-dol";
                case 5: return "lewo-dol";
            }

            return "";
        }

        static void ShowMoves(Move[] moves)
        {
            if (moves == null)
            {
                return;
            }
            string res = "";
            for (int i = 0; i < moves.Length; ++i)
            {
                res += moves[i].ToString() + "\n";
            }
            Console.WriteLine("\nRuchy\n{0}", res);
        }



        class TestCase
        {
            public int fishResult;
            public int[,] grid;
            public List<Point> penguins;
            public Move[] moves;

            public TestCase(int fishResult, int[,] grid, List<Point> penguins = null, Move[] moves = null)
            {
                this.fishResult = fishResult;
                this.grid = grid;
                this.penguins = penguins;
                this.moves = moves;
            }

            public bool CheckResult(int testNo, int stFishResult, Move[] stMoves = null)
            {
                bool result = (stFishResult == this.fishResult);

                Console.WriteLine("Test #{2} Wynik: {0} {1}", stFishResult, result ? "OK!" : "Probuj dalej! :) Prawidlowy wynik to: " + this.fishResult.ToString(), testNo);

                if(this.moves == null && stMoves == null || this.moves == null && (stMoves != null && stMoves.Length == 0))
                {
                    Console.WriteLine("Test #{0} Tablica ruchow jest pusta lub null. Dobrze.", testNo);
                    if (result)
                    {
                        Console.WriteLine("Test #{0} Wszystko dobrze. Brawo! :)", testNo);
                    }
                    return result && true;
                }
                else if(this.moves == null && stMoves != null || this.moves != null && stMoves == null)
                {
                    Console.WriteLine("Test #{0} Nie zgadza sie dlugosc tablicy (sprawdzanie wg null)", testNo);
                    result = false;
                    return result;
                }

                if(this.moves.Length != stMoves.Length)
                {
                    Console.WriteLine("Test #{0} Nie zgadza sie liczba ruchow (dlugosc tablicy)", testNo);
                    result = false;
                    return result;
                }

                for (int i = 0; i < stMoves.Length; ++i)
                {
                    Move m = this.moves[i];
                    Move stM = stMoves[i];
                    if(m.from.x != stM.from.x || m.from.y != stM.from.y)
                    {
                        Console.WriteLine("Test #{0} Ruch #{5} Nie zgadza sie punkt from. Jest ({1}, {2}), a powinno byc ({3}, {4})", testNo, stM.from.x, stM.from.y, m.from.x, m.from.y, i);
                        result = false;
                        return result;
                    }
                    if(m.to.x != stM.to.x || m.to.y != stM.to.y)
                    {
                        Console.WriteLine("Test #{0} Ruch #{5} Nie zgadza sie punkt to. Jest ({1}, {2}), a powinno byc ({3}, {4})", testNo, stM.to.x, stM.to.y, m.to.x, m.to.y, i);
                        result = false;
                        return result;
                    }
                    if(m.fish != stM.fish)
                    {
                        Console.WriteLine("Test #{0} Ruch #{3} Nie zgadza sie liczba ryb. Jest {1}, a powinno byc {2}", testNo, stM.fish, m.fish, i);
                        result = false;
                        return result;
                    }
                    if(m.penguinId != stM.penguinId)
                    {
                        Console.WriteLine("Test #{0} Ruch #{3} Nie zgadza sie indeks pingwina. Jest {1}, a powinno byc {2}", testNo, stM.penguinId, m.penguinId, i);
                        result = false;
                        return result;
                    }
                }

                if (result)
                {
                    Console.WriteLine("Test #{0} Wszystko dobrze. Brawo! :)", testNo);
                }

                return result;

            }

        }

        static void Main(string[] args)
        {
            bool showMoves = false; //wyświetla ruchy z tablicy moves

            bool showBoard = false; //wyświetla tablicę i pingwiny





            Console.WriteLine("Hej, to moja ryba!\n");

            Move[] moves1, moves2;

            List<TestCase> testCases1 = new List<TestCase>();



            testCases1.Add(new TestCase(7, new int[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } }, new List<Point> { new Point(0, 0) }, new Move[] { new Move(new Point(0, 0), new Point(0, 1), 1, 0), new Move(new Point(0, 1), new Point(0, 2), 1, 0), new Move(new Point(0, 2), new Point(1, 2), 1, 0), new Move(new Point(1, 2), new Point(1, 1), 1, 0), new Move(new Point(1, 1), new Point(1, 0), 1, 0), new Move(new Point(1, 0), new Point(2, 1), 1, 0), new Move(new Point(2, 1), new Point(2, 0), 1, 0) }));
            testCases1.Add(new TestCase(8, new int[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } }, new List<Point> { new Point(1, 1) }, new Move[] {new Move(new Point(1, 1), new Point(1, 0), 1, 0),new Move(new Point(1, 0), new Point(0,
0), 1, 0),new Move(new Point(0, 0), new Point(0, 1), 1, 0),new Move(new Point(0, 1), new Point(0, 2)
, 1, 0),new Move(new Point(0, 2), new Point(1, 2), 1, 0),new Move(new Point(1, 2), new Point(2, 2),
1, 0),new Move(new Point(2, 2), new Point(2, 1), 1, 0),new Move(new Point(2, 1), new Point(2, 0), 1,
 0)}));
            testCases1.Add(new TestCase(7, new int[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 2, 1, 1 } }, new List<Point> { new Point(0, 0), new Point(2, 2) }, new Move[] {new Move(new Point(2, 2), new Point(2, 0), 1, 1),new Move(new Point(0, 0), new Point(0,
1), 1, 0),new Move(new Point(0, 1), new Point(0, 2), 1, 0),new Move(new Point(0, 2), new Point(1, 2)
, 1, 0),new Move(new Point(1, 2), new Point(1, 1), 1, 0),new Move(new Point(1, 1), new Point(1, 0),
1, 0),new Move(new Point(1, 0), new Point(2, 1), 1, 0)}));
            testCases1.Add(new TestCase(14, new int[,] { { 1, 2, 0, 3 }, { 1, 2, 0, 3 }, { 1, 2, 0, 3 } }, new List<Point> { new Point(0, 0), new Point(2, 3) }, new Move[] {new Move(new Point(2, 3), new Point(1, 3), 3, 1),new Move(new Point(1, 3), new Point(0,
3), 3, 1),new Move(new Point(0, 0), new Point(0, 1), 1, 0),new Move(new Point(0, 1), new Point(1, 1)
, 2, 0),new Move(new Point(1, 1), new Point(2, 1), 2, 0),new Move(new Point(2, 1), new Point(2, 0),
2, 0),new Move(new Point(2, 0), new Point(1, 0), 1, 0)}));

            testCases1.Add(new TestCase(18, new int[,] { { 1, 0, 2, 0, 3},
                                                         { 1, 3, 0, 2, 0 },
                                                         { 1, 0, 3, 1, 0 },
                                                         { 1, 1, 1, 1, 1 },
                                                         { 1, 0, 0, 0, 0}}, new List<Point> { new Point(2, 2), new Point(4, 0), new Point(0, 4) }, new Move[] {new Move(new Point(2, 2), new Point(1, 1), 3, 0),new Move(new Point(1, 1), new Point(0,
2), 3, 0),new Move(new Point(0, 4), new Point(1, 3), 3, 2),new Move(new Point(4, 0), new Point(3, 0)
, 1, 1),new Move(new Point(3, 0), new Point(2, 0), 1, 1),new Move(new Point(2, 0), new Point(1, 0),
1, 1),new Move(new Point(1, 0), new Point(0, 0), 1, 1),new Move(new Point(1, 3), new Point(2, 3), 2,
 2),new Move(new Point(2, 3), new Point(3, 3), 1, 2),new Move(new Point(3, 3), new Point(3, 2), 1, 2
),new Move(new Point(3, 2), new Point(3, 1), 1, 2)}));

            testCases1.Add(new TestCase(0, new int[,] { { 2, 2, 2, 2, 2},
                                                         { 1, 0, 0, 0, 1 },
                                                         { 1, 0, 3, 0, 0 },
                                                         { 1, 0, 0, 0, 1 },
                                                         { 1, 3, 3, 3, 3}}, new List<Point> { new Point(2, 2) }));
            testCases1.Add(new TestCase(0, new int[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } }));

            Console.WriteLine("--------------------");
            Console.WriteLine("Algorytm nr 1\n");

            for (int i = 0; i < testCases1.Count; ++i)
            {
                Board b1 = new Board();
                b1.SetBoard(testCases1[i].grid, testCases1[i].penguins);
                if (showBoard)
                {
                    Console.WriteLine(b1.ToString());
                }
                int result = b1.GreedyAlgorithm1(out moves1);
                if (showMoves)
                {
                    ShowMoves(moves1);
                }

                testCases1[i].CheckResult(i, result, moves1);
            }

            Console.WriteLine("--------------------");

            List<TestCase> testCases2 = new List<TestCase>();
            testCases2.Add(new TestCase(8, new int[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } }, new List<Point> { new Point(0, 0) }, new Move[] {new Move(new Point(0, 0), new Point(0, 2), 1, 0),new Move(new Point(0, 2), new Point(0,
1), 1, 0),new Move(new Point(0, 1), new Point(2, 2), 1, 0),new Move(new Point(2, 2), new Point(2, 0)
, 1, 0),new Move(new Point(2, 0), new Point(1, 0), 1, 0),new Move(new Point(1, 0), new Point(1, 2),
1, 0),new Move(new Point(1, 2), new Point(1, 1), 1, 0),new Move(new Point(1, 1), new Point(2, 1), 1,
 0)}));
            testCases2.Add(new TestCase(4, new int[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } }, new List<Point> { new Point(1, 1) }, new Move[] {new Move(new Point(1, 1), new Point(1, 0), 1, 0),new Move(new Point(1, 0), new Point(0,
0), 1, 0),new Move(new Point(0, 0), new Point(0, 2), 1, 0),new Move(new Point(0, 2), new Point(0, 1)
, 1, 0)}));
            testCases2.Add(new TestCase(6, new int[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 2, 1, 1 } }, new List<Point> { new Point(0, 0), new Point(2, 2) }, new Move[] {new Move(new Point(2, 2), new Point(2, 0), 1, 1),new Move(new Point(0, 0), new Point(0,
2), 1, 0),new Move(new Point(0, 2), new Point(0, 1), 1, 0),new Move(new Point(0, 1), new Point(1, 1)
, 1, 0),new Move(new Point(1, 1), new Point(1, 0), 1, 0),new Move(new Point(1, 0), new Point(2, 1),
1, 0)}));
            testCases2.Add(new TestCase(14, new int[,] { { 1, 2, 0, 3 }, { 1, 2, 0, 3 }, { 1, 2, 0, 3 } }, new List<Point> { new Point(0, 0), new Point(2, 3) }, new Move[] {new Move(new Point(2, 3), new Point(1, 3), 3, 1),new Move(new Point(1, 3), new Point(0,
3), 3, 1),new Move(new Point(0, 0), new Point(0, 1), 1, 0),new Move(new Point(0, 1), new Point(1, 1)
, 2, 0),new Move(new Point(1, 1), new Point(2, 1), 2, 0),new Move(new Point(2, 1), new Point(2, 0),
2, 0),new Move(new Point(2, 0), new Point(1, 0), 1, 0)}));
            testCases2.Add(new TestCase(18, new int[,] { { 1, 0, 2, 0, 3},
                                                         { 1, 3, 0, 2, 0 },
                                                         { 1, 0, 3, 1, 0 },
                                                         { 1, 1, 1, 1, 1 },
                                                         { 1, 0, 0, 0, 0}}, new List<Point> { new Point(2, 2), new Point(4, 0), new Point(0, 4) }, new Move[] {new Move(new Point(2, 2), new Point(1, 1), 3, 0),new Move(new Point(1, 1), new Point(0,
2), 3, 0),new Move(new Point(4, 0), new Point(3, 0), 1, 1),new Move(new Point(3, 0), new Point(2, 0)
, 1, 1),new Move(new Point(2, 0), new Point(1, 0), 1, 1),new Move(new Point(1, 0), new Point(0, 0),
1, 1),new Move(new Point(0, 4), new Point(3, 2), 3, 2),new Move(new Point(3, 2), new Point(1, 3), 1,
 2),new Move(new Point(1, 3), new Point(2, 3), 2, 2),new Move(new Point(2, 3), new Point(3, 3), 1, 2
),new Move(new Point(3, 3), new Point(3, 4), 1, 2)}));
            testCases2.Add(new TestCase(0, new int[,] { { 2, 2, 2, 2, 2},
                                                         { 1, 0, 0, 0, 1 },
                                                         { 1, 0, 3, 0, 0 },
                                                         { 1, 0, 0, 0, 1 },
                                                         { 1, 3, 3, 3, 3}}, new List<Point> { new Point(2, 2) }));
            testCases2.Add(new TestCase(0, new int[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } }));

            Console.WriteLine("--------------------");
            Console.WriteLine("Algorytm nr 2\n");

            for (int i = 0; i < testCases2.Count; ++i)
            {
                Board b2 = new Board();
                b2.SetBoard(testCases2[i].grid, testCases2[i].penguins);
                if (showBoard)
                {
                    Console.WriteLine(b2.ToString());
                }
                int result = b2.GreedyAlgorithm2(out moves2);
                if (showMoves)
                {
                    ShowMoves(moves2);
                }

                testCases2[i].CheckResult(i, result, moves2);
            }

            Console.WriteLine("--------------------");
        }
    }
}
