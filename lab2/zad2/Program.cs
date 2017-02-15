using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASD
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n\nKoszt brzydkiego daszku:\n\n");
            testujRozw(false, false);
            Console.WriteLine("\n\nBrzydki Daszek:\n\n");
            testujRozw(false, true);
            Console.WriteLine("\n\nKoszt ladnego daszku:\n\n");
            testujRozw(true, false);
            Console.WriteLine("\n\nLadny Daszek:\n\n");
            testujRozw(true, true);
            Console.WriteLine("\n");
        }

        static void testujRozw(bool ladny, bool sprawdz_tab)
        {
            double[] odl0 = { 0, 1, 3, 4, 7 };
            double[] wys0 = { 0, 3, 4, 7, 3 };
            int[] rozw0brzydki = { 0, 2, 4 };
            int[] rozw0ladny = { 0, 1, 3, 4 };
            Console.Write("Test 0:\t");
            wykonajTest(ladny, odl0, wys0, 5.99, ladny?3:2, sprawdz_tab ? (ladny ? rozw0ladny : rozw0brzydki) : null);

            double[] odl = { 0, 2, 4, 10, 12 };
            double[] wys = { 10, 4, 0, 3, 8 };
            int[] rozw = { 0, 2, 4 };
            Console.Write("Test 1:\t");
            wykonajTest(ladny, odl, wys, 11, 2, sprawdz_tab ? rozw : null);

            double[] odl2 = { 0, 1, 3, 4, 10, 14.1 };
            double[] wys2 = { 10, 2, 4, 0, 3, 3 };
            int[] rozw2ladny = { 0, 2, 4, 5 };
            int[] rozw2brzydki = { 0, 3, 5 };
            Console.Write("Test 2:\t");
            wykonajTest(ladny, odl2, wys2, 11, ladny?3:2, sprawdz_tab ? (ladny ? rozw2ladny : rozw2brzydki) : null);

            double[] odl3 = { 0, 3, 6, 9, 12, 15, 18 };
            double[] wys3 = { 0, 4, 8, 12, 16, 20, 24 };
            int[] rozw3 = { 0, 2, 4, 6 };
            Console.Write("Test 3:\t");
            wykonajTest(ladny, odl3, wys3, 10, 3, sprawdz_tab ? rozw3 : null);
        }

        static void wykonajTest(bool ladny, double[] odl, double[] wys, double d, int popr, int[] popr_t)
        {
            DaszekPlanner dp = new DaszekPlanner(odl, wys, d);
            int wynik;
            int[] wynik_t;
            wynik = ladny ? dp.KosztLadnegoDaszku(out wynik_t) : dp.KosztDaszku(out wynik_t);
            porownajWyniki(wynik, wynik_t, popr, popr_t);
        }

        static void porownajWyniki(int rozw, int[] rozw_t, int popr, int[] popr_t)
        {
            if (popr_t == null && rozw==popr)
                Console.WriteLine("ok");
            else
            {
                if (rozw != popr)
                    Console.WriteLine("Bledny wynik! (jest {0}, powino byc {1})", rozw, popr);
                if (rowne(rozw_t, popr_t))
                    Console.WriteLine("ok");
                else
                {
                    Console.WriteLine("Bledna tablica!");
                    Console.Write("\t\t jest \t");
                    wypiszTab(rozw_t);
                    Console.Write("\t\t powinno byc\t");
                    wypiszTab(popr_t);
                    if (rozw == popr)
                        Console.WriteLine("\t(ale obliczony koszt jest ok)");
                }
            }
        }

        static bool rowne(int[] tab1, int[] tab2)
        {
            if ( tab1 == null && tab2 == null ) return true;
            if ( tab1 == null || tab2 == null || tab1.GetLength(0) != tab2.GetLength(0) )
                return false;
            for (int i = 0; i < tab1.GetLength(0); i++)
                if (tab1[i] != tab2[i])
                    return false;
            return true;
        }

        static void wypiszTab(int[] tab)
        {
            if (tab == null)
                Console.WriteLine("null");
            else
            {
                for (int i = 0; i < tab.GetLength(0); i++)
                {
                    Console.Write(tab[i]);
                    if (i < tab.GetLength(0) - 1)
                        Console.Write(", ");
                    else
                        Console.WriteLine();
                }
            }
        }

    }
}
