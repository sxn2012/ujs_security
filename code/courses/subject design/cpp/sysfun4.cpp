/********************************************
文件名：sysfun4.cpp
功能：sys的成员函数4
********************************************/
#include"alclass.h"
void sys::Delete()
{
	int num;
	system("cls");
	if (count <= 0)
	{
		cout << endl << endl << endl << endl;
		cout << "\t\t\t*********************************" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*      菜单为空，删除失败!      *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*********************************" << endl;
		SYSTEMTIME sy;
		GetLocalTime(&sy);
		cout << "\t\t\t  " << sy.wYear << "年" << sy.wMonth << "月" << sy.wDay << "日" << endl;
		cout << "\t\t\t  " << sy.wHour << ":" << sy.wMinute << ":" << sy.wSecond;
		Sleep(1000);
		Print();
	}
	else
	{
		cout << endl << endl << endl << endl;
		cout << "\t\t\t*********************************" << endl;
		cout << "\t\t\t请输入需要删除的餐单序号";
		char * s = new char[20];
		cin >> s;
		num = atoi(s);
		system("cls");
		a[num].remove();
		cout << endl << endl << endl << endl;
		cout << "\t\t\t*********************************" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*           删除成功!           *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*        是否继续删除(y/n)      *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*********************************" << endl;
		SYSTEMTIME sy;
		GetLocalTime(&sy);
		cout << "\t\t\t  " << sy.wYear << "年" << sy.wMonth << "月" << sy.wDay << "日" << endl;
		cout << "\t\t\t  " << sy.wHour << ":" << sy.wMinute << ":" << sy.wSecond;
		char c;
		c = _getch();
		if (c == 'y' || c == 'Y') Delete();
		Print();
	}

}
void sys::Search1()
{
	int num;
	system("cls");
	if (count <= 0)
	{
		cout << endl << endl << endl << endl;
		cout << "\t\t\t*********************************" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*      菜单为空，查找失败!      *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*********************************" << endl;
		SYSTEMTIME sy;
		GetLocalTime(&sy);
		cout << "\t\t\t  " << sy.wYear << "年" << sy.wMonth << "月" << sy.wDay << "日" << endl;
		cout << "\t\t\t  " << sy.wHour << ":" << sy.wMinute << ":" << sy.wSecond;
		Sleep(1000);
		Print();
	}
	else
	{
		cout << "\t\t\t请输入需要查找的餐单序号" << endl;
		cout << "\t\t\t";
		cin >> num;
		if (a[num].count <= 0)
		{
			cout << endl << endl << endl << endl;
			cout << "\t\t\t*********************************" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*             未找到!           *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*********************************" << endl;
			SYSTEMTIME sy;
			GetLocalTime(&sy);
			cout << "\t\t\t  " << sy.wYear << "年" << sy.wMonth << "月" << sy.wDay << "日" << endl;
			cout << "\t\t\t  " << sy.wHour << ":" << sy.wMinute << ":" << sy.wSecond << endl;
		}
		else
		{
			a[num].output1();
		}
		cout << "\t\t\t是否继续查找(y/n)";
		char c;
		c = _getch();
		if (c == 'y' || c == 'Y') Search1();
		Print();
	}
}
void sys::Search2()
{
	int num;
	system("cls");
	if (count <= 0)
	{
		cout << endl << endl << endl << endl;
		cout << "\t\t\t*********************************" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*      菜单为空，查找失败!      *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*********************************" << endl;
		SYSTEMTIME sy;
		GetLocalTime(&sy);
		cout << "\t\t\t  " << sy.wYear << "年" << sy.wMonth << "月" << sy.wDay << "日" << endl;
		cout << "\t\t\t  " << sy.wHour << ":" << sy.wMinute << ":" << sy.wSecond;
		Sleep(1000);
		Print();
	}
	else
	{
		cout << "\t\t\t请输入需要查找的订单序号" << endl;
		cout << "\t\t\t";
		cin >> num;
		if (a[num].count <= 0)
		{
			cout << endl << endl << endl << endl;
			cout << "\t\t\t*********************************" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*             未找到!           *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*********************************" << endl;
			SYSTEMTIME sy;
			GetLocalTime(&sy);
			cout << "\t\t\t  " << sy.wYear << "年" << sy.wMonth << "月" << sy.wDay << "日" << endl;
			cout << "\t\t\t  " << sy.wHour << ":" << sy.wMinute << ":" << sy.wSecond << endl;
		}
		else
		{
			system("cls");
			a[num].output2();
		}
		cout << "\t\t\t是否继续查找(y/n)";
		char c;
		c = _getch();
		if (c == 'y' || c == 'Y') Search2();
		Print();
	}
}