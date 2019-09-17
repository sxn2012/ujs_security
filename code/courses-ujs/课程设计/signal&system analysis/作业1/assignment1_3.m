%assignment 1.3
%y=rectpuls(t);
%中心为t=0;
t=-2:1/10000:2;%设定t的范围
wid=2;%设定宽度
y=rectpuls(t,wid);%表达式
plot(t,y);%画图
ylim([-0.5,1.5]);%设定坐标范围
grid on;%打开网络线