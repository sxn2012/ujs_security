
// Encrypt_Decrypt_MFCDlg.cpp : 实现文件
//
#ifndef _CRT_SECURE_NO_WARNINGS
#define _CRT_SECURE_NO_WARNINGS 1//去除警告提示
#endif
#include "stdafx.h"
#include "Encrypt_Decrypt_MFC.h"
#include "Encrypt_Decrypt_MFCDlg.h"
#include "afxdialogex.h"
#include "des.h"

#include <string>
#ifdef _DEBUG
#define new DEBUG_NEW
#endif
//加解密代码


void CEncrypt_Decrypt_MFCDlg::_3DES_Startup(string k1, string k2, string k3)//3des密钥初始化
{
	srand((unsigned)time(NULL));
	for (int i = 0; i < 8; i++)
		//IV[i] = (unsigned char)(rand() % 255 + 1);
		IV[i] = (unsigned char)((i+2)*16+(i+5));//生成随机向量IV
	//清空中间变量
	for (int i = 0; i < 100; i++)
	{
		m_temp[i] = "";
		c_temp[i] = "";
		p_temp[i] = "";
		ctr_temp[i] = "";
	}
	//string转换成unsigned char*类型
	int n1 = k1.length();
	int n2 = k2.length();
	int n3 = k3.length();
	unsigned char *key1 = new unsigned char[8];
	unsigned char *key2 = new unsigned char[8];
	unsigned char *key3 = new unsigned char[8];
	strcpy((char*)key1, k1.c_str());
	strcpy((char*)key2, k2.c_str());
	strcpy((char*)key3, k3.c_str());
	key1[7] = 0;
	key2[7] = 0;
	key3[7] = 0;
	des_setup((unsigned char*)key1, 8, 0, &s_k1);//初始化密钥k1
	des_setup((unsigned char*)key2, 8, 0, &s_k2);//初始化密钥k2
	des_setup((unsigned char*)key3, 8, 0, &s_k3);//初始化密钥k3
}

string CEncrypt_Decrypt_MFCDlg::_3DES_Encrypt(string m)//3des加密函数
{
	unsigned char *pt = new unsigned char[m.length() + 1];
	unsigned char *temp1 = new unsigned char[m.length() + 1];
	unsigned char *temp2 = new unsigned char[m.length() + 1];
	unsigned char *ct = new unsigned char[m.length() + 1];
	for (int i = 0; i < m.length() + 1; i++)
	{
		pt[i] = 0;
		temp1[i] = 0;
		temp2[i] = 0;
		ct[i] = 0;
	}
	//string转换成unsigned char*类型
	strcpy((char *)pt, m.c_str());
	pt[m.length()] = 0;
	des_ecb_encrypt(pt, temp1, &s_k1);//用k1加密
	des_ecb_decrypt(temp1, temp2, &s_k2);//用k2解密
	des_ecb_encrypt(temp2, ct, &s_k3);//用k3加密
	string c((char *)ct);
	//unsigned char转换成string 类型
	return c;
}

string CEncrypt_Decrypt_MFCDlg::_3DES_Decrypt(string c)//3des解密函数
{
	unsigned char *pt = new unsigned char[c.length() + 1];
	unsigned char *temp1 = new unsigned char[c.length() + 1];
	unsigned char *temp2 = new unsigned char[c.length() + 1];
	unsigned char *ct = new unsigned char[c.length() + 1];
	for (int i = 0; i < c.length() + 1; i++)
	{
		pt[i] = 0;
		temp1[i] = 0;
		temp2[i] = 0;
		ct[i] = 0;
	}
	//string转换成unsigned char *类型
	strcpy((char *)ct, c.c_str());
	ct[c.length()] = 0;
	des_ecb_decrypt(ct, temp2, &s_k3);//用k3解密
	des_ecb_encrypt(temp2, temp1, &s_k2);//用k2加密
	des_ecb_decrypt(temp1, pt, &s_k1);//用k1解密
	//unsigned char *转换成string类型
	string m((char *)pt);
	return m;
}

void CEncrypt_Decrypt_MFCDlg::Inintialize_Encrypt_Decrypt(string s1, string s2, string s3)//3des初始化
{
	_3DES_Startup(s1, s2, s3);//调用密钥初始化函数
}


string CEncrypt_Decrypt_MFCDlg::Encrypt_All_Data_ECB_ZERO(string m)//ECB模式加密，零字节填充
{
	string c = "";
	int k = 0;

	do
	{
		m_temp[k] = m.substr(8 * k, 8);
		//比特填充
		if (m_temp[k].length() < 8)
		{
			int count_sub = 8 - m_temp[k].length();
			m_temp[k].append(count_sub, 0x00);
		}
		c_temp[k] = _3DES_Encrypt(m_temp[k]);
		c = c + c_temp[k];
		k++;
	} while (8 * k < m.length());
	c_temp[k] = "\0";
	return c;
}

string CEncrypt_Decrypt_MFCDlg::Decrypt_All_Data_ECB_ZERO(string c)//ECB模式解密，零字节填充
{
	int k = 0;
	string p = "";
	do
	{
		p_temp[k] = _3DES_Decrypt(c_temp[k]);
		//比特填充
		for (int i = 0; i < p_temp[k].length(); i++)
		if (p_temp[k][i] < 0x08)
			p_temp[k] = p_temp[k].substr(0, i);
		p = p + p_temp[k];
		k++;
	} while (c_temp[k] != "\0");
	return p;
}

string CEncrypt_Decrypt_MFCDlg::Encrypt_All_Data_ECB_PKCS7(string m)//ECB模式加密，PKCS7方式填充
{
	string c = "";
	int k = 0;

	do
	{
		m_temp[k] = m.substr(8 * k, 8);
		//比特填充
		if (m_temp[k].length() < 8)
		{
			int count_sub = 8 - m_temp[k].length();
			m_temp[k].append(count_sub, (char)count_sub);
		}
		c_temp[k] = _3DES_Encrypt(m_temp[k]);
		c = c + c_temp[k];
		k++;
	} while (8 * k < m.length());
	c_temp[k] = "\0";
	return c;
}

string CEncrypt_Decrypt_MFCDlg::Decrypt_All_Data_ECB_PKCS7(string c)//ECB模式解密，PKCS7方式填充
{
	int k = 0;
	string p = "";
	do
	{
		p_temp[k] = _3DES_Decrypt(c_temp[k]);
		//比特填充
		for (int i = 0; i < p_temp[k].length(); i++)
		if (p_temp[k][i] < 0x08)
			p_temp[k] = p_temp[k].substr(0, i);
		p = p + p_temp[k];
		k++;
	} while (c_temp[k] != "\0");
	return p;
}


string CEncrypt_Decrypt_MFCDlg::Encrypt_All_Data_ECB_ANSIX923(string m)//ECB模式加密，ANSI X.923方式填充
{
	string c = "";
	int k = 0;

	do
	{
		m_temp[k] = m.substr(8 * k, 8);
		//比特填充
		if (m_temp[k].length()<8)
		{
			int count_sub = 8 - m_temp[k].length();
			if (count_sub - 1>0)
				m_temp[k].append(count_sub - 1, 0x00);
			m_temp[k].append(1, (char)count_sub);
		}
		c_temp[k] = _3DES_Encrypt(m_temp[k]);
		c = c + c_temp[k];
		k++;
	} while (8 * k < m.length());
	c_temp[k] = "\0";
	return c;
}

string CEncrypt_Decrypt_MFCDlg::Decrypt_All_Data_ECB_ANSIX923(string c)//ECB模式解密，ANSI X.923方式填充
{
	int k = 0;
	string p = "";
	do
	{
		p_temp[k] = _3DES_Decrypt(c_temp[k]);
		//比特填充
		for (int i = 0; i < p_temp[k].length(); i++)
		if (p_temp[k][i] < 0x08)
			p_temp[k] = p_temp[k].substr(0, i);
		p = p + p_temp[k];
		k++;
	} while (c_temp[k] != "\0");
	return p;
}

string CEncrypt_Decrypt_MFCDlg::Encrypt_All_Data_ECB_ISO10126(string m)//ECB模式加密，ISO 10126方式填充
{
	string c = "";
	int k = 0;

	do
	{
		m_temp[k] = m.substr(8 * k, 8);
		//比特填充
		if (m_temp[k].length()<8)
		{
			int count_sub = 8 - m_temp[k].length();
			if (count_sub - 1>0)
			{
				srand((unsigned)time(NULL));
				unsigned int fill = rand() % 256;
				m_temp[k].append(count_sub - 1, (char)fill);
			}
			m_temp[k].append(1, (char)count_sub);
		}
		c_temp[k] = _3DES_Encrypt(m_temp[k]);
		c = c + c_temp[k];
		k++;
	} while (8 * k < m.length());
	c_temp[k] = "\0";
	return c;
}

string CEncrypt_Decrypt_MFCDlg::Decrypt_All_Data_ECB_ISO10126(string c)//ECB模式解密，ISO 10126方式填充
{
	int k = 0;
	string p = "";
	do
	{
		p_temp[k] = _3DES_Decrypt(c_temp[k]);
		//比特填充
		for (int i = 0; i < p_temp[k].length(); i++)
		if (p_temp[k][i] < 0x08)
			p_temp[k] = p_temp[k].substr(0, p_temp[k].length() - (int)p_temp[k][i]);
		p = p + p_temp[k];
		k++;
	} while (c_temp[k] != "\0");
	return p;
}


string CEncrypt_Decrypt_MFCDlg::Encrypt_All_Data_CBC_ZERO(string m)//CBC模式加密，零字节填充
{
	string c = "";
	int k = 0;

	do
	{
		m_temp[k] = m.substr(8 * k, 8);

		//比特填充
		if (m_temp[k].length() < 8)
		{
			int count_sub = 8 - m_temp[k].length();
			m_temp[k].append(count_sub, 0x00);
		}

		//向量异或
		if (k == 0)
		for (int i = 0; i < 8; i++)
			m_temp[k][i] = m_temp[k][i] ^ IV[i];
		else
		for (int i = 0; i < 8; i++)
			m_temp[k][i] = m_temp[k][i] ^ c_temp[k - 1][i];
		c_temp[k] = _3DES_Encrypt(m_temp[k]);
		c = c + c_temp[k];
		k++;
	} while (8 * k < m.length());
	c_temp[k] = "\0";
	return c;
}

