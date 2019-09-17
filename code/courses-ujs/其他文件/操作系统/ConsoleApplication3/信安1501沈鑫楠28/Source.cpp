#include <iostream>
#include <string>
#include <queue>
#include <math.h>
using namespace std;
enum Direction{UP,DOWN};
struct Table
{
	string process_name;//进程名
	int cylinder_num;//柱面号
	int track_num;//磁道号
	int record_num;//物理记录号
};
//queue<Table> q;
//初始化
Table t[100];//= { { "P1", 1, 2, 3 }, { "P2", 1, 3, 4 }, { "P3", 2, 4, 3 } };
int table_num=0;//= 3;
Direction current_direction = UP;//当前方向
int current_cylinder = 0;//当前柱面号
//int current_track = 0;//当前磁道号
int current_record = 0;//当前记录号

/*



*/
void DriverModify()
{
	int tag=-1;
	int i = 0;
	int num = -1;
	if (table_num>0)//请求I/O表有内容
	{
		for (i = 0; i < table_num;i++)//遍历
		{
			if (t[i].cylinder_num==current_cylinder)//与当前柱面相同
			{
				int tem = abs(t[i].record_num - current_record);//旋转距离
				if (num==-1||num>tem)//找出最短的旋转距离
				{
					tag = i;
					num = tem;
				}
			}
		}
		if (num>=0)
		{
			current_cylinder = t[tag].cylinder_num;
			current_record = t[tag].record_num;
			cout << "请求IO表："<<endl;
			for (i = 0; i < table_num;i++)
			{
				cout << t[i].process_name << " " << t[i].cylinder_num << " " << t[i].track_num << " " << t[i].record_num << endl;
			}
			cout << "当前移臂方向:" << current_direction << endl;
			cout << "当前柱面号：" << current_cylinder << endl;
			cout << "当前物理记录号：" << current_record << endl; 
			table_num--;
			for (i = tag; i < table_num;i++)
			{
				t[i] = t[i + 1];
			}
		}
		//没有与当前柱面相同的访问者
		else if (current_direction==UP)//移臂方向向里
		{
			for (i = 0; i < table_num;i++)//遍历
			{
				if (t[i].cylinder_num>current_cylinder)//比当前柱面号大的请求
				{
					int tem = t[i].cylinder_num;
					if (num == -1 || num > tem)//找最小者
					{
						tag = i;
						num = tem;
					}
				}
			}
			if (num>=0)
			{
				current_cylinder = t[tag].cylinder_num;
				current_record = t[tag].record_num;
				cout << "请求IO表："<<endl;
				for (i = 0; i < table_num; i++)
				{
					cout << t[i].process_name << " " << t[i].cylinder_num << " " << t[i].track_num << " " << t[i].record_num << endl;
				}
				cout << "当前移臂方向:" << current_direction << endl;
				cout << "当前柱面号：" << current_cylinder << endl;
				cout << "当前物理记录号：" << current_record << endl;
				table_num--;
				for (i = tag; i < table_num; i++)
				{
					t[i] = t[i + 1];
				}

			}
			else//没有比当前柱面号大的请求
			{
				num = -1;
				current_direction = DOWN;//移臂方向置为外移
				for (i = 0; i < table_num; i++)
				{
					if (t[i].cylinder_num<current_cylinder)//比当前柱面号小的请求
					{
						int tem = t[i].cylinder_num;
						if (num == -1 || num < tem)//找最大者
						{
							tag = i;
							num = tem;
						}
					}
				}
				if (num>=0)
				{
					current_cylinder = t[tag].cylinder_num;
					current_record = t[tag].record_num;
					cout << "请求IO表："<<endl;
					for (i = 0; i < table_num; i++)
					{
						cout << t[i].process_name << " " << t[i].cylinder_num << " " << t[i].track_num << " " << t[i].record_num << endl;
					}
					cout << "当前移臂方向:" << current_direction << endl;
					cout << "当前柱面号：" << current_cylinder << endl;
					cout << "当前物理记录号：" << current_record << endl;
					table_num--;
					for (i = tag; i < table_num; i++)
					{
						t[i] = t[i + 1];
					}
				}
				else;

			}
		}
		else//当前移臂方向向外
		{
			num = -1;
			for (i = 0; i < table_num; i++)//遍历
			{
				if (t[i].cylinder_num < current_cylinder)//比当前柱面号小的访问请求
				{
					int tem = t[i].cylinder_num;
					if (num == -1 || num < tem)//找最大者
					{
						tag = i;
						num = tem;
					}
				}
			}
			if (num>=0)
			{
				current_cylinder = t[tag].cylinder_num;
				current_record = t[tag].record_num;
				cout << "请求IO表："<<endl;
				for (i = 0; i < table_num; i++)
				{
					cout << t[i].process_name << " " << t[i].cylinder_num << " " << t[i].track_num << " " << t[i].record_num << endl;
				}
				cout << "当前移臂方向:" << current_direction << endl;
				cout << "当前柱面号：" << current_cylinder << endl;
				cout << "当前物理记录号：" << current_record << endl;
				table_num--;
				for (i = tag; i < table_num; i++)
				{
					t[i] = t[i + 1];
				}
			}
			else//没有比当前柱面号小的访问请求
			{
				num = -1;
				current_direction = DOWN;//移臂方向置为外移
				for (i = 0; i < table_num; i++)
				{
					if (t[i].cylinder_num > current_cylinder)//寻找比当前柱面号大的访问请求
					{
						int tem = t[i].cylinder_num;
						if (num == -1 || num > tem)//寻找最小者
						{
							tag = i;
							num = tem;
						}
					}
				}
				if (num >= 0)
				{
					current_cylinder = t[tag].cylinder_num;
					current_record = t[tag].record_num;
					cout << "请求IO表："<<endl;
					for (i = 0; i < table_num; i++)
					{
						cout << t[i].process_name << " " << t[i].cylinder_num << " " << t[i].track_num << " " << t[i].record_num << endl;
					}
					cout << "当前移臂方向:" << current_direction << endl;
					cout << "当前柱面号：" << current_cylinder << endl;
					cout << "当前物理记录号：" << current_record << endl;
					table_num--;
					for (i = tag; i < table_num; i++)
					{
						t[i] = t[i + 1];
					}
				}
				else;
			}
		}
		
	}
}
void RequestReceive()
{
	cout << "是否有请求？(Y/N)";
	char c;
	cin >> c;
	if (c=='y'||c=='Y')//有请求
	{
		cout << "请输入进程名、柱面号、磁道号、物理记录号：";
		string name;
		int num1, num2, num3;
		cin >> name >> num1 >> num2 >> num3;//输入进程名、物理地址
	
		Table temp = { name, num1, num2, num3 };
		//q.push(temp);
		t[table_num++] = temp;//登记请求I/O表
	}
	return;
}
int main(_In_ int _Argc, _In_reads_(_Argc) _Pre_z_ char ** _Argv, _In_z_ char ** _Env)
{
	
	double input_num = 0.0;
	while (cout<<"请输入一个随机数："&&cin>>input_num)//输入随机数
	{
		if (input_num>0.5)//大于0.5
		{
			DriverModify();//驱动调度
		}
		else//小于0.5
		{
			RequestReceive();//接受请求
		}
	}
	return 0;
}