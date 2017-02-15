using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zad12
{

partial class Editorial
    {
        /// <summary>
        /// Funkcja znajdująca odległość edycyjną między dwoma tekstami z kosztami operacji.
        /// Każdej z elementarnych operacji edytorskich (wstawienie znaku, usunięcie znaku,
        /// zamiana znaku) jest przypisany jej koszt.
        /// Naszym celem jest znalezienie ciągu przekształceń za pomocą elementarnych operacji
        /// edycyjnych przekształcającego tekst wejściowy na wyjściowy o najmniejszym łącznym koszcie.
        /// By znaleźć taki ciąg, używamy programowania dynamicznego.
        /// Odległość edycyjna tesktów t1[0..i] i t2[0..j] jest równa:
        /// 1) jeśli t1[i] == t2[j] i t1[i] != t2[j-1]: odległości edycyjnej tekstów t1[0..i-1] i t2[0..j-1]
        /// 2) w przeciwnym przypadku minimum z odległości:
        ///     a) odległości między tekstem t1[0..i-1] a t2[0..j] + koszt usunięcia znaku
        ///     b) odległości między tekstem t1[0..i]t2[j] a t2[0..j] + koszt wstawienia znaku  //xy oznacza konkatenację tekstów z i y
        ///     c) odległości między tekstem t1[0..i-1]t2[j] a t2[0..j] + koszt zamiany znaku
        ///     d) odległości między tekstem t1[0..i-2]t2[j-1]t2[j] a t2[0..j] + koszt transpozycji znakow //gdy t1[i-1] == t2[j] && t1[i] == t2[j-1]
        ///     e) odległości między tekstem t1[0..i]t1[i] a t2[0..j] + koszt podwojenia znaku // gdy t1[i] == t2[j] == t2[j-1]
        /// Koszty określone sa  w partametrach wejściowych.
        /// </summary>
        /// <param name="text1">Tekst wejściowy</param>
        /// <param name="text2">Tekst, który chcemy otrzymać na wyjściu</param>
        /// <param name="costs">Koszty przypisane operacjom</param>
        /// <param name="changes">Liczba zmian, których należy dokonać, by przekształcić tekst wejściowy na wyjściowy</param>
        /// <param name="cost">Łączny koszt przekształcenia tekstu</param>
        /// <returns>
        /// Lista kolejnych wersji tekstu wejściowego przy przekształcaniu go na tekst wyjściowy.
        /// Pierwszym elementem listy jest tekst wejściowy, ostatnim tekst wyjściowy.
        /// i-ty element listy to tekst wejđciowz po wykonaniu i zmian.
        /// </returns>
        /// <remarks>
        /// Ze względu na możliwe różne koszty operacji dodawania i odejmowania znaku,
        /// odległość edycyjna nie musi być symetryczna
        /// </remarks>
    static List<String> GetEditorialDistance(string text1, string text2, int[] costs, out int changes, out int cost)
        {
        /* ZMIENIĆ */ changes = cost = 0;
        /* ZMIENIĆ */ return new List<string>();
        }

    // funkcja pomocnicza - można użyć lub nie
    static int Min(params int[] a)
        {
        int min=a[0];
        for ( int i=1 ; i<a.Length ; ++i )
            if ( min>a[i] ) min=a[i];
        return min;
        }

    // funkcja pomocnicza - można użyć lub nie (albo nawet dowolnie przerobić)
    static ChangeType BestOper(int none, int remove, int add, int change, int transpose, int mult)
        {
        int min = Min(none, remove, add, change, transpose, mult);
        if ( min==remove) return ChangeType.RemoveChar;
        if ( min==add) return ChangeType.AddChar;
        if ( min==change) return ChangeType.ChangeChar;
        if ( min==transpose) return ChangeType.TransposeChar;
        if ( min==mult ) return ChangeType.MultiplyChar;
        return ChangeType.None;
        }

    }

}