string CEncrypt_Decrypt_MFCDlg::Decrypt_All_Data_CBC_ZERO(string c)//CBC模式解密，零字节填充
{
	int k = 0;
	string p = "";
	do
	{
		p_temp[k] = _3DES_Decrypt(c_temp[k]);

		//向量异或
		if (k == 0)
		for (int i = 0; i < 8; i++)
			p_temp[k][i] = p_temp[k][i] ^ IV[i];
		else
		for (int i = 0; i < 8; i++)
			p_temp[k][i] = p_temp[k][i] ^ c_temp[k - 1][i];
		//比特填充
		for (int i = 0; i < p_temp[k].length(); i++)
		if (p_temp[k][i] < 0x08)
			p_temp[k] = p_temp[k].substr(0, i);

		p = p + p_temp[k];
		k++;
	} while (c_temp[k] != "\0");
	return p;
}


string CEncrypt_Decrypt_MFCDlg::Encrypt_All_Data_CBC_PKCS7(string m)//CBC模式加密，PKCS7方式填充
{
	string c = "";
	int k = 0;

	do
	{
		m_temp[k] = m.substr(8 * k, 8);

		//比特填充
		if (m_temp[k].length() < 8)
		{
			int count_sub = 8 - m_temp[k].length();
			m_temp[k].append(count_sub, (char)count_sub);
		}

		//向量异或
		if (k == 0)
		for (int i = 0; i < 8; i++)
			m_temp[k][i] = m_temp[k][i] ^ IV[i];
		else
		for (int i = 0; i < 8; i++)
			m_temp[k][i] = m_temp[k][i] ^ c_temp[k - 1][i];
		c_temp[k] = _3DES_Encrypt(m_temp[k]);
		c = c + c_temp[k];
		k++;
	} while (8 * k < m.length());
	c_temp[k] = "\0";
	return c;
}

string CEncrypt_Decrypt_MFCDlg::Decrypt_All_Data_CBC_PKCS7(string c)//CBC模式解密，PKCS7方式填充
{
	int k = 0;
	string p = "";
	do
	{
		p_temp[k] = _3DES_Decrypt(c_temp[k]);

		//向量异或
		if (k == 0)
		for (int i = 0; i < 8; i++)
			p_temp[k][i] = p_temp[k][i] ^ IV[i];
		else
		for (int i = 0; i < 8; i++)
			p_temp[k][i] = p_temp[k][i] ^ c_temp[k - 1][i];
		//比特填充
		for (int i = 0; i < p_temp[k].length(); i++)
		if (p_temp[k][i] < 0x08)
			p_temp[k] = p_temp[k].substr(0, i);

		p = p + p_temp[k];
		k++;
	} while (c_temp[k] != "\0");
	return p;
}

string CEncrypt_Decrypt_MFCDlg::Encrypt_All_Data_CBC_ANSIX923(string m)//CBC模式加密，ANSI X.923方式填充
{
	string c = "";
	int k = 0;

	do
	{
		m_temp[k] = m.substr(8 * k, 8);

		//比特填充
		if (m_temp[k].length()<8)
		{
			int count_sub = 8 - m_temp[k].length();
			if (count_sub - 1>0)
				m_temp[k].append(count_sub - 1, 0x00);
			m_temp[k].append(1, (char)count_sub);
		}

		//向量异或
		if (k == 0)
		for (int i = 0; i < 8; i++)
			m_temp[k][i] = m_temp[k][i] ^ IV[i];
		else
		for (int i = 0; i < 8; i++)
			m_temp[k][i] = m_temp[k][i] ^ c_temp[k - 1][i];
		c_temp[k] = _3DES_Encrypt(m_temp[k]);
		c = c + c_temp[k];
		k++;
	} while (8 * k < m.length());
	c_temp[k] = "\0";
	return c;
}

string CEncrypt_Decrypt_MFCDlg::Decrypt_All_Data_CBC_ANSIX923(string c)//CBC模式解密，ANSI X.923方式填充
{
	int k = 0;
	string p = "";
	do
	{
		p_temp[k] = _3DES_Decrypt(c_temp[k]);

		//向量异或
		if (k == 0)
		for (int i = 0; i < 8; i++)
			p_temp[k][i] = p_temp[k][i] ^ IV[i];
		else
		for (int i = 0; i < 8; i++)
			p_temp[k][i] = p_temp[k][i] ^ c_temp[k - 1][i];
		//比特填充
		for (int i = 0; i < p_temp[k].length(); i++)
		if (p_temp[k][i] < 0x08)
			p_temp[k] = p_temp[k].substr(0, i);

		p = p + p_temp[k];
		k++;
	} while (c_temp[k] != "\0");
	return p;
}

string CEncrypt_Decrypt_MFCDlg::Encrypt_All_Data_CBC_ISO10126(string m)//CBC模式加密，ISO 10126方式填充
{
	string c = "";
	int k = 0;

	do
	{
		m_temp[k] = m.substr(8 * k, 8);

		//比特填充
		if (m_temp[k].length()<8)
		{
			int count_sub = 8 - m_temp[k].length();
			if (count_sub - 1>0)
			{
				srand((unsigned)time(NULL));
				unsigned int fill = rand() % 256;
				m_temp[k].append(count_sub - 1, (char)fill);
			}
			m_temp[k].append(1, (char)count_sub);
		}

		//向量异或
		if (k == 0)
		for (int i = 0; i < 8; i++)
			m_temp[k][i] = m_temp[k][i] ^ IV[i];
		else
		for (int i = 0; i < 8; i++)
			m_temp[k][i] = m_temp[k][i] ^ c_temp[k - 1][i];
		c_temp[k] = _3DES_Encrypt(m_temp[k]);
		c = c + c_temp[k];
		k++;
	} while (8 * k < m.length());
	c_temp[k] = "\0";
	return c;


}

string CEncrypt_Decrypt_MFCDlg::Decrypt_All_Data_CBC_ISO10126(string c)//CBC模式解密，ISO 10126方式填充
{
	int k = 0;
	string p = "";
	do
	{
		p_temp[k] = _3DES_Decrypt(c_temp[k]);

		//向量异或
		if (k == 0)
		for (int i = 0; i < 8; i++)
			p_temp[k][i] = p_temp[k][i] ^ IV[i];
		else
		for (int i = 0; i < 8; i++)
			p_temp[k][i] = p_temp[k][i] ^ c_temp[k - 1][i];
		//比特填充
		for (int i = 0; i < p_temp[k].length(); i++)
		if (p_temp[k][i] < 0x08)
			p_temp[k] = p_temp[k].substr(0, p_temp[k].length() - (int)p_temp[k][i]);

		p = p + p_temp[k];
		k++;
	} while (c_temp[k] != "\0");
	return p;


}


string CEncrypt_Decrypt_MFCDlg::Encrypt_All_Data_CFB_ZERO(string m)//CFB模式加密，零字节填充
{
	string c = "";
	int k = 0;

	do
	{
		m_temp[k] = m.substr(8 * k, 8);

		//比特填充
		if (m_temp[k].length() < 8)
		{
			int count_sub = 8 - m_temp[k].length();
			m_temp[k].append(count_sub, 0x00);
		}

		//向量异或
		string iv((char *)IV);
		if (k == 0)
			c_temp[k] = _3DES_Encrypt(iv);
		else
			c_temp[k] = _3DES_Encrypt(c_temp[k - 1]);
		for (int i = 0; i < 8; i++)
			c_temp[k][i] = c_temp[k][i] ^ m_temp[k][i];
		c = c + c_temp[k];
		k++;
	} while (8 * k < m.length());
	c_temp[k] = "\0";
	return c;


}

string CEncrypt_Decrypt_MFCDlg::Decrypt_All_Data_CFB_ZERO(string c)//CFB模式解密，零字节填充
{
	int k = 0;
	string p = "";
	do
	{
		string iv((char *)IV);
		if (k == 0)
			p_temp[k] = _3DES_Encrypt(iv);
		else
			p_temp[k] = _3DES_Encrypt(c_temp[k - 1]);
		p_temp[k] = p_temp[k].substr(0, 8);
		//向量异或
		for (int i = 0; i < 8; i++)
			p_temp[k][i] = p_temp[k][i] ^ c_temp[k][i];
		//比特填充
		for (int i = 0; i < p_temp[k].length(); i++)
		if (p_temp[k][i] < 0x08)
			p_temp[k] = p_temp[k].substr(0, i);

		p = p + p_temp[k];
		k++;
	} while (c_temp[k] != "\0");
	return p;


}


string CEncrypt_Decrypt_MFCDlg::Encrypt_All_Data_CFB_PKCS7(string m)//CFB模式加密，PKCS7方式填充
{
	string c = "";
	int k = 0;

	do
	{
		m_temp[k] = m.substr(8 * k, 8);

		//比特填充
		if (m_temp[k].length() < 8)
		{
			int count_sub = 8 - m_temp[k].length();
			m_temp[k].append(count_sub, (char)count_sub);
		}

		//向量异或
		string iv((char *)IV);
		if (k == 0)
			c_temp[k] = _3DES_Encrypt(iv);
		else
			c_temp[k] = _3DES_Encrypt(c_temp[k - 1]);
		for (int i = 0; i < 8; i++)
			c_temp[k][i] = c_temp[k][i] ^ m_temp[k][i];
		c = c + c_temp[k];
		k++;
	} while (8 * k < m.length());
	c_temp[k] = "\0";
	return c;


}

string CEncrypt_Decrypt_MFCDlg::Decrypt_All_Data_CFB_PKCS7(string c)//CFB模式解密，PKCS7方式填充
{
	int k = 0;
	string p = "";
	do
	{
		string iv((char *)IV);
		if (k == 0)
			p_temp[k] = _3DES_Encrypt(iv);
		else
			p_temp[k] = _3DES_Encrypt(c_temp[k - 1]);
		p_temp[k] = p_temp[k].substr(0, 8);
		//向量异或
		for (int i = 0; i < 8; i++)
			p_temp[k][i] = p_temp[k][i] ^ c_temp[k][i];
		//比特填充
		for (int i = 0; i < p_temp[k].length(); i++)
		if (p_temp[k][i] < 0x08)
			p_temp[k] = p_temp[k].substr(0, i);

		p = p + p_temp[k];
		k++;
	} while (c_temp[k] != "\0");
	return p;


}


