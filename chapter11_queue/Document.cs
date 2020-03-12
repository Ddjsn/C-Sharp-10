using System;
using System.Collections.Generic;
using System.Text;

namespace chapter11_queue
{
    public class Document
    {
        public string Title { get; private set; }
        public string Content { get; private set; }

        public Document(string title, string content)
        {
            Title = title;
            Content = content;
        }
    }
}
