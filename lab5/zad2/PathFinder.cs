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
        // można dodać pola jeśli będą potrzebne

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
            shortestPaths = new Location[0][];
            return 0;
        }

        /// <summary>
        /// Pomocnicza rekurencyjna metoda do zliczania ścieżek. (nie ma obowiązku korzystania z niej)
        /// </summary>
        private void AllPaths(/* odpowiednie parametry */)
        {
            
        }
    }
}
