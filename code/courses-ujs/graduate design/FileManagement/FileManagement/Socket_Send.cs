using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace FileManagement
{
    public class Socket_Send//发送数据类
    {
        private string _ip = string.Empty;
        private int _port = 0;
        private Socket _socket = null;
        private byte[] buffer = new byte[1024 * 1024 * 2];
        
        public Socket_Send(string ip, int port)
        {
            this._ip = ip;
            this._port = port;//初始化ip，端口
            StartClient();
        }
        public void StartClient()
        {
            try
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//实例化套接字
                IPAddress address = IPAddress.Parse(_ip);//创建IP对象
                IPEndPoint endPoint = new IPEndPoint(address, _port);//创建网络端口包括ip和端口
                _socket.Connect(endPoint);//建立连接
            }
            catch (Exception)
            {
                _socket.Shutdown(SocketShutdown.Both);
                _socket.Close();
            }
            
        }

        public void Sending(string message)//发送消息
        {
            try
            {
                Thread thread = new Thread(SendMessage);//开启发送线程
                thread.Start(message);
            }
            catch(Exception)
            {
            }
        }

        private void SendMessage(object message)
        {
            try
            {
                string sendMessage = (string)message;
                sendMessage = sendMessage.Trim();//获取消息
                if (sendMessage != null && sendMessage != "")
                {
                    sendMessage = CryptoClass.AesEncrypt(sendMessage, CryptoClass.key);//加密
                    _socket.Send(Encoding.UTF8.GetBytes(sendMessage));//编码，发送
                }
                
            }
            catch(Exception)
            {
                _socket.Shutdown(SocketShutdown.Both);
                _socket.Close();
            }
        }

        
        
    }
}
