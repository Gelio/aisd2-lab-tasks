using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASD.Graph;

namespace MatrixRounding
{
    /// <summary>
    /// W domu dokończenie - 2 pkt.
    /// </summary>
    public static class CirculationGraphExtender
    {
        /// <summary>
        /// 1 pkt
        /// Metoda znajduje maksymalną cyrkulacje w grafie. Cyrkulacja różni się od przepływu 
        /// tym, że każdy wierzchołek ma przypisaną dodatkową wartość mówiącą czy w danym wierzchołku jest zachowany przepływ.
        /// Formalniej niech:
        /// d: V(zbiór wierzchołków) -> N (zbiór liczb naturalnych) - zapotrzebowania
        /// w: E (zbiór krawędzi) -> N - przepustowości
        /// Cyrkulacja jest funkcją f: E -> N taką, że 
        ///     1. dla każdej krawędzi e: f(e) <= w(e)
        ///     2. dla każdego wierzchołka v:
        ///         (suma po krawędziach e wchodzących do v z f(e)) - (suma po krawędziach e wychodzących z v z f(e)) = d(v)
        /// </summary>
        /// <param name="graph">Graf w którym poszukujemy cyrkulacji</param>
        /// <param name="demands">wartości d(v)</param>
        /// <param name="circulation">wynikowa cyrkulacja. Jeśli nie istnieje to null</param>
        /// <returns>true wtedy i tylko wtedy gdy w danym grafie istnieje cyrkulacja</returns>
        /// <remarks>
        /// Cyrkulację można policzyć korzystając ze standardowego algorytmu do liczenia maksymalnego przepływu.
        /// Należy stworzyć nową sieć taką samą jak wyjściowa z dodanymi dwoma wierzchołkami (dla ustalenia uwagi nazwijmy je 's' i 't')
        /// Dla każdego wierzchołka v
        ///     jeśli d(v) == 0 to nic nie robimy
        ///     jeśli d(v) > 0 to dodajemy krawędź od v do t o wadze d(v)
        ///     jeśli d(v) < 0 to dodajemy krawędź od s do v o wadze -d(v)
        /// Następnie liczymy maksymalny przepływ w tak stworzonej sieci.
        /// Jeśli wartość maksymalnego przepływu jest równa sumie wag krawędzi wychodzących z s 
        /// to istnieje cyrkulacja w wyjściowym grafie wpp. cyrkulacja nie istnieje.
        /// Dodatkowo maksymalny przepływ w stworzonej sieci po usunięciu s i t jest szukaną cyrkulacją.
        /// UWAGA: zwracany graf nie może zawierać wierzchołków s i t
        /// </remarks>
        public static bool FindCirculation(this IGraph graph, int[] demands, out IGraph circulation)
        {
            circulation = null;
            return false;
        }

        /// <summary>
        /// 2 pkt.
        /// Funkcja ta oblicza cyrkulacje z dodatkowym założeniem, że krawędzie mogą mieć zdefiniowany minimalny przepływ 
        /// </summary>
        /// <param name="graph">Graf w którym poszukujemy cyrkulacji</param>
        /// <param name="demands">wartości d(v)</param>
        /// <param name="lowerBounds">
        /// Dolne ograniczenia na przepustowość. 
        /// Jeśli istnieje krawędź w tym grafie to jej waga jest dolnym ograniczeniem dla analogicznej krawędzi w grafie graph.
        /// Jeśli krawędź nie istnieje to znaczy, że nie ma dolnego ograniczenia na daną krawędź.        
        /// </param>
        /// <param name="circulation">wynikowa cyrkulacja. Jeśli nie istnieje to null</param>
        /// <returns>true wtedy i tylko wtedy gdy w danym grafie istnieje cyrkulacja</returns>
        /// <remarks>
        /// Cyrkulacje z dolnymi ograniczeniami można łatwo obliczyć za pomocą funkcji liczącej cyrkulację.
        /// Dla każdej krawędzi e=vw, dla której istnieje dolne ograniczenie b należy zwiększyć d(v) o b oraz zmniejszyć d(w) o b 
        ///     oraz zmniejszyć przepustowość e o b.
        /// W tak zmodyfikowanym grafie zwykła cyrkulacja jest bardzo podobna do cyrkulacji z dolnymi ograniczeniami.
        /// Należy wykonać tylko jedną prosta operację.
        /// UWAGA: Biblioteka graph nie pozwala liczyć maksymalnego przepływu w grafie w którym krawędzie mają zerową przepustowość.
        ///        Jeśli modyfikacja przepustowości krawędzi doprowadzi do wartości 0 należy taką krawędź usunąć (a później oczywiście dodać).
        /// </remarks>
        public static bool FindCirculationWithLowerBounds(this IGraph graph, int[] demands, IGraph lowerBounds, out IGraph circulation)
        {
            circulation = null;
            return false;
        }

        /// <summary>
        /// 2 pkt.
        /// Funkcja znajduje zaokrąglenie macierzy. Zaokrąglenie Z macierzy M ma następujące właściwości:
        ///     1. dla każedgo i,j Z[i,j] >= Podłoga(M[i,j]) i Sufit(M[i,j]) >= Z[i,j]
        ///     2. dla każdego wiersza suma wiersza zachowuje właściwość 1. 
        ///         Formalnie dla każdego wiersza i SumaWiersza(Z, i) >= Podłoga(SumaWiersza(M, i)) 
        ///             i Sufit(SumaWiersza(M, i)) >= SumaWiersza(Z, i)
        ///     3. dla każdej kolumny suma kolumny zachowuje właściwość 1. 
        ///         Formalnie dla każdej kolumny i SumaKolumny(Z, i) >= Podłoga(SumaKolumny(M, i)) 
        ///             i Sufit(SumaKolumny(M, i)) >= SumaKolumny(Z, i)
        /// </summary>
        /// <param name="matrix">Macierz do zaokrąglenia</param>
        /// <returns>Zaokrąglona macierz</returns>
        /// <remarks>
        /// Zaokrąglenie można obliczyć znajdując cyrkulację z dolnymi ograniczeniami w pewnej specyficznej sieci.
        /// Sieć składa się z wierzchołków s i t oraz po jednym wierzchołku dla każdego wiersza i jednym dla każdej kolumny.
        /// Wierzchołek s jest połączony ze wszystkimi wierzchołkami odpowiadającymi wierszom a t z wierzcholkami odpowiadającymi kolumnom.
        /// Dodatkowo każdy wiersz jest połączony z każdą kolumną oraz t z s krawędzią o bardzo dużej przepustowości.
        /// Należy tylko odpowiednio dobrać ograniczenia górne i dolne oraz zinterpretować wynik.
        /// Wskażówka: Dolne i górne ograniczenia powinny być odpowiedni zaokrąglenim w dół i w górę wartości macierz i sum tyc wartości.
        /// </remarks>
        public static int[,] RoundMatrix(double[,] matrix)
        {
            return new int[matrix.GetLength(0),matrix.GetLength(1)];
        }
    }
}
