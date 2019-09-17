%assignment 1.1
%y=Asin(w*t+p0);
A=1;
w=1;
p0=0;%定义正弦信号中的参数
t=0:pi/200:6*pi;%设定t的范围
y=A*sin(w*t+p0);%表达式
plot(t,y);%画图
grid on;%打开网络线