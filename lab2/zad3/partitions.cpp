#include <iostream>
#include <vector>

#include "partitions.h"

using namespace std;

void Partition::print()
{
	// ma siê ³adnie wypisywaæ
	// ³adnie czyli np. tak: {1,2,3}{4,5}{6}
	for (int i = 0; i < partition.size(); i++) {
		cout << "{";
		for (int j = 0; j < partition[i].size(); j++) {
			cout << partition[i][j];
			if (j != partition[i].size() - 1)
				cout << ",";
		}
		cout << "}";
	}
}

vector<Partition> Partition::extend(int n) {
	vector<Partition> extendedPartitions;
	for (int i = 0; i < partition.size(); i++) {
		// Rozszerzene ka¿dego z istniej¹cych zbiorów o element n
		// i zapisanie go jako nowy podzia³
		Partition partitionCopy = *this;
		partitionCopy.partition[i].push_back(n);
		extendedPartitions.push_back(partitionCopy);
	}

	// Dodanie do podzialu zbioru jednoelementowego
	Partition partitionWithSingleton = *this;
	vector<int> singleton;
	singleton.push_back(n);
	partitionWithSingleton.partition.push_back(singleton);
	extendedPartitions.push_back(partitionWithSingleton);

	return extendedPartitions;
}

vector<Partition> Partition::extendSmallOnly(int n, int maxSize) {
	vector<Partition> extendedPartitions;
	for (int i = 0; i < partition.size(); i++) {
		// Rozszerzenie ka¿dego z istniej¹cych zbiorów o element n
		// i zapisanie go jako nowy podzia³, o ile dodany element
		// nie spowoduje, ¿e podzia³ przestanie byæ ma³y
		if (partition[i].size() + 1 > maxSize)
			continue;

		Partition partitionCopy = *this;
		partitionCopy.partition[i].push_back(n);
		extendedPartitions.push_back(partitionCopy);
	}

	// Dodanie do podzialu zbioru jednoelementowego
	// Ten podzia³ na pewno bêdzie ma³y, bo wszystkie
	// poprzednie zbiory by³y ma³e, a zbiór jednoelementowy
	// na pewno jest ma³y
	Partition partitionWithSingleton = *this;
	vector<int> singleton;
	singleton.push_back(n);
	partitionWithSingleton.partition.push_back(singleton);
	extendedPartitions.push_back(partitionWithSingleton);

	return extendedPartitions;
}

// parametry:
// n       - liczba elementow zbioru
vector<Partition> generatePartitions(int n)
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

	// Warunek brzegowy
	if (n == 1) {
		Partition single;
		return single.extend(1);
	}
	
	// Krok rekurencyjny
	vector<Partition> basePartitions = generatePartitions(n - 1);
	vector<Partition> result;
	for (int i = 0; i < basePartitions.size(); i++) {
		vector<Partition> extendedPartitions = basePartitions[i].extend(n);
		for (int j = 0; j < extendedPartitions.size(); j++)
			result.push_back(extendedPartitions[j]);
	}

	return result;
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
	
	int maxSize = n / 2;
	return generateSmallPartitionsRecursive(n, maxSize);
}

vector<Partition> generateSmallPartitionsRecursive(int n, int maxSize) {
	// Warunek brzegowy
	if (n == 1) {
		Partition single;
		return single.extend(1);
	}

	// Krok rekurencyjny
	vector<Partition> basePartitions = generateSmallPartitionsRecursive(n - 1, maxSize);
	vector<Partition> result;
	for (int i = 0; i < basePartitions.size(); i++) {
		vector<Partition> extendedPartitions = basePartitions[i].extendSmallOnly(n, maxSize);
		for (int j = 0; j < extendedPartitions.size(); j++)
			result.push_back(extendedPartitions[j]);
	}

	return result;
}
