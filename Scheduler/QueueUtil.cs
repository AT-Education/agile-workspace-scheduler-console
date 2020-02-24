using System;
using System.Collections.Generic;
using System.Linq;

namespace Scheduler
{
    public class QueueUtil<T>
    {
        private readonly CircularQueue<T> _queue;
        public QueueUtil(CircularQueue<T> queue)
        {
            _queue = queue;
        }

        public IEnumerable<T> Next(int k=1)
        {
            var result = new List<T>();
            result.AddRange(Enumerable.Range(1,k).Select(i=>_queue.peek()));
            return result;
        }

        public static CircularQueue<T> InitQueue(IEnumerable<T> list)
        {
            var que = new CircularQueue<T>(list.Count());
            foreach (var item in list)
            {
                que.enqueue(item);
            }
            return que;
        }
    }
}
