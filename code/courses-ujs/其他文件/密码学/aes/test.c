#include "RIJNDAEL.h"

#include <stdio.h>
#include <time.h>
#include <stdlib.h>

#define MAXNUM 255
#define RAND_MAX 0x7fff



void main()

{
	int i;
	u4byte IV[4]={0x12345678,0x90123456,0x78901234,0x56789012};//初始向量IV
	u4byte ori1[4],ori2[4],ri[8],ri1[4],ri2[4];
	u4byte out_block1[4],de_block1[4];
	u4byte out_block2[4],de_block2[4];
	u4byte out_block[8],de_block[8];
	u4byte in_key[4]={0x11111111,0x22222222,0x33333333,0x44444444};//密钥初始化
	u4byte plaint_block1[4]={0x31506040,0x28315060,0x40283150,0x60402831};//明文块1
	u4byte plaint_block2[4]={0x50604028,0x31506040,0x28315060,0x40283150};//明文块2
	u4byte x1,x2,x3,x4;
 	time_t t;                 //这两行保证每次产生的随机数不同
    srand( (unsigned) time( &t ) ); 

	printf("ECB~~Start~~~~~~~~~~~~~~\n");
	//产生随机密钥
	for(i=0;i<4;i++)
	{
		x1 = rand()*MAXNUM/RAND_MAX;
		x2 = rand()*MAXNUM/RAND_MAX;
		x3 = rand()*MAXNUM/RAND_MAX;
		x4 = rand()*MAXNUM/RAND_MAX;
		in_key[i] = 0;
		in_key[i] = x1;
		in_key[i]<<=8;
		in_key[i] += x2;
		in_key[i]<<=8;
		in_key[i] += x3;
		in_key[i]<<=8;
		in_key[i] += x4;
	}


	set_key(in_key,128);//设置128位的密钥

	encrypt(plaint_block1,out_block1);//加密明文块1
	encrypt(plaint_block2,out_block2);//加密明文块2
	//拼接密文块
	for (i=0;i<4;i++)
	{
		out_block[i]=out_block1[i];
	}
	for (i=4;i<8;i++)
	{
		out_block[i]=out_block2[i-4];
	}
	printf("Encryption:\n密文： \n");
	for(i=0;i<8;i++)
		printf("%x",out_block[i]);
	printf("\n");

	decrypt(out_block1,de_block1);//解密密文块1
	decrypt(out_block2,de_block2);//解密密文块2
	//拼接明文块
	for (i=0;i<4;i++)
	{
		de_block[i]=de_block1[i];
	}
	for (i=4;i<8;i++)
	{
		de_block[i]=de_block2[i-4];
	}
	printf("Decryption!\n恢复原文： \n");
	for(i=0;i<8;i++)
		printf("%x",de_block[i]);
	printf("\n");
	printf("CBC~~Start~~~~~~~~~~~~~~\n");
	//产生随机密钥
	for(i=0;i<4;i++)
	{
		x1 = rand()*MAXNUM/RAND_MAX;
		x2 = rand()*MAXNUM/RAND_MAX;
		x3 = rand()*MAXNUM/RAND_MAX;
		x4 = rand()*MAXNUM/RAND_MAX;
		in_key[i] = 0;
		in_key[i] = x1;
		in_key[i]<<=8;
		in_key[i] += x2;
		in_key[i]<<=8;
		in_key[i] += x3;
		in_key[i]<<=8;
		in_key[i] += x4;
	}


	set_key(in_key,128);//设置128位的密钥
	for (i=0;i<4;i++)
	{
		ori1[i]=plaint_block1[i]^IV[i];//与初始向量异或
	}
	encrypt(ori1,out_block1);//加密生成密文块1
	for (i=0;i<4;i++)
	{
		ori2[i]=out_block1[i]^plaint_block2[i];//与密文块1异或
	}
	encrypt(ori2,out_block2);//加密生成密文块2
	//拼接密文块
	for (i=0;i<4;i++)
	{
		ri[i]=out_block1[i];
	}
	for (i=4;i<8;i++)
	{
		ri[i]=out_block2[i-4];
	}
	printf("Encryption:\n密文： \n");
	for(i=0;i<8;i++)
		printf("%x",ri[i]);
	printf("\n");
	decrypt(out_block1,de_block1);//解密密文块1
	decrypt(out_block2,de_block2);//解密密文块2
	for (i=0;i<4;i++)
	{
		ri2[i]=out_block1[i]^de_block2[i];//与密文块1异或
	}
	for (i=0;i<4;i++)
	{
		ri1[i]=IV[i]^de_block1[i];//与IV异或
	}
	//拼接明文块
	for (i=0;i<4;i++)
	{
		ri[i]=ri1[i];
	}
	for (i=4;i<8;i++)
	{
		ri[i]=ri2[i-4];
	}
	printf("Decryption!\n恢复原文： \n");
	for(i=0;i<8;i++)
		printf("%x",ri[i]);
	printf("\n");
}