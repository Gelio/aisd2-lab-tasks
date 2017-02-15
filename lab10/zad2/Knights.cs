using System;
using System.Collections.Generic;

namespace Lab10
{
    public class Knights
    {
        int _n;
        int _m;
        // tu możesz dopisać deklaracje potrzebnych składowych


        /// <summary>
        /// Konstruktor klasy Knights
        /// </summary>
        /// <param name="n">Szerokość planszy</param>
        /// <param name="m">Wysokość planszy</param>
        /// <param name="dragons">Pozycje smoków - w polu [0;0] na pewno nie ma smoka</param>
        public Knights(int n, int m, Point[] dragons)
        {
            _n = n;
            _m = m;
            // tu możesz dopisać potrzebne inicjalizacje
        }

        /// <summary>
        /// Metoda która znajduje drogę dla rycerza o ile ta istnieje
        /// </summary>
        /// <param name="route">znaleziona droga (jeśli droga nie istnieje ten parametr nie ma znaczenia)</param>
        /// <returns>TRUE wtedy i tylko wtedy gdy istnieje droga</returns>
        public bool CalculateRoute(out Point[] route)
        {
            route=null;
            return false;
        }

       // tu możesz dopisać pomocnicze metody
       // i wszystko inne co może być potrzebne 

    }

    public struct Point
    {
        public Point(int x, int y) : this()
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; private set; }
        public int Y { get; private set; }

        public override string ToString()
        {
            return string.Format("[{0};{1}]", X, Y);
        }
    }

}
