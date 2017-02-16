#include <iostream>
#include <time.h>
#include "sort.h"
#include "utilities.h"
using namespace std;

#define N 10
#define MAX_NUM 50


int main() {
	int tab[N];
	srand(time(NULL));

	for (int i = 0; i < N; i++)
		tab[i] = rand() % MAX_NUM;

	

	// sort1
	cout << "sort1" << endl;
	shuffle(tab, N);
#if N <= 10
	cout << "Przed sortowaniem" << endl;
	print(tab, N);
#endif

	sort1(tab, N);
#if N <= 10
	cout << "Po sortowaniu" << endl;
	print(tab, N);
#endif
	cout << "Posortowane rosnaco: " << (test(tab, N) ? "tak" : "nie") << endl;
	
	cout << endl << endl;


	// sort2
	cout << "sort2" << endl;
	shuffle(tab, N);
#if N <= 10
	cout << "Przed sortowaniem" << endl;
	print(tab, N);
#endif

	sort2(tab, N);
#if N <= 10
	cout << "Po sortowaniu" << endl;
	print(tab, N);
#endif
	cout << "Posortowane rosnaco: " << (test(tab, N) ? "tak" : "nie") << endl;

	cout << endl << endl;

	
	// sort3
	cout << "sort3" << endl;
	shuffle(tab, N);
#if N <= 10
	cout << "Przed sortowaniem" << endl;
	print(tab, N);
#endif

	sort3(tab, N);
#if N <= 10
	cout << "Po sortowaniu" << endl;
	print(tab, N);
#endif
	cout << "Posortowane rosnaco: " << (test(tab, N) ? "tak" : "nie") << endl;

	cout << endl << endl;
	
	return EXIT_SUCCESS;
}

