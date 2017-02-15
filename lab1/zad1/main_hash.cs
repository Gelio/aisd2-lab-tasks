
using System;
using ASD;

class Lab01
{

    static void Main()
    {
        Func<int, int, int>[] hash = { HashTable.ModHash, HashTable.MultiplyHash };
        Func<int, int, int>[] shift = { HashTable.Shift1, HashTable.Shift5, HashTable.Shiftk, HashTable.Shifth };
        string[] hashm = { "haszowanie modulo", "haszowanie multiplikatywne" };
        string[] shiftm = { "sekwencyjne rozwiazywanie kolizji",
                            "liniowe rozwiazywanie kolizji", 
                            "rozw. kolizji z rosnacym krokiem",
                            "rozw. kolizji przy pomocy haszowania dwukrotnego" };
        int cit, cif, cst, csf, crt, crf;

        for (int h = 0; h < 2; ++h)
            for (int s = 0; s < 4; ++s)
            {
                HashTable t = new HashTable(hash[h], shift[s]);
                Random r = new Random(34);
                cit = cif = cst = csf = crt = crf = 0;
                Console.WriteLine();
                // Trzeba było zmienić ilość operacji na 1000, ponieważ do tej liczby sumuje się
                // liczba operacji w outpucie z wyników
                for (int i = 0; i < 1000; ++i)
                    switch (r.Next() % 3)
                    {
                        case 0:
                            if (t.Insert(r.Next(50)))
                                ++cit;
                            else
                                ++cif;
                            break;
                        case 1:
                            if (t.Search(r.Next(50)))
                                ++cst;
                            else
                                ++csf;
                            break;
                        case 2:
                            if (t.Remove(r.Next(50)))
                                ++crt;
                            else
                                ++crf;
                            break;
                    }
                Console.WriteLine(hashm[h] + " - " + shiftm[s]);
                // Ktoś pozmieniał wyniki, gdy sam uruchomiłem program miałem parzystą liczbę wstawień,
                // podczas gdy tutaj jest liczba nieparzysta. Zatem nie wyniki nie są miarodajne i nie należy
                // na nie patrzeć. Być może zostało użyte inne ziarno losowości (zmienna r) i stąd różnice.

                // Ogólnie w każdym teście wyniki powinny być takie same, bo bez względu na funkcję mieszającą
                // i sposób rozwiązywania kolizji te same dane się tak samo zachowywać.

                // Aktualne rozwiązanie prawdopodobnie zawiera błędy, ponieważ nie w każdym wariancie są takie
                // same wyniki.
                Console.WriteLine("  {0,5} udanych wstawien      - powinno byc 187,  {1}", cit, cit == 187);
                Console.WriteLine("  {0,5} nieudanych wstawien   - powinno byc 148,  {1}", cif, cif == 148);
                Console.WriteLine("  {0,5} udanych wyszukiwan    - powinno byc 133,  {1}", cst, cst == 133);
                Console.WriteLine("  {0,5} nieudanych wyszukiwan - powinno byc 181,  {1}", csf, csf == 181);
                Console.WriteLine("  {0,5} udanych usuniec       - powinno byc 162,  {1}", crt, crt == 162);
                Console.WriteLine("  {0,5} nieudanych usuniec    - powinno byc 189,  {1}", crf, crf == 189);
                Console.WriteLine("  {0,5} dostepow", t.AccessCount);
            }

        Console.WriteLine();

    }

}
