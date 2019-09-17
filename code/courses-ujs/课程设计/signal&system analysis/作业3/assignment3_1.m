%assignment 3.1

g=dsolve('Dg+3*g=ynl','g(0)=0');%求阶跃响应
h=diff(g);%求冲激响应

w=-5*pi:1/10000:5*pi;%频率范围
a=[1,3]; 
b=[3];
H=freqs(b,a,w); %频率响应
subplot(2,1,1);%子图形1
plot(w,abs(H));%画幅度图
grid on;%打开网格线
subplot(2,1,2);%子图形2
plot(w,angle(H));%画相位图
grid on;%打开网格线