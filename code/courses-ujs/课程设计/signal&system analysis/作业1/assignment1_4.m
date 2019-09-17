%assignment 1.4
%y=dirac(t);
%冲激函数;
t=-2:1/10000:2;%设定t的范围
y=dirac(t);%表达式
y=1*sign(y);
plot(t,y);%画图
ylim([-0.5,1.5]);%设定坐标范围
grid on;%打开网络线