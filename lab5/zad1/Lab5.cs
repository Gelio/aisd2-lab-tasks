
// tu można dodać using

using System;
namespace Square
{
    partial class Lab5
    {
        /// <summary>
        /// Funkcja obliczająca najmniejszą możliwą liczbę działek, które należy kupić aby zainwestować amount kwadratów.
        /// Metoda powinna wykorzystywać następujace twierdzenie Lagrang'a:
        /// Dla każdego naturalnego x istnieją naturalne a,b,c,d takie, że x = a*a + b*b + c*c + d*d
        /// Uwaga: liczby naturalne zawierają zero
        /// </summary>
        /// <param name="amount">kwota, którą bohater chce wydać</param>
        /// <param name="areas">wynikowa tablica z powierzchniami kupionych działek</param>
        /// <returns>liczba kupionych działek</returns>
        static int CertificateNumberLagrange(int amount, out int[] areas)
        {
            int total = 5;           
            int[] i = new int[4];        

            for(i[0] = 0; i[0]*i[0] <= amount; i[0]++)
                for(i[1] = 0; i[1]*i[1] <= amount; i[1]++)
                    for(i[2] = 0; i[2]*i[2] <= amount; i[2]++)
                        for(i[3] = 0; i[3]*i[3] <= amount; i[3]++)
                            if (i[0] * i[0] + i[1] * i[1] + i[2] * i[2] + i[3] * i[3] == amount)
                            {
                                total = Convert.ToInt32(i[0] != 0) + Convert.ToInt32(i[1] != 0) + Convert.ToInt32(i[2] != 0) + Convert.ToInt32(i[3] != 0);
                                int[] t = new int[total];

                                for (int j = 0; j < total; j++)
                                    t[j] = i[3 - j];
                                areas = t;
                                return total;
                            }

            areas = new int[0];
            return total;            
        }

        /// <summary>
        /// Funkcja obliczająca najmniejszą możliwą liczbę działek, które należy kupić aby zainwestować amount kwadratów.
        /// Metoda powinna wykorzystywać programowanie dynamiczne.
        /// Należy stworzyć tablicę results taką, że results[i] zawiera wielkości działek, które należy kupić aby wydać i kwadratów.
        /// Tablicę należy budować od i = 1 aż do i = amount.
        /// </summary>
        /// <param name="amount">kwota, którą bohater chce wydać</param>
        /// <param name="areas">wynikowa tablica z powierzchniami kupionych działek</param>
        /// <returns>liczba kupionych działek</returns>
        static int CertificateNumberDynamicPrograming(int amount, out int[] areas)
        {
            int[,] t = new int[amount + 1,5];
            for (int i = 0; i <= amount; i++) t[i, 0] = 5;
            t[0, 0] = 0;

            for (int i = 1; i <= amount; i++)
            {
                int m = Convert.ToInt32(Math.Floor(Math.Sqrt(i)));

                if (m * m == i)
                {
                    t[i, 0] = 1;
                    t[i, 1] = m;
                }
                else
                {
                    for (int j = 1; j * j <= i; j++)
                    {
                        if (t[i - j * j, 0] + 1 < t[i, 0])
                        {
                            t[i, 0] = t[i - j * j, 0] + 1;
                            for (int k = 1; k < t[i, 0]; k++)
                                t[i, k] = t[i - j * j, k];

                            t[i, t[i, 0]] = j;                            
                        }
                    }
                }
            }


            areas = new int[t[amount, 0]];
            for (int i = 0; i < areas.Length; i++)
                areas[i] = t[amount, i + 1];
           
            return areas.Length;            
        }
    }
}
