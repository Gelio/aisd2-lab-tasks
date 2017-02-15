namespace BooksStacking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class Program
    {
        public static void Main(string[] args)
        {
            var testSets = GetTestSets();

            foreach (var testSet in testSets)
            {
                IList<Book> stack;
                int efficiency = Book.ReadCount;
                int height = HighestStack(testSet.Books, out stack);
                efficiency = Book.ReadCount - efficiency;

                bool passed = true;
                Console.WriteLine("Test: " + testSet.Name);
                if (height != testSet.Height)
                {
                    Console.WriteLine("Test failed - wrong height");
                    passed = false;
                }

                if (stack
                    .Zip(testSet.StackIds, (b, id) => new { b.Id, RefId = id })
                    .Any(x => x.Id != x.RefId))
                {
                    Console.WriteLine("Test failed - wrong stack");
                    passed = false;
                }

                if (efficiency > testSet.Efficiency)
                {
                    Console.WriteLine("Warning - efficiency low ({0}, ref: {1})", efficiency, testSet.Efficiency);
                }

                if (passed)
                {
                    Console.WriteLine("Test passed");
                }
                Console.WriteLine("Height: {0}", height);
                foreach (var book in stack)
                {
                    Console.WriteLine("Id: {0}, wysokość: {1}, szerokość: {2}, grubość: {3}", book.Id, book.Height, book.Width, book.Thickness);
                }

                Console.WriteLine();
            }
        }

        private static IEnumerable<TestSet> GetTestSets()
        {
            return new List<TestSet>()
                {
                    new TestSet()
                        {
                            Name = "1", 
                            Books =
                                new List<Book>()
                                    {
                                        new Book(7, 6, 1, 6), 
                                        new Book(1, 5, 6, 3), 
                                        new Book(2, 4, 7, 2), 
                                        new Book(3, 5, 9, 1), 
                                        new Book(10, 11, 1, 9), 
                                        new Book(11, 100, 1, 5), 
                                        new Book(4, 3, 6, 2), 
                                        new Book(5, 10, 9, 3), 
                                        new Book(6, 7, 3, 5), 
                                        new Book(8, 4, 8, 2), 
                                        new Book(9, 6, 8, 3), 
                                    }, 
                            Height = 14, 
                            StackIds = new List<int>() { 5, 6, 7 }, 
                            Efficiency = 400
                        }, 

                    new TestSet()
                        {
                            Name = "2", 
                            Books =
                                new List<Book>()
                                    {
                                        new Book(7, 1, 1, 6), 
                                        new Book(1, 2, 2, 3), 
                                        new Book(2, 3, 3, 2), 
                                        new Book(3, 4, 4, 1), 
                                        new Book(10, 5, 5, 9), 
                                        new Book(11, 6, 6, 5), 
                                        new Book(4, 7, 7, 2), 
                                        new Book(5, 8, 8, 3), 
                                        new Book(6, 9, 9, 5), 
                                        new Book(8, 10, 10, 2), 
                                        new Book(9, 11, 11, 3), 
                                        new Book(12, 12, 12, 3), 
                                        new Book(13, 13, 13, 3), 
                                        new Book(14, 14, 14, 3), 
                                        new Book(15, 15, 15, 3), 
                                        new Book(16, 16, 16, 3), 
                                    }, 
                            Height = 56, 
                            StackIds = new List<int>() { 16, 15, 14, 13, 12, 9, 8, 6, 5, 4, 11, 10, 3, 2, 1, 7 }, 
                            Efficiency = 1000
                        }, 

                    new TestSet()
                        {
                            Name = "3", 
                            Books =
                                new List<Book>()
                                    {
                                        new Book(1, 10, 1, 15), 
                                        new Book(2, 9, 8, 2), 
                                        new Book(3, 8, 3, 10), 
                                        new Book(4, 7, 4, 1), 
                                        new Book(5, 8, 8, 3), 
                                        new Book(6, 9, 9, 5), 
                                        new Book(7, 5, 5, 9), 
                                        new Book(8, 6, 6, 5), 
                                        new Book(9, 7, 7, 2), 
                                    }, 
                            Height = 24, 
                            StackIds = new List<int>() { 6, 5, 9, 8, 7 }, 
                            Efficiency = 300
                        }
                };
        }
    }

    public class Book
    {
        private int width;
        private int height;
        private int thickness;

        public Book(int id, int width, int height, int thickness)
        {
            this.Id = id;
            this.Width = width;
            this.Height = height;
            this.Thickness = thickness;
        }

        public static int ReadCount { get; private set; }

        public int Id { get; private set; }

        public int Width
        {
            get
            {
                ReadCount++;
                return this.width;
            }

            set
            {
                this.width = value;
            }
        }


        public int Height
        {
            get
            {
                ReadCount++;
                return this.height;
            }

            set
            {
                this.height = value;
            }
        }


        public int Thickness
        {
            get
            {
                ReadCount++;
                return this.thickness;
            }

            set
            {
                this.thickness = value;
            }
        }
    }

    public struct TestSet
    {
        public string Name { get; set; }

        public int Efficiency { get; set; }

        public IList<Book> Books { get; set; }

        public int Height { get; set; }

        public IList<int> StackIds { get; set; }
    }
}
