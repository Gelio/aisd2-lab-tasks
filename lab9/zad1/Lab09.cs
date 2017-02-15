
namespace ASD2
{
    static partial class Sequences
    {      

        /// <summary>
        /// Znajduje najdłuższy wspólny (niekoniecznie spójny) podciąg zadanych ciągów.
        /// Algorytm powinien działać w czasie O(n*m),
        /// gdzie n i m oznaczają odpowiednio długości ciągów x i y.
        /// </summary>
        /// <param name="x">Pierwszy ciąg</param>
        /// <param name="y">Drugi ciąg</param>
        /// <param name="lcs">Znaleziony najdłuższy podciąg</param>
        /// <returns>Długość najdłuższego podciągu</returns>
        /// <remarks>
        /// Wskazówka:
        /// Niech x[0...n-1] i y[0...m-1] będą wejściowymi ciągami.
        /// Jeśli x[n-1]==y[m-1], to szukany pociąg ma postać (S,x[n-1]),
        /// gdzie S jest najdłuższym wspólnym podciągiem ciągów x[0...n-2] i y[0...m-2].
        /// W przeciwnym przypadku należy zbadać dwa przypadki:
        /// a) najdłuższy wspólny podciąg ciągów x[0...n-1] i y[0...m-2]
        /// b) najdłuższy wspólny podciąg ciągów x[0...n-2] i y[0...m-1]
        /// a następnie wybrać dłuższy z nich.
        /// 
        /// Prosta procedura rekurencyjna ma złożoność wykładniczą,
        /// ale dzięki zastosowaniu programowania dynamicznego możemy osiągnąć
        /// złożoność O(n*m).
        /// 
        /// Punktacja:  2 pkt.
        /// </remarks>
        public static int FindLongestCommonSubsequence(int[] x, int[] y, out int[] lcs)
        {
            lcs=null;
            return 0;
        }


        /// <summary>
        /// Znajduje segment (spójny podciąg) zadanego ciągu o największej sumie elementów.
        /// Algorytm powinien działać w czasie O(n), gdzie n jest długością ciągu x
        /// Zakładamy, że ciąg pusty ma sumę 0.
        /// </summary>
        /// <param name="x">Ciąg wejściowy</param>
        /// <param name="segStart">Indeks pierwszego elementu segmentu</param>
        /// <param name="segEnd">Indeks ostatniego elementu segmentu +1</param>
        /// <returns>Suma segmentu</returns>
        /// <remarks>
        /// Wskazówka:
        /// Przeglądając ciąg należy zapamiętywać znaleziony dotąd segment o maksymalnej sumie.
        /// 
        /// Punktacja:  1 pkt.
        /// </remarks>
        public static int FindMaxSumSegment(int[] x, out int segStart, out int segEnd)
        {
            segStart = segEnd = 0;
            return 0;
        }


        /// <summary>
        /// Znajduje najdłuższy (niekoniecznie spójny) podciąg roznący zadanego ciągu.
        /// Algorytm powinien działać w czasie O(n^2), gdzie n oznacza długość ciągu x.
        /// </summary>
        /// <param name="x">Ciąg wejściowy</param>
        /// <param name="las">Znaleziony najdłuższy podciąg rosnący</param>
        /// <returns>Długość najdłuższego podciągu rosnącego</returns>
        /// <remarks>
        /// Wskazówka:
        /// Dla każdego elementu pamiętać długość najdluższego podciągu rosnącego kończącego się na tym elemencie.
        /// 
        /// Punktacja:  2 pkt.
        /// </remarks>
        public static int FindLongestAscendingSubsequence(int[] x, out int[] las)
        {
            las=null;
            return 0;
        }

    }
}
