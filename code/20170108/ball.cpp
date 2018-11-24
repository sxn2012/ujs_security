// ball.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <graphics.h>
#include <conio.h>
#include <stdio.h>
#include <time.h>
void main()
{
	initgraph(640, 480);
	IMAGE img;
	loadimage(&img,"C:\\Users\\sxn\\Pictures\\desert.jpg");
	putimage(0,0,&img);
	int x=300,y=200;
	char c=0,d=0;
	int a=0,b=0;
	int n=1;
	while (c!=27)
	{
		
		BeginBatchDraw();
		// 绘制黄线、绿色填充的圆
		setlinecolor(YELLOW);
		setfillcolor(GREEN);
		fillcircle(x, y, 15);
		// 延时
		Sleep(12);
		FlushBatchDraw();
		if (kbhit())
		c=getch();
		if (c==0)
		c=getch();
		setwritemode(R2_XORPEN);
		setlinecolor(YELLOW);
		setfillcolor(GREEN);
		fillcircle(x, y, 15);
		if (d==0)
		{
			loadimage(&img,"C:\\Users\\sxn\\Pictures\\desert.jpg");
			putimage(0,0,&img);
		}
		switch (c)
		{
		case 'w':case 72:y-=2;d=c;break;
		case 'a':case 75:x-=2;d=c;break;
		case 's':case 80:y+=2;d=c;break;
		case 'd':case 77:x+=2;d=c;break;
		case 27:break;
		default:c=d;continue;
		}
		//srand ((unsigned)time(NULL));
		
		while(((x-a)*(x-a)+(y-b)*(y-b)<=625)||(n==1))
		{
		if (n!=1)
		{
		setwritemode(R2_XORPEN);
		setfillcolor(RED);
		fillcircle(a, b, 10);
		}
		do
		{
		a=rand()%640;
		}
		while ((a<=10)||(a>=630));
		do
		{
		b=rand()%480;
		}
		while ((b<=10)||(b>=470));
		//Sleep(20);
		setwritemode(R2_XORPEN);
		setfillcolor(RED);
		fillcircle(a, b, 10);
		n++;
		}
		if (x<=20) x=620;
		else if (x>=620) x=20;
		if (y<=20) y=460;
		else if (y>=460) y=20;
	}
	EndBatchDraw();
	closegraph();
}