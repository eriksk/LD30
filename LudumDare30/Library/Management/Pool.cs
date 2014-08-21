using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Management
{
    public sealed class Pool<T> where T : new()
    {
        private int capacity;
        private T[] items;
        private int count;

        public Pool(int capacity)
        {
            this.capacity = capacity;

            Initialize();
        }

        public int Count { get { return count; } }

        public T this[int index] 
        {
            get { return items[index]; }
        }

        public void Clear()
        {
            count = 0;
        }

        private void Initialize()
        {
            items = new T[capacity];
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = new T();
            }
            count = 0;
        }

        public T Pop() 
        {
            if (count >= capacity)
                throw new Exception("Pool is empty");

            return items[count++];
        }

        public void Push(int index) 
        {
            if (index < 0 || index > count - 1)
                throw new ArgumentOutOfRangeException("index");

            T temp = items[count - 1];
            items[count - 1] = items[index];
            items[index] = temp;
            count--;
        }
    }
}
