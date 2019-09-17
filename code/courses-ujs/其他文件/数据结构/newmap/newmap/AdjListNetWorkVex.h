#pragma once
#ifndef LISTNEXTWORKVEX_ADJ_HEADER
#define LISTNEXTWORKVEX_ADJ_HEADER
#include "AdjListNetworkArc.h"
template<class ElemType, class WeightType>
struct AdjListNetWorkVex
{
	ElemType data;
	AdjListNetWorkArc<WeightType> *firstarc;

	AdjListNetWorkVex();
	AdjListNetWorkVex(ElemType val, AdjListNetWorkArc<WeightType> *adj = NULL);

};
template<class ElemType, class WeightType>
AdjListNetWorkVex<ElemType, WeightType>::AdjListNetWorkVex()
{
	firstarc = NULL;
}
template<class ElemType, class WeightType>
AdjListNetWorkVex<ElemType, WeightType>::AdjListNetWorkVex(ElemType val, AdjListNetWorkArc<WeightType> *adj /* = NULL */)
{
	data = val;
	firstarc = adj;
}




















#endif // !LISTNEXTWORKVEX_ADJ_HEADER
