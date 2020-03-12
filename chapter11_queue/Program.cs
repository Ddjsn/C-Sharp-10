using System;
using System.Threading;

namespace chapter11_queue
{
    class Program
    {
        static void Main(string[] args)
        {
            DocumentManager documentManager = new DocumentManager();

            ProcessDocuments.Start(documentManager);
            for(var i = 0; i < 100; i++)
            {
                var doc = new Document($"Doc {i.ToString()}", "Content");
                documentManager.AddDocument(doc);
                Console.WriteLine($"Added document {doc.Title} t {DateTime.Now.ToString("MM:ss fff")}");
                Thread.Sleep(100);
            }
            Console.WriteLine($"end");
            Console.ReadLine();
        }
    }
}
