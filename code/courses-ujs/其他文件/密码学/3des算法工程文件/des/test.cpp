#include"des.h"
#include <iostream>
#include <stdio.h>
#include <time.h>
#include <math.h>
#include <stdlib.h>
#include <string>

#define PACKETLEN 8
#define RAND_MAX 0x7fff
using namespace std;

des_key s_k1,s_k2,s_k3;
string m_temp[100],c_temp[100],p_temp[100],ctr_temp[100];
unsigned char IV[8];
void _3DES_Startup(string k1,string k2,string k3)
{
	srand((unsigned)time(NULL));
	for(int i=0;i<8;i++)
		IV[i]=(unsigned char)(rand()%255 + 1);
	for(i=0;i<100;i++)
	{
		m_temp[i]="";
		c_temp[i]="";
		p_temp[i]="";
		ctr_temp[i]="";
	}
	int n1=k1.length();
	int n2=k2.length();
	int n3=k3.length();
	unsigned char *key1=new unsigned char[8];
	unsigned char *key2=new unsigned char[8];
	unsigned char *key3=new unsigned char[8];
	strcpy((char*)key1,k1.c_str());
	strcpy((char*)key2,k2.c_str());
	strcpy((char*)key3,k3.c_str());
	key1[7]=0;
	key2[7]=0;
	key3[7]=0;
	des_setup((unsigned char*)key1,8,0,&s_k1);
	des_setup((unsigned char*)key2,8,0,&s_k2);
	des_setup((unsigned char*)key3,8,0,&s_k3);
}

string _3DES_Encrypt(string m)
{
	unsigned char *pt=new unsigned char[m.length()+1];
	unsigned char *temp1=new unsigned char[m.length()+1];
	unsigned char *temp2=new unsigned char[m.length()+1];
	unsigned char *ct=new unsigned char[m.length()+1];
	for(int i=0;i<m.length()+1;i++)
	{
		pt[i]=0;
		temp1[i]=0;
		temp2[i]=0;
		ct[i]=0;
	}
	strcpy((char *)pt,m.c_str());
	pt[m.length()]=0;
	des_ecb_encrypt(pt,temp1,&s_k1);//k1加密
	des_ecb_decrypt(temp1,temp2,&s_k2);//k2解密
	des_ecb_encrypt(temp2,ct,&s_k3);//k3加密
	string c((char *)ct);
	return c;
}

string _3DES_Decrypt(string c)
{
	unsigned char *pt=new unsigned char[c.length()+1];
	unsigned char *temp1=new unsigned char[c.length()+1];
	unsigned char *temp2=new unsigned char[c.length()+1];
	unsigned char *ct=new unsigned char[c.length()+1];
	for(int i=0;i<c.length()+1;i++)
	{
		pt[i]=0;
		temp1[i]=0;
		temp2[i]=0;
		ct[i]=0;
	}
	strcpy((char *)ct,c.c_str());
	ct[c.length()]=0;
	des_ecb_decrypt(ct,temp2,&s_k3);//k3解密
	des_ecb_encrypt(temp2,temp1,&s_k2);//k2加密
	des_ecb_decrypt(temp1,pt,&s_k1);//k1解密
	string m((char *)pt);
	return m;
}

void Inintialize_Encrypt_Decrypt(string s1,string s2,string s3)
{
	_3DES_Startup(s1,s2,s3);
}


string Encrypt_All_Data_ECB_ZERO(string m)//ECB模式加密，零字节填充
{
	string c="";
	int k=0;
	
	do
	{
		m_temp[k]=m.substr(8*k,8);
		//比特填充
		if(m_temp[k].length()<8)
		{
			int count_sub=8-m_temp[k].length();
			m_temp[k].append(count_sub,0x00);
		}
		c_temp[k]=_3DES_Encrypt(m_temp[k]);
		c=c+c_temp[k];
		k++;
	}
	while(8*k<m.length());
	c_temp[k]="\0";
	return c;
}

string Decrypt_All_Data_ECB_ZERO(string c)//ECB模式解密，零字节填充
{
	int k=0;
	string p="";
	do
	{
		p_temp[k]=_3DES_Decrypt(c_temp[k]);
		//比特填充
		for(int i=0;i<p_temp[k].length();i++)
			if(p_temp[k][i]<0x08) 
				p_temp[k]=p_temp[k].substr(0,i);
		p=p+p_temp[k];
		k++;
	}
	while(c_temp[k]!="\0");
	return p;
}

string Encrypt_All_Data_ECB_PKCS7(string m)//ECB模式加密，PKCS7方式填充
{
	string c="";
	int k=0;
	
	do
	{
		m_temp[k]=m.substr(8*k,8);
		//比特填充
		if(m_temp[k].length()<8)
		{
			int count_sub=8-m_temp[k].length();
			m_temp[k].append(count_sub,(char)count_sub);
		}
		c_temp[k]=_3DES_Encrypt(m_temp[k]);
		c=c+c_temp[k];
		k++;
	}
	while(8*k<m.length());
	c_temp[k]="\0";
	return c;
}

string Decrypt_All_Data_ECB_PKCS7(string c)//ECB模式解密，PKCS7方式填充
{
	int k=0;
	string p="";
	do
	{
		p_temp[k]=_3DES_Decrypt(c_temp[k]);
		//比特填充
		for(int i=0;i<p_temp[k].length();i++)
			if(p_temp[k][i]<0x08) 
				p_temp[k]=p_temp[k].substr(0,i);
		p=p+p_temp[k];
		k++;
	}
	while(c_temp[k]!="\0");
	return p;
}