string CEncrypt_Decrypt_MFCDlg::Encrypt_All_Data_CFB_ANSIX923(string m)//CFB模式加密，ANSI X.923方式填充
{
	string c = "";
	int k = 0;

	do
	{
		m_temp[k] = m.substr(8 * k, 8);

		//比特填充
		if (m_temp[k].length()<8)
		{
			int count_sub = 8 - m_temp[k].length();
			if (count_sub - 1>0)
				m_temp[k].append(count_sub - 1, 0x00);
			m_temp[k].append(1, (char)count_sub);
		}

		//向量异或
		string iv((char *)IV);
		if (k == 0)
			c_temp[k] = _3DES_Encrypt(iv);
		else
			c_temp[k] = _3DES_Encrypt(c_temp[k - 1]);
		for (int i = 0; i < 8; i++)
			c_temp[k][i] = c_temp[k][i] ^ m_temp[k][i];
		c = c + c_temp[k];
		k++;
	} while (8 * k < m.length());
	c_temp[k] = "\0";
	return c;
}

string CEncrypt_Decrypt_MFCDlg::Decrypt_All_Data_CFB_ANSIX923(string c)//CFB模式解密，ANSI X.923方式填充
{
	int k = 0;
	string p = "";
	do
	{
		string iv((char *)IV);
		if (k == 0)
			p_temp[k] = _3DES_Encrypt(iv);
		else
			p_temp[k] = _3DES_Encrypt(c_temp[k - 1]);
		p_temp[k] = p_temp[k].substr(0, 8);
		//向量异或
		for (int i = 0; i < 8; i++)
			p_temp[k][i] = p_temp[k][i] ^ c_temp[k][i];
		//比特填充
		for (int i = 0; i < p_temp[k].length(); i++)
		if (p_temp[k][i] < 0x08)
			p_temp[k] = p_temp[k].substr(0, i);

		p = p + p_temp[k];
		k++;
	} while (c_temp[k] != "\0");
	return p;
}


string CEncrypt_Decrypt_MFCDlg::Encrypt_All_Data_CFB_ISO10126(string m)//CFB模式加密，ISO 10126方式填充
{
	string c = "";
	int k = 0;

	do
	{
		m_temp[k] = m.substr(8 * k, 8);

		//比特填充
		if (m_temp[k].length()<8)
		{
			int count_sub = 8 - m_temp[k].length();
			if (count_sub - 1>0)
			{
				srand((unsigned)time(NULL));
				unsigned int fill = rand() % 256;
				m_temp[k].append(count_sub - 1, (char)fill);
			}
			m_temp[k].append(1, (char)count_sub);
		}

		//向量异或
		string iv((char *)IV);
		if (k == 0)
			c_temp[k] = _3DES_Encrypt(iv);
		else
			c_temp[k] = _3DES_Encrypt(c_temp[k - 1]);

		for (int i = 0; i < 8; i++)
			c_temp[k][i] = c_temp[k][i] ^ m_temp[k][i];
		c = c + c_temp[k];
		k++;
	} while (8 * k < m.length());
	c_temp[k] = "\0";
	return c;
}

string CEncrypt_Decrypt_MFCDlg::Decrypt_All_Data_CFB_ISO10126(string c)//CFB模式解密，ISO 10126方式填充
{
	int k = 0;
	string p = "";
	do
	{
		string iv((char *)IV);
		if (k == 0)
			p_temp[k] = _3DES_Encrypt(iv);
		else
			p_temp[k] = _3DES_Encrypt(c_temp[k - 1]);
		p_temp[k] = p_temp[k].substr(0, 8);
		//向量异或
		for (int i = 0; i < 8; i++)
			p_temp[k][i] = p_temp[k][i] ^ c_temp[k][i];
		//比特填充
		for (int i = 0; i < p_temp[k].length(); i++)
		if (p_temp[k][i] < 0x08)
			p_temp[k] = p_temp[k].substr(0, p_temp[k].length() - (int)p_temp[k][i]);

		p = p + p_temp[k];
		k++;
	} while (c_temp[k] != "\0");
	return p;
}


string CEncrypt_Decrypt_MFCDlg::Encrypt_All_Data_OFB_ZERO(string m)//OFB模式加密，零字节填充
{
	string c = "";
	int k = 0;
	for (int i = 0; i < 100; i++)
		c_temp[i] = "";
	do
	{
		m_temp[k] = m.substr(8 * k, 8);

		//比特填充
		if (m_temp[k].length() < 8)
		{
			int count_sub = 8 - m_temp[k].length();
			m_temp[k].append(count_sub, 0x00);
		}

		//向量异或
		string iv((char *)IV);
		string key = iv;
		for (int i = 0; i < k + 1; i++)
			key = _3DES_Encrypt(key);

		for (int i = 0; i < 8; i++)
			c_temp[k].append(1, (char)key[i] ^ m_temp[k][i]);
		c = c + c_temp[k];
		k++;
	} while (8 * k < m.length());
	c_temp[k] = "\0";
	return c;
}

string CEncrypt_Decrypt_MFCDlg::Decrypt_All_Data_OFB_ZERO(string c)//OFB模式解密，零字节填充
{
	int k = 0;
	string p = "";
	for (int i = 0; i < 100; i++)
		p_temp[i] = "";
	do
	{
		string iv((char *)IV);
		string key = iv;
		for (int i = 0; i < k + 1; i++)
			key = _3DES_Encrypt(key);
		key = key.substr(0, 8);
		//向量异或

		for (int i = 0; i < 8; i++)
			p_temp[k].append(1, key[i] ^ c_temp[k][i]);
		//比特填充
		for (int i = 0; i < p_temp[k].length(); i++)
		if (p_temp[k][i] < 0x08)
			p_temp[k] = p_temp[k].substr(0, i);

		p = p + p_temp[k];
		k++;
	} while (c_temp[k] != "\0");
	return p;
}


string CEncrypt_Decrypt_MFCDlg::Encrypt_All_Data_OFB_PKCS7(string m)//OFB模式加密，PKCS7方式填充
{
	string c = "";
	int k = 0;
	for (int i = 0; i < 100; i++)
		c_temp[i] = "";
	do
	{
		m_temp[k] = m.substr(8 * k, 8);

		//比特填充
		if (m_temp[k].length() < 8)
		{
			int count_sub = 8 - m_temp[k].length();
			m_temp[k].append(count_sub, (char)count_sub);
		}

		//向量异或
		string iv((char *)IV);
		string key = iv;
		for (int i = 0; i < k + 1; i++)
			key = _3DES_Encrypt(key);

		for (int i = 0; i < 8; i++)
			c_temp[k].append(1, (char)key[i] ^ m_temp[k][i]);
		c = c + c_temp[k];
		k++;
	} while (8 * k < m.length());
	c_temp[k] = "\0";
	return c;
}

string CEncrypt_Decrypt_MFCDlg::Decrypt_All_Data_OFB_PKCS7(string c)//OFB模式解密，PKCS7方式填充
{
	int k = 0;
	string p = "";
	for (int i = 0; i < 100; i++)
		p_temp[i] = "";
	do
	{
		string iv((char *)IV);
		string key = iv;
		for (int i = 0; i < k + 1; i++)
			key = _3DES_Encrypt(key);
		key = key.substr(0, 8);
		//向量异或

		for (int i = 0; i < 8; i++)
			p_temp[k].append(1, key[i] ^ c_temp[k][i]);
		//比特填充
		for (int i = 0; i < p_temp[k].length(); i++)
		if (p_temp[k][i] < 0x08)
			p_temp[k] = p_temp[k].substr(0, i);

		p = p + p_temp[k];
		k++;
	} while (c_temp[k] != "\0");
	return p;
}

string CEncrypt_Decrypt_MFCDlg::Encrypt_All_Data_OFB_ANSIX923(string m)//OFB模式加密，ANSI X.923方式填充
{
	string c = "";
	int k = 0;
	for (int i = 0; i < 100; i++)
		c_temp[i] = "";
	do
	{
		m_temp[k] = m.substr(8 * k, 8);

		//比特填充
		if (m_temp[k].length()<8)
		{
			int count_sub = 8 - m_temp[k].length();
			if (count_sub - 1>0)
				m_temp[k].append(count_sub - 1, 0x00);
			m_temp[k].append(1, (char)count_sub);
		}

		//向量异或
		string iv((char *)IV);
		string key = iv;
		for (int i = 0; i < k + 1; i++)
			key = _3DES_Encrypt(key);

		for (int i = 0; i < 8; i++)
			c_temp[k].append(1, (char)key[i] ^ m_temp[k][i]);
		c = c + c_temp[k];
		k++;
	} while (8 * k < m.length());
	c_temp[k] = "\0";
	return c;
}

string CEncrypt_Decrypt_MFCDlg::Decrypt_All_Data_OFB_ANSIX923(string c)//OFB模式解密，ANSI X.923方式填充
{
	int k = 0;
	string p = "";
	for (int i = 0; i < 100; i++)
		p_temp[i] = "";
	do
	{
		string iv((char *)IV);
		string key = iv;
		for (int i = 0; i < k + 1; i++)
			key = _3DES_Encrypt(key);
		key = key.substr(0, 8);
		//向量异或

		for (int i = 0; i < 8; i++)
			p_temp[k].append(1, key[i] ^ c_temp[k][i]);
		//比特填充
		for (int i = 0; i < p_temp[k].length(); i++)
		if (p_temp[k][i] < 0x08)
			p_temp[k] = p_temp[k].substr(0, i);

		p = p + p_temp[k];
		k++;
	} while (c_temp[k] != "\0");
	return p;
}

