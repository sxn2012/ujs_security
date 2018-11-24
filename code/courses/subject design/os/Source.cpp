/*
文件名：Source.cpp
工程名：Server_Station.sln
*/

#pragma once
#pragma warning(disable : 4996)
#ifndef _CRT_SECURE_NO_WARNINGS
#define _CRT_SECURE_NO_WARNINGS
#ifndef _AFXDLL
#define _AFXDLL
#ifndef _WIN32_WINNT_MAXVER
#define _WIN32_WINNT_MAXVER
#include <iostream>
#include <conio.h>
//#include <atltime.h>
#include <afxmt.h>
#include <windows.h>
#include <time.h>

//用宏的方式定义常量，
//以便调试时修改方便。
//在实际使用时，
//应把常量修改为题目所要求的数目。
#define MAX_NUM_CUSTOMER 5000//顾客的最大数量
#define CONTAIN_NUM_HALL 200//大厅所能容纳的最大人数
#define MAX_NUM_EACHQUEUE 200//每个队伍的最大排队人数


//定义宏函数，
//使得代码清晰易懂。
#define P_OPERATION(semaphore) WaitForSingleObject(semaphore,INFINITE)//P操作定义
//P操作这个函数有两个参数
//如果直接使用书写起来比较麻烦
//并且第二个参数对于任何信号量而言都是确定不改变的
//所以可以把它定义为宏函数，同时把第二个参数固定为INFINITE
//这样使用P操作代码就显得简洁
#define V_OPERATION(semaphore) ReleaseSemaphore(semaphore,1,NULL)//V操作定义
//V操作这个函数有三个参数
//其中第二个和第三个参数是固定的
//不论对什么信号量进行V操作
//所不同的只是第一个参数
//所以可以把第二个参数固定为1，第三个参数固定为NULL
#define _Time_(t) GetLocalTime(t)//获取当前时间
//获取当前时间这个函数
//函数名过长，不便于书写
//可以把它定义为宏，改短一点
//有利于提升代码的清晰度
#define WAIT_MS(t) Sleep(t)//等候一段时间（毫秒为单位）
#define WAIT_S(t) Sleep(t*1000)//等候一段时间（秒为单位）
//这两个用于等候一段时间的函数
//实际上是调用的同一个函数
//唯一的区别就是参数不同
//在实际应用中
//我们有时候用毫秒计算方便，有时候用秒计算方便
//所以定义了两个宏函数
#define Err_Code() GetLastError()//返回错误代码
//这个函数函数名过长
//可以把它定义为宏，改短一点
//有利于提升代码的清晰度
#define Sem_Create(INI_,MAX_) CreateSemaphore(NULL,INI_,MAX_,NULL)//创建信号量系统调用
//创建信号量这个函数
//一方面函数名太长，不便于书写
//另一方面四个参数中有两个参数是固定的
//所以把它定义为宏函数更为方便
//有利于后面的使用
#define Until_Terminate_All_Threads(NUM,THREAD) WaitForMultipleObjects(NUM,THREAD,TRUE,INFINITE)
//等待多个线程结束
//这个函数四个参数中只有两个是用到的
//另外两个是固定的
//把它定义为宏函数更加简洁
//调用更加方便
#endif
#endif
#endif
using namespace std;//使用命名空间
//类型定义
//使类型名更为直观
typedef struct ID_STRUCT//ID
{
	int num;
}DATA_CUST_ID,*DATA_POINTER_ID;//存储顾客编号的结构体
typedef SYSTEMTIME Current_Time;//当前时间
typedef CCriticalSection Mutex_District;//临界区

Current_Time Time = { 0 };//时间结构体
//为获取当前系统时间做准备
//以后要获取系统时间时，
//只要调用GetLocalTime(&Time)这个函数即可。

