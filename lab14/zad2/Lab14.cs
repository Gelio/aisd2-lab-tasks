
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
        indexes = new int[1] {0};
        return text.Length;
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

