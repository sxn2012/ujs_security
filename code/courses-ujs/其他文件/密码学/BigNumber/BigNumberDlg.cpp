
// BigNumberDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "BigNumber.h"
#include "BigNumberDlg.h"
#include "afxdialogex.h"
#include "BigInt.h"
#ifdef _DEBUG
#define new DEBUG_NEW
#endif


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


// CBigNumberDlg 对话框



CBigNumberDlg::CBigNumberDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CBigNumberDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CBigNumberDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_EDIT1, edit_a);
	DDX_Control(pDX, IDC_EDIT2, edit_b);
	DDX_Control(pDX, IDC_EDIT3, edit_result);
}

BEGIN_MESSAGE_MAP(CBigNumberDlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_ADD, &CBigNumberDlg::OnBnClickedAdd)
	ON_BN_CLICKED(IDC_SUB, &CBigNumberDlg::OnBnClickedSub)
	ON_BN_CLICKED(IDC_MUL, &CBigNumberDlg::OnBnClickedMul)
	ON_BN_CLICKED(IDC_DIV, &CBigNumberDlg::OnBnClickedDiv)
	ON_BN_CLICKED(IDC_MOD, &CBigNumberDlg::OnBnClickedMod)
END_MESSAGE_MAP()


// CBigNumberDlg 消息处理程序

BOOL CBigNumberDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

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

	return TRUE;  // 除非将焦点设置到控件，否则返回 TRUE
}

void CBigNumberDlg::OnSysCommand(UINT nID, LPARAM lParam)
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

void CBigNumberDlg::OnPaint()
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
HCURSOR CBigNumberDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}



void CBigNumberDlg::OnBnClickedAdd()
{
	// TODO: Add your control notification handler code here
	CBigInt a, b, c;
	CString str1, str2, str3;
	GetDlgItem(IDC_EDIT1)->GetWindowText(str1);
	GetDlgItem(IDC_EDIT2)->GetWindowText(str2);
	a.Get(str1, 10);
	b.Get(str2, 10);
	c = a + b;
	c.Put(str3, 10);
	GetDlgItem(IDC_EDIT3)->SetWindowText(str3);
}


void CBigNumberDlg::OnBnClickedSub()
{
	// TODO: Add your control notification handler code here
	CBigInt a, b, c;
	CString str1, str2, str3;
	GetDlgItem(IDC_EDIT1)->GetWindowText(str1);
	GetDlgItem(IDC_EDIT2)->GetWindowText(str2);
	a.Get(str1, 10);
	b.Get(str2, 10);
	c = a - b;
	c.Put(str3, 10);
	GetDlgItem(IDC_EDIT3)->SetWindowText(str3);
}


void CBigNumberDlg::OnBnClickedMul()
{
	// TODO: Add your control notification handler code here
	CBigInt a, b, c;
	CString str1, str2, str3;
	GetDlgItem(IDC_EDIT1)->GetWindowText(str1);
	GetDlgItem(IDC_EDIT2)->GetWindowText(str2);
	a.Get(str1, 10);
	b.Get(str2, 10);
	c = a * b;
	c.Put(str3, 10);
	GetDlgItem(IDC_EDIT3)->SetWindowText(str3);
}


void CBigNumberDlg::OnBnClickedDiv()
{
	// TODO: Add your control notification handler code here
	CBigInt a, b, c;
	CString str1, str2, str3;
	GetDlgItem(IDC_EDIT1)->GetWindowText(str1);
	GetDlgItem(IDC_EDIT2)->GetWindowText(str2);
	a.Get(str1, 10);
	b.Get(str2, 10);
	c = a / b;
	c.Put(str3, 10);
	GetDlgItem(IDC_EDIT3)->SetWindowText(str3);
}


void CBigNumberDlg::OnBnClickedMod()
{
	// TODO: Add your control notification handler code here
	CBigInt a, b, c;
	CString str1, str2, str3;
	GetDlgItem(IDC_EDIT1)->GetWindowText(str1);
	GetDlgItem(IDC_EDIT2)->GetWindowText(str2);
	a.Get(str1, 10);
	b.Get(str2, 10);
	c = a % b;
	c.Put(str3, 10);
	GetDlgItem(IDC_EDIT3)->SetWindowText(str3);
}
