using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Collections
{
    public class Range : IEnumerable<int>
    {
        private readonly int[] numbers;

        public Range(int start, int end)
        {
            numbers = new int[(end - start) + 1];
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = start + i;
            }
        }

        public int Length
        {
            get { return numbers.Length; }
        }

        public int[] ToArray()
        {
            var copy = new int[numbers.Length];

            for (var i = 0; i < numbers.Length; i++)
                copy[i] = numbers[i];

            return copy;
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                yield return numbers[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
