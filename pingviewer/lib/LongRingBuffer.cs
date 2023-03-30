using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pingviewer.lib
{
    internal class LongRingBuffer
    {
        private long[] buffer; // readable, (re-)writable, but not erasable
        private readonly int length;
        private int count; // how many times enqueued
        private int ptr; // pointer of next enqueue position

        public int Count { get; }
        public long[] Buffer { get; }
        public int Length { get; }
        public int Ptr { get; }

        public LongRingBuffer(int length)
        {
            this.length = length;
            buffer = new long[length];
            count = 0;
            ptr = 0;
        }

        public void enqueue(long value)
        {
            buffer[ptr++] = value;
            count++;
            // if pointer exceeds end of array, then rezero pointer value
            if (ptr >= length)
            {
                ptr = 0;
            }
        }

        public long[] getMinifiedArray()
        {
            if (count < length)
            {
                long[] tmp = new long[count];
                Array.Copy(buffer, tmp, count);
                return tmp;
            }
            return buffer;
        }
    }
}
