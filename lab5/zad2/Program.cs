using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiSD_Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            var tests = new[] {
                new Test()
                {
                    No = 0,
                    Width = 2,
                    Height = 2,
                    SideLength = 100,
                    ShortcutLength = 141,
                    Shortcuts = new Location[0],
                    ExpectedResult = 200,
                    ExpectedNoOfPaths = 2
                },
                new Test()
                {
                    No = 1,
                    Width = 3,
                    Height = 3,
                    SideLength = 100,
                    ShortcutLength = 141,
                    Shortcuts = new Location[0],
                    ExpectedResult = 400,
                    ExpectedNoOfPaths = 6
                },
                new Test()
                {
                    No = 2,
                    Width = 4,
                    Height = 3,
                    SideLength = 100,
                    ShortcutLength = 141,
                    Shortcuts = new[] { new Location(0, 0), new Location(2, 1), new Location(0, 1) },
                    ExpectedResult = 382,
                    ExpectedNoOfPaths = 1
                },
                new Test()
                {
                    No = 3,
                    Width = 10,
                    Height = 10,
                    SideLength = 100,
                    ShortcutLength = 141,
                    Shortcuts = new[] 
                        { 
                            new Location(0, 0), 
                            new Location(1, 1), 
                            new Location(2, 2), 
                            new Location(3, 3), 
                            new Location(4, 4), 
                            new Location(5, 5), 
                            new Location(6, 6), 
                            new Location(7, 7),
                            new Location(8, 8),
                        },
                    ExpectedResult = 1269,
                    ExpectedNoOfPaths = 1
                },
                new Test()
                {
                    No = 4,
                    Width = 10,
                    Height = 10,
                    SideLength = 100,
                    ShortcutLength = 141,
                    Shortcuts = new[] { 
                        new Location(0, 0), 
                        new Location(2, 1), 
                        new Location(2, 2),
                        new Location(3, 4), 
                        new Location(5, 5), 
                        new Location(7, 1),
                        new Location(1, 5), 
                        new Location(1, 4), 
                    },
                    ExpectedResult = 1564,
                    ExpectedNoOfPaths = 60
                },
                new Test()
                {
                    No = 5,
                    Width = 10,
                    Height = 10,
                    SideLength = 100,
                    ShortcutLength = 141,
                    Shortcuts = new[] { 
                        new Location(0, 0), 
                        new Location(2, 1), 
                        new Location(2, 2),
                        new Location(2, 3), 
                        new Location(2, 5), 
                        new Location(2, 8),
                        new Location(3, 4), 
                        new Location(3, 7), 
                        new Location(3, 9),
                        new Location(4, 7), 
                        new Location(4, 8), 
                        new Location(5, 2),
                        new Location(5, 3), 
                        new Location(5, 5), 
                        new Location(5, 8),
                        new Location(6, 4), 
                        new Location(8, 8), 
                        new Location(7, 8), 
                        new Location(6, 8), 

                    },
                    ExpectedResult = 1505,
                    ExpectedNoOfPaths = 114
                }
            };

            foreach (var test in tests)
            {
                test.Run();
            }
        }
    }

    public class Test
    {
        public int No { get; set; }
        public int Width { get; set; }

        public int Height { get; set; }

        public int SideLength { get; set; }

        public int ShortcutLength { get; set; }

        public Location[] Shortcuts { get; set; }

        public int ExpectedResult { get; set; }

        public int ExpectedNoOfPaths { get; set; }


        public void Run()
        {
            var pathFinder = new PathFinder(Width, Height, SideLength, ShortcutLength, Shortcuts);
            Location[][] paths;
            var result = pathFinder.FindShortestPath(out paths);

            Console.WriteLine(
                "Test {0}: Oczekiwany wynik: {1}, Otrzymany wynik {2} \t\t {3}",
                No,
                ExpectedResult,
                result,
                ExpectedResult == result ? "OK" : "BŁĄD");

            bool allPaths = true;
            foreach (var path in paths)
            {
                allPaths &= ValidatePath(path);
            }

            Console.WriteLine("\tZwrócone ścieżki:\t\t\t\t\t {0}", paths.Length > 0 && allPaths ? "OK" : "BŁĄD");

            Console.WriteLine(
                "\tOczekiwana liczba ścieżek: {0}, Otrzymana liczba {1} \t {2}",
                ExpectedNoOfPaths,
                paths.Length,
                ExpectedNoOfPaths == paths.Length ? "OK" : "BŁĄD");

            Console.WriteLine();
        }

        private bool ValidatePath(Location[] path)
        {
            Location previous = path[0];
            int length = 0;
            for (int i = 1; i < path.Length; i++)
            {
                if (previous.X < path[i].X && previous.Y < path[i].Y)
                {
                    if (!Shortcuts.Contains(previous))
                    {
                        return false;
                    }

                    length += ShortcutLength;
                }
                else
                {
                    length += SideLength;
                }

                previous = path[i];
            }

            return length == ExpectedResult;
        }
    }

    public class Location
    {
        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; private set; }
        public int Y { get; private set; }

        public override bool Equals(object obj)
        {
            var other = obj as Location;
            if (other == null)
            {
                return false;
            }

            return this.X == other.X && this.Y == other.Y;
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() ^ this.Y.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("[{0};{1}]", this.X, this.Y);
        }
    }
}
