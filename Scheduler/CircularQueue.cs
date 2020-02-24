using System;
namespace Scheduler
{
    public class CircularQueue<T>
    {

        T[] que;
        int head;       // remove from head
        int tail;       // insert at tail
        int currentPeek;

        public CircularQueue(int quesize)
        {
            head = tail = currentPeek = -1;
            que = new T[quesize];
        }

        public void enqueue(T elem)  // if next index to tail == head => Q is FULL
        {
            int newIndex = nextIndex(tail);
            if (newIndex == head)
                throw new BoundedQueueException(QueueStatus.Full);

            tail = newIndex;
            que[newIndex] = elem;
            if (head == -1)
                head = 0;
        }

        public T dequeue()  // After removing from head, if that was the only element in Q
                            // Mark Q to be empty by setting head and tail to -1
        {
            if (head == -1)
                throw new BoundedQueueException(QueueStatus.Empty);

            T elem = que[head];
            que[head] = default(T);

            if (head == tail)
            {
                head = tail = -1;
            }
            else
            {
                head = nextIndex(head);
            }

            return elem;
        }

        private int nextIndex(int index)
        {
            return (index + 1) % que.Length;
        }

        /// <summary>
        /// Only peeks and not dequeue item
        /// </summary>
        /// <returns></returns>
        public T peek()
        {
            if (head==-1)
                throw new BoundedQueueException(QueueStatus.Empty);
            if (currentPeek==-1)
                currentPeek = head;
           
            T elem = que[currentPeek];
            currentPeek = nextIndex(currentPeek);
            return elem;
        }

    }
}
