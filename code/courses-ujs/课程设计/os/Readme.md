Practice of process communication and process synchronization
SHEN XINNAN 
June, 2017
 
1. Content of Design
There are 2 personal ticket selling windows, 1 personal changing/refunding ticket window, and 3 automatic ticket selling machines in the station ticketing hall. The ticket hall has a capacity of 200 people and has an entrance and an exit. The exit/entry door allows only one person to pass at a time. When the customer arrives in the front of the ticket hall, they will queue up to enter the ticket hall. When the customer enters the lobby, he or she will choose the manual or automatic ticketing service according to his own requirements and preferences. If he or she wants to have their ticket signed or refunded, the manual changing/refunding ticket window can be the only one to be chosen. When the process of refunding/changing/purchasing ticket is completed, the customer will leave in order from the exit. As for the ticket seller of the manual service window, when there is a customer, the selling/changing/refunding ticket service is provided to the customer in turn, and when there is no customer the ticket seller has to wait. Design synchronization and mutual exclusion algorithms for customer processes and ticket seller processes using semaphores and PV operations.
2.Requirement
(1) Suppose a service time interval such as 7:00-20:00, or limit the maximum number of customers;
(2) The arrival time of the customers is random, the purchasing/changing/refunding ticket service is random, and the manual/automatic ticket sales are selected randomly. When a specific ticket purchase method is selected, customers choose the queue with the least number of people and the random situation needs to be implemented by random number simulation;
(3)It is necessary to distinguish between different manual service windows and different machines;
(4)The time of selling/changing/refunding ticket should be implemented by a random number.
3. Main Data Structure and Algorithms used
3.1.Main Data Structure used in the Program
£¨1£©A structure of Customer ID

struct ID_STRUCT//ID
{
	int num;
}£»
£¨2£©A structure of current time
SYSTEMTIME Time;
£¨3£©arraies of customer handle and threadid
HANDLE hCustomer[MAX_NUM_CUSTOMER]£»
DWORD dwThreadIdArray_Customer[MAX_NUM_CUSTOMER];
3.2.The Algorithms used in the program
£¨1£©synchronization and mutual exclusion algorithms for processed
semaphore s_enter, s_exit, s_output, s_Machine,
s_person,mutex_change,mutex_buy1,mutex_buy2,mutex_Machine1, mutex_Machine2, mutex_Machine3, s_Machine1, s_Machine2, s_Machine3;
semaphore s, s_serverchange, s_serverbuy1, s_serverbuy2, s_serverMachine1, s_serverMachine2, s_serverMachine3, s_custchange, s_custbuy1, s_custbuy2, s_custMachine1, s_custMachine2, s_custMachine3;
int person_in = 0,waiting_in = 0,waiting_out = 0,waiting_person1 = 0,
waiting_person2 = 0,waiting_Machine1 = 0,waiting_Machine2 = 0,
waiting_Machine3 = 0,waiting_change = 0;
s_enter.value=1;
s_exit.value=1;
s_output.value=1;
s_Machine.value=1;
s_person.value=1;
mutex_change=1;
mutex_buy1.value=1; mutex_buy2.value=1;
mutex_Machine1.value=1; mutex_Machine2.value=1; mutex_Machine3.value=1;
s_Machine1.value=1; s_Machine2.value=1; s_Machine3.value=1;
s.value=200;
s_serverchange.value=0;
s_serverbuy1.value=0; s_serverbuy2.value=0;
s_serverMachine1.value=0; s_serverMachine2.value=0; s_serverMachine3.value=0;
s_custchange.value=0;
s_custbuy1.value=0; s_custbuy2.value=0;
s_custMachine1.value=0; s_custMachine2.value=0; s_custMachine3.value=0;
process customer(int i)//Customer Process
{
P(s);
P(s_enter);
½øÈë£»
V(s_enter);
Receive_Service();//Call a function
P(s_exit);
ÍË³ö£»
V(s_exit);
V(s);
}
process server1()//Manual Service Window 1 Process
{
while(1)
{
P(s_custbuy1);
P(mutex_buy1);
waiting_person1--;
V(s_serverbuy1);
V(mutex_buy1);
}
}
process server2()//Manual Service Window 2 Process
{
while(1)
{
P(s_custbuy2);
P(mutex_buy2);
waiting_person2--;
V(s_serverbuy2);
V(mutex_buy2);
}
}
process server_change()//Changing or Refunding Ticket Window Process
{
while(1)
{
P(s_custchange);
p(mutex_change);
waiting_change--;
V(s_serverchange);
V(mutex_change);
}
}
void Receive_Service()//Function of Receiving Service
{
int ser=rand()%4;
switch(ser)
{
case 0:
  change_method();
  break;
case 1:
  change_method();
  break;
case 2:
case 3:
  Purchase_method();
  break;
}
}
void change_method()//Function of Changing or Refunding Ticket
{
P(mutex_change);
waiting_change++;
V(s_custchange);
V(mutex_change);
P(server_change);
Changing or Refunding£»
}
void Perchase_method()//Function of Purchasing Ticket
{
int way=rand()%2;
if(way==0) Machine_func();//Call Machine Service Function
else Person_func();//Call Manual Service Function
}
void Person_func()//Manual Service
{
Choose a queue with least waiting_person;
P(mutex_buy);
respective waiting_person+1;
V(s_custbuy);
V(mutex_buy);
P(s_serverbuy);
}
void Machine_func()//Machine Service
{
Choose a queue with least waiting_Machine;
P(mutex_Machine);
respective waiting_Machine+1;
V(mutex_Machine);
P(s_Machine);
Use the Machine to buy tickets;
V(s_Machine);
P(mutex_Machine);
respective waiting_Machine-1;
V(mutex_Machine);
}


4.The respective functions of operations
4.1.Enter the critical section 
s.Lock();
4.2.Exit the critical section 
s.Unlock();
4.3.Create a thread
 CreateThread(NULL,0,func,NULL,0,NULL);
4.4.create a semaphore
 CreateSemaphore(NULL, initial value£¬biggest value£¬NULL);
4.5.P-Operation 
WaitForSingleObject(s,INFINITE);
4.6.V-Operation 
ReleaseSemaphore(s,1,NULL);
Note: Above Functions can only be used in Windows.
5. Analysis of the result
Firstly, Customer 1 entered the lobby and chose manual service window to purchase tickets. The choice is manual window 1 as there is no customer in front of every manual window. After a few minutes, customer 1 finished purchasing and exited the lobby.
Then, Customer 2 entered the lobby and chose manual service window to purchase tickets. The choice is manual window 1 as there is no customer in front of every manual window.
Additionally, Customer 3 entered the lobby and chose machine service to purchase tickets. The choice is machine 1 as there is no customer in front of every machine.
Afterwards, Customer 4 entered the lobby and chose machine service to purchase tickets. The choice is machine 2 as there is one customer in front of machine 1.
After that, Customer 5 entered the lobby and chose manual service window to purchase tickets. The choice is manual window 2 as there is one customer in front of manual window one.
¡­¡­
The customers coming back are like this, according to the random number selection method, and then select the queue with the least waiting number. The designed service time in the program is 2 hours, so the program will finish after 2 hours of operation.
6. Conclusion
In this project, I used the principle of thread synchronization and mutual exclusion to implement a practical model of the ticketing hall queuing problem. In addition, I have also improved the skills of designing and developing software. This project allows me to master the design rationally and systematically from the big picture to the details as well as from simple item to complex item.
