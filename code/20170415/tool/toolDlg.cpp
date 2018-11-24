
// toolDlg.cpp : implementation file
//

#include "stdafx.h"
#include "tool.h"
#include "toolDlg.h"
#include "afxdialogex.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

#define SE_CREATE_TOKEN_NAME              TEXT("SeCreateTokenPrivilege")
#define SE_ASSIGNPRIMARYTOKEN_NAME        TEXT("SeAssignPrimaryTokenPrivilege")
#define SE_LOCK_MEMORY_NAME               TEXT("SeLockMemoryPrivilege")
#define SE_INCREASE_QUOTA_NAME            TEXT("SeIncreaseQuotaPrivilege")
#define SE_UNSOLICITED_INPUT_NAME         TEXT("SeUnsolicitedInputPrivilege")
#define SE_MACHINE_ACCOUNT_NAME           TEXT("SeMachineAccountPrivilege")
#define SE_TCB_NAME                       TEXT("SeTcbPrivilege")
#define SE_SECURITY_NAME                  TEXT("SeSecurityPrivilege")
#define SE_TAKE_OWNERSHIP_NAME            TEXT("SeTakeOwnershipPrivilege")
#define SE_LOAD_DRIVER_NAME               TEXT("SeLoadDriverPrivilege")
#define SE_SYSTEM_PROFILE_NAME            TEXT("SeSystemProfilePrivilege")
#define SE_SYSTEMTIME_NAME                TEXT("SeSystemtimePrivilege")
#define SE_PROF_SINGLE_PROCESS_NAME       TEXT("SeProfileSingleProcessPrivilege")
#define SE_INC_BASE_PRIORITY_NAME         TEXT("SeIncreaseBasePriorityPrivilege")
#define SE_CREATE_PAGEFILE_NAME           TEXT("SeCreatePagefilePrivilege")
#define SE_CREATE_PERMANENT_NAME          TEXT("SeCreatePermanentPrivilege")
#define SE_BACKUP_NAME                    TEXT("SeBackupPrivilege")
#define SE_RESTORE_NAME                   TEXT("SeRestorePrivilege")
#define SE_SHUTDOWN_NAME                  TEXT("SeShutdownPrivilege")
#define SE_DEBUG_NAME                     TEXT("SeDebugPrivilege")
#define SE_AUDIT_NAME                     TEXT("SeAuditPrivilege")
#define SE_SYSTEM_ENVIRONMENT_NAME        TEXT("SeSystemEnvironmentPrivilege")
#define SE_CHANGE_NOTIFY_NAME             TEXT("SeChangeNotifyPrivilege")
#define SE_REMOTE_SHUTDOWN_NAME           TEXT("SeRemoteShutdownPrivilege")
#define SE_UNDOCK_NAME                    TEXT("SeUndockPrivilege")
#define SE_SYNC_AGENT_NAME                TEXT("SeSyncAgentPrivilege")
#define SE_ENABLE_DELEGATION_NAME         TEXT("SeEnableDelegationPrivilege")
#define SE_MANAGE_VOLUME_NAME             TEXT("SeManageVolumePrivilege")
#define SE_IMPERSONATE_NAME               TEXT("SeImpersonatePrivilege")
#define SE_CREATE_GLOBAL_NAME             TEXT("SeCreateGlobalPrivilege")
#define SE_TRUSTED_CREDMAN_ACCESS_NAME    TEXT("SeTrustedCredManAccessPrivilege")
#define SE_RELABEL_NAME                   TEXT("SeRelabelPrivilege")
#define SE_INC_WORKING_SET_NAME           TEXT("SeIncreaseWorkingSetPrivilege")
#define SE_TIME_ZONE_NAME                 TEXT("SeTimeZonePrivilege")
#define SE_CREATE_SYMBOLIC_LINK_NAME      TEXT("SeCreateSymbolicLinkPrivilege")


