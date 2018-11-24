import time
import os
import requests
def main():
	l="3"
	j=2013
	k=0
	flag=0
	os.system("cls")
	while j<2017:
		i=3000101001+j%100*10000000
		dir='D:/photo/'+str(j)
		if os.path.exists(dir)==False:
			os.mkdir(dir)
		while i<3160999999:
			d="http://xsgl.ujs.edu.cn/student/tpxs.aspx?id="
			d=d+'/'+str(j)+'/'
			h=str(i)+".jpg"
			d=d+h
			print(d)
			r=requests.get(d)
			if r.status_code==200:
				nam=dir+"/"+h
				fp=open(nam,"wb")
				fp.write(r.content)
				fp.close()
				if os.path.getsize(nam)/1024<1:
					os.remove(nam)
					flag=flag+1
					if flag>5:
						if flag%6==0:
							i=i//1000*1000+1000
						if flag>17:
							if flag%18==0:
								i=i//100000*100000+101000
							if flag>53:
								if flag%54==0:
									flag=0
									break		
				else:
					flag=0		
				
				k=k+1
				if k==5:
					k=0
					print("Downloading...Please wait...")
					time.sleep(5)
					#os.system("cls")
			i=i+1
		j=j+1
	return
if __name__=="__main__": 
	main()


#3150604028
