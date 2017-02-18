using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD
{
    class DaszekPlanner
    {
        private double[] odleglosci;
        private double[] wysokosci;
        private double maxDl;

        public DaszekPlanner(double[] odleglosci, double[] wysokosci, double maxDl)
        {
            this.odleglosci = odleglosci;
            this.wysokosci = wysokosci;
            this.maxDl = maxDl;
        }

        public int KosztDaszku(out int[] daszek)
        {
            // Według mnie w teście 1 jest bład, bo ze słupka 2 (odległość 4, wysokość 0) daszek
            // do słupka 4 (odległośc 12, wysokośc 8) będzie miał długość około 11,3, a maxDl
            // w tamtym teście wynosi 11, czyli taki nie można kupić takiego fragmentu.
            // Spróbowałem zmienić funkcje OdlegloscSzczytow na zwykłą różnicę odległości słupków,
            // wtedy test 1 przechodził, ale test 2 się sypał. Wydaje mi się, że jest to błąd w zadaniu.
            // Po zmianie wysokości ostatniego słupka w teście 1 z wartości 8 na 7 test przechodzi. Polecam
            // Sprawdzać z tym ustatwieniem.

            List<int> daszki = new List<int>();
            daszki.Add(0);
            
            while (daszki.Last() < odleglosci.Length - 1)
            {
                int nastepnyOptymalny = daszki.Last() + 1;
                for (int i=nastepnyOptymalny + 1; i < odleglosci.Length; i++)
                {
                    if (OdlegloscSzczytow(daszki.Last(), i) <= maxDl)
                        nastepnyOptymalny = i;
                }
                daszki.Add(nastepnyOptymalny);
            }

            daszek = daszki.ToArray();
            return daszek.Length - 1;
        }

        public int KosztLadnegoDaszku(out int[] daszek)
        {
            // Ponownie prawdopodobny błąd w zadaniu przy teście 1, zobacz komentarz w funkcji KosztDaszki.
            // Po zmianie wysokości ostatniego słupka na 7 (zamiast 8) test 1 przechodzi poprawnie.

            List<int> daszki = new List<int>();
            daszki.Add(0);

            IComparer<Tuple<double, int>> slupekComparer = new SlupekZNachyleniemComparer(odleglosci);

            while (daszki.Last() < odleglosci.Length - 1)
            {
                // pierwszy element wektora to nachylenie, drugi to indeks slupka
                SortedSet<Tuple<double, int>> dostepne = new SortedSet<Tuple<double, int>>(slupekComparer);
                for (int i=daszki.Last() + 1; i < odleglosci.Length; i++)
                {
                    double odleglosc = OdlegloscSzczytow(daszki.Last(), i);
                    if (odleglosc > maxDl)
                        continue;

                    double nachylenie = NachylenieSlupkow(daszki.Last(), i);
                    dostepne.Add(new Tuple<double, int>(nachylenie, i));
                }

                daszki.Add(dostepne.Max.Item2);
            }

            daszek = daszki.ToArray();
            return daszek.Length - 1;
        }

        private double OdlegloscSzczytow(int slupek1, int slupek2)
        {
            double poziomo = odleglosci[slupek1] - odleglosci[slupek2];
            double pionowo = wysokosci[slupek1] - wysokosci[slupek2];
            return Math.Sqrt(poziomo * poziomo + pionowo * pionowo);
        }

        // Zwraca tangens kąta nachylenia między szczytami słupków
        private double NachylenieSlupkow(int slupek1, int slupek2)
        {
            return (wysokosci[slupek2] - wysokosci[slupek1]) / (odleglosci[slupek2] - odleglosci[slupek1]);
        }
    }

    // Komparator stworzony na potrzeby SortedSet. Można byłoby użyć zwykłego zbioru (Set)
    // albo listy (List) i na końcu dopiero wybrać element o maksymalnym kącie nachylenia.
    // To byłoby nawet bardziej optymalne rozwiązanie, bo nie trzeba trzymać zbioru cały czas
    // posortowanego, a jedynie wybrać słupek o największym kącie nachylenia na samym końcu.
    class SlupekZNachyleniemComparer : IComparer<Tuple<double, int>>
    {
        private double[] odleglosci;
        public SlupekZNachyleniemComparer(double[] odleglosci)
        {
            this.odleglosci = odleglosci;
        }

        public int Compare(Tuple<double, int> slupek1, Tuple<double, int> slupek2)
        {
            int porownianieNachylen = slupek1.Item1.CompareTo(slupek2.Item1);
            if (porownianieNachylen == 0)
                return odleglosci[slupek1.Item2].CompareTo(odleglosci[slupek2.Item2]);
            else
                return porownianieNachylen;
        }
    }
}
