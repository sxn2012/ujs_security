%assignment 4
%求奈奎斯特频率
%y(t)=sin(40*pi*t)+sin(24*pi*t)
n1=[40*pi]; %sinkt中的系数
v1=[1 0 ((40*pi)^2)]; %拉普拉斯变换
c1=tf(n1,v1); %L[sinkt]=k/(s^2+k^2)
n2=[24*pi]; %sinkt中的系数
v2=[1 0 ((24*pi)^2)]; %拉普拉斯变换
c2=tf(n2,v2);  %L[sinkt]=k/(s^2+k^2)
c=c1+c2;%整个系统的拉普拉斯变换
nyquist(c);%奈奎斯特频率图