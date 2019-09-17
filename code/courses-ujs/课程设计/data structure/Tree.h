#pragma once
#ifndef TREE_HEADER
#define TREE_HEADER
#include <graphics.h>//EasyX图形库文件
#include "Status.h"
#include "TreeNode.h"
#include "LinkQueue.h"
class Tree
{
private:
	struct TreeNode *root;//根结点
public:
	Tree();//无参构造函数
	Tree(DATA &d);//有参构造函数
	virtual ~Tree();//析构函数
	Status Init();//初始化，建立初始的树
	void Menu();//开始界面
protected:
	//辅助函数
	int  GetGeneration(TreeNode *r);//返回结点是第几代
	int GetNumber(TreeNode *r);//返回结点是一代中的第几个
	TreeNode *GetParent(TreeNode *r);//返回结点的双亲节点
	void LOrder();//层序遍历
	void NOrder(TreeNode *r);//普通遍历
	void Print(TreeNode *r );
	Status GetNode(string name, struct TreeNode *&node,struct TreeNode *r)const;//通过姓名查找结点
	Status Saving();//将结果保存到文件中
	Status Insert(struct TreeNode *tn, string ParentName);//插入新的结点
	void Delete(TreeNode *&node);//删除结点
	void Enquire_Generation(int gen);
	/*
		各级菜单内部实现的方法
	*/
	void Choice();//主菜单
	void Menu_1();//一级菜单（显示）
	void Menu_2();//二级菜单（添加）
	void Menu_3();//三级菜单（修改）
	void Menu_4();//四级菜单（删除）
	void Menu_5();//五级菜单（查询）
	void Menu_6();//六级菜单（退出）
	void Menu_7();//七级菜单（恢复默认）
	/*
		子菜单
	*/
	void Menu_3_1(TreeNode*&node);//三级子菜单中修改姓名
	void Menu_3_2(TreeNode*&node);//三级子菜单中修改生年
	void Menu_3_3(TreeNode*&node);//三级子菜单中修改卒年
	//
	void Menu_5_1();//五级子菜单中按姓名查询
	void Menu_5_2();//五级子菜单中按代查询
	void Menu_5_3();//五级子菜单中查询孩子
	void Menu_5_4();//五级子菜单中查询父亲
	void Menu_5_5();//五级子菜单中查询兄弟
};
TreeNode *Tree::GetParent(TreeNode *r)
{
	LinkQueue<TreeNode *>q;
	TreeNode *p = NULL;
	TreeNode *temp;
	if (r==NULL||r==root)
	{
		return NULL;
	}
	if (root)
	{
		q.EnQueue(root);
	}
	while (!q.IsEmpty())
	{
		q.DelQueue(p);
		TreeNode *t = p->FirstChild;
		while (t)
		{
			if (t == r) return p;
			t = t->NextSibling;
		}
		if (p->FirstChild)
		{
			q.EnQueue(p->FirstChild);
		}
		temp = p->FirstChild;
		while (temp&&temp->NextSibling)
		{
			q.EnQueue(temp->NextSibling);
			temp = temp->NextSibling;
		}

	}
}
int Tree::GetGeneration(TreeNode *r)
{
	int sum = 0;
	if (r==root)
	{
		return 1;
	}
	else
	{
		TreeNode *p = GetParent(r);
		return GetGeneration(p)+1;
	}
}
void Tree::LOrder()
{
	system("cls");
	LinkQueue<TreeNode *>q;
	TreeNode *p=NULL;
	TreeNode *temp;
	if (root)
	{
		q.EnQueue(root);
	}
	while (!q.IsEmpty())
	{
		q.DelQueue(p);
		TreeNode *t = GetParent(p);
		cout << p->data.MemName << " " << p->data.BirthYear << " " << p->data.DeathYear;
		if (p!=root)
		{
			cout << " " << t->data.MemName;
		} 
		cout << endl;
		if (p->FirstChild)
		{
			q.EnQueue(p->FirstChild);
		}
		temp = p->FirstChild;
		while (temp&&temp->NextSibling)
		{
			q.EnQueue(temp->NextSibling);
			temp = temp->NextSibling;
		}

	}
}
Tree::Tree()
{
	root =NULL;
}
Tree::Tree(DATA &d)
{
	root = new struct TreeNode(d);
}
Tree::~Tree()
{
	delete root;
}
Status Tree::GetNode(string name, struct TreeNode *&node, struct TreeNode *r)const
{
	LinkQueue<TreeNode *>q;
	TreeNode * p=NULL;
	TreeNode * temp;
	if (r)
	{
		q.EnQueue(root);
	}
	while (!q.IsEmpty())
	{
		q.DelQueue(p);
		//
		string d = "";
		p->GetName(d);
		if (d==name)
		{
			node = p;
			return SUCCESS;
		}
		//

		if (p->FirstChild)
		{
			q.EnQueue(p->FirstChild);
		}
		temp = p->FirstChild;
		while (temp&&temp->NextSibling)
		{
			q.EnQueue(temp->NextSibling);
			temp = temp->NextSibling;
		}

	}
	
	return FAILURE;
}
void Tree::NOrder(TreeNode *r)
{
	//_asm int 3
	if (r)
	{
		
		/*if (GetGeneration(r) > 2)
		{
			for (int j = 2; j < GetGeneration(r); j++)
			{
				cout << "        ";
			}
		}
			*/
	
			cout << r->data.MemName << endl ;
	}
	else 
		return;
	
	
	if (r->FirstChild)
	{
		TreeNode *p = r->FirstChild;
		
		
		
		while (p)
		{
			cout << "|";
			for (int i = 1; i < GetGeneration(p);i++)
			{
				cout << "—————";
				//cout << "\t";
			}
			
			NOrder(p);
			
			p = p->NextSibling;
			//cout << endl;

		}
	}

}
void Tree::Print(TreeNode *r)
{
	if (!r)
	{
		return;
	}
	else
	{
		int width = 1551;
		int height = 695;
		initgraph(width, height, NOCLOSE | NOMINIMIZE );

		loadimage(NULL, L"G:\\VS\\C++\\subject designing\\INHERITANCE_OF_KING\\INHERITANCE_OF_KING\\03j58PICcrW.jpg", width, height, false);
		setbkcolor(WHITE);
		HWND hWnd = GetHWnd();//获得窗口句柄
		SetWindowTextW(hWnd, L"王的传承——数据结构课程设计");//设置窗口标题
		settextcolor(GREEN);
		//int i = 0;
		//int gen = GetGeneration(r);
		BeginBatchDraw();//开始批量绘图

		LinkQueue<TreeNode *>q;
		TreeNode *p = NULL;
		TreeNode *temp;
		if (root)
		{
			q.EnQueue(root);
		}
		while (!q.IsEmpty())
		{
			q.DelQueue(p);
			//TreeNode *t = GetParent(p);
			//if (GetGeneration(p) == gen)
			{
				int y = 90 * GetGeneration(p) + 15;
				//i++;
				int x = 92 * GetNumber(p) + 30;
				CString name;
				name=p->data.MemName.c_str();
				outtextxy(x, y, name);
			}

			if (p->FirstChild)
			{
				q.EnQueue(p->FirstChild);
			}
			temp = p->FirstChild;
			while (temp&&temp->NextSibling)
			{
				q.EnQueue(temp->NextSibling);
				temp = temp->NextSibling;
			}


		}
		FlushBatchDraw();//执行未完成的绘图任务
			//return i;
		setlinecolor(MAGENTA);
		LinkQueue<TreeNode *>r;
		TreeNode *s = NULL;
		TreeNode *te;
		if (root)
		{
			r.EnQueue(root);
		}
		while (!r.IsEmpty())
		{
			r.DelQueue(s);
			//TreeNode *t = GetParent(s);
			//if (GetGeneration(p) == gen)
			TreeNode *t = s->FirstChild;
			while (t)
			{
				//int y = 90 * GetGeneration(s) + 15;
				//i++;
				//int x = 92 * GetNumber(s) + 30;
				//CString name;
				//name = s->data.MemName.c_str();
				//outtextxy(x, y, name);
				int x_x = 92 * GetNumber(s) + 50;
				int y_x = 90 * GetGeneration(s) + 29;
				int x_y = 92 * GetNumber(t) + 52;
				int y_y = 90 * GetGeneration(t) + 22;
				line(x_x, y_x, x_y, y_y);





				t = t->NextSibling;
			}

			if (s->FirstChild)
			{
				r.EnQueue(s->FirstChild);
			}
			te = s->FirstChild;
			while (te&&te->NextSibling)
			{
				r.EnQueue(te->NextSibling);
				te = te->NextSibling;
			}


		}

		FlushBatchDraw();//执行未完成的绘图任务


		EndBatchDraw();//结束批量绘图

		char c = 0;
		while (c != 27)//当读入Esc时返回
		{
			c = _getch();
		}

		
		

		//_getch();

		closegraph();
	}
}
int Tree::GetNumber(TreeNode *r)
{
	int i = 0;
	int gen = GetGeneration(r);
	LinkQueue<TreeNode *>q;
	TreeNode *p = NULL;
	TreeNode *temp;
	if (root)
	{
		q.EnQueue(root);
	}
	while (!q.IsEmpty())
	{
		q.DelQueue(p);
		TreeNode *t = GetParent(p);
		if (GetGeneration(p) == gen)
		{
			
			i++;
			if (p==r)
			{
				break;
			}
		}

		if (p->FirstChild)
		{
			q.EnQueue(p->FirstChild);
		}
		temp = p->FirstChild;
		while (temp&&temp->NextSibling)
		{
			q.EnQueue(temp->NextSibling);
			temp = temp->NextSibling;
		}
		

	}
	return i;
}
Status Tree::Insert(struct TreeNode *tn,string ParentName)
{
	if (!root)
	{
		root = tn;
		return SUCCESS;
	} 
	else
	{
		struct TreeNode *p = root;
		struct TreeNode *q = root;
		struct TreeNode *node=NULL;
		if (GetNode(ParentName,node,root)==SUCCESS)
		{
			if (node->FirstChild)
			{
				p = node->FirstChild;
				/*while (p->NextSibling)
				{
					p = p->NextSibling;
				}
				p->NextSibling = tn;
				tn->NextSibling = NULL;*/
				while (p&&p->data.BirthYear<=tn->data.BirthYear)
				{
					q = p;
					p=p->NextSibling;
				}
				if (p)
				{
					if (p==node->FirstChild)
					{
						node->FirstChild = tn;
						tn->NextSibling = p;
					}
					q->NextSibling = tn;
					tn->NextSibling = p;
				}
				else 
				{
					q->NextSibling = tn;
					tn->NextSibling = NULL;
				}
				return SUCCESS;
			}
			else
			{
				node->FirstChild = tn;
				return SUCCESS;
			}
		} 
		else
		{
			return FAILURE;
		}


	}
}
void Tree::Delete(TreeNode *&node)
{
	
	if (node->FirstChild==NULL)
	{
		if (node==root)
		{
			delete node;
			root = NULL;
			return;
		}
		TreeNode *p = GetParent(node);
		TreeNode*q = p->FirstChild;
		TreeNode *t = node;
		if (q == node)
		{
			p->FirstChild = q->NextSibling;
			delete q;
			return;
		}
		while (q != node)
		{
			t = q;
			q = q->NextSibling;
		}
		t->NextSibling = q->NextSibling;
		delete q;
		return;
	}
	else
	{
		TreeNode*temp = node->FirstChild;
		TreeNode*q ;
		while (temp)
		{
			q = temp->NextSibling;
			delete temp;
			temp = q;
		}
		node->FirstChild = NULL;
		if (node==root)
		{
			delete node;
			root = NULL;
			return;
		}
		TreeNode *p = GetParent(node);
		q = p->FirstChild;
		TreeNode *t=node;
		if (q==node)
		{
			p->FirstChild = q->NextSibling;
			delete q;
			return;
		}
		while (q!=node)
		{
			t = q;
			q = q->NextSibling;
		}
		t->NextSibling = q->NextSibling;
		delete q;
	}
	return;
}
Status Tree::Init()
{
	ifstream fin("G:\\VS\\C++\\subject designing\\Genghis Khan.txt", ios::in);
	if (!fin)
		return FAILURE;
	else
	{
		string name1,name2=" ";
		int y1, y2;
		fin >> name1 >> y1 >> y2;
		DATA dr = { name1, y1, y2 };
		struct TreeNode *p = new struct TreeNode(dr);
		Insert(p,name2);
		
		while (!fin.eof())
		{
			fin >> name1 >> y1 >> y2 >> name2;
			DATA d = { name1, y1, y2 };
			struct TreeNode *p1 = new struct TreeNode(d);
			Insert(p1, name2);
		}

		fin.close();
		//NOrder(root);
		//cout << root->data.BirthYear << "\t" << root->data.DeathYear << endl;
		return SUCCESS;
	}
}
Status Tree::Saving()
{
	ofstream fout("G:\\VS\\C++\\subject designing\\test.txt", ios::out);
	if (!fout)
	{
		return FAILURE;
	}
	LinkQueue<TreeNode *>q;
	TreeNode *p = NULL;
	TreeNode *temp;
	if (root)
	{
		q.EnQueue(root);
	}
	while (!q.IsEmpty())
	{
		q.DelQueue(p);
		TreeNode *t = GetParent(p);
		fout << p->data.MemName << " " << p->data.BirthYear << " " << p->data.DeathYear;
		if (p != root)
		{
			fout << " " << t->data.MemName;
		}
		
			
		if (p->FirstChild)
		{
			q.EnQueue(p->FirstChild);
		}
		temp = p->FirstChild;
		while (temp&&temp->NextSibling)
		{
			q.EnQueue(temp->NextSibling);
			temp = temp->NextSibling;
		}

		if (!q.IsEmpty())
		{
			fout << endl;
		}
	}
	fout.close();
	return SUCCESS;
}
void Tree::Enquire_Generation(int gen)
{
	int i = 0;
	system("cls");
	CString str1("查询到的数据信息如下:\n");
	CString str2("姓名     生年     卒年\n");
	CString str = str1 + str2;
	CString temp1, temp2, tt, tem;
	LinkQueue<TreeNode *>q;
	TreeNode *p = NULL;
	TreeNode *temp;
	if (root)
	{
		q.EnQueue(root);
	}
	while (!q.IsEmpty())
	{
		q.DelQueue(p);
		TreeNode *t = GetParent(p);
		if (GetGeneration(p)==gen)
		{
			temp1.Format(L"%d", p->data.BirthYear);
			temp2.Format(L"%d", p->data.DeathYear);
			tt = p->data.MemName.c_str();
			tem = tt + L"     " + temp1 + L"     " + temp2;
			str = str + tem;
			i = 1;
			//cout << p->data.MemName << " " << p->data.BirthYear << " " << p->data.DeathYear << endl;
		}

		if (p->FirstChild)
		{
			q.EnQueue(p->FirstChild);
		}
		temp = p->FirstChild;
		while (temp&&temp->NextSibling)
		{
			q.EnQueue(temp->NextSibling);
			temp = temp->NextSibling;
		}
		if (!q.IsEmpty() && GetGeneration(p) == gen)
		{
			str = str + L"\n";
		}
		
	}
	if (i == 0)
	{
		::MessageBoxW(NULL, L"未查询到!", L"注意", MB_ICONERROR | MB_OK);
		return;
	}
	else
	{
		
		::MessageBoxW(NULL, str, L"查询结果", MB_ICONINFORMATION | MB_OK);
		return;
	}
}
void Tree::Menu()
{
	system("cls");//清屏
	/*
	
	打印系统初始欢迎界面

	*/
	cout << endl << endl << endl << endl;
	cout << "\t\t\t*********************************" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*         欢迎进入系统          *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*********************************" << endl;
	/*
	打印系统时间
	*/
	SYSTEMTIME sy;
	GetLocalTime(&sy);//获取系统时间
	cout << "\t\t\t  " << sy.wYear << "年" << sy.wMonth << "月" << sy.wDay << "日" << endl;//打印日期
	cout << "\t\t\t  " << sy.wHour << ":" << sy.wMinute << ":" << sy.wSecond<<endl;//打印时间
	Sleep(1000);//暂停1秒
	Choice();//调用系统主菜单
}
void Tree::Choice()
{
LOOP2:	system("cls");//清屏
	/*
	打印系统主菜单界面
	
	*/
	cout << endl << endl << endl << endl;
	cout << "\t\t\t*********************************" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*         ◎1.显示族谱          *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*         ◎2.添加成员          *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*         ◎3.修改成员          *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*         ◎4.删除成员          *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*         ◎5.查询信息          *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*         ◎6.退出系统          *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*         ◎7.恢复默认          *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*********************************" << endl;
	/*
	
	打印系统时间
	*/
	SYSTEMTIME sy;
	GetLocalTime(&sy);//获取系统时间
	cout << "\t\t\t  " << sy.wYear << "年" << sy.wMonth << "月" << sy.wDay << "日" << endl;//打印日期
	cout << "\t\t\t  " << sy.wHour << ":" << sy.wMinute << ":" << sy.wSecond << endl;//打印时间
	char c=0;
	//c = _getch();//检测键盘输入
	while (c<'1' || c>'7')//如果不是菜单中的正确选项
	{
		fflush(stdin);//清空输入缓冲区
		c = _getch();//重新从键盘获取读入的字符
		//goto LOOP2;
	}
	int selected = c - '0';//把字符转换为菜单中对应的数字
	switch (selected)//根据选择的菜单项目跳转到相应的子菜单函数执行相应的功能
	{
	case 1:
		Menu_1();
		break;
	case 2:
		Menu_2();
		break;
	case 3:
		Menu_3();
		break;
	case 4:
		Menu_4();
		break;
	case 5:
		Menu_5();
		break;
	case 6:
		Menu_6();
		break;
	case 7:
		Menu_7();
		break;
	default:
		break;
	}
	goto LOOP2;//主菜单函数不能退出，除非用户要求退出
}
void Tree::Menu_1()
{
	system("cls");//清屏
	if (root==NULL)//根结点不存在
	{
		::MessageBoxW(NULL, L"家谱中已不存在任何结点", L"提示", MB_ICONWARNING | MB_OK);
		return;
	}
//	NOrder(root);//依次打印每个结点
	//LOrder();
	Print(root);
	
	return;
	
	
}
void Tree::Menu_6()
{
	
		int n6 = ::MessageBoxW(NULL, L"是否保存", L"提示", MB_ICONQUESTION | MB_YESNOCANCEL);//提示是否保存
		if (n6 == IDYES)//用户按下保存键
		{
			Status s = Saving();//进行保存操作
			if (s == FAILURE)//保存失败，输出失败信息
				::MessageBoxW(NULL, L"保存失败", L"注意", MB_ICONERROR | MB_OK);
			else//保存成功，输出成功信息
				::MessageBoxW(NULL, L"已保存", L"提示", MB_ICONINFORMATION | MB_OK);
			exit(0);//退出系统
		}
		else if (n6==IDNO)//用户不保存
		{
			exit(1);//直接退出系统
		} 
		else//用户不想退出系统
		{
			return;//继续返回主菜单
		}
	
}
void Tree::Menu_7()
{
	int nRe7 = ::MessageBoxW(NULL, L"是否恢复默认值？", L"提示", MB_ICONQUESTION | MB_YESNO);//提示用户是否恢复默认值
	if (nRe7 == IDYES)//用户选择是
	{
		Delete(root);//清空整棵树
		Status s = Init();//重新从文件中载入并构建出整棵树
		if (s == FAILURE)//载入失败，显示错误信息并退出系统
		{
			::MessageBoxW(NULL, L"恢复默认失败", L"注意", MB_ICONERROR | MB_OK);
			exit(1);
		}
		else//载入成功，显示成功信息
		{
			::MessageBoxW(NULL, L"已恢复默认值", L"提示", MB_ICONINFORMATION | MB_OK);
			return;
		}
	}
	else
		return;//不恢复默认，直接返回主菜单
}
void Tree::Menu_2()
{
LOOP1:	system("cls");//清屏
	string name1, name2;//自己的名字，父亲的名字
	int year1, year2;//生年，卒年
	/*
	打印输出的界面

	*/
	cout << endl << endl << endl << endl;
	cout << "\t\t\t*********************************" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*         ╒●姓名：" ;
	cin >> name1;//输入要添加的人的姓名
	cout << "\t\t\t*         ║                    *" << endl;
	cout << "\t\t\t*         ║                    *" << endl;
	cout << "\t\t\t*         ║●生年：" ;
	cin >> year1;//输入要添加的人的生年
	cout << "\t\t\t*         ║                    *" << endl;
	cout << "\t\t\t*         ║                    *" << endl;
	cout << "\t\t\t*         ║●卒年：";
	cin >> year2; //输入要添加的人的卒年
	cout << "\t\t\t*         ║                    *" << endl;
	cout << "\t\t\t*         ║                    *" << endl;
	cout << "\t\t\t*         ╘●父亲姓名：" ;
	cin >> name2;//输入要添加的人的父亲姓名
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*********************************" << endl;
	/*
	拼接字符串，将string和int类型全部转化为CString类型
	把将要添加的人的信息以弹出框形式展现在屏幕上，要求用户确认是否添加
	*/
	CString str1("数据信息如下:\n");
	CString str2("姓名："), str3("生年："), str4("卒年："),str5("父亲姓名："),temp1,temp2,tt1,tt2;
	temp1.Format(L"%d", year1);
	temp2.Format(L"%d", year2);
	tt1 = name1.c_str();
	tt2 = name2.c_str();
	CString cnn("\n");
	CString str = str1 + str2 + tt1 + cnn + str3 + temp1 + cnn + str4 + temp2 + cnn + str5 + tt2;
	int re = ::MessageBoxW(NULL, str, L"是否确定添加？", MB_ICONQUESTION | MB_YESNO);//弹出框，询问用户是否确认添加
	if (re==IDYES)//选择是，进行添加
	{
		DATA d = { name1, year1, year2 };//结构体变量
		struct TreeNode *p = new struct TreeNode(d);//新建结点
		Insert(p, name2);//插入到树中
	}
	re = ::MessageBoxW(NULL, L"是否继续添加？", L"提示", MB_ICONQUESTION | MB_YESNO);//询问用户是否继续添加
	if (re==IDYES)//选择是，跳转到本函数头继续执行
	{
		goto LOOP1;
	}
	return;
}
void Tree::Menu_3()
{
LOOP1:	system("cls");//清屏
	cout << endl << endl << endl << endl;
	cout << "\t\t\t*********************************" << endl;
	cout << "\t\t\t*    ●需要修改的成员姓名：";//
	string name;
	cin >> name;//输入需要修改的人的姓名
	TreeNode*node_modify;//需要修改的结点
	Status s = GetNode(name, node_modify, root);//根据姓名寻找这个结点
	if (s==FAILURE)//没找到，显示错误
	{
		CString str("未找到!"),temp;
		temp = name.c_str();
		str =temp +str ;
		::MessageBoxW(NULL, str, L"注意", MB_ICONERROR | MB_OK);
		int nRe = ::MessageBoxW(NULL, L"是否继续修改", L"提示", MB_ICONQUESTION | MB_YESNO);//弹出框询问是否继续修改
		if (nRe==IDYES)//选择是，从本函数头继续执行
		{
			goto LOOP1;
		}
		return;
	} 
	else//找到
	{
		/*
		打印子菜单，让用户选择
		
		*/
		system("cls");
		cout << endl << endl << endl << endl;
		cout << "\t\t\t*********************************" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*         ◎1.修改姓名          *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*         ◎2.修改生年          *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*         ◎3.修改卒年          *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*                               *" << endl;
		cout << "\t\t\t*********************************" << endl;
		char c = 0;
		while (c<'1'||c>'3')//当输入不正确时，继续输入
		{
			fflush(stdin);
			c = _getch();
		}
		int select = c - '0';//把输入的字符转化为菜单中对应的数字
		switch (select)//根据选择的项目跳转到子菜单函数中继续执行
		{
		case 1:
			Menu_3_1(node_modify);
			break;
		case 2:
			Menu_3_2(node_modify);
			break;
		case 3:
			Menu_3_3(node_modify);
			break;
		default:
			break;
		}
	}
	return;
}
void Tree::Menu_4()
{
LOOP:	system("cls");//清屏
	cout << endl << endl << endl << endl;
	cout << "\t\t\t*********************************" << endl;
	cout << "\t\t\t*    ●需要删除的成员姓名：";
	string name;
	cin >> name;//输入需要删除的成员姓名
	TreeNode*node_delete;//待删除的结点
	Status s = GetNode(name, node_delete, root);//寻找这个结点
	if (s == FAILURE)//没找到，弹出错误信息
	{
		CString str("未找到!"), temp;
		temp = name.c_str();
		str = temp + str;
		::MessageBoxW(NULL, str, L"注意", MB_ICONERROR | MB_OK);
		int nRe = ::MessageBoxW(NULL, L"是否继续删除？", L"提示", MB_ICONQUESTION | MB_YESNO);//提示是否继续删除
		if (nRe==IDYES)//选择是，跳转到本函数开头继续执行
		{
			goto LOOP;
		}
		return;
	}
	else//找到了结点
	{
		/*
		
		字符串拼接，弹出框显示即将删除的节点信息
		
		让用户选择是否确认删除
		*/
		CString s1("即将删除的结点：\n"), s2("  姓名："), s3("  生年："), s4("  卒年：");
		CString tem,tt1,tt2,s;
		tem = name.c_str();
		tt1.Format(L"%d", node_delete->data.BirthYear);
		tt2.Format(L"%d", node_delete->data.DeathYear);
		s = s1 + s2 + tem + L"\n" + s3 + tt1 + L"\n" + s4 + tt2 ;
		int n=::MessageBoxW(NULL, s, L"是否确认删除？", MB_ICONQUESTION | MB_YESNO);//让用户选择是否确认删除
		if (n==IDNO)//选择否，不删除
		{
			int nRe = ::MessageBoxW(NULL, L"是否继续删除？", L"提示", MB_ICONQUESTION | MB_YESNO);//提示是否继续删除
			if (nRe == IDYES)
			{
				goto LOOP;
			}
		}

		else//选择是，删除结点
		{
			Delete(node_delete);//进行删除操作

			/*
			字符串拼接，提示用户结点已删除
			
			*/
			CString str("已删除!"), temp;
			temp = name.c_str();
			str = temp + str;
			::MessageBoxW(NULL, str, L"提示", MB_ICONINFORMATION | MB_OK);
			int nR = ::MessageBoxW(NULL, L"是否继续删除？", L"提示", MB_ICONQUESTION | MB_YESNO);//让用户选择是否继续删除
			if (nR == IDYES)//用户选择是
			{
				goto LOOP;//转到本函数头继续执行
			}

		}
		
	}

	return;
}
void Tree::Menu_5()
{
	system("cls");//清屏
	/*
	打印菜单，让用户选择查询方式
	
	*/
	cout << endl << endl << endl << endl;
	cout << "\t\t\t*********************************" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*         ◎1.按姓名查询        *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*         ◎2.按代查询          *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*         ◎3.查询孩子          *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*         ◎4.查询父亲          *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*         ◎5.查询兄弟          *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*                               *" << endl;
	cout << "\t\t\t*********************************" << endl;
	char c = 0;
	while (c<'1' || c>'5')//如果输入不合法
	{
		
		fflush(stdin);//清空输入缓冲区
		c = _getch();//读取键盘输入内容

		
	}
	int selected = c - '0';//把输入的字符转化为数字
	switch (selected)//根据选择的菜单跳转到相应的子菜单
	{
	case 1:
		Menu_5_1();
		break;
	case 2:
		Menu_5_2();
		break;
	case 3:
		Menu_5_3();
		break;
	case 4:
		Menu_5_4();
		break;
	case 5:
		Menu_5_5();
		break;
	
	default:
		break;
	}
}
void Tree::Menu_3_1(TreeNode*&node)
{
	system("cls");//清屏
	cout << endl << endl << endl << endl;
	cout << "\t\t\t*********************************" << endl;
	cout << "\t\t\t*    ●修改后的姓名：";
	string name;
	cin >> name;//输入修改后的姓名
	/*
	字符串拼接
	
	弹出框对比修改前后的数据差异

	提示用户是否确认修改
	
	*/


	CString str1("        修改之前的数据：   修改之后的数据：\n"), str2("姓名："), str3("生年："), str4("卒年："), temp1, temp2,tt1,tt2;
	temp1 = node->data.MemName.c_str();
	temp2 = name.c_str();
	tt1.Format(L"%d", node->data.BirthYear);
	tt2.Format(L"%d", node->data.DeathYear);
	str2 = str2 + temp1 + L"                     " + temp2 + L"\n";
	str3 = str3 + tt1 + L"                    " + tt1 + L"\n";
	str4 = str4 + tt2 + L"                    " + tt2 + L"\n";
	int nRe=::MessageBoxW(NULL, str1 + str2 + str3 + str4, L"是否确定修改？", MB_ICONQUESTION | MB_YESNO);//弹出框提示用户选择是否确认修改
	if (nRe == IDYES)//如果用户确认了修改
	{
		node->data.MemName = name;//进行修改
		int re = ::MessageBoxW(NULL, L"是否继续修改?", L"提示", MB_ICONQUESTION | MB_YESNO);//弹出框询问是否继续修改
		if (re == IDYES)//选择是，继续修改
		{
			Menu_3();//调用修改函数
			return;//返回
		}
		else
			return;//不继续修改，直接返回
	}
	else//用户未确认修改，返回
		return;
}
void Tree::Menu_3_2(TreeNode*&node)
{
	system("cls");//清屏
	cout << endl << endl << endl << endl;
	cout << "\t\t\t*********************************" << endl;
	cout << "\t\t\t*    ●修改后的生年：";
	int year;
	cin >> year;//输入修改后的生年
	/*
	
	字符串拼接
	

	弹出框对比修改前后的数据


	提示用户选择是否确认修改
	
	*/



	CString str1("        修改之前的数据：   修改之后的数据：\n"), str2("姓名："), str3("生年："), str4("卒年："), temp1, temp2, tt1, tt2;
	tt1 = node->data.MemName.c_str();
	temp1.Format(L"%d", node->data.BirthYear);
	temp2.Format(L"%d", year);
	tt2.Format(L"%d", node->data.DeathYear);
	str2 = str2 + tt1 + L"                   " + tt1 + L"\n";
	str3 = str3 + temp1 + L"                    " + temp2 + L"\n";
	str4 = str4 + tt2 + L"                    " + tt2 + L"\n";
	int nRe = ::MessageBoxW(NULL, str1 + str2 + str3 + str4, L"是否确定修改？", MB_ICONQUESTION | MB_YESNO);//弹出框提示用户是否确认修改
	if (nRe == IDYES)//用户选择是，修改数据
	{
		if (node==root)
		{
			node->data.BirthYear = year;
			int Re = ::MessageBoxW(NULL, L"是否继续修改?", L"提示", MB_ICONQUESTION | MB_YESNO);//提示是否继续修改
			if (Re == IDYES)//选择是，继续修改
			{
				Menu_3();
				return;//返回
			}
			else
				return;
		}
		else
		{
			TreeNode *pa=GetParent(node);//获取结点的父亲结点
			TreeNode *p = pa->FirstChild; 
			TreeNode *q=NULL;
			if (p == node)//如果要修改的结点是第一个孩子结点，先将父亲结点的孩子指针指向它的后一个结点
			{
				pa->FirstChild = node->NextSibling;
				p = NULL;//防止进入下一个循环引发异常
			}
			while (p)
			{

				if (p&&p->NextSibling == node)//如果所修改的结点为下一个结点
				{
					p->NextSibling = node->NextSibling;//将此结点的兄弟指针指向修改的结点的下一个兄弟
					//即把将要修改的结点隔离开来
					break;
				} 
				p = p->NextSibling;
			}
			//p = pa->FirstChild;
			
			p = pa->FirstChild;//重新赋值p
			if (year<=p->data.BirthYear)//如果要插入的位置是第一个位置
			{
				pa->FirstChild = node;
				node->NextSibling = p;//插入
				goto LOOP;//跳过下面的步骤，直接进行修改操作
			}
			while (p)
			{
				q = p;//q保存当前结点的前一个结点
				p = p->NextSibling;//p保存当前结点
				
				if (p&&q->data.BirthYear <= year&&p->data.BirthYear >= year)//如果修改后的数据在两个之间，这就是应该插入的位置
				{
					q->NextSibling = node;
					node->NextSibling = p;//插入
					break;//插入完毕，跳出循环
				}
			}
			if (!p&&q->data.BirthYear<=year)//如果要插入的位置为最后一个位置
			{
				q->NextSibling = node;
				node->NextSibling = NULL;//插入
			}
		LOOP:node->data.BirthYear = year;//修改数据
			int re = ::MessageBoxW(NULL, L"是否继续修改?", L"提示", MB_ICONQUESTION | MB_YESNO);//提示是否继续修改
			if (re == IDYES)//选择是，继续修改
			{
				Menu_3();
				return;//返回
			}
			else
				return;

		}
		
	}
	else
		return;
}
void Tree::Menu_3_3(TreeNode*&node)
{
	system("cls");//清屏
	cout << endl << endl << endl << endl;
	cout << "\t\t\t*********************************" << endl;
	cout << "\t\t\t*    ●修改后的卒年：";
	int year;
	cin >> year;//输入修改后的卒年
	/*
	
	字符串拼接

	弹出框对比修改前后的数据差异
	
	提示用户选择是否确认修改
	
	*/



	CString str1("        修改之前的数据：   修改之后的数据：\n"), str2("姓名："), str3("生年："), str4("卒年："), temp1, temp2, tt1, tt2;
	tt1 = node->data.MemName.c_str();
	temp1.Format(L"%d", node->data.DeathYear);
	temp2.Format(L"%d", year);
	tt2.Format(L"%d", node->data.BirthYear);
	str2 = str2 + tt1 + L"                   " + tt1 + L"\n";
	str3 = str3 + tt2 + L"                    " + tt2 + L"\n";
	str4 = str4 + temp1 + L"                    " + temp2 + L"\n";
	int nRe = ::MessageBoxW(NULL, str1 + str2 + str3 + str4, L"是否确定修改？", MB_ICONQUESTION | MB_YESNO);//弹出框让用户选择是否确认修改
	if (nRe == IDYES)//确认修改
	{
		node->data.BirthYear = year;//修改数据
		int re = ::MessageBoxW(NULL, L"是否继续修改?", L"提示", MB_ICONQUESTION | MB_YESNO);//提示用户是否继续修改
		if (re == IDYES)//继续修改，调用修改的函数
		{
			Menu_3();
			return;//返回
		}
		else
			return;
	}
	else
		return;
}
void Tree::Menu_5_1()
{
LOOP3:	system("cls");//清屏
	cout << endl << endl << endl << endl;
	cout << "\t\t\t*********************************" << endl;  
	cout << "\t\t\t*    ●需要查询的姓名：";
	string name;
	cin >> name;//输入需要查询的姓名
	TreeNode *temp = NULL;
	Status s = GetNode(name, temp, root);//根据姓名查找结点
	/*
	字符串拼接

	如果没有查询成功，弹出框显示错误

	如果查询成功，弹出框显示查询出来的结果
	
	
	*/
	CString st;
	st = name.c_str();
	if (s==FAILURE)//查询失败
	{	
		st = st + L"未查询到！";
		::MessageBoxW(NULL, st, L"注意", MB_ICONERROR | MB_OK);//提示错误信息
		
	}
	else//查询成功
	{
		CString str1("查询到的数据信息如下:\n");
		CString str2("姓名："), str3("生年："), str4("卒年："), temp1, temp2, tt1;
		temp1.Format(L"%d", temp->data.BirthYear);
		temp2.Format(L"%d", temp->data.DeathYear);
		tt1 = name.c_str();
		CString cnn("\n");
		CString str = str1 + str2 + tt1 + cnn + str3 + temp1 + cnn + str4 + temp2 + cnn ;
		::MessageBoxW(NULL, str, L"提示", MB_ICONINFORMATION | MB_OK);//显示查询的结果
	}
	//询问是否继续查询
	int nRet = ::MessageBoxW(NULL, L"是否继续查询?", L"提示", MB_ICONQUESTION | MB_YESNO);
	if (nRet == IDYES)//继续查询
	{
		goto LOOP3;//跳转到函数头部继续执行
	}
}
void Tree::Menu_5_2()
{
LOOP4:	system("cls");//清屏
	cout << endl << endl << endl << endl;
	cout << "\t\t\t*********************************" << endl;
	cout << "\t\t\t*    ●需要查询的代：";
	int generation;
	cin >> generation;//输入代的号码
	Enquire_Generation(generation);//根据第几代查询成员信息
	int nRet = ::MessageBoxW(NULL, L"是否继续查询?", L"提示", MB_ICONQUESTION | MB_YESNO);//弹出框提示是否继续查询
	if (nRet==IDYES)//继续查询
	{
		goto LOOP4;//跳转到函数头继续执行
	}
}
void Tree::Menu_5_3()
{
LOOP3:	system("cls");//清屏
	cout << endl << endl << endl << endl;
	cout << "\t\t\t*********************************" << endl;
	cout << "\t\t\t*    ●需要查询的成员姓名：";
	string name;
	cin >> name;//输入要查询的姓名
	TreeNode *temp = NULL;
	Status s = GetNode(name, temp, root);//根据姓名找到这个结点

	/*
	字符串拼接


	如果查找失败弹出失败信息


	如果查找成功弹出查找结果
	
	
	*/
	CString st;
	st = name.c_str();
	if (s == FAILURE)
	{
		st = st + L"未查询到！";
		::MessageBoxW(NULL, st, L"注意", MB_ICONERROR | MB_OK);//出错信息

	}
	else
	{
		TreeNode *p = temp->FirstChild;
		CString str1("查询到的孩子信息如下:\n");
		CString str2("姓名     生年     卒年\n");
		CString str = str1 + str2;
		CString temp1, temp2, tt, tem;
		st = name.c_str();
		st = st + L"没有孩子!";
		if (!p)
		{
			::MessageBoxW(NULL,st , L"注意", MB_ICONERROR | MB_OK);//没有孩子，出错

		}
		else
		{
			while (p)
			{
				temp1.Format(L"%d", p->data.BirthYear);
				temp2.Format(L"%d", p->data.DeathYear);
				tt = p->data.MemName.c_str();
				tem = tt + L"     " + temp1 + L"     " + temp2;
				str = str + tem;
				//cout << p->data.MemName << " " << p->data.BirthYear << " " << p->data.DeathYear << endl;

				p = p->NextSibling;
				if (p)
				{
					str = str + L"\n";
				}
			}
			::MessageBoxW(NULL, str, L"查询结果", MB_ICONINFORMATION | MB_OK);//弹出框显示查询结果
		}
	}
	int nRet = ::MessageBoxW(NULL, L"是否继续查询?", L"提示", MB_ICONQUESTION | MB_YESNO);//提示是否继续查询
	if (nRet == IDYES)//继续查询，从本函数开头继续执行
	{
		goto LOOP3;
	}
}
void Tree::Menu_5_4()
{
LOOP5:	system("cls");//清屏
	cout << endl << endl << endl << endl;
	cout << "\t\t\t*********************************" << endl;
	cout << "\t\t\t*    ●需要查询的成员姓名：";
	string name;
	cin >> name;//查询的成员姓名
	TreeNode *temp = NULL;
	Status s = GetNode(name, temp, root);//根据姓名查找结点
	TreeNode *q=GetParent(temp);//找到这个结点的父亲

	/*
	字符串拼接

	如果查询成功，弹出框显示查询出的结果

	如果查询失败，弹出框显示出错信息

	*/


	if (q)
	{
		
		CString str1("查询到的父亲信息如下:\n");
		CString str2("姓名："), str3("生年："), str4("卒年："), temp1, temp2, tt1;
		temp1.Format(L"%d", q->data.BirthYear);
		temp2.Format(L"%d", q->data.DeathYear);
		tt1 = q->data.MemName.c_str();
		CString cnn("\n");
		CString str = str1 + str2 + tt1 + cnn + str3 + temp1 + cnn + str4 + temp2 + cnn;
		::MessageBoxW(NULL, str, L"提示", MB_ICONINFORMATION | MB_OK);//弹出框显示查询结果



		//cout << q->data.MemName << " " << q->data.BirthYear << " " << q->data.DeathYear << endl;
	
	}
	else
	{
		CString st;
		st = name.c_str();
		st = st + L"没有父亲!";
		
		
		::MessageBoxW(NULL, st, L"注意", MB_ICONERROR | MB_OK);//没有父亲，显示错误

		
	}
	int nRet = ::MessageBoxW(NULL, L"是否继续查询?", L"提示", MB_ICONQUESTION | MB_YESNO);//提示是否继续查询
	if (nRet == IDYES)//继续查询，从本函数头开始继续运行
	{
		goto LOOP5;
	}
}
void Tree::Menu_5_5()
{
LOOP1:	system("cls");//清屏
	cout << endl << endl << endl << endl;
	cout << "\t\t\t*********************************" << endl;
	cout << "\t\t\t*    ●需要查询的成员姓名：";
	string name;
	cin >> name;//输入需要查询的成员姓名
	int i = 0;
	TreeNode *temp = NULL;
	Status s = GetNode(name, temp, root);//查找这个结点

	/*
	
	字符串拼接


	查找成功，弹出框显示查找结果
	
	
	查找失败，弹出框显示出错信息

	
	*/


	CString st;
	st = name.c_str();
	if (s == FAILURE)
	{
		st = st + L"未查询到！";
		::MessageBoxW(NULL, st, L"注意", MB_ICONERROR | MB_OK);//没查找到这个结点，出错

	}
	else
	{
		TreeNode *q = GetParent(temp);//查找父亲结点
		CString str1("查询到的兄弟信息如下:\n");
		CString str2("姓名     生年     卒年\n");
		CString str = str1 + str2;
		CString temp1, temp2, tt, tem;
		if (q)
		{//查找成功
			q = q->FirstChild;
			while (q)//依次遍历它的孩子结点
			{
				if (q->data.MemName != name)//跳过所查找的结点，寻找它的兄弟节点
				{
					temp1.Format(L"%d", q->data.BirthYear);
					temp2.Format(L"%d", q->data.DeathYear);
					tt = q->data.MemName.c_str();
					tem = tt + L"     " + temp1 + L"     " + temp2;
					str = str + tem;
					//cout << q->data.MemName << " " << q->data.BirthYear << " " << q->data.DeathYear << endl;

				}
				q = q->NextSibling;
				if (q&&q->data.MemName == name)
				{
					q = q->NextSibling;
				}
				if (q)
				{
					str = str + L"\n";
				}
			}
			::MessageBoxW(NULL, str, L"查询结果", MB_ICONINFORMATION | MB_OK);//显示查询结果
	
		}
		else
		{
			::MessageBoxW(NULL, L"查询失败！", L"注意", MB_ICONERROR | MB_OK);//没有父亲结点，出错
		}
	}
	int nRet = ::MessageBoxW(NULL, L"是否继续查询?", L"提示", MB_ICONQUESTION | MB_YESNO);//弹出框询问是否继续查询
	if (nRet == IDYES)//继续查询，跳转到本函数开头继续执行
	{
		goto LOOP1;
	}

}
































































#endif



