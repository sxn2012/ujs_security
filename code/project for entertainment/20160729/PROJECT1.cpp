/*// PROJECT1.cpp : Defines the entry point for the console application.
//



========================================================================
       CONSOLE APPLICATION : PROJECT1
========================================================================


AppWizard has created this PROJECT1 application for you.  

This file contains a summary of what you will find in each of the files that
make up your Project1 application.

PROJECT1.dsp
    This file (the project file) contains information at the project level and
    is used to build a single project or subproject. Other users can share the
    project (.dsp) file, but they should export the makefiles locally.

PROJECT1.cpp
    This is the main application source file.


/////////////////////////////////////////////////////////////////////////////
Other standard files:

StdAfx.h, StdAfx.cpp
    These files are used to build a precompiled header (PCH) file
    named Project1.pch and a precompiled types file named StdAfx.obj.


/////////////////////////////////////////////////////////////////////////////
Other notes:

AppWizard uses "TODO:" to indicate parts of the source code you
should add to or customize.

/////////////////////////////////////////////////////////////////////////////
CAUTION:


THIS PROJECT IS BASED ON ONE-SIDE LIST.



*/
#include "stdafx.h"
#include <iostream>
using namespace std;

struct SNode
{
	DATA data;
	SNode *pNext;
}*g_pHead;
DATA input()
{
	DATA da;
	char a[50],b[50],c[20],d[20],e[20];
	char *p=NULL;
	int sum=0;
	bool t=false;
	cout<<"请输入学号:";
	cin>>a;
	for (p=a;*p!='\0';p++)
		if (((*p<'0')||(*p>'9')))
		{
			t=true;
			break;
		}
	while (t)
	{
		cout<<"输入错误"<<endl;
		system("pause");
		system("cls");
		cout<<"请输入学号:";
		cin>>a;
		for (p=a;*p!='\0';p++)
			if (((*p<'0')||(*p>'9'))) 
			{
				t=true;
				break;
			}
		if (*p=='\0') t=false;
	}
	int snum=atoi(a);
	while (!testnum(snum))
	{
		cout<<"学号已存在，请重新输入"<<endl;
		system("pause");
		system("cls");
		cout<<"请输入学号:";
		cin>>a;
		for (p=a;*p!='\0';p++)
			if (((*p<'0')||(*p>'9'))) 
			{
				t=true;
				break;
			}
		if (*p=='\0') t=false;
		snum=atoi(a);
	}
	da.Snum=atoi(a);
	do
	{	
		t=false;
		system("cls");
		cout<<"请输入姓名:";
		cin>>b;
		for (p=b;*p!='\0';p++)
			if ((*p>='0')&&(*p<='9')) 
			{
				t=true;
				break;
			}
		if (t) 
		{
			cout<<"输入错误"<<endl;
			system("pause");
		}
	}
	while (t);
	char cj;
	while (!testname(b))
	{
		cout<<"有重名，是否确定？（Y/N）";
		cin>>cj;
		while ((cj!='n')&&(cj!='N')&&(cj!='y')&&(cj!='Y'))
		{
			cout<<"输入无效，请重新输入。"<<endl;
			cin>>cj;
		}
		if ((cj=='y')||(cj=='Y')) break;
		do
		{	
			t=false;
			system("cls");
			cout<<"请输入姓名:";
			cin>>b;
			for (p=b;*p!='\0';p++)
				if ((*p>='0')&&(*p<='9')) 
				{
					t=true;
					break;
				}
				if (t) 
				{
					cout<<"输入错误"<<endl;
					system("pause");
				}
		}
		while (t);
	}
	strcpy(da.Sname,b);
	do
	{
		t=false;
		sum=0;
		system("cls");
		cout<<"请输入成绩1:";
		cin>>c;
		for (p=c;*p!='\0';p++)
			if ((*p<'0')||(*p>'9'))
			{
				if (*p='.') 
				{
					if (*(p+1)=='\0') 
					{
						t=true;
						break;
					}
					sum++;
					continue;
				}
				t=true;
				break;
			}
		if (t||(sum>1)) 
		{
			cout<<"输入错误"<<endl;
			system("pause");
		}
	}
	while (t||(sum>1));
	da.Smath=atof(c);
	do
		{
			t=false;
			sum=0;
			system("cls");
			cout<<"请输入成绩2:";
			cin>>d;
			for (p=d;*p!='\0';p++)
				if ((*p<'0')||(*p>'9'))
				{
					if (*p='.') 
					{
						if (*(p+1)=='\0') 
						{
							t=true;
							break;
						}
						sum++;
						continue;
					}
					t=true;
					break;
				}
			if (t||(sum>1)) 
			{
				cout<<"输入错误"<<endl;
				system("pause");
			}
		}
	while (t||(sum>1));
	da.Sengl=atof(d);
	do
	{
		t=false;
		sum=0;
		system("cls");
		cout<<"请输入成绩3:";
		cin>>e;
		for (p=e;*p!='\0';p++)
			if ((*p<'0')||(*p>'9')) 
			{
				if (*p='.') 
				{
					if (*(p+1)=='\0') 
					{
						t=true;
						break;
					}
					sum++;
					continue;
				}
				t=true;
				break;
			}
		if (t||(sum>1)) 
		{
			cout<<"输入错误"<<endl;
			system("pause");
		}
	}
	while (t||(sum>1));
	da.Schin=atof(e);
	cout<<"您输入的数据为："<<endl;
	output(da);
	cout<<"1.确定"<<endl;
	cout<<"2.取消"<<endl;
	int tp;
	do
	{
		cout<<"请输入操作编号:";
		cin>>tp;
		if ((tp<1)||(tp>2)) cout<<"输入无效，请重新输入。"<<endl;
	}
	while ((tp<1)||(tp>2));
	switch(tp)
	{
		case 1:
			return da;
			break;
		case 2:
			cout<<"请重新输入"<<endl;
			system("pause");
			return input();
			break;
		default:
			return input();
	}
}
void output(DATA d)
{
	cout<<d.Snum<<'\t'<<d.Sname<<'\t'<<d.Smath<<'\t'<<d.Sengl<<'\t'<<d.Schin<<endl;
}
void Addfront()//头插法
{
	DATA da=input();
	SNode *pNew=(SNode *)malloc(sizeof(SNode));
	pNew->data=da;
	pNew->pNext=g_pHead;
	g_pHead=pNew;
}
void Addback()//尾插法
{
	DATA da=input();
	SNode *pNew=(SNode *)malloc(sizeof(SNode));
	SNode *p=g_pHead;
	pNew->data=da;
	pNew->pNext=NULL;
	if (!p)
	{
		g_pHead=pNew;
		return;
	}
	while (p->pNext)
		p=p->pNext;
	p->pNext=pNew;
}
void Sequence()//顺序
{
	SNode *p=g_pHead;
	if (!p)
	{
		cout<<"数据为空"<<endl;
		return;
	}
	while (p)
	{
		output(p->data);
		p=p->pNext;
	}
}
void Antitone(SNode *p)//反序
{
	SNode *h=p;
	if (!g_pHead)
	{
		cout<<"数据为空"<<endl;
		return;
	}
	if (h)
	{
		Antitone(h->pNext);
		output(h->data);
	}
}
void Sort()//排序
{
	char c,p;
	do
	{
		system("cls");
		cout<<"1.按学号排序"<<endl;
		cout<<"2.按姓名排序"<<endl;
		cout<<"3.按成绩1排序"<<endl;
		cout<<"4.按成绩2排序"<<endl;
		cout<<"5.按成绩3排序"<<endl;
		cout<<"6.返回上一级菜单"<<endl;
		cout<<"7.返回主菜单"<<endl;
		int i;
		cout<<"请输入操作编号:";
		cin>>p;
		i=judgenum(p);
		while ((i<1)||(i>7))
		{
			cout<<"输入无效，请重新输入。"<<endl;
			cin>>p;
			i=judgenum(p);
		}
		switch (i)
		{
		case 1:
			SortNum();
			break;
		case 2:
			SortName();
			break;
		case 3:
			Sort1();
			break;
		case 4:
			Sort2();
			break;
		case 5:
			Sort3();
			break;
		case 6:case 7:
			Menu();
			return;
		}
		cout<<"是否继续排序（Y或N）:";
		cin>>c;
		while ((c!='n')&&(c!='N')&&(c!='y')&&(c!='Y'))
		{
			cout<<"输入无效，请重新输入。"<<endl;
			cin>>c;
		}
	}
	while ((c=='y')||(c=='Y'));
}
void SortNum()
{
	char p;
	if (!g_pHead)
	{
		cout<<"数据为空"<<endl;
		return;
	}
	system("cls");
	cout<<"1.从小到大排序"<<endl;
	cout<<"2.从大到小排序"<<endl;
	cout<<"3.返回上一级菜单"<<endl;
	cout<<"4.返回主菜单"<<endl;
	int i;
	cout<<"请输入操作编号:";
	cin>>p;
	i=judgenum(p);
	while ((i<1)||(i>4))
	{
		cout<<"输入无效，请重新输入。"<<endl;
		cin>>p;
		i=judgenum(p);
	}
	switch (i)
	{
	case 1:
		SortNum_1();
		break;
	case 2:
		SortNum_2();
		break;
	case 3:
		Sort();
		return;
	case 4:
		Menu();
		return;
	}
	
}
void SortNum_1()
{
	DATA temp;
	SNode *p=g_pHead;
	SNode *q=g_pHead;
	while (p)
	{
		q=p->pNext;
		while (q)
		{
			if (q->data.Snum<p->data.Snum)
			{
				temp=q->data;
				q->data=p->data;
				p->data=temp;
			}
			q=q->pNext;
		}
		p=p->pNext;
	}
	Sequence();
}

