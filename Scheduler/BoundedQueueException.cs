using System;
using System.Runtime.Serialization;

namespace Scheduler
{
    [Serializable]
    internal class BoundedQueueException : Exception
    {
        private QueueStatus _queueStatus;

        public BoundedQueueException()
        {
        }

        public BoundedQueueException(QueueStatus status)
        {
            _queueStatus = status;
        }

        public BoundedQueueException(string message) : base(message)
        {
        }

        public BoundedQueueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BoundedQueueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}