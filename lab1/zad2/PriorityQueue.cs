
using System;
using System.Collections.Generic;

namespace ASD
{

    interface IPriorityQueue
    {
        void Put(int p);     // wstawia element do kolejki
        int GetMax();        // pobiera maksymalny element z kolejki (element jest usuwany z kolejki)
        int ShowMax();       // pokazuje maksymalny element kolejki (element pozostaje w kolejce)
        int Count { get; }   // liczba elementów kolejki
    }

    // List<T> jest implementowana za pomocą tablicy, zatem występują następujące
    // złożności operacji:
    // * Add - O(1) zamortyzowana, O(n) pesymistyczna gdy trzeba zwiększyć rozmiar
    // * InsertAt - O(n) bo przesuwamy elementy
    // * RemoveAt i RemoveAt - O(1) dla ostatniego elementu, O(n) dla pozostałych
    // * [i] (odwołanie do elementu na i-tej pozycji - O(1)

    class LazyPriorityQueue : IPriorityQueue
    {
        private List<int> _queue;

        public LazyPriorityQueue()
        {
            _queue = new List<int>();
        }

        // O(1)
        public void Put(int p)
        {
            _queue.Add(p);
        }

        // Złożność O(n), bo ShowMax ma O(n) oraz dodatkowo
        // trzeba doliczyć przesunięcie wszystkich elementów
        // za indeksem elementu o największej wartości przy usuwaniu
        public int GetMax()
        {
            int maxValue = ShowMax();
            _queue.Remove(maxValue);
            return maxValue;
        }

        // Złożność O(n), trzeba przejrzeć całą tablicę
        public int ShowMax()
        {
            if (_queue.Count == 0)
                throw new InvalidOperationException();

            int maxValue = _queue[0];
            for (int i=1; i < _queue.Count; i++)
            {
                if (maxValue < _queue[i])
                    maxValue = _queue[i];
            }

            return maxValue;
        }

        public int Count
        {
            get
            {
                return _queue.Count;
            }
        }

    } // LazyPriorityQueue


    class EagerPriorityQueue : IPriorityQueue
    {
        // Tablica posortowana rosnąco, aby GetMax było O(1)
        // W przypadku posortowania malejącego GetMax musiałoby przesuwać 
        // wszystkie elementy
        private List<int> _queue;

        public EagerPriorityQueue()
        {
            _queue = new List<int>();
        }

        // Złożność O(n) - w czasie liniowym znajdujemy miejsce dla
        // aktualnie wstawianego elementu
        public void Put(int p)
        {
            if (_queue.Count == 0 || p <= _queue[0])
            {
                _queue.Insert(0, p);
                return;
            }

            for (int i=1; i < _queue.Count; i++)
            {
                if (p <= _queue[i])
                {
                    _queue.Insert(i, p);
                    return;
                }
            }

            _queue.Add(p);
        }

        // Złożność O(1), bo ShowMax ma O(1), usunięcie ostatniego
        // elementu tablicy również O(1)
        public int GetMax()
        {
            int maxValue = ShowMax();
            _queue.RemoveAt(_queue.Count - 1);
            return maxValue;
        }

        // O(1)
        public int ShowMax()
        {
            if (_queue.Count == 0)
                throw new InvalidOperationException();

            return _queue[_queue.Count - 1];
        }

        public int Count
        {
            get
            {
                return _queue.Count;
            }
        }

    } // EagerPriorityQueue


    class HeapPriorityQueue : IPriorityQueue
    {
        // Zwykły kopiec, Max w korzeniu
        private List<int> _queue;

        public HeapPriorityQueue()
        {
            _queue = new List<int>();
        }

        // Złożoność wstawiania O(log n), ponieważ najpierw dodajemy na
        // koniec tablicy (czas stały), a następnie operacja UpHeap ma koszt
        // logarytmiczny (przechodzi po drzewie w pionie, drzewo ma wysokość
        // n = 2^h => h = log n
        public void Put(int p)
        {
            _queue.Add(p);
            UpHeap(_queue.Count - 1);
        }

        // Złożoność O(log n), bo ShowMax ma O(1), a przepisanie i usunięcie
        // ostatniego elementu ma O(1), a DownHeap O(log n) - argumentacja taka
        // sama jak przy UpHeap w operacji Put
        public int GetMax()
        {
            int maxValue = ShowMax();

            _queue[0] = _queue[_queue.Count - 1];
            _queue.RemoveAt(_queue.Count - 1);

            DownHeap(0);

            return maxValue;
        }

        // O(1)
        public int ShowMax()
        {
            if (_queue.Count == 0)
                throw new InvalidOperationException();

            return _queue[0];
        }

        public int Count
        {
            get
            {
                return _queue.Count;
            }
        }

        // O(log N)
        private void UpHeap(int k)
        {
            while (k > 0 && _queue[k] > _queue[k / 2])
            {
                Swap(k, k / 2);
                k /= 2;
            }
        }

        // O(log N)
        private void DownHeap(int k)
        {
            int maxIndex = _queue.Count - 1;

            int leftChild = k * 2 + 1;
            int rightChild = leftChild + 1;

            while (leftChild <= maxIndex)
            {
                int toSwap = -1;
                

                // Sprawdzamy lewego następnika
                if (_queue[k] < _queue[leftChild])
                    toSwap = leftChild;

                // Sprawdzamy prawego następnika o ile istnieje
                if (rightChild <= maxIndex && _queue[k] < _queue[rightChild] && _queue[leftChild] < _queue[rightChild])
                    toSwap = rightChild;

                if (toSwap == -1)
                    return;

                Swap(k, toSwap);
                k = toSwap;
                leftChild = k * 2 + 1;
                rightChild = leftChild + 1;
            }
        }

        private void Swap(int i, int j)
        {
            int c = _queue[i];
            _queue[i] = _queue[j];
            _queue[j] = c;
        }

    } // HeapPriorityQueue

}