string Encrypt_All_Data_ECB_ANSIX923(string m)//ECB模式加密，ANSI X.923方式填充
{
	string c="";
	int k=0;
	
	do
	{
		m_temp[k]=m.substr(8*k,8);
		//比特填充
		if(m_temp[k].length()<8)
		{
			int count_sub=8-m_temp[k].length();
			if(count_sub-1>0)
				m_temp[k].append(count_sub-1,0x00);
			m_temp[k].append(1,(char)count_sub);
		}
		c_temp[k]=_3DES_Encrypt(m_temp[k]);
		c=c+c_temp[k];
		k++;
	}
	while(8*k<m.length());
	c_temp[k]="\0";
	return c;
}

string Decrypt_All_Data_ECB_ANSIX923(string c)//ECB模式解密，ANSI X.923方式填充
{
	int k=0;
	string p="";
	do
	{
		p_temp[k]=_3DES_Decrypt(c_temp[k]);
		//比特填充
		for(int i=0;i<p_temp[k].length();i++)
			if(p_temp[k][i]<0x08) 
				p_temp[k]=p_temp[k].substr(0,i);
		p=p+p_temp[k];
		k++;
	}
	while(c_temp[k]!="\0");
	return p;
}

string Encrypt_All_Data_ECB_ISO10126(string m)//ECB模式加密，ISO 10126方式填充
{
	string c="";
	int k=0;
	
	do
	{
		m_temp[k]=m.substr(8*k,8);
		//比特填充
		if(m_temp[k].length()<8)
		{
			int count_sub=8-m_temp[k].length();
			if(count_sub-1>0)
			{
				srand((unsigned)time(NULL));
				unsigned int fill=rand()%256;
				m_temp[k].append(count_sub-1,(char)fill);
			}
			m_temp[k].append(1,(char)count_sub);
		}
		c_temp[k]=_3DES_Encrypt(m_temp[k]);
		c=c+c_temp[k];
		k++;
	}
	while(8*k<m.length());
	c_temp[k]="\0";
	return c;
}

string Decrypt_All_Data_ECB_ISO10126(string c)//ECB模式解密，ISO 10126方式填充
{
	int k=0;
	string p="";
	do
	{
		p_temp[k]=_3DES_Decrypt(c_temp[k]);
		//比特填充
		for(int i=0;i<p_temp[k].length();i++)
			if(p_temp[k][i]<0x08) 
				p_temp[k]=p_temp[k].substr(0,p_temp[k].length()-(int)p_temp[k][i]);
		p=p+p_temp[k];
		k++;
	}
	while(c_temp[k]!="\0");
	return p;
}


string Encrypt_All_Data_CBC_ZERO(string m)//CBC模式加密，零字节填充
{
	string c="";
	int k=0;
	
	do
	{
		m_temp[k]=m.substr(8*k,8);
		
		//比特填充
		if(m_temp[k].length()<8)
		{
			int count_sub=8-m_temp[k].length();
			m_temp[k].append(count_sub,0x00);
		}

		//向量异或
		if(k==0)
			for(int i=0;i<8;i++)
				m_temp[k][i]=m_temp[k][i]^IV[i];
		else
			for(int i=0;i<8;i++)
				m_temp[k][i]=m_temp[k][i]^c_temp[k-1][i];
		c_temp[k]=_3DES_Encrypt(m_temp[k]);
		c=c+c_temp[k];
		k++;
	}
	while(8*k<m.length());
	c_temp[k]="\0";
	return c;
}

string Decrypt_All_Data_CBC_ZERO(string c)//CBC模式解密，零字节填充
{
	int k=0;
	string p="";
	do
	{
		p_temp[k]=_3DES_Decrypt(c_temp[k]);

		//向量异或
		if(k==0)
			for(int i=0;i<8;i++)
				p_temp[k][i]=p_temp[k][i]^IV[i];
		else
			for(int i=0;i<8;i++)
				p_temp[k][i]=p_temp[k][i]^c_temp[k-1][i];
		//比特填充
		for(int i=0;i<p_temp[k].length();i++)
			if(p_temp[k][i]<0x08) 
				p_temp[k]=p_temp[k].substr(0,i);
		
		p=p+p_temp[k];
		k++;
	}
	while(c_temp[k]!="\0");
	return p;
}


string Encrypt_All_Data_CBC_PKCS7(string m)//CBC模式加密，PKCS7方式填充
{
	string c="";
	int k=0;
	
	do
	{
		m_temp[k]=m.substr(8*k,8);
		
		//比特填充
		if(m_temp[k].length()<8)
		{
			int count_sub=8-m_temp[k].length();
			m_temp[k].append(count_sub,(char)count_sub);
		}

		//向量异或
		if(k==0)
			for(int i=0;i<8;i++)
				m_temp[k][i]=m_temp[k][i]^IV[i];
		else
			for(int i=0;i<8;i++)
				m_temp[k][i]=m_temp[k][i]^c_temp[k-1][i];
		c_temp[k]=_3DES_Encrypt(m_temp[k]);
		c=c+c_temp[k];
		k++;
	}
	while(8*k<m.length());
	c_temp[k]="\0";
	return c;
}

string Decrypt_All_Data_CBC_PKCS7(string c)//CBC模式解密，PKCS7方式填充
{
	int k=0;
	string p="";
	do
	{
		p_temp[k]=_3DES_Decrypt(c_temp[k]);

		//向量异或
		if(k==0)
			for(int i=0;i<8;i++)
				p_temp[k][i]=p_temp[k][i]^IV[i];
		else
			for(int i=0;i<8;i++)
				p_temp[k][i]=p_temp[k][i]^c_temp[k-1][i];
		//比特填充
		for(int i=0;i<p_temp[k].length();i++)
			if(p_temp[k][i]<0x08) 
				p_temp[k]=p_temp[k].substr(0,i);
		
		p=p+p_temp[k];
		k++;
	}
	while(c_temp[k]!="\0");
	return p;
}

