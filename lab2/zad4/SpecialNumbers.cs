
using System;

namespace ASD
{
    class SpecialNumbers
    {
        const int mod = 10000;

        // funkcja rekurencyjna
        // n cyfr
        public static int SpecialNumbersRec(int n)
        {
            if (n == 0)
                return 0;
            return SpecialNumbersRecursiveInternal(n, 9, -1);
        }

        private static int SpecialNumbersRecursiveInternal(int n, int maxDigit, int previousParity)
        {
            if (n == 0)
                return 1;

            if (maxDigit <= 1)
                return maxDigit;

            int totalCount = 0;
            for (int i = 1; i <= maxDigit; i++)
            {
                int currentParity = i % 2;
                if (i == maxDigit || currentParity != previousParity)
                    totalCount += SpecialNumbersRecursiveInternal(n - 1, i, currentParity);
                totalCount %= mod;
            }

            return totalCount;
        }

        // programowanie dynamiczne
        // n cyfr
        public static int SpecialNumbersDP(int n)
        {
            // ZMIEN
            return 0;
        }

        // programowanie dynamiczne
        // n cyfr
        // req - tablica z wymaganiami, jezeli req[i, j] == 0 to znaczy, ze  i + 1 nie moze stac PRZED j + 1
        public static int SpecialNumbersDP(int n, bool[,] req)
        {
            // ZMIEN
            return 0;
        }

    }//class SpecialNumbers

}//namespace ASD