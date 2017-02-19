#include <vector>

using namespace std;

struct Partition
{
public:

	//  ³adne wypisywanie podzia³u
	void print();

	// tu dopisac pola zapamietujace bloki wchodz¹ce w sklad podzia³u
	// wskazowka: na poczatku pliku jest #include <vector>
};

vector<Partition> generateSmallPartitions(int n);

// wskazowka: tu mozna dopisac prototyp funkcji pomocniczej
