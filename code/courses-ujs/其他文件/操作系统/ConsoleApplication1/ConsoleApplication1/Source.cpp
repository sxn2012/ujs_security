#include <iostream>
#include <vector>
using namespace std;
enum Status{ R, E };
struct	PCB
{
	char name[5];
	PCB *next;
	int time;
	int sequence;
	Status status;
};
PCB k1 = { "P1", NULL, 2, 1, R }, k5 = { "P5", &k1, 4, 2, R }, k3 = { "P3", &k5, 1, 3, R }, k4 = { "P4", &k3, 2, 4, R }, k2 = { "P2", &k4, 3, 5, R };
PCB *head = &k2;
vector<PCB> q;
vector<PCB>::iterator pos1,pos2;
void pro(PCB *&p)
{
	cout << p->name << " selected.~~~" << endl;
	p->time--;
	p->sequence--;

	
	if (p->time <= 0)
	{
		p->status = E;
		
		
		q.erase(q.begin());
		
	}
	else if (p->time>0)
	{
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
	cout << "name  sequence  time  status" << endl;
	while (temp)
	{
		t = *temp;
		q.push_back(t);
		cout << t.name << "\t" << t.sequence << "\t" << t.time << "\t" << t.status << endl;
		temp = temp->next;
	}
	cout << endl;
	int i = 0;
	temp =&q[0];
	while (!q.empty())
	{

		
		
		pro(temp);
		for (int j = 0; j < q.size();j++)
		{
			cout << q[j].name << "\t" << q[j].sequence << "\t" << q[j].time << "\t" << q[j].status << endl;
		}
		cout << endl;
		if (q.size() == 0)
		{
			break;
		}
		
		
		
		
		
		
	}
	return 0;

}