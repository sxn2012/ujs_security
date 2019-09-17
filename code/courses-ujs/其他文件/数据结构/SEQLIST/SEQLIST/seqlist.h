#pragma once
#ifndef CLASS_SEQLIST_MEMBER
#define CLASS_SEQLIST_MEMBER
#include<iostream>
#include<assert.h>
using namespace std;
enum {DEFAULT_SIZE=10};
typedef enum{NOT_PRESENT=1,ENTRY_FOUND=0,RANGE_ERROR=2,SUCCESS=0,OVER_FLOW=3}Status;
template<class ElemType>
class SeqList
{
protected:
	int length;
	int maxlength;
	ElemType *elems;
public:
	SeqList(int size = DEFAULT_SIZE);
	SeqList(ElemType v[], int n, int size = DEFAULT_SIZE);
	virtual ~SeqList();
	int GetLength() const;
	bool IsEmpty() const;
	void Clear();
	void Traverse(void(*Visit)(const ElemType &)) const;
	int LocateElem(const ElemType &e);
	Status GetElem(int i, ElemType &e) const;
	Status SetElem(int i, const ElemType &e);
	Status DeleteElem(int i, ElemType &e);
	Status InsertElem(int i, const ElemType &e);
	Status InsertElem(const ElemType &e);
	SeqList(const SeqList<ElemType> &sa);
	SeqList<ElemType> &operator=(const SeqList<ElemType> &sa);
};
template<class ElemType>
SeqList<ElemType>::SeqList(int size = DEFAULT_SIZE)
{
	elems = new ElemType[size];
	assert(elems);
	maxlength = size;
	length = 0;
}
template<class ElemType>
SeqList<ElemType>::SeqList(ElemType v[], int n, int size = DEFAULT_SIZE)
{
	elems = new ElemType[size];
	assert(elems);
	length = n;
	maxlength = size;
	for (int i = 0; i < length; i++)
		elems[i] = v[i];
}
template<class ElemType>
 SeqList<ElemType>:: ~SeqList()
{
	delete[]elems;
	length = 0;
	maxlength = 0;
}
template<class ElemType>
int SeqList<ElemType>::GetLength() const
{
	return length;
}
template<class ElemType>
bool SeqList<ElemType>::IsEmpty() const
{
	return length ? false : true;
}
template<class ElemType>
void SeqList<ElemType>::Clear()
{
	length = 0;
}
template<class ElemType>
void SeqList<ElemType>::Traverse(void(*Visit)(const ElemType &)) const
{
	for (int i = 0; i < length; i++)
		(*Visit)(elems[i]);
}
template<class ElemType>
int SeqList<ElemType>::LocateElem(const ElemType &e)
{
	for (int i = 0; i < length; i++)
	if (elems[i] == e) return i + 1;
	return 0;
}
template<class ElemType>
Status SeqList<ElemType>::GetElem(int i, ElemType &e) const
{
	if (i<1 || i>length) return NOT_PRESENT;
	e = elems[i-1];
	return ENTRY_FOUND;
}
template<class ElemType>
Status SeqList<ElemType>::SetElem(int i, const ElemType &e)
{
	if (i<1 || i>length) return RANGE_ERROR;
	elems[i-1] = e;
	return SUCCESS;
}
template<class ElemType>
Status SeqList<ElemType>::DeleteElem(int i, ElemType &e)
{
	if (i<1 || i>length) return RANGE_ERROR;
	for (int j = i; j < length; j++)
		elems[j - 1] = elems[j];
	length--;
	e = elems[i - 1];
	return SUCCESS;
}
template<class ElemType>
Status SeqList<ElemType>::InsertElem(int i, const ElemType &e)
{
	if (length == maxlength) return OVER_FLOW;
	if (i<1 || i>length+1) return RANGE_ERROR;
	for (int j = length; j >= i; j--)
		elems[j] = elems[j - 1];
	elems[i - 1] = e;
	length++;
	return SUCCESS;
}
template<class ElemType>
Status SeqList<ElemType>::InsertElem(const ElemType &e)
{
	if (length == maxlength) return OVER_FLOW;
	elems[length] = e;
	length++;
	return SUCCESS;
}
template<class ElemType>
SeqList<ElemType>::SeqList(const SeqList<ElemType> &sa)
{
	length = sa.length;
	maxlength = sa.maxlength;
	elems = new ElemType[maxlength];
	assert(elems);
	for (int i = 0; i < length; i++)
		elems[i] = sa.elems[i];
}
template<class ElemType>
SeqList<ElemType> & SeqList<ElemType>:: operator=(const SeqList<ElemType> &sa)
{
	this->length = sa.length;
	for (int i = 0; i < length; i++)
		this->elems[i] = sa.elems[i];
	return *this;
}
int Delmin(SeqList<int> &sa);
void Delel(SeqList<int> &sa, int e);
void Delrep(SeqList<int> &sa);
void Insertnum(SeqList<int> &sa, int e);
void Deletenum(SeqList<int> &sa, int e);
SeqList<int>  Toge(SeqList<int> &sa, SeqList<int> &sb);
void Deleteseq(SeqList<int> &sa, int s, int t);



#endif