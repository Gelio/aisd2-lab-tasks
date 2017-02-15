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
            accessCount++;
            int pos = hash(v, Size);
            int k = 1;
            int initialValue = table[pos];
            while (table[pos] != empty && table[pos] != v && (table[pos] != initialValue || k == 1))
            {
                accessCount++;
                int step = shift(v, k++) % Size;
                pos = (pos + step) % Size;
            }
                

            return table[pos] == v;
        }

        // wstawianie do tablicy elementu v - zwraca false jesli v juz jest w tablicy
        // jesli zapelnienie tablicy >= alfa rozmiar tablicy jest podwajany (elementy są przenoszone)
        public bool Insert(int v)
        {
            if (Search(v))
                return false;

            double tableFill = (double)(ElemCount + 1) / (double)Size;
            if (tableFill >= alfa)
            {
                // Podwójne zwiększenie rozmiaru tablicy
                HashTable enlargedTable = new HashTable(hash, shift, Size * 2, alfa);
                for (int i = 0; i < Size; i++)
                {
                    if (table[i] >= 0)
                        enlargedTable._insertWithoutDuplicateCheck(table[i]);
                }
                table = enlargedTable.table;
            }

            _insertWithoutDuplicateCheck(v);
            return true;
        }

        // usuwanie elementu v z tablicy - zwraca false jesli elementu v nie ma w tablicy
        public bool Remove(int v)
        {
            if (!Search(v))
                return false;

            elemCount--;
            accessCount++;
            int pos = hash(v, Size);
            int k = 1;
            while (table[pos] != v)
            {
                accessCount++;
                int step = shift(v, k++) % Size;
                pos = (pos + step) % Size;
            }
                

            table[pos] = deleted;
            return true;
        }

        private void _insertWithoutDuplicateCheck(int v)
        {
            elemCount++;
            accessCount++;
            int pos = hash(v, Size);
            int k = 1;
            while (table[pos] != deleted && table[pos] != empty)
            {
                accessCount++;
                int step = shift(v, k++) % Size;
                pos = (pos + step) % Size;
            }


            table[pos] = v;
        }

        // Funkcje haszujace - dla wszystkich
        // v     - klucz
        // size  - rozmiar tablicy
        // wynik - indeks elementu v w tablicy rozmiaru size

        // Najprostsze haszowanie modulo
        public static int ModHash(int v, int size)
        {
            // Krok musi być co najmniej 1, stąd to "żonglowanie" jedynkami
            return (v % (size - 1)) + 1;
        }

        // haszowanie multiplikatywne
        public static int MultiplyHash(int v, int size)
        {
            // a = (sqrt(5)-1)/2 = 0.6180339887
            // wynik = czesc_calkowita(size*(czesc_ulamkowa(v*a)))

            double a = 0.6180339887;
            double product = v * a;
            double fractionPart = product - Math.Floor(product);
            return ((int)Math.Floor(size * fractionPart) % (size - 1)) + 1;
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
            // Nie jestem pewny tego rozwiązania, zamiast k można użyć maksymalnej 
            // długości tablicy haszującej (1000 powinno być okej), ale to rozwiązanie
            // wtedy nie ma zbytnio sensu
            return HashTable.ModHash(v, k + 1);
        }

    } // class HashTable

} // namespace ASD 