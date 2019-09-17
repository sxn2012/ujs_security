using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace FileManagement_Admin
{
    public class Socket_Receive
    {
        private string _ip = string.Empty;
        private int _port = 0;
        private Socket _socket = null;
        private byte[] buffer = new byte[1024 * 1024 * 2];
        private Thread thread = null;
        
        public Socket_Receive(string ip, int port)
        {
            this._ip = ip;
            this._port = port;
            StartListen();
        }
        public void StartListen()
        {
            try
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//实例化套接字
                IPAddress address = IPAddress.Parse(_ip);//创建IP对象
                IPEndPoint endPoint = new IPEndPoint(address, _port);//创建网络端口包括ip和端口
                _socket.Bind(endPoint);//绑定
                _socket.Listen(int.MaxValue);//监听客户端连接
                Thread thread = new Thread(ListenClientConnect);
                thread.Start();
            }
            catch (Exception)
            {
                
            }
        }
        private void ListenClientConnect()
        {
            while(true)
            { 
                try
                {
                    Socket clientSocket = _socket.Accept();//接受客户端连接
                    thread = new Thread(ReceiveMessage);
                    thread.Start(clientSocket);
                }
                catch (Exception)
                {
                    break; 
                }
            }
        }
        private void ReceiveMessage(object socket)
        {
            
            Socket clientSocket = (Socket)socket;
            while (true)
            {
                
                try
                {
                    //接收从客户端发来的数据
                    int length = clientSocket.Receive(buffer);
                    string str = Encoding.UTF8.GetString(buffer, 0, length).Trim();
                    str = CryptoClass.AesDecrypt(str, CryptoClass.key);//解密
                    int f = str.IndexOf("用户");
                    if (str != "" && str != null && f >= 0)
                        LogWrite(str);
                    else
                        break;
                    
                }
                catch (Exception)
                {
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                    break;
                }
            }
        }

        private void LogWrite(string message)
        {
            int i = 0;
            while(true)
            { 
                try
                {
                    StreamWriter sw = new StreamWriter("sys.log", true);
                    sw.WriteLine(DateTime.Now.ToString());
                    sw.WriteLine(message);
                    sw.Close();
                    FileInfo fi = new FileInfo("sys.log");
                    if (fi != null && fi.Exists && fi.Length >= 1024)
                    {
                        string rename = DateTime.Now.Year.ToString();
                        int temp = DateTime.Now.Month;
                        if (temp < 10) rename = rename + "0" + temp;
                        else rename = rename + temp;
                        temp = DateTime.Now.Day;
                        if (temp < 10) rename = rename + "0" + temp;
                        else rename = rename + temp;
                        temp = DateTime.Now.Hour;
                        if (temp < 10) rename = rename + "0" + temp;
                        else rename = rename + temp;
                        temp = DateTime.Now.Minute;
                        if (temp < 10) rename = rename + "0" + temp;
                        else rename = rename + temp;
                        temp = DateTime.Now.Second;
                        if (temp < 10) rename = rename + "0" + temp;
                        else rename = rename + temp;
                        rename = rename + ".log";
                        rename = rename.Trim();
                        File.Copy("sys.log", rename);//复制日志文件
                        FileStream fs = new FileStream("sys.log", FileMode.Truncate, FileAccess.ReadWrite);//清空文件
                        fs.Close();//清空原日志文件
                        
                    }
                    return;
                }
                catch(Exception)
                {
                    Thread.Sleep(3000);
                    i++;
                    if (i < 5)
                        continue;
                    else
                        return;
                }
            }
        }

        
        
    }
}
