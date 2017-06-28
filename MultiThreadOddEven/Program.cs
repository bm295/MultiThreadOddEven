using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreadOddEven
{
    class Program
    {
        static AutoResetEvent event1 = new AutoResetEvent(false);
        static AutoResetEvent event2 = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            var t1 = Task.Factory.StartNew(() => PrintOddNumbers());
            var t2 = Task.Factory.StartNew(() => PrintEvenNumbers());

            Task.WaitAny(t1, t2);

            Console.ReadLine();
        }

        static void PrintOddNumbers()
        {
            int[] arr = new int[] { 1, 3, 5, 7, 9, 11, 13, 15 };
            foreach (var item in arr)
            {
                Console.WriteLine(item);
                event2.Set();
                event1.WaitOne();
            }
        }

        static void PrintEvenNumbers()
        {
            int[] arr = new int[] { 2, 4, 6, 8, 10, 12, 14 };
            foreach (var item in arr)
            {
                event2.WaitOne();
                Console.WriteLine(item);
                event1.Set();
            }
            event1.Set();
        }
    }
}
