using System;
using System.Threading.Tasks;
using System.Linq;

namespace chapter38_EF_Core
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //Task task = AddBookAsync("aaa","ppp");
            //task = AddBooksAsync();

            //AddAuthors();
            GetList();
            //testLeftJoin();
            Console.ReadLine();
        }

        private static void GetList()
        {
            using (var context = new BookContext())
            {
                var query = from b in context.Set<Book>()
                            join a in context.Set<Author>() on b.BookId equals a.BookId into aTemp
                            from aT in aTemp.DefaultIfEmpty()
                            select new
                            {
                                b.BookId,
                                b.Title,
                                Name = aT == null ? "" :aT.Name,
                            };
                foreach(var item in query)
                {
                    Console.WriteLine($"{item.BookId} - {item.Title} - {item.Name}");
                }
            }
        }
        private static void AddAuthors()
        {
            using (var context = new BookContext())
            {
                var a1 = new Author
                {
                    Name = "asdf",
                    BookId = 2,
                };
                var a2 = new Author
                {
                    Name = "asdf23",
                    BookId = 2,
                };
                var a3 = new Author
                {
                    Name = "asasfddf",
                    BookId = 0,
                };

                context.AddRange(a1,a2,a3);
                var records = context.SaveChanges();
                Console.WriteLine($"{records} records added");
            }
            Console.WriteLine();
        }

        private static async Task AddBookAsync(string title, string publisher)
        {
            using (var context = new BookContext())
            {
                var book = new Book
                {
                    Title = title,
                    Publisher = publisher,
                };
                context.Add(book);
                var records = await context.SaveChangesAsync();
                Console.WriteLine($"{records} records added");
            }
            Console.WriteLine();
        }

        private static async Task AddBooksAsync()
        {
            using (var context = new BookContext())
            {
                var b1 = new Book
                {
                    Title = "t1",
                    Publisher = "p1",
                };
                var b2 = new Book
                {
                    Title = "t3",
                    Publisher = "p3",
                };
                var b3 = new Book
                {
                    Title = "t3",
                    Publisher = "p3",
                };
                context.AddRange(b1,b2,b3);
                var records = await context.SaveChangesAsync();
                Console.WriteLine($"{records} records added");
            }
            Console.WriteLine();
        }

        private static void testLeftJoin()
        {
            var b1 = new Book
            {
                BookId = 1,
                Title = "t1",
                Publisher = "p1",
            };
            var b2 = new Book
            {
                BookId = 2,
                Title = "t3",
                Publisher = "p3",
            };
            var b3 = new Book
            {
                BookId = 3,
                Title = "t3",
                Publisher = "p3",
            };
            var books = new[] { b1,b2,b3 };
            var a1 = new Author
            {
                Name = "asdf",
                BookId = 2,
            };
            var a2 = new Author
            {
                Name = "asdf23",
                BookId = 2,
            };
            var a3 = new Author
            {
                Name = "asasfddf",
                BookId = 0,
            };
            var alist = new[]{
                a1,a2,a3
            };
            var query = from b in books.AsQueryable()
                        join a in alist.AsQueryable() on b.BookId equals a.BookId into aTemp
                        from aT in aTemp.DefaultIfEmpty()
                        select new
                        {
                            b.BookId,
                            b.Title,
                            Name = aT == null ? "" : aT.Name,
                        };
            foreach (var item in query)
            {
                Console.WriteLine($"{item.BookId} - {item.Title} - {item.Name}");
            }
        }
    }
}
