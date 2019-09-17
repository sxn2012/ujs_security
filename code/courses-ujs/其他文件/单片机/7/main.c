#include "c8051f020.h"                
#include <intrins.h>

sfr16 DAC0  = 0xd2;                 // DAC0 data
#define PRT0CF P0MDOUT
#define PRT1CF P1MDOUT
#define PRT2CF P2MDOUT
//#define SetSpeed 30
// 按0 减速2，按1加速2

unsigned int Count1s=1000;
unsigned int SaveMotorCount,MotorCount=0;
unsigned int iDAC0=2048;
unsigned int SetSpeed=30;
int SD;
unsigned char Count1ms;
char GetKeyValue(void);

void Test7279(bit LoopFlag);
void DispLED(char *DispBuf,char ShowDot);	//ShowDot 显示小数点位 
void TestMotor(void);





void Delay1us(unsigned char us)
{
	while (us)
	{
	  _nop_(); _nop_(); _nop_(); _nop_(); _nop_();
	  --us;
	}
}

void SYSCLK_Init (void)
{
   int i;                              // delay counter
   OSCXCN = 0x67;                      // start external oscillator with
                                       // 18.432MHz crystal
   for (i=0; i < 256; i++) ;           // Wait for osc. to start up
   while (!(OSCXCN & 0x80)) ;          // Wait for crystal osc. to settle
   OSCICN = 0x88;                      // select external oscillator as SYSCLK
                                       // source and enable missing clock
                                       // detector
//	OSCICN = 0x07;   //interal 16MHZ
}


void PORT_Init (void)
{
   XBR0    = 0x07;                     // Enable SMBus, SPI0, and UART0
   XBR1    = 0x04;					   //P10<---int0
   XBR2    = 0x40;                     // Enable crossbar and weak pull-ups
   EMI0CF  = 0x27;
   EMI0TC  = 0x21;
   P74OUT  = 0xFF;
   P0MDOUT = 0x15;

   P1MDOUT |= 0x3C;  					//P1.2-P1.5推挽输出
   P1 &= 0xc3;							//P1.2-P1.5=0
}

void SPI0_Init (void)
{
   SPI0CFG = 0x07;                     // data sampled on 1st SCK rising edge
                                       // 8-bit data words
   SPI0CFG|=0xC0;	//CKPOL =1;

   SPI0CN = 0x03;                      // Master mode; SPI enabled; flags
                                       // cleared
   SPI0CKR = SYSCLK/2/8000000-1;       // SPI clock <= 8MHz (limited by 
                                       // EEPROM spec.)
}

void Timer0_Init (void)
{
   	CKCON|=0x8;
   	TMOD|=0x1;    	//16Bit
	Count1ms=10;
   	TR0 = 0;                         	// STOP Timer0
   	TH0 = (-SYSCLK/1000) >> 8;    		// set Timer0 to overflow in 1ms
   	TL0 = -SYSCLK/1000;
   	TR0 = 1;   	// START Timer0
   	IE|= 0x2; 
}


void Timer0_ISR (void) interrupt 1  //1ms
{
	TH0 = (-SYSCLK/1000) >> 8;  
   	TL0 = -SYSCLK/1000;
	if (Count1ms) Count1ms--;
	if (Count1s) Count1s--;
	else
	{
		Count1s=1000;
        SaveMotorCount=MotorCount;
		MotorCount=0;
		SD=SaveMotorCount/2-SetSpeed;
		SaveMotorCount/=2;
		if (SD)
//此处算法比较简单，其增量控制可以采用PID算法实现。
		{
			if ((SD>5)||(SD<-5))
			iDAC0-=SD*4;
			else
				iDAC0-=SD;
							
		    	DAC0=iDAC0;
		     
			}
	}
}

void Int0_ISR (void) interrupt 0  
{
	MotorCount++;
}

void Delay1ms(unsigned char T)
{
	Count1ms=T;
	while (Count1ms);
}

void Delay1s(unsigned char T)
{
	while (T)
	{
		Delay1ms(200);
		Delay1ms(200);
		Delay1ms(200);
		Delay1ms(200);
		Delay1ms(200);
		T--;
	}
}

void main (void) 
{
    int kp;
	WDTCN = 0xde;
	WDTCN = 0xad;           //关看门狗
	SYSCLK_Init ();         //初始化时钟
	Timer0_Init();			//初始化定时器
	PORT_Init ();           //初始化IO口
	SPI0_Init ();           //初始化SPI0
	CPT1CN|=0x80;			//使能比较器1
	REF0CN = 0x07;  		//使能片内参考电压
	DAC0CN |= 0x80;			//使能DAC0
	DAC0H=0;	DAC0L=0;

	IT0=1;
	IE |= 0x01; 	//使能INT0中断
	EA=1;			//开中断

	Test7279(0);
        DAC0=iDAC0;
	for(;;)
	 {
          TestMotor();
          kp= GetKeyValue();
	  if (kp == 0)
	     SetSpeed = SetSpeed - 2;
	  if ((kp == 1) & (SetSpeed<=38))
	     SetSpeed = SetSpeed + 2;
         }
	     
}

