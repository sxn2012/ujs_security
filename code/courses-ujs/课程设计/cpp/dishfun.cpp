/********************************************
文件名：dishfun.cpp
功能：dish的成员函数
********************************************/
#include"alclass.h"
double dish::Calculate()
{
	return price*num;
}
void dish::input()
{
	cout << "\t\t\t请输入菜名:";
	cin >> this->name;
	cout << "\t\t\t请输入菜的份数:";
	char * s1 = new char[20];
	char * s2 = new char[20];
	cin >> s1;
	this->num = atoi(s1);
	cout << "\t\t\t请输入价格:";
	cin >> s2;
	this->price = atoi(s2);
	this->score = 0;
	delete[]s1;
	delete[]s2;
}
void dish::output1()
{
	cout << "\t\t\t" << name << "\t" << price << "\t" << num << "\t" << score << endl;
}
void dish::output2()
{
	cout << "\t\t\t" << name << "\t" << price << "\t" << num << "\t" << price*num << endl;
}