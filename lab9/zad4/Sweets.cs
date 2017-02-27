using System;

using ASD.Graphs;

namespace Lab09
{
    class Sweets
    {
        /// <summary>
        /// Implementacja zadania 1
        /// </summary>
        /// <param name="childrenCount">Liczba dzieci</param>
        /// <param name="sweetsCount">Liczba batoników</param>
        /// <param name="childrenLikes">
        /// Tablica tablic upodobań dzieci. Tablica childrenLikes[i] zawiera indeksy batoników
        /// które lubi i-te dziecko. Dzieci i batoniki są indeksowane od 0.
        /// </param>
        /// <param name="assignment">
        /// Wynikowy parametr. assigment[i] zawiera indeks batonika które dostało i-te dziecko.
        /// Jeśli dziecko nie dostało żadnego batonika to -1.
        /// </param>
        /// <returns>Liczba dzieci, które dostały batonik.</returns>
        public static int Task1( int childrenCount, int sweetsCount, int[][] childrenLikes, out int[] assignment )
        {
            assignment = null;
            return -1;
        }

        /// <summary>
        /// Implementacja zadania 2
        /// </summary>
        /// <param name="childrenCount">Liczba dzieci</param>
        /// <param name="sweetsCount">Liczba batoników</param>
        /// <param name="childrenLikes">
        /// Tablica tablic upodobań dzieci. Tablica childrenLikes[i] zawiera indeksy batoników
        /// które lubi i-te dziecko. Dzieci i batoniki są indeksowane od 0.
        /// </param>
        /// <param name="childrenLimits">Tablica ograniczeń dla dzieci. childtrenLimits[i] to maksymalna liczba batoników jakie może zjeść i-te dziecko.</param>
        /// <param name="sweetsLimits">Tablica ograniczeń batoników. sweetsLimits[i] to dostępna liczba i-tego batonika.</param>
        /// <param name="happyChildren">Wynikowy parametr zadania 2a. happyChildren[i] powinien zawierać true jeśli dziecko jest zadowolone i false wpp.</param>
        /// <param name="shoppingList">Wynikowy parametr zadania 2b. shoppingList[i] poiwnno zawierać liczbę batoników i-tego rodzaju, które trzeba dokupić.</param>
        /// <returns>Maksymalna liczba rozdanych batoników.</returns>
        public static int Task2( int childrenCount, int sweetsCount, int[][] childrenLikes, int[] childrenLimits, int[] sweetsLimits, out bool[] happyChildren, out int[] shoppingList )
        {
            happyChildren = null;
            shoppingList = null;
            return -1;
        }

    }
}
