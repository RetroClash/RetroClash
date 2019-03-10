using System;
using System.Collections.Generic;

namespace RetroClash.Core.Network
{
    public class Pool<T>
    {
        public Pool(int capacity)
        {
            Stack = new Stack<T>(capacity);
        }

        public Stack<T> Stack { get; set; }

        public int Count => Stack.Count;

        public void Push(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            lock (Stack)
            {
                Stack.Push(item);
            }
        }

        public T Pop()
        {
            lock (Stack)
            {
                return Stack.Pop();
            }
        }
    }
}