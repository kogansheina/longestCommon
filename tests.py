#!/usr/bin/env python

import os
import sys
from time import sleep
"""
  expected is a list of expted tuples : string, index in first string, index in second string
  fl is alist of read line for eac test
"""
def check_result(expected,fl,testnr):
	indexpect = 0
	indexlines = 0
	line = fl[indexlines]
	ind = line.find("=>TEST=")
	nr = testnr
	if ind == -1:
		print("ERROR 1")
	else:
		sub = line[ind+len("=>TEST="):]
		words = sub.split()					
		if testnr != int(words[0]): # test number
			print("required "+str(testnr)+" detected "+words[0])
			print(line)
	indexlines += 3 # skip 'actual' and the empty line															
	line = fl[indexlines]
	ind = line.find("The longest")
	if ind != -1:
		s = line[ind + len("The longest")]
		words = line[ind+len("The longest"):].split()
		longest = int(words[0][1:])
		if longest != 0:
			print("The longest common string has "+str(longest)+" characters")
		else:
			print("test "+str(testnr)+" passed : none expected")	
	else:
		print("ERROR 2")
	indexlines += 1
	line = fl[indexlines]
	ind = line.find("indPivot")
	if ind != -1:
		#print(line[ind:])
		w = line[ind:].split()
		#w[0] = indPivot
		#w[1] = indTest
		#w[2] = match
		pivot = w[0][len("indPivot="):]
		test =  w[1][len("indTest="):]	
		match = w[2][len("match="):]	
		if indexpect < len(expected):
			y = expected[indexpect].split(',')
			if match == y[0]:
				print("test "+str(testnr)+" passed : match="+ match)
			elif len(y[0]) != 0:
				print("test "+str(testnr)+" failed : expected="+y[0]+" received="+match)
			indexpect += 1
		else:	
			print("test "+str(testnr)+" failed : none expected, but received="+match)													
					
def background(filename,logname):

	try:
		fl = open(filename,'r')
		line = fl.readline()
		if len(line) != 0:
			T = int(line)  # read number of tests
			running = [line[:len(line)-1]]
			allexpected = []  # list of expected lists
			for l in range(T):
				expected = []  # list of expected results for each test
				line = fl.readline() # read text
				line = fl.readline() # read one test
				if len(line) == 0:
					break				
				running.append(line[:len(line)-1])
				line = fl.readline() 
				line = line[:len(line)-1] # read the number of common strings
				E = int(line)
				for l in range(E):
					line = fl.readline()  # read all the expected for the test
					if len(line) == 0:
						break
					expected.append(line[:len(line)-1])
				allexpected.append(expected)				
		fl.close()
		line = ' '.join(running)
		lt = "dotnet run " + logname +" " + line
		print("Execute: "+filename+" : " + lt) 
		os.system(lt)
		#sleep(1)
		
		try:
			fl = open(logname,'r')   # open the log file, it is the result of the algorithm
			#print(allexpected)
			for l in range(T):
				readl = []
				eof = False
				while not eof:			
					line = fl.readline()
					if len(line) == 0:
						eof = True
					else:
						readl.append(line) 
						if line.find("Test:") != -1:
							fl.readline()
							break               
				check_result(allexpected[l],readl,l+1)				
			fl.close()

		except IOError:
			print('(3) File %s cannot be open' % (logname))
				
	except IOError:
		print('(2) File %s cannot be open' % (filename))
    
def main(targ,larg):
	background(targ,larg)
 
if __name__ == "__main__":
	if len(sys.argv) < 3:
		print("Arguments needed - test file name, log file name")
		sys.exit(1)
	main(sys.argv[1],sys.argv[2])
