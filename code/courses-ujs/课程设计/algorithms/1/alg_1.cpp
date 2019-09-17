// alg_1.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <graphics.h>
#include <conio.h>
#include <iostream>
#include <algorithm>
#include <cmath>
#include <cstdio>
#include <cstdlib>
#include <cstring>
#include <ctime>
#include <ctype.h>
#include <iostream>
#include <map>
#include <queue>
#include <set>
#include <stack>
#include <string>
#include <vector>
#define eps 1e-8 //精度
#define INF 0x7fffffff //无穷大
#define PI acos(-1.0) //精确值
#define seed 31 //种子
#define maxn 100005 //n的最大值
#define min(X,Y) X<Y?X:Y //两个数的最小值
#define min3(X,Y,Z) X<(Y<Z?Y:Z)?X:(Y<Z?Y:Z) //三个数的最小值
typedef long LL;
typedef unsigned long ULL;
using namespace std;

//分治算法求二维最近点对
struct Point
{
  double x,y;
}p[maxn];//定义点的结构体

int a[maxn];
int minpix1=-1,minpix2=-1;
inline int cmpx(Point a,Point b)//比较x坐标
{
  return a.x<b.x;
}
inline int cmpy(int a,int b)//比较y坐标
{
  return p[a].y<p[b].y;
}
inline double dis(Point a,Point b)//计算两点间距
{
   return sqrt((a.x-b.x)*(a.x-b.x)+(a.y-b.y)*(a.y-b.y));
}

double closest(int low,int high)//计算一系列点间距的最小值
{
   int i,j,k;
   int mid3;
   if(low+1==high) //只有两个点
   { 
	minpix1=low;
	minpix2=high;
    return dis(p[low],p[high]);
   }
   if(low+2==high)//只有三个点
   { 
	 mid3=low+1;
	 double dis1=dis(p[low],p[high]);
	 double dis2=dis(p[low],p[mid3]);
	 double dis3=dis(p[mid3],p[high]);
	 if(dis1<=dis2&&dis1<=dis3)
	 {
		 minpix1=low;
		 minpix2=high;
	 }
	 else if(dis2<=dis1&&dis2<=dis3)
	 {
		 minpix1=low;
		 minpix2=mid3;
	 }
	 else
	 {
		 minpix1=mid3;
		 minpix2=high;
	 }
     return min3(dis(p[low],p[high]),dis(p[low],p[mid3]),dis(p[mid3],p[high]));
   }
   int mid=(low+high)/2; //求中点即左右子集的分界线
   double d=min(closest(low,mid),closest(mid+1,high));//左边和右边的最小点间距
   for(i=low,k=0;i<=high;i++)//把x坐标在p[mid].x-d  ~  p[mid].x+d范围内的点筛选出来
   { 
     if(p[i].x>=p[mid].x-d&&p[i].x<=p[mid].x+d){
        a[k++]=i; //保存这些点的下标索引
     }
   }
   sort(a,a+k,cmpy); //按y坐标进行升序排序
   for(i=0;i<k;i++)
   {
    for(j=i+1;j<k;j++)
	{
       if(p[a[j]].y-p[a[i]].y>=d) //注意下标索引
            break;
	   if(dis(p[a[i]],p[a[j]])<d)
	   {
		   minpix1=a[i];
		   minpix2=a[j];
	   }
       d=min(d,dis(p[a[i]],p[a[j]]));
    }
   }
   return d;
}


int main(int argc, char* argv[])
{
	int i,n;
	cout<<"请输入点的个数:";
	cin>>n;
	cout<<"请分别输入点的坐标（x，y）,注意 -400<x<400 -300<y<300 "<<endl;
	for(i=0;i<n;i++)
	{
		
		do
		{
			cout<<"第"<<i+1<<"个点：";
			cin>>p[i].x>>p[i].y;
		}
		while(p[i].x<-400||p[i].x>400||p[i].y<-300||p[i].y>300);
	}
	sort(p , p + n , cmpx);//按x坐标进行升序排序
   
	cout<<"按任意键开始计算最接近点间距......";
	getch();
	double mindist=closest(0,n-1);
	char buffer[20];
	sprintf(buffer,"%lf",mindist);
	char noti[50]="最短距离：";
	// 初始化图形模式
	initgraph(800, 600);
	setbkcolor(0x005478);//设置背景色
	cleardevice();//清屏
	setcolor(WHITE);
	line(0,300,800,300);
	line(400,0,400,600);
	outtextxy(410,280,'O');
	outtextxy(780,280,'x');
	outtextxy(410,5,'y');
	BeginBatchDraw();
	setfillcolor(RED);
	setlinecolor(RED);
	for(i=0;i<n;i++)
		fillcircle(p[i].x+400,-p[i].y+300,2);
	setcolor(BLUE);
	line(p[minpix1].x+400,-p[minpix1].y+300,p[minpix2].x+400,-p[minpix2].y+300);
	setcolor(WHITE);
	outtextxy((p[minpix1].x+400+p[minpix2].x+400)/2+20,(-p[minpix1].y+300-p[minpix2].y+300)/2,noti);
	outtextxy((p[minpix1].x+400+p[minpix2].x+400)/2+20,(-p[minpix1].y+300-p[minpix2].y+300)/2+20,buffer);
	FlushBatchDraw();
	EndBatchDraw();
	while(!kbhit());//按任意键退出
	closegraph();
	return 0;
}
