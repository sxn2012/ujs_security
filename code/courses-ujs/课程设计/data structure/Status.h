#pragma once
#ifndef STATUS_HEADER
#define STATUS_HEADER
#include <string>
//#include <graphics.h>
using namespace std;
enum Status{SUCCESS,FAILURE};//定义Status枚举类型

struct DATA //数据结构体
{
	string MemName;//姓名
	int BirthYear;//生年
	int DeathYear;//卒年
	DATA()//无参构造函数
	{
		MemName = "";
		BirthYear = 0;
		DeathYear = 0;
	}
	DATA(string name, int year_birth, int year_death)//有参构造函数
	{
		MemName = name;
		BirthYear = year_birth;
		DeathYear = year_death;
	}
	DATA(DATA &d)//拷贝构造函数
	{
		this->MemName = d.MemName;
		this->BirthYear = d.BirthYear;
		this->DeathYear = d.DeathYear;
	}
};

#endif


