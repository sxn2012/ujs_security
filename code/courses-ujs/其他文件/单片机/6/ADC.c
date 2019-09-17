#include "c8051F020.h"
#include <intrins.h>

/*
	压力应变片ADC试验

	运行此程序,压力值将在数码管上显示.用手触摸应变片
	观察压力变化.

*/

#define MUX_TEMP			0x08
#define MUX_VOLT			0x01
#define MUX_PRESS			0x02

unsigned int idata  Temp, Press, Volt;
unsigned char  idata mux_select;

sfr16 ADC0     = 0xbe;                 // ADC0 data

void init_adc(void)
{
	ADC0CN = 0x81;                      // ADC0 enabled; normal tracking
     	                                // mode; ADC0 conversions are initiated 
                                       	// on write to AD0BUSY; ADC0 data is
                                       	// left-justified
	REF0CN = 0x07;                      // enable temp sensor, on-chip VREF,
                                       	// and VREF output buffer
	mux_select = MUX_TEMP;	  			// CPU on-chip temp sensor
	AMX0SL = MUX_TEMP; 	

	ADC0CF = (SYSCLK/2500000) << 3;     // ADC conversion clock = 2.5MHz
	//ADC0CF |= 0x01;                     // PGA gain = 2
	EIE2 &= ~0x02;                      // disable ADC0 EOC interrupt
	EIE1 &= ~0x04;                      // disable ADC0 window compare interrupt
}

//  On-chip temperature
//  AN1. 电位器
//  AN2. 应变片

void read_analog_inputs(void)
{
	long temp_long;
    AD0INT = 0;                      // clear conversion complete indicator
    AD0BUSY = 1;                     // initiate conversion
    while (AD0INT == 0);             // wait for conversion complete

	switch (mux_select)
	{
		case MUX_TEMP:
	      	temp_long = ADC0 - 42380/2;
		    temp_long = (temp_long * 200L) / 156;
			Temp=temp_long;
			AMX0SL = MUX_VOLT;		// Select AIN1 for next read
			mux_select = MUX_VOLT;
		break;

	   	case MUX_VOLT:
			temp_long = ADC0;
			Volt = 24*temp_long/655;
			AMX0SL = MUX_PRESS;		// Select on-chip temp sensor
			mux_select = MUX_PRESS;
		break;
    	
		case MUX_PRESS:
			temp_long = ADC0;
			temp_long = 24*temp_long/655;
			Press = temp_long;
      		AMX0SL = MUX_TEMP;	 
      		mux_select = MUX_TEMP;
		break;
		default:
		AMX0SL = MUX_TEMP;
		mux_select = MUX_TEMP;
		break;
  	}
}
 
unsigned int GetADCValue(char No)
{
	read_analog_inputs();
	read_analog_inputs();
	read_analog_inputs();
	switch (No)
	{
		case 1:
			return Temp;
		case 2:
			return Volt;
		case 3:
			if (Press<10) Press=0;
			return Press;
	}
}


