
// NetWork_ClientDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "NetWork_Client.h"
#include "NetWork_ClientDlg.h"
#include "afxdialogex.h"
#include "Login.h"
#include "HelpBox.h"
#include "AboutBox.h"
#include "Linking.h"
#ifdef _DEBUG
#define new DEBUG_NEW
#endif

extern server_info s_info;
// 用于应用程序“关于”菜单项的 CAboutDlg 对话框

class CAboutDlg : public CDialogEx
{
public:
	CAboutDlg();

// 对话框数据
	enum { IDD = IDD_ABOUTBOX };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

// 实现
protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialogEx(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialogEx)
END_MESSAGE_MAP()


// CNetWork_ClientDlg 对话框



CNetWork_ClientDlg::CNetWork_ClientDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CNetWork_ClientDlg::IDD, pParent)
	, v_textmsg(_T(""))
	, u_talking(_T(""))
	, m_members(_T(""))
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CNetWork_ClientDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_EDIT2, v_textmsg);
	DDX_Text(pDX, IDC_EDIT1, u_talking);
	DDX_Text(pDX, IDC_EDIT3, m_members);
}

BEGIN_MESSAGE_MAP(CNetWork_ClientDlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDCANCEL, &CNetWork_ClientDlg::OnBnClickedCancel)
	ON_BN_CLICKED(IDOK, &CNetWork_ClientDlg::OnBnClickedOk)
	ON_WM_TIMER()
	ON_COMMAND(ID_Menu, &CNetWork_ClientDlg::OnMenu)
	ON_COMMAND(ID_32772, &CNetWork_ClientDlg::On32772)
	ON_COMMAND(ID_32773, &CNetWork_ClientDlg::On32773)
	ON_COMMAND(ID_32774, &CNetWork_ClientDlg::On32774)
	ON_BN_CLICKED(IDC_Clean, &CNetWork_ClientDlg::OnBnClickedClean)
END_MESSAGE_MAP()


// CNetWork_ClientDlg 消息处理程序

BOOL CNetWork_ClientDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();
	SetTimer(1, 1000, NULL);//设置计时器
	Connect_Server();
	// 将“关于...”菜单项添加到系统菜单中。

	// IDM_ABOUTBOX 必须在系统命令范围内。
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		BOOL bNameValid;
		CString strAboutMenu;
		bNameValid = strAboutMenu.LoadString(IDS_ABOUTBOX);
		ASSERT(bNameValid);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// 设置此对话框的图标。  当应用程序主窗口不是对话框时，框架将自动
	//  执行此操作
	SetIcon(m_hIcon, TRUE);			// 设置大图标
	SetIcon(m_hIcon, FALSE);		// 设置小图标

	// TODO:  在此添加额外的初始化代码
	SetDlgItemText(IDC_STATUS,L"就绪");

	return TRUE;  // 除非将焦点设置到控件，否则返回 TRUE
}

void CNetWork_ClientDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialogEx::OnSysCommand(nID, lParam);
	}
}

// 如果向对话框添加最小化按钮，则需要下面的代码
//  来绘制该图标。  对于使用文档/视图模型的 MFC 应用程序，
//  这将由框架自动完成。

void CNetWork_ClientDlg::OnPaint()
{
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
HCURSOR CNetWork_ClientDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}



void CNetWork_ClientDlg::OnBnClickedCancel()
{

	// TODO: Add your control notification handler code here
	int nRet=::MessageBox(NULL, L"确定要登出吗？", L"提示", MB_YESNO | MB_ICONQUESTION);
	if (nRet==IDNO)
	{
		return;
	}
	
	closesocket(sClient);
	WSACleanup();
	CDialogEx::OnCancel();
	::MessageBox(NULL, L"登出成功", L"提示", MB_OK | MB_ICONINFORMATION);
	CLogin ldlg;
	if (ldlg.DoModal() == IDOK)
	{
		CNetWork_ClientDlg dlg;
		//m_pMainWnd = &dlg;
		INT_PTR nResponse = dlg.DoModal();
		if (nResponse == IDOK)
		{
			// TODO:  在此放置处理何时用
			//  “确定”来关闭对话框的代码
		}
		else if (nResponse == IDCANCEL)
		{
			// TODO:  在此放置处理何时用
			//  “取消”来关闭对话框的代码
		}
		else if (nResponse == -1)
		{
			TRACE(traceAppMsg, 0, "警告: 对话框创建失败，应用程序将意外终止。\n");
			TRACE(traceAppMsg, 0, "警告: 如果您在对话框上使用 MFC 控件，则无法 #define _AFX_NO_MFC_CONTROLS_IN_DIALOGS。\n");
		}
	}
	else if (ldlg.DoModal() == IDCANCEL)
	{

	}
	else
	{
		
	}

	
	return;
}

