#pragma once
#ifndef LISTNEXTWORKARC_ADJ_HEADER
#define LISTNEXTWORKARC_ADJ_HEADER
#include "LinkQueue.h"
template<class WeightType>
struct AdjListNetWorkArc
{
	int adjVex;
	WeightType weight;
	AdjListNetWorkArc<WeightType> *nextarc;

	AdjListNetWorkArc();
	AdjListNetWorkArc(int v, WeightType w, AdjListNetWorkArc<WeightType> *next = NULL);
};
template<class WeightType>
AdjListNetWorkArc<WeightType>::AdjListNetWorkArc()
{
	adjVex = -1;
}
template<class WeightType>
AdjListNetWorkArc<WeightType>::AdjListNetWorkArc(int v, WeightType w, AdjListNetWorkArc<WeightType> *next/* =NULL */)
{
	adjVex = v;
	weight = w;
	nextarc = next;
}



#endif // !LISTNEXTWORKARC_ADJ_HEADER
