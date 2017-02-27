using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGreedyFish
{
    static class BoardExtender
    {
        /// <summary>
        /// Wyznacza ruchy i liczbę ryb zebranych dla algorytmu zachłannego (ruch wykonywany jest do najbliższego najlepszego pola [takiego z największą liczbą ryb])
        /// </summary>
        /// <param name="board"></param>
        /// <param name="moves">Tablica ruchów wykonanych przez pingwiny</param>
        /// <returns>Liczba ryb zebrana przez pingwiny</returns>
        public static int GreedyAlgorithm1(this Board board, out Move[] moves)
        {
            moves = null;
            int fishes = -1;
            
            return fishes; //zwrócona liczba ryb
        }

        /// <summary>
        /// Wyznacza ruchy i liczbę ryb zebranych dla algorytmu zachłannego (ruch wykonywany jest do końca kierunku, aż do napotkania "dziury" lub końca siatki)
        /// </summary>
        /// <param name="board"></param>
        /// <param name="moves">Tablica ruchów wykonanych przez pingwiny</param>
        /// <returns>Liczba ryb zebrana przez pingwiny</returns>
        public static int GreedyAlgorithm2(this Board board, out Move[] moves)
        {
            moves = null;
            int fishes = -1;
            
            return fishes; //zwrócona liczba ryb
        }
    }
}
