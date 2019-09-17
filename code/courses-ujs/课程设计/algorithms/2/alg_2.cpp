// alg_2.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <vector>
using namespace std;
#define max_int 0x7FFFFFFF

//Prim算法实现
void prim_alg()
{
    int n;
	cout<<"请输入结点数目：";
    cin>>n;
    vector<vector<int> > a(n, vector<int>(n));
	cout<<"请输入邻接矩阵（"<<n<<"*"<<n<<"），注：无路径请输入-1"<<endl;
    for(int i = 0; i < n ; i++) 
	{
        for(int j = 0; j < n; j++) 
		{
            cin>>a[i][j];
			if(a[i][j]<0) a[i][j]=max_int;
        }
    }
	cout<<"最小生成树生成中......"<<endl;
	cout<<"(";
	int flag=1;
    int pos=0, minimum;
    int min_tree = 0;
    //lowcost数组记录每2个点间最小权值，visited数组标记某点是否已访问
    vector<int> visited, lowcost;
    for ( i = 0; i < n; i++) 
	{
        visited.push_back(0);    //初始化为0，表示都没加入
    }
    visited[0] = 1;   //最小生成树从第一个顶点开始
    for ( i = 0; i < n; i++) 
	{
        lowcost.push_back(a[0][i]);    //权值初始化为0
    }
	
    for ( i = 0; i < n; i++) //枚举n个顶点
	{    
		
        minimum = max_int;
        for (int j = 0; j < n; j++) //找到最小权边对应顶点
		{    
            if(!visited[j] && minimum > lowcost[j]) 
			{
                minimum = lowcost[j];
				
			
                pos = j;
            }
        }
	
        if (minimum == max_int)    //如果min = max_int表示已经不再有点可以加入最小生成树中
            break;
        min_tree += minimum;
		if(flag) cout<<minimum;
		else cout<<","<<minimum;
		flag=0;
        visited[pos] = 1;     //加入最小生成树中
        for ( j = 0; j < n; j++) 
		{
            if(!visited[j] && lowcost[j] > a[pos][j]) lowcost[j] = a[pos][j];   //更新可更新边的权值
        }
    }
	cout<<")"<<endl;
	cout<<"最小生成树生成完毕，权值为：";
    cout<<min_tree<<endl;
	
}


int main(int argc, char* argv[])
{
	prim_alg();
	return 0;
}
