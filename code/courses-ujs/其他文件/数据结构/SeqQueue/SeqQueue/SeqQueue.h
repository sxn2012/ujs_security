#pragma once
#ifndef SEQLIST_HEADER
#define SEQLIST_HEADER
#include <iostream>
#include <assert.h>
using namespace std;
enum{ DEFAULT_SIZE =20 };
template<class ElemType>
class SeqQueue
{
protected:
	int front, length;
	int maxSize;
	ElemType *elems;

public:
	SeqQueue(int size=DEFAULT_SIZE);
	virtual ~SeqQueue();
	bool IsEmpty()const;
	bool IsFull()const;
	Status EnQueue(const ElemType &e);
	Status DelQueue(ElemType &e);

};
template<class ElemType>
SeqQueue<ElemType>::SeqQueue(int size)
{
	maxSize = size;
	if (elems)
	{
		delete[]elems;
	}
	elems = new ElemType[maxSize];
	assert(elems);
	front = length = 0;
}
template<class ElemType>
SeqQueue<ElemType>::~SeqQueue()
{
	delete[]elems;
}
template<class ElemType>
bool SeqQueue<ElemType>::IsEmpty()const
{
	return length == 0;
}
template<class ElemType>
bool SeqQueue<ElemType>::IsFull()const
{
	return length == maxSize;
}
template<class ElemType>
Status SeqQueue<ElemType>::EnQueue(const ElemType &e)
{
	if (IsFull())
	{
		return OVER_FLOW;
	}
	else
	{
		elems[(front+length)%maxSize] = e;
		length++;
		return SUCCESS;
	}
}
template<class ElemType>
Status SeqQueue<ElemType>::DelQueue(ElemType &e)
{
	if (IsEmpty())
	{
		return UNDER_FLOW;
	} 
	else
	{
		e = elems[front];
		length--;
		front = (front + 1) % maxSize;
		return SUCCESS;
	}

}
#endif 
