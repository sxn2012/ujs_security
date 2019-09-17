Picture = imread('magic.bmp','bmp');  
  

  
Double_Picture = Picture;  
Double_Picture = double(Double_Picture);  
%将图像转换成二进制  
path2_id = fopen('secret.txt' , 'r'); %读取秘密信息文件  
[msg , len] = fread(path2_id , 'ubit1');  
[m , n] = size(Double_Picture);  
  
p = 1; %p为秘密信息的计数器  
for f2 = 1:n  
    for f1 = 1:m  
        Double_Picture(f1 , f2) = Double_Picture(f1,f2) - mod(Double_Picture(f1 , f2) , 2) + msg(p , 1);  
        if(p == len)   
            break;  
        end;  
        p = p + 1;  
    end;  
      
    if(p == len)   
        break;  
    end;   
end;  
  
Double_Picture = uint8(Double_Picture);  
imwrite(Double_Picture , '_magic.bmp');  
subplot(1 , 2 , 1) ; imshow(Picture);title('原始图像');  
subplot(1 , 2 , 2) ; imshow(Double_Picture);title('隐藏图像');  