string Encrypt_All_Data_CBC_ANSIX923(string m)//CBC模式加密，ANSI X.923方式填充
{
	string c="";
	int k=0;
	
	do
	{
		m_temp[k]=m.substr(8*k,8);
		
		//比特填充
		if(m_temp[k].length()<8)
		{
			int count_sub=8-m_temp[k].length();
			if(count_sub-1>0)
				m_temp[k].append(count_sub-1,0x00);
			m_temp[k].append(1,(char)count_sub);
		}

		//向量异或
		if(k==0)
			for(int i=0;i<8;i++)
				m_temp[k][i]=m_temp[k][i]^IV[i];
		else
			for(int i=0;i<8;i++)
				m_temp[k][i]=m_temp[k][i]^c_temp[k-1][i];
		c_temp[k]=_3DES_Encrypt(m_temp[k]);
		c=c+c_temp[k];
		k++;
	}
	while(8*k<m.length());
	c_temp[k]="\0";
	return c;
}

string Decrypt_All_Data_CBC_ANSIX923(string c)//CBC模式解密，ANSI X.923方式填充
{
	int k=0;
	string p="";
	do
	{
		p_temp[k]=_3DES_Decrypt(c_temp[k]);

		//向量异或
		if(k==0)
			for(int i=0;i<8;i++)
				p_temp[k][i]=p_temp[k][i]^IV[i];
		else
			for(int i=0;i<8;i++)
				p_temp[k][i]=p_temp[k][i]^c_temp[k-1][i];
		//比特填充
		for(int i=0;i<p_temp[k].length();i++)
			if(p_temp[k][i]<0x08) 
				p_temp[k]=p_temp[k].substr(0,i);
		
		p=p+p_temp[k];
		k++;
	}
	while(c_temp[k]!="\0");
	return p;
}

string Encrypt_All_Data_CBC_ISO10126(string m)//CBC模式加密，ISO 10126方式填充
{
	string c="";
	int k=0;
	
	do
	{
		m_temp[k]=m.substr(8*k,8);
		
		//比特填充
		if(m_temp[k].length()<8)
		{
			int count_sub=8-m_temp[k].length();
			if(count_sub-1>0)
			{
				srand((unsigned)time(NULL));
				unsigned int fill=rand()%256;
				m_temp[k].append(count_sub-1,(char)fill);
			}
			m_temp[k].append(1,(char)count_sub);
		}

		//向量异或
		if(k==0)
			for(int i=0;i<8;i++)
				m_temp[k][i]=m_temp[k][i]^IV[i];
		else
			for(int i=0;i<8;i++)
				m_temp[k][i]=m_temp[k][i]^c_temp[k-1][i];
		c_temp[k]=_3DES_Encrypt(m_temp[k]);
		c=c+c_temp[k];
		k++;
	}
	while(8*k<m.length());
	c_temp[k]="\0";
	return c;
}

string Decrypt_All_Data_CBC_ISO10126(string c)//CBC模式解密，ISO 10126方式填充
{
	int k=0;
	string p="";
	do
	{
		p_temp[k]=_3DES_Decrypt(c_temp[k]);

		//向量异或
		if(k==0)
			for(int i=0;i<8;i++)
				p_temp[k][i]=p_temp[k][i]^IV[i];
		else
			for(int i=0;i<8;i++)
				p_temp[k][i]=p_temp[k][i]^c_temp[k-1][i];
		//比特填充
		for(int i=0;i<p_temp[k].length();i++)
			if(p_temp[k][i]<0x08) 
				p_temp[k]=p_temp[k].substr(0,p_temp[k].length()-(int)p_temp[k][i]);
		
		p=p+p_temp[k];
		k++;
	}
	while(c_temp[k]!="\0");
	return p;
}


string Encrypt_All_Data_CFB_ZERO(string m)//CFB模式加密，零字节填充
{
	string c="";
	int k=0;
	
	do
	{
		m_temp[k]=m.substr(8*k,8);
		
		//比特填充
		if(m_temp[k].length()<8)
		{
			int count_sub=8-m_temp[k].length();
			m_temp[k].append(count_sub,0x00);
		}

		//向量异或
		string iv((char *)IV);
		if(k==0)
			c_temp[k]=_3DES_Encrypt(iv);
		else
			c_temp[k]=_3DES_Encrypt(c_temp[k-1]);
		for(int i=0;i<8;i++)
			c_temp[k][i]=c_temp[k][i]^m_temp[k][i];
		c=c+c_temp[k];
		k++;
	}
	while(8*k<m.length());
	c_temp[k]="\0";
	return c;
}

string Decrypt_All_Data_CFB_ZERO(string c)//CFB模式解密，零字节填充
{
	int k=0;
	string p="";
	do
	{
		string iv((char *)IV);
		if(k==0)
			p_temp[k]=_3DES_Encrypt(iv);
		else
			p_temp[k]=_3DES_Encrypt(c_temp[k-1]);
		p_temp[k]=p_temp[k].substr(0,8);
		//向量异或
		for(int i=0;i<8;i++)
			p_temp[k][i]=p_temp[k][i]^c_temp[k][i];
		//比特填充
		for(i=0;i<p_temp[k].length();i++)
			if(p_temp[k][i]<0x08) 
				p_temp[k]=p_temp[k].substr(0,i);
		
		p=p+p_temp[k];
		k++;
	}
	while(c_temp[k]!="\0");
	return p;
}


