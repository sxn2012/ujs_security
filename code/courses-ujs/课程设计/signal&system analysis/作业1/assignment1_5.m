%assignment 1.5
%y=stepfun(t,0);
%阶跃函数;
t=-2:1/10000:2;%设定t的范围
p=0;%设定分界点
y=stepfun(t,p);%表达式
plot(t,y);%画图
ylim([-0.5,1.5]);%设定坐标范围
grid on;%打开网络线