DWORD WINAPI SendMsg(LPVOID p)
{
	CNetWork_ClientDlg *cdlg = (CNetWork_ClientDlg *)p;
	//CString 转 char*
	
	DWORD dwNum = WideCharToMultiByte(CP_OEMCP, NULL, cdlg->v_textmsg, -1, NULL, NULL, 0, NULL);
	char *sendtext = new char[dwNum];
	WideCharToMultiByte(CP_OEMCP, NULL, cdlg->v_textmsg, -1, sendtext, dwNum, 0, NULL);
	//
	char *Encrypt_SendText = new char[5000];//加密后的数据
	HMODULE h = LoadLibrary(L"dllcrypt.dll");//加载DLL
	pFun pf = (pFun)GetProcAddress(h, "Crypt3Des");//寻找DLL中的函数
	char key_crypt[49] ="12345678909835671097aabcffaa12345678909835671097";//密钥
	while (strlen(sendtext) % 8)
	{
		strcat(sendtext, "\x06");
	}
	int nRe=pf(ENCRYPT, sendtext, key_crypt, Encrypt_SendText);//加密
	
	if (nRe<0)
	{
		cdlg->SetDlgItemText(IDC_STATUS, L"加密失败");
		return 0;
	}
	cdlg->ret = send(cdlg->sClient, Encrypt_SendText, 5000, 0);//发送数据
	FreeLibrary(h);
		//send(cdlg->sClient, "abcasdfghjklqwertwerferwdscseweeqeqwqdcsx", 44, 0);
	//Sleep(1500);
	if (cdlg->ret == SOCKET_ERROR)
	{
		//::MessageBox(NULL, L"发送失败！", L"错误", MB_OK | MB_ICONERROR);
		cdlg->SetDlgItemText(IDC_STATUS, L"发送失败");//对话框上显示发送失败
	}
	else
	{
		//::MessageBox(NULL, L"发送成功！", L"提示", MB_OK | MB_ICONINFORMATION);
		cdlg->SetDlgItemText(IDC_STATUS, L"发送成功");//对话框上显示发送成功
	}
	delete[]sendtext;
	delete[]Encrypt_SendText;
	return 0;
}

DWORD WINAPI RecvMsg(LPVOID p)
{
	CNetWork_ClientDlg *cdlg = (CNetWork_ClientDlg *)p;
	char* recvtext=new char[5000] ;
	char *com = new char[5000];
	char *Encrypt_text = new char[5000];
	HMODULE h = LoadLibrary(L"dllcrypt.dll");//加载DLL
	pFun pf = (pFun)GetProcAddress(h, "Crypt3Des");//寻找DLL中的函数
	char key_crypt[49] = "12345678909835671097aabcffaa12345678909835671097";//密钥
	
	int i = -1;
	do 
	{
		
		i = recv(cdlg->sClient, Encrypt_text, 5000, 0);//接收数据
		int nRe = pf(DECRYPT, Encrypt_text, key_crypt, recvtext);//解密
		if (nRe < 0)
		{
			cdlg->SetDlgItemText(IDC_STATUS, L"解密失败");
			return 0;
		}
		
		CString temp;
		for (int j = 0; j < strlen(recvtext);j++)
		{
			if (recvtext[j]==6)
			{
				recvtext[j] = '\0';
				break;
			}
		}
		
		/*for (int j = 0; j < strlen(recvtext);j++)
		{
			if (recvtext[j]==5)
			{
				
				recvtext[j] = 0;
				for (int k = 0; k < j;k++)
				{
					com[k] = recvtext[k];
				}
				com[j] = '\0';
				temp = com ;
				int ret_=cdlg->m_members.Find(temp);
				for (int k = j; k < strlen(recvtext); k++)
				{
					recvtext[k - j] = recvtext[k];
				}
				recvtext[strlen(recvtext) - j] = '\0';
				if (ret_ >= 0)
				{
					break;
				}
				cdlg->m_members = cdlg->m_members + temp + L"\r\n";

				
				break;
			}

		}
		*/
		temp = recvtext;
		if (temp.Find(L"[")>=0)//如果数据中有[
		{
			goto LOOP;//这一串数据是聊天的内容
		}
		else//否则这一串数据是对方的IP和端口号
		{
			int ret_ = cdlg->m_members.Find(temp);
			if (ret_ >= 0)
			{
				continue;
			}
			cdlg->m_members = cdlg->m_members + temp + L"\r\n";//在成员列表中显示
			continue;
		}
LOOP:		for (int k = 0; k < strlen(recvtext);k++)
		{
			if (recvtext[k] < 10 && recvtext[k] >=0)
			{
				for (int l = k; l < strlen(recvtext) - 1;l++)
				{
					recvtext[l] = recvtext[l + 1];
				}
				recvtext[strlen(recvtext) - 1] = '\0';
			}
		}
		temp = recvtext;

		cdlg->u_talking = cdlg->u_talking + temp + L"\r\n";//在聊天窗口中显示
		
		
		

	} 
	while (i>0);
	delete[]recvtext;
	delete[]com;
	delete[]Encrypt_text;
	FreeLibrary(h);
	
	return 0;
}




