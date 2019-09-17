#include <iostream>
#include <stdlib.h>
#include <time.h>
#include <string>
#include <windows.h>
using namespace std;
typedef enum { RUNNING, READY, WAITING, COMPLETED }Status;//状态
//函数声明
void Initialize();
void Control();
void Execute();
void P(int &s);
void V(int &s);
struct PCB
{
	string process_name;//进程名
	Status process_status;//进程状态
	string waiting_cause;//等待原因
	int break_point;//断点
};
PCB producer = { "producer", RUNNING, "\0", -1 };//生产者进程
PCB consumer = { "consumer", RUNNING, "\0", -1 };//消费者进程
int s1 = -1;
int s2 = -1;
int PC = -1;
PCB *current_process = NULL;//当前进程
int PA[5] = { 4, 0, 2, 1, 6 };//生产者指令序列
int SA[5] = { 0, 3, 1, 5, 6 };//消费者指令序列
//int temp = 0;
char B[10] = { 0 };
char ch[100];
int In = 0;
int Out = 0;
int Temp = 0;
char C, X;
void P(int &s)
{
	cout << "P(s)执行" << endl;
	s = s - 1;
	if (s<0)
	{
		current_process->process_status = WAITING;
		current_process->waiting_cause = (s==s1)?s1:s2;
	}
	else
	{
		current_process->process_status = READY;
	}
	/*_asm
	{
		nop
	}*/
	return;
}
void V(int &s)
{
	cout << "V(s)执行"<<endl;
	s = s + 1;
	if (s<0)
	{
		if (producer.process_status==WAITING)
		{
			producer.process_status = READY;
		}
		if (consumer.process_status==WAITING)
		{
			consumer.process_status = READY;
		}
	}
	current_process->process_status = READY;
	/*_asm
	{
		nop
	}*/
	return;
}
void PUT()
{
	cout << "PUT执行" << endl;
	B[In] = C;
	In = (In + 1) % 10;
	return;
}
void GET()
{
	cout << "GET执行" << endl;
	
	
	
	X = B[Out];
	Out = (Out + 1) % 10;
	return;
}
void Produce()
{
	cout << "Produce执行"<<endl;
	C = ch[Temp++];
	cout << "输入的字符:";
	cout << C <<endl;
	return;
}
void Consume()
{
	cout << "Consume执行" << endl;
	cout << "打印字符：";
	cout << X <<endl;
	return;
}
void GOTO(int L = 0)
{
	cout << "GOTO执行"<<endl;
	PC = L;
	return;
}
void NOP()
{
	cout << "NOP执行" << endl;
	_asm nop
	return;
}
void Err()
{
	cerr << "出错" << endl;
	_asm int 3
	return;
}
void Initialize()//初始化
{
	//初始化信号量
	s1 = 10;
	s2 = 0;
	//初始化PCB
	producer.process_status = READY;
	producer.break_point = 0;
	consumer.process_status = READY;
	consumer.break_point = 0;
	//现行进程初始化
	current_process = &producer;
	PC = 0;
	cout << "请输入一组字符：" << endl;
	cin >> ch;
	Control();//处理器调度程序
	return;
}
void Control()
{
	while (1)
	{
		//保护现场
		current_process->break_point = PC;
		if (producer.process_status == READY || consumer.process_status == READY)
		{
			//随机选择一个就绪进程作为现行进程
			srand((unsigned)time(NULL));
			int selected = rand() % 2;
			if (selected == 0)
			{
				current_process = &producer;
				cout << "选择了生产者进程" << endl;
			}
			else
			{
				current_process = &consumer;
				cout << "选择了消费者进程" << endl;
			}
			//把现行进程状态改为运行态
			current_process->process_status = RUNNING;
			//现行进程的断点值=》PC
			PC = current_process->break_point;
			Execute();//处理器指令执行程序

		}
		else
		{
			return;
		}
	}
}
void Execute()
{
	int i = 0;

	int j = PC;//取出PC
	

	if (current_process->process_name == "producer")
	{
		i = PA[j%5];//获取指令入口地址
		//防止数组越界
		switch (i)
		{
		case 0:
			P(s1);
			break;
		case 1:
			V(s1);
			break;
		case 2:
			PUT();
			break;
		case 3:
			GET();
			break;
		case 4:
			Produce();
			break;
		case 5:
			Consume();
			break;
		case 6:
			GOTO();
			break;
		case 7:
			NOP();
			break;
		default:
			Err();
			break;
		}
	}
	else
	{
		i = SA[j%5];//获取指令入口地址
		//防止数组越界
		switch (i)
		{
		case 0:
			P(s2);
			break;
		case 1:
			V(s2);
			break;
		case 2:
			PUT();
			break;
		case 3:
			GET();
			break;
		case 4:
			Produce();
			break;
		case 5:
			Consume();
			break;
		case 6:
			GOTO();
			break;
		case 7:
			NOP();
			break;
		default:
			Err();
			break;
		}
		
	}
	PC = j + 1;
	current_process->process_status = READY;
	int nRe = ::MessageBoxA(NULL, "生产者进程是否结束？", "提示", MB_YESNO);
	if (nRe==IDYES)
	{
		consumer.process_status = COMPLETED;
	}
	return;
}
int main()
{
	Initialize();//调用初始化函数
	return 0;
}