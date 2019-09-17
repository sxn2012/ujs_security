#include <iostream>
#include <vector>
using namespace std;
enum Status{ R, E };//进程状态
struct	PCB//进程控制块结构体
{
	char name[5];//进程名
	PCB *next;//下一个执行进程的指针
	int time;//执行时间
	int sequence;//优先级
	Status status;//进程状态
};
PCB k1 = { "P1", NULL, 2, 1, R }, k5 = { "P5", &k1, 4, 2, R }, k3 = { "P3", &k5, 1, 3, R }, k4 = { "P4", &k3, 2, 4, R }, k2 = { "P2", &k4, 3, 5, R };//5个进程的初始状态
PCB *head = &k2;//头指针
vector<PCB> q;//存储进程队列的向量
vector<PCB>::iterator pos1,pos2;//迭代器
void pro(PCB *&p)
{
	cout << p->name << " selected.~~~" << endl;
	p->time--;//时间-1
	p->sequence--;//优先级-1

	
	if (p->time <= 0)//时间<=0
	{
		p->status = E;//把标志置为E
		
		
		q.erase(q.begin());//从队列中去除
		
	}
	else if (p->time>0)
	{
		//按优先级大小排序
		for (pos1 = q.begin(); pos1 != q.end();++pos1)
			for (pos2 = pos1 + 1; pos2 != q.end();++pos2)
				if (pos1->sequence<pos2->sequence)
				{
					swap(*pos1, *pos2);
				}
		while (head->status!=R)
		{
			head = head->next;
		}
	}

}
int main()
{

	PCB t;
	PCB *temp = head;
	//打印初始各进程控制块
	cout << "name  sequence  time  status" << endl;
	while (temp)
	{
		t = *temp;
		q.push_back(t);//插入队列
		cout << t.name << "\t" << t.sequence << "\t" << t.time << "\t" << t.status << endl;//打印
		temp = temp->next;
	}
	cout << endl;
	int i = 0;
	temp =&q[0];//指向第一个要执行的进程
	while (!q.empty())
	{

		
		
		pro(temp);//执行进程
		//打印进程控制块
		for (int j = 0; j < q.size();j++)
		{
			cout << q[j].name << "\t" << q[j].sequence << "\t" << q[j].time << "\t" << q[j].status << endl;
		}
		cout << endl;
		if (q.size() == 0)//全部执行完毕退出
		{
			break;
		}
		
		
		
		
		
		
	}
	return 0;

}