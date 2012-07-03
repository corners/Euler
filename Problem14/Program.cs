using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Euler14
{
	class Program
	{
		static void Main(string[] args)
		{
			const ulong max = 1000000;
			var sw = Stopwatch.StartNew();
			var result = PCalculate(max);
			sw.Stop();

			// Now repeat the sequence outputting the values
			Console.WriteLine("Longest sequence between {0} and 1", max);
			// Output the sequeunce by regenerating it
			Console.WriteLine("Sequence = ");
			step(result.Value, n => Console.Write("{0}, ", n));
			Console.WriteLine();
			Console.WriteLine("Time taken = {0} ms", sw.ElapsedMilliseconds);
			Console.WriteLine("Value = {0}", result.Value);
			Console.WriteLine("Longest = {0}", result.Terms);
			Console.ReadLine();
		}

		struct Result
		{
			public ulong Value;
			public ulong Terms;
		}

		static IEnumerable<ulong> Range(ulong start, ulong length)
		{
			for (ulong i = 0; i < length; i++)
				yield return start + i;
		}

		static Result PCalculate(ulong max)
		{
			// Find the largest sequence
			ulong halfMax = max / 2;
			if (halfMax % 2 == 0)
				halfMax -= 1;

			var result = new Result { Value = 1, Terms = 1 };

			var x = (from i in Range(halfMax, max - halfMax).AsParallel()
					 select new Result { Value = i, Terms = step(i, null) }
					 ).Aggregate(result, (a, r) => r.Terms > a.Terms ? r : a);

			return x;
		}

		static Result Calculate(ulong max)
		{
			// Find the largest sequence
			ulong largestStart = 1;
			ulong largestCount = 1;
			ulong halfMax = max / 2;

			// ignore even numbers because they will be 2^n times an odd number
			for (ulong i = max; i >= halfMax; i--)
			{
				// Ignore even numbers
				if (i % 2 == 0)
					continue;

				var n = i;
				var seq = new List<int>();
				var count = step(i, null);

				// Add on even parts
				while ((n * 2) < max)
				{
					n *= 2;
					count += 1;
				}
				if (count > largestCount)
				{
					largestStart = n;
					largestCount = count;
				}
			}

			return new Result { Value = largestStart, Terms = largestCount };
		}

		static ulong step(ulong n, Action<ulong> writer)
		{
			ulong steps = 1;
			while (n != 1)
			{
				steps++;
				if (writer != null)
					writer(n);
				n = (n % 2 == 0) ? n / 2 : (3 * n) + 1;
			}
			if (writer != null)
				writer(n);
			return steps;
		}
	}
}
