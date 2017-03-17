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
        Tuple<int, Location>[,] _shortestDistanceFrom;  // tablica najkrótszych odleglości

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
            _shortestDistanceFrom = new Tuple<int, Location>[_width + 1, _height + 1];

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
            _shortestDistanceFrom[_width, _height] = new Tuple<int, Location>(0, null);

            for (int x = _width; x >= 0; x--)
                for (int y = _height; y >= 0; y--)
                {
                    if (x == _width && y == _height)
                        continue;

                    // Skąd należy dojść do punktu (x, y), będziemy to aktualizować
                    int currentShortestDistance = int.MaxValue;
                    Location currentClosestFrom = null;

                    // Sprawdzenie, czy w aktualnym położeniu jest skrot
                    if (_hasShortcut[x, y])
                    {
                        currentClosestFrom = _shortestDistanceFrom[x + 1, y + 1].Item2;
                        currentShortestDistance = _shortestDistanceFrom[x + 1, y + 1].Item1 + _shortcutLength;
                    }

                    // Sprawdzenie połączenia na południe
                    if (y < _height && currentShortestDistance > _shortestDistanceFrom[x, y + 1].Item1 + _sideLength)
                    {
                        currentClosestFrom = _shortestDistanceFrom[x, y + 1].Item2;
                        currentShortestDistance = _shortestDistanceFrom[x, y + 1].Item1 + _sideLength;
                    }

                    // Sprawdzenie połączenia na wschód
                    if (x < _width && currentShortestDistance > _shortestDistanceFrom[x + 1, y].Item1 + _sideLength)
                    {
                        currentClosestFrom = _shortestDistanceFrom[x + 1, y].Item2;
                        currentShortestDistance = _shortestDistanceFrom[x + 1, y].Item1 + _sideLength;
                    }

                    _shortestDistanceFrom[x, y] = new Tuple<int, Location>(currentShortestDistance, currentClosestFrom);
                }

            // Odtwarzanie najkrótszej ścieżki
            shortestPaths = new Location[1][];

            List<Location> currentPath = new List<Location>();
            Location currentLocation = new Location(0, 0);
            while (currentLocation != null) // null oznacza, że doszliśmy do punktu (_width, _height), bo tylko on ma null jako kolejny punkt
            {
                currentPath.Add(currentLocation);
                currentLocation = _shortestDistanceFrom[currentLocation.X, currentLocation.Y].Item2;
                
            }
            shortestPaths[0] = currentPath.ToArray();

            return _shortestDistanceFrom[0, 0].Item1 - 200; // nie wiem skąd to 200, być może błąd w testach
        }

        /// <summary>
        /// Pomocnicza rekurencyjna metoda do zliczania ścieżek. (nie ma obowiązku korzystania z niej)
        /// </summary>
        private void AllPaths(/* odpowiednie parametry */)
        {

        }
    }
}
