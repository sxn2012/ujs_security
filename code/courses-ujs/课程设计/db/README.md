软件简介
软件系统名称：Magazine Ordering
软件开发平台：Microsoft®Visual Studio 2013 Ultimate、Microsoft®SQL ServerTM2014
该软件运行于Windows平台。
运行环境
服务器端
（1）硬件环境
1GHz或更高的微处理器
使用1G或以上内存
不少于100MB的可用硬盘空间
（2）软件环境
WindowsServer 2008 R2X64或以上的操作系统
SQLServer2008或以上的数据库管理系统
.Net Framework 4.5或以上
客户端
（1）硬件环境
1GHz或更高的微处理器
使用1G或以上内存
不少于100MB的可用硬盘空间
（2）软件环境
Windows7X64或以上的操作系统
.Net Framework 4.5或以上
Visual Studio 2010或以上的编译环境
使用说明
安装和初始化
安装系统必备软件
在服务器上安装好WindowsServer 2008 R2 操作系统，配置好TCP/IP协议，配置好DNS服务器。
在服务器上安装好SQL Server2008，并将用户名和密码配置成用户名：admin，密码：123456。
在服务器上安装好.NET Framework 4.5运行库。
在客户端安装好Windows7操作系统，配置好TCP/IP协议，配置好DNS服务器。
在客户端安装Visual Studio 2013和.NET Framework4.5运行库。
安装报刊管理系统
1.桌面上双击安装文件setup.exe。
2.选择安装路径，并点击下一步按钮。
3.选择是否创建桌面快捷方式，并点击下一步按钮。
4.点击确认安装按钮。
5.安装完成后，选择立即启动软件，点击完成，安装结束。
配置数据库
1.服务器端打开SQL Server 2008,选择Windows身份验证登陆方式，点击连接。
2.连接后打开CreateDB.sql文件，点击执行。
3.打开CreateTab.sql文件，点击执行。
4.打开LoadData.sql文件，点击执行。
5.至此，数据库配置已完成。 
启动报刊管理系统
1.双击MagazineOrdering.exe，启动报刊管理系统。
2.用户登录，管理员的用户名为2001，密码为root。