string Encrypt_All_Data_CFB_PKCS7(string m)//CFB模式加密，PKCS7方式填充
{
	string c="";
	int k=0;
	
	do
	{
		m_temp[k]=m.substr(8*k,8);
		
		//比特填充
		if(m_temp[k].length()<8)
		{
			int count_sub=8-m_temp[k].length();
			m_temp[k].append(count_sub,(char)count_sub);
		}

		//向量异或
		string iv((char *)IV);
		if(k==0)
			c_temp[k]=_3DES_Encrypt(iv);
		else
			c_temp[k]=_3DES_Encrypt(c_temp[k-1]);
		for(int i=0;i<8;i++)
			c_temp[k][i]=c_temp[k][i]^m_temp[k][i];
		c=c+c_temp[k];
		k++;
	}
	while(8*k<m.length());
	c_temp[k]="\0";
	return c;
}

string Decrypt_All_Data_CFB_PKCS7(string c)//CFB模式解密，PKCS7方式填充
{
	int k=0;
	string p="";
	do
	{
		string iv((char *)IV);
		if(k==0)
			p_temp[k]=_3DES_Encrypt(iv);
		else
			p_temp[k]=_3DES_Encrypt(c_temp[k-1]);
		p_temp[k]=p_temp[k].substr(0,8);
		//向量异或
		for(int i=0;i<8;i++)
			p_temp[k][i]=p_temp[k][i]^c_temp[k][i];
		//比特填充
		for(i=0;i<p_temp[k].length();i++)
			if(p_temp[k][i]<0x08) 
				p_temp[k]=p_temp[k].substr(0,i);
		
		p=p+p_temp[k];
		k++;
	}
	while(c_temp[k]!="\0");
	return p;
}


string Encrypt_All_Data_CFB_ANSIX923(string m)//CFB模式加密，ANSI X.923方式填充
{
	string c="";
	int k=0;
	
	do
	{
		m_temp[k]=m.substr(8*k,8);
		
		//比特填充
		if(m_temp[k].length()<8)
		{
			int count_sub=8-m_temp[k].length();
			if(count_sub-1>0)
				m_temp[k].append(count_sub-1,0x00);
			m_temp[k].append(1,(char)count_sub);
		}

		//向量异或
		string iv((char *)IV);
		if(k==0)
			c_temp[k]=_3DES_Encrypt(iv);
		else
			c_temp[k]=_3DES_Encrypt(c_temp[k-1]);
		for(int i=0;i<8;i++)
			c_temp[k][i]=c_temp[k][i]^m_temp[k][i];
		c=c+c_temp[k];
		k++;
	}
	while(8*k<m.length());
	c_temp[k]="\0";
	return c;
}

string Decrypt_All_Data_CFB_ANSIX923(string c)//CFB模式解密，ANSI X.923方式填充
{
	int k=0;
	string p="";
	do
	{
		string iv((char *)IV);
		if(k==0)
			p_temp[k]=_3DES_Encrypt(iv);
		else
			p_temp[k]=_3DES_Encrypt(c_temp[k-1]);
		p_temp[k]=p_temp[k].substr(0,8);
		//向量异或
		for(int i=0;i<8;i++)
			p_temp[k][i]=p_temp[k][i]^c_temp[k][i];
		//比特填充
		for(i=0;i<p_temp[k].length();i++)
			if(p_temp[k][i]<0x08) 
				p_temp[k]=p_temp[k].substr(0,i);
		
		p=p+p_temp[k];
		k++;
	}
	while(c_temp[k]!="\0");
	return p;
}


string Encrypt_All_Data_CFB_ISO10126(string m)//CFB模式加密，ISO 10126方式填充
{
	string c="";
	int k=0;
	
	do
	{
		m_temp[k]=m.substr(8*k,8);
		
		//比特填充
		if(m_temp[k].length()<8)
		{
			int count_sub=8-m_temp[k].length();
			if(count_sub-1>0)
			{
				srand((unsigned)time(NULL));
				unsigned int fill=rand()%256;
				m_temp[k].append(count_sub-1,(char)fill);
			}
			m_temp[k].append(1,(char)count_sub);
		}

		//向量异或
		string iv((char *)IV);
		if(k==0)
			c_temp[k]=_3DES_Encrypt(iv);
		else
			c_temp[k]=_3DES_Encrypt(c_temp[k-1]);
		
		for(int i=0;i<8;i++)
			c_temp[k][i]=c_temp[k][i]^m_temp[k][i];
		c=c+c_temp[k];
		k++;
	}
	while(8*k<m.length());
	c_temp[k]="\0";
	return c;
}

string Decrypt_All_Data_CFB_ISO10126(string c)//CFB模式解密，ISO 10126方式填充
{
	int k=0;
	string p="";
	do
	{
		string iv((char *)IV);
		if(k==0)
			p_temp[k]=_3DES_Encrypt(iv);
		else
			p_temp[k]=_3DES_Encrypt(c_temp[k-1]);
		p_temp[k]=p_temp[k].substr(0,8);
		//向量异或
		for(int i=0;i<8;i++)
			p_temp[k][i]=p_temp[k][i]^c_temp[k][i];
		//比特填充
		for(i=0;i<p_temp[k].length();i++)
			if(p_temp[k][i]<0x08) 
				p_temp[k]=p_temp[k].substr(0,p_temp[k].length()-(int)p_temp[k][i]);
		
		p=p+p_temp[k];
		k++;
	}
	while(c_temp[k]!="\0");
	return p;
}


