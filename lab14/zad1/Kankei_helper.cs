using System.Collections.Generic;
using System.Text;


/// <summary>
/// Opisuje łamigłówkę Kankei.
/// Plansza jest kwadratem, zawiera pola które mogą być wypełnione lub nie. Pola wypełnione muszą
/// tworzyć spójne obszary o polu 5. Oprócz tego dla każdego wiersza i każdej kolumny podana jest
/// dokładna ilość zawartych w niej pełnych elementów.
/// Na początku plansza zawiera pola o wartości null (nieokreślonej) i kilka pól o wartości true
/// (co najmniej jedno pole z każdego obszaru jest zaznaczone). Podczas rozwiązywania łamigłówki
/// należy wypełnić całą planszę wartościami true/false tak, aby były spełnione powyższe warunki
/// (pole == 5, liczba elementów w wierszu i kolumnie).
/// </summary>
partial class Kankei
{
    /// <summary>
    /// Wymiar tablicy board (rozmiar zadania).
    /// </summary>
    int size;
    /// <summary>
    /// Zawiera informację o wymaganej liczbie wypełnionych elementów
    /// w i-tym wierszu (counts[i,0]) i w i-tej kolumnie (counts[i,1])
    /// </summary>
    int[,] counts;
    /// <summary>
    /// Opisuje planszę. Możliwe wartości:
    /// null - pole jeszcze nie określone
    /// false - puste pole
    /// true - pełne pole
    /// </summary>
    bool?[,] board;

    List<bool?[,]> solutions;
    /// <summary>
    /// Lista zawierająca wszystkie możliwe rozwiązania.
    /// </summary>
    public List<bool?[,]> Solutions
    {
        get
        {
            if (solutions == null)
                Solve();
            return solutions;
        }
    }

    public Kankei( bool?[,] board, int[,] counts)
    {
        this.board = board;
        this.counts = counts;
        this.size = board.GetLength(0);
    }
    public Kankei(bool?[,] board, int[] rows, int[] columns)
    {
        this.board = board;
        this.size = board.GetLength(0);
        this.counts = new int[size, 2];
        for (int i = 0; i < size; i++)
        {
            counts[i, 0] = rows[i];
            counts[i, 1] = columns[i];
        }
    }


 



    /// <summary>
    /// Sprawdza, czy dane zawarte w tablicy board spełniają warunki poprawnego rozwiązania.
    /// Jeśli tak, zwraca true
    /// </summary>
    bool ValidateSolution()
    {
        //sprawdzenie pól obszarów
        bool[,] counted = new bool[size, size]; //tablica do FloodFill (unikam wielokrotnego liczenia pól)
        for (int i = 0;i<size;i++)
            for (int j = 0;j<size;j++)
                if (board[i, j]==true)
                {
                    bool isOpen;
                    int pole = FloodArea(i, j, counted, out isOpen);
                    if (pole != 0 && pole != 5)
                        return false;
                }
        return true;
    }


    /// <summary>
    /// Oblicza pole obszaru zapełnionego wartością true w tablicy board algorytmem
    /// FloodFill, zaczynając od podanej komórki.
    /// </summary>
    /// <param name="x">indeks wiersza</param>
    /// <param name="y">indeks kolumny</param>
    /// <returns>pole obszaru</returns>
    int FloodArea(int x, int y, out bool isOpen)
    {
        bool[,] counted = new bool[size, size];
        return FloodArea(x, y, counted, out isOpen);
    }
    /// <summary>
    /// Oblicza pole obszaru zapełnionego wartością true w tablicy board algorytmem
    /// FloodFill, zaczynając od podanego indeksu.
    /// </summary>
    /// <param name="i">nr wiersza</param>
    /// <param name="j">nr kolumny</param>
    /// <param name="counted">tablica określająca które elementy były już zliczone</param>
    /// <returns>pole obszaru</returns>
    int FloodArea(int i, int j, bool[,] counted, out bool isOpen)
    {
        isOpen = false;
        if (i < 0 || i >= size || j < 0 || j >= size || counted[i, j])
            return 0;
        counted[i, j] = true;
        if (board[i, j] == null)
        {
            isOpen = true;
            return 0;
        }
        if ( board[i, j] == false)
            return 0;
        int ret = 1;
        bool tmp;
        ret += FloodArea(i - 1, j, counted, out tmp);
        isOpen = isOpen || tmp;
        ret += FloodArea(i + 1, j, counted, out tmp);
        isOpen = isOpen || tmp;
        ret += FloodArea(i, j - 1, counted, out tmp);
        isOpen = isOpen || tmp;
        ret += FloodArea(i, j + 1, counted, out tmp);
        isOpen = isOpen || tmp;
        return ret;
    }


    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("   \t");
        for (int j = 0; j < size; j++)
            sb.AppendFormat("({0}) ", counts[j, 1]);
        sb.AppendLine();
        for (int i = 0; i < size; i++)
        {
            sb.AppendFormat("({0})\t", counts[i, 0]);
            for (int j = 0; j < size; j++)
                sb.AppendFormat(" {0}  ", board[i, j] == null ? ' ' : (board[i, j].Value ? '1' : '0'));
            sb.AppendLine();
        }
        return sb.ToString();
    }


}