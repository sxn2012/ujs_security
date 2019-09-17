
// HOTPORTDlg.cpp : implementation file
//

#include "stdafx.h"
#include "HOTPORT.h"
#include "HOTPORTDlg.h"
#include "afxdialogex.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


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


// CHOTPORTDlg dialog



CHOTPORTDlg::CHOTPORTDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CHOTPORTDlg::IDD, pParent)
	, user(_T(""))
	, pass(_T(""))
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CHOTPORTDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_EDIT1, user);
	DDX_Text(pDX, IDC_EDIT2, pass);
}

BEGIN_MESSAGE_MAP(CHOTPORTDlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDOK, &CHOTPORTDlg::OnBnClickedOk)
	ON_BN_CLICKED(IDCANCEL, &CHOTPORTDlg::OnBnClickedCancel)
	ON_BN_CLICKED(IDC_ButtonOff, &CHOTPORTDlg::OnBnClickedButtonoff)
END_MESSAGE_MAP()


// CHOTPORTDlg message handlers

BOOL CHOTPORTDlg::OnInitDialog()
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

	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CHOTPORTDlg::OnSysCommand(UINT nID, LPARAM lParam)
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

void CHOTPORTDlg::OnPaint()
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
HCURSOR CHOTPORTDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}



void CHOTPORTDlg::OnBnClickedOk()
{
	UpdateData(TRUE);
	
	FILE *fp = fopen("hpturnon.bat", "w");
	if (fp)
	{
		fprintf(fp, "@echo off\n");
		fprintf(fp, "netsh wlan set hostednetwork mode=allow ssid=");
		fprintf(fp, "%S key=%S\n", user, pass);
		fprintf(fp, "netsh wlan start hostednetwork\n");
		fprintf(fp, "del hpturnon.bat\n");
		fclose(fp);
		WinExec("hpturnon.bat",SW_HIDE);
		//DeleteFileA("hpturnon.bat");
	}
	// TODO: Add your control notification handler code here
	//CDialogEx::OnOK();
	CString a, b;
	user = a;
	pass = b;
	UpdateData(FALSE);
}


void CHOTPORTDlg::OnBnClickedCancel()
{
	
	// TODO: Add your control notification handler code here
	CDialogEx::OnCancel();
}


void CHOTPORTDlg::OnBnClickedButtonoff()
{
	FILE *fp = fopen("hpturnoff.bat", "w");
	if (fp)
	{
		fprintf(fp, "@echo off\n");
		fprintf(fp, "netsh wlan stop hostednetwork\n");
		fprintf(fp, "netsh wlan set hostednetwork mode=disallow\n");
		fprintf(fp, "del hpturnoff.bat\n");
		fclose(fp);
		WinExec("hpturnoff.bat", SW_HIDE);
		//DeleteFileA("hpturnoff.bat");
	}
	// TODO: Add your control notification handler code here
}
