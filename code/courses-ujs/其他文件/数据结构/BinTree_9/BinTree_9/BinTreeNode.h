#pragma once
#ifndef NODE_HEADER_TREE
#define NODE_HEADER_TREE


#include "Status.h"
template<class ElemType>
struct BinTreeNode
{
//protected:
	//数据成员
	ElemType data;//数据域
	//指针域：
	BinTreeNode<ElemType> *leftChild;//左孩子指针
	BinTreeNode<ElemType> *rightChild;//右孩子指针

//public:
	//函数成员

	BinTreeNode();//无参构造函数
	BinTreeNode(const ElemType &d, BinTreeNode<ElemType> *lChild = NULL, BinTreeNode<ElemType> *rChild = NULL);//有参构造函数
	virtual ~BinTreeNode();//析构函数

};

template<class ElemType>
BinTreeNode<ElemType>::BinTreeNode()
{
	leftChild = rightChild = NULL;
}
template<class ElemType>
BinTreeNode<ElemType>::BinTreeNode(const ElemType &d, BinTreeNode<ElemType> *lChild /* = NULL */, BinTreeNode<ElemType> *rChild /* = NULL */)
{
	data = d;
	leftChild = lChild;
	rightChild = rChild;
}
template<class ElemType>
BinTreeNode<ElemType>::~BinTreeNode()
{

}

#endif // !NODE_HEADER_TREE