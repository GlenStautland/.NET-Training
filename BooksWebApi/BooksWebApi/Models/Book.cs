using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksWebApi.Models
{
    public class Book
    {
        public int BookId { get; set; }

        public string BookName { get; set; }

        public string Category { get; set; }

        public string DateOfSelling { get; set; }

        public string BookPhoto { get; set; }

        public int price { get; set; }
    }
}
