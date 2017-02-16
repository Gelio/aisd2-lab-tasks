#pragma once
#include <iostream>
using namespace std;

template <class T>
inline void _swap(T& a, T& b) { T x = a; a = b; b = x; }

void shuffle(int tab[], int n) {
	for (int i = 0; i < n; i++) {
		int doZamiany = rand() % n;
		_swap(tab[i], tab[doZamiany]);
	}
}

void print(int tab[], int n) {
	for (int i = 0; i < n; i++)
		cout << tab[i] << " ";
	cout << endl;
}

template <class T>
T median(T a, T b, T c) {
	if (a >= b) {
		if (b >= c)
			return b;
		else if (c >= a)
			return a;
		else
			return c;
	}
	else {
		if (c <= a)
			return a;
		else if (c >= b)
			return b;
		else
			return c;
	}
}