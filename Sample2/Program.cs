using System;
using System.Threading;
using System.Threading.Tasks.Dataflow;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var buffer = new BufferBlock<int>();
            buffer.Post(1);
            buffer.Post(2);
            buffer.Post(3);

            Console.WriteLine("buffer: " + buffer.Receive());
            Console.WriteLine("buffer: " + buffer.Receive());
            Console.WriteLine("buffer: " + buffer.Receive());

            var broadcast = new BroadcastBlock<int>(i => i);
            broadcast.Post(1);

            Console.WriteLine("broadcast: " + broadcast.Receive());
            Console.WriteLine("broadcast: " + broadcast.Receive());
            Console.WriteLine("broadcast: " + broadcast.Receive());


            var writeOnce = new WriteOnceBlock<int>(i => i);
            writeOnce.Post(1);
            writeOnce.Post(2);
            writeOnce.Post(3);

            Console.WriteLine("write-once: " + writeOnce.Receive());
        }
    }
}
