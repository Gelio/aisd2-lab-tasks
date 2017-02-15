using ASD.Graph;

namespace zadanie8
{
    static class ColoringExtension
    {
        
        /*
         * Strategia Simple Greedy.
         * Kolorujemy wierzchołki w kolejności rosnących indeksów.
         */
        public static int[] SimpleGreedy(this IGraph g)
        {
            //uzupełnij
            return null;
        }

        /*
         * Strategia Smallest Last.
         * 1. Zainicjuj L jako pusty ciąg.
         * 2. Wybierz wierzchołek v najmniejszego stopnia w grafie
         *      (jeśli jest ich kilka, wybierz ten o najmniejszym numerze).
         * 3. Wstaw v na początek L.
         * 4. Usuń v z grafu.
         * Uwaga: Oczywiście, tak naprawdę, grafu nie wolno zmienić !
         */
        public static int[] SmallestLast(this IGraph g)
        {
            //uzupełnij
            return null;
        }

        /*
         * Strategia DSatur.
         * W każdym kroku znajdujemy saturację każdego wierzchołka,
         * tj. liczbę zablokowanych kolorów.
         * Następnie kolorujemy wierzchołek o największej saturacji
         * (jeśli jest ich kilka, wybierz ten o najmniejszym numerze).
         */
        public static int[] DSatur(this IGraph g)
        {
            int n = g.VerticesCount;
            bool[,] blocked = new bool[n, n + 1]; // blocked[v,c] == true oznacza ze dla wierzcholka v kolor c jest zablokowany
            // uzupełnij
            return null;
        }

        /*
         * Strategia inkrementująca.
         * W każdym kroku wybieramy zachłannie maksymalny (w sensie inkluzji)
         * zbiór, który możemy pokolorować kolejnym kolorem.
         * Zbiór ma być najmniejszy w porządku leksykograficznym.
         */
        public static int[] Incremental(this IGraph g)
        {
            //uzupełnij
            return null;
        }
    }
}
