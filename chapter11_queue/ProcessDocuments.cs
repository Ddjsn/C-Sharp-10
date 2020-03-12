using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace chapter11_queue
{
    public class ProcessDocuments
    {
        private DocumentManager _documentManager;
        protected ProcessDocuments(DocumentManager documentManager)
        {
            if(documentManager == null)
            {
                throw new ArgumentNullException(nameof(documentManager));
            }
            _documentManager = documentManager;
        }

        public static void Start(DocumentManager dm)
        {
            Task.Run(new ProcessDocuments(dm).Run);
        }

        protected async Task Run()
        {
            while (true)
            {
                if (_documentManager.IsDocumentAvailable)
                {
                    Document doc = _documentManager.GetDocument();
                    Console.WriteLine($"Processing document {doc.Title} t {DateTime.Now.ToString("MM:ss fff")}");
                }
                Console.WriteLine($"t {DateTime.Now.ToString("MM:ss fff")}");
                await Task.Delay(100);
            }
        }

    }
}
