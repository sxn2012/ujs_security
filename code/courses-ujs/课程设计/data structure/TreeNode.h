#pragma once
#ifndef TNODE_HEADER
#define TNODE_HEADER
#include "Status.h"
using namespace std;
struct TreeNode
{
public:
	DATA data;//数据域
	TreeNode *FirstChild;//孩子指针
	TreeNode *NextSibling;//兄弟指针
	TreeNode();//无参构造函数
	TreeNode(DATA d,TreeNode*child=NULL,TreeNode*sibling=NULL);//有参构造函数
	//辅助函数
	Status GetName(string &name)const;
};
TreeNode::TreeNode():data()
{
	FirstChild = NULL;
	NextSibling = NULL;
}
TreeNode::TreeNode(DATA d, TreeNode*child/* =NULL */, TreeNode*sibling/* =NULL */) : data(d)//调用数据域结构体的构造函数
{
	FirstChild = child;
	NextSibling = sibling;
	/*
	两个指针赋值
	*/
}
Status TreeNode::GetName(string &name)const
{
	name = data.MemName;
	return SUCCESS;
}


#endif
