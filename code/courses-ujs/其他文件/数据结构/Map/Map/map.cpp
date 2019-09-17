// map.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <iostream>
#include <fstream>
using namespace std;
#include <exception>
#include <fstream>
#include "status.h"
#include "AdjListNetworkArc.h"
#include "AdjListNetworkVex.h"
#include "AdjListDirNetwork.h"
#include "AdjMatrixUndirGraph.h"
#include "Node.h"
#include "LinkQueue.h"

template <class ElemType>
void DFS(const AdjMatrixUndirGraph<ElemType> &g,int v,void (*Visit)(const ElemType &))  
{	
    ElemType e;	
    g.SetTag(v, VISITED); //设置顶点v 已访问标记
    g.GetElem(v, e);	//取顶点v的数据元素
    Visit(e);//访问顶点v
    for (int w=g.FirstAdjVex(v); w != -1;w=g.NextAdjVex(v, w))
        if (g.GetTag(w) == UNVISITED)
            DFS(g, w, Visit);//从v的邻接点w开始深度优先搜索
}
template <class ElemType>
void DFSTraverse(const AdjMatrixUndirGraph<ElemType> &g, void (*Visit)(const ElemType &))  
{
	int v;
	for (v=0; v < g.GetVexNum(); v++)//设置未访问标志
		g.SetTag(v, UNVISITED);
          //逐个判断顶点，从未访问顶点开始深度优先搜索
	for (v=0; v < g.GetVexNum(); v++)
 		if (g.GetTag(v) == UNVISITED)
			DFS(g, v, Visit);
}
template <class ElemType>
void BFS(const AdjMatrixUndirGraph<ElemType> &g, int v, void (*Visit)(const ElemType &))
{	
    LinkQueue<int> q;
    int u, w;
    ElemType e;    
    g.SetTag(v, VISITED); //设置访问标志  
    g.GetElem(v, e);//取顶点v的数据元素值
    Visit(e);//访问顶点v
    q.EnQueue(v);//顶点v入队
	while (!q.IsEmpty())  {	
        q.DelQueue(u);//队头顶点u出队
        //逐个判断u的邻接点，若未访问则访问之并入队
        for (w=g.FirstAdjVex(u); w != -1; w=g.NextAdjVex(u, w))
            if (g.GetTag(w) == UNVISITED){ 
                g.SetTag(w, VISITED); 
                g.GetElem(w, e);	
	     Visit(e);  
                q.EnQueue(w);
            }	
     }
}
template <class ElemType>
void BFSTraverse(const AdjMatrixUndirGraph<ElemType> &g,  void (*Visit)(const ElemType &)) {
	int v;
	for (v=0; v < g.GetVexNum(); v++)//设置未访问标志
	     g.SetTag(v, UNVISITED);
           //逐个判断顶点，从未访问顶点开始广度优先搜索
	for (v=0; v < g.GetVexNum(); v++)
		if (g.GetTag(v) == UNVISITED) 
			BFS(g, v, Visit);
}






//从顶点v开始进行深度优先搜索
template <class ElemType, class WeightType>
void DFS(AdjListDirNetwork<ElemType, WeightType> &graph, int v, void(*Visit)(const ElemType &))
{
	ElemType e;
	graph.SetTag(v, VISITED); //设置顶点v 已访问标记
	graph.GetElem(v, e);	//取顶点v的数据元素
	Visit(e);//访问顶点v
	for (int w = graph.FirstAdjVex(v); w != -1; w = graph.NextAdjVex(v, w))
	if (graph.GetTag(w) == UNVISITED)
		DFS(graph, w, Visit);//从v的邻接点w开始深度优先搜索
}

//对图graph进行深度优先遍历
template <class ElemType, class WeightType>
void DFSTraverse(AdjListDirNetwork<ElemType, WeightType> &graph, void(*Visit)(const ElemType &))
{
	int v;
	for (v = 0; v < graph.GetVexNum(); v++)//设置未访问标志
		graph.SetTag(v, UNVISITED);
	//逐个判断顶点，从未访问顶点开始深度优先搜索
	for (v = 0; v < graph.GetVexNum(); v++)
	if (graph.GetTag(v) == UNVISITED)
		DFS(graph, v, Visit);
}

