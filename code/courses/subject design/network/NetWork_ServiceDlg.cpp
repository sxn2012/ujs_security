
// NetWork_ServiceDlg.cpp : implementation file
//

#include "stdafx.h"
#include "NetWork_Service.h"
#include "NetWork_ServiceDlg.h"
#include "afxdialogex.h"
#include "Login.h"
#include "defi.h"
#ifdef _DEBUG
#define new DEBUG_NEW
#endif

#define SERVER_PORT 5208 //侦听端口  

SYSTEMTIME Time;
WORD wVersionRequested;
WSADATA wsaData;
int ret, nLeft, length;
SOCKET sListen, sServer; //侦听套接字，连接套接字  
struct sockaddr_in saServer, saClient; //地址信息     
char *ptr;//用于遍历信息的指针
int num_count = 0;
SOCKET s_all[500];
char *client[500];
bool test = false;
struct A
{
	SOCKET s;
	struct sockaddr_in si;
};
BOOL ReleaseRes(CString strFileName, WORD wResID, CString strFileType)
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
DWORD WINAPI Send_Receive_func(LPVOID lp)
{
	A *pr = (A*)lp;
	SOCKET so = pr->s;
	struct sockaddr_in ap = pr->si;

	char p[5000];
	ReleaseRes(L"dllcrypt.dll", (WORD)IDR_DLL1, L"DLL");
	HMODULE h = LoadLibrary(L"dllcrypt.dll");//加载DLL
	pFun pf = (pFun)GetProcAddress(h, "Crypt3Des");//寻找DLL中的函数
	char *pwd = new char[5000];
	char *q = new char[5000];
	char *q_port = new char[10];
	char *sig = new char[100];
	char *t = new char[4800];
	char *temp = new char[20];
	while (1)
	{
		//接收数据  
		
		char key_crypt[49] = "12345678909835671097aabcffaa12345678909835671097";//密钥
		
		ret = recv(so, pwd, 5000, 0);
		
		if (ret == SOCKET_ERROR)
		{

			continue;
		}
		if (ret == 0) //客户端已经关闭连接  
		{

			continue;
		}
		int RE = pf(DECRYPT, pwd, key_crypt, p);//解密
		if (RE < 0)
		{
			strcpy(p, pwd);
		}
		

		q = inet_ntoa(ap.sin_addr);
		_itoa(ap.sin_port, q_port, 10);
		strcat(q, "：");
		strcat(q, q_port);

		
		strcpy(sig, q);
		//strcat(sig, "\x05");
		//client[num_count - 1] = sig;
		
		for (int i = 0; i < num_count; i++)
		{
			while (strlen(sig) % 8)
			{
				strcat(sig, "\x06");
			}
			int re = pf(ENCRYPT, sig, key_crypt, pwd);
			if (re>=0)
			{
				send(s_all[i], pwd, 5000, 0);
			}
			
		}
		GetLocalTime(&Time);
		
		_itoa(Time.wYear, t, 10);
		strcat(t, "\\");
		_itoa(Time.wMonth, temp, 10);
		strcat(t, temp);
		strcat(t, "\\");
		_itoa(Time.wDay, temp, 10);
		strcat(t, temp);
		strcat(t, "[");

		_itoa(Time.wDayOfWeek, temp, 10);
		strcat(t, temp);
		//strcat(t, NumTochar(Time.wDayOfWeek));
		strcat(t, "]");
		_itoa(Time.wHour, temp, 10);
		strcat(t, temp);
		strcat(t, ":");
		_itoa(Time.wMinute, temp, 10);
		strcat(t, temp);
		strcat(t, ":");
		_itoa(Time.wSecond, temp, 10);
		strcat(t, temp);
		strcat(t, "\r\n");
		strcat(q, ">>>>>");
		strcat(p, "\r\n");
		strcat(q, p);
		strcat(t, q);
		strcpy(q, t);
		//printf("%s", q);
		for (int i = 0; i < num_count; i++)
		{
			while (strlen(q) % 8)
			{
				strcat(q, "\x06");
			}
			int Re=pf(ENCRYPT, q, key_crypt, pwd);
			if (Re>=0)
			{
				send(s_all[i], pwd, 5000, 0);
			}
			
		}
		
	}
	delete[]pwd;
	delete[]q;
	delete[]q_port;
	delete[]t;
	delete[]temp;
	FreeLibrary(h);




	return 0;
}
DWORD WINAPI Serve(LPVOID lp)
{
	CNetWork_ServiceDlg *pq = (CNetWork_ServiceDlg*)lp;
	//WinSock初始化  
	wVersionRequested = MAKEWORD(2, 2); //希望使用的WinSock DLL 的版本  
	ret = WSAStartup(wVersionRequested, &wsaData);
	if (ret != 0)
	{
		//::MessageBox(NULL, L"WSA初始化失败！\n", L"注意", MB_OK | MB_ICONWARNING);
		return 0;  
	}
	//创建Socket,使用TCP协议  
	sListen = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (sListen == INVALID_SOCKET)
	{
		WSACleanup();
		//::MessageBox(NULL, L"socket创建失败！\n", L"注意", MB_OK | MB_ICONWARNING);
		return 0;  
	}
	//构建本地地址信息  
	saServer.sin_family = AF_INET; //地址家族  
	saServer.sin_port = htons(SERVER_PORT); //注意转化为网络字节序  
	saServer.sin_addr.S_un.S_addr = htonl(INADDR_ANY); //使用INADDR_ANY 指示任意地址  

	//绑定  
	ret = bind(sListen, (struct sockaddr *)&saServer, sizeof(saServer));
	if (ret == SOCKET_ERROR)
	{
		//::MessageBox(NULL, L"bind失败！\n", L"注意", MB_OK | MB_ICONWARNING);
		closesocket(sListen); //关闭套接字  
		WSACleanup();
		return 0;  
	}

	//侦听连接请求  
	ret = listen(sListen, 500);
	if (ret == SOCKET_ERROR)
	{
		//::MessageBox(NULL, L"监听失败！\n", L"注意", MB_OK | MB_ICONWARNING);
		closesocket(sListen); //关闭套接字  
		return 0;  
	}
	pq->v_text = pq->v_text + L"服务器已启动！\r\n";
	//pq->UpdateData(FALSE);
	pq->v_text = pq->v_text + L"等待客户登陆...\r\n";
	//pq->UpdateData(FALSE);
	//阻塞等待接受客户端连接  
	while (1)//循环监听客户端，永远不停止，所以，在本项目中，我们没有心跳包。  
	{
		length = sizeof(saClient);
		sServer = accept(sListen, (struct sockaddr *)&saClient, &length);
		if (sServer == INVALID_SOCKET)
		{
			//::MessageBox(NULL, L"接受连接失败！\n", L"注意", MB_OK | MB_ICONWARNING);
			closesocket(sListen); //关闭套接字  
			WSACleanup();
			return 0;
		}
		s_all[num_count++] = sServer;
		A ev = { sServer, saClient };
		A *p = &ev;
		char *s_ = new char[10];
		_itoa(ev.si.sin_port, s_, 10);
		CString t1(inet_ntoa(ev.si.sin_addr));
		CString t2(s_);
		pq->v_text = pq->v_text + t1 + L":" + t2 + L"已连接\r\n";
		//pq->UpdateData(FALSE);
		HANDLE ht = CreateThread(NULL, 0, Send_Receive_func, p, 0, NULL);
		delete[]s_;
	}
	
	return 0;
}




