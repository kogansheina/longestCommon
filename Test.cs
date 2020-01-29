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
	public class Test
	{
		public class OneTest
		{
			int ind;
			List<string> args;
			string X, Y;
			string strData = "";
			Utils.check checker = new Utils.check();
			int level = 0;
			Utils.logger mylog;
			int M, N;
			
			public OneTest(int i, List<string> a, Utils.logger log)
			{
				ind = i;
				args = a;
				mylog = log;				
			}
			public bool check()
			{
				// take environment variables : debug level and log file name
				Int32.TryParse(Environment.GetEnvironmentVariable("DOTNETDEBUG",EnvironmentVariableTarget.User), out level);
				if (level >= 3)
				{
					strData = string.Format("Enter: {0}", System.Reflection.MethodBase.GetCurrentMethod().Name);
					mylog.Log(strData);
					return false;
				}
				strData = string.Format("TEST={4} parameters : {0} {1} {2} {3}", 
								args[0], args[1], args[2], args[3], ind + 1);
				mylog.Log(strData);
				// check and set properly the test parameters
				if (!Int32.TryParse(args[0], out N) || !Int32.TryParse(args[1], out M))
				{
					strData = string.Format("String length is incorrect");
					mylog.Log(strData);
					return false;
				}
	
				return true;
			}
			// look for a char match
			// input : pivot string and the index in the string to start searching
			//         test string and the index in the string to start searching
			// returns: if match exists - indexes in both strings of the match and the matched char
			//          oterwise: -1, -1, '?'
			
			Utils.structResult checkChar(string pivot, int startPivot, string totest, int startTest)
			{			 
				if (level >= 3)
				{
					strData = string.Format("Enter: {0}", System.Reflection.MethodBase.GetCurrentMethod().Name);
					mylog.Log(strData);
					if (level <= 2)
					{
						strData = string.Format("startPivot={0} , pivot={1} , startTest={2} , totest={3}",
											startPivot, pivot, startTest, totest);
						mylog.Log(strData);
					}
				}
				Utils.structResult result = new Utils.structResult();
				// substring in test to be searched
				string h = totest.Substring(startTest, totest.Length - startTest);
				// check for the first checking char from pivot
				int j = h.IndexOf(pivot[startPivot]);
				if (level >= 2)
				{
					strData = string.Format("look for {0} in {1} => {2}", pivot[startPivot], h, j);
					mylog.Log(strData);
				}
				if (j >= 0) // found a match
				{
					// return result steructure
					result.indPivot = startPivot;
					result.indTest = j + startTest;
					result.match = pivot[startPivot];
				}
				if (level >= 3)
				{
					if (level <= 2)
					{
						strData = string.Format("Result checkChar: indPivot={0} , indTest={1} , match={2}", 
											result.indPivot, result.indTest, result.match);
						mylog.Log(strData);
					}
					strData = string.Format("Exit: {0}", System.Reflection.MethodBase.GetCurrentMethod().Name);
					mylog.Log(strData);				
				}
		
				return result;
			}			
		
			List<Utils.structResult> updateIndex(Utils.structWork work, int k, List<Utils.structResult> indexes)
			{
				List<Utils.structResult> newList = indexes;
				int j = 0;						
				// for each char in pivot, look for a match in test string
				while (j < work.test.Length)
				{
					Utils.structResult trial = checkChar(work.pivot, k, work.test, j);
					if (trial.indTest != -1)
					{
						// found a match, store it into indexes
						// eliminate duplicates
						bool exist = false;
						foreach (Utils.structResult y in newList)
						{
							if (y.cmpObj(trial))
							{
								exist = true;
								break;
							}
						}
						if (!exist)
						{
							trial.index = newList.Count;
							newList.Add(trial);
						}
						j = trial.indTest + 1;
					}
					else										
						break;					
				}
				return newList;
			}
			public bool findCommon()
			{
				X = args[2];
				Y = args[3];
				X = checker.checkString(checker.checkLength(N, X, mylog), X);
				Y = checker.checkString(checker.checkLength(M, Y, mylog), Y);
												
				strData = string.Format("TEST={4} actual     : {0} {1} {2} {3}\n", N, M, X, Y, ind + 1);
				mylog.Log(strData);
				// Environment.Exit(0);
				
				// look for the first char matched and added it to result
				// indexes is a list of structures for all matched chars
				List<Utils.structResult> indexes = new List<Utils.structResult>();
				// work is the working class - pivot string (the shortest) an test string
				Utils.structWork work = new Utils.structWork(X, Y);				
				
				// loop on shortest string and build indexes
				for (int k = 0; k < work.pivot.Length; k++) 
				{
					indexes = updateIndex(work, k, indexes);
				}  // for loop
				if (level >= 1)
				{
					mylog.Log("indexes");
					foreach (Utils.structResult y in indexes)
					{
						y.printResult("",mylog);
					}
				}
				// results is a list of results : matched substring and its position in both strings
				List<Utils.finalResult> results = lookForStrings(indexes);
				// no common string
				if (results.Count == 0)
				{
					strData = string.Format("The longest ({0} chars) common substring are:", 0);
					mylog.Log(strData);
					return false;
				}
				// check which string is the longest
				int maxlength = 0;
				List<Utils.finalResult> max = new List<Utils.finalResult>();			
				foreach (Utils.finalResult p in results)
				{
					if (level >= 1)
						p.printResult("final",mylog);
					if (p.match.Length >= maxlength)
					{
						maxlength = p.match.Length;
						if (maxlength > 0)
							max.Add(p);
					}
				}
				strData = string.Format("The longest ({0} chars) common substring are:", maxlength);
				mylog.Log(strData);
			
				// print all the strings with the length equal to the longest one
				foreach (Utils.finalResult p in max)
				{
					if (p.match.Length == maxlength)
						p.printResult(mylog);
				}
				if (level >= 3)
				{
					strData = string.Format("Exit: {0}", System.Reflection.MethodBase.GetCurrentMethod().Name);
					mylog.Log(strData);
				}
				return true;			
			}

			List<Utils.finalResult> lookForStrings(List<Utils.structResult> indexes)
			{
				// loop in indexes list and search for consecutiveness
				List<char> temp = new List<char>();
				List<Utils.finalResult> results = new List<Utils.finalResult>();
				int index = 0;
				while (index < indexes.Count)
				{
					// start is the beginning point
					Utils.structResult start = new Utils.structResult(indexes[index].indPivot, indexes[index].indTest, indexes[index].match, indexes[index].index);
					// work1, begins from start
					Utils.structResult work1 = new Utils.structResult(start.indPivot, start.indTest, start.match, start.index);
					// work2, the structure to be compared
					Utils.structResult work2 = new Utils.structResult();
					temp.Add(work1.match);
					// look for an work2 to be consecutive to work1
					bool found = false;
					while (true)
					{
						bool find = false;
						// begin searching with the next after work1
						for (int y = work1.index + 1; y < indexes.Count; y++)
						{
							if ((indexes[y].indPivot == work1.indPivot + 1) && (indexes[y].indTest == work1.indTest + 1))
							{
								work2 = new Utils.structResult(indexes[y].indPivot, indexes[y].indTest,
																	indexes[y].match, indexes[y].index);
								find = true;						
								break;
							}
						}
						if (find) // found one
						{
							temp.Add(work2.match);
							// continue searching 
							// make work1 the just found consecutive, work2 will be searched from this point
							work1 = work2;
							found = true;
						}
						else 
						{
							if (work2.indPivot >= 0)
							{
								// not found any consecutive, continue from the index of work2
								index = work2.index;
							}
							break; // break the while finding work2
						}
					} // while true
					if (found)
					{
						// found some consecutives common chars, store the string
						results.Add(new Utils.finalResult(start.indPivot, start.indTest, string.Join("", temp)));										
					}
					temp.Clear();
					index++; // continue with the next match char
					// take a new start
				} // the big while
				return results;
			}
	
		}  // OneTest
	}  // Test
} // namespace
