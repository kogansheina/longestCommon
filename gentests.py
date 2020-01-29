#!/usr/bin/env python
# the maximum command line in windows is 32767; we need SL*2+2*2 = 204 for every test
# 205 * 200 + 3 = 41003; so ....

NT = 50 # 200 - maximum number of tests
SL = 50 # 100 - maximum length of the strings
NS = 5  # number of common strings
MAXS = 100
MAXT = 200

import os
import sys
import random
import string
		
def background(filename):
	try:
		if os.path.exists(filename):
			os.remove(filename)
		out = open(filename,'w')
		random.seed()
		T = random.randint(1,NT) # generate number of tests
		out.write(str(T)+'\n')
		#print("T="+str(T))

		for i in range(T):
			out.write("TEST "+str(i+1)+'\n')
			expected = ""
			# generate number of common substring
			n = random.randint(1,NS)
			Morig = random.randint(1,SL) # permit empty string, otherwise start from 2
			Norig = random.randint(1,SL)
			M = Morig
			N = Norig
			if M < N:
				clen = M -1
			else:
				clen = N - 1
			#print("n=%d M=%d N=%d clen=%d" % (n,M,N,clen))
			# generate common strings
			l = []
			ss = ''.join(random.choices(string.digits, k=clen))
			xp = random.randint(0,M) # generate random position into the first string
			if xp > M:
				xp = M - 1
			yp = random.randint(0,N) # generate random position into the second string
			if yp > N:
				yp = N - 1	
			if len(ss) != 0:
				if len(ss) + M > MAXS or len(ss) + N > MAXS: # cut down from the common to fit the max length
					if M < N:
						q = N + len(ss) - MAXS
					else:
						q = M + len(ss) - MAXS
					ss = ss[:len(ss) - q]			
				l.append((ss,xp,yp))
				M += len(ss)        # update the string length with the common length
				N += len(ss)

			if M < MAXS and N < MAXS:  # more common strings
				for j in range(1,n):
					ss = ''.join(random.choices(string.digits, k=clen))
					if len(l) > 0:
						if l[j-1][1] + len(ss)+ 1 < M:
							xp = random.randint(l[j-1][1] + len(ss)+ 1,M)
							if xp > M:
								xp = M - 1
						else:
							break
					else:
						xp = random.randint(len(ss)+ 1,M)
						if xp > M:
							xp = M - 1
					if len(l) > 0:		
						if l[j-1][2] + len(ss)+ 1 < N:
							yp = random.randint(l[j-1][2] + len(ss)+ 1,N)
							if yp > N:
								yp = N - 1
						else:
							break
					else:
						yp = random.randint(len(ss)+ 1,N)
						if yp > N:
							yp = N - 1			
					if len(ss) != 0:
						if len(ss) + M > MAXS or len(ss) + N > MAXS:
							if M < N:
								q = N + len(ss) - MAXS
							else:
								q = M + len(ss) - MAXS
							ss = ss[:len(ss) - q]
						l.append((ss,xp,yp))
						M += len(ss)
						N += len(ss)				
					if M >= MAXS or N >= MAXS:
						break
			# generate sterings
			X = ''.join(random.choices(string.ascii_uppercase, k=Morig))
			Y = ''.join(random.choices(string.ascii_lowercase, k=Norig))
			# insert common strings	
			for t in l:
				qx = X[t[1]:]
				X = X[:t[1]]+t[0]+qx
				qy = Y[t[2]:]
				Y = Y[:t[2]]+t[0]+qy	
				expected += t[0] + ',' + str(t[1]) + ',' + str(t[2]) + ';'
				
			out.write(str(M)+' '+str(N)+' '+X+' '+Y+'\n')
			l = expected.split(';')
			
			out.write(str(len(l)-1)+'\n')  # number of expected
			for k in range(len(l)-1):
				out.write(l[k]+'\n')	
		out.close()
		
	except IOError:
			print ("Cannot open %s file" % (filename))			
 
if __name__ == "__main__":
	if len(sys.argv) < 2:
		print ("Arguments needed - file name")
		sys.exit(1)
	background(sys.argv[1])