// CAboutDlg dialog used for App About

class CAboutDlg : public CDialogEx
{
public:
	CAboutDlg();

// Dialog Data
	enum { IDD = IDD_ABOUTBOX };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

// Implementation
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


// CNetWork_ServiceDlg dialog



CNetWork_ServiceDlg::CNetWork_ServiceDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CNetWork_ServiceDlg::IDD, pParent)
	, v_text(_T(""))
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CNetWork_ServiceDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_EDIT1, v_text);
	DDX_Control(pDX, IDC_BUTTON1, m_bstart);
	DDX_Control(pDX, IDC_BUTTON2, m_bend);
}

BEGIN_MESSAGE_MAP(CNetWork_ServiceDlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDOK, &CNetWork_ServiceDlg::OnBnClickedOk)
	ON_BN_CLICKED(IDCANCEL, &CNetWork_ServiceDlg::OnBnClickedCancel)
	ON_WM_TIMER()
	ON_COMMAND(ID_32771, &CNetWork_ServiceDlg::On32771)
	ON_BN_CLICKED(IDC_BUTTON1, &CNetWork_ServiceDlg::OnBnClickedButton1)
	ON_BN_CLICKED(IDC_BUTTON2, &CNetWork_ServiceDlg::OnBnClickedButton2)
END_MESSAGE_MAP()


