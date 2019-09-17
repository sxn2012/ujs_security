/********************************************
文件名：conde.cpp
功能：构造、析构函数
********************************************/
#include"alclass.h"
dish::dish()
{
	num = 0;
	price = 0;
	score = 0;
	name ="";
}
dish::~dish(){}
menu::menu()
{
	for (int i = 0; i<10; i++) a[i] = dish();
	count = 0;
}
menu::~menu(){}
sys::sys()
{
	for (int i = 0; i<10; i++)
		a[i] = menu();
	count = 0;
	system("color 60");
	cout << endl << endl << endl << endl;
	cout << "\t\t\t*********************************" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*    欢迎进入酒店点餐管理系统   *" << endl;
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
	/*system("cls");
	cout << endl << endl << endl << endl;
	cout << "\t\t\t*********************************" << endl;
	cout << "\t\t\t默认用户名和密码均为root" << endl;
	cout << "\t\t\t*********************************" << endl;
	cout << "\t\t\t用户名：";
	char * s1 = new char[50];
	cin >> s1;
	cout << "\t\t\t密码：";
	char s2[50];
	int j = 0;
	do{
		s2[j] = _getch();
	if (s2[j]!=13)	
		if (s2[j]!=8) cout << "*";
		else {
			cout << "\b \b"; s2[j] = 0; j--;
		}
	else break;
		j++;
	} while (1);*/
}
sys::~sys(){}