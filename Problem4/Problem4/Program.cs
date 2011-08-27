using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Problem4
{
	class Program
	{
		static void Main(string[] args)
		{
			// largest product is 998,001 (999^2) 
			// smallest is 100000 (100*100)
			// largest would likely start with 997,xxx as it's the first number that allows a pallindrome therefore largest is 997,799
			// can calculate the rest by subtracting 1 from the left half. i.e. 996, 995, 994, ... => 996699, 995599, 994499, ...
			// odd number so both factors must be odd.
			// how to determine 2 factors for a number..?

			// smallest product is 101, largest 999. only odd numbers required
			// so if potential number did product >= 100 && <= 999 then success

			int part = 997;
			bool found = false;
			int pallindrome = 0;
			int a = 0;
			int b = 0;

			Stopwatch sw = new Stopwatch();
			sw.Start();

			while (part > 99 && !found)
			{
				pallindrome = (part * 1000) + ReverseIntDw(part);
				pallindrome = (part * 1000) + ReverseInt(part);

				a = 101;
				b = 0;
				while (a < 1000 && !found)
				{
					int remainder;
					b = Math.DivRem(pallindrome, a, out remainder);
					if (remainder == 0 && a > 99 && a < 1000 && b > 99 && b < 1000)
						found = true;
					else
						a += 2;
				}

				if (!found)
					part--;
			}

			sw.Stop();

			if (found)
				Console.WriteLine("Pallindrome {0}. Products {1}, {2}. In {3}milliseconds.", pallindrome, a, b, sw.ElapsedMilliseconds);
			else
				Console.WriteLine("Not found");

			Console.WriteLine("Press any key to continue");
			Console.ReadKey();
		}

		static int ReverseInt(int num)
		{
//			int num = 123;
			int tens = 0;
			int units = 0;
			int hundreds = 0;
			if (num % 10 > 0)
			{
				units = (num % 10);
				tens = num % 100;
				hundreds = num - tens;
				tens = tens - units;
			}
			return (units * 100) + (tens) + (hundreds / 100);
		}

		static int ReverseIntDw(int value)
		{
			int z = value % 10;
			int y10 = (value % 100) - z;
			int x = (value - (y10 + z)) / 100;
			return (z * 100) + y10 + x;
		}
	}
}
