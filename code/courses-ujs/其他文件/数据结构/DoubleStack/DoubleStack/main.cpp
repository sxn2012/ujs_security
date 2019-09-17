#include <iostream>
using namespace std;
#include "status.h"
#include "SeqStack.h"

//用于元素的输出
template <class ElemType>
void Display(const ElemType & e)
{
	cout << e << "  ";
}

void main(void)
{
	SeqStack<int> stack(10);
	int data;


	cout << "请输入5个整数（11 12 13 14 15）：";
	for (int i = 1; i <= 5; i++)
	{
		cin >> data;
		if (stack.Push(1, data) == OVER_FLOW)
		{
			cout << "栈已满！" << endl;	exit(0);
		}
	}
	cout << "请输入4个整数（21 22 23 24）：";
	for (int i = 1; i <= 4; i++)
	{
		cin >> data;
		if (stack.Push(2, data) == OVER_FLOW)
		{
			cout << "栈已满！" << endl;	exit(0);
		}
	}

	cout << "栈1中有 " << stack.GetLength(1) << " 个元素：";
	stack.Traverse(1, Display);
	cout << endl;
	cout << "栈2中有 " << stack.GetLength(2) << " 个元素：";
	stack.Traverse(2, Display);
	cout << endl;

	cout << "栈1中元素依次出栈：";
	while (stack.GetLength(1))
	{
		stack.Pop(1, data);
		cout << data << "  ";
	}
	cout << endl;
	cout << "栈2中元素依次出栈：";
	while (stack.GetLength(2))
	{
		stack.Pop(2, data);
		cout << data << "  ";
	}
	cout << endl;

	if (stack.IsEmpty())
		cout << "当前栈为空！" << endl;

	system("pause");
}