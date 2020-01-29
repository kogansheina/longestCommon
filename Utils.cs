
using System;
using System.IO;

namespace longest2
{
	public class Utils
	{
		static public class Constants
		{
			public const int Tmax = 200; // maximum tests
			public const int Smax = 100; // maximum string lengtbh
		}
		public class logger
		{
			public StreamWriter wr = null;
			public logger() { }
			public logger(StreamWriter w) { wr = w; }
			public void Log(string logMessage)
			{
				if (wr == null)
				{
					Console.Write($"[{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}]");
					Console.WriteLine($"=>{logMessage}");
				}
				else
				{
					wr.Write($"[{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}]");
					wr.WriteLine($"=>{logMessage}");
				}
			}
			public void close()
			{
				if (wr != null)
					wr.Close();
			}
		}
		public class structResult
		{
			public int indPivot;  // index of the match in the pivot string
			public int indTest;   // index to search from
			public char match;    // matched char
			public int index;     // the index into indexes list

			public structResult()
			{
				indPivot = -1;
				indTest = -1;
				match = '?';
				index = 0;
			}
			public structResult(int p, int t, char m, int i)
			{
				indPivot = p;
				indTest = t;
				match = m;
				index = i;
			}
			public void printResult(string prefix, logger log)
			{
				string strData = string.Format("{3} result: {4} => indPivot={0} indTest={1} match={2}",
					indPivot, indTest, match, prefix, index);
				log.Log(strData);
			}
			public bool cmpObj(structResult a)
			{
				if ((indPivot == a.indPivot) && (indTest == a.indTest) && (match == a.match))
					return true;
				return false;
			}
		}
		public class finalResult
		{
			public int indPivot;  // index of the match in the pivot string
			public int indTest;   // index of the match in the test string
			public string match;  // common substring
			
			public finalResult()
			{
				indPivot = -1;
				indTest = -1;
				match = "";
			}
			public finalResult(int p, int t, string m)
			{
				indPivot = p;
				indTest = t;
				match = m;
			}
			public void printResult(logger log)
			{
				string strData = string.Format("indPivot={0} indTest={1} match={2}", indPivot, indTest, match);
				log.Log(strData);
			}
			public void printResult(string prefix,logger log)
			{
				string strData = string.Format("{3} => indPivot={0} indTest={1} match={2}", indPivot, indTest, match, prefix);
				log.Log(strData);
			}
		}
		public class structWork
		{
			public string pivot;  
			public string test;
			
			public structWork(string X, string Y)
			{
				pivot = X;
				test = Y;
				// take as pivot the shortest string
				if (pivot.Length > test.Length)
				{
					string s = X;
					pivot = Y;
					test = s;
				}
			}
			public void printResult(string prefix, logger log)
			{
				string strData = string.Format("{2} work: pivot={0} test={1}", pivot, test, prefix);
				log.Log(strData);
			}
		}
		public class check
		{
			public check() { }
			public bool checkLimit(int limit)
			{
				if (limit < 1 || limit > Constants.Smax)
					return false;
				return true;
			}
			// return string length to its actual size 
			public int checkLength(int len, string s, logger log)
			{
				int l = s.Length;
				if (len > l)
				{
					len = l;
					/*
					string strData = string.Format("=>Length of '{0}' is less than string length - will be considered string length = {1}", s,len);
					log.Log(strData);
					*/
				}
				/*
				else if (len < l)
				{
					string strData = string.Format("=>Length of '{0}' is greater than string length - will be considered input parameter = {1}",s,len);
					log.Log(strData);
				}
				*/
				return len;
			}
			// return string to its actual size 
			public string checkString(int len, string s)
			{
				return s.Substring(0, len);
			}
		}
	}
}
