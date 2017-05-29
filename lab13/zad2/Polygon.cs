using System;
using System.Collections.Generic;

namespace asd2_lab13
{

    public struct Point
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class Polygon
    {
        Point[] vertices;

        // Wspolrzedne x "przystankow zamiatania"
        int[] przystanki;

        // tablica pomocnicza - indeksy wierzcholkow posortowanych od najbardziej lewego
        int[] ind;

        // Odcinki "widziane" przez prosta zamiatajaca trzymamy jako pare indeksow
        // w tablicy vertices, gdzie pierwszy element pary zawsze jest bardziej lewym punktem
        Tuple<int, int>[][] sweepingStates;

        /// <summary>
        /// Wielokat bez samoprzeciec.
        /// </summary>
        /// <param name="vertices">Wierzcholki w kolejnosci przeciwnej do wskazowek zegara.</param>
        public Polygon(Point[] vertices)
        {
            this.vertices = vertices;
            // tu przetwarzanie wstepne
        }

        /// <summary>
        /// Sprawdza, czy punkt o podanych wspolrzednych nalezy do wielokata
        /// (punkt lezacy na ktoryms z bokow rowniez nalezy do wielokata)
        /// </summary>
        /// <param name="px">Wspolrzedna x</param>
        /// <param name="py">Wspolrzedna y</param>
        /// <returns></returns>
        public bool ContainsPoint(double px, double py)
        {
            // uzupelnic
            return false;
        }

        //
        //  Tu mozesz dopisac potrzebne pola i metody pomocnicze
        //

        // Porownanie punktu i odcinka (ktore jest nizej)
        int comparePointToSeg(double px, double py, Tuple<int, int> seg)
        {
            int dxs = vertices[seg.Item2].x - vertices[seg.Item1].x;
            int dys = vertices[seg.Item2].y - vertices[seg.Item1].y;
            double dxp = px - vertices[seg.Item1].x;
            double dyp = py - vertices[seg.Item1].y;
            return (dyp * dxs).CompareTo(dys * dxp);
        }

        //Sprawdzenie, ktory z odcinkow lezy niżej
        // Zakladamy, ze odcinki sie nie przecinaja
        int compareSegments(Tuple<int, int> t1, Tuple<int, int> t2)
        {
            if (t1.Item1 == t2.Item1)
            {
                int t1x = vertices[t1.Item2].x;
                int t1y = vertices[t1.Item2].y;
                return comparePointToSeg(t1x, t1y, t2);
                //return ((t1y-y)*(t2x-x)).CompareTo((t2y-y)*(t1x-x));
            }
            else
                if (vertices[t1.Item1].x >= vertices[t2.Item1].x && vertices[t1.Item1].x <= vertices[t2.Item2].x)
                return comparePointToSeg(vertices[t1.Item1].x, vertices[t1.Item1].y, t2);
            else
                    if (vertices[t1.Item1].x <= vertices[t2.Item1].x && vertices[t1.Item2].x >= vertices[t2.Item1].x)
                return -compareSegments(t2, t1);
            else
                throw new InvalidOperationException();
        }

    }
}
