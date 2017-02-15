using System;
using ASD.Graph;

namespace asd2_lab11
{	
	public static class TrailPlannerExtender
	{
		/// <summary>
		/// Minimalizacja liczby szlakow
		/// </summary>
		/// <param name="g">Wejsciowy graf. Przyjmujemy, ze jest on acykliczny. </param>
		/// <param name="totalCost">Koszt najtanszego znalezionego rozwiazania</param>
		/// <param name="trails">Graf skadajacy sie z krawedzi wchodzacych do rozwiazania</param>
		/// <returns>Metoda zwraca minimalna mozliwa liczbe szlakow</returns>
		public static int MinimumNumberOfTrails(this IGraph g, out int totalCost, out IGraph trails)
		{
            totalCost=0;
            trails=null;
			return 0;
		}
		
		/// <summary>
		/// Minimalizacja kosztu szlakow, z uwzglednieniem hoteli
		/// </summary>
		/// <param name="g">Wejsciowy graf. Przyjmujemy, ze jest on acykliczny. </param>
		/// <param name="vcosts">vcosts[i] to kosz postawienia holetu w miescie i</param>
		/// <param name="trails">Graf skadajacy sie z krawedzi wchodzacych do rozwiazania</param>
		/// <returns>Metoda zwraca minimalny mozliwy koszt rozwiazania</returns>
		public static int MinimumCostOfTrails(this IGraph g, int[] vcosts, out IGraph trails)
		{
            trails=null;
			return 0;
		}
	}
}
