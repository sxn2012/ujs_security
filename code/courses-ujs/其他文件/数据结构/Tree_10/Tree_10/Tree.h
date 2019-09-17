#pragma once
#ifndef TREE_HEADER
#define TREE_HEADER
#include "TreeNode.h"
#include "LinkQueue.h"
template<class ElemType>
class Tree
{
protected:
	TreeNode<ElemType> *root;
	void PreOrder(TreeNode<ElemType>*r, void(*Visit)(const ElemType &))const;
	void PostOrder(TreeNode<ElemType>*r, void(*Visit)(const ElemType &))const;
public:
	Tree();
	Tree(const ElemType &e);
	virtual ~Tree();
	void CreateTree(TreeNode<ElemType>*&r);
	void PreOrder(void(*Visit)(const ElemType &))const;
	void PostOrder(void(*Visit)(const ElemType &))const;
	void LevelOrder(void(*Visit)(const ElemType &))const;


	void changeroot(TreeNode<ElemType> *r) { root = r; }



	int Height(TreeNode<ElemType> *r=root)const
	{
		
	}
	int Degree()const
	{

	}
};
template<class ElemType>
Tree<ElemType>::Tree()
{
	root = new TreeNode<ElemType>();
}
template<class ElemType>
Tree<ElemType>::Tree(const ElemType &e)
{
	root = new TreeNode<ElemType>(e);
}
template<class ElemType>
Tree<ElemType>::~Tree()
{
}
template<class ElemType>
void Tree<ElemType>::CreateTree(TreeNode<ElemType>*&r)
{
	ElemType ch;
	cin >> ch;
	if (ch == (ElemType)'#')
	{
		r = NULL;
	}
	else
	{
		r = new TreeNode<ElemType>(ch);
		CreateTree(r->FirstChild);
		CreateTree(r->NextSibling);
	}
}
template<class ElemType>
void Tree<ElemType>::PreOrder(TreeNode<ElemType>*r, void(*Visit)(const ElemType &))const
{
	if (r)
	{
		(*Visit)(r->data);
		if (r->FirstChild)
			PreOrder(r->FirstChild, Visit);
		if (r->NextSibling)
			PreOrder(r->NextSibling, Visit);
	}
}
template<class ElemType>
void Tree<ElemType>::PostOrder(TreeNode<ElemType>*r, void(*Visit)(const ElemType &))const
{
	if (r)
	{
		if (r->FirstChild)
			PostOrder(r->FirstChild, Visit);
		(*Visit)(r->data);
		if (r->NextSibling)
			PostOrder(r->NextSibling, Visit);
	}
}
template<class ElemType>
void Tree<ElemType>::PreOrder(void(*Visit)(const ElemType &))const
{
	PreOrder(root, Visit);
}
template<class ElemType>
void Tree<ElemType>::PostOrder(void(*Visit)(const ElemType &))const
{
	PostOrder(root, Visit);
}

template<class ElemType>
void Tree<ElemType>::LevelOrder(void(*Visit)(const ElemType &))const
{
	LinkQueue<TreeNode<ElemType> *>q;
	TreeNode<ElemType> *p;
	TreeNode<ElemType> *temp;
	if (root)
	{
		q.EnQueue(root);
	}
	while (!q.IsEmpty())
	{
		q.DelQueue(p);
		(*Visit)(p->data);
		if (p->FirstChild)
		{
			q.EnQueue(p->FirstChild);
		}
		temp = p->FirstChild;
		while(temp&&temp->NextSibling)
		{
			q.EnQueue(temp->NextSibling);
			temp = temp->NextSibling;
		}

	}
}






#endif // !TREE_HEADER
