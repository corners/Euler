using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem18
{
    class Node
    {
        public Node()
        {
            
        }
        
        public readonly int Value;
        public readonly Node Left;
        public readonly Node Right;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var triangle = 
@"
   3
  7 4
 2 4 6
8 5 9 3";

            var elements = ToArray(triangle);
            var rows = CountRows(elements.Length);

            // todo build into tree

            // sum

            // find shortest path

        }

        /// <summary>
        /// Count the number of rows based on the number of elements
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        static int CountRows(int length)
        {
            int rows = 0;
            int next = 1;
            while (length > 0)
            {
                rows++;
                length -= next;
                next++;
            }
            if (length != 0)
                throw new ArgumentException("Invalid number of elements");
            return rows;
        }

        static int[] ToArray(string triangle)
        {
            return triangle.Split(new[] {' ', '\r', '\n' }, StringSplitOptions.None)
                           .Where(s => !string.IsNullOrWhiteSpace(s))
                           .Select(s => Convert.ToInt32(s))
                           .ToArray();
        }
    }
}
