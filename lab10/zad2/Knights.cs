using System;
using System.Collections.Generic;

namespace Lab10
{
    public class Knights
    {
        int _n;
        int _m;
        // tu możesz dopisać deklaracje potrzebnych składowych
        Point[] _dragons;


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
            _dragons = dragons;
        }

        /// <summary>
        /// Metoda która znajduje drogę dla rycerza o ile ta istnieje
        /// </summary>
        /// <param name="route">znaleziona droga (jeśli droga nie istnieje ten parametr nie ma znaczenia)</param>
        /// <returns>TRUE wtedy i tylko wtedy gdy istnieje droga</returns>
        public bool CalculateRoute(out Point[] route)
        {
            bool[,] isPointVisited = new bool[_n, _m];
            int pointsLeft = _n * _m - _dragons.Length;
            foreach (Point dragon in _dragons)
                isPointVisited[dragon.X, dragon.Y] = true;


            List<Point> routeList = new List<Point>(pointsLeft);
            if (CalculateRouteRecursive(isPointVisited, pointsLeft, routeList, 0, 0))
            {
                route = routeList.ToArray();
                return true;
            }

            route = null;
            return false;
        }

        private bool CalculateRouteRecursive(bool[,] isPointVisited, int pointsLeft, List<Point> route, int x, int y)
        {
            if (x < 0 || x >= _n || y < 0 || y >= _m)
                return false;
            if (isPointVisited[x, y])
                return false;

            route.Add(new Point(x, y));
            pointsLeft--;
            isPointVisited[x, y] = true;
            if (pointsLeft == 0)
                return true;
            
            if (CalculateRouteRecursive(isPointVisited, pointsLeft, route, x + 2, y + 1) ||
                CalculateRouteRecursive(isPointVisited, pointsLeft, route, x + 2, y - 1) ||
                CalculateRouteRecursive(isPointVisited, pointsLeft, route, x - 2, y + 1) ||
                CalculateRouteRecursive(isPointVisited, pointsLeft, route, x - 2, y - 1) ||
                CalculateRouteRecursive(isPointVisited, pointsLeft, route, x + 1, y + 2) ||
                CalculateRouteRecursive(isPointVisited, pointsLeft, route, x + 1, y - 2) ||
                CalculateRouteRecursive(isPointVisited, pointsLeft, route, x - 1, y + 2) ||
                CalculateRouteRecursive(isPointVisited, pointsLeft, route, x - 1, y - 2))
            {
                return true;
            }

            pointsLeft++;
            route.RemoveAt(route.Count - 1);
            isPointVisited[x, y] = false;
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
