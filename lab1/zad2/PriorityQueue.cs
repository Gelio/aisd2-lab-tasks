
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


    class LazyPriorityQueue : IPriorityQueue
    {
        private List<int> _queue;

        public LazyPriorityQueue()
        {
            _queue = new List<int>();
        }

        public void Put(int p)
        {
            _queue.Add(p);
        }

        public int GetMax()
        {
            int maxValue = ShowMax();
            _queue.Remove(maxValue);
            return maxValue;
        }

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

        public int GetMax()
        {
            int maxValue = ShowMax();
            _queue.RemoveAt(_queue.Count - 1);
            return maxValue;
        }

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

        public HeapPriorityQueue()
        {
        }

        public void Put(int p)
        {
        }

        public int GetMax()
        {
            return 0;
        }

        public int ShowMax()
        {
            return 0;
        }

        public int Count
        {
            get
            {
                return 0;
            }
        }

    } // HeapPriorityQueue

}
