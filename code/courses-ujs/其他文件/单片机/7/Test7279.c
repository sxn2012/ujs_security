#include "c8051f020.h" 
#include <intrins.h>

sbit	HD7279_DAT=P1^7;
sbit	HD7279_CLK=P1^6;

#define NOSELECT7279  	P5 |= 0x80		//SPICS4(P57)=1
#define SELECT7279  	P5 &= ~(0x80)  	//SPICS4(P57)=0;
#define Set7279DAT  	HD7279_DAT=1
#define Clr7279DAT  	HD7279_DAT=0
#define Set7279CLK  	HD7279_CLK=1
#define Clr7279CLK  	HD7279_CLK=0

//      HD7279测试/演示程序             
 
//	I/O口初始化 

void Delay1ms(unsigned char T);
void Delay1s(unsigned char T);
void Delay1us(unsigned char T);

void Send7279Byte(unsigned char ch)
{	
	char i;
	SELECT7279;     	//置CS低电平 
	Delay1us(50);		//延时50μ
	
	for (i=0;i<8;i++)
	{	
		if (ch&0x80)	//输出7位到HD7279A的DATA端 
		{
			Set7279DAT;
		}
		else
		{
			Clr7279DAT;
		}
		Set7279CLK;		//置CLK高电平 
		ch=ch<<1;		//待发数据左移 
		Delay1us(8);	//延时8μ
		Clr7279CLK;		//置CLK低电平 
		Delay1us(8);	//延时50μ
	}
	Clr7279DAT;			//发送完毕，DATA端置低，返回 
}
unsigned char Receive7279Byte(void)
{
	unsigned char i,ch;
	ch=0;		
	Set7279DAT;			//DATA端置为高电平，输入状态
	Delay1us(50);		//延时50μ
	for (i=0;i<8;i++)
	{
		Set7279CLK;		//置CLK高电平
		Delay1us(8);	//延时8μ
		ch=ch<<1;		//接收数据左移1位
		if (HD7279_DAT)
			ch+=1;		//接收1位数据
		Clr7279CLK;		//置CLK低电平
		Delay1us(8);	//延时8μ
	}
	Clr7279DAT;			//接收完毕，DATA端重新置成低电平(输出状态)
	return ch;
}

void FlashLED(unsigned char No)
{
	char i;
	Send7279Byte(0x88);	//发闪烁指令 
	i=0x1;
	while (No)
	{
		i=i<<1;
		No--;
	}
	Send7279Byte(~i);   //1闪烁
	NOSELECT7279;     	//置CS高电平 
}

/*
void BlankLED(unsigned char ch)
{
	Send7279Byte(0x98);	//发消隐指令 
	Send7279Byte(ch);   //1-显示 0-消隐
	NOSELECT7279;     	//置CS高电平 
}
*/

void MoveLeft(void)
{
	Send7279Byte(0xA1);	//发左移指令 
	NOSELECT7279;     	//置CS高电平 
}

void MoveRight(void)
{
	Send7279Byte(0xA0);	//发右移指令 
	NOSELECT7279;     	//置CS高电平 
}

unsigned char code BdSeg[]={
					0x7e,0x30,0x6d,0x79, // 0 1 2 3 
                   	0x33,0x5b,0x5f,0x70, // 4 5 6 7
                   	0x7f,0x7b,0x77,0x1f, // 8 9 a b
                   	0x4e,0x3d,0x4f,0x47, // c d e f
                   	0x00,0x01}; 
/*
;              b6
;             ----
;          b1| b0 |b5
;             ----        small
;          b2| b3 |b4
;             ---- .b7
*/
void DispLED(char *DispBuf,char ShowDot)//ShowDot 显示小数点位 
{
char i,ch;
	ShowDot--;
	for (i=0;i<6;i++)
	{
		ch=DispBuf[i];
		if ((ch>='a') && (ch<='f'))
		{
		   ch-='a';ch+=0xa;
		}
		if ((ch>='A') && (ch<='F'))
		{
		   ch-='A';ch+=0xa;
		}
		Send7279Byte(0x90+5-i);	//不译码
		if (ch==' ')
			Send7279Byte(0x00); 
		else
			if (ch=='-')
			 	Send7279Byte(0x01); 
			else 
			{
				if (ShowDot==i)
					Send7279Byte(0x80|BdSeg[ch&0x0f]); 
				else
					Send7279Byte(BdSeg[ch&0x0f]); 
			}
	}
	NOSELECT7279;     	//置CS高电平 
}

char GetKeyValue(void)
{
	char KeyValue;
	if (CPT1CN&0x40) return -1;	//无键按下 
	Send7279Byte(0x15);	//发读键盘指令 
	KeyValue=Receive7279Byte();
	NOSELECT7279;     	//置CS高电平 
	return KeyValue; 
}

void WaitKeyOff(void)
{
	while  (!(CPT1CN&0x40));
}

void Test7279(bit LoopFlag)
{
	char i,KeyValue;
	Delay1ms(25);		//等待25ms复位时间 
	Send7279Byte(0xA4);	//发复位指令 
	NOSELECT7279;     	//置CS高电平 
	if (LoopFlag==0) return;
	DispLED("123456",0); 	//显示123456
	for (i=0;i<8;i++)
	{
		Delay1s(1);
		MoveLeft();
	}

	DispLED("123456",0); 	//显示123456
	DispLED("7890AB",2); 	//显示123456
	DispLED("CDEF -",3); 	//显示123456

	for (i=0;i<6;i++)
	{
		Delay1s(1);
		MoveRight();
	}

	DispLED("F    1",1); 	//显示123456

	FlashLED(0);	//第一位闪烁
	Delay1s(1);
	FlashLED(1);	//第二位闪烁
	Delay1s(1);
	FlashLED(8);	//关闪烁

	//BlankLED(0x23); //注意:执行消隐后,键盘输入中断口不能恢复.

	for (;;)
	{	
		KeyValue=GetKeyValue();
         if(KeyValue!=-1)
          {
		    Send7279Byte(0xC8);	//发送键码值，按方式1译码下载显示 
		    Send7279Byte(KeyValue%16);
		    NOSELECT7279;     	//置CS高电平 
		    WaitKeyOff();
           }
	}
}