string CEncrypt_Decrypt_MFCDlg::Encrypt_All_Data_OFB_ISO10126(string m)//OFB模式加密，ISO 10126方式填充
{
	string c = "";
	int k = 0;
	for (int i = 0; i < 100; i++)
		c_temp[i] = "";
	do
	{
		m_temp[k] = m.substr(8 * k, 8);

		//比特填充
		if (m_temp[k].length()<8)
		{
			int count_sub = 8 - m_temp[k].length();
			if (count_sub - 1>0)
			{
				srand((unsigned)time(NULL));
				unsigned int fill = rand() % 256;
				m_temp[k].append(count_sub - 1, (char)fill);
			}
			m_temp[k].append(1, (char)count_sub);
		}

		//向量异或
		string iv((char *)IV);
		string key = iv;
		for (int i = 0; i < k + 1; i++)
			key = _3DES_Encrypt(key);

		for (int i = 0; i < 8; i++)
			c_temp[k].append(1, (char)key[i] ^ m_temp[k][i]);
		c = c + c_temp[k];
		k++;
	} while (8 * k < m.length());
	c_temp[k] = "\0";
	return c;
}

string CEncrypt_Decrypt_MFCDlg::Decrypt_All_Data_OFB_ISO10126(string c)//OFB模式解密，ISO 10126方式填充
{
	int k = 0;
	string p = "";
	for (int i = 0; i < 100; i++)
		p_temp[i] = "";
	do
	{
		string iv((char *)IV);
		string key = iv;
		for (int i = 0; i < k + 1; i++)
			key = _3DES_Encrypt(key);
		key = key.substr(0, 8);
		//向量异或

		for (int i = 0; i < 8; i++)
			p_temp[k].append(1, key[i] ^ c_temp[k][i]);
		//比特填充
		for (int i = 0; i < p_temp[k].length(); i++)
		if (p_temp[k][i] < 0x08)
			p_temp[k] = p_temp[k].substr(0, p_temp[k].length() - (int)p_temp[k][i]);


		p = p + p_temp[k];
		k++;
	} while (c_temp[k] != "\0");
	return p;
}



string CEncrypt_Decrypt_MFCDlg::Encrypt_All_Data_CTR_ZERO(string m)//CTR模式加密，零字节填充
{
	string c = "";
	int k = 0;
	for (int i = 0; i < 100; i++)
		ctr_temp[i] = "";
	do
	{
		m_temp[k] = m.substr(8 * k, 8);
		//比特填充
		if (m_temp[k].length() < 8)
		{
			int count_sub = 8 - m_temp[k].length();
			m_temp[k].append(count_sub, 0x00);
		}
		//向量异或
		ctr_temp[k].append(1, (char)k);
		while (ctr_temp[k].length() < 8)
			ctr_temp[k].append(1, '0');
		c_temp[k] = _3DES_Encrypt(ctr_temp[k]);
		for (int i = 0; i < 8; i++)
			c_temp[k][i] = c_temp[k][i] ^ m_temp[k][i];
		c_temp[k] = c_temp[k].substr(0, 8);
		c = c + c_temp[k];
		k++;
	} while (8 * k < m.length());
	c_temp[k] = "\0";
	return c;
}

string CEncrypt_Decrypt_MFCDlg::Decrypt_All_Data_CTR_ZERO(string c)//CTR模式解密，零字节填充
{
	int k = 0;
	string p = "";
	for (int i = 0; i < 100; i++)
		p_temp[i] = "";
	do
	{
		m_temp[k] = _3DES_Encrypt(ctr_temp[k]);
		for (int i = 0; i < 8; i++)
			p_temp[k].append(1, (char)(m_temp[k][i] ^ c_temp[k][i]));
		//比特填充
		for (int i = 0; i < p_temp[k].length(); i++)
		if (p_temp[k][i] < 0x08)
			p_temp[k] = p_temp[k].substr(0, i);
		p = p + p_temp[k];
		k++;
	} while (c_temp[k] != "\0");
	return p;
}

string CEncrypt_Decrypt_MFCDlg::Encrypt_All_Data_CTR_PKCS7(string m)//CTR模式加密，PKCS7方式填充
{
	string c = "";
	int k = 0;
	for (int i = 0; i < 100; i++)
		ctr_temp[i] = "";
	do
	{
		m_temp[k] = m.substr(8 * k, 8);
		//比特填充
		if (m_temp[k].length() < 8)
		{
			int count_sub = 8 - m_temp[k].length();
			m_temp[k].append(count_sub, (char)count_sub);
		}
		//向量异或
		ctr_temp[k].append(1, (char)k);
		while (ctr_temp[k].length() < 8)
			ctr_temp[k].append(1, '0');
		c_temp[k] = _3DES_Encrypt(ctr_temp[k]);
		for (int i = 0; i < 8; i++)
			c_temp[k][i] = c_temp[k][i] ^ m_temp[k][i];
		c_temp[k] = c_temp[k].substr(0, 8);
		c = c + c_temp[k];
		k++;
	} while (8 * k < m.length());
	c_temp[k] = "\0";
	return c;
}

string CEncrypt_Decrypt_MFCDlg::Decrypt_All_Data_CTR_PKCS7(string c)//CTR模式解密，PKCS7方式填充
{
	int k = 0;
	string p = "";
	for (int i = 0; i < 100; i++)
		p_temp[i] = "";
	do
	{
		m_temp[k] = _3DES_Encrypt(ctr_temp[k]);
		for (int i = 0; i < 8; i++)
			p_temp[k].append(1, (char)(m_temp[k][i] ^ c_temp[k][i]));
		//比特填充
		for (int i = 0; i < p_temp[k].length(); i++)
		if (p_temp[k][i] < 0x08)
			p_temp[k] = p_temp[k].substr(0, i);
		p = p + p_temp[k];
		k++;
	} while (c_temp[k] != "\0");
	return p;
}


string CEncrypt_Decrypt_MFCDlg::Encrypt_All_Data_CTR_ANSIX923(string m)//CTR模式加密，ANSI X.923方式填充
{
	string c = "";
	int k = 0;
	for (int i = 0; i < 100; i++)
		ctr_temp[i] = "";
	do
	{
		m_temp[k] = m.substr(8 * k, 8);
		//比特填充
		if (m_temp[k].length()<8)
		{
			int count_sub = 8 - m_temp[k].length();
			if (count_sub - 1>0)
				m_temp[k].append(count_sub - 1, 0x00);
			m_temp[k].append(1, (char)count_sub);
		}
		//向量异或
		ctr_temp[k].append(1, (char)k);
		while (ctr_temp[k].length() < 8)
			ctr_temp[k].append(1, '0');
		c_temp[k] = _3DES_Encrypt(ctr_temp[k]);
		for (int i = 0; i < 8; i++)
			c_temp[k][i] = c_temp[k][i] ^ m_temp[k][i];
		c_temp[k] = c_temp[k].substr(0, 8);
		c = c + c_temp[k];
		k++;
	} while (8 * k < m.length());
	c_temp[k] = "\0";
	return c;
}

string CEncrypt_Decrypt_MFCDlg::Decrypt_All_Data_CTR_ANSIX923(string c)//CTR模式解密，ANSI X.923方式填充
{
	int k = 0;
	string p = "";
	for (int i = 0; i < 100; i++)
		p_temp[i] = "";
	do
	{
		m_temp[k] = _3DES_Encrypt(ctr_temp[k]);
		for (int i = 0; i < 8; i++)
			p_temp[k].append(1, (char)(m_temp[k][i] ^ c_temp[k][i]));
		//比特填充
		for (int i = 0; i < p_temp[k].length(); i++)
		if (p_temp[k][i] < 0x08)
			p_temp[k] = p_temp[k].substr(0, i);
		p = p + p_temp[k];
		k++;
	} while (c_temp[k] != "\0");
	return p;
}

string CEncrypt_Decrypt_MFCDlg::Encrypt_All_Data_CTR_ISO10126(string m)//CTR模式加密，ISO 10126方式填充
{
	string c = "";
	int k = 0;
	for (int i = 0; i < 100; i++)
		ctr_temp[i] = "";
	do
	{
		m_temp[k] = m.substr(8 * k, 8);
		//比特填充
		if (m_temp[k].length()<8)
		{
			int count_sub = 8 - m_temp[k].length();
			if (count_sub - 1>0)
			{
				srand((unsigned)time(NULL));
				unsigned int fill = rand() % 256;
				m_temp[k].append(count_sub - 1, (char)fill);
			}
			m_temp[k].append(1, (char)count_sub);
		}

		//向量异或
		ctr_temp[k].append(1, (char)k);
		while (ctr_temp[k].length() < 8)
			ctr_temp[k].append(1, '0');
		c_temp[k] = _3DES_Encrypt(ctr_temp[k]);
		for (int i = 0; i < 8; i++)
			c_temp[k][i] = c_temp[k][i] ^ m_temp[k][i];
		c_temp[k] = c_temp[k].substr(0, 8);
		c = c + c_temp[k];
		k++;
	} while (8 * k < m.length());
	c_temp[k] = "\0";
	return c;
}

string CEncrypt_Decrypt_MFCDlg::Decrypt_All_Data_CTR_ISO10126(string c)//CTR模式解密，ISO 10126方式填充
{
	int k = 0;
	string p = "";
	for (int i = 0; i < 100; i++)
		p_temp[i] = "";
	do
	{
		m_temp[k] = _3DES_Encrypt(ctr_temp[k]);
		for (int i = 0; i < 8; i++)
			p_temp[k].append(1, (char)(m_temp[k][i] ^ c_temp[k][i]));
		//比特填充
		for (int i = 0; i < p_temp[k].length(); i++)
		if (p_temp[k][i] < 0x08)
			p_temp[k] = p_temp[k].substr(0, p_temp[k].length() - (int)p_temp[k][i]);
		p = p + p_temp[k];
		k++;
	} while (c_temp[k] != "\0");
	return p;
}



// CEncrypt_Decrypt_MFCDlg 对话框



CEncrypt_Decrypt_MFCDlg::CEncrypt_Decrypt_MFCDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CEncrypt_Decrypt_MFCDlg::IDD, pParent)
	, m_encryptway(0)
	, m_fillway(0)
	, m_enchoose(0)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CEncrypt_Decrypt_MFCDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_COMBO_KEYBYTES, m_cb_keybytes);
	//	DDX_Control(pDX, IDC_LIST2, m_list_key);
	//  DDX_Control(pDX, IDC_RADIO1, m_encrypt_way);
}

