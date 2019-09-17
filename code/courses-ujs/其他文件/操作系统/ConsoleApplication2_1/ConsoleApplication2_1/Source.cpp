#include <iostream>
#include <string>
#include <stdlib.h>
#include <time.h>
#define Length 128
#define Unrefered -1
using namespace std;
struct page
{
	int num;//页号
	int sign;//标志
	int block;//主存块号
	int place;//在磁盘上的位置
};
struct order
{
	string operate;//操作
	int page_num;//页号
	int unit_num;//单元号
};
struct page assignment[7] = { { 0, 1, 5, 11 }, { 1, 1, 8, 12 }, { 2, 1, 9, 13 }, { 3, 1, 1, 21 }, { 4, 0, Unrefered, 22 }, { 5, 0, Unrefered, 23 }, { 6, 0, Unrefered, 121 } };//定义初始作业的页表
bool exist(int num,int n)//判断主存块号是否已存在
{
	for (int i = 0; i < 7;i++)
	{
		if (i==n)
		{

			continue;
		}
		if (assignment[i].block==num)
		{
			return true;
		}
	}
	return false;
}
int main()
{
	//打印初始页表
	printf("初始页表：\n");
	for (int i = 0; i < 7;i++)
	{
		printf("%d %d %d %d\n", assignment[i].num, assignment[i].sign, assignment[i].block, assignment[i].place);
	}
	struct order sequence[12];
	//输入需要执行的指令序列
	printf("请输入作业的指令序列:\n");
	printf("操作 页号 单元号\n");
	for (int i = 0; i < 12;i++)
	{
		cin >> sequence[i].operate >> sequence[i].page_num >> sequence[i].unit_num;
	}
	//输出每条指令执行时访问的地址
	printf("操作 页号 地址\n");
	for (int i = 0; i < 12;i++)
	{
		if (assignment[sequence[i].page_num].sign==1)//标志为1，在内存中
		{
			int addr = assignment[sequence[i].page_num].block*Length + sequence[i].unit_num;//计算出物理地址
			
			cout << sequence[i].operate << " " << sequence[i].page_num << " " << addr << endl;//输出
		}
		else//标志为0，缺页中断
		{
			cout<<sequence[i].operate<<" "<<sequence[i].page_num<<"*"<<endl;//输出
			//中断处理，把页面调入主存
			assignment[sequence[i].page_num].sign = 1;
			assignment[sequence[i].page_num].block = 0;
			do 
			{	
				assignment[sequence[i].page_num].block++;
			} while (exist(assignment[sequence[i].page_num].block, sequence[i].page_num));//分配主存块号
		}
	}
	return 0;
}


