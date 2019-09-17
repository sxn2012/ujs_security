#pragma once
#ifndef TREE_TOTAL_HEADER
#define TREE_TOTAL_HEADER
#include "BinTreeNode.h"
#include "LinkQueue.h"
#define max(a,b) a>b?a:b
template<class ElemType>
class BinaryTree
{
protected:
	//数据成员
	BinTreeNode<ElemType> *root;
	//辅助函数:
	BinTreeNode<ElemType> *CopyTree(BinTreeNode<ElemType> *t);//复制二叉树
	void Destroy(BinTreeNode<ElemType> *&r);//删除以r为根的二叉树
	void PreOrder(BinTreeNode<ElemType> *r, void(*Visit)(const ElemType&))const;//先序遍历以r为根的二叉树
	void InOrder(BinTreeNode<ElemType> *r, void(*Visit)(const ElemType&))const;//中序遍历以r为根的二叉树
	void PostOrder(BinTreeNode<ElemType> *r, void(*Visit)(const ElemType&))const;//后序遍历以r为根的二叉树
	int Height(const BinTreeNode<ElemType> *r)const;//二叉树高度
	int NodeCount(const BinTreeNode<ElemType> *r)const;//二叉树结点个数
	BinTreeNode<ElemType> *Parent(BinTreeNode<ElemType> *r, const BinTreeNode<ElemType>*p)const;//在r为根的二叉树中求p的双亲
	//********************************


	

public:
	BinaryTree();//无参构造函数
	BinaryTree(const ElemType &e);//有参构造函数
	virtual ~BinaryTree();//析构函数
	BinTreeNode<ElemType> *GetRoot()const;//求二叉树的根
	bool IsEmpty()const;//判断二叉树是否为空
	Status GetElem(BinTreeNode<ElemType>*p, ElemType &e)const;
	Status SetElem(BinTreeNode<ElemType>*p, const ElemType &e);
	void InOrder(void(*Visit)(const ElemType &))const;//中序遍历
	void PreOrder(void(*Visit)(const ElemType &))const;//先序遍历
	void PostOrder(void(*Visit)(const ElemType &))const;//后序遍历
	void LevelOrder(void(*Visit)(const ElemType &))const;//层序遍历
	int NodeCount()const;//求结点个数
	BinTreeNode<ElemType> *LeftChild(const BinTreeNode<ElemType> *p)const;//结点的左孩子
	BinTreeNode<ElemType> *RightChild(const BinTreeNode<ElemType> *p)const;//结点的右孩子
	BinTreeNode<ElemType> *LeftSibling(const BinTreeNode<ElemType> *p)const;//结点的左兄弟
	BinTreeNode<ElemType> *RightSibling(const BinTreeNode<ElemType> *p)const;//结点的右兄弟
	BinTreeNode<ElemType> *Parent(const BinTreeNode<ElemType>*p)const;//结点的双亲
	void InsertLeftChild(BinTreeNode<ElemType>*p, const ElemType &e);//插入左孩子
	void InsertRightChild(BinTreeNode<ElemType>*p, const ElemType &e);//插入右孩子
	void DeleteLeftChild(BinTreeNode<ElemType>*p);//删除左子树
	void DeleteRightChild(BinTreeNode<ElemType>*p);//删除右子树
	int Height()const;//求二叉树的高
	BinaryTree(const BinaryTree<ElemType>&t);//拷贝构造函数
	BinaryTree(BinTreeNode<ElemType> *r);//建立以r为根的二叉树
	BinaryTree<ElemType>&operator=(const BinaryTree<ElemType> &t);//赋值运算符重载
	//用遍历方法构造二叉树：
	void CreateBtrPre(BinTreeNode<ElemType>*&r);
	//void CreateBtrIn(BinTreeNode<ElemType>*&r);
	//void CreateBtrPost(BinTreeNode<ElemType>*&r);
	//void CreateBinaryTreePreIn

	void changeroot(BinTreeNode<ElemType> *r) { root = r; }



