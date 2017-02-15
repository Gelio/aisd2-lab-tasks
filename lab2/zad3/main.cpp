#include <iostream>
#include <vector>

#include "partitions.h"

using namespace std;

void test(int n)
{
	vector<Partition> partitions;
	int s;

	partitions = generateSmallPartitions(n);
	s = partitions.size();	
	cout<< endl<<endl<<"Male podzialy zbioru ["<<n<<"]:"<<endl;
	for (int i=0; i< s; ++i)
	{
		cout<<i+1<<". ";
		partitions[i].print();
		cout<<endl;
	}

}


void main()
{
	test(5);
	cout<<endl<<"powinno byc 26 malych z 52 wszystkich";

	test(6);
	cout<<endl<<"powinno byc 166 malych z 203 wszystkich";
	cout<<endl;
}