//从顶点v开始广度优先搜索
template <class ElemType, class WeightType>
void BFS(AdjListDirNetwork<ElemType, WeightType> &graph, int v, void(*Visit)(const ElemType &))
{
	LinkQueue<int> q;
	int u, w;
	ElemType e;
	graph.SetTag(v, VISITED); //设置访问标志  
	graph.GetElem(v, e);//取顶点v的数据元素值
	Visit(e);//访问顶点v
	q.EnQueue(v);//顶点v入队
	while (!q.IsEmpty())
	{
		q.DelQueue(u);//队头顶点u出队
		//逐个判断u的邻接点，若未访问则访问之并入队
		for (w = graph.FirstAdjVex(u); w != -1; w = graph.NextAdjVex(u, w))
		if (graph.GetTag(w) == UNVISITED)
		{
			graph.SetTag(w, VISITED);
			graph.GetElem(w, e);
			Visit(e);
			q.EnQueue(w);
		}
	}
}

//对图graph进行广度优先遍历
template <class ElemType, class WeightType>
void BFSTraverse(AdjListDirNetwork<ElemType, WeightType> &graph, void(*Visit)(const ElemType &))
{
	int v;
	for (v = 0; v < graph.GetVexNum(); v++)//设置未访问标志
		graph.SetTag(v, UNVISITED);
	//逐个判断顶点，从未访问顶点开始广度优先搜索
	for (v = 0; v < graph.GetVexNum(); v++)
	if (graph.GetTag(v) == UNVISITED)
		BFS(graph, v, Visit);
}

void Display(const char &e)
{
	cout << e << " ";
}
bool LoadData(AdjListDirNetwork<char, int> &g)
{
	ifstream in("GraphData.txt",ios::in);
	if (!in) return false;
	int a,b;
	in>>a>>b;
	char c[15];
	for (int i=0;i<a;i++)
	{ 
		in>>c[i];
		g.InsertVex(c[i]);
	}
	int e1[35],e2[35],we[35];
	int j=0;
	while(!in.eof())
	{
		in>>e1[j]>>e2[j]>>we[j];
		g.InsertArc(e1[j],e2[j],we[j]);
		j++;
	}

	in.close();
	return true;
}
void main(void)
{
	AdjListDirNetwork<char, int> graph(20, 9999);
	char start, end;

	//从文件GraphData.txt中读取有向图数据，建立图graph
	if (!LoadData(graph))//--------------------调用LoadData函数从文件GraphData.txt读取有向图数据建立图graph，请自己实现
	{
		cout << "图建立失败！" << endl;
		exit(1);
	}
	cout << "图的深度优先遍历序列为：";
	DFSTraverse(graph, Display);
	cout << endl;
	cout << "图的广度优先遍历序列为：";
	BFSTraverse(graph, Display);
	cout << endl;
/*	//-----------------------------以下测试第1题------------------------------------------------
	cout << "请输入路径的起点名称（A）：";
	cin >> start;//输入：A
	cout << "请输入路径的终点名称（G）：";
	cin >> end;//输入：G
	if (ExistPathDFS(graph, start, end))//------------------调用第1题函数--------------------------
		cout << "按照深度优先搜索策略判断：" << start << "与" << end << "存在路径!" << endl;
	else
		cout << "按照深度优先搜索策略判断：" << start << "与" << end << "不存在路径!" << endl;
	//-----------------------------以下测试第2题------------------------------------------------
	cout << "请输入路径的起点名称（A）：";
	cin >> start;//输入：A
	cout << "请输入路径的终点名称（H）：";
	cin >> end;//输入：H
	if (ExistPathBFS(graph, start, end))//------------------调用第2题函数--------------------------
		cout << "按照广度优先搜索策略判断：" << start << "与" << end << "存在路径!" << endl;
	else
		cout << "按照广度优先搜索策略判断：" << start << "与" << end << "不存在路径!" << endl;
	//-----------------------------以下测试第3题------------------------------------------------
	cout << "请输入计算最短路径的起点名称（A）：";
	cin >> start;//输入：A
	int *path = new int[graph.GetVexNum()], *dist = new int[graph.GetVexNum()];
	ShortestPathDij(graph, graph.GetOrder(start), path, dist);//------------------调用第3题函数--------------------------
	//输出起点到所有顶点的最短路径和最短路径长度
	OutputShortestPathDij(graph, graph.GetOrder(start), path, dist);//-------------------调用OutputShortestPathDij函数输出结果，请自己实现
	delete[]path;
	delete[]dist;
*/
	system("pause");
}
