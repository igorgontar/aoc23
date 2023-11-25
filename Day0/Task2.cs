using System;
using Utils;

namespace Day0
{
    class Task2
    {
        public void Run()
        {
            var o = new {Name="abc", Surname="xzy"};
            var so = JsonUtils.Stringify(o);
            Console.WriteLine(so);

            Console.WriteLine($"Hello Advent of Code 2023 from {this.GetType()}");
            var list = "1,2,3".Split(',',' ',StringSplitOptions.RemoveEmptyEntries|StringSplitOptions.TrimEntries).Select(x => Int32.Parse(x));
            list.Print();
        }
    }
}