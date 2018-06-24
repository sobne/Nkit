using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueueDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var dm = new BookManager();

            //QueueBook.Start(dm);
            QueueBook.Start(dm);
            // Create documents and add them to the DocumentManager
            for (int i = 0; i < 100; i++)
            {
                Book doc = new Book("Doc-" + i.ToString());
                dm.Add(doc);
                Console.WriteLine("Added document {0}", doc.Name);
                Thread.Sleep(new Random().Next(20));
            }

            Console.ReadKey();
        }
    }
}
