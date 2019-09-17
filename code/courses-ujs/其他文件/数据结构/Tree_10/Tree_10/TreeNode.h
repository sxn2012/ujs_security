#pragma once
#ifndef TREE_NODE_H
#define TREE_NODE_H
#include "status.h"
template<class ElemType>
struct TreeNode
{
//protected:
	ElemType data;
	TreeNode<ElemType> *FirstChild;//º¢×Ó
	TreeNode<ElemType> *NextSibling;//ÐÖµÜ
//public:
	TreeNode();
	TreeNode(const ElemType &d, TreeNode<ElemType>*child = NULL, TreeNode<ElemType>*sibling = NULL);

};
template<class ElemType>
TreeNode<ElemType>::TreeNode()
{
	FirstChild = NULL;
	NextSibling = NULL;
}
template<class ElemType>
TreeNode<ElemType>::TreeNode(const ElemType &d, TreeNode<ElemType>*child /* = NULL */, TreeNode<ElemType>*sibling /* = NULL */)
{
	data = d;
	FirstChild = child;
	NextSibling = sibling;
}






#endif // !TREE_NODE_H
