import socket,sys,os,select,time,threading,msvcrt
from OpenSSL import *
def verifycert_func(connection_test, certificate_test, error_number, depth, ok):
	# 验证证书有效性
	certstr=certificate_test.get_subject()
	tempstr=str(certstr)
	tempstr=tempstr[19:-2]
	tempstr=tempstr.replace('/',',')
	tempstr=tempstr.replace('=',':')
	print 'Certificate has been verified: ' + tempstr
	return ok
ip=raw_input("Enter IP address:")
port=raw_input("Enter port number:")
os.system("pause")
threadLock=threading.Lock()
# 初始化并加载证书
receiving_context = SSL.Context(SSL.SSLv23_METHOD)
receiving_context.set_options(SSL.OP_NO_SSLv2)
receiving_context.set_verify(SSL.VERIFY_PEER|SSL.VERIFY_FAIL_IF_NO_PEER_CERT, verifycert_func) # Demand a certificate
receiving_context.use_privatekey_file ('serverkey.pem')
receiving_context.use_certificate_file('servercrt.pem')
receiving_context.load_verify_locations('cacrt.pem')
# 设置接收套接字
socket_r = SSL.Connection(receiving_context, socket.socket(socket.AF_INET, socket.SOCK_STREAM))
socket_r.bind((ip, int(port)))
socket_r.listen(10)
connection_, address_ = socket_r.accept()		
print 'a client has been connected. IP address:'+str(address_[0])+' Port:'+str(address_[1])
ip_=raw_input("Enter IP address:")
port_=raw_input("Enter port number:")
os.system("pause")
# 初始化并加载证书
sending_context = SSL.Context(SSL.SSLv23_METHOD)
sending_context.set_verify(SSL.VERIFY_PEER, verifycert_func) # Demand a certificate
sending_context.use_privatekey_file ('clientkey.pem')
sending_context.use_certificate_file('clientcrt.pem')
sending_context.load_verify_locations('cacrt.pem')
# 设置发送套接字
socket_s = SSL.Connection(sending_context, socket.socket(socket.AF_INET, socket.SOCK_STREAM))
socket_s.connect((ip_, int(port_)))


#发送线程
class Sending_Thread(threading.Thread):
	def __init__(self,threadID,name,counter):
		threading.Thread.__init__(self)
		self.threadID=threadID
		self.name=name
		self.counter=counter
		
	def run(self):
		#threadLock.acquire()
		while 1:
			sending()
		#threadLock.release()
#接收线程
class Receiving_Thread(threading.Thread):
	def __init__(self,threadID,name,counter):
		threading.Thread.__init__(self)
		self.threadID=threadID
		self.name=name
		self.counter=counter
		
	def run(self):
		
			
		#threadLock.acquire()
		receiving()
		#threadLock.release()
		#time.sleep(2)

#用于发送的函数
def sending():
	sending_message = sys.stdin.readline()#读入输入的数据 			
	if sending_message=='q\n':	
		return
	try:
		socket_s.send(sending_message)#发送数据
	except SSL.Error:
		print 'link has been disconnected.'
		return
#用于接收的函数
def receiving():
	while 1:
		try:	
			received_message = connection_.recv(1024)#接收数据
			sys.stdout.write(received_message)#显示
			sys.stdout.flush()
		except SSL.Error:
			print 'link has been disconnected.'
			return
#主程序中开启发送和接收线程
receiving_T=Receiving_Thread(1,"receive",1)
sending_T=Sending_Thread(2,"send",2)
receiving_T.start()
sending_T.start()