string Encrypt_All_Data_OFB_ZERO(string m)//OFB模式加密，零字节填充
{
	string c="";
	int k=0;
	for(int i=0;i<100;i++)
			c_temp[i]="";
	do
	{
		m_temp[k]=m.substr(8*k,8);
		
		//比特填充
		if(m_temp[k].length()<8)
		{
			int count_sub=8-m_temp[k].length();
			m_temp[k].append(count_sub,0x00);
		}

		//向量异或
		string iv((char *)IV);
		string key=iv;
		for(int i=0;i<k+1;i++)
			key=_3DES_Encrypt(key);
		
		for(i=0;i<8;i++)
			c_temp[k].append(1,(char)key[i]^m_temp[k][i]);
		c=c+c_temp[k];
		k++;
	}
	while(8*k<m.length());
	c_temp[k]="\0";
	return c;
}

string Decrypt_All_Data_OFB_ZERO(string c)//OFB模式解密，零字节填充
{
	int k=0;
	string p="";
	for(int i=0;i<100;i++)
			p_temp[i]="";
	do
	{
		string iv((char *)IV);
		string key=iv;
		for(int i=0;i<k+1;i++)
			key=_3DES_Encrypt(key);
		key=key.substr(0,8);
		//向量异或
		
		for(i=0;i<8;i++)
			p_temp[k].append(1,key[i]^c_temp[k][i]);
		//比特填充
		for(i=0;i<p_temp[k].length();i++)
			if(p_temp[k][i]<0x08) 
				p_temp[k]=p_temp[k].substr(0,i);
		
		p=p+p_temp[k];
		k++;
	}
	while(c_temp[k]!="\0");
	return p;
}


string Encrypt_All_Data_OFB_PKCS7(string m)//OFB模式加密，PKCS7方式填充
{
	string c="";
	int k=0;
	for(int i=0;i<100;i++)
			c_temp[i]="";
	do
	{
		m_temp[k]=m.substr(8*k,8);
		
		//比特填充
		if(m_temp[k].length()<8)
		{
			int count_sub=8-m_temp[k].length();
			m_temp[k].append(count_sub,(char)count_sub);
		}

		//向量异或
		string iv((char *)IV);
		string key=iv;
		for(int i=0;i<k+1;i++)
			key=_3DES_Encrypt(key);
		
		for(i=0;i<8;i++)
			c_temp[k].append(1,(char)key[i]^m_temp[k][i]);
		c=c+c_temp[k];
		k++;
	}
	while(8*k<m.length());
	c_temp[k]="\0";
	return c;
}

string Decrypt_All_Data_OFB_PKCS7(string c)//OFB模式解密，PKCS7方式填充
{
	int k=0;
	string p="";
	for(int i=0;i<100;i++)
		p_temp[i]="";
	do
	{
		string iv((char *)IV);
		string key=iv;
		for(int i=0;i<k+1;i++)
			key=_3DES_Encrypt(key);
		key=key.substr(0,8);
		//向量异或
		
		for(i=0;i<8;i++)
			p_temp[k].append(1,key[i]^c_temp[k][i]);
		//比特填充
		for(i=0;i<p_temp[k].length();i++)
			if(p_temp[k][i]<0x08) 
				p_temp[k]=p_temp[k].substr(0,i);
		
		p=p+p_temp[k];
		k++;
	}
	while(c_temp[k]!="\0");
	return p;
}

string Encrypt_All_Data_OFB_ANSIX923(string m)//OFB模式加密，ANSI X.923方式填充
{
	string c="";
	int k=0;
	for(int i=0;i<100;i++)
			c_temp[i]="";
	do
	{
		m_temp[k]=m.substr(8*k,8);
		
		//比特填充
		if(m_temp[k].length()<8)
		{
			int count_sub=8-m_temp[k].length();
			if(count_sub-1>0)
				m_temp[k].append(count_sub-1,0x00);
			m_temp[k].append(1,(char)count_sub);
		}

		//向量异或
		string iv((char *)IV);
		string key=iv;
		for(int i=0;i<k+1;i++)
			key=_3DES_Encrypt(key);
		
		for(i=0;i<8;i++)
			c_temp[k].append(1,(char)key[i]^m_temp[k][i]);
		c=c+c_temp[k];
		k++;
	}
	while(8*k<m.length());
	c_temp[k]="\0";
	return c;
}

string Decrypt_All_Data_OFB_ANSIX923(string c)//OFB模式解密，ANSI X.923方式填充
{
	int k=0;
	string p="";
	for(int i=0;i<100;i++)
		p_temp[i]="";
	do
	{
		string iv((char *)IV);
		string key=iv;
		for(int i=0;i<k+1;i++)
			key=_3DES_Encrypt(key);
		key=key.substr(0,8);
		//向量异或
		
		for(i=0;i<8;i++)
			p_temp[k].append(1,key[i]^c_temp[k][i]);
		//比特填充
		for(i=0;i<p_temp[k].length();i++)
			if(p_temp[k][i]<0x08) 
				p_temp[k]=p_temp[k].substr(0,i);
		
		p=p+p_temp[k];
		k++;
	}
	while(c_temp[k]!="\0");
	return p;
}

