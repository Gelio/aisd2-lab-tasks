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
            Stack<Book> highestStack = new Stack<Book>();
            int highestStackHeight = 0;

            HighestStackHelper(books, ref highestStack, ref highestStackHeight);

            stack = new List<Book>(highestStack);
            return highestStackHeight;
        }

        private static void HighestStackHelper(IList<Book> books, ref Stack<Book> currentStack, ref int currentStackHeight)
        {
            Stack<Book> highestStack = currentStack;
            int highestStackHeight = currentStackHeight;
            List<Book> currentStackReversed = new List<Book>(currentStack);
            currentStackReversed.Reverse();

            Book topBook = currentStack.Count > 0 ? currentStack.Peek() : null;
            for (int i = 0; i < books.Count; i++)
            {
                if (currentStack.Contains(books[i]))
                    continue;

                Book book = books[i];
                if (topBook != null)
                {
                    if ((topBook.Height < book.Height || topBook.Width < book.Width) && (topBook.Height < book.Width || topBook.Width < book.Height))
                        continue;
                }

                Stack<Book> newStack = new Stack<Book>(currentStackReversed);
                newStack.Push(book);
                int newStackHeight = currentStackHeight + book.Thickness;
                HighestStackHelper(books, ref newStack, ref newStackHeight);
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