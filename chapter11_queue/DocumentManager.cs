using System;
using System.Collections.Generic;
using System.Text;

namespace chapter11_queue
{
    public class DocumentManager
    {
        private readonly Queue<Document> _documentQueue = new Queue<Document>();

        public void AddDocument(Document document)
        {
            lock (this)
            {
                _documentQueue.Enqueue(document);
            }
        }

        public Document GetDocument()
        {
            Document document = null;
            lock (this)
            {
                document = _documentQueue.Dequeue();
            }

            return document;
        }

        public bool IsDocumentAvailable => _documentQueue.Count > 0;
    }
}
