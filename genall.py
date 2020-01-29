#!/usr/bin/env python
from __future__ import print_function
from rpython import print
import os
import sys
from gentests import background

def main(argv):
	n = int(argv[0])
	m = int(argv[1])
	for i in range(n,m+1,1):
		f = "logs/tt"+str(i)+".txt"
		background(f)	
		print("Generated "+f)

if __name__ == "__main__":
	if len(sys.argv) < 3:
		print("Arguments needed - range of tests")
		sys.exit(1)
	main(sys.argv[1:])
