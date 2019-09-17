#include "c8051f020.h"                
#include <intrins.h>

/*
	直流电机试验

	试验准备:用连接线将JH端子的CKMOT与P10（INT0）
	运行此程序
*/

sfr16 DAC0     = 0xd2;                 // DAC0 data
extern unsigned int SaveMotorCount;

void Delay1ms(unsigned char T);
void DispLED(char *DispBuf,char ShowDot);	//ShowDot 显示小数点位 

extern unsigned int iDAC0;

void TestMotor(void)
{
char buf[6];
//	DAC0=iDAC0;
//	for (;;)
//	{
			Delay1ms(100);
			buf[0]='F';
			buf[1]=' ';
			buf[2]=(SaveMotorCount%10000)/1000;
			buf[3]=(SaveMotorCount%1000)/100;
			buf[4]=(SaveMotorCount%100)/10;
			buf[5]=(SaveMotorCount%10);
			DispLED(buf,0);			  	

//	}
}
