using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Sample4
{
    class Program
    {
        static async Task Main(string[] args)
        {

            int fn(int input) { Console.WriteLine(input); return input + 1; }

            var step1 = new TransformBlock<int, int>(fn);
            var step2 = new TransformBlock<int, int>(fn);
            var step3 = new ActionBlock<int>(i=> { });

            step1.LinkTo(step2);
            step2.LinkTo(step3);

            _ = step1.Completion
              .ContinueWith(_ =>
              {
                  Console.WriteLine("step 1 complete");
                  step2.Complete();
              });

            _ = step2.Completion.ContinueWith(_ =>
            {
                Console.WriteLine("step 2 complete");
                step3.Complete();
            });
            step1.Post(1);
            step1.Complete();
            
            await step3.Completion;
            Console.WriteLine("done");
        }
    }
}
