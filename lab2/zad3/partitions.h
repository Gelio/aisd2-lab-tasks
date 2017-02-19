#include <vector>

using namespace std;

struct Partition
{
private:
	vector<vector<int>> partition;
public:

	//  �adne wypisywanie podzia�u
	void print();
	// rozszerzenie podzialu o element n (generuje partition.size() + 1 kolejnych podzial�w
	vector<Partition> extend(int n);
	// to samo co extend, tylko zostawia jedynie ma�e podzia�y
	vector<Partition> extendSmallOnly(int n, int maxSize);
};

vector<Partition> generateSmallPartitions(int n);
vector<Partition> generateSmallPartitionsRecursive(int n, int maxSize);
