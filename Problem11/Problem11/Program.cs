using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Drawing;

namespace Problem11
{
	enum Direction
	{
		Left,
		Down,
		DiagLeft,
		DiagRight,
	}

	class Program
	{
		static void Main(string[] args)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			object result = Problem11();
			sw.Stop();
			Console.WriteLine("Answer is {0}. Calculated in {1} ms", result.ToString(), sw.ElapsedMilliseconds);
			Console.ReadKey();
		}

		static int[,] matrix = new[,]
			{//	   0   1   2   3   4   5   6   7   8   9  10  11  12  13  14  15  16  17  18  19
				{  8,  2, 22, 97, 38, 15,  0, 40,  0, 75,  4,  5,  7, 78, 52, 12, 50, 77, 91,  8 }, // 0
				{ 49, 49, 99, 40, 17, 81, 18, 57, 60, 87, 17, 40, 98, 43, 69, 48,  4, 56, 62,  0 }, // 1
				{ 81, 49, 31, 73, 55, 79, 14, 29, 93, 71, 40, 67, 53, 88, 30,  3, 49, 13, 36, 65 }, // 2
				{ 52, 70, 95, 23,  4, 60, 11, 42, 69, 24, 68, 56,  1, 32, 56, 71, 37,  2, 36, 91 }, // 3
				{ 22, 31, 16, 71, 51, 67, 63, 89, 41, 92, 36, 54, 22, 40, 40, 28, 66, 33, 13, 80 }, // 4
				{ 24, 47, 32, 60, 99,  3, 45,  2, 44, 75, 33, 53, 78, 36, 84, 20, 35, 17, 12, 50 }, // 5
				{ 32, 98, 81, 28, 64, 23, 67, 10, 26, 38, 40, 67, 59, 54, 70, 66, 18, 38, 64, 70 }, // 6
				{ 67, 26, 20, 68,  2, 62, 12, 20, 95, 63, 94, 39, 63,  8, 40, 91, 66, 49, 94, 21 }, // 7
				{ 24, 55, 58,  5, 66, 73, 99, 26, 97, 17, 78, 78, 96, 83, 14, 88, 34, 89, 63, 72 }, // 8
				{ 21, 36, 23,  9, 75,  0, 76, 44, 20, 45, 35, 14,  0, 61, 33, 97, 34, 31, 33, 95 }, // 9
				{ 78, 17, 53, 28, 22, 75, 31, 67, 15, 94,  3, 80,  4, 62, 16, 14,  9, 53, 56, 92 }, // 10
				{ 16, 39,  5, 42, 96, 35, 31, 47, 55, 58, 88, 24,  0, 17, 54, 24, 36, 29, 85, 57 }, // 11
				{ 86, 56,  0, 48, 35, 71, 89,  7,  5, 44, 44, 37, 44, 60, 21, 58, 51, 54, 17, 58 }, // 12
				{ 19, 80, 81, 68,  5, 94, 47, 69, 28, 73, 92, 13, 86, 52, 17, 77,  4, 89, 55, 40 }, // 13
				{  4, 52,  8, 83, 97, 35, 99, 16,  7, 97, 57, 32, 16, 26, 26, 79, 33, 27, 98, 66 }, // 14
				{ 88, 36, 68, 87, 57, 62, 20, 72,  3, 46, 33, 67, 46, 55, 12, 32, 63, 93, 53, 69 }, // 15
				{  4, 42, 16, 73, 38, 25, 39, 11, 24, 94, 72, 18,  8, 46, 29, 32, 40, 62, 76, 36 }, // 16
				{ 20, 69, 36, 41, 72, 30, 23, 88, 34, 62, 99, 69, 82, 67, 59, 85, 74,  4, 36, 16 }, // 17
				{ 20, 73, 35, 29, 78, 31, 90,  1, 74, 31, 49, 71, 48, 86, 81, 16, 23, 57,  5, 54 }, // 18
				{  1, 70, 54, 71, 83, 51, 54, 69, 16, 92, 33, 48, 61, 43, 52,  1, 89, 19, 67, 48 }  // 19
			};

		class Result
		{
			public Result()
			{
				x = -1;
				y = -1;
				value = 0;
			}

			public int x;
			public int y;
			public int value;
			public int[] source;
			public Direction direction;
		}

		class Path
		{
			public Direction Direction;
			public Func<Point, Point> NextPointFn;
		}

		/// <summary>
		/// What is the greatest product of four adjacent numbers in any direction (up, down, left, right, or diagonally) in the 20x20 grid?
		/// </summary>
		/// <returns></returns>
		static object Problem11()
		{
			object result = "";

			int width = 4;

			var paths = new []
			{
				new Path() { Direction = Direction.Left, NextPointFn = NextPointLeft },
				new Path() { Direction = Direction.Down, NextPointFn = NextPointDown },
				new Path() { Direction = Direction.DiagLeft, NextPointFn = NextPointDiagDownLeft },
				new Path() { Direction = Direction.DiagRight, NextPointFn = NextPointDiagDownRight },
			};

			Result max = new Result();
			foreach (var path in paths)
			{
				foreach (Point point in Points(matrix))
				{
					int[] n = Elements(matrix, point, width, path.NextPointFn).ToArray();
					// Not all paths are valid
					if (n.Length == width)
					{
						int product = n.Aggregate((runningProduct, nextFactor) => runningProduct * nextFactor);
						if (product > max.value)
						{
							max.x = point.X;
							max.y = point.Y;
							max.value = product;
							max.source = n;
							max.direction = path.Direction;
						}
					}
				}
			}
			return string.Format("Point ({0},{1}) Direction {2} Product {3}, Values {4}", max.x, max.y ,max.direction, max.value, 
				string.Join(", ", max.source.Select(n => n.ToString()).ToArray()));
		}

		static IEnumerable<Point> Points(int[,] matrix)
		{
			for (int y = 0; y < matrix.GetUpperBound(0); y++)
			{
				for (int x = 0; x < matrix.GetUpperBound(1); x++)
				{
					yield return new Point(x, y);
				}
			}
		}

		static Point NextPointLeft(Point current)
		{
			return new Point(current.X - 1, current.Y);
		}

		static Point NextPointDown(Point current)
		{
			return new Point(current.X, current.Y + 1);
		}

		static Point NextPointDiagDownLeft(Point current)
		{
			return new Point(current.X - 1, current.Y + 1);
		}

		static Point NextPointDiagDownRight(Point current)
		{
			return new Point(current.X + 1, current.Y + 1);
		}

		static IEnumerable<Point> GetPoints(int[,] matrix, Point point, int length, Func<Point, Point> NextPointFn)
		{
			yield return point;

			for (int i = 1; i < length; i++)
			{
				point = NextPointFn(point);
				yield return point;
			}
		}

		static IEnumerable<int> Elements(int[,] matrix, Point point, int length, Func<Point, Point> NextPointFn)
		{
			return GetPoints(matrix, point, length, NextPointFn)
				.TakeWhile(pt => ValidPoint(matrix, pt))
				.Select(pt => matrix[pt.Y, pt.X]);
		}

		static bool ValidPoint(int[,] matrix, Point point)
		{
			return point.X >= matrix.GetLowerBound(1)
				&& point.X <= matrix.GetUpperBound(1)
				&& point.Y >= matrix.GetLowerBound(0)
				&& point.Y <= matrix.GetUpperBound(0);
		}

	}
}
