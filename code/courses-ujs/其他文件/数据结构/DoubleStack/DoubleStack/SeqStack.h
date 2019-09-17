#pragma once
#ifndef SEQSTACK_HEADER_DOUBLESTACK 
#define SEQSTACK_HEADER_DOUBLESTACK
#include <iostream>
#include <assert.h>
using namespace std;
enum{DEFAULT_SIZE=20};
template<class ElemType>
class SeqStack
{
protected:
	int top1, top2;
	int maxSize;
	ElemType *elems;
public:
	SeqStack(int size = DEFAULT_SIZE);
	virtual ~SeqStack();
	Status Push(int No,const ElemType e);
	Status Pop(int No, ElemType &e);
	Status Top(int No,ElemType &e)const;
	void Traverse(int No, void(*Visit)(const ElemType &))const;
	bool IsEmpty()const;
	bool IsFull()const;
	int GetLength(int No)const;
};
template<class ElemType>
SeqStack<ElemType>::SeqStack(int size)
{
	maxSize = size;
	top1 = -1;
	top2 = size;
	if (elems) delete[]elems;
	elems = new ElemType[maxSize];
	assert(elems);
}
template<class ElemType>
SeqStack<ElemType>::~SeqStack()
{
	delete[]elems;
}
template<class ElemType>
bool SeqStack<ElemType>::IsEmpty()const
{
	return ((top1 == -1) && (top2 == maxSize));
}
template<class ElemType>
bool SeqStack<ElemType>::IsFull()const
{
	return (top1+1==top2);
}
template<class ElemType>
Status SeqStack<ElemType>::Push(int No,const ElemType e)
{
	if (IsFull())
		return OVER_FLOW;
	else
	{
		switch (No)
		{
		case 1:
			elems[++top1] = e;
			return SUCCESS;
			break;
		case 2:
			elems[--top2] = e;
			return SUCCESS;
			break;

		}
	}
}
template<class ElemType>
Status SeqStack<ElemType>::Pop(int No,ElemType &e)
{
	if (IsEmpty())
		return UNDER_FLOW;
	else if ((No == 1) && (top1 == -1))
	{
		return UNDER_FLOW;
	}
	else if ((No == 2) && (top2 == maxSize))
	{
		return UNDER_FLOW;
	}
	else
	{
		switch (No)
		{
		case 1:
			e = elems[top1--];
			return SUCCESS;
			break;
		case 2:
			e = elems[top2++];
			return SUCCESS;
			break;
		}
	}
}
template<class ElemType>
void SeqStack<ElemType>::Traverse(int No, void(*Visit)(const ElemType &))const
{
	switch (No)
	{
	case 1:
		for (int i = top1; i >= 0; i--)
			(*Visit)(elems[i]);
		break;
	case 2:
		for (int i = top2; i <maxSize; i++)
			(*Visit)(elems[i]);
		break;
	}
}
template<class ElemType>
int SeqStack<ElemType>::GetLength(int No)const
{
	switch (No)
	{
	case 1:
		return top1 - (-1);
		break;
	case 2:
		return maxSize - top2;
		break;
	}
}
template<class ElemType>
Status SeqStack<ElemType>::Top(int No,ElemType &e)const
{
	if (IsEmpty())
	{
		return UNDER_FLOW;
	}
	else if ((No == 1) && (top1 == -1))
	{
		return UNDER_FLOW;
	}
	else if ((No == 2) && (top2 == maxSize))
	{
		return UNDER_FLOW;
	}
	else
	{
		switch (No)
		{
		case 1:
			e = elems[top1];
			break;
		case 2:
			e = elems[top2];
			break;
		}
	}
}
#endif