
#include "resource.h"
int main(int argc,char *argv[])
{
	system("color 64");
	HWND hwnd = GetForegroundWindow();//使hwnd代表最前端的窗口
	ShowWindow(hwnd, SW_MAXIMIZE);//最大化 hwnd 所代表的窗口 
	//SetConsoleTitle(L"王的传承——数据结构课程设计");//设置窗口标题
	SetWindowTextW(hwnd, L"王的传承——数据结构课程设计");
	Tree t;
	Status s=t.Init();
	if (s==FAILURE)
	{
		::MessageBoxW(NULL, L"初始化失败", L"注意", MB_ICONERROR | MB_OK);
		return -1;
	}
	t.Menu();
	system("pause");

	return 0;
}