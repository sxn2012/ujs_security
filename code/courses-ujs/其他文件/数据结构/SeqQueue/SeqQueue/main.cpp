#include <iostream>
using namespace std;
#include "status.h"
#include "SeqQueue.h"

void main(void)
{
	SeqQueue<int> queue(5);
	int data;


	cout << "请输入5个整数（11 12 13 14 15）：";
	for (int i = 1; i <= 5; i++)
	{
		cin >> data;
		if (queue.EnQueue(data) == OVER_FLOW)
			cout << "队列已满！" << endl;
	}
	cout << "出队两个整数：";
	for (int i = 1; i <= 2; i++)
	{
		if (queue.DelQueue(data) != UNDER_FLOW)
			cout << data << "  ";
		else
			cout << endl << "队列为空！" << endl;
	}
	cout << endl;
	cout << "请输入4个整数（21 22 23 24）：";
	for (int i = 1; i <= 4; i++)
	{
		cin >> data;
		if (queue.EnQueue(data) == OVER_FLOW)
			cout << "队列已满！" << endl;
	}
	cout << "全部整数出队：";
	while (!queue.IsEmpty())
	{
		queue.DelQueue(data);
		cout << data << "  ";
	}
	cout << endl;

	system("pause");
}