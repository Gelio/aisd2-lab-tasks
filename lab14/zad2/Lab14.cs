
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
            int shortestPattern = -1;
            List<int> shortestPatternMatches = null;
            int previousPrefixLength = -1;
            foreach (int prefixLength in P.OrderByDescending(x => x))
            {
                if (prefixLength == previousPrefixLength)
                    continue;
                previousPrefixLength = prefixLength;
                if (prefixLength == 0)
                    break;

                List<int> patternMatches = IsPattern(text, text.Substring(0, prefixLength));
                if (patternMatches != null)
                {
                    shortestPattern = prefixLength;
                    shortestPatternMatches = patternMatches;
                }
            }

            if (shortestPattern == -1)
            {
                indexes = new int[0];
                return 0;
            }

            indexes = shortestPatternMatches.ToArray();
            return shortestPattern;
        }

        private static List<int> IsPattern(string text, string prefix)
        {
            int[] prefixP = StringMatching.ComputeP(prefix);
            List<int> matches = StringMatching.KMP(text, prefix, prefixP);

            for (int i = 1; i < matches.Count; i++)
            {
                if (matches[i] - matches[i - 1] > prefix.Length)
                    return null;
            }

            if (matches.Last() + prefix.Length < text.Length)
                return null;

            return matches;
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
            return null;
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
            return null;
        }

    }

}

