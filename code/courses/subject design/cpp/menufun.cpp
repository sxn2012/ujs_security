/********************************************
文件名：menufun.cpp
功能：menu的成员函数
********************************************/
#include"alclass.h"
void menu::remove()
{
	count = -1;
}
void menu::input()
{
	system("cls");
	cout << "\t\t\t*********************************" << endl;
	char * s = new char[20];
	int k;
	cout << "\t\t\t请输入菜的种类个数:";
	cin >> s;
	k = atoi(s);
	count = 0;
	for (int i = 1; i<=k; i++)
	{
		cout << "\t\t\t*******************************************" << endl;
		a[i].input();
		count++;
	}
	del = count;
	delete[]s;
}
void menu::output1()
{
	system("cls");
	cout << "\t\t\t菜名" << "\t" << "单价" << "\t" << "数量" << "\t" << "评分" << endl;
	for (int i = 1; i<=count; i++)
		a[i].output1();
}
void menu::output2()
{
	cout << "\t\t\t菜名" << "\t" << "单价" << "\t" << "数量" << "\t" << "总价" << endl;
	for (int i = 1; i<=del; i++)
		a[i].output2();
	Sleep(1000);
}
double menu::Calculate()
{
	double s = 0;
	for (int i = 1; i<=count; i++)
	{
		s = s + a[i].Calculate();
	}
	return s;
}