/********************************************
文件名：sysfun1.cpp
功能：sys的成员函数1
********************************************/
#include"alclass.h"
void sys::Print()
{
	system("cls");
	cout << endl << endl << endl << endl;
	cout << "\t\t\t*********************************" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*           1.点餐管理          *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*           2.订单管理          *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*           3.结账管理          *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*           4.查看历史          *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*           5.菜谱评分          *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*           6.退出系统          *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*********************************" << endl;
	SYSTEMTIME sy;
	GetLocalTime(&sy);
	cout << "\t\t\t\t\t  " << sy.wYear << "年" << sy.wMonth << "月" << sy.wDay << "日" << endl;
	cout << "\t\t\t\t\t  " << sy.wHour << ":" << sy.wMinute << ":" << sy.wSecond << endl;
	int n;
	char c = _getch();
	n = c - '0';
	switch (n)
	{
	case 1:Order(); break;
	case 2:Manage(); break;
	case 3:Calculate(); break;
	case 4:History(); break;
	case 5:Mark(); break;
	case 6:Quit(); break;
	default:Shut(); break;
	}
}
void sys::Quit()
{
	system("cls");
	cout << endl << endl << endl << endl;
	cout << "\t\t\t*********************************" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*      你确定要退出吗(y/n)      *" << endl;
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
	cout << "\t\t\t\t\t  " << sy.wYear << "年" << sy.wMonth << "月" << sy.wDay << "日" << endl;
	cout << "\t\t\t\t\t  " << sy.wHour << ":" << sy.wMinute << ":" << sy.wSecond << endl;
	char c;
	c = _getch();
	if (c == 'y' || c == 'Y')
	{
		system("cls");
		cout << endl << endl << endl << endl;
		cout << "\t\t\t*********************************" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*  再见，欢迎下次再次使用本系统 *" << endl;
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
		cout << "\t\t\t\t\t  " << sy.wYear << "年" << sy.wMonth << "月" << sy.wDay << "日" << endl;
		cout << "\t\t\t\t\t  " << sy.wHour << ":" << sy.wMinute << ":" << sy.wSecond << endl;
		exit(0);
	}
}
void sys::Shut()
{
	system("cls");
	system("color 1F");
	cout << endl << endl << endl << endl;
	cout << "\t\t*****************************************" << endl;
	cout << "\t\t*                                       *" << endl;
	cout << "\t\t*                                       *" << endl;
	cout << "\t\t*                                       *" << endl;
	cout << "\t\t*                                       *" << endl;
	cout << "\t\t*                                       *" << endl;
	cout << "\t\t*                                       *" << endl;
	cout << "\t\t*                                       *" << endl;
	cout << "\t\t* 对不起，你的错误操作导致了本系统崩溃。*" << endl;
	cout << "\t\t*                                       *" << endl;
	cout << "\t\t*                                       *" << endl;
	cout << "\t\t*                                       *" << endl;
	cout << "\t\t*                                       *" << endl;
	cout << "\t\t*                                       *" << endl;
	cout << "\t\t*                                       *" << endl;
	cout << "\t\t*                                       *" << endl;
	cout << "\t\t*                                       *" << endl;
	cout << "\t\t*****************************************" << endl;
	SYSTEMTIME sy;
	GetLocalTime(&sy);
	cout << "\t\t\t\t\t  " << sy.wYear << "年" << sy.wMonth << "月" << sy.wDay << "日" << endl;
	cout << "\t\t\t\t\t  " << sy.wHour << ":" << sy.wMinute << ":" << sy.wSecond << endl;
	Sleep(1000);
	//system("shutdown -s -t 0");
	exit(1);
}
void sys::Order()//点餐管理
{
	system("cls");
	cout << endl << endl << endl << endl;
	cout << "\t\t\t*********************************" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*           1.增加餐单          *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*           2.修改餐单          *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*           3.删除餐单          *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*           4.查看餐单          *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*           5.返回上一级菜单    *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*           6.返回主菜单        *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*********************************" << endl;
	SYSTEMTIME sy;
	GetLocalTime(&sy);
	cout << "\t\t\t\t\t  " << sy.wYear << "年" << sy.wMonth << "月" << sy.wDay << "日" << endl;
	cout << "\t\t\t\t\t  " << sy.wHour << ":" << sy.wMinute << ":" << sy.wSecond << endl;
	int n;
	char c = _getch();
	n = c - '0';
	switch (n)
	{
	case 1:Add(); break;
	case 2:Modify1(); break;
	case 3:Delete(); break;
	case 4:Search1(); break;
	case 5:case 6:Print(); break;
	default:Shut(); break;
	}
}
void sys::Manage()//订单管理
{
	system("cls");
	cout << endl << endl << endl << endl;
	cout << "\t\t\t*********************************" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*           1.增加订单          *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*           2.修改订单          *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*           3.删除订单          *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*           4.查看订单          *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*           5.返回上一级菜单    *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*           6.返回主菜单        *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*********************************" << endl;
	SYSTEMTIME sy;
	GetLocalTime(&sy);
	cout << "\t\t\t\t\t  " << sy.wYear << "年" << sy.wMonth << "月" << sy.wDay << "日" << endl;
	cout << "\t\t\t\t\t  " << sy.wHour << ":" << sy.wMinute << ":" << sy.wSecond << endl;
	int n;
	char c = _getch();
	n = c - '0';
	switch (n)
	{
	case 1:Add(); break;
	case 2:Modify2(); break;
	case 3:Delete(); break;
	case 4:Search2(); break;
	case 5:case 6:Print(); break;
	default:Shut(); break;
	}
}
void sys::Calculate()//结账管理
{
	int k;
	double sum = 0;
	system("cls");
	cout << endl << endl << endl << endl;
	cout << "\t\t\t*********************************" << endl;
	cout << "\t\t\t";
	cout << "请输入需要结账的订单号:";
	cin >> k;
	if (a[k].count <= 0)
	{
		cout << "\t\t\t订单号不存在!" << endl;
		Sleep(1000);
		Print();
	}
	else
	{
		sum += a[k].Calculate();
		cout << "\t\t\t总价为" << sum << endl;
		Sleep(1000);
		Print();
	}

}