	//第九题
	int Width()const
	{
		LinkQueue<BinTreeNode<ElemType> *>q;
		BinTreeNode<ElemType> *p;
		int s[100] = { 0 };
		int i = 0, j = 0;
		if (root)
		{
			q.EnQueue(root);
		}
		while (!q.IsEmpty())
		{
			q.DelQueue(p);
			
			s[i++] = Height()-Height(p)+1;//求每个结点的深度
			if (p->leftChild)
			{
				q.EnQueue(p->leftChild);
			}
			if (p->rightChild)
			{
				q.EnQueue(p->rightChild);
			}
		}
		//深度一样的结点在同一层，先对数组进行排序，然后找出深度一样的点的个数的最大值
		for (i = 0; i < 100;i++)
		{
			if (s[i]==0)
			{
				break;
			}
		}
		int n = i;//数组中的有效值个数
		//比较排序
		for (i = 0; i < n;i++)
			for (j = i+1; j < n;j++)
				if (s[i]>s[j])
				{
					int swap = s[i];
					s[i] = s[j];
					s[j] = swap;
				}
		//找有序数组中相同值的个数的最大值
		int summax = 0;
		int c = 0;
		for (i = 0; i < n; i++)
		{
			c = 0;
			for (j = i + 1; j < n; j++)
				if (s[i]==s[j])
				{
					c++;
					if (c>summax)
					{
						summax = c;
					}
				} 
				else
				{
					break;
				}
		}
			
		return summax;

	}
	//第十四题
	int CountLeaf(BinTreeNode<ElemType> *r)const
	{
		if (!r)
		{
			return 0;
		} 
		else if (r->leftChild==NULL&&r->rightChild==NULL)
		{
			return 1;
		}
		else
		{
			return CountLeaf(r->leftChild) + CountLeaf(r->rightChild);
		}
	}

};

template<class ElemType>
BinTreeNode<ElemType>* BinaryTree<ElemType>::CopyTree(BinTreeNode<ElemType> *t)
{
	BinaryTree<ElemType>*r=new BinaryTree<ElemType>(t->data);
	return r;
}
template<class ElemType>
void BinaryTree<ElemType>::Destroy(BinTreeNode<ElemType> *&r)
{
	if (r)
	{
		Destroy(r->leftChild);
		Destroy(r->rightChild);
		delete r;
		r = NULL;
	}
}
template<class ElemType>
void BinaryTree<ElemType>::PreOrder(BinTreeNode<ElemType> *r, void(*Visit)(const ElemType&))const
{
	if (r)
	{
		(*Visit)(r->data);
		PreOrder(r->leftChild,Visit);
		PreOrder(r->rightChild,Visit);
	}
}
template<class ElemType>
void BinaryTree<ElemType>::InOrder(BinTreeNode<ElemType> *r, void(*Visit)(const ElemType&))const
{
	if (r)
	{
		InOrder(r->leftChild,Visit);
		(*Visit)(r->data);
		InOrder(r->rightChild,Visit);
	}
}
template<class ElemType>
void BinaryTree<ElemType>::PostOrder(BinTreeNode<ElemType> *r, void(*Visit)(const ElemType&))const
{
	if (r)
	{
		PostOrder(r->leftChild,Visit);
		PostOrder(r->rightChild,Visit);
		(*Visit)(r->data);
	}
}
template<class ElemType>
int BinaryTree<ElemType>::Height(const BinTreeNode<ElemType> *r)const
{
	if (r)
	{
		return max(Height(r->leftChild) + 1, Height(r->rightChild) + 1);
	} 
	else
	{
		return 0;
	}
}
template<class ElemType>
int BinaryTree<ElemType>::NodeCount(const BinTreeNode<ElemType> *r)const
{
	if (r)
	{
		return NodeCount(r->leftChild) + NodeCount(r->rightChild) + 1;
	} 
	else
	{
		return 0;
	}
}
template<class ElemType>
BinTreeNode<ElemType>* BinaryTree<ElemType>::Parent(BinTreeNode<ElemType> *r, const BinTreeNode<ElemType>*p)const
{
	if (!r)
	{
		return NULL;
	} 
	else if (r->leftChild==p||r->rightChild==p)
	{
		return r;
	} 
	else
	{
		BinTreeNode<ElemType>*tmp;
		tmp = Parent(r->leftChild, p);
		if (tmp)
		{
			return tmp;
		} 
		tmp = Parent(r->rightChild, p);
		if (tmp)
		{
			return tmp;
		} 
		else
		{
			return NULL;
		}
	}
}


