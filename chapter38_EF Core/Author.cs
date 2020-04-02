using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace chapter38_EF_Core
{
    [Table("Author")]
    public class Author
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Name { get; set; }
    }
}
