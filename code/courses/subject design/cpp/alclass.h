/********************************************
文件名：alclass.h
功能：头文件
********************************************/
#include<iostream>
#include<string>
#include<conio.h>
#include<windows.h>
#include<time.h>
#include<stdlib.h>
#include<process.h>
using namespace std;
class dish//菜类
{
public:
	double score;//评分
	string name;//菜名
	int num;//菜的编号
	double price;//单价
	dish();
	~dish();
	void input();
	void output1();
	void output2();
	double Calculate();
};
class menu//菜单类
{
private:
	int del;//删除后等于count
public:
	int count;//菜种类的数量
	dish a[100];
	void input();
	void output1();
	void output2();
	void remove();
	double Calculate();
	menu();
	~menu();
};
class sys//系统类
{
private:
	menu a[100];
	int count;//菜单的数量
public:
	void Print();
	void Order();
	void Manage();
	void Calculate();
	void History();
	void Mark();
	void Add();
	void Modify1();
	void Modify2();
	void Delete();
	void Search1();
	void Search2();
	void Quit();
	void Shut();
	bool test();
	sys();
	~sys();
};