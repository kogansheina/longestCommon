# longestCommon
find the longest common substring between 2 given strings

This program find all the common strings between two given strings. 
It gives the longest common string.

The algorithm receives: T M N <s1> <s2> [M N <s1> <s2> ..... M N <s1> <s2>]
T = number of tests (maximum 200)
groups of 4 parameters for every test:
M = maximum length of s1 (maximum 100)
N = maximum length of s2 (maximum 100)
s1, s2 the strings to be compared

The program, also, receives - as the first parameter - 'null' or the name of a log file.
For 'null' option, all the printings are done to console.
It uses an environment variable : DOTNETDEBUG which is the debuging level: 0 - 3

gentests.py - generates random test sequences into a file (received as parameter). 

NOTE: 
====
due to the restrictions of command line in windows, it generates up to 50 tests,
the maximum string length is 50 and mqximum number of common strings is 5.

To avoid to have common strings besixes of the generated ones, the first string has ramdom
upper case letters, the second has random lower case lwetters and common strings have random digits.

The file contains T tests and the expected common strings with their positions for every test.

genall.py - receives a range of numbers and generates (using gentests.py), under 'logs' folder,
test files : tt<n>.txt

tests.py - read the test file and the correspondent log file and compare the expected to the received.
It receives the test file name and the log file name.

testall.py - receives a range of numbers and test (using tests.py), under 'logs' folder,
test files : tt<n>.txt, logs file : log_tt<n>.txt
