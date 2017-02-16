#pragma once
#include "utilities.h"

template <class T>
bool test(T tab[], int n)
{
	for (int i = 1; i<n; ++i)
		if (tab[i - 1]>tab[i]) return false;
	return true;
}


template <class T>
void sort1(T tab[], int n)
{
	// Sortowanie przez wstawianie (InsertionSort)
	for (int i = 1; i < n; i++) {
		for (int j = i - 1; j >= 0; j--) {
			if (tab[j + 1] <= tab[j])
				_swap(tab[j + 1], tab[j]);
		}
	}

}


template <class T>
void sort2(T tab[], int n)
{
	// Sortowanie przez wybór (SelectionSort)
	for (int i = 0; i < n; i++) {
		int minIndex = i;
		for (int j = i + 1; j < n; j++) {
			if (tab[j] < tab[minIndex])
				minIndex = j;
		}

		_swap(tab[i], tab[minIndex]);
	}
}


// Zwraca indeks elementu v (dzielacego)
template <class T>
int partition(T tab[], int n, T v) {
	int i = 0, j = n;
	do {
		do
			i++;
		while (tab[i] < v);
		do
			j--;
		while (tab[j] > v);
		if (i < j)
			_swap(tab[i], tab[j]);
	} while (i < j);
	tab[0] = tab[j];
	tab[j] = v;

	return j;
}

template <class T>
void sort3(T tab[], int n)
{
	if (n <= 1)
		return;

	// v (pivot) - mediana z 3 elementów
	T v = median(tab[0], tab[n / 2], tab[n - 1]);
	

	int j = partition(tab, n, v);
	if (j > 0)
		sort3(tab, j);
	if (j < n - 1)
		sort3(tab + j + 1, n - j - 1);
}



// Nie mam pojêcia po co jest tutaj heapsort, byæ mo¿e to ju¿
// zosta³o rozwi¹zane i wrzucone od razu do treœci na Google Drive
template <class T>
void heapsort(T tab[], int n)
{
	int i;
	for (i = n / 2 - 1; i >= 0; --i)
		makeheap(tab, i, n);
	for (i = n - 1; i > 0; --i)
	{
		_swap(tab[0], tab[i]);
		makeheap(tab, 0, i);
	}
}

// tworzy kopiec o korzeniu w elemencie tab[k]
template <class T>
void makeheap(T tab[], int k, int n)
{
	T x;
	int i, j;
	i = k;
	j = (i << 1) + 1;
	x = tab[i];
	while (j < n)
	{
		if (j + 1 < n && tab[j] < tab[j + 1]) ++j;
		if (x >= tab[j]) break;
		tab[i] = tab[j];
		i = j;
		j = (i << 1) + 1;
	}
	tab[i] = x;
}
