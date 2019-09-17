#pragma once
#ifndef QUENUE_HEADER_QUEUE_COMPOSE
#define QUENUE_HEADER_QUEUE_COMPOSE

#include<iostream>
#include <assert.h>
using namespace std;

template<class ElemType>
class Queue
{
protected:
	SeqStack<ElemType> s1, s2;
public:
	Queue(int size1 = DEFAULT_SIZE, int size2 = DEFAULT_SIZE);
	virtual ~Queue();
	Status EnQueue(const ElemType &e);
	Status DeQueue(ElemType &e);
	bool IsEmpty()const;
	bool IsFull()const;
};
template<class ElemType>
Queue<ElemType>::Queue(int size1, int size2) :s1(size1),s2(size2)
{
}
template<class ElemType>
Queue<ElemType>::~Queue()
{
}
template<class ElemType>
bool Queue<ElemType>::IsEmpty()const
{
	return s1.IsEmpty() && s2.IsEmpty();
}
template<class ElemType>
bool Queue<ElemType>::IsFull()const
{
	return s1.IsFull() && s2.IsFull();
}
template<class ElemType>
Status Queue<ElemType>::EnQueue(const ElemType &e)
{
	if (IsFull())
	{
		return OVER_FLOW;
	} 
	else
	{
		if (s1.IsFull())
		{
			return s2.Push(e);
		} 
		else
		{
			return s1.Push(e);
		}
	}
}
template<class ElemType>
Status Queue<ElemType>::DeQueue(ElemType &e)
{
	
	if (IsEmpty())
	{
		return UNDER_FLOW;
	} 
	else
	{
		e = s1.GetElem(0);
		if (s1.IsFull())
		{
			ElemType tmp = s2.GetElem(0);
			for (int i = 1; i < s1.GetSize(); i++)
				s1.ChangeElem(i - 1,s1.GetElem(i));
			s1.ChangeElem(s1.GetSize(), tmp);
			for (int i = 1; i < s2.GetSize(); i++)
				s2.ChangeElem(i - 1,s2.GetElem(i));
			s2.Dec();
		} 
		else
		{
			for (int i = 1; i < s1.GetSize(); i++)
				s1.ChangeElem(i - 1, s1.GetElem(i));
			s1.Dec();
		}
	}
}

#endif 
