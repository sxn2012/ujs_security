#include <iostream>
using namespace std;
#include "status.h"
#include "Node.h"
#include "LinkQueue.h"
#include "BinTreeNode.h"
#include "BinaryTree.h"

template <class Elemtype>
void Display(const Elemtype e)
{
	cout << e << " ";
}

void main(void)
{
	BinaryTree<char> tree;
	BinTreeNode<char> *r = tree.GetRoot();
	cout << "请输入二叉树的先序序列，#表示空指针（ABDG#L##HM###EIN##O###C#FJ##K##）：" << endl;
	tree.CreateBtrPre(r);//输入：ABDG#L##HM###EIN##O###C#FJ##K##
	tree.changeroot(r);
	cout << "二叉树的先序序列为：";
	tree.PreOrder(Display);
	cout << endl;
	cout << "二叉树的中序序列为：";
	tree.InOrder(Display);
	cout << endl;
	cout << "二叉树的后序序列为：";
	tree.PostOrder(Display);
	cout << endl;
	cout << "二叉树的层次序列为：";
	tree.LevelOrder(Display);
	cout << endl;
	//---------------------以下测试第9题函数--------------------------------------------
	cout << "二叉树的最大宽度为：" << tree.Width() << endl;//--------调用第9题函数
	//---------------------以下测试第14题函数----------------------------------------------------------------
	cout << "二叉树的叶子节点数目为：" << tree.CountLeaf(tree.GetRoot()) << endl;//----------调用第14题函数

	system("pause");
}