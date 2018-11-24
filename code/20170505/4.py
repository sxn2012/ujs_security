import urllib
import requests
import os
import re
from bs4 import BeautifulSoup
def main():
	s=requests.session()
	user=input('Please input your account:')
	passwd=input("Please input your password:")
	proxies={
	'http':'http://127.0.0.1:1080',
	'https':'https://127.0.0.1:1080',
	}
	headers={
	'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36'
	}
	data = {'email':user,'pass':passwd}
	url='https://www.facebook.com/login.php?login_attempt=1'
	resp=s.get(url,headers=headers,proxies=proxies,timeout=100)
	#print(resp.content)
	#r=resp
	r=s.post(url,data,headers=headers,proxies=proxies,timeout=100)
	fp=open("F://1.html","wb")
	fp.write(resp.content)
	fp.close()
	fp=open("F://2.html","wb")
	fp.write(r.content)
	fp.close()
	#print(r.content.decode('gb2312')) 
	if os.path.getsize("F://2.html")/1024<100:
		print("Login Failure!")
	else:
		print("Login Success!")
		fil=open("F:\\a.txt","r")
		fin=open("F:\\b.txt","w",encoding='utf-8')
		while(1):
			search=fil.readline()
			if not search:
				break
			#search=input('Please input the information:')

			url='https://www.facebook.com/search/top/?q='+search+'&init=mag_glass&tas=0.47663200767140945'
			r=s.get(url,verify = False,headers=headers,proxies=proxies,timeout=100)
			fp=open("F://3.html","wb")
			fp.write(r.content)
			fp.close()
			#web=open("F://3.html",'rb')
			#htm=web.read()
			htm=r.content
			htm=htm.decode('utf-8')
			#soup = BeautifulSoup(htm,"html.parser")
			#soup=BeautifulSoup(htm,"html.parser")
			#pid=soup.findAll('a',{'class':'_5d-5'})
			#pid=soup.findAll(name='a',attrs={'data-testid':re.compile(r'^serp_result_link')})
			pid=re.findall(r'<div class="_5d-5">(.*)</div></div></a></div></div>',htm)
			if pid:
				st=search+' '+pid[0]+'\n'
				#stri=bytes(s,encoding="utf-8")
				fin.write(st)
			else:
				st=search+" Not Found!\n"
				#stri=bytes(s,encoding="utf-8")
				fin.write(st)
		fil.close()
		fin.close()
	return
if __name__=="__main__": 
	main()