BEGIN_MESSAGE_MAP(CEncrypt_Decrypt_MFCDlg, CDialogEx)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDOK, &CEncrypt_Decrypt_MFCDlg::OnBnClickedOk)
	ON_BN_CLICKED(IDCANCEL, &CEncrypt_Decrypt_MFCDlg::OnBnClickedCancel)
	ON_BN_CLICKED(IDSETKEY, &CEncrypt_Decrypt_MFCDlg::OnBnClickedSetkey)
	ON_BN_CLICKED(IDENCRYPT, &CEncrypt_Decrypt_MFCDlg::OnBnClickedEncrypt)
	ON_BN_CLICKED(IDDECRYPT, &CEncrypt_Decrypt_MFCDlg::OnBnClickedDecrypt)
	ON_BN_CLICKED(IDC_RADIO11, &CEncrypt_Decrypt_MFCDlg::OnBnClickedRadio11)
	ON_BN_CLICKED(IDC_RADIO12, &CEncrypt_Decrypt_MFCDlg::OnBnClickedRadio12)
	ON_BN_CLICKED(IDC_RADIO13, &CEncrypt_Decrypt_MFCDlg::OnBnClickedRadio13)
	ON_BN_CLICKED(IDC_RADIO14, &CEncrypt_Decrypt_MFCDlg::OnBnClickedRadio14)
	ON_BN_CLICKED(IDC_RADIO15, &CEncrypt_Decrypt_MFCDlg::OnBnClickedRadio15)
	ON_BN_CLICKED(IDC_RADIO21, &CEncrypt_Decrypt_MFCDlg::OnBnClickedRadio21)
	ON_BN_CLICKED(IDC_RADIO22, &CEncrypt_Decrypt_MFCDlg::OnBnClickedRadio22)
	ON_BN_CLICKED(IDC_RADIO23, &CEncrypt_Decrypt_MFCDlg::OnBnClickedRadio23)
	ON_BN_CLICKED(IDC_RADIO24, &CEncrypt_Decrypt_MFCDlg::OnBnClickedRadio24)
	ON_BN_CLICKED(IDC_RADIO31, &CEncrypt_Decrypt_MFCDlg::OnBnClickedRadio31)
	ON_BN_CLICKED(IDC_RADIO32, &CEncrypt_Decrypt_MFCDlg::OnBnClickedRadio32)
	ON_BN_CLICKED(IDCHOOSEFILE, &CEncrypt_Decrypt_MFCDlg::OnBnClickedChoosefile)
	ON_CBN_SELCHANGE(IDC_COMBO_KEYBYTES, &CEncrypt_Decrypt_MFCDlg::OnCbnSelchangeComboKeybytes)
//	ON_WM_NCHITTEST()
//ON_WM_NCXBUTTONDOWN()
//ON_WM_NCHITTEST()
ON_WM_NCHITTEST()
//ON_WM_CTLCOLOR()
ON_WM_CTLCOLOR()
ON_WM_TIMER()
//ON_EN_CHANGE(IDC_EDIT_KEY, &CEncrypt_Decrypt_MFCDlg::OnEnChangeEditKey)
END_MESSAGE_MAP()


// CEncrypt_Decrypt_MFCDlg 消息处理程序

BOOL CEncrypt_Decrypt_MFCDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// 设置此对话框的图标。  当应用程序主窗口不是对话框时，框架将自动
	//  执行此操作
	SetIcon(m_hIcon, TRUE);			// 设置大图标
	SetIcon(m_hIcon, FALSE);		// 设置小图标

	// TODO:  在此添加额外的初始化代码
	SetTimer(1, 1000, NULL);
	
	m_cb_keybytes.AddString(L"8字节");
	m_cb_keybytes.AddString(L"24字节");
	GetDlgItem(IDENCRYPT)->ShowWindow(FALSE);
	GetDlgItem(IDDECRYPT)->ShowWindow(FALSE);
	GetDlgItem(IDCHOOSEFILE)->ShowWindow(FALSE);
	GetDlgItem(IDC_BITSTRING_EDIT)->ShowWindow(FALSE);//隐藏比特流框
	GetDlgItem(IDC_STATIC7)->ShowWindow(FALSE);
	GetDlgItem(IDC_PLAIN_EDIT)->EnableWindow(FALSE);
	GetDlgItem(IDC_CONFIDENTIAL_EDIT)->EnableWindow(FALSE);
	GetDlgItem(IDC_EDIT_KEY1)->ShowWindow(FALSE);
	GetDlgItem(IDC_EDIT_KEY2)->ShowWindow(FALSE);
	GetDlgItem(IDC_EDIT_KEY3)->ShowWindow(FALSE);
	GetDlgItem(IDC_STATIC3)->ShowWindow(FALSE);
	GetDlgItem(IDC_STATIC4)->ShowWindow(FALSE);
	GetDlgItem(IDC_STATIC5)->ShowWindow(FALSE);
	GetDlgItem(IDC_PLAIN_EDIT)->ShowWindow(FALSE);//显示明文输入框
	GetDlgItem(IDC_STATIC1)->ShowWindow(FALSE);
	GetDlgItem(IDC_EDIT_KEY)->ShowWindow(FALSE);//隐藏密钥输入框
	GetDlgItem(IDSETKEY)->ShowWindow(FALSE);//隐藏设置密钥按钮
	GetDlgItem(IDC_CONFIDENTIAL_EDIT)->ShowWindow(FALSE);//隐藏密文框
	GetDlgItem(IDC_BITSTRING_EDIT)->ShowWindow(FALSE);//隐藏比特流框
	GetDlgItem(IDC_ORIGINAL_EDIT)->ShowWindow(FALSE);//隐藏原文框
	GetDlgItem(IDC_STATIC6)->ShowWindow(FALSE);
	GetDlgItem(IDC_STATIC7)->ShowWindow(FALSE);
	GetDlgItem(IDC_STATIC8)->ShowWindow(FALSE);
	//GetDlgItem(IDC_STATIC2)->ShowWindow(FALSE);
	//GetDlgItem(IDC_COMBO_KEYBYTES)->ShowWindow(FALSE);//隐藏密钥选择位数下拉框
	//GetDlgItem(IDC_ORIGINAL_EDIT)->EnableWindow(FALSE);
	GetDlgItem(IDC_BITSTRING_EDIT)->EnableWindow(FALSE);
	GetDlgItem(IDC_EDIT_KEY1)->EnableWindow(FALSE);
	GetDlgItem(IDC_EDIT_KEY2)->EnableWindow(FALSE);
	GetDlgItem(IDC_EDIT_KEY3)->EnableWindow(FALSE);
	GetDlgItem(IDC_RADIO11)->ShowWindow(FALSE);
	GetDlgItem(IDC_RADIO12)->ShowWindow(FALSE);
	GetDlgItem(IDC_RADIO13)->ShowWindow(FALSE);
	GetDlgItem(IDC_RADIO14)->ShowWindow(FALSE);
	GetDlgItem(IDC_RADIO15)->ShowWindow(FALSE);
	GetDlgItem(IDC_RADIO21)->ShowWindow(FALSE);
	GetDlgItem(IDC_RADIO22)->ShowWindow(FALSE);
	GetDlgItem(IDC_RADIO23)->ShowWindow(FALSE);
	GetDlgItem(IDC_RADIO24)->ShowWindow(FALSE);
	GetDlgItem(IDC_STATIC9)->ShowWindow(FALSE);
	GetDlgItem(IDC_STATIC10)->ShowWindow(FALSE);
	return TRUE;  // 除非将焦点设置到控件，否则返回 TRUE
}

// 如果向对话框添加最小化按钮，则需要下面的代码
//  来绘制该图标。  对于使用文档/视图模型的 MFC 应用程序，
//  这将由框架自动完成。

void CEncrypt_Decrypt_MFCDlg::OnPaint()
{
	CRect   rect;
	CPaintDC   dc(this);
	GetClientRect(rect);
	dc.FillSolidRect(rect, RGB(186,226,234));   //背景
	if (IsIconic())
	{
		CPaintDC dc(this); // 用于绘制的设备上下文

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// 使图标在工作区矩形中居中
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// 绘制图标
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}

//当用户拖动最小化窗口时系统调用此函数取得光标
//显示。
HCURSOR CEncrypt_Decrypt_MFCDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}



void CEncrypt_Decrypt_MFCDlg::OnBnClickedOk()//确认响应
{
	// TODO: Add your control notification handler code here
	
}


void CEncrypt_Decrypt_MFCDlg::OnBnClickedCancel()//取消响应
{
	// TODO: Add your control notification handler code here
	CDialogEx::OnCancel();
}