// CNetWork_ServiceDlg message handlers

BOOL CNetWork_ServiceDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
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

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	// TODO: Add extra initialization here
	SetTimer(1, 1000, NULL);         //启动定时器
	
	if (test==true)
	{
		m_bstart.EnableWindow(0);
		SetDlgItemText(IDC_STATUS, L"服务器工作中");
		CNetWork_ServiceDlg *l = this;
		CreateThread(NULL, 0, Serve, l, 0, NULL);
	}
	else
	{
		m_bend.EnableWindow(0);
		SetDlgItemText(IDC_STATUS, L"就绪");
	}
	
	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CNetWork_ServiceDlg::OnSysCommand(UINT nID, LPARAM lParam)
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

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CNetWork_ServiceDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CNetWork_ServiceDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}



void CNetWork_ServiceDlg::OnBnClickedOk()
{
	// TODO: Add your control notification handler code here
	//CDialogEx::OnOK();
}


void CNetWork_ServiceDlg::OnBnClickedCancel()
{
	// TODO: Add your control notification handler code here
	TerminateThread(Serve,0);//结束线程
	closesocket(sListen);
	closesocket(sServer);
	WSACleanup();
	CDialogEx::OnCancel();
}


void CNetWork_ServiceDlg::OnTimer(UINT_PTR nIDEvent)
{
	// TODO: Add your message handler code here and/or call default
	CString strTime;
	CTime tm;
	tm = CTime::GetCurrentTime();
	strTime = tm.Format("%Y-%m-%d %H:%M:%S");
	SetDlgItemText(IDC_ShowTime, strTime);        //显示系统时间
	SetDlgItemText(IDC_EDIT1, v_text);
	CDialogEx::OnTimer(nIDEvent);
}


void CNetWork_ServiceDlg::On32771()
{
	CAboutDlg clg;
	clg.DoModal();
	// TODO: Add your command handler code here
}


void CNetWork_ServiceDlg::OnBnClickedButton1()
{
	
	if (test==true)
	{
		
		return;
	}
	test = true;
	
	m_bstart.EnableWindow(0);
	m_bend.EnableWindow(1);
	CDialogEx::OnCancel();
	CLogin clg;
	clg.DoModal();
	// TODO: Add your control notification handler code here
}


void CNetWork_ServiceDlg::OnBnClickedButton2()
{
	test = false;
	SetDlgItemText(IDC_STATUS, L"就绪");
	m_bend.EnableWindow(0);
	m_bstart.EnableWindow(1);
	v_text.Empty();
	TerminateThread(Serve, 0);//结束线程
	closesocket(sListen);
	closesocket(sServer);
	WSACleanup();
	UpdateData(FALSE);
	// TODO: Add your control notification handler code here
}