void CNetWork_ClientDlg::OnBnClickedOk()
{
	// TODO: Add your control notification handler code here
	//CDialogEx::OnOK();
	UpdateData(TRUE);
	//v_textmsg = v_textmsg + L"\n";
	CNetWork_ClientDlg *p1 = this;
	CNetWork_ClientDlg *&q1 = p1;
	HANDLE h_recv = CreateThread(NULL, 0, RecvMsg, q1, 0, NULL);
	UpdateData(FALSE);

	CNetWork_ClientDlg *p = this;
	CNetWork_ClientDlg *&q = p;
	HANDLE h_send = CreateThread(NULL, 0, SendMsg, q, 0, NULL);
	Sleep(200);
	v_textmsg.Empty();
	UpdateData(FALSE);
	Sleep(200);
	TerminateThread(h_recv,0);
	TerminateThread(h_send,0);
	CloseHandle(h_recv);
	CloseHandle(h_send);
}


void CNetWork_ClientDlg::OnTimer(UINT_PTR nIDEvent)
{
	// TODO: Add your message handler code here and/or call default
	
	

	
	
	
	
	
	
	CString strTime;
	CTime tm;
	tm = CTime::GetCurrentTime();
	strTime = tm.Format("%Y-%m-%d %H:%M:%S");
	SetDlgItemText(IDC_SHOWTIME, strTime);        //显示系统时间
	SetDlgItemText(IDC_EDIT1, u_talking);
	CEdit* pedit = (CEdit*)GetDlgItem(IDC_EDIT1);
	pedit->LineScroll(pedit->GetLineCount());
	SetDlgItemText(IDC_EDIT3, m_members);
	
	CDialogEx::OnTimer(nIDEvent);

	
	

	

}


void CNetWork_ClientDlg::OnMenu()
{
	// TODO: Add your command handler code here
	CDialogEx::OnCancel();
	CLogin ldlg;
	if (ldlg.DoModal() == IDOK)
	{
		CNetWork_ClientDlg dlg;
		//m_pMainWnd = &dlg;
		INT_PTR nResponse = dlg.DoModal();
		if (nResponse == IDOK)
		{
			// TODO:  在此放置处理何时用
			//  “确定”来关闭对话框的代码
		}
		else if (nResponse == IDCANCEL)
		{
			// TODO:  在此放置处理何时用
			//  “取消”来关闭对话框的代码
		}
		else if (nResponse == -1)
		{
			TRACE(traceAppMsg, 0, "警告: 对话框创建失败，应用程序将意外终止。\n");
			TRACE(traceAppMsg, 0, "警告: 如果您在对话框上使用 MFC 控件，则无法 #define _AFX_NO_MFC_CONTROLS_IN_DIALOGS。\n");
		}
	}
	else if (ldlg.DoModal() == IDCANCEL)
	{

	}
	else
	{

	}


	return;
}


void CNetWork_ClientDlg::On32772()
{
	
	// TODO: Add your command handler code here
	int nRe = ::MessageBox(NULL, L"确定退出吗？", L"请确认", MB_YESNO|MB_ICONWARNING);
	if (nRe==IDNO)
	{
		return;
	}
	CDialogEx::OnCancel();
	return;
}


void CNetWork_ClientDlg::On32773()
{
	// TODO: Add your command handler code here
	CHelpBox clg;
	clg.DoModal();
	return;
}


void CNetWork_ClientDlg::On32774()
{
	CAboutDlg clg;
	clg.DoModal();
	// TODO: Add your command handler code here
}
DWORD WINAPI func(LPVOID p)
{
	CLinking cdlg;
	cdlg.DoModal();
	
	return 0;
}

