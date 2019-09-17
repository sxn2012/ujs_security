#include"seqlist.h"
template<class ElemType>
void Print(const ElemType &ea)
{
	cout << ea << "\t";
}
int main(int argc, char*argv[])
{
	SeqList<int> s1(12);
	int a;
	for (int i = 1; i <= 7; i++)
	{
		s1.InsertElem(2 * i + 1);
		s1.GetElem(i, a);
		cout << a << "\t";
	}
	cout << endl;
	system("pause");
	Insertnum(s1, 10);
	//Deletenum(s1, 7);
	s1.Traverse(Print);
	
	return 0;
}