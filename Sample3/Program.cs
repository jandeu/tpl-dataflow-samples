using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var createRandomNumber = new TransformBlock<int, int>(seed =>
            {
                var random = new Random(seed);
                return random.Next();
            });

            var processOddNumbers = new TransformBlock<int, string>(input =>
            {
                Console.WriteLine($"Processed odd number {input}");
                return "Odd";
            });

            var processEvenNumbers = new TransformBlock<int, string>(input =>
            {
                Console.WriteLine($"Processed even number {input}");
                return "Even";
            });

            var printResult = new ActionBlock<string>((input) => Console.WriteLine(input));

            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };
            createRandomNumber.LinkTo(processEvenNumbers,linkOptions, x => x % 2 == 0);
            createRandomNumber.LinkTo(processOddNumbers, linkOptions, x => x % 2 == 1);

            processEvenNumbers.LinkTo(printResult, linkOptions);
            processOddNumbers.LinkTo(printResult, linkOptions);

            createRandomNumber.Post(5);
            createRandomNumber.Post(87);
            createRandomNumber.Post(878);
            createRandomNumber.Post(877);
            createRandomNumber.Post(524);
            createRandomNumber.Post(975);
            createRandomNumber.Post(14);
            createRandomNumber.Post(82);
            createRandomNumber.Complete();
            await printResult.Completion;

        }
    }
}
