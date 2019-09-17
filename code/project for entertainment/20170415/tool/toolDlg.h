
// toolDlg.h : header file
//

#pragma once


// CtoolDlg dialog
class CtoolDlg : public CDialogEx
{
// Construction
public:
	CtoolDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_TOOL_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedOk();
	afx_msg void OnBnClickedCancel();
	afx_msg void OnBnClickedShutdown();
	afx_msg void OnBnClickedReboot();
	afx_msg void OnBnClickedLogoff();
	int time_h;
	int time_m;
	int time_s;
	afx_msg void OnBnClickedCancelall();
};
