using System;

namespace ASD
{

    class HashTable
    {
        // klucze w tablicy sa unikalne i nieujemne

        private const int empty = -1;
        private const int deleted = -2;

        private double alfa; // wspolczynnik wypelnienia przy ktorym tablica jest dwukrotnie powiekszana
        private int elemCount;
        private int accessCount;
        private int[] table;
        private Func<int, int, int> hash;   // podstawowa funkcja haszujaca
        private Func<int, int, int> shift;  // funkcja przesuniecia wywolywana w razie konfliktu, drugi parametr - numer iteracji

        // domyslny rozmiar poczatkowy 8
        // domyslny wspolczynnik zapelnienia powodujacy podwojenie rozmiaru 7/8
        public HashTable(Func<int, int, int> _hash, Func<int, int, int> _shift, int _size = 8, double _alfa = 7.0/8.0)
        {
            hash = _hash;
            shift = _shift;
            alfa = _alfa;
            elemCount = accessCount = 0;
            table = new int[_size];
            for (int i = 0; i < _size; ++i)
                table[i] = empty;
        }

        public int Size
        {
            get
            {
                return table.Length;
            }
        }

        public int AccessCount
        {
            get
            {
                return accessCount;
            }
        }

        public int ElemCount  // liczba elementow w tablicy (z uwzglednieniem skasowanych)
        {
            get
            {
                return elemCount;
            }
        }

        // wyszukiwanie w tablicy elementu v - zwraca true jesli element v jest w tablicy
        public bool Search(int v)
        {
            // TODO !!!!!!!!!!
            return false; // zmienic !!!
        }

        // wstawianie do tablicy elementu v - zwraca false jesli v juz jest w tablicy
        // jesli zapelnienie tablicy >= alfa rozmiar tablicy jest podwajany (elementy są przenoszone)
        public bool Insert(int v)
        {
            // TODO !!!!!!!!!!
            return false; // zmienic !!!
        }

        // usuwanie elementu v z tablicy - zwraca false jesli elementu v nie ma w tablicy
        public bool Remove(int v)
        {
            // TODO !!!!!!!!!!
            return false; // zmienic !!!
        }

        // Funkcje haszujace - dla wszystkich
        // v     - klucz
        // size  - rozmiar tablicy
        // wynik - indeks elementu v w tablicy rozmiaru size

        // Najprostsze haszowanie modulo
        public static int ModHash(int v, int size)
        {
            // TODO !!!!!!!!!!
            return 0; // zmienic !!!
        }

        // haszowanie multiplikatywne
        public static int MultiplyHash(int v, int size)
        {
            // a = (sqrt(5)-1)/2 = 0.6180339887
            // wynik = czesc_calkowita(size*(czesc_ulamkowa(v*a)))

            // TODO !!!!!!!!!!
            return 0; // zmienic !!!
        }

        // Funkcje rozwiazywania kolizji - dla wszystkich
        // v     - klucz
        // k     - numer kroku ( przy probie dostepu do klucza v wystapilo juz k kolizji )
        // wynik - przesuniecie wzgledem aktualnego indeksu

        // sekwencyjne rozwiazywanie kolizji
        // - szukamy pierwszego wolnego miejsca (z krokiem 1)
        public static int Shift1(int v, int k) { return 1; }

        // liniowe rozwiazywanie kolizji
        // - szukamy pierwszego wolnego miejsca z zadanym krokiem > 1 (np. 5)
        //   (krok musi byc wzglednie pierwszy z rozmiarem tablicy)
        public static int Shift5(int v, int k) { return 5; }

        // rozwiazywanie kolizji z rosnacym krokiem
        // - w k-tej probie przesuwamy sie o k
        public static int Shiftk(int v, int k) { return k; }

        // rozwiazywanie kolizji przy pomocy haszowania dwukrotnego
        // - rozny krok dla roznych kluczy !
        // do wyznaczenia kroku zastosowac jakies proste haszowanie modulo
        // (dla kazdego klucza krok musi byc wzglednie pierwszy z rozmiarem tablicy)
        public static int Shifth(int v, int k)
        {
            // TODO !!!!!!!!!!
            return 0; // zmienic !!!
        }

    } // class HashTable

} // namespace ASD 