void CEncrypt_Decrypt_MFCDlg::OnBnClickedSetkey()//按下设置密钥按钮
{
	// TODO: Add your control notification handler code here
	//显示3个密钥框
	GetDlgItem(IDC_EDIT_KEY1)->ShowWindow(TRUE);
	GetDlgItem(IDC_EDIT_KEY2)->ShowWindow(TRUE);
	GetDlgItem(IDC_EDIT_KEY3)->ShowWindow(TRUE);
	GetDlgItem(IDC_STATIC3)->ShowWindow(TRUE);
	GetDlgItem(IDC_STATIC4)->ShowWindow(TRUE);
	GetDlgItem(IDC_STATIC5)->ShowWindow(TRUE);
	GetDlgItem(IDSETKEY)->ShowWindow(FALSE);//禁用设置密钥按钮
	CString CKey;
	GetDlgItem(IDC_EDIT_KEY)->GetWindowText(CKey);//获取密钥
	string skey;
	CStringA temp(CKey.GetBuffer(0));
	CKey.ReleaseBuffer();
	skey = temp.GetBuffer(0);
	temp.ReleaseBuffer();
	string key1, key2, key3;
	int nIndex = m_cb_keybytes.GetCurSel();
	CString indexText;
	m_cb_keybytes.GetLBText(nIndex, indexText);
	if (indexText==L"8字节")//选择8字节密钥
	{
		//相关提示信息
		if (skey.length()<8)
		{
			::MessageBox(NULL, L"密钥少于8字节，系统将补零！", L"提示", MB_OK|MB_ICONINFORMATION);
		}
		if (skey.length()>8)
		{
			::MessageBox(NULL, L"密钥多于8字节，系统去除多余部分！", L"提示", MB_OK | MB_ICONINFORMATION);
		}
		while (skey.length()<8)
		{
			skey.append(1, '0');//少于8字节补零
		}
		skey = skey.substr(0, 8);//多于8字节截断
		//3个密钥均为上述生成的8字节密钥
		key1 = skey;
		key2 = skey;
		key3 = skey;
	}
	else if (indexText == L"24字节")//选择24字节密钥
	{
		//相关提示信息
		if (skey.length()<24)
		{
			::MessageBox(NULL, L"密钥少于24字节，系统将补零！", L"提示", MB_OK | MB_ICONINFORMATION);
		}
		if (skey.length()>24)
		{
			::MessageBox(NULL, L"密钥多于24字节，系统去除多余部分！", L"提示", MB_OK | MB_ICONINFORMATION);
		}
		while (skey.length() < 24)//少于24字节补零
		{
			skey.append(1, '0');
		}
		skey = skey.substr(0, 24);//多于24字节截断
		//密钥1为0-7字节
		key1 = skey.substr(0, 8);
		//密钥2为8-15字节
		key2 = skey.substr(8, 8);
		//密钥3为16-23字节
		key3 = skey.substr(16, 8);
	}
	//将3个密钥转换为CString类型
	CString k1,k2,k3;
	k1 = CA2T(key1.c_str());
	k2 = CA2T(key2.c_str());
	k3 = CA2T(key3.c_str());
	Inintialize_Encrypt_Decrypt(key1, key2, key3);//初始化密钥
	//将三个密钥显示出来
	GetDlgItem(IDC_EDIT_KEY1)->SetWindowText(k1);
	GetDlgItem(IDC_EDIT_KEY2)->SetWindowText(k2);
	GetDlgItem(IDC_EDIT_KEY3)->SetWindowText(k3);
	GetDlgItem(IDSETKEY)->EnableWindow(FALSE);//禁用设置密钥按钮
	GetDlgItem(IDC_COMBO_KEYBYTES)->EnableWindow(FALSE);//禁用选择字节数选项
	GetDlgItem(IDC_EDIT_KEY)->EnableWindow(FALSE);//禁用密钥输入框
	GetDlgItem(IDENCRYPT)->ShowWindow(TRUE);//启用加密按钮
	//启用加密模式和填充模式选择键
	GetDlgItem(IDC_RADIO11)->ShowWindow(TRUE);
	GetDlgItem(IDC_RADIO12)->ShowWindow(TRUE);
	GetDlgItem(IDC_RADIO13)->ShowWindow(TRUE);
	GetDlgItem(IDC_RADIO14)->ShowWindow(TRUE);
	GetDlgItem(IDC_RADIO15)->ShowWindow(TRUE);
	GetDlgItem(IDC_RADIO21)->ShowWindow(TRUE);
	GetDlgItem(IDC_RADIO22)->ShowWindow(TRUE);
	GetDlgItem(IDC_RADIO23)->ShowWindow(TRUE);
	GetDlgItem(IDC_RADIO24)->ShowWindow(TRUE);
	GetDlgItem(IDC_STATIC9)->ShowWindow(TRUE);
	GetDlgItem(IDC_STATIC10)->ShowWindow(TRUE);

}


void CEncrypt_Decrypt_MFCDlg::OnBnClickedEncrypt()//按下加密按钮
{
	// TODO: Add your control notification handler code here
	GetDlgItem(IDSETKEY)->EnableWindow(FALSE);//禁用设置密钥按钮
	GetDlgItem(IDC_COMBO_KEYBYTES)->EnableWindow(FALSE);//禁用密钥选择框
	GetDlgItem(IDC_EDIT_KEY)->EnableWindow(FALSE);//禁用密钥输入框
	GetDlgItem(IDC_CONFIDENTIAL_EDIT)->ShowWindow(TRUE);//显示密文框
	GetDlgItem(IDC_STATIC6)->ShowWindow(TRUE);
	GetDlgItem(IDSETKEY)->ShowWindow(FALSE);//禁用设置密钥按钮
	GetDlgItem(IDC_EDIT_KEY1)->ShowWindow(FALSE);
	GetDlgItem(IDC_EDIT_KEY2)->ShowWindow(FALSE);
	GetDlgItem(IDC_EDIT_KEY3)->ShowWindow(FALSE);
	GetDlgItem(IDC_STATIC3)->ShowWindow(FALSE);
	GetDlgItem(IDC_STATIC4)->ShowWindow(FALSE);
	GetDlgItem(IDC_STATIC5)->ShowWindow(FALSE);
	if (m_enchoose == 0)
	{
		GetDlgItem(IDC_BITSTRING_EDIT)->ShowWindow(TRUE);//显示比特流框
		GetDlgItem(IDC_STATIC7)->ShowWindow(TRUE);
	}
	GetDlgItem(IDC_ORIGINAL_EDIT)->ShowWindow(FALSE);//隐藏明文框
	GetDlgItem(IDC_STATIC8)->ShowWindow(FALSE);
	//禁用加密模式和填充模式选择键
	GetDlgItem(IDC_PLAIN_EDIT)->EnableWindow(FALSE);
	GetDlgItem(IDC_RADIO11)->ShowWindow(FALSE);
	GetDlgItem(IDC_RADIO12)->ShowWindow(FALSE);
	GetDlgItem(IDC_RADIO13)->ShowWindow(FALSE);
	GetDlgItem(IDC_RADIO14)->ShowWindow(FALSE);
	GetDlgItem(IDC_RADIO15)->ShowWindow(FALSE);
	GetDlgItem(IDC_RADIO21)->ShowWindow(FALSE);
	GetDlgItem(IDC_RADIO22)->ShowWindow(FALSE);
	GetDlgItem(IDC_RADIO23)->ShowWindow(FALSE);
	GetDlgItem(IDC_RADIO24)->ShowWindow(FALSE);
	GetDlgItem(IDC_RADIO31)->EnableWindow(FALSE);
	GetDlgItem(IDC_RADIO32)->EnableWindow(FALSE);
	GetDlgItem(IDC_STATIC9)->ShowWindow(FALSE);
	GetDlgItem(IDC_STATIC10)->ShowWindow(FALSE);
	UpdateData(TRUE);
	CString CPlainText;
	if (m_enchoose==1)//选择加密文件
		CPlainText = cfileplain;//明文为文件内容
	else
		GetDlgItem(IDC_PLAIN_EDIT)->GetWindowText(CPlainText);//获取明文输入框信息
	//CString转换为string类型
	CStringA t(CPlainText.GetBuffer(0));
	CPlainText.ReleaseBuffer();
	string m_t = t.GetBuffer(0);//m_t为明文
	t.ReleaseBuffer();
	string c_t;



	//根据选择的加密模式和填充方式加密
	if (m_encryptway == 0 && m_fillway == 0)
		c_t = Encrypt_All_Data_ECB_ZERO(m_t);
	else if (m_encryptway == 0 && m_fillway == 1)
		c_t = Encrypt_All_Data_ECB_PKCS7(m_t);
	else if (m_encryptway == 0 && m_fillway == 2)
		c_t = Encrypt_All_Data_ECB_ANSIX923(m_t);
	else if (m_encryptway == 0 && m_fillway == 3)
		c_t = Encrypt_All_Data_ECB_ISO10126(m_t);
	else if (m_encryptway == 1 && m_fillway == 0)
		c_t = Encrypt_All_Data_CBC_ZERO(m_t);
	else if (m_encryptway == 1 && m_fillway == 1)
		c_t = Encrypt_All_Data_CBC_PKCS7(m_t);
	else if (m_encryptway == 1 && m_fillway == 2)
		c_t = Encrypt_All_Data_CBC_ANSIX923(m_t);
	else if (m_encryptway == 1 && m_fillway == 3)
		c_t = Encrypt_All_Data_CBC_ISO10126(m_t);
	else if (m_encryptway == 2 && m_fillway == 0)
		c_t = Encrypt_All_Data_CFB_ZERO(m_t);
	else if (m_encryptway == 2 && m_fillway == 1)
		c_t = Encrypt_All_Data_CFB_PKCS7(m_t);
	else if (m_encryptway == 2 && m_fillway == 2)
		c_t = Encrypt_All_Data_CFB_ANSIX923(m_t);
	else if (m_encryptway == 2 && m_fillway == 3)
		c_t = Encrypt_All_Data_CFB_ISO10126(m_t);
	else if (m_encryptway == 3 && m_fillway == 0)
		c_t = Encrypt_All_Data_OFB_ZERO(m_t);
	else if (m_encryptway == 3 && m_fillway == 1)
		c_t = Encrypt_All_Data_OFB_PKCS7(m_t);
	else if (m_encryptway == 3 && m_fillway == 2)
		c_t = Encrypt_All_Data_OFB_ANSIX923(m_t);
	else if (m_encryptway == 3 && m_fillway == 3)
		c_t = Encrypt_All_Data_OFB_ISO10126(m_t);
	else if (m_encryptway == 4 && m_fillway == 0)
		c_t = Encrypt_All_Data_CTR_ZERO(m_t);
	else if (m_encryptway == 4 && m_fillway == 1)
		c_t = Encrypt_All_Data_CTR_PKCS7(m_t);
	else if (m_encryptway == 4 && m_fillway == 2)
		c_t = Encrypt_All_Data_CTR_ANSIX923(m_t);
	else if (m_encryptway == 4 && m_fillway == 3)
		c_t = Encrypt_All_Data_CTR_ISO10126(m_t);
	


	string temp0 = "";//比特流（16进制）
	for (int i = 0; i < c_t.length(); i++)
	{
		int t0 = (int)c_t[i];//记录字符串的某一位字符
		if (t0 < 0) t0 = 256 + t0;//负数处理
		char c0, c1;
		c0 = (t0 % 16 < 10) ? (t0 % 16 + '0') : (t0 % 16 - 10 + 'A');//16进制的低位
		c1 = (t0 / 16 < 10) ? (t0 / 16 + '0') : (t0 / 16 - 10 + 'A');//16进制的高位
		//字符串追加字符
		temp0.append("0x");
		temp0.append(1, c1);
		temp0.append(1, c0);
		temp0.append(" ");
		if ((i + 1) % 7 == 0) temp0.append("\r\n");//换行
	}
	
	CString CConText;
	CConText = CA2T(c_t.c_str());
	if (m_enchoose == 1)//文件加密
	{
		//将加密结果保存到文件中
		// 设置过滤器   
		TCHAR szFilter[] = _T("文本文件(*.txt)|*.txt|所有文件(*.*)|*.*||");
		// 构造保存文件对话框   
		CFileDialog fileDlg(FALSE, _T("txt"), NULL, OFN_HIDEREADONLY | OFN_OVERWRITEPROMPT, szFilter, this);
		fileDlg.m_ofn.lpstrTitle = L"保存密文文件";
		CString strFilePath;
		if (IDOK == fileDlg.DoModal())
		{
			// 如果点击了文件对话框上的“保存”按钮，则将选择的文件路径显示到编辑框里   
			strFilePath = fileDlg.GetPathName();
			// 判断文件是否存在,如果存在则去掉只读属性
			if (PathFileExists(strFilePath) && !PathIsDirectory(strFilePath))
			{
				DWORD dwAttrs = GetFileAttributes(strFilePath);
				if (dwAttrs != INVALID_FILE_ATTRIBUTES
					&& (dwAttrs & FILE_ATTRIBUTE_READONLY))
				{
					dwAttrs &= ~FILE_ATTRIBUTE_READONLY;
					SetFileAttributes(strFilePath, dwAttrs);
				}
			}
			// 打开文件
			CStdioFile file;
			BOOL ret = file.Open(strFilePath,CFile::modeCreate  | CFile::modeWrite | CFile::shareDenyWrite);
			if (!ret)
			{
				::AfxMessageBox(L"打开文件失败");
				return;
			}
			file.SeekToEnd();
			file.WriteString(CConText);//写入文件
			file.Close();//关闭文件
			strFilePath = L"<文件>" + strFilePath;//显示文件路径
			GetDlgItem(IDC_CONFIDENTIAL_EDIT)->SetWindowText(strFilePath);
		}
		
	}
	else//字符串加密
	{
		GetDlgItem(IDC_CONFIDENTIAL_EDIT)->SetWindowText(CConText);//显示加密后的字符串
		CString t1;
		t1 = CA2T(temp0.c_str());
		GetDlgItem(IDC_BITSTRING_EDIT)->SetWindowText(t1);//显示加密后的比特串
	}
	GetDlgItem(IDENCRYPT)->ShowWindow(FALSE);//禁用加密按钮
	GetDlgItem(IDDECRYPT)->ShowWindow(TRUE);//启用解密按钮
	GetDlgItem(IDC_ORIGINAL_EDIT)->SetWindowText(L"");//清空解密框内容

}


