using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiSD_Lab5
{
    public class PathFinder
    {
        int _width;
        int _height;
        int _sideLength;
        int _shortcutLength;
        Location[] _shortcuts;
        bool[,] _hasShortcut;  // tablica skrótów
        Tuple<int, List<Location>>[,] _shortestDistanceFrom;  // tablica najkrótszych odleglości

        /// <summary>
        /// Konstruktor klasy PathFinder
        /// </summary>
        /// <param name="width">Szerokość miasta</param>
        /// <param name="height">Wysokość miasta</param>
        /// <param name="sideLength">Długość normalnej drogi</param>
        /// <param name="shortcutLength">Długość drogi na skróty</param>
        /// <param name="shortcuts">Lista skrótów (wystąpienie punktu [x,y] oznacza, że z [x,y] można przejść do [x+1,y+1] skrótem)</param>
        public PathFinder(int width, int height, int sideLength, int shortcutLength, Location[] shortcuts)
        {
            _width = width;
            _height = height;
            _sideLength = sideLength;
            _shortcutLength = shortcutLength;
            _shortcuts = shortcuts;
            _hasShortcut = new bool[_width + 1, _height + 1];
            _shortestDistanceFrom = new Tuple<int, List<Location>>[_width, _height];

            for (int x = 0; x < _width; x++)
                for (int y=0; y < _height; y++)
                    _shortestDistanceFrom[x, y] = new Tuple<int, List<Location>>(int.MaxValue, new List<Location>());

            foreach (Location shortcut in _shortcuts)
            {
                _hasShortcut[shortcut.X, shortcut.Y] = true;
            }
        }

        /// <summary>
        /// Metoda znajduje najkrótszą szcieżkę w mieście
        /// </summary>
        /// <param name="shortestPaths">
        /// Parametr wyjściowy - znaleziona tablica ze wszystkimi najkrótszymi scieżkami (zaczynając od punktu [0,0] kończąc na celu).
        /// W przypadku realizacji zadania zwracającego jedną ścieżkę należy zwrócić tablicę zawierającą dokładnie jedną tablicę z najkrótszą ścieżka.
        /// </param>
        /// <returns></returns>
        public int FindShortestPath(out Location[][] shortestPaths)
        {


            // Odległość do samego siebie jest rowna 0
            _shortestDistanceFrom[_width - 1, _height - 1] = new Tuple<int, List<Location>>(0, new List<Location>());

            for (int x = _width - 1; x >= 0; x--)
                for (int y = _height - 1; y >= 0; y--)
                {
                    // Omijamy punkt docelowy
                    if (x == _width - 1 && y == _height - 1)
                        continue;

                    // Skąd należy dojść do punktu (x, y), będziemy to aktualizować
                    int currentShortestDistance = int.MaxValue;
                    List<Location> currentClosestFrom = new List<Location>();

                    // Sprawdzenie, czy w aktualnym położeniu jest skrot
                    if (_hasShortcut[x, y] && x < _width - 1 && y < _height - 1)
                    {
                        currentClosestFrom.Add(new Location(x + 1, y + 1));
                        currentShortestDistance = _shortestDistanceFrom[x + 1, y + 1].Item1 + _shortcutLength;
                    }

                    // Sprawdzenie połączenia na południe
                    if (y < _height - 1)
                    {
                        int newDistance = _shortestDistanceFrom[x, y + 1].Item1 + _sideLength;
                        if (currentShortestDistance > newDistance)
                        {
                            // Znaleziono lepsze połączenie
                            currentClosestFrom.Clear();
                            currentClosestFrom.Add(new Location(x, y + 1));
                            currentShortestDistance = newDistance;
                        }
                        else if (currentShortestDistance == _shortestDistanceFrom[x, y + 1].Item1 + _sideLength)
                        {
                            // Znaleziono alternatywne połączenie
                            currentClosestFrom.Add(new Location(x, y + 1));
                        }
                    }

                    // Sprawdzenie połączenia na wschód
                    if (x < _width - 1)
                    {
                        int newDistance = _shortestDistanceFrom[x + 1, y].Item1 + _sideLength;
                        if (currentShortestDistance > newDistance)
                        {
                            currentClosestFrom.Clear();
                            currentClosestFrom.Add(new Location(x + 1, y));
                            currentShortestDistance = newDistance;
                        }
                        else if (currentShortestDistance == newDistance)
                        {
                            currentClosestFrom.Add(new Location(x + 1, y));
                        }
                    }

                    _shortestDistanceFrom[x, y] = new Tuple<int, List<Location>>(currentShortestDistance, currentClosestFrom);
                }

            // Odtwarzanie najkrótszej ścieżki
            shortestPaths = ConstructAllPaths(new Location(0, 0)).Select(path => path.ToArray()).ToArray();
            return _shortestDistanceFrom[0, 0].Item1;
        }

        /// <summary>
        /// Pomocnicza rekurencyjna metoda do zliczania ścieżek. (nie ma obowiązku korzystania z niej)
        /// </summary>
        private List<List<Location>> ConstructAllPaths(Location startingLocation)
        {
            if (startingLocation.X == _width - 1 && startingLocation.Y == _height - 1)
                return new List<List<Location>>(1) { new List<Location>(1) { startingLocation } };

            //List<List<Location>> paths = new List<List<Location>>();
            IEnumerable<List<Location>> paths = new List<List<Location>>();
            foreach (Location nextCrossing in _shortestDistanceFrom[startingLocation.X, startingLocation.Y].Item2)
            {
                List<List<Location>> pathsToNextCrossing = ConstructAllPaths(nextCrossing);
                foreach (List<Location> pathToNextCrossing in pathsToNextCrossing)
                    pathToNextCrossing.Insert(0, startingLocation);
                paths = paths.Concat(pathsToNextCrossing).ToList();
            }
            return paths.ToList();
        }
    }
}
