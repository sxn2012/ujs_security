/********************************************
文件名：sysfun3.cpp
功能：sys的成员函数3
********************************************/
#include"alclass.h"
void sys::Modify1()
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
		cout << "\t\t\t*      菜单为空，修改失败!      *" << endl;
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
		cout << "\t\t\t请输入需要修改的餐单序号" << endl;
		char * s = new char[20];
		cin >> s;
		system("cls");
		cout << endl << endl << endl << endl;
		num = atoi(s);
		a[num].output1();
		Sleep(1000);
		system("cls");
	loop1:a[num].input();
		if (test())
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
			if (count>10)
				cout << "\t\t\t*    修改成功！订单编号为" << num << "     *" << endl;
			else
				cout << "\t\t\t*    修改成功！订单编号为0" << num << "     *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*      是否继续修改(y/n)        *" << endl;
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
			cout << endl << endl << endl << endl;
			cout << "\t\t\t*********************************" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*      价格不一致,修改失败！    *" << endl;
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
			goto loop1;
		}
		char c;
		c = _getch();
		if (c == 'y' || c == 'Y') Modify1();
		Print();
		delete[]s;
	}
}
void sys::Modify2()
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
		cout << "\t\t\t*      菜单为空，修改失败!      *" << endl;
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
		cout << "\t\t\t请输入需要修改的餐单序号" << endl;
		char * s = new char[20];
		cin >> s;
		system("cls");
		cout << endl << endl << endl << endl;
		cout << "\t\t\t*********************************" << endl;
		cout << "\t\t\t需要修改的数据如下：";
		num = atoi(s);
		a[num].output2();
		Sleep(1000);
		system("cls");
	loop1:a[num].input();
		if (test())
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
			if (count>10)
				cout << "\t\t\t*    修改成功！订单编号为" << num << "     *" << endl;
			else
				cout << "\t\t\t*    修改成功！订单编号为0" << num << "     *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*      是否继续修改(y/n)        *" << endl;
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
			cout << endl << endl << endl << endl;
			cout << "\t\t\t*********************************" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*                               *" << endl;
			cout << "\t\t\t*      价格不一致,修改失败！    *" << endl;
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
			goto loop1;
		}
		char c;
		c = _getch();
		if (c == 'y' || c == 'Y') Modify2();
		Print();
		delete[]s;
	}
}
