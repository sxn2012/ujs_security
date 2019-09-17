// 1.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <math.h>
#include <time.h>
using namespace std;

int gmod(int a,int b)           //求两个整数模的逆元  
{  
    int s0=1,s1=0,t0=0,t1=1;        //对s1，s0，t1，t0进行初始化  
    int bb=b;                       //保留b的初值  
    if(a==1)  
        return 1;  
    if(a%b==0)  
        return 0;                   //若a，b不互素，直接返回0  
    int q,r,s,t;                    //声明辗转反除法需要用到的整数  
    while(b%a!=0)                   //直到a%b等于1才结束while  
    {  
        q=b/a;  
        r=b%a;  
  
        b=a;  
        a=r;  
  
        s=s0-q*s1;  
        s0=s1;  
        s1=s;  
  
        t=t0-q*t1;  
        t0=t1;  
        t1=t;  
    }  
    if(t>0)  
        return t;  
    else  
        return t+bb;  
}  


bool IsPrime(int n)
{
	if (n<=1)
	{
		return false;
	}
	for (int i=2;i<=sqrt(n);i++)
		if (n%i==0)
			return false;
	return true;
}
bool Judge(int a,int b)
{
	if(a<=1) return false;
	for(int i=2;i<=a;i++)
		if (a%i==0&&b%i==0)
		{
			return false;
		}
	return true;
}



int main(int argc, char* argv[])
{
	srand((unsigned)time(NULL));
	int p=0,q=0;
	while (!IsPrime(p))
	{
		p=rand()%10;
	}
	while(!IsPrime(q)&&q!=p)
	{
		q=rand()%10;
	}
	cout<<"p="<<p<<endl<<"q="<<q<<endl;
	int n=p*q;
	int l=(p-1)*(q-1);
	cout<<"n="<<n<<endl<<"phi(n)="<<l<<endl;
	unsigned int e=0;
	do 
	{
		e=rand()%15;
	} 
	while (!Judge(e,l));

	unsigned int d=0;


	d=gmod(e,l);
	cout<<"e="<<e<<endl<<"d="<<d<<endl;
	int m[]={3,1,5,0,6,0,4,0,2,8};
	int c[10];
	cout<<"m=";
	for (int i=0;i<10;i++)
	{
		cout<<m[i];
	}
	cout<<endl;
	cout<<"c=";
	for ( i=0;i<10;i++)
	{
		c[i]=int(pow(m[i],e))%n;
		cout<<c[i];
	}
	cout<<endl;
	int o[10];
	for(i=0;i<10;i++)
	{
		o[i]=int(pow(c[i],d))%n;
		cout<<o[i];
	}
	cout<<endl;
	return 0;
}
