#include <iostream>
using namespace std;
#include "status.h"
#include "Node.h"
#include "LinkQueue.h"
#include "TreeNode.h"
#include "Tree.h"

template <class Elemtype>
void Display(const Elemtype e)
{
	cout << e << " ";
}

void main(void)
{
	Tree<char> tree;
	cout << "请输入树的先根序列，#表示空指针（ABE#FK#L###CG##DH#IM#N##J####）：" << endl;
	TreeNode<char> *r;
	tree.CreateTree(r);//输入：ABE#FK#L###CG##DH#IM#N##J####
	tree.changeroot(r);
	cout << "树的先根序列为：";
	tree.PreOrder(Display);
	cout << endl;
	cout << "树的后根序列为：";
	tree.PostOrder(Display);
	cout << endl;
	cout << "树的层次序列为：";
	tree.LevelOrder(Display);
	cout << endl;
	//---------------------以下测试第10题函数--------------------------------------------
	//cout << "树的深度为：" << tree.Height() << endl;//--------调用第10题函数
	//---------------------以下测试第15题函数----------------------------------------------------------------
	//cout << "树的度为：" << tree.Degree() << endl;//----------调用第15题函数

	system("pause");
}