/*定义临界区*/
Mutex_District s_enter, //入口
s_exit,//出口
s_output,//输出互斥
//比较队伍时的互斥
s_mashine,
s_person,
//队列的互斥信号
mutex_change,//改签/退票窗口
mutex_buy1,//1号人工窗口
mutex_buy2,//2号人工窗口
mutex_mashine1,//1号机器
mutex_mashine2,//2号机器
mutex_mashine3,//3号机器
//机器互斥使用
s_mashine1,//1
s_mashine2,//2
s_mashine3;//3





/*定义信号量*/
HANDLE s,//售票大厅内剩余空位
//等候顾客的营业员
s_serverchange, //改签/退票窗口
s_serverbuy1,//1号人工窗口
s_serverbuy2,//2号人工窗口
s_servermashine1,//1号机器
s_servermashine2,//2号机器
s_servermashine3,//3号机器
//等候营业员的顾客
s_custchange,//改签/退票窗口
s_custbuy1,//1号人工窗口
s_custbuy2,//2号人工窗口
s_custmashine1,//1号机器
s_custmashine2,//2号机器
s_custmashine3;//3号机器



/*定义计数器*/
INT person_sum_number = 0,//大厅内的人数
waiting_in_number = 0,//等待进入的人数
waiting_out_number = 0,//等待退出的人数
waiting_person1_count = 0,//等待1号人工窗口的人数
waiting_person2_count = 0,//等待2号人工窗口的人数
waiting_mashine1_count = 0,//等待1号机器的人数
waiting_mashine2_count = 0,//等待2号机器的人数
waiting_mashine3_count = 0,//等待3号机器的人数
waiting_change_refund_count = 0;//等待改签/退票窗口的人数



inline WORD Get_min_NUM(WORD a, WORD b, WORD c)//求3个数最小值
{
	return (a > b) ? min(b, c) : min(a, c);
}

UINT Init_all_Semaphore()
{
	/*初始化信号量*/
	s = Sem_Create( CONTAIN_NUM_HALL, CONTAIN_NUM_HALL);//创建信号量
	if (!s)
	{
		cerr << "信号量创建失败:" << Err_Code() << endl;//异常处理
		ExitProcess(1);
	}
	s_serverchange = Sem_Create( 0, 1);//创建信号量
	if (!s_serverchange)
	{
		cerr << "信号量创建失败:" << Err_Code() << endl;//异常处理
		ExitProcess(1);
	}
	s_serverbuy1 = Sem_Create( 0, 1);//创建信号量
	if (!s_serverbuy1)
	{
		cerr << "信号量创建失败:" << Err_Code() << endl;//异常处理
		ExitProcess(1);
	}
	s_serverbuy2 = Sem_Create( 0, 1);//创建信号量
	if (!s_serverbuy2)
	{
		cerr << "信号量创建失败:" << Err_Code() << endl;//异常处理
		ExitProcess(1);
	}
	s_servermashine1 = Sem_Create( 0, 1);//创建信号量
	if (!s_servermashine1)
	{
		cerr << "信号量创建失败:" << Err_Code() << endl;//异常处理
		ExitProcess(1);
	}
	s_servermashine2 = Sem_Create( 0, 1);//创建信号量
	if (!s_servermashine2)
	{
		cerr << "信号量创建失败:" << Err_Code() << endl;//异常处理
		ExitProcess(1);
	}
	s_servermashine3 = Sem_Create( 0, 1);//创建信号量
	if (!s_servermashine3)
	{
		cerr << "信号量创建失败:" << Err_Code() << endl;//异常处理
		ExitProcess(1);
	}
	s_custchange = Sem_Create( 0, CONTAIN_NUM_HALL);//创建信号量
	if (!s_custchange)
	{
		cerr << "信号量创建失败:" << Err_Code() << endl;//异常处理
		ExitProcess(1);
	}
	s_custbuy1 = Sem_Create( 0, CONTAIN_NUM_HALL);//创建信号量
	if (!s_custbuy1)
	{
		cerr << "信号量创建失败:" << Err_Code() << endl;//异常处理
		ExitProcess(1);
	}
	s_custbuy2 = Sem_Create( 0, CONTAIN_NUM_HALL);//创建信号量
	if (!s_custbuy2)
	{
		cerr << "信号量创建失败:" << Err_Code() << endl;//异常处理
		ExitProcess(1);
	}
	s_custmashine1 = Sem_Create( 0, CONTAIN_NUM_HALL);//创建信号量
	if (!s_custmashine1)
	{
		cerr << "信号量创建失败:" << Err_Code() << endl;//异常处理
		ExitProcess(1);
	}
	s_custmashine2 = Sem_Create( 0, CONTAIN_NUM_HALL);//创建信号量
	if (!s_custmashine2)
	{
		cerr << "信号量创建失败:" << Err_Code() << endl;//异常处理
		ExitProcess(1);
	}
	s_custmashine3 = Sem_Create( 0, CONTAIN_NUM_HALL);//创建信号量
	if (!s_custmashine3)
	{
		cerr << "信号量创建失败:" << Err_Code() << endl;//异常处理
		ExitProcess(1);
	}


	return 0;
}


