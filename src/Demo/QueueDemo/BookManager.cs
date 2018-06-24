using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueueDemo
{
    public class Book
    {
        public string Name { get; private set; }

        public Book(string name)
        {
            this.Name = name;
        }
    }
    public class BookManager
    {
        private readonly Queue<Book> _queue = new Queue<Book>();
        public void Add(Book item)
        {
            lock (this)
            {
                _queue.Enqueue(item);
            }
        }
        public Book Get()
        {
            Book doc = null;
            lock (this)
            {
                if (this.IsAvailable)
                    doc = _queue.Dequeue();
            }
            return doc;
        }
        public bool IsAvailable
        {
            get
            {
                lock (this)
                {
                    return _queue.Count > 0;
                }

            }
        }
    }

    public class QueueBook
    {
        public static void Start(BookManager mgr)
        {
            Task.Factory.StartNew(new QueueBook(mgr).Run);
        }

        protected QueueBook(BookManager mgr)
        {
            if (mgr == null)
                throw new ArgumentNullException("qm");
            _mgr = mgr;
        }

        private BookManager _mgr;

        protected void Run()
        {
            while (true)
            {
                if (_mgr.IsAvailable)
                {
                    Book item = _mgr.Get();
                    if (item != null)
                        Console.WriteLine("Get item {0}", item.Name);
                }
                Thread.Sleep(new Random().Next(20));
            }
        }
    }
}
