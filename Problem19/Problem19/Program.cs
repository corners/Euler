using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Problem19
{
	class Program
	{
		static void Main(string[] args)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			object result = Problem19();
			sw.Stop();
			Console.WriteLine("Answer is {0}. Calculated in {1} ms", result.ToString(), sw.ElapsedMilliseconds);
			Console.ReadKey();
		}

        // 364/7 = 52 
        // Therefore each year starts 1 day later than the last. e.g. Fri 1/1/2010 and Sat 1/1/2011
        // Except leap years which start 2 days later

        // so the sequence is;
        // Mon 1/1/1900, Tue 1/1/1901, Wed 1/1/1902, Thu 1/1/1903, Fri 1/1/1904*, Sun 1/1/1905, Mon 1/1/1906, Tue 1/1/1907, Wed 1/1/1908*, Fri 1/1/1909 

        // So start day for nth year is: 
        // y_0 = 1900
        // l = number of leap years between y_0 and y_(n-1)
        // (y_n-y_0)=l

		/*
		pattern of 4 years
		1901, 1902, 1903, 1904 upto 1997,98,99,00 (4th year always a leap year
		3*365 + 366 = 1461 (not divisible by 7 
		lengths of months
		31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31
		31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31
		31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31
		31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31
		*/
		static object Problem19()
		{
			object result = "";



			return result;
		}

	}
}
