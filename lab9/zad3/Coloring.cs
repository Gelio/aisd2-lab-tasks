
namespace ASD.Graph
{

public static class ColoringExtender
    {

    // koloruje graf algorytmem zachlannym (byc moze niepotymalnie)
    public static int GreedyColor(this IGraph g, out int[] colors)
        {
        // kazdemu wierzcholkowi 
        // przydzielamy najmniejszy kolor nie kolidujacy z juz pokolorowanymi sasiadami
        // (wpisujemy go do tablicy colors)
        // zwracamy liczbe uzytych kolorow

        /* ZMIENIC */ int k=g.VerticesCount;
        /* ZMIENIC */ colors=new int[k];
        /* ZMIENIC */ for ( int i=0 ; i<k ; ++i )
        /* ZMIENIC */     colors[i]=i+1;
        /* ZMIENIC */ return k;
        }

    // koloruje graf algorytmem z powrotami (optymalnie)
    public static int BacktrackingColor(this IGraph g, out int[] colors)
        {
        var gc = new Coloring(g);
        gc.Color(0,new int[g.VerticesCount],0);
        colors=gc.bestColors;
        return gc.bestColorsNumber;
        }

    // klasa pomocnicza dla algorytmu z powrotami
    private sealed class Coloring
        {
        
        // tablica pamietajaca najlepsze dotychczas znalezione pokolorowanie
        internal int[] bestColors=null;

        // zmienna pamietajaca liczbe kolorow w najlepszym dotychczas znalezionym pokolorowaniu
        internal int bestColorsNumber;

        // badany graf
        private IGraph g;
        
        // konstruktor
        internal Coloring(IGraph g)
            {
            this.g=g;
            bestColorsNumber=g.VerticesCount+1;
            }

        // rekurencyjna metoda znajdujaca najlepsze pokolorowanie
        // v - wierzcholek do pokolorowania
        // colors - tablica kolorow
        // n - liczba kolorow uzytych w pokolorowaniu zapisanym w colors
        internal void Color(int v ,int[] colors, int n)
            {
            // tu zaimplementowac algorytm z powrotami

            /* ZMIENIC */ int k=g.VerticesCount;
            /* ZMIENIC */ bestColors=new int[k];
            /* ZMIENIC */ for ( int i=0 ; i<k ; ++i )
            /* ZMIENIC */     bestColors[i]=i+1;
            /* ZMIENIC */ bestColorsNumber=k;
            }

        }  // class Coloring

    }  // class ColoringExtender

}  // namespace ASD.Graph