string Encrypt_All_Data_OFB_ISO10126(string m)//OFB模式加密，ISO 10126方式填充
{
	string c="";
	int k=0;
	for(int i=0;i<100;i++)
			c_temp[i]="";
	do
	{
		m_temp[k]=m.substr(8*k,8);
		
		//比特填充
		if(m_temp[k].length()<8)
		{
			int count_sub=8-m_temp[k].length();
			if(count_sub-1>0)
			{
				srand((unsigned)time(NULL));
				unsigned int fill=rand()%256;
				m_temp[k].append(count_sub-1,(char)fill);
			}
			m_temp[k].append(1,(char)count_sub);
		}

		//向量异或
		string iv((char *)IV);
		string key=iv;
		for(int i=0;i<k+1;i++)
			key=_3DES_Encrypt(key);
		
		for(i=0;i<8;i++)
			c_temp[k].append(1,(char)key[i]^m_temp[k][i]);
		c=c+c_temp[k];
		k++;
	}
	while(8*k<m.length());
	c_temp[k]="\0";
	return c;
}

string Decrypt_All_Data_OFB_ISO10126(string c)//OFB模式解密，ISO 10126方式填充
{
	int k=0;
	string p="";
	for(int i=0;i<100;i++)
		p_temp[i]="";
	do
	{
		string iv((char *)IV);
		string key=iv;
		for(int i=0;i<k+1;i++)
			key=_3DES_Encrypt(key);
		key=key.substr(0,8);
		//向量异或
		
		for(i=0;i<8;i++)
			p_temp[k].append(1,key[i]^c_temp[k][i]);
		//比特填充
		for(i=0;i<p_temp[k].length();i++)
			if(p_temp[k][i]<0x08) 
				p_temp[k]=p_temp[k].substr(0,p_temp[k].length()-(int)p_temp[k][i]);
		
		
		p=p+p_temp[k];
		k++;
	}
	while(c_temp[k]!="\0");
	return p;
}



string Encrypt_All_Data_CTR_ZERO(string m)//CTR模式加密，零字节填充
{
	string c="";
	int k=0;
	for(int i=0;i<100;i++)
		ctr_temp[i]="";
	do
	{
		m_temp[k]=m.substr(8*k,8);
		//比特填充
		if(m_temp[k].length()<8)
		{
			int count_sub=8-m_temp[k].length();
			m_temp[k].append(count_sub,0x00);
		}
		//向量异或
		ctr_temp[k].append(1,(char)k);
		while(ctr_temp[k].length()<8)
			ctr_temp[k].append(1,'0');
		c_temp[k]=_3DES_Encrypt(ctr_temp[k]);
		for(int i=0;i<8;i++)
			c_temp[k][i]=c_temp[k][i]^m_temp[k][i];
		c_temp[k]=c_temp[k].substr(0,8);
		c=c+c_temp[k];
		k++;
	}
	while(8*k<m.length());
	c_temp[k]="\0";
	return c;
}

string Decrypt_All_Data_CTR_ZERO(string c)//CTR模式解密，零字节填充
{
	int k=0;
	string p="";
	for(int i=0;i<100;i++)
		p_temp[i]="";
	do
	{
		m_temp[k]=_3DES_Encrypt(ctr_temp[k]);
		for(int i=0;i<8;i++)
			p_temp[k].append(1,(char)(m_temp[k][i]^c_temp[k][i]));
		//比特填充
		for(i=0;i<p_temp[k].length();i++)
			if(p_temp[k][i]<0x08) 
				p_temp[k]=p_temp[k].substr(0,i);
		p=p+p_temp[k];
		k++;
	}
	while(c_temp[k]!="\0");
	return p;
}

string Encrypt_All_Data_CTR_PKCS7(string m)//CTR模式加密，PKCS7方式填充
{
	string c="";
	int k=0;
	for(int i=0;i<100;i++)
		ctr_temp[i]="";
	do
	{
		m_temp[k]=m.substr(8*k,8);
		//比特填充
		if(m_temp[k].length()<8)
		{
			int count_sub=8-m_temp[k].length();
			m_temp[k].append(count_sub,(char)count_sub);
		}
		//向量异或
		ctr_temp[k].append(1,(char)k);
		while(ctr_temp[k].length()<8)
			ctr_temp[k].append(1,'0');
		c_temp[k]=_3DES_Encrypt(ctr_temp[k]);
		for(int i=0;i<8;i++)
			c_temp[k][i]=c_temp[k][i]^m_temp[k][i];
		c_temp[k]=c_temp[k].substr(0,8);
		c=c+c_temp[k];
		k++;
	}
	while(8*k<m.length());
	c_temp[k]="\0";
	return c;
}

string Decrypt_All_Data_CTR_PKCS7(string c)//CTR模式解密，PKCS7方式填充
{
	int k=0;
	string p="";
	for(int i=0;i<100;i++)
		p_temp[i]="";
	do
	{
		m_temp[k]=_3DES_Encrypt(ctr_temp[k]);
		for(int i=0;i<8;i++)
			p_temp[k].append(1,(char)(m_temp[k][i]^c_temp[k][i]));
		//比特填充
		for(i=0;i<p_temp[k].length();i++)
			if(p_temp[k][i]<0x08) 
				p_temp[k]=p_temp[k].substr(0,i);
		p=p+p_temp[k];
		k++;
	}
	while(c_temp[k]!="\0");
	return p;
}


string Encrypt_All_Data_CTR_ANSIX923(string m)//CTR模式加密，ANSI X.923方式填充
{
	string c="";
	int k=0;
	for(int i=0;i<100;i++)
		ctr_temp[i]="";
	do
	{
		m_temp[k]=m.substr(8*k,8);
		//比特填充
		if(m_temp[k].length()<8)
		{
			int count_sub=8-m_temp[k].length();
			if(count_sub-1>0)
				m_temp[k].append(count_sub-1,0x00);
			m_temp[k].append(1,(char)count_sub);
		}
		//向量异或
		ctr_temp[k].append(1,(char)k);
		while(ctr_temp[k].length()<8)
			ctr_temp[k].append(1,'0');
		c_temp[k]=_3DES_Encrypt(ctr_temp[k]);
		for(int i=0;i<8;i++)
			c_temp[k][i]=c_temp[k][i]^m_temp[k][i];
		c_temp[k]=c_temp[k].substr(0,8);
		c=c+c_temp[k];
		k++;
	}
	while(8*k<m.length());
	c_temp[k]="\0";
	return c;
}

