using System;
using ASD.Graph;
using System.Collections.Generic;
using System.Linq;

namespace lab9
{
	public static class AntsExtender
	{

        /// <summary>
        /// Sprawdza czy istnieje kraw�d�, kt�rej dodanie/poszerzenie poprawi przep�yw zapas�w
        /// </summary>
        /// <param name="baseGraph">graf</param>
        /// <param name="sources">numery wierzcho�k�w - wej�� do mrowiska</param>
        /// <param name="destinations">numery wierzcho�k�w - magazyn�w</param>
        /// <param name="flowValue">warto�� przep�ywu przed rozbudow� mrowiska</param>
        /// <returns>kraw�d� o wadze 1, kt�r� nale�y doda� lub poszerzy� (zwracamy te� kraw�d�
        /// o wadze 1)</returns>
		public static Edge? ImprovementChecker (this IGraph baseGraph, int[] sources, int[] destinations, out int flowValue)
		{
        flowValue=0;
        return null;
		}

	}
}