bool EnableSpecificPrivilege(LPCTSTR lpPrivilegeName)
{

	HANDLE hToken = NULL;
	TOKEN_PRIVILEGES Token_Privilege;
	BOOL bRet = TRUE;

	do
	{
		if (0 == OpenProcessToken(GetCurrentProcess(), TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, &hToken))
		{
			
			bRet = FALSE;
			break;
		}

		if (0 == LookupPrivilegeValue(NULL, lpPrivilegeName, &Token_Privilege.Privileges[0].Luid))
		{
			
			bRet = FALSE;
			break;
		}

		Token_Privilege.PrivilegeCount = 1;
		Token_Privilege.Privileges[0].Attributes = SE_PRIVILEGE_ENABLED;
		//Token_Privilege.Privileges[0].Luid.LowPart=17;//SE_BACKUP_PRIVILEGE  
		//Token_Privilege.Privileges[0].Luid.HighPart=0;  


		if (0 == AdjustTokenPrivileges(hToken, FALSE, &Token_Privilege, sizeof(Token_Privilege), NULL, NULL))
		{
			
			bRet = FALSE;
			break;
		}

	} while (false);

	if (NULL != hToken)
	{
		CloseHandle(hToken);
	}

	return bRet;

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


// CtoolDlg dialog



CtoolDlg::CtoolDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CtoolDlg::IDD, pParent)
	, time_h(0)
	, time_m(0)
	, time_s(0)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CtoolDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_EDIT1, time_h);
	DDX_Text(pDX, IDC_EDIT2, time_m);
	DDX_Text(pDX, IDC_EDIT3, time_s);
}

BEGIN_MESSAGE_MAP(CtoolDlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDOK, &CtoolDlg::OnBnClickedOk)
	ON_BN_CLICKED(IDCANCEL, &CtoolDlg::OnBnClickedCancel)
	ON_BN_CLICKED(IDC_SHUTDOWN, &CtoolDlg::OnBnClickedShutdown)
	ON_BN_CLICKED(IDC_REBOOT, &CtoolDlg::OnBnClickedReboot)
	ON_BN_CLICKED(IDC_LOGOFF, &CtoolDlg::OnBnClickedLogoff)
	ON_BN_CLICKED(IDC_CANCELALL, &CtoolDlg::OnBnClickedCancelall)
END_MESSAGE_MAP()


// CtoolDlg message handlers

BOOL CtoolDlg::OnInitDialog()
{
	DeleteFileA("restart.bat");
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

	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CtoolDlg::OnSysCommand(UINT nID, LPARAM lParam)
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

void CtoolDlg::OnPaint()
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
HCURSOR CtoolDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}



void CtoolDlg::OnBnClickedOk()
{
	// TODO: Add your control notification handler code here
	CDialogEx::OnOK();
}


void CtoolDlg::OnBnClickedCancel()
{
	// TODO: Add your control notification handler code here
	CDialogEx::OnCancel();
}


void CtoolDlg::OnBnClickedShutdown()
{
	ShowWindow(SW_HIDE);
	EnableSpecificPrivilege(SE_SHUTDOWN_NAME);
	UpdateData(TRUE);
	int time = time_h * 3600 + time_m * 60 + time_s;
	time = time * 1000;
	time_h = time_m = time_s = 0;
	UpdateData(FALSE);
	Sleep(time);
	ExitWindowsEx(EWX_SHUTDOWN|EWX_FORCE, 0);
	ShowWindow(SW_SHOW);
	// TODO: Add your control notification handler code here
}


void CtoolDlg::OnBnClickedReboot()
{
	ShowWindow(SW_HIDE);
	EnableSpecificPrivilege(SE_SHUTDOWN_NAME);
	UpdateData(TRUE);
	int time = time_h * 3600 + time_m * 60 + time_s;
	time = time * 1000;
	time_h = time_m = time_s = 0;
	UpdateData(FALSE);
	Sleep(time);
	ExitWindowsEx(EWX_REBOOT | EWX_FORCE, 0);
	ShowWindow(SW_SHOW);
	// TODO: Add your control notification handler code here
}


void CtoolDlg::OnBnClickedLogoff()
{
	ShowWindow(SW_HIDE);
	UpdateData(TRUE);
	int time = time_h * 3600 + time_m * 60 + time_s;
	time = time * 1000;
	time_h = time_m = time_s = 0;
	UpdateData(FALSE);
	Sleep(time);
	ExitWindowsEx(EWX_LOGOFF | EWX_FORCE, 0);
	ShowWindow(SW_SHOW);
	// TODO: Add your control notification handler code here
}


void CtoolDlg::OnBnClickedCancelall()
{
	FILE *fp = fopen("restart.bat", "w");
	if (fp)
	{
		fprintf(fp, "@echo off\n");
		fprintf(fp, "tool.exe");
		fclose(fp);
	}
	WinExec("restart.bat", SW_HIDE);
	
	exit(0);
	// TODO: Add your control notification handler code here
}
