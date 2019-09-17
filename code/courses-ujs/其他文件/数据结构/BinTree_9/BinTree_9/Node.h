#pragma once
#ifndef NODE_HEADER_FILE
#define NODE_HEADER_FILE
#include "Status.h"
template<class ElemType>
struct Node
{
//protected:
	ElemType data;
	Node<ElemType>*next;
//public:
	Node();
	Node(ElemType e, Node<ElemType>*link = NULL);
	virtual ~Node();
};
template<class ElemType>
Node<ElemType>::Node()
{
	next = NULL;
}
template<class ElemType>
Node<ElemType>::Node(ElemType e, Node<ElemType>*link /* = NULL */)
{
	data = e;
	next = link;
}
template<class ElemType>
Node<ElemType>::~Node()
{

}


#endif // !NODE_HEADER_FILE
