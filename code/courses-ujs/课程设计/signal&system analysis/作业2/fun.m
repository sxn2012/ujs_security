%assignment 2.2
%计算三角波信号频谱
%函数定义脚本
function value=fun(t,w)
 if (abs(t)<=1)
     value=(1-abs(t)).*(exp(j*w*t));
 else value=0;
end
