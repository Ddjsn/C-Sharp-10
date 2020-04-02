using System;
using System.Linq;

namespace chapter13_Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            LeftJoin();
        }

        public static void LeftJoin()
        {
            var ids = new[] { "1", "3", "4", "5" };
            var list = new[]{
                new { id = "1", name = "t1" },
                new {id = "3",name = "t3" }
            };
            var query = (from i in ids
                         join l in list on i equals l.id into lTemp
                         from lT in lTemp.DefaultIfEmpty()
                         select new
                         {
                             i,
                             id = lT == null ? "0" : lT.id,
                             lT.name,
                         }).ToList();
            foreach (var item in query)
            {
                Console.WriteLine($"{item.i} - {item.id} - {item.name}");
            }
        }
    }
}
