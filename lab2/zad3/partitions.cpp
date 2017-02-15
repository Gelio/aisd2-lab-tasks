#include <iostream>
#include <vector>

#include "partitions.h"

using namespace std;

void Partition::print()
    {
    // ma siê ³adnie wypisywaæ
    // ³adnie czyli np. tak: {1,2,3}{4,5}{6}
    }

// parametry:
// n       - liczba elementow zbioru
vector<Partition> generateSmallPartitions(int n)
{

/*
PodpowiedŸ:

Rekurencyjny algorytm generowania WSZYSTKICH podzia³ów zbioru:

	warunek brzegowy:
	G[1] = {1} 

	krok rekurencyjny:

	G[n-1] = b1,b2,b3,...,bm  
	G[n] =  b1 u {n},b2,b3,...,bm
			b1,b2 u {n},b3,...,bm
			b1,b2,b3 u {n},...,bm
			...
			b1,b2,b3,...,bm u {n}
			b1,b2,b3,...,bm, {n}
*/

return vector<Partition>(); // to usunac
}

// wskazowka: tu mozna dopisac funkcje pomocnicza