void SortNum_2()
{
	DATA temp;
	SNode *p=g_pHead;
	SNode *q=g_pHead;
	while (p)
	{
		q=p->pNext;
		while (q)
		{
			if (q->data.Snum>p->data.Snum)
			{
				temp=q->data;
				q->data=p->data;
				p->data=temp;
			}
			q=q->pNext;
		}
		p=p->pNext;
	}
	Sequence();
}
void SortName()
{
	char p;
	if (!g_pHead)
	{
		cout<<"数据为空"<<endl;
		return;
	}
	system("cls");
	cout<<"1.从小到大排序"<<endl;
	cout<<"2.从大到小排序"<<endl;
	cout<<"3.返回上一级菜单"<<endl;
	cout<<"4.返回主菜单"<<endl;
	int i;
	cout<<"请输入操作编号:";
	cin>>p;
	i=judgenum(p);
	while ((i<1)||(i>4))
	{
		cout<<"输入无效，请重新输入。"<<endl;
		cin>>p;
		i=judgenum(p);
	}
	switch (i)
	{
	case 1:
		SortName_1();
		break;
	case 2:
		SortName_2();
		break;
	case 3:
		Sort();
		return;
	case 4:
		Menu();
		return;
	}
}
void SortName_1()
{
	DATA temp;
	SNode *p=g_pHead;
	SNode *q=g_pHead;
	while (p)
	{
		q=p->pNext;
		while (q)
		{
			if (strcmp(q->data.Sname,p->data.Sname)<0)
			{
				temp=q->data;
				q->data=p->data;
				p->data=temp;
			}
			q=q->pNext;
		}
		p=p->pNext;
	}
	Sequence();
}
void SortName_2()
{
	DATA temp;
	SNode *p=g_pHead;
	SNode *q=g_pHead;
	while (p)
	{
		q=p->pNext;
		while (q)
		{
			if (strcmp(q->data.Sname,p->data.Sname)>0)
			{
				temp=q->data;
				q->data=p->data;
				p->data=temp;
			}
			q=q->pNext;
		}
		p=p->pNext;
	}
	Sequence();
}
void Sort1()
{
	char p;
	if (!g_pHead)
	{
		cout<<"数据为空"<<endl;
		return;
	}
	system("cls");
	cout<<"1.从小到大排序"<<endl;
	cout<<"2.从大到小排序"<<endl;
	cout<<"3.返回上一级菜单"<<endl;
	cout<<"4.返回主菜单"<<endl;
	int i;
	cout<<"请输入操作编号:";
	cin>>p;
	i=judgenum(p);
	while ((i<1)||(i>4))
	{
		cout<<"输入无效，请重新输入。"<<endl;
		cin>>p;
		i=judgenum(p);
	}
	switch (i)
	{
	case 1:
		Sort1_1();
		break;
	case 2:
		Sort1_2();
		break;
	case 3:
		Sort();
		return;
	case 4:
		Menu();
		return;
	}
}
void Sort1_1()
{
	DATA temp;
	SNode *p=g_pHead;
	SNode *q=g_pHead;
	while (p)
	{
		q=p->pNext;
		while (q)
		{
			if (q->data.Smath<p->data.Smath)
			{
				temp=q->data;
				q->data=p->data;
				p->data=temp;
			}
			q=q->pNext;
		}
		p=p->pNext;
	}
	Sequence();
	
}
void Sort1_2()
{
	DATA temp;
	SNode *p=g_pHead;
	SNode *q=g_pHead;
	while (p)
	{
		q=p->pNext;
		while (q)
		{
			if (q->data.Smath>p->data.Smath)
			{
				temp=q->data;
				q->data=p->data;
				p->data=temp;
			}
			q=q->pNext;
		}
		p=p->pNext;
	}
	Sequence();
}
void Sort2()
{
	char p;
	if (!g_pHead)
	{
		cout<<"数据为空"<<endl;
		return;
	}
	system("cls");
	cout<<"1.从小到大排序"<<endl;
	cout<<"2.从大到小排序"<<endl;
	cout<<"3.返回上一级菜单"<<endl;
	cout<<"4.返回主菜单"<<endl;
	int i;
	cout<<"请输入操作编号:";
	cin>>p;
	i=judgenum(p);
	while ((i<1)||(i>4))
	{
		cout<<"输入无效，请重新输入。"<<endl;
		cin>>p;
		i=judgenum(p);
	}
	switch (i)
	{
	case 1:
		Sort2_1();
		break;
	case 2:
		Sort2_2();
		break;
	case 3:
		Sort();
		return;
	case 4:
		Menu();
		return;
	}
}
void Sort2_1()
{
	DATA temp;
	SNode *p=g_pHead;
	SNode *q=g_pHead;
	while (p)
	{
		q=p->pNext;
		while (q)
		{
			if (q->data.Sengl<p->data.Sengl)
			{
				temp=q->data;
				q->data=p->data;
				p->data=temp;
			}
			q=q->pNext;
		}
		p=p->pNext;
	}
	Sequence();
}
void Sort2_2()
{
	DATA temp;
	SNode *p=g_pHead;
	SNode *q=g_pHead;
	while (p)
	{
		q=p->pNext;
		while (q)
		{
			if (q->data.Sengl>p->data.Sengl)
			{
				temp=q->data;
				q->data=p->data;
				p->data=temp;
			}
			q=q->pNext;
		}
		p=p->pNext;
	}
	Sequence();
}
void Sort3()
{
	char p;
	if (!g_pHead)
	{
		cout<<"数据为空"<<endl;
		return;
	}
	system("cls");
	cout<<"1.从小到大排序"<<endl;
	cout<<"2.从大到小排序"<<endl;
	cout<<"3.返回上一级菜单"<<endl;
	cout<<"4.返回主菜单"<<endl;
	int i;
	cout<<"请输入操作编号:";
	cin>>p;
	i=judgenum(p);
	while ((i<1)||(i>4))
	{
		cout<<"输入无效，请重新输入。"<<endl;
		cin>>p;
		i=judgenum(p);
	}
	switch (i)
	{
	case 1:
		Sort3_1();
		break;
	case 2:
		Sort3_2();
		break;
	case 3:
		Sort();
		return;
	case 4:
		Menu();
		return;
	}
}
void Sort3_1()
{
	DATA temp;
	SNode *p=g_pHead;
	SNode *q=g_pHead;
	while (p)
	{
		q=p->pNext;
		while (q)
		{
			if (q->data.Schin<p->data.Schin)
			{
				temp=q->data;
				q->data=p->data;
				p->data=temp;
			}
			q=q->pNext;
		}
		p=p->pNext;
	}
	Sequence();
}
void Sort3_2()
{
	DATA temp;
	SNode *p=g_pHead;
	SNode *q=g_pHead;
	while (p)
	{
		q=p->pNext;
		while (q)
		{
			if (q->data.Schin>p->data.Schin)
			{
				temp=q->data;
				q->data=p->data;
				p->data=temp;
			}
			q=q->pNext;
		}
		p=p->pNext;
	}
	Sequence();
}
void Browse()//浏览
{
	char c,p;
	do
	{
		system("cls");
		cout<<"1.顺序浏览"<<endl;
		cout<<"2.倒序浏览"<<endl;
		cout<<"3.返回上一级菜单"<<endl;
		cout<<"4.返回主菜单"<<endl;
		int i;
		cout<<"请输入操作编号:";
		cin>>p;
		i=judgenum(p);
		while ((i<1)||(i>4))
		{
			cout<<"输入无效，请重新输入。"<<endl;
			cin>>p;
			i=judgenum(p);
		}
		switch (i)
		{
		case 1:
			Sequence();
			system("pause");
			break;
		case 2:
			Antitone(g_pHead);
			system("pause");
			break;
		case 3:case 4:
			Menu();
			return;
		}
		cout<<"是否继续浏览（Y或N）:";
		cin>>c;
	}
	while ((c=='y')||(c=='Y'));
}
void Insert()//插入
{
	char c,p;
	do
	{
		system("cls");
		cout<<"1.插入到头"<<endl;
		cout<<"2.插入到尾"<<endl;
		cout<<"3.返回上一级菜单"<<endl;
		cout<<"4.返回主菜单"<<endl;
		int i;
		cout<<"请输入操作编号:";
		cin>>p;
		i=judgenum(p);
		while ((i<1)||(i>4))
		{
			cout<<"输入无效，请重新输入。"<<endl;
			cin>>p;
			i=judgenum(p);
		}
		switch (i)
		{
		case 1:
			Addfront();
			break;
		case 2:
			Addback();
			break;
		case 3:case 4:
			Menu();
			return;
		}
		cout<<"插入成功"<<endl;
		cout<<"所有数据如下："<<endl;
		Sequence();
		cout<<"是否继续插入（Y或N）:";
		cin>>c;
		while ((c!='n')&&(c!='N')&&(c!='y')&&(c!='Y'))
		{
			cout<<"输入无效，请重新输入。"<<endl;
			cin>>c;
		}
	}
	while ((c=='y')||(c=='Y'));
}
void Delete()//删除
{
	system("cls");
	char c;
	if (!g_pHead)
	{
		cout<<"数据为空"<<endl;
		system("pause");
		return;
	}
	do
	{
		system("cls");
		cout<<"所有数据如下："<<endl;
		Sequence();
		SNode *p=g_pHead;
		cout<<"请输入需要删除的学生学号:"<<endl;
		int n;
		int s=0;
		cin>>n;
		int tp;
		SNode *q;
		if (p->data.Snum==n)
		{
			cout<<"即将删除："<<endl;
			output(p->data);
			cout<<"1.确定"<<endl;
			cout<<"2.取消"<<endl;
			do
			{
				cout<<"请输入操作编号:";
				cin>>tp;
				if ((tp<1)||(tp>2)) cout<<"输入无效，请重新输入。"<<endl;
			}
			while ((tp<1)||(tp>2));
			if (tp==1)
			{
				g_pHead=p->pNext;
				free(p);
				cout<<"删除成功"<<endl;
				system("pause");
				return;
			}
			else 
			{
				system("cls");
				Delete();
			}
		}
		while (p)
		{
			if (p->data.Snum==n)
			{
				cout<<"即将删除："<<endl;
				output(p->data);
				cout<<"1.确定"<<endl;
				cout<<"2.取消"<<endl;
				do
				{
					cout<<"请输入操作编号:";
					cin>>tp;
					if ((tp<1)||(tp>2)) cout<<"输入无效，请重新输入。"<<endl;
				}
				while ((tp<1)||(tp>2));
				if (tp==1)
				{
					q->pNext=p->pNext;
					free(p);
					cout<<"删除成功"<<endl;
					s=1;
					system("pause");
					break;
				}
				else 
				{
					system("pause");
					Delete();
				}
			}
			q=p;//q是p的前驱节点	
			p=p->pNext;
		}
		if (!s) 
		{
			cout<<"未找到该数据,请重新输入"<<endl;
			system("pause");
			Delete();
		}
		cout<<"是否继续删除（Y或N）:";
		cin>>c;
		while ((c!='n')&&(c!='N')&&(c!='y')&&(c!='Y'))
		{
			cout<<"输入无效，请重新输入。"<<endl;
			cin>>c;
		}
	}
	while ((c=='y')||(c=='Y'));
}
void Modify()//修改
{
	char c;
	char a[50];
	char *k=NULL;
	bool t=false;
	if (!g_pHead)
	{
		cout<<"数据为空"<<endl;
		system("pause");
		return;
	}
	do
	{
		system("cls");
		cout<<"所有数据如下："<<endl;
		Sequence();
		SNode *p=g_pHead;
		cout<<"请输入需要修改的学生学号:"<<endl;
		int n;
		int s=0;
		cin>>a;
		for (k=a;*k!='\0';k++)
			if (((*k<'0')||(*k>'9')))
			{
				t=true;
				break;
			}
		while (t)
		{
			cout<<"输入错误"<<endl;
			system("pause");
			system("cls");
			cout<<"请输入需要修改的学生学号:";
			cin>>a;
			for (k=a;*k!='\0';k++)
			if (((*k<'0')||(*k>'9'))) 
			{
				t=true;
				break;
			}
			if (*k=='\0') t=false;
		}
		n=atoi(a);
		while (p)
		{
			if (p->data.Snum==n)
			{
				output(p->data);
				cout<<"请输入修改后的数据:"<<endl;
				p->data.Snum=-1;
				strcpy(p->data.Sname,"");
				p->data=input();
				s=1;
				cout<<"修改成功"<<endl;
				system("pause");
				break;
			}
			p=p->pNext;
		}
		if (!s) 
		{
			cout<<"未找到该数据,请重新输入"<<endl;
			system("pause");
			Modify();
		}
		cout<<"是否继续修改（Y或N）:";
		cin>>c;
		while ((c!='n')&&(c!='N')&&(c!='y')&&(c!='Y'))
		{
			cout<<"输入无效，请重新输入。"<<endl;
			cin>>c;
		}
	}
	while ((c=='y')||(c=='Y'));
}
void FindNum()
{
	char a[50];
	char *k=NULL;
	bool t=false;
	if (!g_pHead)
	{
		cout<<"数据为空"<<endl;
		system("pause");
		return;
	}
	SNode *p=g_pHead;
	cout<<"请输入需要查找的学生学号:"<<endl;
	int n;
	int s=0;
	cin>>a;
	for (k=a;*k!='\0';k++)
		if (((*k<'0')||(*k>'9')))
		{
			t=true;
			break;
		}
	while (t)
	{
		cout<<"输入错误"<<endl;
		system("pause");
		system("cls");
		cout<<"请输入需要查找的学生学号:";
		cin>>a;
		for (k=a;*k!='\0';k++)
		if (((*k<'0')||(*k>'9'))) 
		{
			t=true;
			break;
		}
		if (*k=='\0') t=false;
	}
	n=atoi(a);	
	cout<<"数据是:"<<endl;
	while (p)
	{
		if (p->data.Snum==n)
		{
			output(p->data);
			s=1;			
		}
		p=p->pNext;
	}
	if (s==1)
		system("pause");
	if (!s) 
	{
		system("cls");
		cout<<"未找到该数据,请重新输入"<<endl;
		system("pause");
		FindNum();
	}
}
void FindName()
{
	char b[50];
	char *k=NULL;
	bool t=false;
	if (!g_pHead)
	{
		cout<<"数据为空"<<endl;
		system("pause");
		return;
	}
	SNode *p=g_pHead;
	cout<<"请输入需要查找的学生姓名:"<<endl;
	char n[50];
	int s=0;
	cin>>b;
	for (k=b;*k!='\0';k++)
		if ((*k>='0')&&(*k<='9')) 
		{
			t=true;
			break;
		}
	if (*k=='\0') t=false;
	while (t)
	{
		cout<<"输入错误"<<endl;
		system("pause");
		system("cls");
		cout<<"请输入需要查找的学生姓名:";
		cin>>b;
		for (k=b;*k!='\0';k++)
		if (((*k<'0')||(*k>'9'))) 
		{
			t=true;
			break;
		}
		if (*k=='\0') t=false;
	}
	strcpy(n,b);
	cout<<"数据是:"<<endl;
	while (p)
	{
		if (strcmp(p->data.Sname,n)==0)
		{
			output(p->data);
			s=1;
		}
		p=p->pNext;
	}
	if (s==1)
		system("pause");
	if (!s) 
	{
		system("cls");
		cout<<"未找到该数据,请重新输入"<<endl;
		system("pause");
		FindName();
	}
}
void Find1()
{
	char a[20];
	char *k=NULL;
	bool t=false;
	if (!g_pHead)
	{
		cout<<"数据为空"<<endl;
		system("pause");
		return;
	}
	SNode *p=g_pHead;
	cout<<"请输入需要查找的学生成绩1:"<<endl;
	int n;
	int s=0;
	cin>>a;
	for (k=a;*k!='\0';k++)
		if (((*k<'0')||(*k>'9')))
		{
			t=true;
			break;
		}
	while (t)
	{
		cout<<"输入错误"<<endl;
		system("pause");
		system("cls");
		cout<<"请输入需要查找的学生成绩1:";
		cin>>a;
		for (k=a;*k!='\0';k++)
		if (((*k<'0')||(*k>'9'))) 
		{
			t=true;
			break;
		}
		if (*k=='\0') t=false;
	}
	n=atoi(a);
	cout<<"数据是:"<<endl;
	while (p)
	{
		if (p->data.Smath==n)
		{
			output(p->data);
			s=1;
		}
		p=p->pNext;
	}
	if (s==1)
		system("pause");
	if (!s) 
	{
		system("cls");
		cout<<"未找到该数据,请重新输入"<<endl;
		system("pause");
		Find1();
	}
}
void Find2()
{
	char a[20];
	char *k=NULL;
	bool t=false;
	if (!g_pHead)
	{
		cout<<"数据为空"<<endl;
		system("pause");
		return;
	}
	SNode *p=g_pHead;
	cout<<"请输入需要查找的学生成绩2:"<<endl;
	int n;
	int s=0;
	cin>>a;
	for (k=a;*k!='\0';k++)
		if (((*k<'0')||(*k>'9')))
		{
			t=true;
			break;
		}
	while (t)
	{
		cout<<"输入错误"<<endl;
		system("pause");
		system("cls");
		cout<<"请输入需要查找的学生成绩2:";
		cin>>a;
		for (k=a;*k!='\0';k++)
		if (((*k<'0')||(*k>'9'))) 
		{
			t=true;
			break;
		}
		if (*k=='\0') t=false;
	}
	n=atoi(a);
	while (p)
	{
		if (p->data.Sengl==n)
		{
			cout<<"数据是:"<<endl;
			output(p->data);
			s=1;
		}
		p=p->pNext;
	}
	if (s==1)
		system("pause");
	if (!s) 
	{
		system("cls");
		cout<<"未找到该数据,请重新输入"<<endl;
		system("pause");
		Find2();
	}
}
void Find3()
{
	char a[20];
	char *k=NULL;
	bool t=false;
	if (!g_pHead)
	{
		cout<<"数据为空"<<endl;
		system("pause");
		return;
	}
	SNode *p=g_pHead;
	cout<<"请输入需要查找的学生成绩3:"<<endl;
	int n;
	int s=0;
	cin>>a;
	for (k=a;*k!='\0';k++)
		if (((*k<'0')||(*k>'9')))
		{
			t=true;
			break;
		}
	while (t)
	{
		cout<<"输入错误"<<endl;
		system("pause");
		system("cls");
		cout<<"请输入需要查找的学生成绩3:";
		cin>>a;
		for (k=a;*k!='\0';k++)
		if (((*k<'0')||(*k>'9'))) 
		{
			t=true;
			break;
		}
		if (*k=='\0') t=false;
	}
	n=atoi(a);
	cout<<"数据是:"<<endl;
	while (p)
	{
		if (p->data.Schin==n)
		{
			output(p->data);
			s=1;
		}
		p=p->pNext;
	}
	if (s==1)
		system("pause");
	if (!s) 
	{
		system("cls");
		cout<<"未找到该数据,请重新输入"<<endl;
		system("pause");
		Find3();
	}
}
bool testnum(int n)
{
	SNode *p=g_pHead;
	while (p)
	{
		if (p->data.Snum==n) return false;
		p=p->pNext;
	}
	return true;
}
bool testname(char c[])
{
	SNode *p=g_pHead;
	while (p)
	{
		if (strcmp(p->data.Sname,c)==0) return false;
		p=p->pNext;
	}
	return true;
}
void Search()//查找
{
	char c,p;
	do
	{
		system("cls");
		cout<<"1.按学号查找"<<endl;
		cout<<"2.按姓名查找"<<endl;
		cout<<"3.按成绩1查找"<<endl;
		cout<<"4.按成绩2查找"<<endl;
		cout<<"5.按成绩3查找"<<endl;
		cout<<"6.返回上一级菜单"<<endl;
		cout<<"7.返回主菜单"<<endl;
		int i;
		cout<<"请输入操作编号:";
		cin>>p;
		i=judgenum(p);
		while ((i<1)||(i>7))
		{
			cout<<"输入无效，请重新输入。"<<endl;
			cin>>p;
			i=judgenum(p);
		}
		switch (i)
		{
		case 1:
			FindNum();
			break;
		case 2:
			FindName();
			break;
		case 3:
			Find1();
			break;
		case 4:
			Find2();
			break;
		case 5:
			Find3();
			break;
		case 6:case 7:
			Menu();
			return;
		}
		cout<<"是否继续查找（Y或N）:";
		cin>>c;
		while ((c!='n')&&(c!='N')&&(c!='y')&&(c!='Y'))
		{
			cout<<"输入无效，请重新输入。"<<endl;
			cin>>c;
		}
	}
	while ((c=='y')||(c=='Y'));
}
void leave()//防止内存泄漏
{
	system("cls");
	SNode *p=g_pHead;
	while (g_pHead)
	{
		p=g_pHead->pNext;
		free(g_pHead);
		g_pHead=p;
	}
	cout<<"退出成功"<<endl;
	system("pause");
}
void Menu()//主菜单
{
	system("cls");
	cout<<"1.浏览所有信息"<<endl;
	cout<<"2.添加信息"<<endl;
	cout<<"3.删除信息"<<endl;
	cout<<"4.修改信息"<<endl;
	cout<<"5.查找信息"<<endl;
	cout<<"6.排序信息"<<endl;
	cout<<"7.退出"<<endl;
	int i=0;
	char p;
	do
	{
		cout<<"请输入操作编号:";
		cin>>p;
		i=judgenum(p);
		if ((i<1)||(i>7)) cout<<"输入无效，请重新输入。"<<endl;
	}
	while ((i<1)||(i>7));
	switch (i)
	{
		case 1:
			Browse();//浏览
			break;
		case 2:
			Insert();//插入
			break;
		case 3:
			Delete();//删除
			break;
		case 4:
			Modify();//修改
			break;
		case 5:
			Search();//搜索
			break;
		case 6:
			Sort();//排序
			break;
		default:
			leave();
			exit(0);
			return;
	}
}
int judgenum(char h)
{
	if ((h<'0')||(h>'9')) return -1;
	else return h-'0';
}
int main(int argc, char* argv[])
{
	while (1)
		Menu();
	return 0;
}
/*
1.验证输入的有效性（学号是否重复，姓名重复时候显示提示信息）
2.提高软件的可用性（删除修改前显示字段，添加完成后显示字段）
3.文件的输入输出
4.删除方式的多样性（按照学号、姓名、成绩1、2、3删除）
5.排序方式的多样性（姓名相同时按照学号排序等等)
6.加入总分、每人平均分、每项平均分
7.输出时加入表头
*/