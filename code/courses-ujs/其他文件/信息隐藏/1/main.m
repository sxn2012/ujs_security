%function y=functionname()
%filePath='F:\\U盘文件\\信息隐藏\\test.bmp';
%filePath='F:\\U盘文件\\信息隐藏\\medish.bmp';
filePath='./magic.bmp';
con=imread(filePath);
%disp('con1');
%con
imshow(con);
s=size(con);
tmp=zeros(s);
if(s(1)~=s(2))
    helpdlg('sorry','I can''t handle that...');
end
transMatrix=magic(s(1));

%connect
%置换
for i=1:s(1)
    for j=1:s(1)
        t=mod(transMatrix(i,j),s(1));
        if t==0
            t=s(1);
        end
        tmp(i,j,:)=con(ceil(transMatrix(i,j)/s(1)),t,:);
    end
end
imshow(tmp);
%幻方置换
for k=1:s(1)^2
    for i=1:s(1)
        for j=1:s(1)
            if transMatrix(i,j)~=1
                transMatrix(i,j)=transMatrix(i,j)-1;
            else
                transMatrix(i,j)=s(1)^2;
            end
        end
    end
    for i=1:s(1)
        for j=1:s(1)
            t=mod(transMatrix(i,j),s(1));
            if t==0
                t=s(1);
            end
            tmp(i,j,:)=con(ceil(transMatrix(i,j)/s(1)),t,:);
        end
    end
    imshow(tmp);
end
%逆置换
for i=1:s(1)
    for j=1:s(1)
        t=mod(transMatrix(i,j),s(1));
        if t==0
            t=s(1);
        end
        con(ceil(transMatrix(i,j)/s(1)),t,:)=tmp(i,j,:);
    end
end
imshow(con);
%end