UINT Change_Method(INT m = 0,INT id = 0)
{
	mutex_change.Lock();//进入临界区
	waiting_change_refund_count++;//排队人数+1
	_Time_(&Time);//获得当前本地时间
	
	V_OPERATION(s_custchange);
	mutex_change.Unlock();//退出临界区
	//互斥输出
	s_output.Lock();
	cout << "当前时间：" << Time.wHour << ":" << Time.wMinute << ":" << Time.wSecond << "\t";
	cout << "顾客" << id << "开始排队（改签/退票窗口）" //<< endl;
		<< " ";
	cout << "当前等待人数：" << waiting_change_refund_count << endl;
	s_output.Unlock();
	P_OPERATION(s_serverchange);
	_Time_(&Time);//获得当前本地时间
	s_output.Lock();
	cout << "当前时间：" << Time.wHour << ":" << Time.wMinute << ":" << Time.wSecond << "\t";
	if (m==0)
		cout << "顾客" << id << "开始改签" << endl;
	else if (m==1)
		cout << "顾客" << id << "开始退票" << endl;
	s_output.Unlock();
	//开始改签/退票过程
	//这个过程需要一段时间
	//设置为35秒
	//因此，需要等候35秒后再继续运行
	WAIT_S(35);
	
	
	
	
			

	return 0;
}

