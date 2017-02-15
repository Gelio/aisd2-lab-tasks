
namespace ASD.Text
{
using System.Collections.Generic;

/// <summary>
/// Klasa statyczna zawierająca implementacje różnych metod wyszukiwania wzorców w tekście
/// </summary>
public static partial class StringMatching
    {

    /// <summary>
    /// Wyszukiwanie wzorca w tekście algorytmem Knutha-Morrisa-Pratta
    /// </summary>
    /// <param name="y">Badany tekst</param>
    /// <param name="x">Szukany wzorzec</param>
    /// <returns>Lista zawierająca początkowe indeksy wystąpień wzorca x w tekście y</returns>
    public static List<int> KMP(string y, string x, int[] P)
        {
        int n,m,i,j;
        n=y.Length;
        m=x.Length;
        List<int> ml = new List<int>();
        for ( j=i=0 ; i<=n-m ; i+=j==0?1:j-P[j] )
            {
            for ( j=P[j] ; j<m && y[i+j]==x[j] ; ++j ) ;
            if ( j==m ) ml.Add(i);
            }
        return ml;
        }

    /// <summary>
    /// Obliczanie dlugości prefikso-sufiksów wzorca
    /// </summary>
    /// <param name="x">Analizowany wzorzec</param>
    /// <returns>Obliczona tablica długości prefikso-sufiksów</returns>
    public static int[] ComputeP(string x)
        {
        int[] P = new int[x.Length+1];
        int t;
        P[0]=P[1]=t=0;
        for ( int j=2 ; j<=x.Length ; ++j )
            {
            // niezmiennik: t=P[j-1]  // długość prefikso-sufiksu z poprzedniej iteracji 
            while ( t>0 && x[t]!=x[j-1] )
                t=P[t];
            if ( x[t]==x[j-1] ) ++t;  // czy można wydłużyć prefikso-sufiks ?
            P[j]=t;
            }
        return P;
        }

    } // class StringMatching

} // namespace ASD.Text
