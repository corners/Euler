using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Project17
{
	class Program
	{
		static void Main(string[] args)
		{
			var sw = Stopwatch.StartNew();
			var result = Calculate();
			sw.Stop();

			Console.WriteLine("Letters = {0}", result.Count);
			Console.WriteLine("Duration = {0} ms", sw.ElapsedMilliseconds);
			Console.ReadKey();
		}

		struct Result
		{
			public int Count;
		}

		static Result Calculate()
		{
			string[] units = new [] {
				"one",
				"two",
				"three",
				"four",
				"five",
				"six",
				"seven",
				"eight",
				"nine",
			};

			string[] tens = new[] {
				"ten",
				"twenty",
				"thirty",
				"fourty",
				"fifty",
				"sixty",
				"seventy",
				"eighty",
				"ninety",
			};

			string[] exceptions = new[]
				{
					"eleven",
					"twelve",
					"thirteen",
					"fourteen",
					"fifteen",
					"sixteen",
					"seventeen",
					"eighteen",
					"nineteen",
				};
			return new Result { Count = 0 };
		}


	}
}
