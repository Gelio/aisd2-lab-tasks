#include <vector>

using namespace std;

struct Partition
{
public:

	//  �adne wypisywanie podzia�u
	void print();

	// tu dopisac pola zapamietujace bloki wchodz�ce w sklad podzia�u
	// wskazowka: na poczatku pliku jest #include <vector>
};

vector<Partition> generateSmallPartitions(int n);

// wskazowka: tu mozna dopisac prototyp funkcji pomocniczej
