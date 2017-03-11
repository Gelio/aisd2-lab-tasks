using System;
using System.Collections.Generic;

namespace lab4a
{
    public class ArrayRange
    {
        public int Start { get; set; }
        public int End { get; set; }

        public bool Empty
        {
            get
            {
                return Start > End;
            }
        }

        public ArrayRange(int start, int end)
        {
            Start = start;
            End = end;
        }

        public int GetLowerMedianIndex()
        {
            if (Start > End)
                throw new InvalidOperationException("Pusty zakres");

            return (int)((End + Start) / 2);
        }

        public int RemoveItemsBeforeLowerMedian()
        {
            int lowerMedianIndex = GetLowerMedianIndex();
            int itemsRemoved = lowerMedianIndex - Start + 1;

            Start = lowerMedianIndex + 1;
            return itemsRemoved;
        }

        public int GetUpperMedianIndex()
        {
            if (Start > End)
                throw new InvalidOperationException("Pusty zakres");

            return (int)((End + Start + 1) / 2);
        }

        public int RemoveItemsAfterUpperMedian()
        {
            int upperMedianIndex = GetUpperMedianIndex();
            int itemsRemoved = End - upperMedianIndex + 1;

            End = upperMedianIndex - 1;
            return itemsRemoved;
        }
    }

	public class Competition
	{
		//Wariant A (k-ty element)
		/// <summary>
		/// Znajduje zwycięzcę konkursu na najlepszą trzcinę
		/// </summary>
		/// <returns>
		/// Wysokość zwycięskiej trzciny
		/// </returns>
		/// <param name='arrays'>
		/// Tablice (skrzynki) biorące udział w konkursie. 
		/// </param>
		/// <param name='k'>
		/// k - parametr poszukiwania zwycięzcy z treści zadania
		/// </param>
		/// <param name='winnerArray'>
		/// Numer tablicy (skrzynki) w której znajduje się zwycięzca
		/// </param>
		/// <param name='winnerArrayPos'>
		/// Pozycja zwycięzcy w jego tablicy.
		/// </param>
		/// 
		public static int FindWinner (int[][] arrays, int k, out int winnerArray, out int winnerArrayPos)
		{
            List<int> validArrays = new List<int>(arrays.Length);
            ArrayRange[] leftArrayRanges = new ArrayRange[arrays.Length];
            int elementsLeft = 0;
            for (int i = 0; i < arrays.Length; i++)
            {
                elementsLeft += arrays[i].Length;
                validArrays.Add(i);
                leftArrayRanges[i] = new ArrayRange(0, arrays[i].Length - 1);
            }

            while (validArrays.Count > 1)
            {
                if (k >= elementsLeft / 2)
                {
                    // Usuwamy elementy mniejsze lub równe dolnej medianie zbioru, którego dolna mediana jest
                    // minimalna spośród wszystkich median
                    int[] arrayWithLowestMedian = arrays[validArrays[0]];
                    ArrayRange arrayWithLowestMedianRange = leftArrayRanges[validArrays[0]];
                    int arrayWithLowestMedianIndex = 0;
                    for (int i=1; i < validArrays.Count; i++)
                    {
                        int[] currentArray = arrays[validArrays[i]];
                        ArrayRange currentArrayRange = leftArrayRanges[validArrays[i]];
                        if (currentArray[currentArrayRange.GetLowerMedianIndex()] < arrayWithLowestMedian[arrayWithLowestMedianRange.GetLowerMedianIndex()])
                        {
                            arrayWithLowestMedian = currentArray;
                            arrayWithLowestMedianRange = currentArrayRange;
                            arrayWithLowestMedianIndex = validArrays[i];
                        }
                    }

                    int itemsRemovedCount = arrayWithLowestMedianRange.RemoveItemsBeforeLowerMedian();
                    k -= itemsRemovedCount;
                    elementsLeft -= itemsRemovedCount;
                }
                else
                {
                    // Usuwamy elementy większe lub rowne gornej medianie zbioru, którego górna mediana jest
                    // maksymalna spośród wszystkich median
                    int[] arrayWithBiggestMedian = arrays[validArrays[0]];
                    ArrayRange arrayWithBiggestMedianRange = leftArrayRanges[validArrays[0]];
                    int arrayWithBiggestMedianIndex = validArrays[0];
                    for (int i = 1; i < validArrays.Count; i++)
                    {
                        int[] currentArray = arrays[validArrays[i]];
                        ArrayRange currentArrayRange = leftArrayRanges[validArrays[i]];
                        if (currentArray[currentArrayRange.GetUpperMedianIndex()] > arrayWithBiggestMedian[arrayWithBiggestMedianRange.GetUpperMedianIndex()])
                        {
                            arrayWithBiggestMedian = currentArray;
                            arrayWithBiggestMedianRange = currentArrayRange;
                            arrayWithBiggestMedianIndex = validArrays[i];
                        }
                    }

                    int itemsRemovedCount = arrayWithBiggestMedianRange.RemoveItemsAfterUpperMedian();
                    elementsLeft -= itemsRemovedCount;
                }

                for (int i=0; i < validArrays.Count; i++)
                {
                    if (leftArrayRanges[validArrays[i]].Empty)
                        validArrays.RemoveAt(i);
                }
            }


            winnerArray = validArrays[0];
            winnerArrayPos = leftArrayRanges[winnerArray].Start + k;
			return arrays[winnerArray][winnerArrayPos];
		}
    }
}

