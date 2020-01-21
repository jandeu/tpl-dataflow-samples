using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var r = new Random();
            var actionBlock = new ActionBlock<string>(input =>
            {
                Thread.Sleep(r.Next(100, 2000));
                Console.WriteLine($"Done: {input}, threadId: {Thread.CurrentThread.ManagedThreadId}");
            }, new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 1, EnsureOrdered = true });

            for (int i = 0; i < 1000; i++)
            {
                // Thread.Sleep(r.Next(100, 200));
                var msg = $"msg_{i}";

                Console.WriteLine($"Adding {msg}");
                actionBlock.Post(msg);
            }

            Console.WriteLine("action block input count: " + actionBlock.InputCount);

            while (true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
