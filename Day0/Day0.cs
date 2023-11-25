using System;

namespace Day0
{
    class Day0
    {
        public static void Main(string[] args)
        {
            try
            {
                var p1 = new Task1();
                p1.Run();
                var p2 = new Task2();
                p2.Run();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Main() - {0}", ex);
            }
        }
    }

    public static class ListExt
    {
        public static void Print<T>(this IEnumerable<T> list)
        {
            var s = string.Join(',', list);
            Console.WriteLine(s);
        }
    }
}