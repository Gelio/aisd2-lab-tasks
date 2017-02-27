
namespace ASD
{
using System.Collections.Generic;

/// <summary>
/// Kolejka priorytetowa
/// </summary>
/// <typeparam name="K">Typ przechowywanych elementów (kluczy)</typeparam>
/// <remarks>
/// Kryterium porównywania elementów należy podać jako parametr konstruktora.<br/>
/// <br/>
/// Implementacja za pomocą kopca.
/// </remarks>
public class PriorityQueue<K>
    {

    /// <summary>
    /// Lista zawierająca elementy kolejki priorytetowej
    /// </summary>
    private List<K> elements;

    /// <summary>
    /// Kryterium porównywania elementów
    /// </summary>
    /// <remarks>
    /// Kryterium powinno zwracać <b>true</b> jeśli lewy argument porównania ma lepszy priorytet niż prawy.
    /// </remarks>
    private System.Func<K,K,bool> cmp;

    /// <summary>
    /// Tworzy pustą kolejkę priorytetową
    /// </summary>
    /// <param name="cmp">Kryterium porównywania elementów</param>
    /// <remarks>
    /// Kryterium powinno zwracać <b>true</b> jeśli lewy argument porównania ma lepszy priorytet niż prawy.<br/>
    /// </remarks>
    public PriorityQueue(System.Func<K,K,bool> cmp)
        {
        this.cmp=cmp;
        elements=new List<K>();
        }

    /// <summary>
    /// Liczba elementów kolejki priorytetowej (właściwość tylko do odczytu)
    /// </summary>
    /// <remarks>
    /// Liczba elementów jest odpowiednio modyfikowana przez metody <see cref="Put"/> i <see cref="Get"/>.
    /// </remarks>
    public int Count => elements.Count;

    /// <summary>
    /// Wstawia element do kolejki priorytetowej
    /// </summary>
    /// <param name="k">Wstawiany element (klucz)</param>
    /// <returns>Informacja czy wstawianie powiodło się</returns>
    /// <remarks>
    /// Jeśli element o zadanym kluczu już jest w kolejce to metoda zwraca <b>false</b>, a element nie jest wstawiany
    /// (klucze elementów należących do kolejki są unikalne).
    /// </remarks>
    public bool Put(K k)
        {
        elements.Add(k);
        int i=elements.Count-1;
        int j=(i-1)>>1;  // j = ojciec i
        while ( j>=0 && cmp(k,elements[j]) )
            {
            elements[i]=elements[j];
            i=j;
            j=(i-1)>>1;
            }
        elements[i]=k;
        return true;
        }

    /// <summary>
    /// Pobiera z kolejki element o najlepszym priorytecie
    /// </summary>
    /// <returns>Pobrany element (klucz)</returns>
    /// <remarks>
    /// Pobrany element jest usuwany z kolejki.
    /// </remarks>
    public K Get()
        {
        K k= elements[0];
        K e=elements[elements.Count-1];
        elements.RemoveAt(elements.Count-1);
        int i=0, j=1;
        while ( j<elements.Count )
            {
            if ( j+1<elements.Count && cmp(elements[j+1],elements[j]) ) ++j;
            if ( !cmp(elements[j],e) ) break;
            elements[i]=elements[j];
            i=j;
            j=(i<<1)+1; // j = syn i
            }
        if ( elements.Count>0 )
            elements[i]=e;
        return k;
        }

    }  // class PriorityQueue<I,P>

}  // namespace ASD.Graphs
