using System;
using System.Collections.Generic;

namespace Utils.Collections
{
    public class RingList<E> : IEnumerable<E> 
    {
        private readonly E[] items;
        private int head;
        private int tail;
        private int count;
       
            public RingList(int capacity) {
            this.items = new E[capacity];
            }
   
            public int capacity() { return items.Length; }
       
            private int inc(int i) {
            return (++i == items.Length) ? 0 : i;
        }
   
            private int dec(int i) {
            return (--i == -1) ? (items.Length-1) : i;
        }
   
            private void addTail(E x) {
            items[tail] = x;
            tail = inc(tail);
            ++count;
        }

            private void addHead(E x) {
            head = dec(head);
            items[head] = x;
            ++count;
        }

            private E removeTail() {
            E[] items = this.items;
            tail = dec(tail);
            E x = items[tail];
            items[tail] = default(E);
            --count;
            return x;
        }
   
            private E removeHead() {
            E[] items = this.items;
            E x = items[head];
            items[head] = default(E);
            head = inc(head);
            --count;
            return x;
        }
   
        private E removeAt(int i) {
            E[] items = this.items;
            // if removing front item, just advance
            if (i == head) {
                return removeHead();
            } else if (i == dec(tail)) {
                return removeTail();
            } else {
                E e = default(E);
                // slide over all others up through putIndex.
                for (;;) {
                    int next = inc(i);
                    if (next != tail) {
                        items[i] = items[next];
                        i = next;
                    } else {
                        e = items[i];
                     items[i] = default(E);
                        tail = i;
                        break;
                    }
                }
                --count;
                return e;
            }
        }

        private int indexToPosition(int i) {
             //int n = (head + i) % items.length;
             int n = head + i;
             n = n < items.Length ? n : (n-items.Length);
             return n;
        }

        public E getHead()
        {
            if(count==0)
                return default(E);
            return items[head];
        }

        public E getTail()
        {
            if(count==0)
                return default(E);
            int n = indexToPosition(count-1);
            return items[n];
        }
   
        public E get(int i) {
             if(i<0 || i>=count)
                     throw new IndexOutOfRangeException();
             int n = indexToPosition(i);
             return items[n];
        }
   
        public E set(int i, E e) {
             if(i<0 || i>=count)
                     throw new IndexOutOfRangeException();
             E[] items = this.items;
             int n = indexToPosition(i);
             E x = items[n];
             items[n] = e;
             return x;
        }
   
        public E addHeadAndRoll(E e) {
             E removed = default(E);
            if(count > 0 && count == items.Length)
                     removed = removeTail();
             addHead(e);
            return removed;
        }

        public E addTailAndRoll(E e) {
             E removed = default(E);
             if(count > 0 && count == items.Length)
                     removed = removeHead();
             addTail(e);
            return removed;
        }
  
        public E remove(int i) {
             if(i<0 || i>=count)
                     throw new IndexOutOfRangeException();
             int n = indexToPosition(i);
             return removeAt(n);
        }
   
        public bool remove(E o) {
            E[] items = this.items;
            int i = head;
            int k = 0;
            for (;;) {
                if (k++ >= count)
                    return false;
                if (object.Equals(o, items[i])) {
                    removeAt(i);
                    return true;
                }
                i = inc(i);
            }
        }

            public int indexOf(E o) {
            E[] items = this.items;
                int i = head;
                int k = 0;
                while (k < count) {
                    if (object.Equals(o, items[i]))
                        return k;
                    i = inc(i);
                    k++;
                }
            return -1;
            }

        public bool contains(E o) {
            return indexOf(o) >= 0;
        }

        public int size() {
             return count;
        }

        public void clear() {
            E[] items = this.items;
            int i = head;
            int k = count;
            while (k-- > 0) {
                items[i] = default(E);
                i = inc(i);
            }
            count = 0;
            tail = 0;
            head = 0;
        }

            public E[] toArray() {
            E[] items = this.items;
            E[] a = new E[count];
            int k = 0;
            int i = head;
            while (k < count) {
                a[k++] = items[i];
                i = inc(i);
            }
            return a;
            }

        public IEnumerator<E> GetEnumerator()
        {
            E[] items = this.items;
            int i = head;
            int k = count;
            while (k-- > 0)
            {
                yield return items[i];
                i = inc(i);
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            E[] items = this.items;
            int i = head;
            int k = count;
            while (k-- > 0)
            {
                yield return items[i];
                i = inc(i);
            }
        }
    }
}