UINT Mashine_Selection(INT id = 0)
{
	s_mashine.Lock();//进入临界区
	INT i = Get_min_NUM(waiting_mashine1_count, waiting_mashine2_count, waiting_mashine3_count);//三个中的最小值
	INT a = waiting_mashine1_count;
	INT b = waiting_mashine2_count;
	INT c = waiting_mashine3_count;
	if (i==a)//第一个最小
	{
		s_mashine.Unlock();
		mutex_mashine1.Lock();
		waiting_mashine1_count++;
		_Time_(&Time);//获得当前本地时间
		s_output.Lock();
		cout << "当前时间：" << Time.wHour << ":" << Time.wMinute << ":" << Time.wSecond << "\t";
		cout << "顾客" << id << "开始排队（1号机器）" << " ";
		cout << "当前等待人数:" << waiting_mashine1_count - 1 << endl;
		s_output.Unlock();
		
		mutex_mashine1.Unlock();
	
		_Time_(&Time);//获得当前本地时间
		s_mashine1.Lock();
		s_output.Lock();
		cout << "当前时间：" << Time.wHour << ":" << Time.wMinute << ":" << Time.wSecond << "\t";
		cout << "顾客" << id << "开始在1号机器买票" << endl;
		s_output.Unlock();
		
		WAIT_S(35);
		s_mashine1.Unlock();
		

		mutex_mashine1.Lock();
		waiting_mashine1_count--;
		mutex_mashine1.Unlock();
		
	}
	else if (i==b)//第二个最小
	{
		s_mashine.Unlock();
		mutex_mashine2.Lock();
		waiting_mashine2_count++;
		_Time_(&Time);//获得当前本地时间
		s_output.Lock();
		cout << "当前时间：" << Time.wHour << ":" << Time.wMinute << ":" << Time.wSecond << "\t";
		cout << "顾客" << id << "开始排队（2号机器）" << " ";
		cout << "当前等待人数:" << waiting_mashine2_count - 1<< endl;
		s_output.Unlock();
	
		mutex_mashine2.Unlock();
		
		_Time_(&Time);//获得当前本地时间
		s_mashine2.Lock();
		s_output.Lock();
		cout << "当前时间：" << Time.wHour << ":" << Time.wMinute << ":" << Time.wSecond << "\t";
		cout << "顾客" << id << "开始在2号机器买票" << endl;
		s_output.Unlock();
		
		WAIT_S(35);
		s_mashine2.Unlock();
		

		mutex_mashine2.Lock();
		waiting_mashine2_count--;
		mutex_mashine2.Unlock();
		
	}
	else //第三个最小
	{
		s_mashine.Unlock();
		mutex_mashine3.Lock();
		waiting_mashine3_count++;
		_Time_(&Time);//获得当前本地时间
		s_output.Lock();
		cout << "当前时间：" << Time.wHour << ":" << Time.wMinute << ":" << Time.wSecond << "\t";
		cout << "顾客" << id << "开始排队（3号机器）" << " ";
		cout << "当前等待人数:" << waiting_mashine3_count - 1<< endl;
		s_output.Unlock();
		
		mutex_mashine3.Unlock();
		
		_Time_(&Time);//获得当前本地时间
		s_mashine3.Lock();
		s_output.Lock();
		cout << "当前时间：" << Time.wHour << ":" << Time.wMinute << ":" << Time.wSecond << "\t";
		cout << "顾客" << id << "开始在3号机器买票" << endl;
		s_output.Unlock();
		
		WAIT_S(35);
		s_mashine3.Unlock();

		mutex_mashine3.Lock();
		waiting_mashine3_count--;
		mutex_mashine3.Unlock();
		
	}

	return 0;
}

UINT Person_Selection(INT id = 0)
{
	s_person.Lock();//进入临界区
	
	INT i = min(waiting_person1_count, waiting_person2_count);//两个中的最小值
	INT a = waiting_person1_count;
	INT b = waiting_person2_count;
	if (i==a)//第一个最小
	{
		s_person.Unlock();
		
		mutex_buy1.Lock();
		waiting_person1_count++;
		_Time_(&Time);//获得当前本地时间
		
		V_OPERATION(s_custbuy1);
		mutex_buy1.Unlock();

		s_output.Lock();
		cout << "当前时间：" << Time.wHour << ":" << Time.wMinute << ":" << Time.wSecond << "\t";
		cout << "顾客" << id << "开始排队（1号人工窗口）" //<< endl;
			<< " ";
		cout << "当前等待人数:" << waiting_person1_count << endl;
		s_output.Unlock();


		P_OPERATION(s_serverbuy1);
		_Time_(&Time);//获得当前本地时间
		s_output.Lock();
		cout << "当前时间：" << Time.wHour << ":" << Time.wMinute << ":" << Time.wSecond << "\t";
		cout << "顾客" << id << "开始在1号人工窗口买票" << endl;
		s_output.Unlock();
		
		
		
			
		
	}
	else//第二个最小
	{
		s_person.Unlock();
		
		mutex_buy2.Lock();
		waiting_person2_count++;
		_Time_(&Time);//获得当前本地时间
		
		V_OPERATION(s_custbuy2);
		mutex_buy2.Unlock();
		s_output.Lock();
		cout << "当前时间：" << Time.wHour << ":" << Time.wMinute << ":" << Time.wSecond << "\t";
		cout << "顾客" << id << "开始排队（2号人工窗口）" //<< endl;
			<< " ";
		cout << "当前等待人数:" << waiting_person2_count << endl;
		s_output.Unlock();

		P_OPERATION(s_serverbuy2);
		_Time_(&Time);//获得当前本地时间
		s_output.Lock();
		cout << "当前时间：" << Time.wHour << ":" << Time.wMinute << ":" << Time.wSecond << "\t";
		cout << "顾客" << id << "开始在2号人工窗口买票" << endl;
		s_output.Unlock();
	
		
		
		
	}




	return 0;
}


