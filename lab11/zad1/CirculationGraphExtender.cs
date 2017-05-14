using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASD.Graphs;

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
        public static bool FindCirculation(this Graph graph, int[] demands, out Graph circulation)
        {
            int n = graph.VerticesCount;
            Graph circulationCapacity = graph.IsolatedVerticesGraph(true, graph.VerticesCount + 2);
            int s = n;
            int t = n + 1;

            int sourceCirculation = 0;
            for (int v = 0; v < n; v++)
            {
                foreach (Edge e in graph.OutEdges(v))
                    circulationCapacity.AddEdge(e);
                if (demands[v] > 0)
                    circulationCapacity.AddEdge(v, t, demands[v]);
                else if (demands[v] < 0)
                {
                    sourceCirculation -= demands[v];
                    circulationCapacity.AddEdge(s, v, -demands[v]);
                }
            }

            double circulationValue = circulationCapacity.FordFulkersonDinicMaxFlow(s, t, out Graph circulationFlow, MaxFlowGraphExtender.BFPath);
            if (circulationValue != sourceCirculation)
            {
                circulation = null;
                return false;
            }

            circulation = graph.IsolatedVerticesGraph();
            for (int v = 0; v < n; v++)
            {
                foreach (Edge e in circulationFlow.OutEdges(v))
                {
                    if (e.Weight == 0)
                        continue;
                    if (e.To == s || e.To == t)
                        continue;

                    circulation.AddEdge(e);
                }
            }

            return true;
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
        public static bool FindCirculationWithLowerBounds(this Graph graph, int[] demands, Graph lowerBounds, out Graph circulation)
        {
            int n = graph.VerticesCount;
            List<Edge> edgesToRestore = new List<Edge>();
            Graph graphClone = graph.Clone();
            int[] newDemands = demands.Clone() as int[];

            for (int v = 0; v < n; v++)
            {
                foreach (Edge e in lowerBounds.OutEdges(v))
                {
                    newDemands[e.From] += Convert.ToInt32(e.Weight);
                    newDemands[e.To] -= Convert.ToInt32(e.Weight);
                    graphClone.ModifyEdgeWeight(e.From, e.To, -e.Weight);
                    if (graphClone.GetEdgeWeight(e.From, e.To) <= 0)
                    {
                        edgesToRestore.Add(e);
                        graphClone.DelEdge(e);
                    }
                }
            }

            bool circulationFound = graphClone.FindCirculation(newDemands, out circulation);
            if (!circulationFound)
                return false;

            for (int v = 0; v < n; v++)
            {
                foreach (Edge e in lowerBounds.OutEdges(v))
                {
                    if (!circulation.GetEdgeWeight(e.From, e.To).IsNaN())
                        circulation.ModifyEdgeWeight(e.From, e.To, e.Weight);
                    else
                        circulation.AddEdge(e);
                }
            }

            return true;
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
            int rows = matrix.GetLength(0),
                columns = matrix.GetLength(1);

            Graph g = new AdjacencyMatrixGraph(true, rows + columns + 2);
            int s = 0;
            int t = 1;
            // 2, ..., 2 + rows - 1 - rows vertices
            // 2 + rows, ..., 2 + rows + columns - 1 - column vertices

            double[] rowSums = new double[rows];
            double[] columnSums = new double[columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                    rowSums[i] += matrix[i, j];

                g.AddEdge(s, 2 + i, Math.Ceiling(rowSums[i]) - rowSums[i]);
            }
            for (int j = 0; j < columns; j++)
            {
                for (int i = 0; i < rows; i++)
                    columnSums[j] += matrix[i, j];

                g.AddEdge(2 + rows + j, t, Math.Ceiling(columnSums[j]) - columnSums[j]);
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                    g.AddEdge(2 + i, 2 + rows + j, int.MaxValue);
            }

            g.AddEdge(t, s, int.MaxValue);
            return new int[rows, columns];
        }
    }
}