void CEncrypt_Decrypt_MFCDlg::OnBnClickedDecrypt()//按下解密按钮
{
	// TODO: Add your control notification handler code here
	GetDlgItem(IDENCRYPT)->ShowWindow(TRUE);//启用加密按钮
	GetDlgItem(IDDECRYPT)->ShowWindow(FALSE);//禁用解密按钮
	GetDlgItem(IDC_CONFIDENTIAL_EDIT)->ShowWindow(FALSE);//隐藏密文框
	GetDlgItem(IDC_BITSTRING_EDIT)->ShowWindow(FALSE);//隐藏比特流框
	GetDlgItem(IDC_ORIGINAL_EDIT)->ShowWindow(TRUE);//显示原文框
	GetDlgItem(IDC_STATIC6)->ShowWindow(FALSE);
	GetDlgItem(IDC_STATIC7)->ShowWindow(FALSE);
	GetDlgItem(IDC_STATIC8)->ShowWindow(TRUE);
	UpdateData(TRUE);
	CString CConText;
	if (m_enchoose == 1)//解密文件
	{
		CString strFilePath;
		GetDlgItem(IDC_CONFIDENTIAL_EDIT)->GetWindowText(strFilePath);//获取文件路径
		strFilePath = strFilePath.Mid(4);//去除密文框前面的<文件>标识符
		CStdioFile file;
		CString strText;
		//打开文件
		if (!file.Open(strFilePath, CFile::modeRead))
		{
			::AfxMessageBox(_T("文件打开失败。"));
			return;
		}
		//读文件
		strText = _T("");
		CConText = L"";
		while (file.ReadString(strText))
			CConText = CConText + strText;//追加到密文中
		//关闭文件
		file.Close();
	}
	else
		GetDlgItem(IDC_PLAIN_EDIT)->GetWindowText(CConText);//获取密文框信息
	//CString转换成string类型
	CStringA t(CConText.GetBuffer(0));
	CConText.ReleaseBuffer();
	string c_t = t.GetBuffer(0);//c_t为密文
	t.ReleaseBuffer();
	string m_t;



	//根据选择的加密方式和填充模式解密
	if (m_encryptway == 0 && m_fillway == 0)
		m_t = Decrypt_All_Data_ECB_ZERO(c_t);
	else if (m_encryptway == 0 && m_fillway == 1)
		m_t = Decrypt_All_Data_ECB_PKCS7(c_t);
	else if (m_encryptway == 0 && m_fillway == 2)
		m_t = Decrypt_All_Data_ECB_ANSIX923(c_t);
	else if (m_encryptway == 0 && m_fillway == 3)
		m_t = Decrypt_All_Data_ECB_ISO10126(c_t);
	else if (m_encryptway == 1 && m_fillway == 0)
		m_t = Decrypt_All_Data_CBC_ZERO(c_t);
	else if (m_encryptway == 1 && m_fillway == 1)
		m_t = Decrypt_All_Data_CBC_PKCS7(c_t);
	else if (m_encryptway == 1 && m_fillway == 2)
		m_t = Decrypt_All_Data_CBC_ANSIX923(c_t);
	else if (m_encryptway == 1 && m_fillway == 3)
		m_t = Decrypt_All_Data_CBC_ISO10126(c_t);
	else if (m_encryptway == 2 && m_fillway == 0)
		m_t = Decrypt_All_Data_CFB_ZERO(c_t);
	else if (m_encryptway == 2 && m_fillway == 1)
		m_t = Decrypt_All_Data_CFB_PKCS7(c_t);
	else if (m_encryptway == 2 && m_fillway == 2)
		m_t = Decrypt_All_Data_CFB_ANSIX923(c_t);
	else if (m_encryptway == 2 && m_fillway == 3)
		m_t = Decrypt_All_Data_CFB_ISO10126(c_t);
	else if (m_encryptway == 3 && m_fillway == 0)
		m_t = Decrypt_All_Data_OFB_ZERO(c_t);
	else if (m_encryptway == 3 && m_fillway == 1)
		m_t = Decrypt_All_Data_OFB_PKCS7(c_t);
	else if (m_encryptway == 3 && m_fillway == 2)
		m_t = Decrypt_All_Data_OFB_ANSIX923(c_t);
	else if (m_encryptway == 3 && m_fillway == 3)
		m_t = Decrypt_All_Data_OFB_ISO10126(c_t);
	else if (m_encryptway == 4 && m_fillway == 0)
		m_t = Decrypt_All_Data_CTR_ZERO(c_t);
	else if (m_encryptway == 4 && m_fillway == 1)
		m_t = Decrypt_All_Data_CTR_PKCS7(c_t);
	else if (m_encryptway == 4 && m_fillway == 2)
		m_t = Decrypt_All_Data_CTR_ANSIX923(c_t);
	else if (m_encryptway == 4 && m_fillway == 3)
		m_t = Decrypt_All_Data_CTR_ISO10126(c_t);
	/*if (m_enchoose == 1)//解密文件
	{
		CString strFilePath;
		GetDlgItem(IDC_PLAIN_EDIT)->GetWindowText(strFilePath);//获取文件路径
		strFilePath = strFilePath.Mid(4);//去除密文框前面的<文件>标识符
		CStdioFile file;
		CString strText;
		//打开文件
		if (!file.Open(strFilePath, CFile::modeRead))
		{
			::AfxMessageBox(_T("文件打开失败。"));
			return;
		}
		//读文件
		strText = _T("");
		CString cplain;
		cplain = L"";
		while (file.ReadString(strText))
			cplain = cplain + strText;//追加到密文中
		//关闭文件
		file.Close();
		CStringA t1(cplain.GetBuffer(0));
		cplain.ReleaseBuffer();
		m_t = t1.GetBuffer(0);
		t1.ReleaseBuffer();
	}
	else
	{
		CString cplain;
		GetDlgItem(IDC_PLAIN_EDIT)->GetWindowText(cplain);
		CStringA t1(cplain.GetBuffer(0));
		cplain.ReleaseBuffer();
		m_t = t1.GetBuffer(0);
		t1.ReleaseBuffer();
	}*/
	//string转换为cstring类型
	CString COriText;
	COriText = CA2T(m_t.c_str());
	if (m_enchoose == 1)//解密文件
	{
		// 设置过滤器   
		TCHAR szFilter[] = _T("文本文件(*.txt)|*.txt|所有文件(*.*)|*.*||");
		// 构造保存文件对话框   
		CFileDialog fileDlg(FALSE, _T("txt"), NULL, OFN_HIDEREADONLY | OFN_OVERWRITEPROMPT, szFilter, this);
		fileDlg.m_ofn.lpstrTitle = L"保存原文文件";
		CString strFilePath;
		if (IDOK == fileDlg.DoModal())
		{
			// 如果点击了文件对话框上的“保存”按钮，则将选择的文件路径显示到编辑框里   
			strFilePath = fileDlg.GetPathName();
			// 判断文件是否存在,如果存在则去掉只读属性
			if (PathFileExists(strFilePath) && !PathIsDirectory(strFilePath))
			{
				DWORD dwAttrs = GetFileAttributes(strFilePath);
				if (dwAttrs != INVALID_FILE_ATTRIBUTES
					&& (dwAttrs & FILE_ATTRIBUTE_READONLY))
				{
					dwAttrs &= ~FILE_ATTRIBUTE_READONLY;
					SetFileAttributes(strFilePath, dwAttrs);
				}
			}
			// 打开文件
			CStdioFile file;
			BOOL ret = file.Open(strFilePath, CFile::modeCreate | CFile::modeWrite | CFile::shareDenyWrite);
			if (!ret)
			{
				::AfxMessageBox(L"打开文件失败");
				return;
			}
			file.SeekToEnd();
			file.WriteString(COriText);//写入文件
			file.Close();
			strFilePath = L"<文件>" + strFilePath;//记录文件路径
			GetDlgItem(IDC_ORIGINAL_EDIT)->SetWindowText(strFilePath);//显示文件路径
		}

	}
	else//解密字符串
		GetDlgItem(IDC_ORIGINAL_EDIT)->SetWindowText(COriText);//显示解密后的原文




	GetDlgItem(IDSETKEY)->EnableWindow(TRUE);//启用设置密钥按钮
	GetDlgItem(IDC_COMBO_KEYBYTES)->EnableWindow(TRUE);//启用设置密钥位数框
	GetDlgItem(IDC_EDIT_KEY)->EnableWindow(TRUE);//启用密钥输入框




	//启用加密模式和填充模式选择键
	GetDlgItem(IDC_RADIO11)->ShowWindow(TRUE);
	GetDlgItem(IDC_RADIO12)->ShowWindow(TRUE);
	GetDlgItem(IDC_RADIO13)->ShowWindow(TRUE);
	GetDlgItem(IDC_RADIO14)->ShowWindow(TRUE);
	GetDlgItem(IDC_RADIO15)->ShowWindow(TRUE);
	GetDlgItem(IDC_RADIO21)->ShowWindow(TRUE);
	GetDlgItem(IDC_RADIO22)->ShowWindow(TRUE);
	GetDlgItem(IDC_RADIO23)->ShowWindow(TRUE);
	GetDlgItem(IDC_RADIO24)->ShowWindow(TRUE);
	GetDlgItem(IDC_RADIO31)->EnableWindow(TRUE);
	GetDlgItem(IDC_RADIO32)->EnableWindow(TRUE);
	GetDlgItem(IDC_STATIC9)->ShowWindow(TRUE);
	GetDlgItem(IDC_STATIC10)->ShowWindow(TRUE);
	GetDlgItem(IDC_PLAIN_EDIT)->EnableWindow(TRUE);//启用明文输入框
	GetDlgItem(IDC_CONFIDENTIAL_EDIT)->SetWindowText(L"");//清空密文框
	GetDlgItem(IDC_BITSTRING_EDIT)->SetWindowText(L"");//清空比特流框
	GetDlgItem(IDSETKEY)->ShowWindow(TRUE);//启用设置密钥按钮
}





