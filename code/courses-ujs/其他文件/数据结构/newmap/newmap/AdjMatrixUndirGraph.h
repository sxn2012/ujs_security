#pragma once
#ifndef UNDIRGRAPH_ADJMATRIX_HEADER
#define UNDIRGRAPH_ADJMATRIX_HEADER
template <class ElemType>
class AdjMatrixUndirGraph
{
protected:
	int vexNum, vexMaxNum, arcNum;//顶点数目，最大顶点数目，边数目
	int **arcs;//存放边信息的邻接矩阵
	ElemType *vertexes;//存放顶点信息的数组
	mutable Status *tag;//标志数组
public:
	AdjMatrixUndirGraph(ElemType es[], int vertexNum,
		int vertexMaxNum = DEFAULT_SIZE);
	AdjMatrixUndirGraph(int vertexMaxNum = DEFAULT_SIZE);
	~AdjMatrixUndirGraph();
	void Clear();//清空图
	bool IsEmpty();//判断无向图是否为空
	int GetOrder(ElemType &d) const;//求顶点的序号
	Status GetElem(int v, ElemType &d) const;//求顶点元素的值 
	Status SetElem(int v, const ElemType &d);//设置顶点的元素值
	int GetVexNum() const;//返回顶点个数
	int GetArcNum() const;//返回边数
	int FirstAdjVex(int v) const;//返回顶点v的第一个邻接点
	int NextAdjVex(int v1, int v2) const;//返回顶点v1相对于v2的下一个邻接点
	void InsertVex(const ElemType &d);//插入元素值为d的顶点
	void InsertArc(int v1, int v2);//插入顶点为v1和v2的边
	void DeleteVex(const ElemType &d);//删除元素值为d的顶点
	void DeleteArc(int v1, int v2);//删除顶点为v1和v2的边
	Status GetTag(int v) const;//返回顶点v的标志
	void SetTag(int v, Status val)//设置顶点v的标志为val
	{
		tag[v] = val;
	}
	AdjMatrixUndirGraph(const
		AdjMatrixUndirGraph<ElemType> &g);	//拷贝构造函数
	AdjMatrixUndirGraph<ElemType> &operator =(
		const AdjMatrixUndirGraph<ElemType> &g); //赋值重载
	void Display();//显示邻接矩阵无向图
};
template <class ElemType>
AdjMatrixUndirGraph<ElemType>::AdjMatrixUndirGraph(int vertexMaxNum)
{
	if (vertexMaxNum < 0)
		throw out_of_range("允许的顶点最大数目不能为负!");
	vexNum = 0;
	vexMaxNum = vertexMaxNum;
	arcNum = 0;
	vertexes = new ElemType[vexMaxNum];
	tag = new Status[vexMaxNum];
	arcs = (int **)new int *[vexMaxNum];
	for (int v = 0; v < vexMaxNum; v++)
		arcs[v] = new int[vexMaxNum];
}
template <class ElemType>
AdjMatrixUndirGraph<ElemType>::AdjMatrixUndirGraph(ElemType es[],
	int vertexNum, int vertexMaxNum)   {
	if (vertexMaxNum < 0)
		throw out_of_range("允许的顶点最大数目不能为负!");
	if (vertexMaxNum < vertexNum)
		throw out_of_range("顶点数目不能大于允许的顶点最大数目!");
	vexNum = vertexNum;	vexMaxNum = vertexMaxNum;
	arcNum = 0;	vertexes = new ElemType[vexMaxNum];
	tag = new Status[vexMaxNum];
	arcs = (int **)new int *[vexMaxNum];
	for (int v = 0; v < vexMaxNum; v++) 	arcs[v] = new int[vexMaxNum];
	for (int v = 0; v < vexNum; v++) {
		vertexes[v] = es[v];  tag[v] = UNVISITED;
		for (int u = 0; u < vexNum; u++)  arcs[v][u] = 0;
	}
}
template <class ElemType>
int AdjMatrixUndirGraph<ElemType>::FirstAdjVex(int v) const
{
	if (v < 0 || v >= vexNum)
		throw out_of_range("v不合法!");
	for (int u = 0; u < vexNum; u++)
	if (arcs[v][u] != 0)
		return u;
	return -1;
}
template <class ElemType>
int AdjMatrixUndirGraph<ElemType>::NextAdjVex(int v1,
	int v2) const
{
	if (v1 < 0 || v1 >= vexNum)   throw out_of_range("v1不合法!");
	if (v2 < 0 || v2 >= vexNum)    throw out_of_range("v2不合法!");
	if (v1 == v2) throw out_of_range("v1不能等于v2!");
	for (int u = v2 + 1; u < vexNum; u++)
	if (arcs[v1][u] != 0)
		return u;
	return -1;
}
template <class ElemType>
void AdjMatrixUndirGraph<ElemType>::InsertVex(const ElemType &d)  {
	if (vexNum == vexMaxNum)
		throw out_of_range("图的顶点数不能超过允许的最大数!");
	vertexes[vexNum] = d;
	tag[vexNum] = UNVISITED;
	for (int v = 0; v <= vexNum; v++) {
		arcs[vexNum][v] = 0;
		arcs[v][vexNum] = 0;
	}
	vexNum++;
}
template <class ElemType>
void AdjMatrixUndirGraph<ElemType>::InsertArc(int v1, int v2)
{
	if (v1 < 0 || v1 >= vexNum)    throw out_of_range("v1不合法!");
	if (v2 < 0 || v2 >= vexNum)    throw out_of_range("v2不合法!");
	if (v1 == v2)    throw out_of_range("v1不能等于v2!");
	if (arcs[v1][v2] == 0) {
		arcNum++;
		arcs[v1][v2] = 1;
		arcs[v2][v1] = 1;
	}
}
template <class ElemType>
void AdjMatrixUndirGraph<ElemType>::DeleteVex(const ElemType &d)  {
	int v;
	for (v = 0; v < vexNum; v++)//寻找顶点d在顶点数组中的下标
	if (vertexes[v] == d)     break;
	if (v == vexNum)  throw out_of_range("图中不存在要删除的顶点!");
	for (int u = 0; u < vexNum; u++) //删除依附于顶点d的边
	if (arcs[v][u] == 1) {
		arcNum--;
		arcs[v][u] = 0;
		arcs[u][v] = 0;
	}
	vexNum--; //顶点数目减1
	if (v < vexNum) {//顶点d不是顶点表中最后一个顶点
		//移动最后一个顶点信息到顶点d的位置
		vertexes[v] = vertexes[vexNum];
		tag[v] = tag[vexNum];//移动最后一个顶点的访问标志
		//移动邻接矩阵中最后一行和最后一列到顶点d的行和列
		for (int u = 0; u <= vexNum; u++)
			arcs[v][u] = arcs[vexNum][u];
		for (int u = 0; u <= vexNum; u++)
			arcs[u][v] = arcs[u][vexNum];
	}
}
template <class ElemType>
void AdjMatrixUndirGraph<ElemType>::DeleteArc(int v1, int v2)
{
	if (v1 < 0 || v1 >= vexNum)    throw out_of_range("v1不合法!");
	if (v2 < 0 || v2 >= vexNum)    throw out_of_range("v2不合法!");
	if (v1 == v2)    throw out_of_range("v1不能等于v2!");
	if (arcs[v1][v2] != 0)
	{
		arcNum--;
		arcs[v1][v2] = 0;
		arcs[v2][v1] = 0;
	}
}







#endif // !STATUS_HEADER
