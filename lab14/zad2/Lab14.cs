
using System.Linq;
using System.Runtime.InteropServices;
using ASD.Text;

namespace ASD2
{
    using System.Collections.Generic;

    public static partial class Lab14
    {

        /// <summary>
        /// Wyznaczanie minimalnego szablonu
        /// </summary>
        /// <param name="text">Napis do namalowania</param>
        /// <param name="indexes">Tablica zawierająca indeksy "punktów przyłozenia" początku szablonu</param>
        /// <returns>Długość szablonu</returns>
        public static int MinPattern(string text, out int[] indexes)
        {
            int[] P = StringMatching.ComputeP(text);
            int textLength = text.Length;
            int shortestPatternLength = textLength;
            List<int> shortestPatternMatches = new List<int>() { 0 };

            foreach (int prefixLength in P.Where(x => x > 0).Distinct().OrderByDescending(x => x))
            {
                //if (shortestPatternLength < textLength && shortestPatternLength <= 2 * prefixLength)
                //{
                //    // Using "tip 2"
                //    shortestPatternLength = prefixLength;
                //    shortestPatternMatches = null;
                //}
                //else
                //{
                    // Check if the pattern matches
                    string prefix = text.Substring(0, prefixLength);
                    int[] prefixP = StringMatching.ComputeP(prefix);
                    List<int> matches = StringMatching.KMP(text, prefix, prefixP);

                    bool patternMatches = IsPattern(text, prefix, matches);
                    if (patternMatches)
                    {
                        shortestPatternLength = prefixLength;
                        shortestPatternMatches = matches;
                    }
                //}
            }

            if (shortestPatternMatches == null)
            {
                string prefix = text.Substring(0, shortestPatternLength);
                shortestPatternMatches = StringMatching.KMP(text, prefix, StringMatching.ComputeP(prefix));
            }

            indexes = shortestPatternMatches.ToArray();
            return shortestPatternLength;
        }

        private static bool IsPattern(string text, string prefix, List<int> matches)
        {
            for (int i = 1; i < matches.Count; i++)
            {
                if (matches[i] - matches[i - 1] > prefix.Length)
                    return false;
            }

            if (matches.Last() + prefix.Length < text.Length)
                return false;

            return true;
        }

        /// <summary>
        /// Badanie równoważności cyklicznej z wykorzystaniem algorytmu KMP
        /// </summary>
        /// <param name="text1">Pierwszy badany tekst</param>
        /// <param name="text2">Drugi badany tekst</param>
        /// <returns>
        /// Jeśli teksty nie są cyklicznie równoważne zwraca null
        /// Jeśli teksty są cyklicznie równoważne zwraca liczbę znaków,
        ///    które trzeba przenieść z początku pierwszego tekstu na koniec aby uzyskać drugi tekst
        /// </returns>
        public static int? CyclicEquivalenceKMP(string text1, string text2)
        {
            int n = text1.Length;

            if (n != text2.Length)
                return null;

            if (text1 == text2)
                return 0;

            int[] P = StringMatching.ComputeP(text1 + text2);
            if (P.Last() == 0)
                return null;
            return P.Last();
        }

        /// <summary>
        /// Bezposrednie badanie równoważności cyklicznej
        /// </summary>
        /// <param name="text1">Pierwszy badany tekst</param>
        /// <param name="text2">Drugi badany tekst</param>
        /// <returns>
        /// Jeśli teksty nie są cyklicznie równoważne zwraca null
        /// Jeśli teksty są cyklicznie równoważne zwraca liczbę znaków,
        ///    które trzeba przenieść z początku pierwszego tekstu na koniec aby uzyskać drugi tekst
        /// </returns>
        public static int? CyclicEquivalenceDirect(string text1, string text2)
        {
            int i,
                j,
                n = text1.Length,
                k = 0;

            if (n != text2.Length)
                return null;

            i = j = 0;

            while (i < n && j < n && k <= n)
            {
                for (k = 0; k < n; k++)
                {
                    if (text1[(i + k) % n] != text2[(j + k) % n])
                        break;
                }
                k++;

                if (k <= n)
                {
                    if (text1[(i + k) % n] > text2[(j + k) % n])
                        i += k;
                    else
                        j += k;
                }
            }

            if (k <= n)
                return null;

            return (i + (n - j)) % n;
        }
    }

}

