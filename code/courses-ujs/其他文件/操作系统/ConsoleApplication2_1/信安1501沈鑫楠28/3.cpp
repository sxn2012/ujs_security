#include <iostream>
#include <string>
#include <stdlib.h>
#include <time.h>
#include <vector>
#define Length 128
#define Unrefered -1
using namespace std;
struct page
{
	int num;//页号
	int sign;//标志
	int block;//主存块号
	int modifysign;//修改标志
	int place;//在磁盘上的位置
};
struct order
{
	string operate;//操作
	int page_num;//页号
	int unit_num;//单元号
};
struct page assignment[7] = { { 0, 1, 5, 0, 11 }, { 1, 1, 8, 0, 12 }, { 2, 1, 9, 0, 13 }, { 3, 1, 1, 0, 21 }, { 4, 0, Unrefered, 0, 22 }, { 5, 0, Unrefered, 0, 23 }, { 6, 0, Unrefered, 0, 121 } };//定义初始页表
bool exist(int num, int n)//判断主存块号是否已存在
{
	for (int i = 0; i < 7; i++)
	{
		if (i == n)
		{

			continue;
		}
		if (assignment[i].block == num)
		{
			return true;
		}
	}
	return false;
}
int main()
{
	vector<int> vec;//定义向量表示空闲块
	vec.push_back(3);
	vec.push_back(2);
	vec.push_back(1);
	vec.push_back(0);
	//初始化
	
	vector<int>::iterator it;
	printf("初始页面:");
	for (it = vec.begin(); it != vec.end(); ++it)//遍历输出初始页面
	{
		printf("%d\t", *it);
	}
	printf("\n");
	fflush(stdin);
	fflush(stdout);
	struct order sequence[12];
	printf("请输入作业的指令序列:\n");//输入要执行的指令序列
	printf("操作 页号 单元号\n");
	for (int i = 0; i < 12; i++)
	{
		cin >> sequence[i].operate >> sequence[i].page_num >> sequence[i].unit_num;
	}

	for (int i = 0; i < 12; i++)
	{
		int L = sequence[i].page_num;//要访问的页面
		
		
		if (assignment[L].sign == 1)//在主存中
		{
			if (sequence[i].operate == "存" || sequence[i].operate == "cun")//被修改
			{
				assignment[L].modifysign = 1;//修改标志置为1
			}
			int addr = assignment[L].block*Length + sequence[i].unit_num;
			
			int j = 0;
			for (it = vec.begin(); it != vec.end(); ++it, ++j)
			{
				if (*it == L)//改变这一页在数组中的位置
				{
					vec.erase(it);
					vec.insert(vec.begin(), L);
					break;
				}
			}
		}
		else//缺页
		{
			int J = vec.back();//数组中最久未被访问的页
			if (assignment[J].modifysign==1)//被修改过
			{
				cout << "OUT " << J << endl;//调出
			}
			cout << "IN " << L << endl;//调入新的页
			vec.pop_back();
			vec.insert(vec.begin(),L);//插入到数组中
			//旧的页被调出，结构体中的内容改变
			assignment[J].sign = 0;
			assignment[J].block = Unrefered;
			//新的页被调入，结构体中相关内容改变
			assignment[L].sign = 1;
			assignment[L].block = 0;
			do 
			{
				assignment[L].block++;
			} while (exist(assignment[L].block,L));
		}
		//输出执行这条指令后主存中页面的状态
		cout << "当前状态：";
		for (it = vec.begin(); it != vec.end(); ++it)
		{
			printf("%d\t", *it);
		}
		cout << endl;
	}
	return 0;
}


