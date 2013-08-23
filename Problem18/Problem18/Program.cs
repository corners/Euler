using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Problem18
{
    [DebuggerDisplay("{Value}")]
    class Node
    {
        public Node(int value)
        {
            Value = value;
        }

        public int Row;
        public int Column;
        public int Value;

        // nodes above
        public Node Left;
        public Node Right;

        public Node[] GetParents()
        {
            return new Node[] { Left, Right }.Where(n => n != null).ToArray();
        }
    }

    class NodeSum
    {
        public NodeSum(Node node, int sum)
        {
            Node = node;
            Sum = sum;
        }

        public int Sum;
        public Node Node;
    }


    class Program
    {
        static void Main(string[] args)
        {

            string input = File.ReadAllText(@"..\..\..\..\Problem67\triangle.txt");

            var sw = new Stopwatch();
            sw.Start();
            int max = Calculate(input);
            sw.Stop();
            
            Console.WriteLine("Largest path sum = {0} in {1} ms", max, sw.ElapsedMilliseconds);
            Console.ReadLine();
        }

        static int Calculate(string triangle)
        {
            var elements = ToArray(triangle);

            var nodes = BuildTree(elements);

            var sums = Sum(nodes);

            // The biggest value
            return sums.Last().OrderByDescending(ns => ns.Sum).First().Sum;
        }

        private static NodeSum[][] Sum(Node[][] nodes)
        {
            NodeSum[][] sums = new NodeSum[nodes.Length][];
            NodeSum[] previous = new NodeSum[] {};
            for (int rowNum = 0; rowNum < nodes.Length; rowNum++)
            {
                Node[] row = nodes[rowNum];

                NodeSum[] current = new NodeSum[row.Length];
                sums[rowNum] = current;
                for (int i = 0; i < row.Length; i++)
                {
                    var parents = GetParents(rowNum, i, previous).Where(n => n != null);
                    var maxPath = 0;
                    if (parents.Count() > 0)
                        maxPath = parents.Select(n => n.Sum).Max();

                    current[i] = new NodeSum(row[i], row[i].Value + maxPath);
                }
                previous = current;
            }
            return sums;
        }

        private static Node[][] BuildTree(int[] elements)
        {
            var rows = Segments(elements.Length).Select(p => elements.Skip(p.Item1).Take(p.Item2).ToArray()).ToArray();

            Node[][] nodes = new Node[rows.Length][];
            Node[] previous = null;
            for (int rowNum = 0; rowNum < rows.Length; rowNum++)
            {
                int[] row = rows[rowNum];

                Node[] current = new Node[row.Length];
                nodes[rowNum] = current;
                for (int i = 0; i < row.Length; i++)
                {
                    var node = new Node(row[i]) { Row = rowNum, Column = i };
                    var parents = GetParents<Node>(rowNum, i, previous);
                    node.Left = parents[0];
                    node.Right = parents[1];
                    current[i] = node;
                }
                previous = current;
            }
            return nodes;
        }

        static T[] GetParents<T>(int rowNum, int rowIndex, T[] previousRow) where T : class
        {
            T left = null;
            T right = null;
            var parents = ImmediateParents(rowNum, rowIndex);
            if (parents.Item1.HasValue)
                left = previousRow[parents.Item1.Value];
            if (parents.Item2.HasValue)
                right = previousRow[parents.Item2.Value];
            return new T[] { left, right };
        }

        // return the index of the two elements above the specified index. Will be null if there is no element. Assumes the triangle is broken into rows with the first element being the top (smallest).
        static Tuple<int?, int?> ImmediateParents(int row, int rowIndex)
        {
            int? left;
            int? right;
            if (row == 0)
            {
                left = null;
                right = null;
            }
            else
            {
                left = rowIndex - 1;
                if (left < 0) left = null;

                right = rowIndex;
                int parentLenght = row;
                if (right >= parentLenght) right = null;
            }
            return Tuple.Create(left, right);
        }

        // return the start index and length of each row of the triangle in the array
        static IEnumerable<Tuple<int, int>> Segments(int n)
        {
            int i = 0;
            int length = 1;
            while (i < n)
            {
                yield return Tuple.Create(i, length);

                i += length;
                length++;
            }
            if (i > n)
                throw new ArgumentException("n must be a factorial");
        }

        static int[] ToArray(string triangle)
        {
            return triangle.Split(new[] {' ', '\r', '\n' }, StringSplitOptions.None)
                           .Where(s => !string.IsNullOrWhiteSpace(s))
                           .Select(s => Convert.ToInt32(s))
                           .ToArray();
        }

        const string triangle =
@"
   3
  7 4
 2 4 6
8 5 9 3";

        const string triangle2 =
@"
75
95 64
17 47 82
18 35 87 10
20 04 82 47 65
19 01 23 75 03 34
88 02 77 73 07 63 67
99 65 04 28 06 16 70 92
41 41 26 56 83 40 80 70 33
41 48 72 33 47 32 37 16 94 29
53 71 44 65 25 43 91 52 97 51 14
70 11 33 28 77 73 17 78 39 68 17 57
91 71 52 38 17 14 91 43 58 50 27 29 48
63 66 04 68 89 53 67 30 73 16 69 87 40 31
04 62 98 27 23 09 70 98 73 93 38 53 60 04 23
";


    }
}