UINT Purchase_Method_Choice(INT id = 0)
{
	srand(time(NULL));
LOOP:	INT way = //0;
		rand() % 3;//随机数
	switch (way)
	{
	
	case 1:
	
	
		//选择人工购票
			Person_Selection(id);

			break;
	
	case 2:
		
		//选择机器购票
			Mashine_Selection(id);
			
			break;
	default:
		goto LOOP;
		break;
	}
	
	


	return 0;
}


UINT Receive_ServiceSort_Choice(INT id=0)
{
	srand(time(NULL));
	INT ser = //2;
		rand() % 10;//随机数
	switch (ser)
	{
	case 0://改签
		Change_Method(0,id);
		break;
	case 1://退票
		Change_Method(1,id);
		break;
	case 2://买票
	case 3:
	case 4:
	case 5:
	case 6:
	case 7:
	case 8:
	case 9:
		//为了保持概率符合实际，使得买票比退票/改签的人数多，设置2-9作为买票的随机数
		Purchase_Method_Choice(id);
		break;
	default:
		return 1;
		break;
	}




	




	return 0;
}









DWORD WINAPI Customer_Thread(LPVOID lp)//顾客线程
{
	DATA_CUST_ID *pmda = (DATA_POINTER_ID)lp;
	INT ID = pmda->num + 1;//顾客编号
	//顾客进入大厅
	P_OPERATION(s);
	s_enter.Lock();//进入临界区
	_Time_(&Time);//获得当前本地时间
	s_output.Lock();
	cout << "当前时间：" << Time.wHour << ":" << Time.wMinute << ":" << Time.wSecond << "\t";
	cout << "顾客" << ID << "已进入售票大厅！" << "\t";
	waiting_in_number--;
	person_sum_number++;
	cout << "售票大厅内人数：" << person_sum_number << endl;
	s_output.Unlock();
	s_enter.Unlock();//退出临界区

	Receive_ServiceSort_Choice(ID);//顾客接受服务
	waiting_out_number++;
	
	s_exit.Lock();//进入临界区
	//顾客退出大厅
	_Time_(&Time);//获得当前本地时间
	s_output.Lock();
	cout << "当前时间：" << Time.wHour << ":" << Time.wMinute << ":" << Time.wSecond << "   ";
	cout << "顾客" << ID << "已退出售票大厅！" << " ";
	waiting_out_number--;
	person_sum_number--;
	cout << "剩余等待退出人数：" << waiting_out_number << " ";
	cout << "售票大厅内人数：" << person_sum_number << endl;
	s_output.Unlock();
	s_exit.Unlock();//退出临界区
	V_OPERATION(s);
	return 0;






}

DWORD WINAPI Server_Person1_Thread(LPVOID lp=NULL)//1号买票窗口进程
{
	DATA_CUST_ID *pmda = (DATA_POINTER_ID)lp;
	INT ID = 1;//编号为1
	while (1)
	{
		
		P_OPERATION(s_custbuy1);
		mutex_buy1.Lock();//进入临界区
		waiting_person1_count--;//排队人数-1
		
		
		
		
		V_OPERATION(s_serverbuy1);
		mutex_buy1.Unlock();//退出临界区
		WAIT_S(35);//等待一段时间后，为下一个顾客服务
		//这段等待的时间可视为为第一个用户服务所花费的时间
		//设置为35秒
		




	}
	return 0;
}


