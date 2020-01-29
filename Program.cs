using System;
using System.Collections.Generic; // for List
using System.IO;
/*
 * find the longest common substring in 2 given strings
 * parameters: T - number of tests
 * length of strings
 * strings
 * environment variables: DOTNETLOG   - log file, if not given the printings are done to console
 */ 
namespace longest2 
{
	class Program
	{

		/****************************************************************
								  main          
		 ****************************************************************/
		static void Main(string[] args)
		{
			int T;
			string strData = "";
			string logfile = "";
			Utils.logger mylog;
			StreamWriter wr = null;
			
			logfile = args[0];
			if (logfile != "null")
				wr = new StreamWriter(logfile); 			
			else
				wr = null;		
			// create logger
			mylog = new Utils.logger(wr);
			// check parameters
			if (args.Length == 0)
			{
				strData = string.Format("parameters: logfile (null or name)tests len1 len2 str1 str2[len1 len2 str1 str2]");
				mylog.Log(strData);
				return;
			}
			
			if ((args.Length - 2) % 4 != 0)
			{
				strData = string.Format("number of arguments of cases, must be a multiple of 4");
				mylog.Log(strData);
				mylog.close();
				return;
			}
			if (!Int32.TryParse(args[1], out T))
			{
				strData = string.Format("tests number is wrong");
				mylog.Log(strData);
				mylog.close();
				return;
			}
			if ((T < 1) || (T > Utils.Constants.Tmax))
			{
				strData = string.Format("number of test cases must be in range 1-{0}", Utils.Constants.Tmax);
				mylog.Log(strData);
				mylog.close();
				return;
			}

			Test.OneTest test; 
			// execute all tests
			for (int i = 0; i < T; i++)
			{
				List<string> arg = new List<string>(){args[i*4+2],args[i*4+3],args[i*4+4],args[i*4+5]};
				test = new Test.OneTest(i, arg, mylog);	
				if (test.check())
				{
					if (!test.findCommon())
						strData = string.Format("Test: {0} failed\n", i + 1);
					else
						strData = string.Format("Test: {0} passed\n", i + 1);
					mylog.Log(strData);
				}
			}
			mylog.close();
			
		} //main
	}
}