string Decrypt_All_Data_CTR_ANSIX923(string c)//CTR模式解密，ANSI X.923方式填充
{
	int k=0;
	string p="";
	for(int i=0;i<100;i++)
		p_temp[i]="";
	do
	{
		m_temp[k]=_3DES_Encrypt(ctr_temp[k]);
		for(int i=0;i<8;i++)
			p_temp[k].append(1,(char)(m_temp[k][i]^c_temp[k][i]));
		//比特填充
		for(i=0;i<p_temp[k].length();i++)
			if(p_temp[k][i]<0x08) 
				p_temp[k]=p_temp[k].substr(0,i);
		p=p+p_temp[k];
		k++;
	}
	while(c_temp[k]!="\0");
	return p;
}

string Encrypt_All_Data_CTR_ISO10126(string m)//CTR模式加密，ISO 10126方式填充
{
	string c="";
	int k=0;
	for(int i=0;i<100;i++)
		ctr_temp[i]="";
	do
	{
		m_temp[k]=m.substr(8*k,8);
		//比特填充
		if(m_temp[k].length()<8)
		{
			int count_sub=8-m_temp[k].length();
			if(count_sub-1>0)
			{
				srand((unsigned)time(NULL));
				unsigned int fill=rand()%256;
				m_temp[k].append(count_sub-1,(char)fill);
			}
			m_temp[k].append(1,(char)count_sub);
		}

		//向量异或
		ctr_temp[k].append(1,(char)k);
		while(ctr_temp[k].length()<8)
			ctr_temp[k].append(1,'0');
		c_temp[k]=_3DES_Encrypt(ctr_temp[k]);
		for(int i=0;i<8;i++)
			c_temp[k][i]=c_temp[k][i]^m_temp[k][i];
		c_temp[k]=c_temp[k].substr(0,8);
		c=c+c_temp[k];
		k++;
	}
	while(8*k<m.length());
	c_temp[k]="\0";
	return c;
}

string Decrypt_All_Data_CTR_ISO10126(string c)//CTR模式解密，ISO 10126方式填充
{
	int k=0;
	string p="";
	for(int i=0;i<100;i++)
		p_temp[i]="";
	do
	{
		m_temp[k]=_3DES_Encrypt(ctr_temp[k]);
		for(int i=0;i<8;i++)
			p_temp[k].append(1,(char)(m_temp[k][i]^c_temp[k][i]));
		//比特填充
		for(i=0;i<p_temp[k].length();i++)
			if(p_temp[k][i]<0x08) 
				p_temp[k]=p_temp[k].substr(0,p_temp[k].length()-(int)p_temp[k][i]);
		p=p+p_temp[k];
		k++;
	}
	while(c_temp[k]!="\0");
	return p;
}


int main()

