#pragma once
#ifndef SEQSTACK_HEADER_QUEUE_COMPOSE
#define SEQSTACK_HEADER_QUEUE_COMPOSE
#include <iostream>
#include <assert.h>
using namespace std;

template<class ElemType>
class SeqStack
{
protected:
	int top;
	int maxSize;
	ElemType *elems;
public:
	SeqStack(int size = DEFAULT_SIZE);
	virtual ~SeqStack();
	Status Push( const ElemType e);
	Status Pop(ElemType &e);
	Status Top( ElemType &e)const;
	void Traverse(void(*Visit)(const ElemType &))const;
	void Inverse();
	void Clear();
	bool IsEmpty()const;
	bool IsFull()const;
	int GetLength()const;
	int GetSize()const;
	ElemType GetElem(int i)const;
	void ChangeElem(int i, const ElemType &e);
	void Dec();
	SeqStack<ElemType> &operator=(const SeqStack<ElemType>&s);
	
};
template<class ElemType>
SeqStack<ElemType>::SeqStack(int size)
{
	maxSize = size;
	top = -1;
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
	return (top == -1);
}
template<class ElemType>
bool SeqStack<ElemType>::IsFull()const
{
	return (top==maxSize);
}
template<class ElemType>
Status SeqStack<ElemType>::Push( const ElemType e)
{
	if (IsFull())
		return OVER_FLOW;
	else
	{
		
		
			elems[++top] = e;
			return SUCCESS;
		
		

		
	}
}
template<class ElemType>
Status SeqStack<ElemType>::Pop( ElemType &e)
{
	if (IsEmpty())
		return UNDER_FLOW;
	
	else
	{
		
			e = elems[top--];
			return SUCCESS;
			
		
		
	}
}
template<class ElemType>
void SeqStack<ElemType>::Traverse( void(*Visit)(const ElemType &))const
{
	
	
	
		for (int i = top; i >= 0; i--)
			(*Visit)(elems[i]);
		
	
	
}
template<class ElemType>
int SeqStack<ElemType>::GetLength()const
{
	
	
		return top - (-1);
		
	
}
template<class ElemType>
int SeqStack<ElemType>::GetSize()const
{
	return maxSize;
}
template<class ElemType>
Status SeqStack<ElemType>::Top(ElemType &e)const
{
	if (IsEmpty())
	{
		return UNDER_FLOW;
	}
	
	else
	{
		
		
			e = elems[top];
			return SUCCESS;
	}
}
template<class ElemType>
void SeqStack<ElemType>::Clear()
{
	top = -1;
}
template<class ElemType>
void SeqStack<ElemType>::Inverse()//ÄæÖÃ
{
	SeqStack<ElemType> s(maxSize);
	for (int i = 0; i <= top;i++)
	{
		s.Push(elems[i]);
	}
	Clear();
	ElemType *p = new ElemType[maxSize];
	assert(p);
	int i = 0;
	while (s.Pop(p[i])==SUCCESS)
	{
		Push(p[i]);
		i++;
	}
}
template<class ElemType>
SeqStack<ElemType> & SeqStack<ElemType>::operator=(const SeqStack<ElemType>&s)
{
	SeqStack<ElemType> sa(s.GetSize());
	for (int i = 0; i <= top; i++)
		sa.elems[i]=s.elems[i];
	sa.top=s.top;
	return sa;
}
template<class ElemType>
ElemType SeqStack<ElemType>::GetElem(int i)const
{
	return elems[i];
}
template<class ElemType>
void SeqStack<ElemType>::ChangeElem(int i, const ElemType &e)
{
	elems[i] = e;
}
template<class ElemType>
void SeqStack<ElemType>::Dec()
{
	top--;
}
#endif