DWORD WINAPI Server_Person2_Thread(LPVOID lp = NULL)//2号买票窗口线程
{
	DATA_CUST_ID *pmda = (DATA_POINTER_ID)lp;
	INT ID = 2;//编号为2
	while (1)
	{
		
		P_OPERATION(s_custbuy2);
		mutex_buy2.Lock();//进入临界区
		waiting_person2_count--;//等候人数-1
		
		
		
		
		V_OPERATION(s_serverbuy2);
		mutex_buy2.Unlock();//退出临界区
		WAIT_S(35);//等待一段时间后，为下一个顾客服务
		//这段等待的时间可视为为第一个用户服务所花费的时间
		//设置为35秒
		





	}
	return 0;
}

DWORD WINAPI Server_ChangeRefund_Thread(LPVOID lp=NULL)//改签/退票窗口线程
{
	while (1)
	{
		

		P_OPERATION(s_custchange);
		mutex_change.Lock();//进入临界区
		waiting_change_refund_count--;//等候人数-1
		
		
		
		
		V_OPERATION(s_serverchange);
		mutex_change.Unlock();//退出临界区
		WAIT_S(35);//等待一段时间后，为下一个顾客服务
		//这段等待的时间可视为为第一个用户服务所花费的时间
		//设置为35秒
		





	}




	return 0;
}