void CEncrypt_Decrypt_MFCDlg::OnBnClickedRadio11()//ECB模式
{
	// TODO: Add your control notification handler code here
	m_encryptway = 0;


}


void CEncrypt_Decrypt_MFCDlg::OnBnClickedRadio12()//CBC模式
{
	// TODO: Add your control notification handler code here
	m_encryptway = 1;


}


void CEncrypt_Decrypt_MFCDlg::OnBnClickedRadio13()//CFB模式
{
	// TODO: Add your control notification handler code here
	m_encryptway = 2;


}


void CEncrypt_Decrypt_MFCDlg::OnBnClickedRadio14()//OFB模式
{
	// TODO: Add your control notification handler code here
	m_encryptway = 3;
}


void CEncrypt_Decrypt_MFCDlg::OnBnClickedRadio15()//CTR模式
{
	// TODO: Add your control notification handler code here
	m_encryptway = 4;


}


void CEncrypt_Decrypt_MFCDlg::OnBnClickedRadio21()//ZERO模式
{
	// TODO: Add your control notification handler code here
	m_fillway = 0;
}


void CEncrypt_Decrypt_MFCDlg::OnBnClickedRadio22()//PKCS7模式
{
	// TODO: Add your control notification handler code here
	m_fillway = 1;


}


void CEncrypt_Decrypt_MFCDlg::OnBnClickedRadio23()//ANSI X.923模式
{
	// TODO: Add your control notification handler code here
	m_fillway = 2;


}


void CEncrypt_Decrypt_MFCDlg::OnBnClickedRadio24()//ISO 10126模式
{
	// TODO: Add your control notification handler code here
	m_fillway = 3;
}


void CEncrypt_Decrypt_MFCDlg::OnBnClickedRadio31()//选择加密字符串方式
{
	// TODO: Add your control notification handler code here
	m_enchoose = 0;


	GetDlgItem(IDC_PLAIN_EDIT)->ShowWindow(TRUE);//显示明文输入框
	GetDlgItem(IDC_STATIC1)->ShowWindow(TRUE);
	GetDlgItem(IDCHOOSEFILE)->ShowWindow(FALSE);//禁用选择文件按钮

	GetDlgItem(IDC_PLAIN_EDIT)->EnableWindow(TRUE);//启用明文输入框
	GetDlgItem(IDC_PLAIN_EDIT)->SetWindowText(L"");//清空明文输入框
	GetDlgItem(IDC_CONFIDENTIAL_EDIT)->SetWindowText(L"");//清空密文框
	GetDlgItem(IDC_ORIGINAL_EDIT)->SetWindowText(L"");//清空原文框
}


void CEncrypt_Decrypt_MFCDlg::OnBnClickedRadio32()//选择加密文件方式
{
	// TODO: Add your control notification handler code here
	m_enchoose = 1;

	GetDlgItem(IDC_STATIC7)->ShowWindow(FALSE);
	GetDlgItem(IDC_STATIC1)->ShowWindow(TRUE);
	GetDlgItem(IDC_PLAIN_EDIT)->ShowWindow(FALSE);//隐藏明文输入框
	GetDlgItem(IDCHOOSEFILE)->ShowWindow(TRUE);//启用选择文件按钮
	GetDlgItem(IDC_PLAIN_EDIT)->EnableWindow(FALSE);//禁用明文输入框

	GetDlgItem(IDC_PLAIN_EDIT)->SetWindowText(L"");//清空明文框
	GetDlgItem(IDC_CONFIDENTIAL_EDIT)->SetWindowText(L"");//清空密文框
	GetDlgItem(IDC_ORIGINAL_EDIT)->SetWindowText(L"");//清空原文框
}


void CEncrypt_Decrypt_MFCDlg::OnBnClickedChoosefile()
{
	// TODO: Add your control notification handler code here
	// 设置过滤器   
	TCHAR szFilter[] = _T("文本文件(*.txt)|*.txt|所有文件(*.*)|*.*||");
	// 构造打开文件对话框   
	CFileDialog fileDlg(TRUE, _T("txt"), NULL, 0, szFilter, this);
	fileDlg.m_ofn.lpstrTitle = L"打开明文文件";
	CString strFilePath;


	if (IDOK == fileDlg.DoModal())
	{
		// 如果点击了文件对话框上的“打开”按钮，则将选择的文件路径显示到编辑框里   
		strFilePath = fileDlg.GetPathName();
		CStdioFile file;
		CString strText;
		//打开文件
		if (!file.Open(strFilePath, CFile::modeRead))
		{
			::AfxMessageBox(_T("文件打开失败。"));
			return;
		}
		//读文件
		strText = _T("");
		cfileplain = L"";
		while (file.ReadString(strText))
			cfileplain = cfileplain + strText;


		//关闭文件
		file.Close();
		GetDlgItem(IDC_PLAIN_EDIT)->ShowWindow(TRUE);//显示明文输入框
		strFilePath = L"<文件>" + strFilePath;
		GetDlgItem(IDC_PLAIN_EDIT)->SetWindowText(strFilePath);
	}
}


void CEncrypt_Decrypt_MFCDlg::OnCbnSelchangeComboKeybytes()
{
	// TODO: Add your control notification handler code here
	GetDlgItem(IDC_EDIT_KEY)->ShowWindow(TRUE);//显示密钥输入框
	GetDlgItem(IDSETKEY)->ShowWindow(TRUE);//显示设置密钥按钮
	GetDlgItem(IDC_EDIT_KEY)->SetWindowText(L"");//密钥输入框清空


}


LRESULT CEncrypt_Decrypt_MFCDlg::OnNcHitTest(CPoint point)
{
	// TODO: Add your message handler code here and/or call default


	int ret = CDialog::OnNcHitTest(point);
	
	//禁止改变窗口大小
	if (HTTOP == ret || HTBOTTOM == ret || HTLEFT == ret || HTRIGHT == ret || HTBOTTOMLEFT == ret || HTBOTTOMRIGHT == ret || HTTOPLEFT == ret || HTTOPRIGHT == ret || HTCAPTION == ret)
		return HTCLIENT;



	return ret;

}




HBRUSH CEncrypt_Decrypt_MFCDlg::OnCtlColor(CDC* pDC, CWnd* pWnd, UINT nCtlColor)
{
	
	HBRUSH hbr = CDialog::OnCtlColor(pDC, pWnd, nCtlColor);
	// TODO:  Change any attributes of the DC here
	if (nCtlColor == CTLCOLOR_BTN)          //更改按钮颜色
	{
		
		pDC->SetTextColor(RGB(254, 1, 1));
		pDC->SetBkColor(RGB(117, 31, 111));
		HBRUSH b = CreateSolidBrush(RGB(186, 226, 234));
		return b;
	}

	else if (nCtlColor == CTLCOLOR_EDIT)   //更改编辑框
	{
		
		pDC->SetTextColor(RGB(24, 6, 102));
		pDC->SetBkColor(RGB(254, 198, 103));
		HBRUSH b = CreateSolidBrush(RGB(186, 226, 234));
		return b;


	}
	 if (nCtlColor == CTLCOLOR_STATIC)  //更改静态文本
	{


		pDC->SetTextColor(RGB(254, 1, 1));
		pDC->SetBkColor(RGB(186, 226, 234));
		HBRUSH b = CreateSolidBrush(RGB(186, 226, 234));
		return b;


	}




	// TODO:  Return a different brush if the default is not desired
	return hbr;
}




void CEncrypt_Decrypt_MFCDlg::OnTimer(UINT_PTR nIDEvent)
{
	
	CString strTime;
	CTime tm;
	tm = CTime::GetCurrentTime();
	strTime = tm.Format("%Y-%m-%d %H:%M:%S");
	SetDlgItemText(IDC_ShowTime, strTime);        //显示系统时间



	// TODO: Add your message handler code here and/or call default
	CDialogEx::OnTimer(nIDEvent);
}