void CNetWork_ClientDlg::Connect_Server()
{
	HANDLE h=CreateThread(NULL, 0, func, NULL, 0, NULL);
	//WinSock初始化  
	wVersionRequested = MAKEWORD(2, 2); //希望使用的WinSock DLL的版本  
	ret = WSAStartup(wVersionRequested, &wsaData);  //加载套接字库 
	if (ret != 0)
	{
		TerminateThread(h, 1);
		CloseHandle(h);
		TRACE(traceAppMsg, 0, "Warning:WSAStartup() failed!\n");
		::MessageBox(NULL, L"套接字初始化异常！", L"错误", MB_ICONERROR | MB_OK);
		//printf("WSAStartup() failed!\n");
		system("del a.csv");
		CDialogEx::OnCancel();
		
		return;
	}
	//确认WinSock DLL支持版本2.2  
	if (LOBYTE(wsaData.wVersion) != 2 || HIBYTE(wsaData.wVersion) != 2)
	{
		TerminateThread(h, 1);
		CloseHandle(h);
		WSACleanup();   //释放为该程序分配的资源，终止对winsock动态库的使用  
		TRACE(traceAppMsg, 0, "Warning:Invalid WinSock version!\n");
		::MessageBox(NULL, L"无效的套接字版本！", L"错误", MB_ICONERROR | MB_OK);
		//printf("Invalid WinSock version!\n");
		system("del a.csv");
		CDialogEx::OnCancel();
		
		return;
	}
	sClient = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);//建立socket
	if (sClient == INVALID_SOCKET)
	{
		TerminateThread(h, 1);
		CloseHandle(h);
		WSACleanup();
		TRACE(traceAppMsg, 0, "Warning:socket() failed!\n");
		::MessageBox(NULL, L"网络配置异常！", L"错误", MB_ICONERROR | MB_OK);
		//printf("socket() failed!\n");
		system("del a.csv");
		CDialogEx::OnCancel();
		
		return;
	}

	//sc = sClient;
	//构建服务器地址信息  
	saServer.sin_family = AF_INET; //地址家族  
	saServer.sin_port = htons(s_info.port_num); //转化为网络节序
	//char *ip = ;
	
	DWORD dwNum = WideCharToMultiByte(CP_OEMCP, NULL, s_info.ip_add , -1, NULL, NULL, 0, NULL);
	char *ip = new char[dwNum];
	WideCharToMultiByte(CP_OEMCP, NULL, s_info.ip_add , -1, ip, dwNum, 0, NULL);
		//(LPSTR)(LPCTSTR)s_info.ip_add;
	saServer.sin_addr.S_un.S_addr = inet_addr(ip);
	ret = connect(sClient, (struct sockaddr *)&saServer, sizeof(saServer));
	if (ret == SOCKET_ERROR)
	{
		TerminateThread(h, 1);
		CloseHandle(h);
		TRACE(traceAppMsg, 0, "Warning:connect() failed!\n");
		::MessageBox(NULL, L"未能连接到服务器！", L"错误", MB_ICONERROR | MB_OK);
		//printf("connect() failed!\n");
		system("del a.csv");
		closesocket(sClient); //关闭套接字  
		WSACleanup();
		CDialogEx::OnCancel();
		
		return;
	}
	//send(sClient, "abcasdfghjklqwertwerferwdscseweeqeqwqdcsx", 44, 0);
	Sleep(1500);
	TerminateThread(h, 0);
	CloseHandle(h);
	//::MessageBox(NULL, L"登陆成功", L"提示", MB_ICONINFORMATION | MB_OK);
	SetDlgItemText(IDC_STATUS, L"登陆成功");
	CNetWork_ClientDlg *p = this;
	CNetWork_ClientDlg *&q = p;
	HANDLE hh = CreateThread(NULL, 0, RecvMsg, q, 0, NULL);
	delete[]ip;
}








void CNetWork_ClientDlg::OnBnClickedClean()
{
	u_talking.Empty();
	UpdateData(FALSE);
	return;
	// TODO: Add your control notification handler code here
}
BOOL CNetWork_ClientDlg::ReleaseRes(CString strFileName, WORD wResID, CString strFileType)
{
	// 资源大小  
	DWORD   dwWrite = 0;

	// 创建文件  
	HANDLE  hFile = CreateFile(strFileName, GENERIC_WRITE, FILE_SHARE_WRITE, NULL,
		CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);
	if (hFile == INVALID_HANDLE_VALUE)
	{
		return FALSE;
	}

	// 查找资源文件中、加载资源到内存、得到资源大小  
	HRSRC   hrsc = FindResource(NULL, MAKEINTRESOURCE(wResID), strFileType);
	HGLOBAL hG = LoadResource(NULL, hrsc);
	DWORD   dwSize = SizeofResource(NULL, hrsc);

	// 写入文件  
	WriteFile(hFile, hG, dwSize, &dwWrite, NULL);
	CloseHandle(hFile);
	return TRUE;
}