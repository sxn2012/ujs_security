%assignment 2.1
%绘制三角波信号
%频谱
N=10;%(-10,10)范围
n1=-N:-1;%负半轴范围
F1=-4*j*sin(n1*pi/2)/pi^2./n1.^2;%负半轴频谱表达式
n2=1:N;%正版轴范围
F2=-4*j*sin(n2*pi/2)/pi^2./n2.^2;%正半轴频谱表达式
F3=0;%原点频谱为0
F=[F1 F3 F2];%合并
n=-N:N;%总的范围
subplot(2,1,1);%子图形1
stem(n,abs(F));%画幅度图
grid on;%打开网格线
subplot(2,1,2)%子图形2
stem(n,angle(F));%画相位图
grid on;%打开网格线