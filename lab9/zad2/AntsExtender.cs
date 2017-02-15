using System;
using ASD.Graph;
using System.Collections.Generic;
using System.Linq;

namespace lab9
{
	public static class AntsExtender
	{

        /// <summary>
        /// Sprawdza czy istnieje krawêdŸ, której dodanie/poszerzenie poprawi przep³yw zapasów
        /// </summary>
        /// <param name="baseGraph">graf</param>
        /// <param name="sources">numery wierzcho³ków - wejœæ do mrowiska</param>
        /// <param name="destinations">numery wierzcho³ków - magazynów</param>
        /// <param name="flowValue">wartoœæ przep³ywu przed rozbudow¹ mrowiska</param>
        /// <returns>krawêdŸ o wadze 1, któr¹ nale¿y dodaæ lub poszerzyæ (zwracamy te¿ krawêdŸ
        /// o wadze 1)</returns>
		public static Edge? ImprovementChecker (this IGraph baseGraph, int[] sources, int[] destinations, out int flowValue)
		{
        flowValue=0;
        return null;
		}

	}
}

