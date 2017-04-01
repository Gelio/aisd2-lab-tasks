namespace BooksStacking
{
    using System;
    using System.Collections.Generic;

    public partial class Program
    {
        /// <summary>
        /// Procedura wyliczająca optymalny (najwyższy stos)
        /// </summary>
        /// <param name="books">Lista dostępnych książek</param>
        /// <param name="stack">Parametr wyjściowy, w którym powinien znaleźć się optymalny stos</param>
        /// <returns>Wysokość optymalnego stosu</returns>
        public static int HighestStack(IList<Book> books, out IList<Book> stack)
        {
            List<Book> highestStack = new List<Book>();
            int highestStackHeight = 0;

            HighestStackHelper(books, ref highestStack, ref highestStackHeight);

            stack = highestStack;
            return highestStackHeight;
        }

        private static void HighestStackHelper(IList<Book> books, ref List<Book> currentStack, ref int currentStackHeight, int? topBookHeight = null, int? topBookWidth = null)
        {
            List<Book> highestStack = currentStack;
            int highestStackHeight = currentStackHeight;

            for (int i = 0; i < books.Count; i++)
            {
                if (currentStack.Contains(books[i]))
                    continue;

                Book book = books[i];
                // To limit the amount of Height/Width/Thickness reads it would be beneficial
                // to save them in an array once and use it from there
                int bookHeight = book.Height,
                    bookWidth = book.Width;
                if (topBookHeight <= bookHeight || topBookWidth <= bookWidth)
                    continue;

                // An assumption has been made that a book cannot have negative thickness
                // Otherwise if it's negative we should not even consider it
                List<Book> newStack = new List<Book>(currentStack);
                newStack.Add(book);
                int newStackHeight = currentStackHeight + book.Thickness;
                HighestStackHelper(books, ref newStack, ref newStackHeight, bookHeight, bookWidth);
                if (newStackHeight > highestStackHeight)
                {
                    highestStack = newStack;
                    highestStackHeight = newStackHeight;
                }
            }

            currentStack = highestStack;
            currentStackHeight = highestStackHeight;
        }
    }
}