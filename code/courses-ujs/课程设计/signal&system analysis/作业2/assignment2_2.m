%assignment 2.2
%计算三角波信号频谱
%主函数脚本
w = linspace(-6 * pi,6*pi,512);%生成行向量
n = length(w);%求行向量w的长度
F = zeros(1,N);%生成全0的矩阵
for i=1:n
    F(i)=quadl(@fun,-1,1,[],[],w(i));%求积分
end
subplot(2,1,1);%子图形1
plot(w,F);%F的图像
grid on;%打开网格线
subplot(2,1,2);%子图形2
plot(w,F-sinc(w./2./pi).^2);%误差图形
grid on;%打开网格线