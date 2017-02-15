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
    /// Rozwiązuje łamigłówkę Kankei. Wszystkie możliwe rozwiązania zapisuje w polu solutions.
    /// Badane są tylko pola przyległe do pól już wypełnionych.
    /// 
    /// Rozwiązanie jest rekurencyjne.
    /// Schemat działania:
    /// 1. Sprawdzenie kompletności rozwiązania (zatrzymanie rekurencji,
    ///     zapis poprawnych rozwiązań).
    /// 2. Wybór kolejnego elementu, którego wartość będzie wyznaczana.
    ///     a. Pryzpisanie mu wartości true:
    ///         - sprawdzenie, czy ograniczenie na ilość elementów jest spełnione
    ///         - sprawdzenie, czy nie powstał za duży wypełniony obszar
    ///         - wywołanie rekurencyjne z tak powiększonym rozwiązaniem
    ///     b. Przypisanie mu wartości false:
    ///         - sprawdzenie, czy nie został zablokowany obszar o polu < 5
    ///         - wywołanie rekurencyjne z tak powiększonym rozwiązaniem
    /// </summary>
    public void Solve()
    {
        Queue<int> open = new Queue<int>();
        solutions = new List<bool?[,]>();
        for (int x = 0; x < size; x++)
            for (int y = 0; y<size; y++)
                if (board[x, y] != null && board[x, y] == true)
                    EnqueueNeighbors(x, y, open);
        Solve(open);
    }
    /// <summary>
    /// Pomocnicza funkcja rekurencyjna.
    /// </summary>
    /// <param name="open">zawiera nierozpatrzone pola sąsiadujące z pełnymi polami</param>
    void Solve(Queue<int> open)
    {
        //1. Sprawdzenie czy rozwiązanie jest kompletne.

        // wykorzystac metode ValidateSolution()
        //    jesli zwroci true dodac rozwiazanie opisane przez aktualny board do listy solutions
        // sprawdzic tez czy kolejka open nie jest pusta
        //    jesli pusta to powrot

        // wyjac element z kolejki open i rozlozyc na wspolzedne x i y

        //2. Przypisanie polu v wszystkich możliwych wartości (czyli true i false)

        //2.a true

        // ustawic odpowiedni element board na true

        // wykluczanie rozwiązań:
        // sprawdzenie, czy liczniki kolumn i wierszy nie zostały przekorczone
        // i czy czy spójny obszar nie ma za dużego pola
        // wykorzystac metody 
        //    CheckSum
        // i 
        //    int FloodArea(int x, int y, out bool isOpen)
        
        // jesli rozwiazanie czastkowe jest poprawne
        // utworzyc nowa kolejke sasiadow
        //    i wywolac metode Solve(Queue<int> nopen) rekurencyjnie  

        //2.b false

        // ustawic odpowiedni element board na false

        // WAZNA OPTYMALIZACJA 
        // sprawdzic, czy jakis zapełniany obszar nie został zablokowany zanim osiągnął rozmiar 5
        // jesli tak to return
        // optymalizacja jest wazna dla duzej planszy !!!
        // dla malych mozna pominac

        // wywolac metode Solve(Queue<int> open) rekurencyjnie  

        // ustawic odpowiedni element board na null
    }




    /// <summary>
    /// Umieszcza na kolejce q wszystkie nierozpatrzone pola sąsiadujące z podanym.
    /// </summary>
    /// <param name="x">indeks wiersza</param>
    /// <param name="y">indeks kolumny</param>
    /// <param name="q">kolejka, na której umieszczeni będą sąsiedzi</param>
    /// <remarks> elementy w kolejce reprezentowane sa jako v = size * x + y </remarks>
    void EnqueueNeighbors(int x, int y, Queue<int> q)
    {
    return;
    }



    /// <summary>
    /// Sprawdza, czy częściowe rozwiązanie w tablicy board nie przekracza
    /// ograniczeń dla danego wiersza i kolumny.
    /// </summary>
    /// <param name="x">indeks wiersza</param>
    /// <param name="y">indeks kolumny</param>
    /// <returns>true jesli ograniczenia nie sa przekroczone</returns>
    bool CheckSum(int x, int y)
    {
        return true;
    }




}