{
/*
unsigned char pt[PACKETLEN+1];//明文
unsigned char ct[PACKETLEN+1];//密文
//中间量
unsigned char temp1[PACKETLEN+1];
unsigned char temp2[PACKETLEN+1];
//密钥
unsigned char key1[8]={'a','a','a','a','a','a','a','a'};//1
unsigned char key2[8]={'a','a','a','a','a','a','a','a'};//2
unsigned char key3[8]={'a','a','a','a','a','a','a','a'};//3

	int i = 0;
	int in_len = PACKETLEN;//长度
	time_t t;                 //这两行保证每次产生的随机数不同
    srand( (unsigned) time( &t ) ); 

	pt[in_len] = '\0';
	//明文通过随机数产生
	for(i=0;i<in_len;i++)
		pt[i] = 97+rand()*26/RAND_MAX;
	printf("Origin Data:%s\n",pt);
	//密钥通过随机数产生
	for(i=0;i<in_len;i++)
	{
		key1[i] = 97+rand()*26/RAND_MAX;
		key2[i] = 97+rand()*26/RAND_MAX;
		key3[i] = 97+rand()*26/RAND_MAX;
	}

des_key skey1,skey2,skey3;
//结尾标记
pt[in_len]=ct[in_len]='\0';
temp1[in_len]=temp2[in_len]='\0';
//初始化
des_setup((unsigned char*)key1,PACKETLEN,0,&skey1);
des_setup((unsigned char*)key2,PACKETLEN,0,&skey2);
des_setup((unsigned char*)key3,PACKETLEN,0,&skey3);
//加密过程
des_ecb_encrypt(pt,temp1,&skey1);//k1加密
des_ecb_decrypt(temp1,temp2,&skey2);//k2解密
des_ecb_encrypt(temp2,ct,&skey3);//k3加密
//解密过程
des_ecb_decrypt(ct,temp2,&skey3);//k3解密
des_ecb_encrypt(temp2,temp1,&skey2);//k2加密
des_ecb_decrypt(temp1,pt,&skey1);//k1解密

printf("明文%s\n",pt);

printf("密文%s\n",ct);

printf("恢复明文%s\n",pt); 
*/

Inintialize_Encrypt_Decrypt("abcsssss","defuuuuu","uuutghtg");
string m_t;//="yyuuip112wweqweqwewfscx";
cout<<"输入明文:";
cin>>m_t;
cout<<"明文:"<<endl<<m_t<<endl;


string c_t="";
string p_t="";
int k=0;
/*
do
{
	m_temp[k]=m_t.substr(8*k,8);
	c_temp[k]=_3DES_Encrypt(m_temp[k]);
	c_t=c_t+c_temp[k];
	k++;
}
while(8*k<m_t.length());
*/
//string c_t="";
/*
k=0;
do
{
	//q_temp[100]=c_t.substr(8*k,8);
	p_temp[k]=_3DES_Decrypt(c_temp[k]);
	p_t=p_t+p_temp[k];
	k++;
}
while(8*k<m_t.length());
*/




/*

cout<<"密文:"<<endl;
c_t=Encrypt_All_Data_ECB_ZERO(m_t);
cout<<c_t<<endl;


p_t=Decrypt_All_Data_ECB_ZERO(c_t);

cout<<"恢复明文:"<<endl<<p_t<<endl;

cout<<"密文:"<<endl;
c_t=Encrypt_All_Data_ECB_PKCS7(m_t);
cout<<c_t<<endl;


p_t=Decrypt_All_Data_ECB_PKCS7(c_t);

cout<<"恢复明文:"<<endl<<p_t<<endl;
cout<<"密文:"<<endl;
c_t=Encrypt_All_Data_ECB_ANSIX923(m_t);
cout<<c_t<<endl;


p_t=Decrypt_All_Data_ECB_ANSIX923(c_t);

cout<<"恢复明文:"<<endl<<p_t<<endl;
cout<<"密文:"<<endl;
c_t=Encrypt_All_Data_ECB_ISO10126(m_t);
cout<<c_t<<endl;


p_t=Decrypt_All_Data_ECB_ISO10126(c_t);

cout<<"恢复明文:"<<endl<<p_t<<endl;

cout<<"密文:"<<endl;
c_t=Encrypt_All_Data_CBC_ZERO(m_t);
cout<<c_t<<endl;


p_t=Decrypt_All_Data_CBC_ZERO(c_t);

cout<<"恢复明文:"<<endl<<p_t<<endl;

cout<<"密文:"<<endl;
c_t=Encrypt_All_Data_CBC_PKCS7(m_t);
cout<<c_t<<endl;


p_t=Decrypt_All_Data_CBC_PKCS7(c_t);

cout<<"恢复明文:"<<endl<<p_t<<endl;
cout<<"密文:"<<endl;
c_t=Encrypt_All_Data_CBC_ANSIX923(m_t);
cout<<c_t<<endl;


p_t=Decrypt_All_Data_CBC_ANSIX923(c_t);

cout<<"恢复明文:"<<endl<<p_t<<endl;
cout<<"密文:"<<endl;
c_t=Encrypt_All_Data_CBC_ISO10126(m_t);
cout<<c_t<<endl;


p_t=Decrypt_All_Data_CBC_ISO10126(c_t);

cout<<"恢复明文:"<<endl<<p_t<<endl;


cout<<"密文:"<<endl;
c_t=Encrypt_All_Data_CFB_ZERO(m_t);
cout<<c_t<<endl;


p_t=Decrypt_All_Data_CFB_ZERO(c_t);

cout<<"恢复明文:"<<endl<<p_t<<endl;

cout<<"密文:"<<endl;
c_t=Encrypt_All_Data_CFB_PKCS7(m_t);
cout<<c_t<<endl;


p_t=Decrypt_All_Data_CFB_PKCS7(c_t);

cout<<"恢复明文:"<<endl<<p_t<<endl;
cout<<"密文:"<<endl;
c_t=Encrypt_All_Data_CFB_ANSIX923(m_t);
cout<<c_t<<endl;


p_t=Decrypt_All_Data_CFB_ANSIX923(c_t);

cout<<"恢复明文:"<<endl<<p_t<<endl;
cout<<"密文:"<<endl;
c_t=Encrypt_All_Data_CFB_ISO10126(m_t);
cout<<c_t<<endl;


p_t=Decrypt_All_Data_CFB_ISO10126(c_t);

cout<<"恢复明文:"<<endl<<p_t<<endl;



cout<<"密文:"<<endl;
c_t=Encrypt_All_Data_OFB_ZERO(m_t);
cout<<c_t<<endl;


p_t=Decrypt_All_Data_OFB_ZERO(c_t);

cout<<"恢复明文:"<<endl<<p_t<<endl;


cout<<"密文:"<<endl;
c_t=Encrypt_All_Data_OFB_PKCS7(m_t);
cout<<c_t<<endl;


p_t=Decrypt_All_Data_OFB_PKCS7(c_t);

cout<<"恢复明文:"<<endl<<p_t<<endl;

cout<<"密文:"<<endl;
c_t=Encrypt_All_Data_OFB_ANSIX923(m_t);
cout<<c_t<<endl;


p_t=Decrypt_All_Data_OFB_ANSIX923(c_t);

cout<<"恢复明文:"<<endl<<p_t<<endl;

cout<<"密文:"<<endl;
c_t=Encrypt_All_Data_OFB_ISO10126(m_t);
cout<<c_t<<endl;


p_t=Decrypt_All_Data_OFB_ISO10126(c_t);

cout<<"恢复明文:"<<endl<<p_t<<endl;
cout<<endl<<endl;
for(int i=0;i<8;i++)
	cout<<hex<<(int)IV[i]<<"\t";

*/
cout<<"密文:"<<endl;
c_t=Encrypt_All_Data_CTR_ISO10126(m_t);
cout<<c_t<<endl;


p_t=Decrypt_All_Data_CTR_ISO10126(c_t);

cout<<"恢复明文:"<<endl<<p_t<<endl;

system("PAUSE");

return 0;

}