template<class ElemType>
BinaryTree<ElemType>::BinaryTree()
{
	root = new BinTreeNode<ElemType>();
}
template<class ElemType>
BinaryTree<ElemType>::BinaryTree(const ElemType &e)
{
	root = new BinTreeNode<ElemType>(e);
}
template<class ElemType>
BinaryTree<ElemType>::~BinaryTree()
{
}
template<class ElemType>
BinTreeNode<ElemType>* BinaryTree<ElemType>::GetRoot()const
{
	return root;
}
template<class ElemType>
bool BinaryTree<ElemType>::IsEmpty()const
{
	return root == NULL;
}
template<class ElemType>
Status BinaryTree<ElemType>::GetElem(BinTreeNode<ElemType>*p, ElemType &e)const
{
	if (p)
	{
		e = p->data;
		return SUCCESS;
	} 
	else
	{
		return NOT_PRESENT;
	}
}
template<class ElemType>
Status BinaryTree<ElemType>::SetElem(BinTreeNode<ElemType>*p, const ElemType &e)
{
	if (p)
	{
		p->data = e;
		return SUCCESS;
	} 
	else
	{
		return NOT_PRESENT;
	}
}
template<class ElemType>
void BinaryTree<ElemType>::InOrder(void(*Visit)(const ElemType &))const
{
	InOrder(root, Visit);
}
template<class ElemType>
void BinaryTree<ElemType>::PreOrder(void(*Visit)(const ElemType &))const
{
	PreOrder(root, Visit);
}
template<class ElemType>
void BinaryTree<ElemType>::PostOrder(void(*Visit)(const ElemType &))const
{
	PostOrder(root, Visit);
}
template<class ElemType>
void BinaryTree<ElemType>::LevelOrder(void(*Visit)(const ElemType &))const
{
	LinkQueue<BinTreeNode<ElemType> *>q;
	BinTreeNode<ElemType> *p;
	if (root)
	{
		q.EnQueue(root);
	}
	while (!q.IsEmpty())
	{
		q.DelQueue(p);
		(*Visit)(p->data);
		if (p->leftChild)
		{
			q.EnQueue(p->leftChild);
		}
		if (p->rightChild)
		{
			q.EnQueue(p->rightChild);
		}
	}
}
template<class ElemType>
int BinaryTree<ElemType>::NodeCount()const
{
	return NodeCount(root);
}
template<class ElemType>
BinTreeNode<ElemType>* BinaryTree<ElemType>::LeftChild(const BinTreeNode<ElemType> *p)const
{
	return p->leftChild;
}
template<class ElemType>
BinTreeNode<ElemType>* BinaryTree<ElemType>::RightChild(const BinTreeNode<ElemType> *p)const
{
	return p->rightChild;
}
template<class ElemType>
BinTreeNode<ElemType>* BinaryTree<ElemType>::LeftSibling(const BinTreeNode<ElemType> *p)const
{
	BinTreeNode<ElemType>*r = Parent(root,p);
	if (!r)
	{
		return NULL;
	}
	else if (r->rightChild==p)
	{
		return r->leftChild;
	} 
	else
	{
		return NULL;
	}
	
}
template<class ElemType>
BinTreeNode<ElemType>* BinaryTree<ElemType>::RightSibling(const BinTreeNode<ElemType> *p)const
{
	BinTreeNode<ElemType>*r = Parent(root,p);
	if (!r)
	{
		return NULL;
	}
	else if (r->leftChild == p)
	{
		return r->rightChild;
	}
	else
	{
		return NULL;
	}
}
template<class ElemType>
BinTreeNode<ElemType>* BinaryTree<ElemType>::Parent(const BinTreeNode<ElemType>*p)const
{
	return Parent(root, p);
}
template<class ElemType>
void BinaryTree<ElemType>::InsertLeftChild(BinTreeNode<ElemType>*p, const ElemType &e)
{
	if (!p)
	{
		return;
	} 
	else
	{
		BinTreeNode<ElemType> *child = new BinTreeNode<ElemType>(e);
		if (p->leftChild)
		{
			child->leftChild = p->leftChild;
		}
		p->leftChild = child;
	}
	
}
template<class ElemType>
void BinaryTree<ElemType>::InsertRightChild(BinTreeNode<ElemType>*p, const ElemType &e)
{
	if (!p)
	{
		return;
	}
	else
	{
		BinTreeNode<ElemType> *child = new BinTreeNode<ElemType>(e);
		if (p->rightChild)
		{
			child->rightChild = p->rightChild;
		}
		p->rightChild = child;
	}

}
template<class ElemType>
void BinaryTree<ElemType>::DeleteLeftChild(BinTreeNode<ElemType>*p)
{
	if (!p)
	{
		return;
	} 
	else
	{
		p->leftChild = NULL;
	}
}
template<class ElemType>
void BinaryTree<ElemType>::DeleteRightChild(BinTreeNode<ElemType>*p)
{
	if (!p)
	{
		return;
	}
	else
	{
		p->rightChild = NULL;
	}
}
template<class ElemType>
int BinaryTree<ElemType>::Height()const
{
	return Height(root);
}
template<class ElemType>
BinaryTree<ElemType>::BinaryTree(BinTreeNode<ElemType>*r)
{
	root = r;
	
}
template<class ElemType>
BinaryTree<ElemType>& BinaryTree<ElemType>::operator=(const BinaryTree<ElemType>&t)
{
	return t;
}
template<class ElemType>
void BinaryTree<ElemType>::CreateBtrPre(BinTreeNode<ElemType>*&r)
{
	ElemType ch;
	cin >> ch;
	if (ch==(ElemType)'#')
	{
		r = NULL;
	} 
	else
	{
		r = new BinTreeNode<ElemType>(ch);
		CreateBtrPre(r->leftChild);
		CreateBtrPre(r->rightChild);
	}
}
























#endif // !TREE_TOTAL_HEADER
