
#include "elem.h"

template <class T>
inline void _swap(T& a, T& b) { T x=a; a=b ; b=x; }

template <class T>
bool test(T tab[],int n)
    {
    for ( int i=1 ; i<n ; ++i )
        if ( tab[i-1]>tab[i] ) return false;
    return true;
    }


template <class T> 
void sort1(T tab[],int n)
    // nie zmieniac nazwy (ani sygnatury funkcji) - bedzie testowane automatycznie
    // w komentarzu napisac jaki to algorytm
    {
    // implementacja pierwszego algorytmu sortowania
    }


template <class T> 
void sort2(T tab[],int n)
    // nie zmieniac nazwy (ani sygnatury funkcji) - bedzie testowane automatycznie
    // w komentarzu napisac jaki to algorytm
    {
    // implementacja drugiego algorytmu sortowania
    }


template <class T> 
void sort3(T tab[],int n)
    // nie zmieniac nazwy (ani sygnatury funkcji) - bedzie testowane automatycznie
    // w komentarzu napisac jaki to algorytm
    {
    // implementacja trzeciego algorytmu sortowania
    }


template <class T> 
void heapsort(T tab[],int n)
    {
    int i;
    for ( i=n/2-1 ; i>=0 ; --i )
        makeheap(tab,i,n);
    for ( i=n-1 ; i>0 ; --i )
        {
        _swap(tab[0],tab[i]);
        makeheap(tab,0,i);
        }
    }

// tworzy kopiec o korzeniu w elemencie tab[k]
template <class T> 
void makeheap(T tab[],int k,int n)
    {
    T x;
    int i,j;
    i=k;
    j=(i<<1)+1;
    x=tab[i];
    while ( j<n )
        {
        if ( j+1<n && tab[j]<tab[j+1] ) ++j;
        if ( x>=tab[j] ) break;
        tab[i]=tab[j];
        i=j;
        j=(i<<1)+1;
        }
    tab[i]=x;
    }
