// XX.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <graphics.h>
#include <conio.h>
#include <time.h>
#include <stdio.h>
#include <iostream>
using namespace std;
#include <math.h>

#define PI 3.14159265359

void Draw(int hour, int minute, int second)
{
	double a_hour, a_min, a_sec;					// ʱ���֡�����Ļ���ֵ
	int x_hour, y_hour, x_min, y_min, x_sec, y_sec;	// ʱ���֡������ĩ��λ��
	
	// ����ʱ���֡�����Ļ���ֵ
	a_sec = second * 2 * PI / 60;
	a_min = minute * 2 * PI / 60 + a_sec / 60;
	a_hour= hour * 2 * PI / 12 + a_min / 12;
	
	// ����ʱ���֡������ĩ��λ��
	x_sec = 320 + (int)(120 * sin(a_sec));
	y_sec = 240 - (int)(120 * cos(a_sec));
	x_min = 320 + (int)(100 * sin(a_min));
	y_min = 240 - (int)(100 * cos(a_min));
	x_hour= 320 + (int)(70 * sin(a_hour));
	y_hour= 240 - (int)(70 * cos(a_hour));

	// ��ʱ��
	setlinestyle(PS_SOLID, 10, NULL);
	setlinecolor(WHITE);
	line(320, 240, x_hour, y_hour);

	// ������
	setlinestyle(PS_SOLID, 6, NULL);
	setlinecolor(LIGHTGRAY);
	line(320, 240, x_min, y_min);

	// ������
	setlinestyle(PS_SOLID, 2, NULL);
	setlinecolor(RED);
	line(320, 240, x_sec, y_sec);
}
void main()
{
	initgraph(640, 480);		// ��ʼ�� 640 x 480 �Ļ�ͼ����

	// ����һ���򵥵ı���
	circle(320, 240, 2);
	circle(320, 240, 60);
	circle(320, 240, 160);
	moveto(320, 85);
	outtext("12");
	moveto(320,380);
	outtext("6");
	outtextxy(296, 310, _T("BestAns"));

	// ���� XOR ��ͼģʽ
	setwritemode(R2_XORPEN);	// ���� XOR ��ͼģʽ

	// ���Ʊ���
	SYSTEMTIME ti;				// ����������浱ǰʱ��
	while(!kbhit())				// ��������˳��ӱ�����
	{
		GetLocalTime(&ti);		// ��ȡ��ǰʱ��
		Draw(ti.wHour, ti.wMinute, ti.wSecond);	// ������
		Sleep(1000);							// ��ʱ 1 ��
		Draw(ti.wHour, ti.wMinute, ti.wSecond);	// �����루������ͻ�����Ĺ�����һ���ģ�
	}

	closegraph();				// �رջ�ͼ����
}