INT main(_In_ INT _Argc, _In_reads_(_Argc) _Pre_z_ PSTR _Argv[], _In_z_ PSTR _Env[])//主函数入口
{
	INT sum = 0;
	srand(time(NULL));//随机种子
	Init_all_Semaphore();//初始化
	HANDLE hServer_Buy1, hServer_Buy2, hCustomer[MAX_NUM_CUSTOMER];
	HANDLE hServer_Change;
	HANDLE hServer_Mashine1, hServer_Mashine2, hServer_Mashine3;
	DATA_POINTER_ID pDataArray_Server1, pDataArray_Server2, pDataArray_Customer[MAX_NUM_CUSTOMER];
	DWORD   dwThreadIdArray_Server1, dwThreadIdArray_Server2, dwThreadIdArray_Customer[MAX_NUM_CUSTOMER];
	DWORD   dwThreadIdArray_Server_Change;
	DWORD   dwThreadIdArray_Mashine1, dwThreadIdArray_Mashine2, dwThreadIdArray_Mashine3;
	
	pDataArray_Server1 = (DATA_POINTER_ID)HeapAlloc(GetProcessHeap(), HEAP_ZERO_MEMORY,sizeof(DATA_CUST_ID));
	pDataArray_Server1->num = 1;
	_Time_(&Time);//获得当前本地时间
	//互斥输出
	s_output.Lock();
	cout << "当前时间：" << Time.wHour << ":" << Time.wMinute << ":" << Time.wSecond << " ";
	cout << "人工售票窗口" <<  1 << "线程已建立" << endl;
	s_output.Unlock();
	hServer_Buy1 = CreateThread(NULL, 0, Server_Person1_Thread, NULL, 0, &dwThreadIdArray_Server1);//建立线程（人工购票服务窗口）
	if (!hServer_Buy1)
	{
		cerr << "线程建立错误:" << Err_Code() << endl;
		ExitProcess(1);
	}
		
	WAIT_MS(1000);
	pDataArray_Server2 = (DATA_POINTER_ID)HeapAlloc(GetProcessHeap(), HEAP_ZERO_MEMORY, sizeof(DATA_CUST_ID));
	pDataArray_Server2->num = 2;
	_Time_(&Time);//获得当前本地时间
	s_output.Lock();
	cout << "当前时间：" << Time.wHour << ":" << Time.wMinute << ":" << Time.wSecond << " ";
	cout << "人工售票窗口" << 2 << "线程已建立" << endl;
	s_output.Unlock();
	hServer_Buy2 = CreateThread(NULL, 0, Server_Person2_Thread, NULL, 0, &dwThreadIdArray_Server2);//建立线程（人工购票服务窗口）
	if (!hServer_Buy2)
	{
		cerr << "线程建立错误:" << Err_Code() << endl;
		ExitProcess(1);
	}
	
	hServer_Change = CreateThread(NULL, 0, Server_ChangeRefund_Thread, NULL, 0, &dwThreadIdArray_Server_Change);//建立进程（人工改签退票窗口）
	if (!hServer_Change)
	{
		cerr << "线程建立错误:" << Err_Code() << endl;
		ExitProcess(1);
	}
	_Time_(&Time);//获得当前本地时间
	s_output.Lock();
	cout << "当前时间：" << Time.wHour << ":" << Time.wMinute << ":" << Time.wSecond << " ";
	cout << "人工改签退票窗口线程已建立" << endl;
	cout << endl << endl;
	cout << "*********所有窗口线程均已建立成功！**********" << endl;
	s_output.Unlock();
	
	WAIT_S(3);
	system("cls");
	Current_Time t_time = Time;//记录当前时间
	for (INT i = 0; i < MAX_NUM_CUSTOMER; i++)
	{
		pDataArray_Customer[i] = (DATA_POINTER_ID)HeapAlloc(GetProcessHeap(), HEAP_ZERO_MEMORY, sizeof(DATA_CUST_ID));//分配空间记录线程ID
		pDataArray_Customer[i]->num = i;
		_Time_(&Time);//获得当前本地时间
		if ((Time.wHour - t_time.wHour) >2)//服务超过2小时结束
		{
			cerr << "****************今日服务已结束*****************" << endl;
			sum = i;
			break;
		}
		s_output.Lock();
		cout << "当前时间：" << Time.wHour << ":" << Time.wMinute << ":" << Time.wSecond << " ";
		cout << "顾客" << i + 1 << "已到达售票大厅门口，排队等候进入" << " ";
		waiting_in_number++;
		cout << "当前等待进入人数：" << waiting_in_number << endl;
		s_output.Unlock();
		hCustomer[i] = CreateThread(NULL, 0, Customer_Thread, pDataArray_Customer[i], 0, &dwThreadIdArray_Customer[i]);//建立线程（人工购票服务窗口）
		if (!hCustomer[i])
		{
			cerr << "线程建立错误:" << Err_Code() << endl;
			ExitProcess(1);
		}
		
		DWORD t = 1500 + rand() % 2000;
		WAIT_MS(t);//顾客到来的时间是随机的
		if (i!=0&&i%4==0)//每4个清屏一次
		{
			WAIT_S(5);
			system("cls");//清屏
		}
	}


	

	Until_Terminate_All_Threads(sum, hCustomer);//等待线程结束


	Until_Terminate_All_Threads(1, &hServer_Buy1);
	Until_Terminate_All_Threads(1, &hServer_Buy2);
	Until_Terminate_All_Threads(1, &hServer_Change);
	//关闭线程
	BOOL nRe1=CloseHandle(hServer_Buy1);
	if (nRe1!=S_OK)
	{
		cerr << "线程(人工窗口1）关闭失败:" << Err_Code() << endl;
		ExitProcess(1);
	}
	BOOL nRe2=CloseHandle(hServer_Buy2);
	if (nRe2 != S_OK)
	{
		cerr << "线程（人工窗口2）关闭失败:" << Err_Code() << endl;
		ExitProcess(1);
	}
	BOOL nRe3=CloseHandle(hServer_Change);
	if (nRe3 != S_OK)
	{
		cerr << "线程（人工改签/退票窗口）关闭失败:" << Err_Code() << endl;
		ExitProcess(1);
	}
	s_output.Lock();
	cout << endl << endl;
	cout << "********所有窗口线程均已结束!***************" << endl;
	s_output.Unlock();
	return 0;
}


















