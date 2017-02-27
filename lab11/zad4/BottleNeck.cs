
using ASD.Graphs;

public static class BottleNeckExtender
    {

    /// <summary>
    /// Wyszukiwanie "wąskich gardeł" w sieci przesyłowej
    /// </summary>
    /// <param name="g">Graf przepustowości krawędzi</param>
    /// <param name="c">Graf kosztów rozbudowy sieci (kosztów zwiększenia przepustowości)</param>
    /// <param name="p">Tablica mocy produkcyjnych/zapotrzebowania w poszczególnych węzłach</param>
    /// <param name="flowValue">Maksymalna osiągalna produkcja (parametr wyjściowy)</param>
    /// <param name="cost">Koszt rozbudowy sieci, aby możliwe było osiągnięcie produkcji flowValue (parametr wyjściowy)</param>
    /// <param name="flow">Graf przepływu dla produkcji flowValue (parametr wyjściowy)</param>
    /// <param name="ext">Tablica rozbudowywanych krawędzi (parametr wyjściowy)</param>
    /// <returns>
    /// 0 - zapotrzebowanie można zaspokoić bez konieczności zwiększania przepustowości krawędzi<br/>
    /// 1 - zapotrzebowanie można zaspokoić, ale trzeba zwiększyć przepustowość (niektórych) krawędzi<br/>
    /// 2 - zapotrzebowania nie można zaspokoić (zbyt małe moce produkcyjne lub nieodpowiednia struktura sieci
    ///     - można jedynie zwiększać przepustowości istniejących krawędzi, nie wolno dodawać nowych)
    /// </returns>
    /// <remarks>
    /// Każdy element tablicy p opisuje odpowiadający mu wierzchołek<br/>
    ///    wartość dodatnia oznacza moce produkcyjne (wierzchołek jest źródłem)<br/>
    ///    wartość ujemna oznacza zapotrzebowanie (wierzchołek jest ujściem),
    ///       oczywiście "możliwości pochłaniające" ujścia to moduł wartości elementu<br/>
    ///    "zwykłym" wierzchołkom odpowiada wartość 0 w tablicy p<br/>
    /// <br/>
    /// Jeśli funkcja zwraca 0, to<br/>
    ///    parametr flowValue jest równy modułowi sumy zapotrzebowań<br/>
    ///    parametr cost jest równy 0<br/>
    ///    parametr ext jest pustą (zeroelementową) tablicą<br/>
    /// Jeśli funkcja zwraca 1, to<br/>
    ///    parametr flowValue jest równy modułowi sumy zapotrzebowań<br/>
    ///    parametr cost jest równy sumarycznemu kosztowi rozbudowy sieci (zwiększenia przepustowości krawędzi)<br/>
    ///    parametr ext jest tablicą zawierającą informację o tym o ile należy zwiększyć przepustowości krawędzi<br/>
    /// Jeśli funkcja zwraca 2, to<br/>
    ///    parametr flowValue jest równy maksymalnej możliwej do osiągnięcia produkcji
    ///      (z uwzględnieniem zwiększenia przepustowości)<br/>
    ///    parametr cost jest równy sumarycznemu kosztowi rozbudowy sieci (zwiększenia przepustowości krawędzi)<br/>
    ///    parametr ext jest tablicą zawierającą informację o tym o ile należy zwiększyć przepustowości krawędzi<br/>
    /// Uwaga: parametr ext zawiera informacje jedynie o krawędziach, których przepustowości trzeba zwiększyć
    //     (każdy element tablicy to opis jednej takiej krawędzi)
    /// </remarks>
    public static int BottleNeck(this Graph g, Graph c, int[] p, out int flowValue, out int cost, out Graph flow, out Edge[] ext)
        {
        flowValue =-1;  // ZMIENIĆ 
        cost = -1;      // ZMIENIĆ
        flow = null;    // ZMIENIĆ
        ext = null;     // ZMIENIĆ
        return -1;      // ZMIENIĆ
        }

    }
